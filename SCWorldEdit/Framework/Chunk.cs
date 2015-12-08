using System;

namespace SCWorldEdit.Framework
{
    public struct ChunkPosition
    {
        public Int32 ChunkX;
        public Int32 ChunkY;

        public ChunkPosition(Int32 chunkX, Int32 chunkY)
        {
            ChunkX = chunkX;
            ChunkY = chunkY;
        }
    }

    public struct Block
    {
        public Byte BlockType;
        public Byte BlockData;

        public Block(Byte blockType, Byte blockData)
        {
            BlockType = blockType;
            BlockData = blockData;
        }
    }

    public class Chunk
    {
        public Int32 ChunkX { get; private set; }
        public Int32 ChunkY { get; private set; }
        const int SizeX = 16;
        const int SizeY = 128;
        const int SizeZ = 16;
        public Block[] Blocks { get; private set; }

        public Chunk(Int32 chunkX = -1, Int32 chunkY = -1)
        {
            ChunkX = chunkX;
            ChunkY = chunkY;
            Blocks = new Block[SizeX * SizeY * SizeZ];
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

        public Block GetBlockInChunk(int x, int y, int z)
        {
            int index = CalculateBlockIndex(x, y, z);
            return Blocks[index];
        }

        public void SetBlockInChunk(int x, int y, int z, Block block)
        {
            int index = CalculateBlockIndex(x, y, z);
            Blocks[index] = block;
        }
    }
}