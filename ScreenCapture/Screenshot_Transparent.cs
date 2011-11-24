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
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;
using HelpersLib;

namespace ScreenCapture
{
    public static partial class Screenshot
    {
        public static Image CaptureWindowTransparent(IntPtr handle)
        {
            if (handle.ToInt32() > 0)
            {
                Rectangle rect = CaptureHelpers.GetWindowRectangle(handle);
                Bitmap whiteBackground = null, blackBackground = null, whiteBackground2 = null;
                MyCursor cursor = null;
                bool isTransparent = false;

                try
                {
                    if (DrawCursor)
                    {
                        cursor = NativeMethods.CaptureCursor();
                    }

                    using (Form form = new Form())
                    {
                        form.BackColor = Color.White;
                        form.FormBorderStyle = FormBorderStyle.None;
                        form.ShowInTaskbar = false;

                        if (!NativeMethods.IsWindowMaximized(handle) && NativeMethods.IsDWMEnabled())
                        {
                            const int offset = 20;

                            rect.Inflate(offset, offset);
                            rect.Intersect(CaptureHelpers.GetScreenBounds());
                        }

                        NativeMethods.ShowWindow(form.Handle, (int)WindowShowStyle.ShowNormalNoActivate);
                        NativeMethods.SetWindowPos(form.Handle, handle, rect.X, rect.Y, rect.Width, rect.Height, NativeMethods.SWP_NOACTIVATE);
                        Thread.Sleep(10);
                        Application.DoEvents();

                        whiteBackground = (Bitmap)Screenshot.CaptureRectangleNative(rect);

                        form.BackColor = Color.Black;
                        Application.DoEvents();

                        blackBackground = (Bitmap)Screenshot.CaptureRectangleNative(rect);

                        form.BackColor = Color.White;
                        Application.DoEvents();

                        whiteBackground2 = (Bitmap)Screenshot.CaptureRectangleNative(rect);

                        form.Close();
                    }

                    Bitmap transparentImage;

                    if (IsImagesEqual(whiteBackground, whiteBackground2))
                    {
                        transparentImage = CreateTransparentImage(whiteBackground, blackBackground);
                        isTransparent = true;
                    }
                    else
                    {
                        StaticHelper.WriteLine("Transparent capture failed.");
                        transparentImage = whiteBackground2;
                    }

                    if (cursor != null)
                    {
                        Point cursorOffset = CaptureHelpers.FixScreenCoordinates(rect.Location);
                        CaptureHelpers.DrawCursorToImage(cursor, transparentImage, cursorOffset);
                    }

                    if (isTransparent)
                    {
                        transparentImage = TrimTransparent(transparentImage);
                    }

                    return transparentImage;
                }
                finally
                {
                    if (isTransparent && whiteBackground != null) whiteBackground.Dispose();
                    if (blackBackground != null) blackBackground.Dispose();
                    if (whiteBackground2 != null) whiteBackground2.Dispose();
                    if (cursor != null) cursor.Dispose();
                }
            }

            return null;
        }

        public static Image CaptureActiveWindowTransparent()
        {
            IntPtr handle = NativeMethods.GetForegroundWindow();

            return CaptureWindowTransparent(handle);
        }

        private static bool IsImagesEqual(Bitmap bmp1, Bitmap bmp2)
        {
            using (UnsafeBitmap unsafeBitmap1 = new UnsafeBitmap(bmp1))
            using (UnsafeBitmap unsafeBitmap2 = new UnsafeBitmap(bmp2))
            {
                return unsafeBitmap1 == unsafeBitmap2;
            }
        }

        private static Bitmap CreateTransparentImage(Bitmap whiteBackground, Bitmap blackBackground)
        {
            if (whiteBackground != null && blackBackground != null && whiteBackground.Size == blackBackground.Size)
            {
                Bitmap result = new Bitmap(whiteBackground.Width, whiteBackground.Height, PixelFormat.Format32bppArgb);

                using (UnsafeBitmap whiteBitmap = new UnsafeBitmap(whiteBackground, true, ImageLockMode.ReadOnly))
                using (UnsafeBitmap blackBitmap = new UnsafeBitmap(blackBackground, true, ImageLockMode.ReadOnly))
                using (UnsafeBitmap resultBitmap = new UnsafeBitmap(result, true, ImageLockMode.WriteOnly))
                {
                    int pixelCount = blackBitmap.PixelCount;

                    for (int i = 0; i < pixelCount; i++)
                    {
                        ColorBgra white = whiteBitmap.GetPixel(i);
                        ColorBgra black = blackBitmap.GetPixel(i);

                        double alpha = (black.Red - white.Red + 255) / 255.0;

                        if (alpha == 1)
                        {
                            resultBitmap.SetPixel(i, white);
                        }
                        else if (alpha > 0)
                        {
                            white.Blue = (byte)(black.Blue / alpha);
                            white.Green = (byte)(black.Green / alpha);
                            white.Red = (byte)(black.Red / alpha);
                            white.Alpha = (byte)(255 * alpha);

                            resultBitmap.SetPixel(i, white);
                        }
                    }
                }

                return result;
            }

            return whiteBackground;
        }

        private static Bitmap TrimTransparent(Bitmap bitmap)
        {
            Rectangle source = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            Rectangle rect = source;

            using (UnsafeBitmap unsafeBitmap = new UnsafeBitmap(bitmap, true, ImageLockMode.ReadOnly))
            {
                rect = TrimTransparentFindX(unsafeBitmap, rect);
                rect = TrimTransparentFindY(unsafeBitmap, rect);
                rect = TrimTransparentFindWidth(unsafeBitmap, rect);
                rect = TrimTransparentFindHeight(unsafeBitmap, rect);
            }

            if (source != rect)
            {
                return CaptureHelpers.CropBitmap(bitmap, rect);
            }

            return bitmap;
        }

        private static Rectangle TrimTransparentFindX(UnsafeBitmap unsafeBitmap, Rectangle rect)
        {
            for (int x = rect.X; x < rect.Width; x++)
            {
                for (int y = rect.Y; y < rect.Height; y++)
                {
                    if (unsafeBitmap.GetPixel(x, y).Alpha > 0)
                    {
                        rect.X = x;
                        return rect;
                    }
                }
            }

            return rect;
        }

        private static Rectangle TrimTransparentFindY(UnsafeBitmap unsafeBitmap, Rectangle rect)
        {
            for (int y = rect.Y; y < rect.Height; y++)
            {
                for (int x = rect.X; x < rect.Width; x++)
                {
                    if (unsafeBitmap.GetPixel(x, y).Alpha > 0)
                    {
                        rect.Y = y;
                        return rect;
                    }
                }
            }

            return rect;
        }

        private static Rectangle TrimTransparentFindWidth(UnsafeBitmap unsafeBitmap, Rectangle rect)
        {
            for (int x = rect.Width - 1; x >= rect.X; x--)
            {
                for (int y = rect.Y; y < rect.Height; y++)
                {
                    if (unsafeBitmap.GetPixel(x, y).Alpha > 0)
                    {
                        rect.Width = x - rect.X + 1;
                        return rect;
                    }
                }
            }

            return rect;
        }

        private static Rectangle TrimTransparentFindHeight(UnsafeBitmap unsafeBitmap, Rectangle rect)
        {
            for (int y = rect.Height - 1; y >= rect.Y; y--)
            {
                for (int x = rect.X; x < rect.Width; x++)
                {
                    if (unsafeBitmap.GetPixel(x, y).Alpha > 0)
                    {
                        rect.Height = y - rect.Y + 1;
                        return rect;
                    }
                }
            }

            return rect;
        }

        private static Bitmap QuickTrimTransparent(Bitmap bitmap)
        {
            Rectangle source = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            Rectangle rect = source;

            using (UnsafeBitmap unsafeBitmap = new UnsafeBitmap(bitmap, true, ImageLockMode.ReadOnly))
            {
                int middleX = rect.Width / 2;
                int middleY = rect.Height / 2;

                // Find X
                for (int x = rect.X; x < rect.Width; x++)
                {
                    if (unsafeBitmap.GetPixel(x, middleY).Alpha > 0)
                    {
                        rect.X = x;
                        break;
                    }
                }

                // Find Y
                for (int y = rect.Y; y < rect.Height; y++)
                {
                    if (unsafeBitmap.GetPixel(middleX, y).Alpha > 0)
                    {
                        rect.Y = y;
                        break;
                    }
                }

                // Find Width
                for (int x = rect.Width - 1; x >= rect.X; x--)
                {
                    if (unsafeBitmap.GetPixel(x, middleY).Alpha > 0)
                    {
                        rect.Width = x - rect.X + 1;
                        break;
                    }
                }

                // Find Height
                for (int y = rect.Height - 1; y >= rect.Y; y--)
                {
                    if (unsafeBitmap.GetPixel(middleX, y).Alpha > 0)
                    {
                        rect.Height = y - rect.Y + 1;
                        break;
                    }
                }
            }

            if (source != rect)
            {
                return CaptureHelpers.CropBitmap(bitmap, rect);
            }

            return bitmap;
        }

        private static byte[,] windows7Corner = new byte[12, 2] {
            {0, 0}, {1, 0}, {2, 0}, {3, 0}, {4, 0},
            {0, 1}, {1, 1}, {2, 1},
            {0, 2}, {1, 2},
            {0, 3},
            {0, 4}
        };

        private static byte[,] windowsVistaCorner = new byte[8, 2] {
            {0, 0}, {1, 0}, {2, 0}, {3, 0},
            {0, 1}, {1, 1},
            {0, 2},
            {0, 3}
        };

        private static Bitmap RemoveCorners(Image img)
        {
            byte[,] corner;

            Version os = Environment.OSVersion.Version;

            if (os.Major == 6 && os.Minor == 1) // Windows 7
            {
                corner = windows7Corner;
            }
            else if (os.Major == 6 && os.Minor == 0) // Windows Vista
            {
                corner = windowsVistaCorner;
            }
            else
            {
                return null;
            }

            return RemoveCorners(img, corner);
        }

        private static Bitmap RemoveCorners(Image img, byte[,] cornerData)
        {
            Bitmap bmp = new Bitmap(img);

            for (int i = 0; i < cornerData.GetLength(0); i++)
            {
                // Left top corner
                bmp.SetPixel(cornerData[i, 0], cornerData[i, 1], Color.Transparent);

                // Right top corner
                bmp.SetPixel(bmp.Width - cornerData[i, 0] - 1, cornerData[i, 1], Color.Transparent);

                // Left bottom corner
                bmp.SetPixel(cornerData[i, 0], bmp.Height - cornerData[i, 1] - 1, Color.Transparent);

                // Right bottom corner
                bmp.SetPixel(bmp.Width - cornerData[i, 0] - 1, bmp.Height - cornerData[i, 1] - 1, Color.Transparent);
            }

            return bmp;
        }
    }
}