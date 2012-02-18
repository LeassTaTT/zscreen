﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using HelpersLib;
using UploadersLib;
using ZScreenLib;

namespace ZScreenTesterGUI
{
    public partial class TesterGUI : Form
    {
        public enum UploaderType
        {
            None,
            ImageUploader,
            TextUploader,
            FileUploader,
            UrlShortener
        }

        public class UploaderInfo
        {
            public UploaderType UploaderType;
            public ImageDestination ImageUploader;
            public TextDestination TextUploader;
            public FileDestination FileUploader;
            public UrlShortenerType UrlShortener;
            public WorkerTask Task;
            public int Index;
            public Stopwatch Timer;
        }

        public bool Testing
        {
            get { return isTesting; }
            set
            {
                isTesting = value;
                btnTestAll.Enabled = !value;
                btnTestSelected.Enabled = !value;
                testSelectedUploadersToolStripMenuItem.Enabled = !value;
            }
        }

        public string TestImageFilePath { get; set; }

        public string TestFilePath { get; set; }

        public string TestText { get; set; }

        public string TestURL { get; set; }

        private bool isTesting = false;

        public TesterGUI()
        {
            InitializeComponent();

            ListViewItem lvi;

            ListViewGroup imageUploadersGroup = new ListViewGroup("Image Uploaders", HorizontalAlignment.Left);
            ListViewGroup textUploadersGroup = new ListViewGroup("Text Uploaders", HorizontalAlignment.Left);
            ListViewGroup fileUploadersGroup = new ListViewGroup("File Uploaders", HorizontalAlignment.Left);
            ListViewGroup urlShortenersGroup = new ListViewGroup("URL Shorteners", HorizontalAlignment.Left);
            lvUploaders.Groups.AddRange(new[] { imageUploadersGroup, textUploadersGroup, fileUploadersGroup, urlShortenersGroup });

            foreach (ImageDestination uploader in Enum.GetValues(typeof(ImageDestination)))
            {
                switch (uploader)
                {
                    case ImageDestination.MediaWiki:
                    case ImageDestination.Twitsnaps: // Not possible to upload without post Twitter
                    case ImageDestination.FileUploader: // We are going to test this in File Uploader tests
                        continue;
                }

                lvi = new ListViewItem(uploader.GetDescription());
                lvi.Tag = new UploaderInfo { UploaderType = UploaderType.ImageUploader, ImageUploader = uploader };
                lvi.Group = imageUploadersGroup;
                lvUploaders.Items.Add(lvi);
            }

            foreach (TextDestination uploader in Enum.GetValues(typeof(TextDestination)))
            {
                switch (uploader)
                {
                    case TextDestination.FileUploader:
                        continue;
                }

                lvi = new ListViewItem(uploader.GetDescription());
                lvi.Tag = new UploaderInfo { UploaderType = UploaderType.TextUploader, TextUploader = uploader };
                lvi.Group = textUploadersGroup;
                lvUploaders.Items.Add(lvi);
            }

            foreach (FileDestination uploader in Enum.GetValues(typeof(FileDestination)))
            {
                switch (uploader)
                {
                    case FileDestination.CustomUploader:
                        continue;
                }

                lvi = new ListViewItem(uploader.GetDescription());
                lvi.Tag = new UploaderInfo { UploaderType = UploaderType.FileUploader, FileUploader = uploader };
                lvi.Group = fileUploadersGroup;
                lvUploaders.Items.Add(lvi);
            }

            foreach (UrlShortenerType uploader in Enum.GetValues(typeof(UrlShortenerType)))
            {
                lvi = new ListViewItem(uploader.GetDescription());
                lvi.Tag = new UploaderInfo { UploaderType = UploaderType.UrlShortener, UrlShortener = uploader };
                lvi.Group = urlShortenersGroup;
                lvUploaders.Items.Add(lvi);
            }

            PrepareListItems();
        }

        private void TesterGUI_Load(object sender, EventArgs e)
        {
            CheckPaths();
        }

        private void CheckPaths()
        {
            if (string.IsNullOrEmpty(TestImageFilePath) || !File.Exists(TestImageFilePath) ||
                string.IsNullOrEmpty(TestFilePath) || !File.Exists(TestFilePath))
            {
                using (OpenFileDialog dlg = new OpenFileDialog())
                {
                    dlg.Filter = "Image Files (*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
                    dlg.Title = "Browse for a test image file. It will be used for Image/File upload tests.";

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        TestFilePath = TestImageFilePath = dlg.FileName;
                    }
                    else
                    {
                        Close();
                    }
                }
            }

            if (string.IsNullOrEmpty(TestText))
            {
                TestText = "ZScreen test.";
            }

            if (string.IsNullOrEmpty(TestURL))
            {
                TestURL = "http://code.google.com/p/zscreen/";
            }
        }

        private void PrepareListItems()
        {
            for (int i = 0; i < lvUploaders.Items.Count; i++)
            {
                ListViewItem lvi = lvUploaders.Items[i];

                while (lvi.SubItems.Count < 3)
                {
                    lvi.SubItems.Add(string.Empty);
                }

                lvi.SubItems[1].Text = "Waiting";
                lvi.BackColor = Color.LightYellow;

                UploaderInfo uploadInfo = lvi.Tag as UploaderInfo;
                if (uploadInfo != null)
                {
                    uploadInfo.Index = i;
                }
            }
        }

        private void ConsoleWriteLine(string value)
        {
            if (!this.IsDisposed)
            {
                this.Invoke(new MethodInvoker(delegate
                    {
                        txtConsole.AppendText(value + "\r\n");
                    }));
            }
        }

        public void StartTest(UploaderInfo[] uploaders)
        {
            Testing = true;

            BackgroundWorker bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += (x, x2) => Testing = false;
            bw.RunWorkerAsync(uploaders);
        }

        public enum UploadStatus
        {
            Uploading,
            Uploaded
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = (BackgroundWorker)sender;
            UploaderInfo[] uploaders = (UploaderInfo[])e.Argument;

            foreach (UploaderInfo uploader in uploaders)
            {
                if (this.IsDisposed || !isTesting || uploader == null)
                {
                    break;
                }

                TaskInfo tiTester = new TaskInfo() { Job = WorkerTask.JobLevel2.UploadFromClipboard };
                WorkerTask task = new WorkerTask(new BackgroundWorker() { WorkerReportsProgress = true }, tiTester);

                uploader.Timer = new Stopwatch();
                uploader.Timer.Start();

                bw.ReportProgress((int)UploadStatus.Uploading, uploader);

                try
                {
                    switch (uploader.UploaderType)
                    {
                        case UploaderType.ImageUploader:
                            if (!task.WorkflowConfig.DestConfig.TaskClipboardContent.Contains(ClipboardContentEnum.Data))
                            {
                                task.WorkflowConfig.DestConfig.ImageUploaders.Add(uploader.ImageUploader);
                                task.UpdateLocalFilePath(TestImageFilePath);
                                task.UploadImage();
                            }
                            break;
                        case UploaderType.FileUploader:
                            task.WorkflowConfig.DestConfig.FileUploaders.Add(uploader.FileUploader);
                            task.UpdateLocalFilePath(TestFilePath);
                            task.UploadFile();
                            break;
                        case UploaderType.TextUploader:
                            task.WorkflowConfig.DestConfig.TextUploaders.Add(uploader.TextUploader);
                            task.SetText(TestText);
                            task.UploadText();
                            break;
                        case UploaderType.UrlShortener:
                            task.WorkflowConfig.DestConfig.LinkUploaders.Add(uploader.UrlShortener);
                            task.ShortenURL(TestURL);
                            break;
                        default:
                            throw new Exception("Unknown uploader.");
                    }
                }
                catch (Exception ex)
                {
                    ConsoleWriteLine(ex.ToString());
                }

                uploader.Timer.Stop();
                uploader.Task = task;

                bw.ReportProgress((int)UploadStatus.Uploaded, uploader);
            }
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!this.IsDisposed)
            {
                UploaderInfo uploader = e.UserState as UploaderInfo;

                if (uploader != null && uploader.Task.UploadResults.Count > 0)
                {
                    lvUploaders.Items[uploader.Index].Tag = uploader;

                    switch ((UploadStatus)e.ProgressPercentage)
                    {
                        case UploadStatus.Uploading:
                            lvUploaders.Items[uploader.Index].BackColor = Color.Gold;
                            lvUploaders.Items[uploader.Index].SubItems[1].Text = "Uploading...";
                            lvUploaders.Items[uploader.Index].SubItems[2].Text = string.Empty;
                            break;
                        case UploadStatus.Uploaded:
                            if (uploader.Task != null && !string.IsNullOrEmpty(uploader.Task.UploadResults[0].URL))
                            {
                                lvUploaders.Items[uploader.Index].BackColor = Color.LightGreen;
                                lvUploaders.Items[uploader.Index].SubItems[1].Text = "Success: " + uploader.Task.UploadResults[0].URL;
                            }
                            else
                            {
                                lvUploaders.Items[uploader.Index].BackColor = Color.LightCoral;
                                lvUploaders.Items[uploader.Index].SubItems[1].Text = "Failed: " + uploader.Task.ToErrorString();
                            }

                            lvUploaders.Items[uploader.Index].SubItems[2].Text = uploader.Timer.ElapsedMilliseconds + " ms";

                            break;
                    }
                }
            }
        }

        private void openURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvUploaders.SelectedItems.Count > 0)
            {
                UploaderInfo uploader = lvUploaders.SelectedItems[0].Tag as UploaderInfo;

                if (uploader != null && uploader.Task != null && uploader.Task.UploadResults.Count > 0 && !string.IsNullOrEmpty(uploader.Task.UploadResults[0].URL))
                {
                    ThreadPool.QueueUserWorkItem(x => Process.Start(uploader.Task.UploadResults[0].URL));
                }
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvUploaders.SelectedItems.Count > 0)
            {
                List<string> urls = new List<string>();
                UploaderInfo uploader;

                foreach (ListViewItem lvi in lvUploaders.SelectedItems)
                {
                    uploader = lvi.Tag as UploaderInfo;
                    if (uploader != null && uploader.Task != null && !string.IsNullOrEmpty(uploader.Task.UploadResults[0].URL))
                    {
                        urls.Add(string.Format("{0}: {1}", lvi.Text, uploader.Task.UploadResults[0].URL));
                    }
                }

                if (urls.Count > 0)
                {
                    Clipboard.SetText(string.Join("\r\n", urls.ToArray()));
                }
            }
        }

        private void TesterGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            isTesting = false;
        }

        private void btnTestAll_Click(object sender, EventArgs e)
        {
            UploaderInfo[] uploaders = lvUploaders.Items.Cast<ListViewItem>().Select(x => x.Tag as UploaderInfo).ToArray();
            StartTest(uploaders);
        }

        private void btnTestSelected_Click(object sender, EventArgs e)
        {
            UploaderInfo[] uploaders = lvUploaders.SelectedItems.Cast<ListViewItem>().Select(x => x.Tag as UploaderInfo).ToArray();
            StartTest(uploaders);
        }
    }
}