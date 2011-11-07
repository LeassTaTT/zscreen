#region License Information (GPL v2)

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
using System.Diagnostics;
using System.Windows.Forms;
using HelpersLib;
using ZScreenLib;

namespace ZScreenGUI
{
    public partial class AutoCapture : Form
    {
        public bool IsRunning;

        private Timer timer = new Timer();
        private Timer statusTimer = new Timer { Interval = 250 };
        private WorkerTask.JobLevel2 mJob;
        private int mDelay;
        private bool waitUploads;
        private int count;
        private int timeleft;
        private int percentage;
        private Stopwatch stopwatch = new Stopwatch();

        public event JobsEventHandler EventJob;

        public AutoCapture()
        {
            InitializeComponent();
            timer.Tick += TimerTick;
            statusTimer.Tick += StatusTimerTick;
            LoadSettings();
        }

        private void LoadSettings()
        {
            cbScreenshotTypes.Items.AddRange(typeof(AutoScreenshotterJobs).GetEnumDescriptions());
            cbScreenshotTypes.SelectedIndex = (int)Engine.ConfigUI.AutoCaptureScreenshotTypes;
            nudDelay.Time = Engine.ConfigUI.AutoCaptureDelayTimes;
            nudDelay.Value = Engine.ConfigUI.AutoCaptureDelayTime;
            cbAutoMinimize.Checked = Engine.ConfigUI.AutoCaptureAutoMinimize;
            cbWaitUploads.Checked = Engine.ConfigUI.AutoCaptureWaitUploads;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (waitUploads && UploadManager.UploadInfoList.Count > 0)
            {
                timer.Interval = 1000;
            }
            else
            {
                StartTimer();
                timer.Interval = mDelay;
                EventJob(this, mJob);
                count++;
            }
        }

        private void StatusTimerTick(object sender, EventArgs e)
        {
            UpdateStatus();
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            Execute();
        }

        public void Execute()
        {
            if (IsRunning)
            {
                IsRunning = false;
                tspbBar.Value = 0;
                btnExecute.Text = "Start";
            }
            else
            {
                IsRunning = true;
                btnExecute.Text = "Stop";
                switch (Engine.ConfigUI.AutoCaptureScreenshotTypes)
                {
                    case AutoScreenshotterJobs.TAKE_SCREENSHOT_SCREEN:
                        mJob = WorkerTask.JobLevel2.CaptureEntireScreen;
                        break;
                    case AutoScreenshotterJobs.TAKE_SCREENSHOT_WINDOW_ACTIVE:
                        mJob = WorkerTask.JobLevel2.CaptureActiveWindow;
                        break;
                    case AutoScreenshotterJobs.TAKE_SCREENSHOT_LAST_CROPPED:
                        mJob = WorkerTask.JobLevel2.CaptureLastCroppedWindow;
                        break;
                }

                timer.Interval = 1000;
                mDelay = (int)Engine.ConfigUI.AutoCaptureDelayTime;
                waitUploads = Engine.ConfigUI.AutoCaptureWaitUploads;
                if (Engine.ConfigUI.AutoCaptureAutoMinimize)
                {
                    this.WindowState = FormWindowState.Minimized;
                }
            }

            timer.Enabled = IsRunning;
            statusTimer.Enabled = IsRunning;
        }

        private void StartTimer()
        {
            stopwatch.Reset();
            stopwatch.Start();
        }

        private void UpdateStatus()
        {
            timeleft = Math.Max(0, mDelay - (int)stopwatch.ElapsedMilliseconds);
            percentage = (int)(100 - (double)timeleft / mDelay * 100);
            tspbBar.Value = percentage;
            tsslStatus.Text = " Timeleft: " + timeleft + "ms (" + percentage + "%)";
            this.Text = "Auto Capture - Count: " + count;
        }

        private void cbScreenshotTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.AutoCaptureScreenshotTypes = (AutoScreenshotterJobs)cbScreenshotTypes.SelectedIndex;
        }

        private void cbAutoMinimize_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.AutoCaptureAutoMinimize = cbAutoMinimize.Checked;
        }

        private void cbWaitUploads_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.AutoCaptureWaitUploads = cbWaitUploads.Checked;
        }

        private void nudDelay_ValueChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.AutoCaptureDelayTime = nudDelay.Value;
        }

        private void nudDelay_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.AutoCaptureDelayTimes = nudDelay.Time;
        }
    }
}