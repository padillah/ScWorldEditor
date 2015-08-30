using System;
using System.Collections.Generic;
using System.IO;
using ICSharpCode.SharpZipLib;

//TODO: World should be Image 768x768 (256 * 3)

namespace SCWorldEdit.Rules
{
	public interface IScRulesEngine
	{
		ScWorld LoadWorld(string argFileName);
	}

	public class ScRulesEngine : IScRulesEngine
	{
		public ScWorld LoadWorld(string argFileName)
		{
			//Create Temp directory
			DirectoryInfo localDirectory = Directory.CreateDirectory("ScWorldEdit");
			string newFileName = localDirectory.FullName + "\\Temp.zip";

			File.Copy(argFileName, newFileName, true);

			//Open the zip file.
			//Save the three files to the Temp dir
			ZipWrapper.ExtractZipFile(newFileName, localDirectory.FullName);
			
			//Open the "Chunks.dat" file.
			string chunkFileName = localDirectory.FullName + "\\Chunks.dat";
			ScWorld localWorld = new ScWorld(chunkFileName);

			var fileStream = File.Open(chunkFileName, FileMode.Open, FileAccess.ReadWrite);
			//var writer = new BinaryWriter(fileStream);
			var file = new BinaryReader(fileStream);

            Dictionary<ChunkPosition, Int32> chunkOffsetDirectory = new Dictionary<ChunkPosition, Int32>();
            for (int i = 0; i < 65536; ++i)
			{
                Int32 chunkX = file.ReadInt32();
                Int32 chunkY = file.ReadInt32();
                Int32 offset = file.ReadInt32();
                ChunkPosition position = new ChunkPosition(chunkX, chunkY);
                chunkOffsetDirectory.Add(position, offset);
			}
            //TODO: Fill ScWorld class.
            foreach(var pair in chunkOffsetDirectory)
            {
                fileStream.Position = pair.Value;
                UInt32 magic1 = file.ReadUInt32();
                UInt32 magic2 = file.ReadUInt32();
                if (magic1 != 0xDEADBEEF || magic2 != 0xFFFFFFFF)
                    throw new FormatException("Not a Chunks.dat file.");
                Int32 chunkX = file.ReadInt32();
                Int32 chunkY = file.ReadInt32();
                if (chunkX != pair.Key.ChunkX || chunkY != pair.Key.ChunkY)
                    throw new InvalidDataException("Chunk header does not match chunk directory.");
                Chunk chunk = new Chunk(chunkX, chunkY);
                for (int i = 0; i < chunk.Blocks.Length; ++i)
                {
                    chunk.Blocks[i].BlockType = file.ReadByte();
                    chunk.Blocks[i].BlockData = file.ReadByte();
                }
            }

			return localWorld;
		}
	}
}
