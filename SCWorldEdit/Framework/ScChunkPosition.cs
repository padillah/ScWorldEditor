using System;

namespace SCWorldEdit.Framework
{
    public struct ScChunkPosition
    {
        public Int32 ChunkX { get; private set; }
        public Int32 ChunkZ { get; private set; }
        public Int32 Offset { get; private set; }

        public ScChunkPosition(Int32 chunkX, Int32 chunkZ, Int32 argOffset)
        {
            ChunkX = chunkX;
            ChunkZ = chunkZ;
            Offset = argOffset;
        }
    }
}