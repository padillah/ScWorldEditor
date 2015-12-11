using System;
using System.IO;
using System.Windows.Media.Media3D;

namespace SCWorldEdit.Framework
{

    public class ScChunk
    {
        public Int32 ChunkX { get; private set; }
        public Int32 ChunkZ { get; private set; }

        const int SizeX = 16;
        const int SizeY = 128;
        const int SizeZ = 16;
        public ScBlock[] Blocks { get; private set; }

        public ScChunk(ScChunkPosition argChunkPosition, byte[] argChunkInfo)
        {
            //TODO: Thread this (now that the fileread is done).
            Blocks = new ScBlock[SizeX * SizeY * SizeZ];
            /**/

            //Read the next two 32-byte pieces.
            UInt32 magic1 = BitConverter.ToUInt32(argChunkInfo, 0);
            UInt32 magic2 = BitConverter.ToUInt32(argChunkInfo, 4);

            //Check that the Magic bytes are valid
            if (magic1 != 0xDEADBEEF || magic2 != 0xFFFFFFFF)
                throw new FormatException("Not a Chunks.dat file.");

            //Read the next two 32-byte pieces as the chunk X and Z (there is no Y because the entire height is always described)
            Int32 chunkX = BitConverter.ToInt32(argChunkInfo, 8);

            //Int32 chunkZ = file.ReadInt32();
            Int32 chunkZ = BitConverter.ToInt32(argChunkInfo, 12);

            //Validate that the chunk being read is the same as the chunk at the offset.
            if (chunkX != argChunkPosition.ChunkX || chunkZ != argChunkPosition.ChunkZ)
                throw new InvalidDataException("Chunk header does not match chunk directory.");

            ChunkX = chunkX;
            ChunkZ = chunkZ;

            /**/
            int infoIndex = 16;
            for (var blockYPosition = 0; blockYPosition < Blocks.Length; ++blockYPosition)
            {
                //Need to calc each block position.
                //Start at ChunkX, 0, ChunkZ
                //For every 128 ChunkY we increment ChunkZ
                //For every 16 ChunkZ we reset ChunkZ and increment ChunkX

                Blocks[blockYPosition] = new ScBlock(new Point3D(ChunkX, 0, ChunkZ), argChunkInfo[infoIndex++], argChunkInfo[infoIndex++] ); //Block Type, Block Data                
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