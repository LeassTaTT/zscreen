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

namespace UploadersLib
{
    public class UploadersAPIKeys
    {
        // Image Uploaders
        public string ImageShackKey { get; set; }
        public string TinyPicID { get; set; }
        public string TinyPicKey { get; set; }
        public string ImgurAnonymousKey { get; set; }
        public string ImgurConsumerKey { get; set; }
        public string ImgurConsumerSecret { get; set; }
        public string FlickrKey { get; set; }
        public string FlickrSecret { get; set; }
        public string UploadScreenshotKey { get; set; }
        public string ImageBamKey { get; set; }
        public string ImageBamSecret { get; set; }
        public string TwitsnapsKey { get; set; }

        // File Uploaders
        public string DropboxConsumerKey { get; set; }
        public string DropboxConsumerSecret { get; set; }
        public string SendSpaceKey { get; set; }
        public string DropIOKey { get; set; }

        // Text Uploaders
        public string PastebinKey { get; set; }
    }
}