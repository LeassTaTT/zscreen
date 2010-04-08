#region License Information (GPL v2)
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

using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using UploadersLib;
using ZSS;

namespace ZScreenLib
{
    public class BalloonTipHelper
    {
        private WorkerTask task;
        private NotifyIcon niTray;

        public BalloonTipHelper(NotifyIcon notifyIcon)
        {
            this.niTray = notifyIcon;
        }

        public BalloonTipHelper(NotifyIcon notifyIcon, WorkerTask task)
            : this(notifyIcon)
        {
            this.task = task;
        }

        public string ShowBalloonTip()
        {
            StringBuilder sbMsg = new StringBuilder();
            ToolTipIcon tti = ToolTipIcon.Info;

            niTray.Tag = task;

            if (task.Job == WorkerTask.Jobs.LANGUAGE_TRANSLATOR)
            {
                //sbMsg.AppendLine("Languages: " + task.TranslationInfo.Result.TranslationType);
                sbMsg.AppendLine(task.TranslationInfo.Result.TranslationType);
                sbMsg.AppendLine("Source: " + task.TranslationInfo.SourceText);
                sbMsg.AppendLine("Result: " + task.TranslationInfo.Result.TranslatedText);
                //sbMsg.AppendLine(string.Format("{0} >> {1}", task.TranslationInfo.Result.SourceText, task.TranslationInfo.Result.TranslatedText));
            }
            else
            {
                switch (task.JobCategory)
                {
                    case JobCategoryType.TEXT:
                        sbMsg.AppendLine(string.Format("Destination: {0}", task.MyTextUploader));
                        break;
                    case JobCategoryType.SCREENSHOTS:
                    case JobCategoryType.PICTURES:
                        switch (task.MyImageUploader)
                        {
                            case ImageDestType.CUSTOM_UPLOADER:
                                sbMsg.AppendLine(string.Format("Destination: {0} ({1})", task.MyImageUploader.GetDescription(), task.DestinationName));
                                break;
                            default:
                                sbMsg.AppendLine(string.Format("Destination: {0}", task.MyImageUploader.GetDescription()));
                                break;
                        }
                        break;
                }


                string fileOrUrl = "";

                if (task.MyImageUploader == ImageDestType.CLIPBOARD || task.MyImageUploader == ImageDestType.FILE)
                {
                    // just local file 
                    if (!string.IsNullOrEmpty(task.FileName.ToString()))
                    {
                        sbMsg.AppendLine("Name: " + task.FileName);
                    }
                    fileOrUrl = string.Format("{0}: {1}", task.MyImageUploader.GetDescription(), task.LocalFilePath);
                }
                else
                {
                    // remote file
                    if (!string.IsNullOrEmpty(task.RemoteFilePath))
                    {
                        if (task.FileName != null && !string.IsNullOrEmpty(task.FileName.ToString()))
                        {
                            sbMsg.AppendLine("Name: " + task.FileName);
                        }
                        fileOrUrl = string.Format("URL: {0}", task.RemoteFilePath);

                        if (string.IsNullOrEmpty(task.RemoteFilePath) && task.Errors.Count > 0)
                        {
                            tti = ToolTipIcon.Warning;
                            sbMsg.AppendLine("Warnings: ");
                            foreach (string err in task.Errors)
                            {
                                sbMsg.AppendLine(err);
                            }
                        }
                    }
                    else
                    {
                        if (task.Errors.Count > 0)
                        {
                            tti = ToolTipIcon.Error;
                            fileOrUrl = "Warning: " + task.Errors[task.Errors.Count - 1];
                        }
                    }
                }

                if (!string.IsNullOrEmpty(fileOrUrl))
                {
                    sbMsg.AppendLine(fileOrUrl);
                }

                if (Engine.conf.ShowUploadDuration && task.UploadDuration > 0)
                {
                    sbMsg.AppendLine("Upload duration: " + task.UploadDuration + " ms");
                }
            }

            string message = sbMsg.ToString();

            if (!string.IsNullOrEmpty(message))
            {
                niTray.ShowBalloonTip(1000, Application.ProductName, message, tti);
            }

            return message;
        }

        public void ClickBalloonTip()
        {
            if (niTray.Tag != null)
            {
                WorkerTask task = (WorkerTask)niTray.Tag;
                string cbString;
                switch (task.Job)
                {
                    case WorkerTask.Jobs.LANGUAGE_TRANSLATOR:
                        cbString = task.TranslationInfo.Result.TranslatedText;
                        if (!string.IsNullOrEmpty(cbString))
                        {
                            Clipboard.SetText(cbString);
                        }
                        break;
                    default:
                        switch (task.MyImageUploader)
                        {
                            case ImageDestType.FILE:
                            case ImageDestType.CLIPBOARD:
                                cbString = task.LocalFilePath;
                                if (!string.IsNullOrEmpty(cbString))
                                {
                                    Process.Start(cbString);
                                }
                                break;
                            default:
                                cbString = task.RemoteFilePath;
                                if (!string.IsNullOrEmpty(cbString))
                                {
                                    Process.Start(cbString);
                                }
                                break;
                        }
                        break;
                }
            }
        }
    }
}