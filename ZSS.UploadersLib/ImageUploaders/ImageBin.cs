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

using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UploadersLib.HelperClasses;

namespace UploadersLib.ImageUploaders
{
    public sealed class ImageBin : ImageUploader
    {
        public override string Host
        {
            get
            {
                return "ImageBin";
            }
        }

        public override UploadResult Upload(Stream stream, string fileName)
        {
            UploadResult ur = new UploadResult();

            Dictionary<string, string> arguments = new Dictionary<string, string>();
            arguments.Add("t", "file");
            arguments.Add("name", "ZScreen");
            arguments.Add("tags", "zscreen");
            arguments.Add("description", "test");
            arguments.Add("adult", "t");
            arguments.Add("sfile", "Upload");
            arguments.Add("url", string.Empty);

            ur.Source = UploadData(stream, "http://imagebin.ca/upload.php", fileName, "f", arguments);

            if (!string.IsNullOrEmpty(ur.Source))
            {
                Match match = Regex.Match(ur.Source, @"(?<=ca/view/).+(?=\.html'>)");
                if (match != null)
                {
                    string url = "http://imagebin.ca/img/" + match.Value + Path.GetExtension(fileName);
                    ur.URL = url;
                }
            }

            return ur;
        }
    }
}