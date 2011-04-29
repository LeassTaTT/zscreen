﻿#region License Information (GPL v2)

/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
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

using System.Drawing;
using System.IO;
using UploadersLib.HelperClasses;

namespace UploadersLib
{
    public abstract class ImageUploader : Uploader
    {
        public abstract string Name { get; }

        public abstract UploadResult UploadImage(Stream stream, string fileName);

        public UploadResult UploadImage(Image image, string fileName)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, image.RawFormat);
                return UploadImage(stream, fileName);
            }
        }

        public UploadResult UploadImage(string filePath)
        {
            if (File.Exists(filePath))
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    UploadResult ifm = UploadImage(stream, Path.GetFileName(filePath));
                    // ifm.LocalFilePath = filePath;
                    return ifm;
                }
            }

            return null;
        }
    }

    public abstract class ImageUploaderOptions
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}