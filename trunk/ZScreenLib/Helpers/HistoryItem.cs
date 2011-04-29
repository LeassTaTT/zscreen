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

using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using UploadersLib;
using UploadersLib.HelperClasses;

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
        /// ImageShack, TinyPic, FTP...
        /// </summary>
        public string DestinationName { get; set; }
        public string Description { get; set; }
        public ImageFileManager ScreenshotManager { get; set; }
        public JobLevel1 JobCategory { get; set; }
        public ImageUploaderType ImageDestCategory { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string UploadDuration { get; set; }
        public long FileSize { get; set; }

        public HistoryItem() { }

        public HistoryItem(WorkerTask task)
        {
            this.JobName = task.Job2.GetDescription();
            this.FileName = task.FileName.ToString();
            this.LocalPath = task.LocalFilePath;
            if (string.IsNullOrEmpty(task.RemoteFilePath) && task.LinkManager != null)
            {
                string fp = Engine.Portable ? Path.Combine(Application.StartupPath, this.LocalPath) : this.LocalPath;
                this.RemotePath = task.LinkManager.GetLocalFilePathAsUri(fp);
            }
            else
            {
                this.RemotePath = task.RemoteFilePath;
            }
            this.DestinationName = GetDestinationName(task);
            this.ScreenshotManager = task.LinkManager;
            this.JobCategory = task.Job1;
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
            switch (t.Job1)
            {
                case JobLevel1.IMAGES:
                    return string.Format("{0}", t.MyImageUploader.GetDescription());
                case JobLevel1.TEXT:
                    string dest = string.Empty;
                    switch (t.Job3)
                    {
                        case WorkerTask.JobLevel3.ShortenURL:
                            dest = t.MyUrlShortenerType.GetDescription();
                            break;
                        default:
                            dest = t.MyTextUploader.GetDescription();
                            break;
                    }
                    return string.Format("{0}", dest);
                case JobLevel1.BINARY:
                    switch (t.MyFileUploader)
                    {
                        case FileUploaderType.CustomUploader:
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