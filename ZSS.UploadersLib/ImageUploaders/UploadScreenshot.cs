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
using System.Xml.Linq;
using HelpersLib;
using UploadersLib.HelperClasses;

namespace UploadersLib.ImageUploaders
{
    public class UploadScreenshot : ImageUploader
    {
        private string APIKey { get; set; }

        public override string Host
        {
            get
            {
                return ImageUploaderType.UPLOADSCREENSHOT.GetDescription();
            }
        }

        public UploadScreenshot(string key)
        {
            APIKey = key;
        }

        public override UploadResult Upload(Stream stream, string fileName)
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();
            arguments.Add("apiKey", APIKey);
            arguments.Add("xmlOutput", "1");
            //arguments.Add("testMode", "1");

            string response = UploadData(stream, "http://img1.uploadscreenshot.com/api-upload.php", fileName, "userfile", arguments);

            return ParseResult(response);
        }

        private UploadResult ParseResult(string source)
        {
            UploadResult ur = new UploadResult(source);

            if (!string.IsNullOrEmpty(source))
            {
                XDocument xdoc = XDocument.Parse(source);
                XElement xele = xdoc.Root.Element("upload");

                string error = xele.GetElementValue("errorCode");
                if (!string.IsNullOrEmpty(error))
                {
                    string errorMessage;

                    switch (error)
                    {
                        case "1":
                            errorMessage = "The MD5 sum that you provided did not match the MD5 sum that we calculated for the uploaded image file." +
                                " There may of been a network interruption during upload. Suggest that you try the upload again.";
                            break;
                        case "2":
                            errorMessage = "The apiKey that you provided does not exist or has been banned. Please contact us for more information.";
                            break;
                        case "3":
                            errorMessage = "The file that you provided was not a png or jpg.";
                            break;
                        case "4":
                            errorMessage = "The file that you provided was too large, currently the limit per file is 50MB.";
                            break;
                        case "99":
                        default:
                            errorMessage = "An unkown error occured, please contact the admin and include a copy of the file that you were trying to upload.";
                            break;
                    }

                    Errors.Add(errorMessage);
                }
                else
                {
                    ur.URL = xele.GetElementValue("original");
                    ur.ThumbnailURL = xele.GetElementValue("small");
                    ur.DeletionURL = xele.GetElementValue("deleteurl");
                }
            }

            return ur;
        }
    }
}