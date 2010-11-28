﻿#region License Information (GPL v2)

/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2010  Brandon Zimmerman

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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Web;
using System.Windows.Forms;
using HelpersLib;
using UploadersLib.HelperClasses;
using ZUploader.HelperClasses;

namespace UploadersLib
{
    public class Uploader
    {
        public delegate void ProgressEventHandler(ProgressManager progress);
        public event ProgressEventHandler ProgressChanged;

        public static ProxySettings ProxySettings = new ProxySettings();

        public List<string> Errors { get; set; }
        public string UserAgent { get; set; }
        public bool IsUploading { get; set; }

        private bool stopUpload;

        public Uploader()
        {
            this.Errors = new List<string>();
            this.UserAgent = string.Format("{0} {1}", Application.ProductName, Application.ProductVersion);
        }

        protected void OnProgressChanged(ProgressManager progress)
        {
            if (ProgressChanged != null)
            {
                ProgressChanged(progress);
            }
        }

        public string ToErrorString()
        {
            return string.Join("\r\n", Errors.ToArray());
        }

        public void StopUpload()
        {
            if (IsUploading)
            {
                stopUpload = true;
            }
        }

        #region Post methods

        /// <summary>
        /// Method: POST
        /// </summary>
        protected string GetResponse(string url, Dictionary<string, string> arguments)
        {
            string boundary = "---------------" + FastDateTime.Now.Ticks.ToString("x");

            byte[] data = MakeInputContent(boundary, arguments, true);

            using (HttpWebResponse response = GetResponse(url, data, boundary))
            {
                return ResponseToString(response);
            }
        }

        /// <summary>
        /// Method: POST
        /// </summary>
        protected string GetResponse(string url)
        {
            return GetResponse(url, null);
        }

        /// <summary>
        /// Method: POST
        /// </summary>
        protected string GetRedirectionURL(string url, Dictionary<string, string> arguments)
        {
            string boundary = "---------------" + FastDateTime.Now.Ticks.ToString("x");

            byte[] data = MakeInputContent(boundary, arguments, true);

            using (HttpWebResponse response = GetResponse(url, data, boundary))
            {
                if (response != null)
                {
                    return response.ResponseUri.OriginalString;
                }
            }

            return null;
        }

        private HttpWebResponse GetResponseUsingPost(string url, Stream stream, string boundary)
        {
            IsUploading = true;
            stopUpload = false;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AllowWriteStreamBuffering = ProxySettings.ProxyConfig != ProxyConfigType.NoProxy;
                request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                request.ContentLength = stream.Length;
                request.ContentType = "multipart/form-data; boundary=" + boundary;
                request.KeepAlive = false;
                request.Method = "POST";
                request.ProtocolVersion = HttpVersion.Version11;
                request.Proxy = ProxySettings.GetWebProxy;
                request.ServicePoint.Expect100Continue = false;
                request.ServicePoint.UseNagleAlgorithm = false;
                request.Timeout = -1;
                request.UserAgent = UserAgent;

                using (Stream requestStream = request.GetRequestStream())
                {
                    ProgressManager progress = new ProgressManager(stream.Length);
                    stream.Position = 0;
                    byte[] buffer = new byte[4096];
                    int bytesRead;

                    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        if (stopUpload) return null;

                        requestStream.Write(buffer, 0, bytesRead);

                        if (progress.ChangeProgress(bytesRead))
                        {
                            OnProgressChanged(progress);
                        }
                    }
                }

                return (HttpWebResponse)request.GetResponse();
            }
            catch (Exception e)
            {
                if (!stopUpload)
                {
                    this.Errors.Add(e.ToString());
                    Debug.WriteLine(e.ToString());
                }
            }
            finally
            {
                IsUploading = false;
            }

            return null;
        }

        private HttpWebResponse GetResponse(string url, byte[] data, string boundary)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(data, 0, data.Length);
                return GetResponseUsingPost(url, stream, boundary);
            }
        }

        protected string UploadData(Stream data, string fileName, string url, string fileFormName, Dictionary<string, string> arguments)
        {
            IsUploading = true;
            stopUpload = false;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AllowWriteStreamBuffering = ProxySettings.ProxyConfig != ProxyConfigType.NoProxy;
                request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                request.KeepAlive = false;
                request.Method = "POST";
                request.ProtocolVersion = HttpVersion.Version11;
                request.Proxy = ProxySettings.GetWebProxy;
                request.ServicePoint.Expect100Continue = false;
                request.ServicePoint.UseNagleAlgorithm = false;
                request.Timeout = -1;
                request.UserAgent = UserAgent;

                string boundary = "---------------" + FastDateTime.Now.Ticks.ToString("x");
                request.ContentType = "multipart/form-data; boundary=" + boundary;

                byte[] bytesArguments = MakeInputContent(boundary, arguments, false);

                string fileInputContent = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: {3}\r\n\r\n",
                    boundary, fileFormName, fileName, Helpers.GetMimeType(fileName));

                byte[] bytesFileInputContent = Encoding.UTF8.GetBytes(fileInputContent);
                byte[] bytesLast = Encoding.UTF8.GetBytes(string.Format("\r\n--{0}--\r\n", boundary));

                request.ContentLength = bytesArguments.Length + bytesFileInputContent.Length + data.Length + bytesLast.Length;

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(bytesArguments, 0, bytesArguments.Length);
                    requestStream.Write(bytesFileInputContent, 0, bytesFileInputContent.Length);

                    ProgressManager progress = new ProgressManager(data.Length);
                    data.Position = 0;
                    byte[] buffer = new byte[4096];
                    int bytesRead;

                    while ((bytesRead = data.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        if (stopUpload) return null;

                        requestStream.Write(buffer, 0, bytesRead);

                        if (progress.ChangeProgress(bytesRead))
                        {
                            OnProgressChanged(progress);
                        }
                    }

                    requestStream.Write(bytesLast, 0, bytesLast.Length);
                }

                return ResponseToString(request.GetResponse());
            }
            catch (Exception e)
            {
                if (!stopUpload)
                {
                    this.Errors.Add(e.ToString());
                    Debug.WriteLine(e.ToString());
                }
            }
            finally
            {
                IsUploading = false;
            }

            return null;
        }

        #endregion Post methods

        #region Get methods

        /// <summary>
        /// Method: GET
        /// </summary>
        protected string GetResponseString(string url, Dictionary<string, string> arguments)
        {
            if (arguments != null && arguments.Count > 0)
            {
                url += "?" + string.Join("&", arguments.Select(x => x.Key + "=" + HttpUtility.UrlEncode(x.Value)).ToArray());
            }

            using (HttpWebResponse response = GetResponseUsingGet(url))
            {
                return ResponseToString(response);
            }
        }

        /// <summary>
        /// Method: GET
        /// </summary>
        protected string GetResponseString(string url)
        {
            return GetResponseString(url, null);
        }

        private HttpWebResponse GetResponseUsingGet(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                request.KeepAlive = false;
                request.Method = "GET";
                request.Proxy = ProxySettings.GetWebProxy;
                request.UserAgent = UserAgent;

                return (HttpWebResponse)request.GetResponse();
            }
            catch (Exception e)
            {
                this.Errors.Add(e.ToString());
                Debug.WriteLine(e.ToString());
            }

            return null;
        }

        #endregion Get methods

        #region Helper methods

        private byte[] MakeInputContent(string boundary, string name, string value)
        {
            string format = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}\r\n", boundary, name, value);
            return Encoding.UTF8.GetBytes(format);
        }

        private byte[] MakeInputContent(string boundary, Dictionary<string, string> contents, bool isFinal)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                byte[] bytes;

                if (contents != null)
                {
                    foreach (KeyValuePair<string, string> content in contents)
                    {
                        bytes = MakeInputContent(boundary, content.Key, content.Value);
                        stream.Write(bytes, 0, bytes.Length);
                    }
                }

                if (isFinal)
                {
                    bytes = MakeFinalBoundary(boundary);
                    stream.Write(bytes, 0, bytes.Length);
                }

                return stream.ToArray();
            }
        }

        private byte[] MakeFinalBoundary(string boundary)
        {
            return Encoding.UTF8.GetBytes(string.Format("--{0}--\r\n", boundary));
        }

        private string ResponseToString(WebResponse response)
        {
            if (response != null)
            {
                using (response)
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            }

            return null;
        }

        #endregion Helper methods
    }
}