using System;

namespace SCWorldEdit.Framework
{
    public struct ScChunkPosition
    {
        public Int32 ChunkX;
        public Int32 ChunkZ;

        public ScChunkPosition(Int32 chunkX, Int32 chunkZ)
        {
            ChunkX = chunkX;
            ChunkZ = chunkZ;
        }
    }
}