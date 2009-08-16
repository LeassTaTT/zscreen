﻿using System;
using System.ComponentModel;
using System.Windows.Forms;
using ZScreenLib;

namespace ZScreenCLI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ZScreenLib.Program.Load();

            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                this.niTray.Icon = ResxMgr.BusyIcon;
                try
                {
                    if (args[1].ToLower() == "crop_shot")
                    {
                        // Crop Shot
                        CropShot(WorkerTask.Jobs.TAKE_SCREENSHOT_CROPPED);
                    }
                    else if (args[1].ToLower() == "selected_window")
                    {
                        // Selected Window
                        CropShot(WorkerTask.Jobs.TAKE_SCREENSHOT_WINDOW_SELECTED);
                    }
                    else if (args[1].ToLower() == "clipboard_upload")
                    {
                        // Clipboard Upload
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                }
                this.niTray.Icon = ResxMgr.ReadyIcon;
            }
            else
            {
                this.Close();
            }
        }

        private void CropShot(WorkerTask.Jobs job)
        {
            Worker worker = new Worker();
            // TODO: replace temp by null. Get Worker to check for null before using BackgroundWorker
            BackgroundWorker temp = new BackgroundWorker();
            temp.WorkerReportsProgress = true;
            WorkerTask task = new WorkerTask(temp, job);
            task.MyImageUploader = ZScreenLib.Program.conf.ScreenshotDestMode;
            worker.CaptureRegionOrWindow(ref task);
            new BalloonTipHelper(this.niTray, task).ShowBalloonTip();
            UploadManager.SetClipboardText(task);
        }

        private void ClipboardUpload()
        {

        }

        private void niTray_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void niTray_BalloonTipClosed(object sender, EventArgs e)
        {
            this.Close();
        }

        private void niTray_BalloonTipClicked(object sender, EventArgs e)
        {
            if (ZScreenLib.Program.conf.BalloonTipOpenLink)
            {
                NotifyIcon ni = (NotifyIcon)sender;
                new BalloonTipHelper(ni).ClickBalloonTip();
            }
            this.Close();
        }
    }
}