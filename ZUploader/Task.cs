﻿#region License Information (GPL v2)

/*
    ZUploader - A program that allows you to upload images, texts or files
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
using System.Windows.Forms;
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
            Info.DataType = dataType;
            Info.UploadDestination = dataType;
        }

        public static Task CreateDataUploaderTask(EDataType dataType, Stream stream, string fileName, EDataType destination = EDataType.Default)
        {
            Task task = new Task(dataType, TaskJob.DataUpload);
            if (destination != EDataType.Default) task.Info.UploadDestination = destination;
            task.Info.FileName = fileName;
            task.data = stream;
            return task;
        }

        // string filePath -> FileStream data
        public static Task CreateFileUploaderTask(EDataType dataType, string filePath, EDataType destination = EDataType.Default)
        {
            Task task = new Task(dataType, TaskJob.FileUpload);
            if (destination != EDataType.Default) task.Info.UploadDestination = destination;
            task.Info.FilePath = filePath;
            task.data = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return task;
        }

        // Image image -> MemoryStream data (in thread)
        public static Task CreateImageUploaderTask(Image image, EDataType destination = EDataType.Default)
        {
            Task task = new Task(EDataType.Image, TaskJob.ImageUpload);
            if (destination != EDataType.Default) task.Info.UploadDestination = destination;
            task.Info.FileName = "Require image encoding...";
            task.tempImage = image;
            return task;
        }

        // string text -> MemoryStream data (in thread)
        public static Task CreateTextUploaderTask(string text, EDataType destination = EDataType.Default)
        {
            Task task = new Task(EDataType.Text, TaskJob.TextUpload);
            if (destination != EDataType.Default) task.Info.UploadDestination = destination;
            task.Info.FileName = new NameParser().Convert(Program.Settings.NameFormatPattern) + ".txt";
            task.tempText = text;
            return task;
        }

        public static Task CreateURLShortenerTask(string url)
        {
            Task task = new Task(EDataType.URL, TaskJob.ShortenURL);
            task.Info.FileName = "URL shorten";
            task.Info.Result.URL = url;
            return task;
        }

        #endregion Constructors

        public void Start()
        {
            if (Status == TaskStatus.InQueue && !IsStopped)
            {
                OnUploadPreparing();

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

        private void UploadThread(object sender, DoWorkEventArgs e)
        {
            CheckJob();

            if (Program.UploadersConfig == null)
            {
                Program.UploaderSettingsResetEvent.WaitOne();
            }

            Status = TaskStatus.Uploading;
            Info.Status = "Uploading";
            Info.StartTime = DateTime.UtcNow;
            bw.ReportProgress((int)TaskProgress.ReportStarted);

            try
            {
                switch (Info.UploadDestination)
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
                if (uploader != null) Info.Result.Errors = uploader.Errors;
            }

            if (!IsStopped && Info.Result != null)
            {
                if (!Info.Result.IsError && string.IsNullOrEmpty(Info.Result.URL))
                {
                    Info.Result.Errors.Add("URL is empty.");
                }

                if (!Info.Result.IsError && (Program.Settings.URLShortenAfterUpload || Info.Job == TaskJob.ShortenURL))
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
                    ImageData imageData = TaskHelper.PrepareImageAndFilename(tempImage);
                    data = imageData.ImageStream;
                    Info.FileName = imageData.Filename;
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
                case ImageDestination.ImageShack:
                    imageUploader = new ImageShackUploader(ZKeys.ImageShackKey, Program.UploadersConfig.ImageShackAccountType,
                        Program.UploadersConfig.ImageShackRegistrationCode)
                    {
                        IsPublic = Program.UploadersConfig.ImageShackShowImagesInPublic
                    };
                    break;
                case ImageDestination.TinyPic:
                    imageUploader = new TinyPicUploader(ZKeys.TinyPicID, ZKeys.TinyPicKey, Program.UploadersConfig.TinyPicAccountType,
                        Program.UploadersConfig.TinyPicRegistrationCode);
                    break;
                case ImageDestination.Imgur:
                    imageUploader = new Imgur(Program.UploadersConfig.ImgurAccountType, ZKeys.ImgurAnonymousKey, Program.UploadersConfig.ImgurOAuthInfo)
                    {
                        ThumbnailType = Program.UploadersConfig.ImgurThumbnailType
                    };
                    break;
                case ImageDestination.Flickr:
                    imageUploader = new FlickrUploader(ZKeys.FlickrKey, ZKeys.FlickrSecret, Program.UploadersConfig.FlickrAuthInfo, Program.UploadersConfig.FlickrSettings);
                    break;
                case ImageDestination.Photobucket:
                    imageUploader = new Photobucket(Program.UploadersConfig.PhotobucketOAuthInfo, Program.UploadersConfig.PhotobucketAccountInfo);
                    break;
                case ImageDestination.UploadScreenshot:
                    imageUploader = new UploadScreenshot(ZKeys.UploadScreenshotKey);
                    break;
                case ImageDestination.Twitpic:
                    TwitPicOptions twitPicOptions = new TwitPicOptions();
                    twitPicOptions.Username = Program.UploadersConfig.TwitPicUsername;
                    twitPicOptions.Password = Program.UploadersConfig.TwitPicPassword;
                    twitPicOptions.TwitPicThumbnailMode = Program.UploadersConfig.TwitPicThumbnailMode;
                    twitPicOptions.ShowFull = Program.UploadersConfig.TwitPicShowFull;
                    imageUploader = new TwitPicUploader(twitPicOptions);
                    break;
                case ImageDestination.Twitsnaps:
                    int index = Program.UploadersConfig.TwitterSelectedAccount;

                    if (Program.UploadersConfig.TwitterOAuthInfoList.HasValidIndex(index))
                    {
                        imageUploader = new TwitSnapsUploader(ZKeys.TwitsnapsKey, Program.UploadersConfig.TwitterOAuthInfoList[index]);
                    }
                    break;
                case ImageDestination.yFrog:
                    YfrogOptions yFrogOptions = new YfrogOptions(ZKeys.ImageShackKey);
                    yFrogOptions.Username = Program.UploadersConfig.YFrogUsername;
                    yFrogOptions.Password = Program.UploadersConfig.YFrogPassword;
                    yFrogOptions.Source = Application.ProductName;
                    imageUploader = new YfrogUploader(yFrogOptions);
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
                case TextDestination.Pastebin:
                    textUploader = new PastebinUploader(ZKeys.PastebinKey, Program.UploadersConfig.PastebinSettings);
                    break;
                case TextDestination.PastebinCA:
                    textUploader = new PastebinCaUploader(ZKeys.PastebinCaKey);
                    break;
                case TextDestination.Paste2:
                    textUploader = new Paste2Uploader();
                    break;
                case TextDestination.Slexy:
                    textUploader = new SlexyUploader();
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
                case FileDestination.Dropbox:
                    NameParser parser = new NameParser { IsFolderPath = true };
                    string uploadPath = parser.Convert(Dropbox.TidyUploadPath(Program.UploadersConfig.DropboxUploadPath));
                    fileUploader = new Dropbox(Program.UploadersConfig.DropboxOAuthInfo, uploadPath, Program.UploadersConfig.DropboxAccountInfo);
                    break;
                case FileDestination.RapidShare:
                    fileUploader = new RapidShare(Program.UploadersConfig.RapidShareUsername, Program.UploadersConfig.RapidSharePassword,
                        Program.UploadersConfig.RapidShareFolderID);
                    break;
                case FileDestination.SendSpace:
                    fileUploader = new SendSpace(ZKeys.SendSpaceKey);
                    switch (Program.UploadersConfig.SendSpaceAccountType)
                    {
                        case AccountType.Anonymous:
                            SendSpaceManager.PrepareUploadInfo(ZKeys.SendSpaceKey);
                            break;
                        case AccountType.User:
                            SendSpaceManager.PrepareUploadInfo(ZKeys.SendSpaceKey, Program.UploadersConfig.SendSpaceUsername, Program.UploadersConfig.SendSpacePassword);
                            break;
                    }
                    break;
                case FileDestination.Minus:
                    fileUploader = new Minus(Program.UploadersConfig.MinusConfig, new OAuthInfo(ZKeys.MinusConsumerKey, ZKeys.MinusConsumerSecret));
                    break;
                case FileDestination.Box:
                    fileUploader = new Box(ZKeys.BoxKey)
                    {
                        AuthToken = Program.UploadersConfig.BoxAuthToken,
                        FolderID = Program.UploadersConfig.BoxFolderID,
                        Share = Program.UploadersConfig.BoxShare
                    };
                    break;
                case FileDestination.CustomUploader:
                    if (Program.UploadersConfig.CustomUploadersList.HasValidIndex(Program.UploadersConfig.CustomUploaderSelected))
                    {
                        fileUploader = new CustomUploader(Program.UploadersConfig.CustomUploadersList[Program.UploadersConfig.CustomUploaderSelected]);
                    }
                    break;
                case FileDestination.FTP:
                    int index = Program.UploadersConfig.GetFtpIndex(Info.DataType);

                    if (Program.UploadersConfig.FTPAccountList2.HasValidIndex(index))
                    {
                        fileUploader = new FTPUploader(Program.UploadersConfig.FTPAccountList2[index]);
                    }
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
                    urlShortener = new BitlyURLShortener(ZKeys.BitlyLogin, ZKeys.BitlyKey);
                    break;
                case UrlShortenerType.Google:
                    urlShortener = new GoogleURLShortener(Program.UploadersConfig.GoogleURLShortenerAccountType, ZKeys.GoogleApiKey,
                        Program.UploadersConfig.GoogleURLShortenerOAuthInfo);
                    break;
                case UrlShortenerType.ISGD:
                    urlShortener = new IsgdURLShortener();
                    break;
                case UrlShortenerType.Jmp:
                    urlShortener = new JmpURLShortener(ZKeys.BitlyLogin, ZKeys.BitlyKey);
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
            uploader.BufferSize = (int)Math.Pow(2, Program.Settings.BufferSizePower) * 1024;
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