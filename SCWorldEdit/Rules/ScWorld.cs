using System.Collections.Generic;
using System.Dynamic;

namespace SCWorldEdit.Rules
{
	public class ScWorld
	{
		public List<DirectoryEntry> DirectoryEntries { get; set; }
		public string FileName { get; private set; }
		
		public ScWorld(string argFileName)
		{
			FileName = argFileName;
			DirectoryEntries = new List<DirectoryEntry>();
		}

		public void AddDirectoryEntry(int argChunkX, int argChunkY, int argOffset)
		{
			DirectoryEntries.Add(new DirectoryEntry(argChunkX,argChunkY,argOffset));
		}
	}

	public class DirectoryEntry
	{
		public int ChunkX { get; private set; }
		public int ChunkY { get; private set; }
		public int Offset { get; private set; }

		public DirectoryEntry(int argChunkX, int argChunkY, int argOffset)
		{
			ChunkX = argChunkX;
			ChunkY = argChunkY;
			Offset = argOffset;
		}
	}
}