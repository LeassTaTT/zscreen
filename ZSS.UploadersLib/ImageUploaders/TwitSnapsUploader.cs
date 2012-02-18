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
    public sealed class TwitSnapsUploader : ImageUploader
    {
        public OAuthInfo AuthInfo { get; set; }

        private const string APIURL = "http://twitsnaps.com/dev/image/upload.xml";

        private string APIKey;

        public override string Host
        {
            get
            {
                return ImageDestination.Twitsnaps.GetDescription();
            }
        }

        public TwitSnapsUploader(string apiKey, OAuthInfo oauth)
        {
            APIKey = apiKey;
            AuthInfo = oauth;
        }

        public override UploadResult Upload(Stream stream, string fileName)
        {
            using (TwitterMsg msgBox = new TwitterMsg("Update Twitter Status"))
            {
                msgBox.ShowDialog();
                return Upload(stream, fileName, msgBox.Message);
            }
        }

        private UploadResult Upload(Stream stream, string fileName, string msg)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("appKey", APIKey);
            args.Add("consumerKey", AuthInfo.ConsumerKey);
            args.Add("consumerSecret", AuthInfo.ConsumerSecret);
            args.Add("oauthToken", AuthInfo.UserToken);
            args.Add("oauthSecret", AuthInfo.UserSecret);

            if (!string.IsNullOrEmpty(msg))
            {
                args.Add("message", msg);
            }

            string source = UploadData(stream, APIURL, fileName, "media", args);

            return ParseResult(source);
        }

        private UploadResult ParseResult(string source)
        {
            UploadResult ur = new UploadResult(source);

            if (!string.IsNullOrEmpty(source))
            {
                XDocument xd = XDocument.Parse(source);
                XElement xe;

                xe = xd.Element("image");

                if (xe != null)
                {
                    string id = xe.GetElementValue("id");
                    ur.URL = "http://twitsnaps.com/snap/" + id;
                    ur.ThumbnailURL = "http://twitsnaps.com/thumb/" + id;
                }
                else
                {
                    xe = xd.Element("error");

                    if (xe != null)
                    {
                        Errors.Add("Error: " + xe.GetElementValue("description"));
                    }
                }
            }

            return ur;
        }
    }
}