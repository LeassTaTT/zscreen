﻿#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2009  Brandon Zimmerman

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
using System.IO;
using System.Text;
using UploadersLib;
using UploadersLib.Helpers;
using System.Windows.Forms;

namespace ZScreenLib
{
    public class HistoryItem
    {
        public string JobName { get; set; }
        public string FileName { get; set; }
        private string mLocalPath = string.Empty;
        public string LocalPath
        {
            get
            {
                return Engine.Portable ? Path.Combine(Application.StartupPath, mLocalPath) : mLocalPath;
            }
            set
            {
                mLocalPath = value;
            }
        }
        public string RemotePath { get; set; }
        /// <summary>
        /// Full Image, Active Window, Cropped Window...
        /// </summary>
        public string DestinationMode { get; set; }
        /// <summary>
        /// ImageShack, TinyPic, FTP...
        /// </summary>
        public string DestinationName { get; set; }
        public string Description { get; set; }
        public ImageFileManager ScreenshotManager { get; set; }
        public JobCategoryType JobCategory { get; set; }
        public ImageDestType ImageDestCategory { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string UploadDuration { get; set; }
        public long FileSize { get; set; }

        public HistoryItem() { }

        public HistoryItem(WorkerTask task)
        {
            this.JobName = task.Job.GetDescription();
            this.FileName = task.FileName.ToString();
            this.LocalPath = task.LocalFilePath;
            if (string.IsNullOrEmpty(task.RemoteFilePath) && task.LinkManager != null) 
            {
            	this.RemotePath = task.LinkManager.GetLocalFilePathAsUri();
            }
            else if (task.LinkManager != null) 
            {
            	this.RemotePath = task.RemoteFilePath;
            }
            this.DestinationMode = task.MyImageUploader.GetDescription();
            this.DestinationName = GetDestinationName(task);
            this.ScreenshotManager = task.LinkManager;
            this.JobCategory = task.JobCategory;
            this.ImageDestCategory = task.MyImageUploader;
            this.StartTime = task.StartTime;
            this.EndTime = task.EndTime;
            this.UploadDuration = task.UploadDuration + " ms";
            if (!string.IsNullOrEmpty(this.LocalPath) && File.Exists(this.LocalPath))
            {
                this.FileSize = new FileInfo(this.LocalPath).Length;
            }

            this.Description = task.GetDescription();
        }

        public override string ToString()
        {
            switch (Engine.conf.HistoryListFormat)
            {
                case HistoryListFormat.NAME:
                    return FileName;
                case HistoryListFormat.TIME_NAME:
                    return EndTime.ToLongTimeString() + " - " + FileName;
                case HistoryListFormat.DATE_TIME_NAME:
                    return EndTime.ToShortDateString() + " - " + EndTime.ToLongTimeString() + " - " + FileName;
                case HistoryListFormat.DATE_NAME:
                    return EndTime.ToShortDateString() + " - " + FileName;
                default:
                    return FileName;
            }
        }

        public string GetStatistics()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(String.Format("Job: {0} ({1})", this.JobName, this.DestinationName));
            sb.AppendLine(String.Format("Date Started: {0}", this.StartTime.ToShortDateString()));
            sb.AppendLine(String.Format("Time Started: {0}", this.StartTime.ToLongTimeString()));
            sb.AppendLine(String.Format("Time Uploaded: {0}", this.EndTime.ToLongTimeString()));
            if (this.FileSize == 0 && !string.IsNullOrEmpty(this.LocalPath) && File.Exists(this.LocalPath))
            {
                this.FileSize = new FileInfo(this.LocalPath).Length;
            }

            if (this.FileSize > 0)
            {
                sb.AppendLine(String.Format("File Size: {0} ({1} bytes)", FileSystem.GetFileSize(this.FileSize), this.FileSize.ToString("N0")));
            }

            sb.AppendLine(String.Format("Upload Duration: {0}", this.UploadDuration));
            return sb.ToString().TrimEnd();
        }

        private string GetDestinationName(WorkerTask t)
        {
            switch (t.JobCategory)
            {
                case JobCategoryType.PICTURES:
                case JobCategoryType.SCREENSHOTS:
                    switch (t.MyImageUploader)
                    {
                        case ImageDestType.CUSTOM_UPLOADER:
                            return string.Format("{0}: {1}", t.MyImageUploader.GetDescription(), t.DestinationName);
                        default:
                            return string.Format("{0}", t.MyImageUploader.GetDescription());
                    }
                case JobCategoryType.TEXT:
                    return string.Format("{0}", t.MyTextUploader.ToString());
                case JobCategoryType.BINARY:
                    switch (t.MyFileUploader)
                    {
                        case FileUploaderType.FTP:
                            return string.Format("{0}: {1}", t.MyImageUploader.GetDescription(), t.DestinationName);
                        default:
                            return string.Format("{0}", t.MyImageUploader.GetDescription());
                    }
            }

            return string.Empty;
        }
    }
}