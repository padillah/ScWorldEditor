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

    public class ScChunk
    {
        public Int32 ChunkX { get; private set; }
        public Int32 ChunkZ { get; private set; }

        const int SizeX = 16;
        const int SizeY = 128;
        const int SizeZ = 16;
        public ScBlock[] Blocks { get; private set; }

        public ScChunk(Int32 chunkX = -1, Int32 chunkZ = -1)
        {
            ChunkX = chunkX;
            ChunkZ = chunkZ;
            Blocks = new ScBlock[SizeX * SizeY * SizeZ];
            for (var i = 0; i < Blocks.Length; ++i)
            {
                Blocks[i].BlockType = 0;// Air
                Blocks[i].BlockData = 0;// No data
            }
        }

        private int CalculateBlockIndex(int x, int y, int z)
        {
            return y + x * SizeY + z * SizeY * SizeX;
        }

        public ScBlock GetBlockInChunk(int x, int y, int z)
        {
            int index = CalculateBlockIndex(x, y, z);
            return Blocks[index];
        }

        public void SetBlockInChunk(int x, int y, int z, ScBlock block)
        {
            int index = CalculateBlockIndex(x, y, z);
            Blocks[index] = block;
        }
    }
}