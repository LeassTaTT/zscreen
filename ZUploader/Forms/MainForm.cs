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
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using HelpersLib;
using HelpersLib.Hotkey;
using HistoryLib;
using UploadersAPILib;
using UploadersLib;
using UploadersLib.HelperClasses;
using ZUploader.Properties;

namespace ZUploader
{
    public partial class MainForm : HotkeyForm
    {
        public bool IsReady { get; private set; }

        public HotkeyManager HotkeyManager { get; private set; }

        private bool trayClose;

        public MainForm()
        {
            InitControls();
            UpdateControls();
        }

        private void AfterLoadJobs()
        {
            LoadSettings();

            if (!Program.IsSilentRun)
            {
                SplashForm.CloseSplash();
            }

            InitHotkeys();
            UseCommandLineArgs(Environment.GetCommandLineArgs());

            IsReady = true;

            Program.MyLogger.WriteLine("Startup time: {0}ms", Program.StartTimer.ElapsedMilliseconds);
        }

        private void InitControls()
        {
            InitializeComponent();

            this.Text = Program.Title;
            this.Icon = Resources.ZUploader;
            niTray.Icon = Resources.ZUploaderSmallIcon;

            foreach (string imageUploader in ZAppHelper.GetEnumDescriptions<ImageDestination>())
            {
                tsmiImageUploaders.DropDownItems.Add(new ToolStripMenuItem(imageUploader));
            }
            tsmiImageUploaders.DropDownItemClicked += new ToolStripItemClickedEventHandler(tsddbImageUploaders_DropDownItemClicked);

            foreach (string fileUploader in ZAppHelper.GetEnumDescriptions<FileDestination>())
            {
                tsmiFileUploaders.DropDownItems.Add(new ToolStripMenuItem(fileUploader));
            }
            tsmiFileUploaders.DropDownItemClicked += new ToolStripItemClickedEventHandler(tsddbFileUploaders_DropDownItemClicked);

            foreach (string textUploader in ZAppHelper.GetEnumDescriptions<TextDestination>())
            {
                tsmiTextUploaders.DropDownItems.Add(new ToolStripMenuItem(textUploader));
            }
            tsmiTextUploaders.DropDownItemClicked += new ToolStripItemClickedEventHandler(tsddbTextUploaders_DropDownItemClicked);

            foreach (string urlShortener in ZAppHelper.GetEnumDescriptions<UrlShortenerType>())
            {
                tsmiURLShorteners.DropDownItems.Add(new ToolStripMenuItem(urlShortener));
            }
            tsmiURLShorteners.DropDownItemClicked += new ToolStripItemClickedEventHandler(tsddbURLShorteners_DropDownItemClicked);

            ImageList il = new ImageList();
            il.ColorDepth = ColorDepth.Depth32Bit;
            il.Images.Add(Properties.Resources.navigation_090_button);
            il.Images.Add(Properties.Resources.cross_button);
            il.Images.Add(Properties.Resources.tick_button);
            il.Images.Add(Properties.Resources.navigation_000_button);
            lvUploads.SmallImageList = il;
            lvUploads.FillLastColumn();

            UploadManager.ListViewControl = lvUploads;

#if DEBUG
            // Test upload button
            tsbDebug.Visible = true;
#endif
        }

        private void LoadSettings()
        {
            niTray.Visible = Program.Settings.ShowTray;

            if (Program.IsSilentRun && Program.Settings.ShowTray)
            {
                Hide();
            }

            if (ZAppHelper.GetEnumLength<ImageDestination>() <= Program.Settings.SelectedImageUploaderDestination)
            {
                Program.Settings.SelectedImageUploaderDestination = 0;
            }

            ((ToolStripMenuItem)tsmiImageUploaders.DropDownItems[Program.Settings.SelectedImageUploaderDestination]).Checked = true;
            UploadManager.ImageUploader = (ImageDestination)Program.Settings.SelectedImageUploaderDestination;

            if (ZAppHelper.GetEnumLength<FileDestination>() <= Program.Settings.SelectedFileUploaderDestination)
            {
                Program.Settings.SelectedFileUploaderDestination = 0;
            }

            ((ToolStripMenuItem)tsmiFileUploaders.DropDownItems[Program.Settings.SelectedFileUploaderDestination]).Checked = true;
            UploadManager.FileUploader = (FileDestination)Program.Settings.SelectedFileUploaderDestination;

            if (ZAppHelper.GetEnumLength<TextDestination>() <= Program.Settings.SelectedTextUploaderDestination)
            {
                Program.Settings.SelectedTextUploaderDestination = 0;
            }

            ((ToolStripMenuItem)tsmiTextUploaders.DropDownItems[Program.Settings.SelectedTextUploaderDestination]).Checked = true;
            UploadManager.TextUploader = (TextDestination)Program.Settings.SelectedTextUploaderDestination;

            if (ZAppHelper.GetEnumLength<UrlShortenerType>() <= Program.Settings.SelectedURLShortenerDestination)
            {
                Program.Settings.SelectedURLShortenerDestination = 0;
            }

            ((ToolStripMenuItem)tsmiURLShorteners.DropDownItems[Program.Settings.SelectedURLShortenerDestination]).Checked = true;
            UploadManager.URLShortener = (UrlShortenerType)Program.Settings.SelectedURLShortenerDestination;

            UpdateUploaderMenuNames();
            UpdateCaptureMenu();
        }

        private void UpdateControls()
        {
            tsbCopy.Enabled = tsbOpen.Enabled = copyURLToolStripMenuItem.Visible = openURLToolStripMenuItem.Visible =
                copyShortenedURLToolStripMenuItem.Visible = copyThumbnailURLToolStripMenuItem.Visible = copyDeletionURLToolStripMenuItem.Visible =
                showErrorsToolStripMenuItem.Visible = copyErrorsToolStripMenuItem.Visible = showResponseToolStripMenuItem.Visible =
                uploadFileToolStripMenuItem.Visible = stopUploadToolStripMenuItem.Visible = false;

            int itemsCount = lvUploads.SelectedItems.Count;

            if (itemsCount > 0)
            {
                UploadResult result = lvUploads.SelectedItems[0].Tag as UploadResult;

                if (result != null)
                {
                    if (!string.IsNullOrEmpty(result.URL))
                    {
                        tsbCopy.Enabled = tsbOpen.Enabled = copyURLToolStripMenuItem.Visible = openURLToolStripMenuItem.Visible = true;

                        if (itemsCount > 1)
                        {
                            copyURLToolStripMenuItem.Text = string.Format("Copy URLs ({0})", itemsCount);
                        }
                        else
                        {
                            copyURLToolStripMenuItem.Text = "Copy URL";
                        }
                    }

                    if (!string.IsNullOrEmpty(result.ThumbnailURL))
                    {
                        copyThumbnailURLToolStripMenuItem.Visible = true;
                    }

                    if (!string.IsNullOrEmpty(result.DeletionURL))
                    {
                        copyDeletionURLToolStripMenuItem.Visible = true;
                    }

                    if (!string.IsNullOrEmpty(result.ShortenedURL))
                    {
                        copyShortenedURLToolStripMenuItem.Visible = true;
                    }

                    if (result.IsError)
                    {
                        showErrorsToolStripMenuItem.Visible = true;
                        copyErrorsToolStripMenuItem.Visible = true;
                    }

                    if (!string.IsNullOrEmpty(result.Source))
                    {
                        showResponseToolStripMenuItem.Visible = true;
                    }
                }

                int index = lvUploads.SelectedIndices[0];
                stopUploadToolStripMenuItem.Visible = UploadManager.Tasks[index].Status != TaskStatus.Completed;
            }
            else
            {
                uploadFileToolStripMenuItem.Visible = true;
            }
        }

        private void UpdateUploaderMenuNames()
        {
            tsmiImageUploaders.Text = "Image uploader: " + UploadManager.ImageUploader.GetDescription();
            tsmiFileUploaders.Text = "File uploader: " + UploadManager.FileUploader.GetDescription();
            tsmiTextUploaders.Text = "Text uploader: " + UploadManager.TextUploader.GetDescription();
            tsmiURLShorteners.Text = "URL shortener: " + UploadManager.URLShortener.GetDescription();
        }

        private void UpdateCaptureMenu()
        {
            CaptureDestination = Program.Settings.CaptureOutput;
            bool isCaptureUpload = CaptureDestination == ScreenshotDestination.Upload;
            tsmiCaptureOutputUpload.Checked = isCaptureUpload;
            tsmiCaptureOutputClipboard.Checked = !isCaptureUpload;
            tsmiCaptureOutput.Text = "Capture output: " + CaptureDestination.ToString();
        }

        public void UseCommandLineArgs(string[] args)
        {
            if (args != null && args.Length > 1)
            {
                for (int i = 1; i < args.Length; i++)
                {
                    if (args[i].Equals("-clipboardupload", StringComparison.InvariantCultureIgnoreCase))
                    {
                        UploadManager.ClipboardUpload();
                    }
                    else if (args[i][0] != '-')
                    {
                        UploadManager.UploadFile(args[i]);
                    }
                }
            }
        }

        private UploadResult GetCurrentUploadResult()
        {
            UploadResult result = null;

            if (lvUploads.SelectedItems.Count > 0)
            {
                result = lvUploads.SelectedItems[0].Tag as UploadResult;
            }

            return result;
        }

        private void OpenURL()
        {
            UploadResult result = GetCurrentUploadResult();

            if (result != null && !string.IsNullOrEmpty(result.URL))
            {
                StaticHelper.LoadBrowser(result.URL);
            }
        }

        private void CopyURL()
        {
            if (lvUploads.SelectedItems.Count > 0)
            {
                string[] array = lvUploads.SelectedItems.Cast<ListViewItem>().Select(x => x.Tag as UploadResult).
                    Where(x => x != null && !string.IsNullOrEmpty(x.URL)).Select(x => x.URL).ToArray();

                if (array != null && array.Length > 0)
                {
                    string urls = string.Join("\r\n", array);

                    if (!string.IsNullOrEmpty(urls))
                    {
                        ZAppHelper.CopyTextSafely(urls);
                    }
                }
            }
        }

        private void CopyShortenedURL()
        {
            UploadResult result = GetCurrentUploadResult();

            if (result != null && !string.IsNullOrEmpty(result.ShortenedURL))
            {
                ZAppHelper.CopyTextSafely(result.ShortenedURL);
            }
        }

        private void CopyThumbnailURL()
        {
            UploadResult result = GetCurrentUploadResult();

            if (result != null && !string.IsNullOrEmpty(result.ThumbnailURL))
            {
                ZAppHelper.CopyTextSafely(result.ThumbnailURL);
            }
        }

        private void CopyDeletionURL()
        {
            UploadResult result = GetCurrentUploadResult();

            if (result != null && !string.IsNullOrEmpty(result.DeletionURL))
            {
                ZAppHelper.CopyTextSafely(result.DeletionURL);
            }
        }

        private string GetErrors()
        {
            string errors = string.Empty;
            UploadResult result = GetCurrentUploadResult();

            if (result != null && result.IsError)
            {
                errors = string.Join("\r\n\r\n", result.Errors.ToArray());
            }

            return errors;
        }

        private void ShowErrors()
        {
            string errors = GetErrors();

            if (!string.IsNullOrEmpty(errors))
            {
                Exception e = new Exception("Upload errors: " + errors);
                new ErrorForm(Application.ProductName, e, Program.MyLogger, Program.LogFilePath, ZLinks.URL_ISSUES).ShowDialog();
            }
        }

        private void CopyErrors()
        {
            string errors = GetErrors();

            if (!string.IsNullOrEmpty(errors))
            {
                ZAppHelper.CopyTextSafely(errors);
            }
        }

        private void ShowResponse()
        {
            UploadResult result = GetCurrentUploadResult();

            if (result != null && !string.IsNullOrEmpty(result.Source))
            {
                ResponseForm form = new ResponseForm(result.Source);
                form.Icon = this.Icon;
                form.Show();
            }
        }

        #region Form events

        protected override void SetVisibleCore(bool value)
        {
            if (value && !this.IsHandleCreated)
            {
                if (Program.IsSilentRun && Program.Settings.ShowTray)
                {
                    value = false;
                    CreateHandle();
                }

                AfterLoadJobs();
            }

            base.SetVisibleCore(value);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            Activate();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && Program.Settings.ShowTray && !trayClose)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) ||
                e.Data.GetDataPresent(DataFormats.Bitmap, false) ||
                e.Data.GetDataPresent(DataFormats.Text, false))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            UploadManager.DragDropUpload(e.Data);
        }

        private void tsbClipboardUpload_Click(object sender, EventArgs e)
        {
            UploadManager.ClipboardUploadWithContentViewer();
        }

        private void tsbFileUpload_Click(object sender, EventArgs e)
        {
            UploadManager.UploadFile();
        }

        private void tsbDebug_Click(object sender, EventArgs e)
        {
            UploadManager.UploadImage(Resources.ZUploaderLogo);
        }

        private void tsbCopy_Click(object sender, EventArgs e)
        {
            CopyURL();
        }

        private void tsbOpen_Click(object sender, EventArgs e)
        {
            OpenURL();
        }

        private void tsbHistory_Click(object sender, EventArgs e)
        {
            new HistoryForm(Program.HistoryFilePath, Program.Settings.HistoryMaxItemCount, "ZUploader - History").Show();
        }

        private void tsbSettings_Click(object sender, EventArgs e)
        {
            SettingsForm form = new SettingsForm();
            form.Icon = this.Icon;
            form.Show();
        }

        private void tsbAbout_Click(object sender, EventArgs e)
        {
            AboutForm form = new AboutForm();
            form.Icon = this.Icon;
            form.Show();
        }

        private void tsbDonate_Click(object sender, EventArgs e)
        {
            StaticHelper.LoadBrowser(ZLinks.URL_DONATE_ZU);
        }

        private void lvUploads_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        private void lvUploads_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                UpdateControls();
                cmsUploads.Show(lvUploads, e.X + 1, e.Y + 1);
            }
        }

        private void openURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenURL();
        }

        private void copyURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyURL();
        }

        private void copyShortenedURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyShortenedURL();
        }

        private void copyThumbnailURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyThumbnailURL();
        }

        private void copyDeletionURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyDeletionURL();
        }

        private void showErrorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowErrors();
        }

        private void copyErrorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyErrors();
        }

        private void showResponseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowResponse();
        }

        private void uploadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UploadManager.UploadFile();
        }

        private void stopUploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvUploads.SelectedIndices.Count > 0)
            {
                foreach (int index in lvUploads.SelectedIndices)
                {
                    UploadManager.Tasks[index].Stop();
                }
            }
        }

        private void lvUploads_DoubleClick(object sender, EventArgs e)
        {
            OpenURL();
        }

        private void tsddbImageUploaders_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            for (int i = 0; i < tsmiImageUploaders.DropDownItems.Count; i++)
            {
                ToolStripMenuItem tsmi = (ToolStripMenuItem)tsmiImageUploaders.DropDownItems[i];
                if (tsmi.Checked = tsmi == e.ClickedItem)
                {
                    Program.Settings.SelectedImageUploaderDestination = i;
                    UploadManager.ImageUploader = (ImageDestination)i;
                }
            }

            UpdateUploaderMenuNames();
        }

        private void tsddbFileUploaders_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            for (int i = 0; i < tsmiFileUploaders.DropDownItems.Count; i++)
            {
                ToolStripMenuItem tsmi = (ToolStripMenuItem)tsmiFileUploaders.DropDownItems[i];
                if (tsmi.Checked = tsmi == e.ClickedItem)
                {
                    Program.Settings.SelectedFileUploaderDestination = i;
                    UploadManager.FileUploader = (FileDestination)i;
                }
            }

            UpdateUploaderMenuNames();
        }

        private void tsddbTextUploaders_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            for (int i = 0; i < tsmiTextUploaders.DropDownItems.Count; i++)
            {
                ToolStripMenuItem tsmi = (ToolStripMenuItem)tsmiTextUploaders.DropDownItems[i];
                if (tsmi.Checked = tsmi == e.ClickedItem)
                {
                    Program.Settings.SelectedTextUploaderDestination = i;
                    UploadManager.TextUploader = (TextDestination)i;
                }
            }

            UpdateUploaderMenuNames();
        }

        private void tsddbURLShorteners_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            for (int i = 0; i < tsmiURLShorteners.DropDownItems.Count; i++)
            {
                ToolStripMenuItem tsmi = (ToolStripMenuItem)tsmiURLShorteners.DropDownItems[i];
                if (tsmi.Checked = tsmi == e.ClickedItem)
                {
                    Program.Settings.SelectedURLShortenerDestination = i;
                    UploadManager.URLShortener = (UrlShortenerType)i;
                }
            }

            UpdateUploaderMenuNames();
        }

        private void tsmiCaptureOutputUpload_Click(object sender, EventArgs e)
        {
            Program.Settings.CaptureOutput = ScreenshotDestination.Upload;
            UpdateCaptureMenu();
        }

        private void tsmiCaptureOutputClipboard_Click(object sender, EventArgs e)
        {
            Program.Settings.CaptureOutput = ScreenshotDestination.Clipboard;
            UpdateCaptureMenu();
        }

        private void tsddbUploadersConfig_Click(object sender, EventArgs e)
        {
            new UploadersConfigForm(Program.UploadersConfig, ZKeys.GetAPIKeys()) { Icon = this.Icon }.ShowDialog();
        }

        #region Tray events

        private void niTray_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!Visible)
            {
                Show();
            }

            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
            }

            BringToFront();
            Activate();
        }

        private void niTray_BalloonTipClicked(object sender, EventArgs e)
        {
            string url = niTray.Tag as string;

            if (!string.IsNullOrEmpty(url))
            {
                StaticHelper.LoadBrowser(url);
            }
        }

        private void tsmiTrayClipboardUpload_Click(object sender, EventArgs e)
        {
            UploadManager.ClipboardUploadWithContentViewer();
        }

        private void tsmiTrayFileUpload_Click(object sender, EventArgs e)
        {
            UploadManager.UploadFile();
        }

        private void tsmiTrayExit_Click(object sender, EventArgs e)
        {
            trayClose = true;
            Close();
        }

        #endregion Tray events

        #endregion Form events
    }
}