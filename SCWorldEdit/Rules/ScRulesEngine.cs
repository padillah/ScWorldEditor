using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib;

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

			for (int i = 0; i < 65536; i++)
			{
				//TODO: Fill ScWorld class.
				localWorld.AddDirectoryEntry(file.ReadInt32(), file.ReadInt32(), file.ReadInt32());
			}

			return localWorld;
		}
	}
}
