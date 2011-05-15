﻿#region License Information (GPL v2)

/*
    ZUploader - A program that allows you to upload images, text or files in your clipboard
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

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using HelpersLib;
using UploadersLib;
using UploadersLib.FileUploaders;
using UploadersLib.HelperClasses;
using UploadersLib.ImageUploaders;
using UploadersLib.TextUploaders;
using UploadersLib.URLShorteners;
using ZUploader.HelperClasses;

namespace ZUploader
{
    public class Task : IDisposable
    {
        public delegate void TaskEventHandler(UploadInfo info);

        public event TaskEventHandler UploadStarted;
        public event TaskEventHandler UploadPreparing;
        public event TaskEventHandler UploadProgressChanged;
        public event TaskEventHandler UploadCompleted;

        public UploadInfo Info { get; private set; }
        public TaskStatus Status { get; private set; }
        public bool IsWorking { get { return Status == TaskStatus.Preparing || Status == TaskStatus.Uploading; } }
        public bool IsStopped { get; private set; }

        private Stream data;
        private Image tempImage;
        private string tempText;
        private BackgroundWorker bw;
        private Uploader uploader;

        #region Constructors

        private Task(EDataType dataType, TaskJob job)
        {
            Status = TaskStatus.InQueue;
            Info = new UploadInfo();
            Info.Job = job;
            Info.UploaderType = dataType;
        }

        public static Task CreateDataUploaderTask(EDataType dataType, Stream stream, string fileName)
        {
            Task task = new Task(dataType, TaskJob.DataUpload);
            task.Info.FileName = fileName;
            task.data = stream;
            return task;
        }

        // string filePath -> FileStream data
        public static Task CreateFileUploaderTask(EDataType dataType, string filePath)
        {
            Task task = new Task(dataType, TaskJob.FileUpload);
            task.Info.FilePath = filePath;
            task.data = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return task;
        }

        // Image image -> MemoryStream data (in thread)
        public static Task CreateImageUploaderTask(EDataType dataType, Image image)
        {
            Task task = new Task(dataType, TaskJob.ImageUpload);
            task.Info.FileName = "Require image encoding...";
            task.tempImage = image;
            return task;
        }

        // string text -> MemoryStream data (in thread)
        public static Task CreateTextUploaderTask(EDataType dataType, string text)
        {
            Task task = new Task(dataType, TaskJob.TextUpload);
            task.Info.FileName = new NameParser().Convert(Program.Settings.NameFormatPattern) + ".txt";
            task.tempText = text;
            return task;
        }

        #endregion Constructors

        public void Start()
        {
            if (Status == TaskStatus.InQueue && !IsStopped)
            {
                OnUploadPreparing();
                ApplyProxySettings();

                bw = new BackgroundWorker();
                bw.WorkerReportsProgress = true;
                bw.DoWork += new DoWorkEventHandler(UploadThread);
                bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
                bw.RunWorkerAsync();
            }
        }

        public void Stop()
        {
            IsStopped = true;

            if (Status == TaskStatus.InQueue)
            {
                OnUploadCompleted();
            }
            else if (Status == TaskStatus.Uploading && uploader != null)
            {
                uploader.StopUpload();
            }
        }

        private void ApplyProxySettings()
        {
            ProxySettings proxy = new ProxySettings();
            if (!string.IsNullOrEmpty(Program.Settings.ProxySettings.Host))
            {
                proxy.ProxyConfig = ProxyConfigType.ManualProxy;
            }
            proxy.ProxyActive = Program.Settings.ProxySettings;
            Uploader.ProxySettings = proxy;
        }

        private void UploadThread(object sender, DoWorkEventArgs e)
        {
            CheckJob();

            Status = TaskStatus.Uploading;
            Info.Status = "Uploading";
            Info.StartTime = DateTime.UtcNow;
            bw.ReportProgress((int)TaskProgress.ReportStarted);

            try
            {
                switch (Info.UploaderType)
                {
                    case EDataType.Image:
                        Info.Result = UploadImage(data, Info.FileName);
                        break;
                    case EDataType.File:
                        Info.Result = UploadFile(data, Info.FileName);
                        break;
                    case EDataType.Text:
                        Info.Result = UploadText(data);
                        break;
                }
            }
            catch (Exception ex)
            {
                uploader.Errors.Add(ex.ToString());
            }
            finally
            {
                if (Info.Result == null) Info.Result = new UploadResult();
                Info.Result.Errors = uploader.Errors;
            }

            if (!IsStopped && Info.Result != null)
            {
                if (!Info.Result.IsError && string.IsNullOrEmpty(Info.Result.URL))
                {
                    Info.Result.Errors.Add("URL is empty.");
                }

                if (!Info.Result.IsError && Program.Settings.URLShortenAfterUpload)
                {
                    Info.Result.ShortenedURL = ShortenURL(Info.Result.URL);
                }
            }

            Info.UploadTime = DateTime.UtcNow;
        }

        private void CheckJob()
        {
            if (Info.Job == TaskJob.ImageUpload && tempImage != null)
            {
                using (tempImage)
                {
                    EImageFormat imageFormat;
                    data = TaskHelper.PrepareImage(tempImage, out imageFormat);
                    Info.FileName = TaskHelper.PrepareFilename(imageFormat, tempImage);
                }
            }
            else if (Info.Job == TaskJob.TextUpload && !string.IsNullOrEmpty(tempText))
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(tempText);
                data = new MemoryStream(byteArray);
            }
        }

        public UploadResult UploadImage(Stream stream, string fileName)
        {
            ImageUploader imageUploader = null;

            switch (UploadManager.ImageUploader)
            {
                case ImageDestination.IMAGESHACK:
                    imageUploader = new ImageShackUploader(ZAPILib.Keys.ImageShackKey, Program.Settings.UploadersConfig.ImageShackRegistrationCode);
                    break;
                case ImageDestination.TINYPIC:
                    imageUploader = new TinyPicUploader(ZAPILib.Keys.TinyPicID, ZAPILib.Keys.TinyPicKey, Program.Settings.UploadersConfig.TinyPicRegistrationCode);
                    break;
                /*case ImageDestType2.IMAGEBIN:
                    imageUploader = new ImageBin();
                    break;
                case ImageDestType2.IMG1:
                    imageUploader = new Img1Uploader();
                    break;*/
                case ImageDestination.IMGUR:
                    imageUploader = new Imgur(Program.Settings.UploadersConfig.ImgurAccountType, ZAPILib.Keys.ImgurAnonymousKey, Program.Settings.UploadersConfig.ImgurOAuthInfo);
                    break;
            }

            if (imageUploader != null)
            {
                PrepareUploader(imageUploader);
                return imageUploader.Upload(stream, fileName);
            }

            return null;
        }

        public UploadResult UploadText(Stream stream)
        {
            TextUploader textUploader = null;

            switch (UploadManager.TextUploader)
            {
                case TextUploaderType.PASTEBIN:
                    textUploader = new PastebinUploader(ZAPILib.Keys.PastebinKey);
                    break;
                case TextUploaderType.PASTEBIN_CA:
                    textUploader = new PastebinCaUploader(ZAPILib.Keys.PastebinCaKey);
                    break;
                case TextUploaderType.SLEXY:
                    textUploader = new SlexyUploader();
                    break;
                case TextUploaderType.PASTE2:
                    textUploader = new Paste2Uploader();
                    break;
            }

            if (textUploader != null)
            {
                PrepareUploader(textUploader);
                string url = textUploader.UploadText(stream);
                return new UploadResult(null, url);
            }

            return null;
        }

        public UploadResult UploadFile(Stream stream, string fileName)
        {
            FileUploader fileUploader = null;

            switch (UploadManager.FileUploader)
            {
                case FileUploaderType.RapidShare:
                    fileUploader = new RapidShare();
                    break;
                case FileUploaderType.SendSpace:
                    fileUploader = new SendSpace(ZAPILib.Keys.SendSpaceKey);
                    SendSpaceManager.PrepareUploadInfo(ZAPILib.Keys.SendSpaceKey, null, null);
                    break;
                case FileUploaderType.Dropbox: // TODO: Dropbox account
                    fileUploader = new Dropbox(new OAuthInfo(ZAPILib.Keys.DropboxConsumerKey, ZAPILib.Keys.DropboxConsumerSecret));
                    break;
                /*case FileUploaderType.FileSonic:
                    fileUploader = new FileSonic("", "");
                    break;
                case FileUploaderType2.FileBin:
                    fileUploader = new FileBin();
                    break;
                case FileDestination.DropIO:
                    fileUploader = new DropIO(Program.DropIOKey);
                    break;*/
                case FileUploaderType.ShareCX:
                    fileUploader = new ShareCX();
                    break;
                /*case FileUploaderType.FilezFiles:
                    fileUploader = new FilezFiles();
                    break;*/
                case FileUploaderType.CustomUploader:
                    fileUploader = new CustomUploader(Program.Settings.CustomUploader);
                    break;
                case FileUploaderType.FTP:
                    fileUploader = new FTPUploader(Program.Settings.FTPAccount);
                    break;
            }

            if (fileUploader != null)
            {
                PrepareUploader(fileUploader);
                return fileUploader.Upload(stream, fileName);
            }

            return null;
        }

        public string ShortenURL(string url)
        {
            URLShortener urlShortener = null;

            switch (UploadManager.URLShortener)
            {
                case UrlShortenerType.BITLY:
                    urlShortener = new BitlyURLShortener(ZAPILib.Keys.BitlyLogin, ZAPILib.Keys.BitlyKey);
                    break;
                case UrlShortenerType.Google:
                    urlShortener = new GoogleURLShortener(ZAPILib.Keys.GoogleURLShortenerKey);
                    break;
                case UrlShortenerType.ISGD:
                    urlShortener = new IsgdURLShortener();
                    break;
                case UrlShortenerType.Jmp:
                    urlShortener = new JmpURLShortener(ZAPILib.Keys.BitlyLogin, ZAPILib.Keys.BitlyKey);
                    break;
                /*case UrlShortenerType.THREELY:
                    urlShortener = new ThreelyURLShortener(Program.ThreelyKey);
                    break;*/
                case UrlShortenerType.TINYURL:
                    urlShortener = new TinyURLShortener();
                    break;
                case UrlShortenerType.TURL:
                    urlShortener = new TurlURLShortener();
                    break;
            }

            if (urlShortener != null)
            {
                Status = TaskStatus.URLShortening;
                return urlShortener.ShortenURL(url);
            }

            return null;
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch ((TaskProgress)e.ProgressPercentage)
            {
                case TaskProgress.ReportStarted:
                    OnUploadStarted();
                    break;
                case TaskProgress.ReportProgress:
                    ProgressManager progress = e.UserState as ProgressManager;
                    if (progress != null)
                    {
                        Info.Progress = progress;
                        OnUploadProgressChanged();
                    }
                    break;
            }
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnUploadCompleted();
        }

        private void PrepareUploader(Uploader currentUploader)
        {
            uploader = currentUploader;
            uploader.BufferSize = Program.Settings.BufferSizePower;
            uploader.ProgressChanged += (x) => bw.ReportProgress((int)TaskProgress.ReportProgress, x);
        }

        private void OnUploadPreparing()
        {
            Status = TaskStatus.Preparing;

            switch (Info.Job)
            {
                case TaskJob.ImageUpload:
                case TaskJob.TextUpload:
                    Info.Status = "Preparing";
                    break;
                default:
                    Info.Status = "Starting";
                    break;
            }

            if (UploadPreparing != null)
            {
                UploadPreparing(Info);
            }
        }

        private void OnUploadStarted()
        {
            if (UploadStarted != null)
            {
                UploadStarted(Info);
            }
        }

        private void OnUploadProgressChanged()
        {
            if (UploadProgressChanged != null)
            {
                UploadProgressChanged(Info);
            }
        }

        private void OnUploadCompleted()
        {
            Status = TaskStatus.Completed;

            if (!IsStopped)
            {
                Info.Status = "Done";
            }
            else
            {
                Info.Status = "Stopped";
            }

            if (UploadCompleted != null)
            {
                UploadCompleted(Info);
            }

            Dispose();
        }

        public void Dispose()
        {
            if (data != null) data.Dispose();
            if (tempImage != null) tempImage.Dispose();
            if (bw != null) bw.Dispose();
        }
    }
}