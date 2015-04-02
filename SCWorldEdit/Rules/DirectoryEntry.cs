using System;

namespace SCWorldEdit.Rules
{
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
}