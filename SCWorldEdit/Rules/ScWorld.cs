using System;
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

		public void AddDirectoryEntry(Int32 argChunkX, Int32 argChunkY, Int32 argOffset)
		{
			DirectoryEntries.Add(new DirectoryEntry(argChunkX,argChunkY,argOffset));
		}
	}

	public class DirectoryEntry
	{
		private Chunk _chunk;
		public Int32 ChunkX { get; private set; }
		public Int32 ChunkY { get; private set; }
		public Int32 Offset { get; private set; }

		public Chunk Chunk
		{
			get
			{
				if (_chunk == null)
				{
					_chunk = new Chunk();
				}
				return _chunk;
			}
			set { _chunk = value; }
		}

		public DirectoryEntry(int argChunkX, int argChunkY, int argOffset)
		{
			ChunkX = argChunkX;
			ChunkY = argChunkY;
			Offset = argOffset;
		}
	}

	public class Chunk
	{
		
	}
}