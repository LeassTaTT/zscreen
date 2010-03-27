﻿#region License Information (GPL v2)
/*
    ZUploader - A program that allows you to upload images, text or files in your clipboard
    Copyright (C) 2010 ZScreen Developers

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
#endregion

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using UploadersLib;
using UploadersLib.FileUploaders;
using UploadersLib.Helpers;
using UploadersLib.ImageUploaders;
using UploadersLib.TextUploaders;
using ZUploader.Properties;

namespace ZUploader
{
    public class Task : IDisposable
    {
        public delegate void UploadStartedEventHandler(Task sender);
        public delegate void UploadCompletedEventHandler(Task sender, UploadResult result);
        public delegate void UploadProgressChangedEventHandler(Task sender, int progress);

        public event UploadStartedEventHandler UploadStarted;
        public event UploadCompletedEventHandler UploadCompleted;
        public event UploadProgressChangedEventHandler UploadProgressChanged;

        public DataManager DataManager { get; private set; }
        public int ID { get; private set; }
        public int Progress { get; private set; }

        private BackgroundWorker bw;

        #region Constructors

        public Task()
        {
            DataManager = new DataManager();
            ID = UploadManager.GetID();
        }

        public Task(EDataType dataType, string filePath)
            : this()
        {
            DataManager.FileType = dataType;
            DataManager.FileName = Path.GetFileName(filePath);

            switch (dataType)
            {
                case EDataType.File:
                    DataManager.Data = File.ReadAllBytes(filePath);
                    break;
                case EDataType.Image:
                    DataManager.Image = Image.FromFile(filePath);
                    break;
                case EDataType.Text:
                    DataManager.Text = File.ReadAllText(filePath);
                    break;
            }
        }

        public Task(EDataType dataType, Stream stream, string fileName)
            : this()
        {
            switch (DataManager.FileType = dataType)
            {
                case EDataType.File:
                    DataManager.Data = Helpers.GetBytes(stream);
                    break;
                case EDataType.Image:
                    DataManager.Image = Image.FromStream(stream);
                    break;
                case EDataType.Text:
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        DataManager.Text = sr.ReadToEnd();
                    }
                    break;
            }

            DataManager.FileName = fileName;
        }

        public Task(byte[] data, string fileName)
            : this()
        {
            DataManager.FileType = EDataType.File;
            DataManager.Data = data;
            DataManager.FileName = fileName;
        }

        public Task(Image image, string fileName)
            : this()
        {
            DataManager.FileType = EDataType.Image;
            DataManager.Image = image;
            DataManager.FileName = fileName;
        }

        public Task(string text)
            : this()
        {
            DataManager.FileType = EDataType.Text;
            DataManager.Text = text;
        }

        #endregion

        public void Start()
        {
            OnUploadStarted();

            bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.DoWork += new DoWorkEventHandler(UploadThread);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.RunWorkerAsync();
        }

        private void UploadThread(object sender, DoWorkEventArgs e)
        {
            switch (DataManager.FileType)
            {
                case EDataType.File:
                    e.Result = UploadFile(DataManager.Data, DataManager.FileName);
                    break;
                case EDataType.Image:
                    e.Result = UploadImage(DataManager.Image, DataManager.FileName);
                    break;
                case EDataType.Text:
                    e.Result = UploadText(DataManager.Text);
                    break;
            }
        }

        public UploadResult UploadImage(Image image, string fileName)
        {
            ImageUploader imageUploader = null;

            switch (UploadManager.ImageUploader)
            {
                case ImageDestType2.IMAGESHACK:
                    imageUploader = new ImageShackUploader("78EHNOPS04e77bc6df1cc0c5fc2e92e11c7b4a1a", string.Empty, UploadMode.API);
                    ((ImageShackUploader)imageUploader).Public = false;
                    break;
                case ImageDestType2.TINYPIC:
                    imageUploader = new TinyPicUploader("e2aabb8d555322fa", "00a68ed73ddd54da52dc2d5803fa35ee", UploadMode.API);
                    break;
                case ImageDestType2.IMAGEBIN:
                    imageUploader = new ImageBin();
                    break;
                case ImageDestType2.IMG1:
                    imageUploader = new Img1Uploader();
                    break;
                case ImageDestType2.IMGUR:
                    imageUploader = new Imgur();
                    break;
                default:
                    break;
                /*
                case ImageDestType.FLICKR:
                    imageUploader = new FlickrUploader(Engine.conf.FlickrAuthInfo, Engine.conf.FlickrSettings);
                    break;
                case ImageDestType.IMAGEBAM:
                    ImageBamUploaderOptions imageBamOptions = new ImageBamUploaderOptions(Engine.conf.ImageBamApiKey, Engine.conf.ImageBamSecret,
                    Adapter.GetImageBamGalleryActive()) { NSFW = Engine.conf.ImageBamContentNSFW };
                    imageUploader = new ImageBamUploader(imageBamOptions);
                    break;
                case ImageDestType.TWITPIC:
                    TwitPicOptions twitpicOpt = new TwitPicOptions();
                    twitpicOpt.UserName = Adapter.TwitterGetActiveAcct().UserName;
                    twitpicOpt.Password = Adapter.TwitterGetActiveAcct().Password;
                    // twitpicOpt.TwitPicUploadType = Engine.conf.TwitPicUploadMode;
                    twitpicOpt.TwitPicThumbnailMode = Engine.conf.TwitPicThumbnailMode;
                    twitpicOpt.ShowFull = Engine.conf.TwitPicShowFull;
                    imageUploader = new TwitPicUploader(twitpicOpt);
                    break;
                case ImageDestType.TWITSNAPS:
                    TwitSnapsOptions twitsnapsOpt = new TwitSnapsOptions();
                    twitsnapsOpt.UserName = Adapter.TwitterGetActiveAcct().UserName;
                    twitsnapsOpt.Password = Adapter.TwitterGetActiveAcct().Password;
                    imageUploader = new TwitSnapsUploader(twitsnapsOpt);
                    break;
                case ImageDestType.YFROG:
                    YfrogOptions yfrogOp = new YfrogOptions(Engine.IMAGESHACK_KEY);
                    yfrogOp.UserName = Adapter.TwitterGetActiveAcct().UserName;
                    yfrogOp.Password = Adapter.TwitterGetActiveAcct().Password;
                    yfrogOp.Source = Application.ProductName;
                    // yfrogOp.UploadType = Engine.conf.YfrogUploadMode;
                    imageUploader = new YfrogUploader(yfrogOp);
                    break;
                 */
            }

            if (imageUploader != null)
            {
                imageUploader.ProgressChanged += (x) => bw.ReportProgress(x);
                ImageFileManager ifm = imageUploader.UploadImage(image, fileName);
                UploadResult ur = new UploadResult
                {
                    URL = ifm.GetFullImageUrl(),
                    ThumbnailURL = ifm.GetThumbnailUrl(),
                    DeletionURL = ifm.GetDeletionLink(),
                    Errors = imageUploader.Errors
                };
                return ur;
            }

            return null;
        }

        public UploadResult UploadText(string text)
        {
            TextUploader textUploader = null;

            switch (UploadManager.TextUploader)
            {
                case TextDestType2.PASTE2:
                    textUploader = new Paste2Uploader();
                    break;
                case TextDestType2.PASTEBIN:
                    textUploader = new PastebinUploader();
                    break;
                case TextDestType2.PASTEBIN_CA:
                    textUploader = new PastebinCaUploader();
                    break;
                case TextDestType2.SLEXY:
                    textUploader = new SlexyUploader();
                    break;
                default:
                    break;
            }

            if (textUploader != null)
            {
                string url = textUploader.UploadText(TextInfo.FromString(text));
                UploadResult ur = new UploadResult
                {
                    URL = url,
                    Errors = textUploader.Errors
                };
                return ur;
            }

            return null;
        }

        public UploadResult UploadFile(byte[] data, string fileName)
        {
            FileUploader fileUploader = null;

            switch (UploadManager.FileUploader)
            {
                case FileUploaderType2.FTP:
                    fileUploader = new FTPUploader(Settings.Default.FTPAccount);
                    break;
                case FileUploaderType2.SendSpace:
                    fileUploader = new SendSpace();
                    SendSpaceManager.PrepareUploadInfo(null, null);
                    break;
                case FileUploaderType2.RapidShare:
                    fileUploader = new RapidShare(new RapidShareOptions()
                    {
                        AccountType = RapidShareAcctType.Free
                    });
                    break;
                case FileUploaderType2.FileBin:
                    fileUploader = new FileBin();
                    break;
            }

            if (fileUploader != null)
            {
                fileUploader.ProgressChanged += (x) => bw.ReportProgress(x);
                string url = fileUploader.Upload(data, fileName);
                UploadResult ur = new UploadResult
                {
                    URL = url,
                    Errors = fileUploader.Errors
                };
                return ur;
            }

            return null;
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (Progress < e.ProgressPercentage)
            {
                Progress = e.ProgressPercentage;
                OnUploadProgressChanged(Math.Min(Progress, 99));
            }
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            UploadResult ur = (UploadResult)e.Result;
            OnUploadCompleted(ur);
        }

        private void OnUploadStarted()
        {
            if (UploadStarted != null)
            {
                UploadStarted(this);
            }
        }

        private void OnUploadCompleted(UploadResult result)
        {
            if (UploadCompleted != null)
            {
                UploadCompleted(this, result);
            }
        }

        private void OnUploadProgressChanged(int progress)
        {
            if (UploadProgressChanged != null)
            {
                UploadProgressChanged(this, progress);
            }
        }

        public void Dispose()
        {
            if (DataManager != null)
            {
                DataManager.Dispose();
            }
        }
    }
}