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
using System.Collections.Specialized;
using System.IO;
using System.Xml.Linq;
using HelpersLib;
using UploadersLib.HelperClasses;

namespace UploadersLib.ImageUploaders
{
    public sealed class Photobucket : ImageUploader, IOAuth
    {
        public override string Host
        {
            get
            {
                return "Photobucket";
            }
        }

        private const string URLRequestToken = "http://api.photobucket.com/login/request";
        private const string URLAuthorize = "http://photobucket.com/apilogin/login";
        private const string URLAccessToken = "http://api.photobucket.com/login/access";

        public PhotobucketAccountInfo AccountInfo = new PhotobucketAccountInfo();

        public OAuthInfo AuthInfo { get; set; }

        public Photobucket(OAuthInfo oauth)
        {
            AuthInfo = oauth;
        }

        public Photobucket(OAuthInfo oauth, PhotobucketAccountInfo accountInfo)
            : this(oauth)
        {
            AccountInfo = accountInfo;
        }

        public string GetAuthorizationURL()
        {
            return GetAuthorizationURL(URLRequestToken, URLAuthorize, AuthInfo, null, HttpMethod.Post);
        }

        public bool GetAccessToken(string verificationCode)
        {
            AuthInfo.AuthVerifier = verificationCode;

            NameValueCollection nv = GetAccessTokenEx(URLAccessToken, AuthInfo, HttpMethod.Post);

            if (nv != null)
            {
                AccountInfo.Subdomain = nv["subdomain"];
                AccountInfo.AlbumID = nv["username"];
                return !string.IsNullOrEmpty(AccountInfo.Subdomain);
            }

            return false;
        }

        public PhotobucketAccountInfo GetAccountInfo()
        {
            return AccountInfo;
        }

        public override UploadResult Upload(Stream stream, string fileName)
        {
            return UploadMedia(stream, fileName, AccountInfo.ActiveAlbumPath);
        }

        public UploadResult UploadMedia(Stream stream, string fileName, string albumID)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("id", albumID); // Album identifier.
            args.Add("type", "image"); // Media type. Options are image, video, or base64.

            /*
            // Optional
            args.Add("title", ""); // Searchable title to set on the media. Maximum 250 characters.
            args.Add("description", ""); // Searchable description to set on the media. Maximum 2048 characters.
            args.Add("scramble", "false"); // Indicates if the filename should be scrambled. Options are true or false.
            args.Add("degrees", ""); // Degrees of rotation in 90 degree increments.
            args.Add("size", ""); // Size to resize an image to. (Images can only be made smaller.)
            */

            string url = "http://api.photobucket.com/album/!/upload";
            string query = OAuthManager.GenerateQuery(url, args, HttpMethod.Post, AuthInfo);
            query = FixURL(query);

            string response = UploadData(stream, query, fileName, "uploadfile");

            UploadResult ur = new UploadResult(response);

            if (!string.IsNullOrEmpty(response))
            {
                XDocument xd = XDocument.Parse(response);
                XElement xe;

                if ((xe = xd.GetNode("response/content")) != null)
                {
                    ur.URL = xe.GetElementValue("url");
                    ur.ThumbnailURL = xe.GetElementValue("thumb");
                }
            }

            return ur;
        }

        public bool CreateAlbum(string albumID, string albumName)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("id", albumID); // Album identifier.
            args.Add("name", albumName); // Name of result. Must be between 2 and 50 characters. Valid characters are letters, numbers, underscore ( _ ), hyphen (-), and space.

            string url = "http://api.photobucket.com/album/!";
            string query = OAuthManager.GenerateQuery(url, args, HttpMethod.Post, AuthInfo);
            query = FixURL(query);

            string response = SendPostRequest(query, args);

            if (!string.IsNullOrEmpty(response))
            {
                XDocument xd = XDocument.Parse(response);
                XElement xe;

                if ((xe = xd.GetNode("response")) != null)
                {
                    string status = xe.GetElementValue("status");

                    return !string.IsNullOrEmpty(status) && status == "OK";
                }
            }

            return false;
        }

        private string FixURL(string url)
        {
            return url.Replace("api.photobucket.com", AccountInfo.Subdomain);
        }
    }

    public class PhotobucketAccountInfo
    {
        public string Subdomain { get; set; }
        public string AlbumID { get; set; }
        public List<string> AlbumList = new List<string>();
        public int ActiveAlbumID = 0;

        public string ActiveAlbumPath
        {
            get
            {
                return AlbumList[ActiveAlbumID];
            }
        }
    }
}