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

using System.Threading;
using System.Windows.Forms;
using HelpersLib;

namespace ZSS.UpdateCheckerLib
{
    public partial class UpdateCheckerLabel : UserControl
    {
        private UpdateChecker updateChecker;
        private bool isBusy;

        public UpdateCheckerLabel()
        {
            InitializeComponent();
        }

        public void CheckUpdate(UpdateChecker updateChecker)
        {
            this.updateChecker = updateChecker;

            if (!isBusy)
            {
                isBusy = true;

                lblStatus.Visible = false;
                llblUpdateAvailable.Visible = false;

                pbLoading.Visible = true;
                lblCheckingUpdates.Visible = true;

                new Thread(CheckingUpdate).Start();
            }
        }

        private void CheckingUpdate()
        {
            updateChecker.CheckUpdate();
            UpdateControls();
            isBusy = false;
        }

        private void UpdateControls()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(UpdateControls));
            }
            else
            {
                pbLoading.Visible = false;
                lblCheckingUpdates.Visible = false;

                switch (updateChecker.UpdateInfo.Status)
                {
                    case UpdateStatus.UpdateCheckFailed:
                        lblStatus.Text = "Update check failed";
                        lblStatus.Visible = true;
                        break;
                    case UpdateStatus.UpdateRequired:
                        llblUpdateAvailable.Text = "Newest version of " + updateChecker.ApplicationName + " available";
                        llblUpdateAvailable.Visible = true;
                        break;
                    case UpdateStatus.UpToDate:
                        lblStatus.Text = updateChecker.ApplicationName + " is up to date";
                        lblStatus.Visible = true;
                        break;
                }
            }
        }

        private void llblUpdateAvailable_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (updateChecker != null && updateChecker.UpdateInfo != null && !string.IsNullOrEmpty(updateChecker.UpdateInfo.URL))
            {
                DownloaderForm downloader = new DownloaderForm(updateChecker.UpdateInfo.URL, updateChecker.UpdateInfo.Summary);
                downloader.ShowDialog();
                if (downloader.Status == DownloaderFormStatus.InstallStarted) Application.Exit();
            }
        }
    }
}