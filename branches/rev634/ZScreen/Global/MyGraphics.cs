﻿#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2009  Brandon Zimmerman

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
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using ZSS.Helpers;

namespace ZSS
{
    class MyGraphics
    {    

        /// <summary>
        /// Function to get a Rectangle of all the screens combined
        /// </summary>
        /// <returns></returns>
        public static Rectangle GetScreenBounds()
        {
            Point topLeft = new Point(0, 0);
            Point bottomRight = new Point(0, 0);
            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.Bounds.X < topLeft.X) topLeft.X = screen.Bounds.X;
                if (screen.Bounds.Y < topLeft.Y) topLeft.Y = screen.Bounds.Y;
                if ((screen.Bounds.X + screen.Bounds.Width) > bottomRight.X) bottomRight.X = screen.Bounds.X + screen.Bounds.Width;
                if ((screen.Bounds.Y + screen.Bounds.Height) > bottomRight.Y) bottomRight.Y = screen.Bounds.Y + screen.Bounds.Height;
            }
            return new Rectangle(topLeft.X, topLeft.Y, (bottomRight.X + Math.Abs(topLeft.X)), (bottomRight.Y + Math.Abs(topLeft.Y)));
        }

        /// <summary>
        /// Function to get Image without memory errors
        /// </summary>
        /// <param name="fp"></param>
        /// <returns></returns>
        public static Image GetImageSafely(string fp)
        {
            Bitmap bmp = null;
            try
            {
                Image img = Image.FromFile(fp);
                bmp = new Bitmap(img);
                img.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return bmp;
        }

        public static GraphicsPath RoundedRectangle(Rectangle rect, int CornerRadius)
        {
            int X = rect.X, Y = rect.Y, RectWidth = rect.Width, RectHeight = rect.Height;
            GraphicsPath gPath = new GraphicsPath();
            gPath.AddLine(X + CornerRadius, Y, X + RectWidth - (CornerRadius * 2), Y);
            gPath.AddArc(X + RectWidth - (CornerRadius * 2), Y, CornerRadius * 2, CornerRadius * 2, 270, 90);
            gPath.AddLine(X + RectWidth, Y + CornerRadius, X + RectWidth, Y + RectHeight - (CornerRadius * 2));
            gPath.AddArc(X + RectWidth - (CornerRadius * 2), Y + RectHeight - (CornerRadius * 2), CornerRadius * 2, CornerRadius * 2, 0, 90);
            gPath.AddLine(X + RectWidth - (CornerRadius * 2), Y + RectHeight, X + CornerRadius, Y + RectHeight);
            gPath.AddArc(X, Y + RectHeight - (CornerRadius * 2), CornerRadius * 2, CornerRadius * 2, 90, 90);
            gPath.AddLine(X, Y + RectHeight - (CornerRadius * 2), X, Y + CornerRadius);
            gPath.AddArc(X, Y, CornerRadius * 2, CornerRadius * 2, 180, 90);
            gPath.CloseFigure();
            return gPath;
        }

        public static void SaveImageToMemoryStream(Image img, MemoryStream ms, ImageFormat format)
        {
            //image quality setting only works for JPEG

            if (format == ImageFormat.Jpeg)
            {
                EncoderParameter quality = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, Program.conf.ImageQuality);
                ImageCodecInfo codec = GetEncoderInfo("image/jpeg");

                EncoderParameters encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = quality;

                img.Save(ms, codec, encoderParams);
            }
            else
            {
                img.Save(ms, format);
            }
        }

        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            for (int x = 0; x < codecs.Length; x++)
                if (codecs[x].MimeType == mimeType)
                    return codecs[x];
            return null;
        }

        public static Rectangle GetRectangle(int x, int y, int width, int height)
        {
            if (width < 0)
            {
                x = x + width;
                width = -width;
            }
            if (height < 0)
            {
                y = y + height;
                height = -height;
            }
            return new Rectangle(x, y, width, height);
        }
    }
}