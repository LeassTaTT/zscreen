﻿using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using UploadersLib.HelperClasses;

namespace UploadersLib.FileUploaders
{
    public class Minus : FileUploader
    {
        public CookieCollection Cookies { get; set; }
        public string GalleryID { get; set; }

        public MinusSignInResponse SignIn(string username, string password)
        {
            string url = @"http://min.us/api/SignIn";

            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("username", username);
            args.Add("password1", password);

            string response = SendPostRequest(url, args, ResponseType.Text);

            Cookies = LastResponseCookies;

            return JsonConvert.DeserializeObject<MinusSignInResponse>(response);
        }

        public MinusCreateGalleryResponse CreateGallery()
        {
            string url = @"http://min.us/api/CreateGallery";

            string response = SendPostRequest(url, null, ResponseType.Text, Cookies);

            MinusCreateGalleryResponse responseObj = JsonConvert.DeserializeObject<MinusCreateGalleryResponse>(response);

            if (responseObj != null)
            {
                GalleryID = responseObj.Editor_ID;
            }

            return responseObj;
        }

        public override UploadResult Upload(Stream stream, string fileName)
        {
            string url = @"http://min.us/api/UploadItem";

            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("editor_id", GalleryID);
            args.Add("key", "OK");
            args.Add("filename", fileName);

            string response = UploadData(stream, url, fileName, "file", args, Cookies);

            UploadResult result = new UploadResult(response);

            if (!string.IsNullOrEmpty(response))
            {
                MinusUploadItemResponse responseObj = JsonConvert.DeserializeObject<MinusUploadItemResponse>(response);

                if (responseObj != null && !string.IsNullOrEmpty(responseObj.ID))
                {
                    result.URL = "http://min.us/i" + responseObj.ID;
                }
            }

            return result;
        }
    }

    public class MinusSignInResponse
    {
        public bool Success { get; set; }
    }

    public class MinusCreateGalleryResponse
    {
        public string Editor_ID { get; set; }
        public string Reader_ID { get; set; }
    }

    public class MinusUploadItemResponse
    {
        public string ID { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string FileSize { get; set; }
    }
}