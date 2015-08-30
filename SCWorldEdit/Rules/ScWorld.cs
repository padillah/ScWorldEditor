using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SCWorldEdit.Rules
{
    public class ScWorld
    {
        public Dictionary<ChunkPosition, Chunk> ChunkDictionary { get; set; }
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

        public ScWorld(string argFileName)
        {
            Random localRan;
            FileName = argFileName;
            ChunkDictionary = new Dictionary<ChunkPosition, Chunk>();

            _worldImage = null;
        }

        public void AddChunk(Chunk chunk)
        {
            ChunkDictionary.Add(new ChunkPosition(chunk.ChunkX, chunk.ChunkY), chunk);

            if (_minX == 0)
                _minX = chunk.ChunkX;

            if (_minY == 0)
                _minY = chunk.ChunkY;
            _minX = Math.Min(chunk.ChunkX, _minX);
            _minY = Math.Min(chunk.ChunkY, _minY);
            _maxX = Math.Max(chunk.ChunkX, _maxX);
            _maxY = Math.Max(chunk.ChunkY, _maxY);
        }

        public Block GetBlock(int x, int y, int z)
        {
            int blockX = x % 16;
            int blockZ = z % 16;
            int chunkX = (x - blockX) / 16;
            int chunkY = (z - blockZ) / 16;
            ChunkPosition position = new ChunkPosition(chunkX, chunkY);
            if (!ChunkDictionary.ContainsKey(position))
                throw new ArgumentException("Chunk not found.");
            Chunk chunk = ChunkDictionary[position];
            Block block = chunk.GetBlockInChunk(blockX, y, blockZ);
            return block;
        }

        public void SetBlock(int x, int y, int z, Block block)
        {
            int blockX = x % 16;
            int blockZ = z % 16;
            int chunkX = (x - blockX) / 16;
            int chunkY = (z - blockZ) / 16;
            ChunkPosition position = new ChunkPosition(chunkX, chunkY);
            if (!ChunkDictionary.ContainsKey(position))
                throw new ArgumentException("Chunk not found.");
            Chunk chunk = ChunkDictionary[position];
            chunk.SetBlockInChunk(blockX, y, blockZ, block);
        }

        private WriteableBitmap CreateImage()
        {
            WriteableBitmap localBitmap = new WriteableBitmap((_maxX * 2) + 1, (_maxY * 2) + 1, 96.0, 96.0, PixelFormats.Bgr24, BitmapPalettes.Halftone256);

            int localRange = _maxX * _maxY * localBitmap.BackBufferStride;
            byte[] _worldBytes = new byte[localRange];

            foreach (var pair in ChunkDictionary)
            {
                //offset = (argY * stride) + (argX * bpp) + colorByte
                int localByteOffset = (pair.Value.ChunkY * localBitmap.BackBufferStride) + (pair.Value.ChunkX * _bpp) + 0; //We're just using RED right now.
                _worldBytes[localByteOffset] = 254;
                _worldBytes[localByteOffset + 1] = 254;
            }
            localBitmap.WritePixels(new Int32Rect(0, 0, _maxX, _maxY), _worldBytes, localBitmap.BackBufferStride, 0, 0);

            return localBitmap;
        }
    }
}