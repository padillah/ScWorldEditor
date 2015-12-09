using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace SCWorldEdit.Framework
{
    public class ScWorld
    {
        public Dictionary<ScChunkPosition, ScChunk> ChunkDictionary { get; set; }
        public string FileName { get; private set; }

        public WriteableBitmap WorldImage
        {
            get
            {
                if (_worldImage == null)
                {
                    _worldImage = CreateImage();
                }
                return _worldImage;
            }
            set { _worldImage = value; }
        }

        public double WorldHeight
        {
            get { return _worldImage.Height * 2; }
        }

        public double WorldWidth
        {
            get { return _worldImage.Width * 2; }
        }

        private Int32 _minX = 0;
        private Int32 _minY = 0;
        private Int32 _maxX = 0;
        private Int32 _maxY = 0;
        private const int _bpp = 3;

        private WriteableBitmap _worldImage;

        public Point3D CameraPosition { get; set; }

        public Vector3D CameraLook { get; set; }

        public Camera WorldCamera { get; set; }

        public Model3DGroup WorldModelGroup { get; set; }

        public ScWorld()
        {
            CameraPosition = new Point3D(3, 2, -4);
            CameraLook = new Vector3D(-2.5, -1.5, 3.5);

            WorldCamera = new PerspectiveCamera(
                CameraPosition,
                CameraLook,  //Need to lookat 0.5, 0.5, -0.5 to see the center of the cube.
                new Vector3D(0, 1, 0), 45);

            DirectionalLight localLight = new DirectionalLight(Colors.White, new Vector3D(1, -1, -1));

            WorldModelGroup = new Model3DGroup();

            WorldModelGroup.Children.Add(localLight);

            ScBlock localBlock = new ScBlock(new Point3D(0, 0, 0));
            ScBlock localBlock2 = new ScBlock(new Point3D(2, 0, 0));
            ScBlock localBlock3 = new ScBlock(new Point3D(1, 1, 0));

            WorldModelGroup.Children.Add(localBlock.BlockModel);
            WorldModelGroup.Children.Add(localBlock2.BlockModel);
            WorldModelGroup.Children.Add(localBlock3.BlockModel);

        }

        public void Load(string argFileName)
        {
            FileName = argFileName;
            ChunkDictionary = new Dictionary<ScChunkPosition, ScChunk>();

            /**/
            using (var fileStream = File.Open(FileName, FileMode.Open, FileAccess.ReadWrite))
            {
                using (var file = new BinaryReader(fileStream))
                {
                    Dictionary<ScChunkPosition, Int32> chunkOffsetDirectory = new Dictionary<ScChunkPosition, Int32>();
                    for (int i = 0; i < 65536; ++i)
                    {
                        Int32 chunkX = file.ReadInt32();
                        Int32 chunkY = file.ReadInt32();
                        Int32 offset = file.ReadInt32();

                        if (offset > 0) //If the offset is zero there is not really a chunk there. The game engine will regenerate the chunk, it doesn't need to store it.
                        {
                            ScChunkPosition position = new ScChunkPosition(chunkX, chunkY);
                            chunkOffsetDirectory.Add(position, offset);
                        }
                    }

                    //TODO: Fill ScWorld class.
                    foreach (var pair in chunkOffsetDirectory)
                    {
                        //Go to the chunk offset
                        fileStream.Position = pair.Value;

                        //Read the next two 32-byte pieces.
                        UInt32 magic1 = file.ReadUInt32();
                        UInt32 magic2 = file.ReadUInt32();

                        //Check that the Magic bytes are valid
                        if (magic1 != 0xDEADBEEF || magic2 != 0xFFFFFFFF)
                            throw new FormatException("Not a Chunks.dat file.");

                        //Read the next two 32-byte pieces as the chunk X and Y (there is no Z because the entire height is always described)
                        Int32 chunkX = file.ReadInt32();
                        Int32 chunkZ = file.ReadInt32();

                        //Validate that the chunk being read is the same as the chunk at the offset.
                        if (chunkX != pair.Key.ChunkX || chunkZ != pair.Key.ChunkZ)
                            throw new InvalidDataException("Chunk header does not match chunk directory.");

                        //Make a new chunk for this position
                        ScChunk chunk = new ScChunk(chunkX, chunkZ);

                        //Read the bytes into the chunk
                        for (int i = 0; i < chunk.Blocks.Length; ++i)
                        {
                            chunk.Blocks[i].BlockType = file.ReadByte();
                            chunk.Blocks[i].BlockData = file.ReadByte();
                        }

                        AddChunk(chunk);
                    }
                }
            }
            /**/

            _worldImage = CreateImage();
        }

        public void AddChunk(ScChunk chunk)
        {
            ChunkDictionary.Add(new ScChunkPosition(chunk.ChunkX, chunk.ChunkZ), chunk);

            if (_minX == 0)
                _minX = chunk.ChunkX;

            if (_minY == 0)
                _minY = chunk.ChunkZ;
            _minX = Math.Min(chunk.ChunkX, _minX);
            _minY = Math.Min(chunk.ChunkZ, _minY);
            _maxX = Math.Max(chunk.ChunkX, _maxX);
            _maxY = Math.Max(chunk.ChunkZ, _maxY);
        }

        public ScBlock GetBlock(int x, int y, int z)
        {
            int blockX = x % 16;
            int blockZ = z % 16;
            int chunkX = (x - blockX) / 16;
            int chunkZ = (z - blockZ) / 16;
            ScChunkPosition position = new ScChunkPosition(chunkX, chunkZ);
            if (!ChunkDictionary.ContainsKey(position))
                throw new ArgumentException("Chunk not found.");
            ScChunk chunk = ChunkDictionary[position];
            ScBlock block = chunk.GetBlockInChunk(blockX, y, blockZ);
            return block;
        }

        public void SetBlock(int x, int y, int z, ScBlock block)
        {
            int blockX = x % 16;
            int blockZ = z % 16;
            int chunkX = (x - blockX) / 16;
            int chunkZ = (z - blockZ) / 16;
            ScChunkPosition position = new ScChunkPosition(chunkX, chunkZ);
            if (!ChunkDictionary.ContainsKey(position))
                throw new ArgumentException("Chunk not found.");
            ScChunk chunk = ChunkDictionary[position];
            chunk.SetBlockInChunk(blockX, y, blockZ, block);
        }

        private WriteableBitmap CreateImage()
        {
            //TODO: The bitmap should be set to either a percentage of the existing, or add a simple border. 2x is too large now and will only get larger as "real" maps are opened.
            WriteableBitmap localBitmap = new WriteableBitmap((_maxX * 2) + 1, (_maxY * 2) + 1, 96.0, 96.0, PixelFormats.Bgr24, BitmapPalettes.Halftone256);

            return localBitmap;

            //int localRange = _maxX * _maxY * localBitmap.BackBufferStride;
            //byte[] _worldBytes = new byte[localRange];

            //foreach (var pair in ChunkDictionary)
            //{
            //    //offset = (argY * stride) + (argX * bpp) + colorByte
            //    int localByteOffset = (pair.Value.ChunkY * localBitmap.BackBufferStride) + (pair.Value.ChunkX * _bpp) + 0; //We're just using RED right now.
            //    _worldBytes[localByteOffset] = 254;
            //    _worldBytes[localByteOffset + 1] = 254;
            //}

            //localBitmap.WritePixels(new Int32Rect(0, 0, _maxX, _maxY), _worldBytes, localBitmap.BackBufferStride, 0, 0);

            //return localBitmap;
        }
    }
}