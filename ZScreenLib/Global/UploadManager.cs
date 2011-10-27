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
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows.Forms;
using HelpersLib;
using HistoryLib;
using UploadersLib;
using UploadersLib.HelperClasses;
using ZScreenLib.Properties;

namespace ZScreenLib
{
    /// <summary>
    /// Class reponsible for Adding or Retrieving Clipboard Text and Setting Text to Clipboard
    /// </summary>
    public static class UploadManager
    {
        public static MyListView ListViewControl { get; set; }

        public static List<UploadInfo> UploadInfoList = new List<UploadInfo>();

        public static UploadResult UploadResultLast { get; set; }

        public static int CumulativePercentage { get; private set; }

        private static int UniqueNumber = 0;

        public static List<WorkerTask> Tasks { get; private set; }

        static UploadManager()
        {
            Tasks = new List<WorkerTask>();
        }

        #region Queue

        public static void UploadImage(WorkerTask task)
        {
            if (task.tempImage != null)
            {
                StartUpload(task);
            }
        }

        private static void StartUpload(WorkerTask task)
        {
            Tasks.Add(task);
            task.ID = Tasks.Count - 1;
            task.UploadPreparing += new WorkerTask.TaskEventHandler(task_UploadPreparing);
            task.UploadStarted += new WorkerTask.TaskEventHandler(task_UploadStarted);
            task.UploadProgressChanged2 += new WorkerTask.TaskEventHandler(task_UploadProgressChanged);
            task.UploadCompleted += new WorkerTask.TaskEventHandler(task_UploadCompleted);
            CreateListViewItem(task);
            // StartTasks();
        }

        private static void StartTasks()
        {
            int workingTasksCount = Tasks.Count(x => x.IsWorking);
            WorkerTask[] inQueueTasks = Tasks.Where(x => x.Status == TaskStatus.InQueue).ToArray();

            if (inQueueTasks.Length > 0)
            {
                int len = inQueueTasks.Length;

                for (int i = 0; i < len; i++)
                {
                    inQueueTasks[i].Start();
                }
            }
        }

        private static void task_UploadPreparing(WorkerTask info)
        {
            Engine.EngineLogger.WriteLine("Upload preparing. ID: {0}", info.ID);
            ChangeListViewItemStatus(info);
        }

        private static void task_UploadStarted(WorkerTask task)
        {
            string status = string.Format("Upload started. ID: {0}, Filename: {1}", task.ID, task.Info.FileName);
            if (!string.IsNullOrEmpty(task.Info.LocalFilePath)) status += ", Filepath: " + task.Info.LocalFilePath;
            StaticHelper.WriteLine(status);

            ListViewItem lvi = ListViewControl.Items[task.ID];
            lvi.Text = task.Info.FileName;
            lvi.SubItems[1].Text = task.Status.GetDescription();
            lvi.ImageIndex = 0;
        }

        private static void task_UploadProgressChanged(WorkerTask info)
        {
            if (ListViewControl != null)
            {
                ListViewItem lvi = ListViewControl.Items[info.ID];
                lvi.SubItems[2].Text = string.Format("{0:N0}%  {1:N0} KiB / {2:N0} KiB", info.Progress.Percentage,
                    info.Progress.Position / 1024, info.Progress.Length / 1024);
                lvi.SubItems[3].Text = string.Format("{0:N0} kB/s", info.Progress.Speed);
                lvi.SubItems[4].Text = string.Format("{0:00}:{1:00}", info.Progress.Elapsed.Minutes, info.Progress.Elapsed.Seconds);
                lvi.SubItems[5].Text = string.Format("{0:00}:{1:00}", info.Progress.Remaining.Minutes, info.Progress.Remaining.Seconds);
            }
        }

        private static void task_UploadCompleted(WorkerTask task)
        {
            try
            {
                if (ListViewControl != null && task != null && task.Result != null)
                {
                    ListViewItem lvi = ListViewControl.Items[task.ID];
                    lvi.Tag = task.Result;

                    if (task.Result.IsError)
                    {
                        string errors = string.Join("\r\n\r\n", task.Result.Errors.ToArray());

                        StaticHelper.WriteLine("Upload failed. ID: {0}, Filename: {1}, Errors:\r\n{2}", task.ID, task.Info.FileName, errors);

                        lvi.SubItems[1].Text = "Error";
                        lvi.SubItems[8].Text = string.Empty;
                        lvi.ImageIndex = 1;

                        if (Engine.ConfigUI.CompleteSound)
                        {
                            SystemSounds.Asterisk.Play();
                        }
                    }
                    else
                    {
                        StaticHelper.WriteLine("Upload completed. ID: {0}, Filename: {1}, URL: {2}, Duration: {3}ms", task.ID, task.Info.FileName,
                            task.Result.URL, (int)task.UploadDuration);

                        lvi.SubItems[1].Text = task.Status.GetDescription();
                        lvi.ImageIndex = 2;

                        if (!string.IsNullOrEmpty(task.Result.URL))
                        {
                            string url = string.IsNullOrEmpty(task.Result.ShortenedURL) ? task.Result.URL : task.Result.ShortenedURL;

                            lvi.SubItems[8].Text = url;

                            if (Engine.ConfigWorkflow.ClipboardOverwrite)
                            {
                                ZAppHelper.CopyTextSafely(url);
                            }

                            if (Engine.ConfigUI.HistorySave)
                            {
                                HistoryManager.AddHistoryItemAsync(Engine.HistoryPath, task.GenerateHistoryItem());
                            }

                            //if (Engine.niTray.Visible)
                            //{
                            //    Program.mainForm.niTray.Tag = info.Result.URL;
                            //    Program.mainForm.niTray.ShowBalloonTip(5000, "ZUploader - Upload completed", info.Result.URL, ToolTipIcon.Info);
                            //}
                        }

                        if (Engine.ConfigUI.CompleteSound)
                        {
                            SystemSounds.Exclamation.Play();
                        }
                    }

                    lvi.EnsureVisible();
                }
            }
            finally
            {
                StartTasks();
                //  UpdateTrayIcon();
            }
        }

        private static void ChangeListViewItemStatus(WorkerTask wt)
        {
            if (ListViewControl != null)
            {
                ListViewItem lvi = ListViewControl.Items[wt.ID];
                lvi.SubItems[1].Text = wt.Status.GetDescription();
            }
        }

        private static void CreateListViewItem(WorkerTask wt)
        {
            if (ListViewControl != null)
            {
                StaticHelper.WriteLine("Upload in queue. ID: {0}, Job: {1}, Type: {2}, Host: {3}",
                    wt.ID, wt.Job1, wt.Job2, wt.GetDestinationName());

                ListViewItem lvi = new ListViewItem();
                lvi.Text = wt.Info.FileName;
                lvi.SubItems.Add("In queue");
                lvi.SubItems.Add(string.Empty);
                lvi.SubItems.Add(string.Empty);
                lvi.SubItems.Add(string.Empty);
                lvi.SubItems.Add(string.Empty);
                lvi.SubItems.Add(wt.GetDescription());
                lvi.SubItems.Add(wt.GetDestinationName());
                lvi.SubItems.Add(string.Empty);
                lvi.BackColor = wt.ID % 2 == 0 ? Color.White : Color.WhiteSmoke;
                lvi.ImageIndex = 3;
                ListViewControl.Items.Add(lvi);
                lvi.EnsureVisible();
                ListViewControl.FillLastColumn();
            }
        }

        #endregion Queue

        /// <summary>
        /// Function to be called when a new Worker thread starts
        /// </summary>
        public static int Queue()
        {
            int number = UniqueNumber++;
            UploadInfoList.Add(new UploadInfo(number));
            return number;
        }

        public static void SetCumulativePercentatge(int perc)
        {
            CumulativePercentage = Math.Max(CumulativePercentage, perc);
        }

        public static void Clear()
        {
            UploadInfoList.Clear();
        }

        /// <summary>
        /// Remove Last Screenshot from Clipboard Manager after setting to Clipboard
        /// </summary>
        public static bool Commit(int number)
        {
            UploadInfo find = GetInfo(number);
            if (find != null)
            {
                return UploadInfoList.Remove(find);
            }
            return false;
        }

        public static UploadInfo GetInfo(int number)
        {
            return UploadInfoList.Find(x => x.ID == number);
        }

        public static int GetAverageProgress()
        {
            return UploadInfoList.Sum(x => x.UploadPercentage) / UploadInfoList.Count;
        }

        /// <summary>
        /// Sets Clipboard text and returns the content
        /// </summary>
        /// <returns></returns>
        public static void ShowUploadResults(WorkerTask task, bool showDialog)
        {
            if (!task.WorkflowConfig.ClipboardOverwrite && !Clipboard.ContainsFileDropList() && !Clipboard.ContainsImage() && !Clipboard.ContainsText() || task.WorkflowConfig.ClipboardOverwrite)
            {
                if (task.UploadResults.Count > 0)
                {
                    if (task.WorkflowConfig.DestConfig.TaskClipboardContent.Count > 1 || Engine.ConfigUI.ShowUploadResultsWindow || showDialog)
                    {
                        ClipboardOptions cmp = new ClipboardOptions(task);
                        cmp.Icon = Resources.zss_main;
                        if (showDialog) { cmp.ShowDialog(); }
                        else { NativeMethods.ShowWindow(cmp.Handle, (int)WindowShowStyle.ShowNoActivate); }
                        showDialog = true;
                    }
                }

                if (task.WorkflowConfig.DestConfig.Outputs.Contains(OutputEnum.Clipboard))
                {
                    StringBuilder clipboardText = new StringBuilder();

                    if (task.JobIsImageToClipboard())
                    {
                        Adapter.CopyImageToClipboard(task.tempImage);
                    }
                    else if (task.WorkflowConfig.DestConfig.TaskClipboardContent.Contains(ClipboardContentEnum.Local))
                    {
                        foreach (UploadResult ur in task.UploadResults)
                        {
                            if (Engine.ConfigUI.ConfLinkFormat.Count > 0)
                            {
                                clipboardText.AppendLine(ur.GetUrlByType((LinkFormatEnum)task.WorkflowConfig.DestConfig.LinkFormat[0], ur.LocalFilePath));
                            }

                            if (!Engine.ConfigUI.ClipboardAppendMultipleLinks && clipboardText.Length > 0)
                            {
                                break;
                            }
                        }
                    }
                    else if (task.WorkflowConfig.DestConfig.TaskClipboardContent.Contains(ClipboardContentEnum.OCR))
                    {
                        if (!string.IsNullOrEmpty(task.OCRText))
                        {
                            clipboardText.Append(task.OCRText);
                        }
                    }

                // If the user requests for the full image URL, preference is given for the Shortened URL is exists
                    else if (task.Job1 == EDataType.Image && task.WorkflowConfig.DestConfig.LinkFormat.Contains((int)LinkFormatEnum.FULL))
                    {
                        if (task.Job3 == WorkerTask.JobLevel3.ShortenURL && !string.IsNullOrEmpty(task.UploadResults[0].ShortenedURL))
                        {
                            foreach (UploadResult ur in task.UploadResults)
                            {
                                if (!string.IsNullOrEmpty(ur.ShortenedURL))
                                {
                                    clipboardText.AppendLine(ur.ShortenedURL);
                                    if (!Engine.ConfigUI.ClipboardAppendMultipleLinks && clipboardText.Length > 0)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        // If no shortened URL exists then default full URL will be used
                        else
                        {
                            foreach (UploadResult ur in task.UploadResults)
                            {
                                if (!string.IsNullOrEmpty(ur.URL))
                                {
                                    clipboardText.AppendLine(FileSystem.GetBrowserFriendlyUrl(ur.URL));
                                    if (!Engine.ConfigUI.ClipboardAppendMultipleLinks && clipboardText.Length > 0)
                                    {
                                        break;
                                    }
                                }
                            }
                            if (clipboardText.Length == 0 && task.WorkflowConfig.DestConfig.TaskClipboardContent.Contains(ClipboardContentEnum.Local))
                            {
                                foreach (UploadResult ur in task.UploadResults)
                                {
                                    if (!string.IsNullOrEmpty(ur.LocalFilePath))
                                    {
                                        clipboardText.AppendLine(ur.LocalFilePath);
                                        if (!Engine.ConfigUI.ClipboardAppendMultipleLinks && clipboardText.Length > 0)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    else
                    {
                        // From this point onwards app needs to respect all other Clipboard URL modes for Images
                        if (task.UploadResults.Count > 0 && task.Job1 == EDataType.Image)
                        {
                            foreach (UploadResult ur in task.UploadResults)
                            {
                                if (task.WorkflowConfig.DestConfig.LinkFormat.Count > 0)
                                {
                                    clipboardText.AppendLine(ur.GetUrlByType((LinkFormatEnum)task.WorkflowConfig.DestConfig.LinkFormat[0], ur.URL));
                                }

                                if (!Engine.ConfigUI.ClipboardAppendMultipleLinks && clipboardText.Length > 0)
                                {
                                    break;
                                }
                            }
                        }
                        // Text and File catagories are still left to process. Exception for Google Translate
                        else if (task.Job1 == EDataType.Text && task.Job2 == WorkerTask.JobLevel2.Translate)
                        {
                            if (task.TranslationInfo != null)
                            {
                                clipboardText.AppendLine(task.TranslationInfo.Result);
                            }
                        }
                        // Text and File catagories are still left to process. If shortened URL exists, preference is given to that
                        else if (task.UploadResults.Count > 0 && task.Job3 == WorkerTask.JobLevel3.ShortenURL && !string.IsNullOrEmpty(task.UploadResults[0].ShortenedURL))
                        {
                            foreach (UploadResult ur in task.UploadResults)
                            {
                                if (!string.IsNullOrEmpty(ur.ShortenedURL))
                                {
                                    clipboardText.AppendLine(ur.ShortenedURL);
                                    break;
                                }
                            }
                        }
                        // Otherwise full URL for Text or File is used
                        else if (task.UploadResults.Count > 0)
                        {
                            clipboardText.AppendLine(FileSystem.GetBrowserFriendlyUrl(task.UploadResults[0].URL));
                        }
                    }

                    if (clipboardText.Length > 0)
                    {
                        bool bClipboardFormatOnLongUrl = Engine.ConfigUI.EnableClipboardFormatForLongURLs && task.Job1 == EDataType.URL;
                        bool bClipboardFormat = bClipboardFormatOnLongUrl || task.Job1 != EDataType.URL;
                        string tempText = string.Empty;
                        NameParser np = new NameParser(NameParserType.Clipboard);
                        np.link = clipboardText.ToString().Trim();
                        np.size = task.Info.FileSize;
                        np.name = task.Info.FileName;
                        if (bClipboardFormat)
                        {
                            np.WindowText = task.Info.WindowTitleText;
                        }

                        tempText = bClipboardFormat ? np.Convert(Engine.ConfigUI.ClipboardFormat) : clipboardText.ToString().Trim();

                        if (!string.IsNullOrEmpty(tempText))
                        {
                            StaticHelper.WriteLine("Setting Clipboard with URL: " + tempText);
                            Clipboard.SetText(tempText); // auto
                            Engine.zPreviousSetClipboardText = tempText;
                            Engine.IsClipboardUploading = false;

                            // optional deletion link
                            if (task.UploadResults != null)
                            {
                                foreach (UploadResult ur in task.UploadResults)
                                {
                                    string linkdel = ur.DeletionURL;
                                    if (!string.IsNullOrEmpty(linkdel))
                                    {
                                        StaticHelper.WriteLine("Deletion Link: " + linkdel);
                                    }
                                }
                            }
                        }
                    }
                }

                if (!showDialog)
                {
                    task.Dispose();
                }
            }
        }

        public static void ResetCumulativePercentage()
        {
            CumulativePercentage = 0;
        }
    }
}