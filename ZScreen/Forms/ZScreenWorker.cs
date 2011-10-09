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
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using Gif.Components;
using GraphicsMgrLib;
using HelpersLib;
using HistoryLib;
using Microsoft.WindowsAPICodePack.Taskbar;
using UploadersLib;
using UploadersLib.HelperClasses;
using UploadersLib.OtherServices;
using ZScreenGUI.Properties;
using ZScreenLib;
using ZSS.ColorsLib;
using ZUploader.HelperClasses;

namespace ZScreenGUI
{
    public partial class ZScreen : ZScreenCoreUI
    {
        internal bool bAutoScreenshotsOpened, bDropWindowOpened;

        #region Worker Events

        public override void BwApp_DoWork(object sender, DoWorkEventArgs e)
        {
            WorkerTask bwTask = (WorkerTask)e.Argument;
            if (bwTask.States.Contains(WorkerTask.TaskState.CancellationPending))
            {
                return;
            }

            bwTask.States.Add(WorkerTask.TaskState.ThreadMode);

            bwTask.ID = UploadManager.Queue();

            if (Engine.conf.PromptForUpload && !bwTask.TaskClipboardContent.Contains(ClipboardContentEnum.Data) &&
                !bwTask.TaskClipboardContent.Contains(ClipboardContentEnum.Local) &&
                (bwTask.Job2 == WorkerTask.JobLevel2.CaptureEntireScreen ||
                bwTask.Job2 == WorkerTask.JobLevel2.CaptureActiveWindow) &&
                MessageBox.Show("Do you really want to upload to " + bwTask.GetActiveImageUploadersDescription() + "?",
                Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                e.Result = bwTask;
                return;
            }

            if (bwTask.WasToTakeScreenshot)
            {
                if (Engine.conf.ScreenshotDelayTime > 0)
                {
                    Thread.Sleep((int)Engine.conf.ScreenshotDelayTime);
                }
            }

            bwTask.PublishData();

            if (bwTask.WorkflowConfig.Outputs.Contains(OutputEnum.RemoteHost) && bwTask.UploadResults.Count > 0)
            {
                foreach (UploadResult ur in bwTask.UploadResults)
                {
                    if (bwTask.ShouldShortenURL(ur.URL) && string.IsNullOrEmpty(ur.ShortenedURL))
                    {
                        bwTask.ShortenURL(ur, ur.URL);
                    }
                }
            }

            e.Result = bwTask;
        }

        public override void BwApp_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch ((WorkerTask.ProgressType)e.ProgressPercentage)
            {
                case WorkerTask.ProgressType.PrintImage:
                    Adapter.PrintImage(e.UserState as Image);
                    break;
                case WorkerTask.ProgressType.PrintText:
                    Adapter.PrintText(e.UserState as string);
                    break;
                case (WorkerTask.ProgressType)102:
                    Adapter.CopyImageToClipboard(e.UserState as Image);
                    break;
                case (WorkerTask.ProgressType)103:
                    Adapter.SaveImage(e.UserState as Image);
                    break;
                case (WorkerTask.ProgressType)104:
                    Adapter.CopyDataToClipboard(e.UserState);
                    break;
                case WorkerTask.ProgressType.COPY_TO_CLIPBOARD_IMAGE:
                    if (e.UserState.GetType() == typeof(string))
                    {
                        Adapter.CopyImageToClipboard(e.UserState as string);
                    }
                    else if (e.UserState is Image)
                    {
                        Adapter.CopyImageToClipboard((Image)e.UserState);
                    }

                    break;
                case WorkerTask.ProgressType.FLASH_ICON:
                    Adapter.FlashNotifyIcon(this.niTray, e.UserState as Icon);
                    break;
                case WorkerTask.ProgressType.UpdateCropMode:
                    this.cboCropGridMode.Checked = Engine.conf.CropGridToggle;
                    break;
                case WorkerTask.ProgressType.CHANGE_TRAY_ICON_PROGRESS:
                    UploadManager.SetCumulativePercentatge((int)((ProgressManager)e.UserState).Percentage);
                    Adapter.UpdateNotifyIconProgress(this.niTray, UploadManager.CumulativePercentage);
                    Adapter.TaskbarSetProgressValue(this, UploadManager.CumulativePercentage);
                    this.Text = string.Format("{0}% - {1}", UploadManager.CumulativePercentage, Engine.GetProductName());
                    break;
                case WorkerTask.ProgressType.UPDATE_PROGRESS_MAX:
                    TaskbarProgressBarState tbps = (TaskbarProgressBarState)e.UserState;
                    Adapter.TaskbarSetProgressState(this, tbps);
                    break;
                case WorkerTask.ProgressType.ShowBalloonTip:
                    WorkerTask task = e.UserState as WorkerTask;
                    if (Engine.conf.ShowBalloonTip)
                    {
                        ShowBalloonTip(task);
                    }
                    break;
                case WorkerTask.ProgressType.ShowTrayWarning:
                    Adapter.TaskbarSetProgressValue(this, 33);
                    Adapter.TaskbarSetProgressState(this, TaskbarProgressBarState.Error);
                    Adapter.SetNotifyIconBalloonTip(this.niTray, this.Text, e.UserState as string, ToolTipIcon.Warning);
                    break;
            }

            rtbDebugLog.Text = Engine.EngineLogger.ToString();
        }

        public override void BwApp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            WorkerTask task = (WorkerTask)e.Result;
            if (task == null) return;

            this.Text = Engine.GetProductName();
            niTray.Tag = task;

            if (task.UploadResults.Count > 0)
            {
                UploadManager.UploadResultLast = task.UploadResults[task.UploadResults.Count - 1];
                UploadManager.ResetCumulativePercentage();
            }

            try
            {
                if (task.IsError && task.Errors[0].Contains(ExceptionMessage.ProxyAuthenticationRequired))
                {
                    ProxyConfig pc = new ProxyConfig();
                    if (pc.ShowDialog() == DialogResult.OK)
                    {
                        this.ProxyAdd(pc.Proxy);
                        this.cboProxyConfig.SelectedIndex = (int)ProxyConfigType.ManualProxy;
                        Uploader.ProxySettings = Adapter.CheckProxySettings();
                    }
                    RetryTask(task);
                }

                WorkerTask checkTask = RetryUpload(task);

                if (checkTask.States.Contains(WorkerTask.TaskState.RetryPending))
                {
                    string message = string.Format("{0}\r\n\r\nAutomatically retrying upload for {1}.", string.Join("\r\n", task.Errors.ToArray()), checkTask.GetActiveImageUploadersDescription());
                    niTray.ShowBalloonTip(5000, Application.ProductName, message, ToolTipIcon.Warning);
                }
                else
                {
                    task.States.Add(WorkerTask.TaskState.Finished);
                    StaticHelper.WriteLine(string.Format("Job completed: {0}", task.Job2));

                    if (task.TaskClipboardContent.Contains(ClipboardContentEnum.Local) && Engine.conf.ShowSaveFileDialogImages)
                    {
                        string fp = Adapter.SaveImage(task.tempImage);
                        if (!string.IsNullOrEmpty(fp))
                        {
                            task.UpdateLocalFilePath(fp);
                        }
                    }

                    switch (task.Job1)
                    {
                        case JobLevel1.Text:
                            if (task.Job2 == WorkerTask.JobLevel2.Translate)
                            {
                                UpdateGoogleTranslateGUI(task.TranslationInfo);

                                Loader.MyGTGUI.btnTranslate.Enabled = true;
                                Loader.MyGTGUI.btnTranslateTo.Enabled = true;
                            }
                            break;
                        case JobLevel1.Image:
                            if (!task.TaskClipboardContent.Contains(ClipboardContentEnum.Local) && Engine.conf.DeleteLocal && File.Exists(task.LocalFilePath))
                            {
                                try
                                {
                                    File.Delete(task.LocalFilePath);
                                }
                                catch (Exception ex) // TODO: sometimes file is still locked... delete those files sometime
                                {
                                    StaticHelper.WriteException(ex, "Error while finalizing job");
                                }
                            }
                            break;
                    }

                    if (Engine.conf.TwitterEnabled)
                    {
                        Adapter.TwitterMsg(task);
                    }

                    bool bLastSourceButtonsEnabled = task.UploadResults.Count > 0 && !string.IsNullOrEmpty(task.UploadResults[task.UploadResults.Count - 1].Source);
                    this.btnOpenSourceText.Enabled = bLastSourceButtonsEnabled;
                    this.btnOpenSourceBrowser.Enabled = bLastSourceButtonsEnabled;
                    this.btnOpenSourceString.Enabled = bLastSourceButtonsEnabled;

                    if (task.UploadResults.Count > 0 || File.Exists(task.LocalFilePath) || task.Job2 == WorkerTask.JobLevel2.Translate)
                    {
                        if (Engine.conf.CompleteSound)
                        {
                            if (Engine.conf.EnableSounds && !string.IsNullOrEmpty(Engine.conf.SoundPath) && File.Exists(Engine.conf.SoundPath))
                                new SoundPlayer(Engine.conf.SoundPath).Play();
                            else
                                SystemSounds.Exclamation.Play();
                        }

                        if (Engine.conf.ShowBalloonTip)
                        {
                            ShowBalloonTip(task);
                        }
                    }

                    if (task.IsError)
                    {
                        foreach (string error in task.Errors)
                        {
                            StaticHelper.WriteLine(error);
                        }
                        niTray.ShowBalloonTip(5000, Application.ProductName, niTray.BalloonTipText + Environment.NewLine + task.Errors[task.Errors.Count - 1], ToolTipIcon.Error);
                    }
                }

                if (!task.IsError)
                {
                    AddHistoryItem(task);
                }

                // do this last
                UploadManager.ShowUploadResults(task, false);
            }
            catch (Exception ex)
            {
                StaticHelper.WriteException(ex, "Job Completed with errors: ");
            }
            finally
            {
                UploadManager.Commit(task.ID);

                if (TaskbarManager.IsPlatformSupported)
                {
                    Adapter.TaskbarSetProgressState(this, TaskbarProgressBarState.NoProgress);
                }
            }

            rtbDebugLog.Text = Engine.EngineLogger.ToString();

            this.niTray.Text = this.Text; // do not update notifyIcon text if there are other jobs active
            this.niTray.Icon = Resources.zss_tray;
        }

        #endregion Worker Events

        #region Create Worker

        public override WorkerTask CreateTask(WorkerTask.JobLevel2 job, TaskInfo tiCreateTask = null)
        {
            if (tiCreateTask == null)
            {
                tiCreateTask = new TaskInfo();
            }
            tiCreateTask.Job = job;
            tiCreateTask.DestConfig = ucDestOptions;

            WorkerTask tempTask = new WorkerTask(CreateWorker(), tiCreateTask);

            switch (job)
            {
                case WorkerTask.JobLevel2.Translate:
                    Loader.MyGTGUI.btnTranslate.Enabled = false;
                    tempTask.SetTranslationInfo(new GoogleTranslateInfo()
                    {
                        Text = Loader.MyGTGUI.txtTranslateText.Text,
                        SourceLanguage = Engine.MyGTConfig.GoogleSourceLanguage,
                        TargetLanguage = Engine.MyGTConfig.GoogleTargetLanguage
                    });

                    break;
            }

            if (tempTask.IsNotCanceled())
                tempTask.SetNotifyIconStatus(this.niTray, Resources.zss_busy);

            return tempTask;
        }

        #endregion Create Worker

        #region Google Translate

        public void StartWorkerTranslator()
        {
            if (Clipboard.ContainsText())
            {
                RunWorkerAsync_LanguageTranslator(new GoogleTranslateInfo()
                {
                    Text = Clipboard.GetText(),
                    SourceLanguage = Engine.MyGTConfig.GoogleSourceLanguage,
                    TargetLanguage = Engine.MyGTConfig.GoogleTargetLanguage
                });
            }
        }

        private void UpdateGoogleTranslateGUI(GoogleTranslateInfo info)
        {
            Loader.MyGTGUI.txtTranslateText.Text = info.Text;
            Loader.MyGTGUI.txtLanguages.Text = info.SourceLanguage + " -> " + info.TargetLanguage;
            Loader.MyGTGUI.txtTranslateResult.Text = info.Result;
        }

        #endregion Google Translate

        #region RunWorkerAsync Job 1

        /// <summary>
        /// Worker for Screenshots: Active Window, Crop, Entire Screen
        /// </summary>
        /// <param name="job">Job Type</param>
        public void RunWorkerAsync_Screenshots(WorkerTask ssTask)
        {
            ssTask.WasToTakeScreenshot = true;
            ssTask.RunWorker();
        }

        public void RunWorkerAsync_Text(WorkerTask task)
        {
            task.RunWorker();
        }

        #region Screenshots

        public override void CaptureActiveWindow()
        {
            WorkerTask hkawTask = CreateTask(WorkerTask.JobLevel2.CaptureActiveWindow);
#if DEBUG
            UploadManager.UploadImage(hkawTask);
#endif
            RunWorkerAsync_Screenshots(hkawTask);
        }

        public override void CaptureEntireScreen()
        {
            WorkerTask hkesTask = CreateTask(WorkerTask.JobLevel2.CaptureEntireScreen);
            RunWorkerAsync_Screenshots(hkesTask);
        }

        public override void CaptureSelectedWindow()
        {
            WorkerTask hkswTask = CreateTask(WorkerTask.JobLevel2.CaptureSelectedWindow);
            RunWorkerAsync_Screenshots(hkswTask);
        }

        public void CaptureSelectedWindowFromList(IntPtr handle)
        {
            TaskInfo tiCaptureWindowFromList = new TaskInfo() { Handle = handle };
            WorkerTask hkswTask = CreateTask(WorkerTask.JobLevel2.CaptureSelectedWindowFromList, tiCaptureWindowFromList);
            RunWorkerAsync_Screenshots(hkswTask);
        }

        public override void CaptureRectRegion()
        {
            WorkerTask hkrcTask = CreateTask(WorkerTask.JobLevel2.CaptureRectRegion);
            RunWorkerAsync_Screenshots(hkrcTask);
        }

        public override void CaptureRectRegionLast()
        {
            WorkerTask hkrclTask = CreateTask(WorkerTask.JobLevel2.CaptureLastCroppedWindow);
            RunWorkerAsync_Screenshots(hkrclTask);
        }

        public override void CaptureFreeHandRegion()
        {
            WorkerTask hkfhrTask = CreateTask(WorkerTask.JobLevel2.CaptureFreeHandRegion);
            RunWorkerAsync_Screenshots(hkfhrTask);
        }

        #endregion Screenshots

        #endregion RunWorkerAsync Job 1

        #region RunWorkerAsync Job 2

        public void RunWorkerAsync_LanguageTranslator(GoogleTranslateInfo translationInfo)
        {
            WorkerTask gtTask = CreateTask(WorkerTask.JobLevel2.Translate);
            if (Loader.MyGTGUI == null)
            {
                Loader.MyGTGUI = new GoogleTranslateGUI(Engine.MyGTConfig);
            }
            Loader.MyGTGUI.btnTranslate.Enabled = false;
            Loader.MyGTGUI.btnTranslateTo.Enabled = false;
            gtTask.SetTranslationInfo(translationInfo);
            gtTask.RunWorker();
        }

        public void UploadUsingClipboardOrGoogleTranslate()
        {
            if (Clipboard.ContainsText() && Engine.MyGTConfig.AutoTranslate && Clipboard.GetText().Length <= Engine.MyGTConfig.AutoTranslateLength)
            {
                StartWorkerTranslator();
            }
            else
            {
                UploadUsingClipboard();
            }
        }

        public void UploadUsingClipboard()
        {
            if (!Engine.IsClipboardUploading)
            {
                Engine.IsClipboardUploading = true;

                WorkerTask cbTask = CreateTask(WorkerTask.JobLevel2.UploadFromClipboard);

                if (Clipboard.ContainsImage())
                {
                    cbTask.SetImage(Clipboard.GetImage());
                    cbTask.RunWorker();
                }
                else if (Clipboard.ContainsText())
                {
                    cbTask.SetText(Clipboard.GetText());
                    if (cbTask.tempText != Engine.zPreviousSetClipboardText)
                    {
                        cbTask.RunWorker();
                        Engine.IsClipboardUploading = true;
                    }
                    else
                    {
                        Engine.IsClipboardUploading = false;
                    }
                }
                else if (Clipboard.ContainsFileDropList())
                {
                    UploadUsingFileSystem(FileSystem.GetExplorerFileList(Clipboard.GetFileDropList()).ToArray());
                }
            }
        }

        public void UploadUsingDragDrop(string fp)
        {
            WorkerTask ddTask = CreateTask(WorkerTask.JobLevel2.UploadFromExplorer);
            ddTask.UpdateLocalFilePath(fp);
            ddTask.RunWorker();
        }

        public bool UploadUsingFileSystem(params string[] fileList)
        {
            List<string> strListFilePath = new List<string>();
            bool succ = true;
            foreach (string fp in fileList)
            {
                try
                {
                    if (!string.IsNullOrEmpty(fp) && File.Exists(fp))
                    {
                        if (GraphicsMgr.IsValidImage(fp))
                        {
                            string dirfp = Path.GetDirectoryName(fp);
                            string fsfp = FileSystem.GetUniqueFilePath(Engine.Workflow, Engine.ImagesDir, Path.GetFileName(fp));
                            if (fp != fsfp && dirfp != Engine.ImagesDir)
                            {
                                string dir = Path.GetDirectoryName(fsfp);
                                if (!Directory.Exists(dir))
                                {
                                    Directory.CreateDirectory(dir);
                                }
                                File.Copy(fp, fsfp, true);
                                strListFilePath.Add(fsfp);
                            }
                            else
                            {
                                strListFilePath.Add(fp);
                            }
                            if (Path.GetDirectoryName(fp) == Engine.conf.FolderMonitorPath)
                            {
                                File.Delete(fp);
                            }
                        }
                        else
                        {
                            strListFilePath.Add(fp); // yes we use the orignal file path
                        }
                    }
                }
                catch (Exception ex)
                {
                    StaticHelper.WriteException(ex, "Error while uploading using file system");
                    succ = false;
                }
            }

            List<Image> tempImages = new List<Image>();
            bool bCreateAni = strListFilePath.Count < Engine.Workflow.ImageAnimatedFramesMax && GraphicsMgr.HasIdenticalImages(strListFilePath, out tempImages);

            if (bCreateAni)
            {
                WorkerTask agifTask = CreateTask(WorkerTask.JobLevel2.UploadFromExplorer);
                agifTask.SetImages(tempImages);
                agifTask.RunWorker();
            }
            else
            {
                foreach (string fp in strListFilePath)
                {
                    if (GraphicsMgr.IsValidImage(fp))
                    {
                        WorkerTask fpimgTask = CreateTask(WorkerTask.JobLevel2.UploadFromClipboard);
                        fpimgTask.SetImage(fp);
                        fpimgTask.RunWorker();
                    }
                    else if (FileSystem.IsValidTextFile(fp))
                    {
                        WorkerTask fptfTask = CreateTask(WorkerTask.JobLevel2.UploadFromClipboard);
                        fptfTask.SetText(File.ReadAllText(fp));
                        fptfTask.RunWorker();
                    }
                    else
                    {
                        WorkerTask fpdataTask = CreateTask(WorkerTask.JobLevel2.UploadFromExplorer);
                        fpdataTask.UpdateLocalFilePath(fp);
                        fpdataTask.RunWorker();
                    }
                }
            }
            return succ;
        }

        #endregion RunWorkerAsync Job 2

        #region Auto Capture

        public override void ShowAutoCapture()
        {
            if (!bAutoScreenshotsOpened)
            {
                bAutoScreenshotsOpened = true;
                AutoCapture autoScreenshots = new AutoCapture { Icon = Resources.zss_main };
                autoScreenshots.EventJob += new JobsEventHandler(EventJobs);
                autoScreenshots.FormClosed += new FormClosedEventHandler(autoScreenshots_FormClosed);
                autoScreenshots.Show();
                if (Engine.conf.AutoCaptureExecute) autoScreenshots.Execute();
            }
        }

        private void autoScreenshots_FormClosed(object sender, FormClosedEventArgs e)
        {
            bAutoScreenshotsOpened = false;
        }

        internal void EventJobs(object sender, WorkerTask.JobLevel2 job)
        {
            WorkerTask task = CreateTask(job);
            switch (job)
            {
                case WorkerTask.JobLevel2.CaptureEntireScreen:
                    CaptureEntireScreen();
                    break;
                case WorkerTask.JobLevel2.CaptureActiveWindow:
                    CaptureActiveWindow();
                    break;
                case WorkerTask.JobLevel2.CaptureSelectedWindow:
                    CaptureSelectedWindow();
                    break;
                case WorkerTask.JobLevel2.CaptureRectRegion:
                    CaptureRectRegion();
                    break;
                case WorkerTask.JobLevel2.CaptureLastCroppedWindow:
                    CaptureRectRegionLast();
                    break;
                case WorkerTask.JobLevel2.AutoCapture:
                    ShowAutoCapture();
                    break;
                case WorkerTask.JobLevel2.UploadFromClipboard:
                    UploadUsingClipboardOrGoogleTranslate();
                    break;
                case WorkerTask.JobLevel2.UploadFromExplorer:
                    ShowDropWindow();
                    break;
                case WorkerTask.JobLevel2.Translate:
                    StartWorkerTranslator();
                    break;
                case WorkerTask.JobLevel2.SCREEN_COLOR_PICKER:
                    ShowScreenColorPicker();
                    break;
            }
        }

        #endregion Auto Capture

        #region Drop Window

        public override void ShowDropWindow()
        {
            if (!bDropWindowOpened)
            {
                bDropWindowOpened = true;
                DropWindow dw = new DropWindow();
                dw.Result += new StringsEventHandler(dw_Result);
                dw.FormClosed += new FormClosedEventHandler(dw_FormClosed);
                dw.Show();
                Rectangle taskbar = NativeMethods.GetTaskbarRectangle();
                if (Engine.conf.LastDropBoxPosition == Point.Empty)
                {
                    dw.Location = new Point(SystemInformation.PrimaryMonitorSize.Width - dw.Width - 100,
                        SystemInformation.PrimaryMonitorSize.Height - taskbar.Height - dw.Height - 10);
                }
                else
                {
                    dw.Location = Engine.conf.LastDropBoxPosition;
                }
            }
        }

        private void dw_Result(object sender, string[] strings)
        {
            if (strings != null)
            {
                UploadUsingFileSystem(strings);
            }
        }

        private void dw_FormClosed(object sender, FormClosedEventArgs e)
        {
            bDropWindowOpened = false;
        }

        #endregion Drop Window

        #region Screen Color Picker

        public override void ShowScreenColorPicker()
        {
            DialogColor dialogColor = new DialogColor { ScreenPicker = true };
            dialogColor.Show();
        }

        #endregion Screen Color Picker

        #region Retry Methods

        public void RetryTask(WorkerTask task)
        {
            task.Errors.Clear();
            task.MyWorker = CreateWorker();
            task.PublishData();
        }

        public WorkerTask RetryUpload(WorkerTask task)
        {
            if (task.UploadResults.Count > 0 && task.Job2 != WorkerTask.JobLevel2.Translate)
            {
                if (!task.TaskClipboardContent.Contains(ClipboardContentEnum.Data) && !task.TaskClipboardContent.Contains(ClipboardContentEnum.Local) &&
                    string.IsNullOrEmpty(task.UploadResults[0].URL) && Engine.conf.ImageUploadRetryOnFail && task.States.Contains(WorkerTask.TaskState.RetryPending) && File.Exists(task.LocalFilePath))
                {
                    WorkerTask task2 = CreateTask(WorkerTask.JobLevel2.UploadImage);
                    task2.SetImage(task.LocalFilePath);
                    task2.States.Add(WorkerTask.TaskState.Finished); // we do not retry again

                    if (task.Job1 == JobLevel1.Image)
                    {
                        if (Engine.conf.ImageUploadRandomRetryOnFail)
                        {
                            List<ImageUploaderType> randomDest = new List<ImageUploaderType>() { ImageUploaderType.IMAGESHACK, ImageUploaderType.TINYPIC, ImageUploaderType.IMGUR };
                            int r = Adapter.RandomNumber(3, 3 + randomDest.Count - 1);
                            while (task.MyImageUploaders.Contains((ImageUploaderType)r))
                            {
                                r = Adapter.RandomNumber(3, 3 + randomDest.Count - 1);
                            }
                            if (!task.MyImageUploaders.Contains((ImageUploaderType)r))
                            {
                                task.MyImageUploaders.Add((ImageUploaderType)r);
                            }
                        }
                        else
                        {
                            if (!task.MyImageUploaders.Contains(ImageUploaderType.TINYPIC))
                            {
                                task.MyImageUploaders.Add((ImageUploaderType.TINYPIC));
                            }
                            else if (!task.MyImageUploaders.Contains(ImageUploaderType.IMAGESHACK))
                            {
                                task.MyImageUploaders.Add((ImageUploaderType.IMAGESHACK));
                            }
                        }
                    }

                    task2.MyWorker.RunWorkerAsync(task2);
                    return task2;
                }
            }
            task.States.Add(WorkerTask.TaskState.Finished);
            return task;
        }

        #endregion Retry Methods

        #region History

        public void AddHistoryItem(WorkerTask task)
        {
            if (Engine.conf.HistorySave)
            {
                foreach (UploadResult ur in task.UploadResults)
                {
                    HistoryManager.AddHistoryItemAsync(Engine.HistoryPath, task.GenerateHistoryItem(ur));
                }
            }

            Adapter.AddRecentItem(task.LocalFilePath);
        }

        #endregion History
    }
}