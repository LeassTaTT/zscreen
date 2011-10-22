﻿#region License Information (GPL v2)

/*
    ZUploader - A program that allows you to upload images, texts or files
    Copyright (C) 2008-2011 ZScreen Developers

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/

#endregion License Information (GPL v2)

using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ScreenCapture
{
    public unsafe class UnsafeBitmap : IDisposable
    {
        public ColorBgra* Pointer { get; private set; }

        public bool IsLocked { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public int PixelCount
        {
            get
            {
                return Width * Height;
            }
        }

        private Bitmap bitmap;
        private BitmapData bitmapData;

        public UnsafeBitmap(Bitmap bitmap, bool lockBitmap = false, ImageLockMode imageLockMode = ImageLockMode.ReadWrite)
        {
            this.bitmap = bitmap;
            Width = bitmap.Width;
            Height = bitmap.Height;

            if (lockBitmap) Lock(imageLockMode);
        }

        public void Lock(ImageLockMode imageLockMode = ImageLockMode.ReadWrite)
        {
            if (!IsLocked)
            {
                IsLocked = true;
                bitmapData = bitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                Pointer = (ColorBgra*)bitmapData.Scan0.ToPointer();
            }
        }

        public void Unlock()
        {
            if (IsLocked)
            {
                bitmap.UnlockBits(bitmapData);
                bitmapData = null;
                Pointer = null;
                IsLocked = false;
            }
        }

        public static bool operator ==(UnsafeBitmap bmp1, UnsafeBitmap bmp2)
        {
            return Compare(bmp1, bmp2);
        }

        public static bool operator !=(UnsafeBitmap bmp1, UnsafeBitmap bmp2)
        {
            return !Compare(bmp1, bmp2);
        }

        public override bool Equals(object obj)
        {
            return obj != null && obj is UnsafeBitmap && (UnsafeBitmap)obj == this;
        }

        public override int GetHashCode()
        {
            return PixelCount;
        }

        public static bool Compare(UnsafeBitmap bmp1, UnsafeBitmap bmp2)
        {
            int pixelCount = bmp1.PixelCount;

            if (pixelCount != bmp2.PixelCount) return false;

            if (!bmp1.IsLocked) bmp1.Lock(ImageLockMode.ReadOnly);
            if (!bmp2.IsLocked) bmp2.Lock(ImageLockMode.ReadOnly);

            ColorBgra* pointer1 = bmp1.Pointer;
            ColorBgra* pointer2 = bmp2.Pointer;

            for (int i = 0; i < pixelCount; i++)
            {
                if (pointer1->Bgra != pointer2->Bgra) return false;

                pointer1++;
                pointer2++;
            }

            return true;
        }

        public ColorBgra GetPixel(int i)
        {
            return Pointer[i];
        }

        public ColorBgra GetPixel(int x, int y)
        {
            return Pointer[x + y * Width];
        }

        public void SetPixel(int i, ColorBgra color)
        {
            Pointer[i] = color;
        }

        public void SetPixel(int x, int y, ColorBgra color)
        {
            Pointer[x + y * Width] = color;
        }

        public void SetPixel(int i, uint color)
        {
            Pointer[i] = color;
        }

        public void Dispose()
        {
            Unlock();
        }
    }
}