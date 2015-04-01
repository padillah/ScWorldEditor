using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Windows;
using System.Windows.Input;
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
			get { return _worldImage.Width*2; }
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
			DirectoryEntries.Add(new DirectoryEntry(argChunkX, argChunkY, argOffset));

			//If the offset is zero the chunk doesn't exist in the file.
			if (argOffset != 0)
			{
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
			//localRan = new Random();
			//WorldImage = new WriteableBitmap(512, 512, 96.0, 96.0, PixelFormats.Bgr24, BitmapPalettes.Halftone256);
			WriteableBitmap localBitmap = new WriteableBitmap((_maxX*2) + 1, (_maxY*2) + 1, 96.0, 96.0, PixelFormats.Bgr24, BitmapPalettes.Halftone256);

			int localRange = _maxX * _maxY * localBitmap.BackBufferStride;
			byte[] _worldBytes = new byte[localRange];

			//localRan.NextBytes(_worldBytes);
			//for (int i = 0; i < localRange; i++)
			//{
			//////	_worldBytes[i] = 128;
			//}
			foreach (DirectoryEntry currentEntry in DirectoryEntries)
			{
				//offset = (argY * stride) + (argX * bpp) + colorByte
				int localByteOffset = (currentEntry.ChunkY * localBitmap.BackBufferStride) + (currentEntry.ChunkX * _bpp) + 0; //We're just using RED right now.
				_worldBytes[localByteOffset] = 254;
				_worldBytes[localByteOffset+1] = 254;
			}
			localBitmap.WritePixels(new Int32Rect(0, 0, _maxX, _maxY), _worldBytes, localBitmap.BackBufferStride, 0, 0);
			//WorldImage.WritePixels(new Int32Rect(0, 0, 512, 512), _worldBytes, 512 * 3, 0);

			return localBitmap;
		}

		// The DrawPixel method updates the WriteableBitmap by using 
		// unsafe code to write a pixel into the back buffer. 
		private void DrawPixel(int argX, int argY, int argColor)
		{
			//int column = (int)e.GetPosition(i).X;
			//int row = (int)e.GetPosition(i).Y;

			// Reserve the back buffer for updates.
			WorldImage.Lock();

			unsafe
			{
				// Get a pointer to the back buffer. 
				int pBackBuffer = (int)WorldImage.BackBuffer;

				// Find the address of the pixel to draw.
				pBackBuffer += argY * WorldImage.BackBufferStride;
				pBackBuffer += argX * 4;

				// Compute the pixel's color. 
				int color_data = 255 << 16; // R
				color_data |= 128 << 8;   // G
				color_data |= 255 << 0;   // B 

				// Assign the color data to the pixel.
				*((int*)pBackBuffer) = color_data;
			}

			// Specify the area of the bitmap that changed.
			WorldImage.AddDirtyRect(new Int32Rect(argX, argY, 1, 1));

			// Release the back buffer and make it available for display.
			WorldImage.Unlock();
		}

		public BitmapSource FromArray(byte[] data, int w, int h, int ch)
		{
			PixelFormat format = PixelFormats.Default;

			if (ch == 1) format = PixelFormats.Gray8; //grey scale image 0-255
			if (ch == 3) format = PixelFormats.Bgr24; //RGB
			if (ch == 4) format = PixelFormats.Bgr32; //RGB + alpha


			WriteableBitmap wbm = new WriteableBitmap(w, h, 96, 96, format, null);
			wbm.WritePixels(new Int32Rect(0, 0, w, h), data, ch * w, 0);

			return wbm;
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