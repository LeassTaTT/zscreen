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
using System.IO;
using System.Windows.Forms;

namespace HelpersLib
{
    public partial class ErrorForm : Form
    {
        public string ApplicationName { get; private set; }
        public Exception Error { get; private set; }
        public Logger Logger { get; private set; }
        public string LogPath { get; private set; }
        public string BugReportPath { get; private set; }

        public ErrorForm(string productName, Exception error, Logger logger, string logPath, string bugReportPath)
        {
            InitializeComponent();

            ApplicationName = productName;
            Error = error;
            Logger = logger;
            LogPath = logPath;
            BugReportPath = bugReportPath;

            Text = string.Format("{0} - Error", ApplicationName);
            Logger.WriteException(Error, "Unhandled exception");

            if (Error != null)
            {
                lblErrorMessage.Text = Error.Message;
                txtException.Text = Error.ToString();
            }

            btnOpenLogFile.Visible = !string.IsNullOrEmpty(LogPath) && File.Exists(LogPath);
            btnSendBugReport.Visible = !string.IsNullOrEmpty(BugReportPath);
        }

        private void btnCopyAll_Click(object sender, EventArgs e)
        {
            string text = txtException.Text;

            if (!string.IsNullOrEmpty(text))
            {
                ZAppHelper.CopyTextSafely(text);
            }
        }

        private void btnOpenLogFile_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(LogPath) && File.Exists(LogPath))
            {
                Process.Start(LogPath);
            }
        }

        private void btnSendBugReport_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(BugReportPath))
            {
                StaticHelper.LoadBrowser(BugReportPath);
            }
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            Logger.WriteLine("{0} continue.", ProductName);
            Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Logger.WriteLine("{0} closing. Reason: Unhandled exception", ProductName);
            Application.Exit();
        }

        private void ErrorForm_Shown(object sender, EventArgs e)
        {
            Activate();
            BringToFront();
        }

        public static void ThrowExceptionForTest()
        {
            throw new Exception("Error line one!\r\nError line two!");
        }
    }
}