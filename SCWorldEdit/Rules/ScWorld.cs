using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SCWorldEdit.Rules
{
    public class ScWorld
    {
        public List<DirectoryEntry> DirectoryEntries { get; set; }
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
            DirectoryEntries = new List<DirectoryEntry>();

            _worldImage = null;
        }

        public void AddDirectoryEntry(Int32 argChunkX, Int32 argChunkY, Int32 argOffset)
        {
            //If the offset is zero the chunk doesn't exist in the file.
            if (argOffset != 0)
            {
                DirectoryEntries.Add(new DirectoryEntry(argChunkX, argChunkY, argOffset));

                if (_minX == 0)
                    _minX = argChunkX;

                if (_minY == 0)
                    _minY = argChunkY;

                _minX = Math.Min(argChunkX, _minX);
                _minY = Math.Min(argChunkY, _minY);
                _maxX = Math.Max(argChunkX, _maxX);
                _maxY = Math.Max(argChunkY, _maxY);

            }
        }

        private WriteableBitmap CreateImage()
        {
            WriteableBitmap localBitmap = new WriteableBitmap((_maxX * 2) + 1, (_maxY * 2) + 1, 96.0, 96.0, PixelFormats.Bgr24, BitmapPalettes.Halftone256);

            int localRange = _maxX * _maxY * localBitmap.BackBufferStride;
            byte[] _worldBytes = new byte[localRange];

            foreach (DirectoryEntry currentEntry in DirectoryEntries)
            {
                //offset = (argY * stride) + (argX * bpp) + colorByte
                int localByteOffset = (currentEntry.ChunkY * localBitmap.BackBufferStride) + (currentEntry.ChunkX * _bpp) + 0; //We're just using RED right now.
                _worldBytes[localByteOffset] = 254;
                _worldBytes[localByteOffset + 1] = 254;
            }
            localBitmap.WritePixels(new Int32Rect(0, 0, _maxX, _maxY), _worldBytes, localBitmap.BackBufferStride, 0, 0);

            return localBitmap;
        }
    }
}