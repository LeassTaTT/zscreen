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

using UploadersLib;

namespace UploadersAPILib
{
    public static class ZKeys
    {
        // Image Uploaders
        public const string ImageShackKey = "78EHNOPS04e77bc6df1cc0c5fc2e92e11c7b4a1a";
        public const string TinyPicID = "e2aabb8d555322fa";
        public const string TinyPicKey = "00a68ed73ddd54da52dc2d5803fa35ee";
        public const string ImgurAnonymousKey = "af2fde9818ae53e7670ab52fb8ade644"; // "97d0aaf3f70d10cc96d8b8e62431f4d2";
        public const string ImgurConsumerKey = "89dec1eceb03e7028647445fdde8af2204e87fd58"; // "cc6a3227dc7cbe15d2754b194ae3c26504db122ab";
        public const string ImgurConsumerSecret = "77e4e25ba8bf9c3ce2a9b9a09092d3d0"; // "edd13f72e7c9908b50c8090a8e912b73";
        public const string FlickrKey = "009382d913746758f23d0ba9906b9fde";
        public const string FlickrSecret = "7a147b763b1c7ebc";
        public const string PhotobucketConsumerKey = "149828681";
        public const string PhotobucketConsumerSecret = "d2638b653e88315aac528087e9db54e3";
        public const string UploadScreenshotKey = "2807828f379860848433221358";
        public const string ImageBamKey = "P9MMWVORXYCVM9LL";
        public const string ImageBamSecret = "8NHVT2W777DEHZBV54A4CIT23XJFTL1D";
        public const string TwitsnapsKey = "694aaea3bc8f28479c383cbcc094fb55";

        // File Uploaders
        public const string DropboxConsumerKey = "0te7j9ype9lrdfn";
        public const string DropboxConsumerSecret = "r5d3aptd9a0cwp9";
        public const string MinusConsumerKey = "b57b69843f7a302a276dde89890fc6";
        public const string MinusConsumerSecret = "3fb097f08314d713959b1f41d543b0";
        public const string SendSpaceKey = "LV6OS1R0Q3";
        public const string DropIOKey = "6c65e2d2bfd858f7d0aa6509784f876483582eea";

        // Text Uploaders
        public const string PastebinKey = "4b23be71ec78bbd4fb96735320aa09ef";
        public const string PastebinCaKey = "KxTofLKQThSBZ63Gpa7hYLlMdyQlMD6u";

        // URL Shorteners
        public const string BitlyLogin = "jaex";
        public const string BitlyKey = "R_1734f57b772acb3c048eb2365075743b";
        public const string BitlyConsumerKey = "91e5a02e001ada9c1122f54d73bc442d9cc2a7ab";
        public const string BitlyConsumerSecret = "fe5f906b5f5e8114d5c266a709a9438c94e1cd3f";
        public const string GoogleConsumerKey = "1011702346808.apps.googleusercontent.com";
        public const string GoogleConsumerSecret = "EmT1pAOddpDRYUDd5smypTbH";
        public const string KlamKey = "a4e5a8de710d80db774a8264f4588ffb";
        public const string ThreelyKey = "em5893833";

        // Other Services
        public const string TwitterConsumerKey = "Jzzcm6ytcyml14sQIvqvmA";
        public const string TwitterConsumerSecret = "aJYZ9W1gJnGMgSqhRYrvoUyUc14FssVJOFAqHjriU";
        public const string GoogleApiKey = "AIzaSyCcYJvYPvS3UE0JqqsSNpjPjN1NPBmMbmE";
        public const string PicnikKey = "3aacd2de4563b8817de708b29b5bdd0e";

        public static UploadersAPIKeys GetAPIKeys()
        {
            return new UploadersAPIKeys
            {
                TinyPicID = TinyPicID,
                TinyPicKey = TinyPicKey,
                ImgurConsumerKey = ImgurConsumerKey,
                ImgurConsumerSecret = ImgurConsumerSecret,
                FlickrKey = FlickrKey,
                FlickrSecret = FlickrSecret,
                PhotobucketConsumerKey = PhotobucketConsumerKey,
                PhotobucketConsumerSecret = PhotobucketConsumerSecret,
                DropboxConsumerKey = DropboxConsumerKey,
                DropboxConsumerSecret = DropboxConsumerSecret,
                MinusConsumerKey = MinusConsumerKey,
                MinusConsumerSecret = MinusConsumerSecret,
                SendSpaceKey = SendSpaceKey,
                PastebinKey = PastebinKey,
                TwitterConsumerKey = TwitterConsumerKey,
                TwitterConsumerSecret = TwitterConsumerSecret,
                GoogleTranslateKey = GoogleApiKey,
                GoogleConsumerKey = GoogleConsumerKey,
                GoogleConsumerSecret = GoogleConsumerSecret
            };
        }
    }
}