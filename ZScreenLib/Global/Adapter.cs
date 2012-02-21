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
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using GraphicsMgrLib;
using HelpersLib;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.WindowsAPICodePack.Taskbar;
using UploadersLib;
using UploadersLib.FileUploaders;
using UploadersLib.HelperClasses;
using UploadersLib.ImageUploaders;
using ZScreenLib.Properties;

namespace ZScreenLib
{
    /// <summary>
    /// Class for public static methods for use in ZScreen
    /// </summary>
    public static class Adapter
    {
        #region Worker Tasks

        public static void PrintImage(Image img)
        {
            if (img != null)
            {
                new PrintForm(img, Engine.ConfigUI.PrintSettings).Show();
            }
        }

        public static void PrintText(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                new PrintTextForm(text, Engine.ConfigUI.PrintSettings).Show();
            }
        }

        public static void CopyDataToClipboard(object data)
        {
            Clipboard.SetDataObject(data, true);
        }

        public static void CopyImageToClipboard(Image img, bool bCompatible)
        {
            if (img != null)
            {
                if (bCompatible)
                {
                    CopyMultiFormatBitmapToClipboard(img);
                }
                else
                {
                    CopyMultiFormatBitmapToClipboardPng(img);
                }
            }
        }

        private static void CopyMultiFormatBitmapToClipboard(Image img)
        {
            if (img != null)
            {
                MemoryStream ms = new MemoryStream();
                MemoryStream ms2 = new MemoryStream();
                Bitmap bmp = new Bitmap(img);
                bmp.Save(ms, ImageFormat.Bmp);
                byte[] b = ms.GetBuffer();
                ms2.Write(b, 14, (int)ms.Length - 14);
                ms.Position = 0;
                DataObject dataObject = new DataObject();
                dataObject.SetData(DataFormats.Bitmap, bmp);
                dataObject.SetData(DataFormats.Dib, ms2);
                Clipboard.SetDataObject(dataObject, true, 3, 1000);
            }
        }

        private static void CopyMultiFormatBitmapToClipboardPng(this Image image)
        {
            using (var opaque = image.CreateOpaqueBitmap(Color.White))
            using (var stream = new MemoryStream())
            {
                image.Save(stream, ImageFormat.Png);

                Clipboard.Clear();
                var data = new DataObject();
                data.SetData(DataFormats.Bitmap, true, opaque);
                data.SetData("PNG", true, stream);
                Clipboard.SetDataObject(data, true, 3, 1000);
            }
        }

        private static Image CreateOpaqueBitmap(this Image image, Color backgroundColor)
        {
            var bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format24bppRgb);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(backgroundColor);
                graphics.DrawImage(image, 0, 0, image.Width, image.Height);
            }

            return bitmap;
        }

        public static void CopyImageToClipboard(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                using (Image img = Image.FromFile(filePath))
                {
                    CopyImageToClipboard(img, false);
                }

                DebugHelper.WriteLine(string.Format("Saved {0} as an Image to Clipboard...", filePath));
            }
        }

        public static void FlashNotifyIcon(NotifyIcon ni, Icon ico)
        {
            if (ni != null && ico != null)
            {
                ni.Icon = ico;
            }
        }

        public static void SetNotifyIconBalloonTip(NotifyIcon ni, string title, string msg, ToolTipIcon ico)
        {
            if (ni != null && !string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(msg))
            {
                ni.ShowBalloonTip(5000, title, msg, ico);
            }
        }

        public static void UpdateNotifyIconProgress(NotifyIcon ni, int progress)
        {
            if (ni != null)
            {
                using (Bitmap img = (Bitmap)GraphicsMgr.DrawProgressIcon(UploadManager.GetAverageProgress()))
                {
                    IntPtr hicon = img.GetHicon();
                    ni.Icon = Icon.FromHandle(hicon);
                    NativeMethods.DestroyIcon(hicon);
                }
            }
        }

        #endregion Worker Tasks

        public static string ResourcePath = Path.Combine(Application.StartupPath, "ZSS.ResourcesLib.dll");

        public static void SaveMenuConfigToList<T>(ToolStripDropDownButton src_tsddb, List<T> dest_list)
        {
            dest_list.Clear();
            foreach (var obj in src_tsddb.DropDownItems)
            {
                if (obj is ToolStripMenuItem)
                {
                    ToolStripMenuItem tsmi = (ToolStripMenuItem)obj;
                    if (tsmi.Checked)
                    {
                        dest_list.Add((T)tsmi.Tag);
                    }
                }
            }
        }

        public static void AddToClipboardByDoubleClick(Control tp)
        {
            Control ctl = tp.GetNextControl(tp, true);
            while (ctl != null)
            {
                if (ctl.GetType() == typeof(TextBox))
                {
                    ctl.DoubleClick += TextBox_DoubleClick;
                }
                ctl = tp.GetNextControl(ctl, true);
            }
        }

        public static void TextBox_DoubleClick(object sender, EventArgs e)
        {
            TextBox tb = ((TextBox)sender);
            if (!string.IsNullOrEmpty(tb.Text))
            {
                Clipboard.SetText(tb.Text); // ok
            }
        }

        #region FTP Methods

        public static void TestFTPAccount(FTPAccount account, bool silent)
        {
            string msg = string.Empty;
            string sfp = account.GetSubFolderPath();
            bool succ = false;
            switch (account.Protocol)
            {
                case FTPProtocol.SFTP:
                    SFTP sftp = new SFTP(account);
                    if (!sftp.isInstantiated)
                    {
                        msg = "An SFTP client couldn't be instantiated, not enough information.\nCould be a missing key file.";
                    }
                    else
                    {
                        sftp.Connect();
                        List<string> createddirs = new List<string>();
                        if (!sftp.DirectoryExists(sfp))
                        {
                            createddirs = sftp.CreateMultipleDirectorys(FTPHelpers.GetPaths(sfp));
                        }
                        if (sftp.IsConnected)
                        {
                            msg = (createddirs.Count == 0) ? "Connected!" : "Conected!\nCreated folders;\n";
                            for (int x = 0; x <= createddirs.Count - 1; x++)
                            {
                                msg += createddirs[x] + "\n";
                            }
                            msg += " \n\nPing results:\n " + SendPing(account.Host, 3);
                            sftp.Disconnect();
                        }
                    }
                    break;
                default:
                    using (FTP ftpClient = new FTP(account))
                    {
                        try
                        {
                            succ = ftpClient.Test(sfp);
                            if (succ)
                            {
                                msg = "Success!";
                            }
                        }
                        catch (Exception e)
                        {
                            if (e.Message.StartsWith("Could not change working directory to"))
                            {
                                try
                                {
                                    ftpClient.MakeMultiDirectory(sfp);
                                    ftpClient.Test(sfp);
                                    msg = "Success!\nAuto created folders: " + sfp;
                                }
                                catch (Exception e2)
                                {
                                    msg = e2.Message;
                                }
                            }
                            else
                            {
                                msg = e.Message;
                            }
                        }
                    }

                    if (succ && !string.IsNullOrEmpty(msg))
                    {
                        string ping = SendPing(account.Host, 3);
                        if (!string.IsNullOrEmpty(ping))
                        {
                            msg += "\n\nPing results:\n" + ping;
                        }
                    }
                    break;
            }
            if (succ && silent)
            {
                DebugHelper.WriteLine(string.Format("Tested {0} sub-folder path in {1}", sfp, account.ToString()));
            }
            else if (succ)
            {
                MessageBox.Show(msg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static string SendPing(string host)
        {
            return SendPing(host, 1);
        }

        public static string SendPing(string host, int count)
        {
            string[] status = new string[count];

            using (Ping ping = new Ping())
            {
                PingReply reply;
                //byte[] buffer = Encoding.ASCII.GetBytes(new string('a', 32));
                for (int i = 0; i < count; i++)
                {
                    reply = ping.Send(host, 3000);
                    if (reply.Status == IPStatus.Success)
                    {
                        status[i] = reply.RoundtripTime.ToString() + " ms";
                    }
                    else
                    {
                        status[i] = "Timeout";
                    }
                    Thread.Sleep(100);
                }
            }

            return string.Join(", ", status);
        }

        public static bool CheckFTPAccounts(int id)
        {
            return Engine.ConfigUploaders.FTPAccountList2.HasValidIndex(id);
        }

        public static bool CheckFTPAccounts(WorkerTask task, int id)
        {
            bool result = CheckFTPAccounts(id);
            if (!result) task.Errors.Add("An FTP account does not exist or not selected properly.");
            return result;
        }

        #endregion FTP Methods

        public static bool FindItemInList<T>(List<T> list, string name)
        {
            foreach (T item in list)
            {
                if (item.ToString() == name)
                {
                    return true;
                }
            }
            return false;
        }

        #region Proxy Methods

        public static ProxySettings CheckProxySettings()
        {
            DebugHelper.WriteLine("Proxy Config: " + Engine.ConfigUI.ConfigProxy.ProxyConfigType.ToString() + " called by " + new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name);
            return new ProxySettings { ProxyConfig = Engine.ConfigUI.ConfigProxy.ProxyConfigType, ProxyActive = Engine.ConfigUI.ConfigProxy.ProxyActive };
        }

        public static void TestProxyAccount(ProxyInfo acc)
        {
            string msg = "Success!";

            try
            {
                NetworkCredential cred = new NetworkCredential(acc.UserName, acc.Password);
                WebProxy wp = new WebProxy(acc.GetAddress(), true, null, cred);
                WebClient wc = new WebClient { Proxy = wp };
                wc.DownloadString("http://www.google.com");
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (!string.IsNullOrEmpty(msg))
            {
                MessageBox.Show(msg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion Proxy Methods

        public static void DeleteFile(string fp)
        {
            if (File.Exists(fp))
            {
                Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(fp, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                                                                   Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
            }
        }

        #region Twitter Methods

        /// <summary>
        /// Returns the active TwitterAuthInfo object; if nothing is active then a new TwitterAuthInfo object is returned
        /// </summary>
        /// <returns></returns>
        public static OAuthInfo TwitterGetActiveAccount()
        {
            if (Engine.ConfigUploaders.TwitterOAuthInfoList.HasValidIndex(Engine.ConfigUploaders.TwitterSelectedAccount))
            {
                return Engine.ConfigUploaders.TwitterOAuthInfoList[Engine.ConfigUploaders.TwitterSelectedAccount];
            }

            return new OAuthInfo(Engine.ConfigUI.ApiKeys.TwitterConsumerKey, Engine.ConfigUI.ApiKeys.TwitterConsumerSecret);
        }

        public static void TwitterMsg(WorkerTask task)
        {
            StringBuilder sb = new StringBuilder();
            foreach (UploadResult ur in task.UploadResults)
            {
                sb.AppendLine(ur.URL);
            }
            if (sb.Length > 0)
            {
                TwitterMsg(sb.ToString());
            }
        }

        public static void ShowTwitterClient()
        {
            TwitterMsg(string.Empty);
        }

        public static void TwitterMsg(string url)
        {
            OAuthInfo acc = TwitterGetActiveAccount();
            if (!string.IsNullOrEmpty(acc.UserToken))
            {
                TwitterMsg msg = new TwitterMsg(TwitterGetActiveAccount(), string.Format("{0} - Update Twitter Status...", acc.Description));
                msg.ActiveAccountName = acc.Description;
                msg.Icon = Resources.zss_main;
                msg.Config = Engine.ConfigUI.TwitterClientConfig;
                msg.FormClosed += new FormClosedEventHandler(twitterClient_FormClosed);
                msg.txtTweet.Text = url;
                msg.Show();
            }
        }

        private static void twitterClient_FormClosed(object sender, FormClosedEventArgs e)
        {
            TwitterMsg msg = sender as TwitterMsg;
            Engine.ConfigUI.TwitterClientConfig = msg.Config;
        }

        #endregion Twitter Methods

        public static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        public static string ZScreenCliPath()
        {
            return Application.ExecutablePath;
        }

        public static string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public static string AppRevision
        {
            get
            {
                return AssemblyVersion.Split('.')[3];
            }
        }

        public static string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public static string GetDirPathUsingFolderBrowser(string title)
        {
            string newDir = string.Empty;
            if (TaskbarManager.IsPlatformSupported)
            {
                var dlg = new CommonOpenFileDialog
                              {
                                  EnsureReadOnly = true,
                                  IsFolderPicker = true,
                                  AllowNonFileSystemItems = true,
                                  Title = title
                              };

                if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    newDir = dlg.FileName;
                }
            }
            else
            {
                var dlg = new FolderBrowserDialog { Description = title };
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    newDir = dlg.SelectedPath;
                }
            }
            return newDir;
        }

        #region "Windows 7 only"

        public static void TaskbarSetProgressState(Form form, TaskbarProgressBarState tbps)
        {
            if (form != null && form.ShowInTaskbar && TaskbarManager.IsPlatformSupported && Engine.zWindowsTaskbar != null)
            {
                Engine.zWindowsTaskbar.SetProgressState(tbps);
            }
        }

        public static void TaskbarSetProgressValue(Form form, int progress)
        {
            if (form != null && form.ShowInTaskbar && TaskbarManager.IsPlatformSupported && Engine.zWindowsTaskbar != null)
            {
                Engine.zWindowsTaskbar.SetProgressValue(progress, 100);
            }
        }

        public static void AddRecentItem(string filePath)
        {
            if (TaskbarManager.IsPlatformSupported && File.Exists(filePath) && Engine.zJumpList != null)
            {
                try
                {
                    Engine.zJumpList.KnownCategoryToDisplay = JumpListKnownCategoryType.Recent;
                    Microsoft.WindowsAPICodePack.Taskbar.JumpList.AddToRecent(filePath);
                }
                catch (Exception ex)
                {
                    DebugHelper.WriteException(ex, "Error while adding Recent Item to Windows 7 Taskbar");
                }
            }
        }

        #endregion "Windows 7 only"

        public static bool ClipboardMonitor
        {
            get
            {
                return Engine.ConfigUI.MonitorImages || Engine.ConfigUI.MonitorText || Engine.ConfigUI.MonitorFiles || Engine.ConfigUI.MonitorUrls;
            }
        }

        public static void UpdateTinyPicRegCode()
        {
            if (Uploader.ProxySettings != null && Engine.ConfigWorkflow != null)
            {
                try
                {
                    if (Engine.ConfigUploaders.TinyPicRememberUserPass &&
                        !string.IsNullOrEmpty(Engine.ConfigUploaders.TinyPicUsername) &&
                        !string.IsNullOrEmpty(Engine.ConfigUploaders.TinyPicPassword))
                    {
                        var tpu = new TinyPicUploader(Engine.ConfigUI.ApiKeys.TinyPicID, Engine.ConfigUI.ApiKeys.TinyPicKey, AccountType.User);
                        var regCode = tpu.UserAuth(Engine.ConfigUploaders.TinyPicUsername,
                            Engine.ConfigUploaders.TinyPicPassword);
                        if (Engine.ConfigUploaders.TinyPicRegistrationCode != regCode)
                        {
                            DebugHelper.WriteLine(string.Format("Updated TinyPic Shuk from {0} to {1}", Engine.ConfigUploaders.TinyPicRegistrationCode, regCode));
                            Engine.ConfigUploaders.TinyPicRegistrationCode = regCode;
                        }
                    }
                }
                catch (Exception ex)
                {
                    DebugHelper.WriteException(ex, "error while trying to update TinyPic registration code.");
                }
            }
        }
    }
}