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
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using HelpersLib;
using Starksoft.Net.Ftp;
using Starksoft.Net.Proxy;
using ZUploader.HelperClasses;

namespace UploadersLib
{
    public sealed class FTP : IDisposable
    {
        public event Uploader.ProgressEventHandler ProgressChanged;

        public FTPAccount Account { get; set; }

        public FtpClient Client { get; set; }

        public bool AutoReconnect { get; set; }

        public Logger logger = new Logger();

        private ProgressManager progress;

        public FTP(FTPAccount account)
        {
            this.Account = account;
            this.Client = new FtpClient();

            switch (account.Protocol)
            {
                case FTPProtocol.FTPS:
                    if (File.Exists(account.FtpsCertLocation))
                    {
                        Client.SecurityProtocol = account.FtpsSecurityProtocol;
                        Client.SecurityCertificates.Add(X509Certificate.CreateFromSignedFile(account.FtpsCertLocation));
                    }
                    else
                        logger.WriteLine("Can't find ftps certificate (" + account.FtpsCertLocation + ")");
                    break;
                default:
                    Client.SecurityProtocol = FtpSecurityProtocol.None;
                    break;
            }

            Client.Host = account.Host;
            Client.Port = account.Port;
            Client.DataTransferMode = account.IsActive ? TransferMode.Active : TransferMode.Passive;

            if (null != Uploader.ProxySettings)
            {
                IProxyClient proxy = Uploader.ProxySettings.GetProxyClient();
                {
                    if (proxy != null)
                    {
                        Client.Proxy = proxy;
                    }
                }
            }

            Client.TransferProgress += new EventHandler<TransferProgressEventArgs>(OnTransferProgressChanged);
            Client.ConnectionClosed += new EventHandler<ConnectionClosedEventArgs>(Client_ConnectionClosed);
        }

        private void OnTransferProgressChanged(object sender, TransferProgressEventArgs e)
        {
            if (ProgressChanged != null)
            {
                progress.ChangeProgress(e.BytesTransferred);
                ProgressChanged(progress);
            }
        }

        private void Client_ConnectionClosed(object sender, ConnectionClosedEventArgs e)
        {
            if (AutoReconnect)
            {
                Connect();
            }
        }

        public bool Connect(string username, string password)
        {
            if (!Client.IsConnected && !string.IsNullOrEmpty(password))
            {
                Client.Open(username, password);
            }
            return Client.IsConnected;
        }

        public bool Connect()
        {
            return Connect(Account.UserName, Account.Password);
        }

        public void Disconnect()
        {
            if (Client != null && Client.IsConnected)
            {
                Client.Close();
            }
        }

        public void UploadData(Stream stream, string remotePath)
        {
            Connect();
            progress = new ProgressManager(stream.Length);
            Client.PutFile(stream, remotePath, FileAction.Create);
        }

        public void UploadData(byte[] data, string remotePath)
        {
            using (MemoryStream stream = new MemoryStream(data, false))
            {
                UploadData(stream, remotePath);
            }
        }

        public void UploadFile(string localPath, string remotePath)
        {
            using (FileStream stream = new FileStream(localPath, FileMode.Open))
            {
                UploadData(stream, remotePath);
            }
        }

        public void UploadImage(Image image, string remotePath)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, image.RawFormat);
                UploadData(stream, remotePath);
            }
        }

        public void UploadText(string text, string remotePath)
        {
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(text), false))
            {
                UploadData(stream, remotePath);
            }
        }

        public void UploadFiles(string[] localPaths, string remotePath)
        {
            string filename;
            foreach (string file in localPaths)
            {
                if (!string.IsNullOrEmpty(file))
                {
                    filename = Path.GetFileName(file);
                    if (File.Exists(file))
                    {
                        UploadFile(file, FTPHelpers.CombineURL(remotePath, filename));
                    }
                    else if (Directory.Exists(file))
                    {
                        List<string> filesList = new List<string>();
                        filesList.AddRange(Directory.GetFiles(file));
                        filesList.AddRange(Directory.GetDirectories(file));
                        string path = FTPHelpers.CombineURL(remotePath, filename);
                        MakeDirectory(path);
                        UploadFiles(filesList.ToArray(), path);
                    }
                }
            }
        }

        public void DownloadFile(string remotePath, string localPath)
        {
            Connect();
            Client.GetFile(remotePath, localPath, FileAction.Create);
        }

        public void DownloadFile(string remotePath, Stream outStream)
        {
            Connect();
            Client.GetFile(remotePath, outStream, false);
        }

        public void DownloadFiles(IEnumerable<FtpItem> files, string localPath)
        {
            string directoryPath;
            foreach (FtpItem file in files)
            {
                if (file != null && !string.IsNullOrEmpty(file.Name))
                {
                    if (file.ItemType == FtpItemType.Directory)
                    {
                        FtpItemCollection newFiles = GetDirList(file.FullPath);
                        directoryPath = Path.Combine(localPath, file.Name);
                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }

                        DownloadFiles(newFiles, directoryPath);
                    }
                    else if (file.ItemType == FtpItemType.File)
                    {
                        DownloadFile(file.FullPath, Path.Combine(localPath, file.Name));
                    }
                }
            }
        }

        public FtpItemCollection GetDirList(string remotePath)
        {
            Connect();
            return Client.GetDirList(remotePath);
        }

        public bool Test(string remotePath)
        {
            if (Connect())
            {
                remotePath = FTPHelpers.AddSlash(remotePath, FTPHelpers.SlashType.Prefix);
                try
                {
                    Client.ChangeDirectory(remotePath);
                }
                catch (Exception)
                {
                    Client.MakeDirectory(remotePath);
                    Client.ChangeDirectory(remotePath);
                }

                return true;
            }
            return false;
        }

        public void MakeDirectory(string remotePath)
        {
            if (Connect())
            {
                try
                {
                    Client.MakeDirectory(remotePath);
                }
                catch (Exception e)
                {
                    StaticHelper.WriteException(e);
                }
            }
        }

        public void MakeMultiDirectory(string remotePath)
        {
            List<string> paths = FTPHelpers.GetPaths(remotePath);

            foreach (string path in paths)
            {
                MakeDirectory(path);
            }
        }

        public void Rename(string fromRemotePath, string toRemotePath)
        {
            Connect();
            Client.Rename(fromRemotePath, toRemotePath);
        }

        public void DeleteFile(string remotePath)
        {
            Connect();
            Client.DeleteFile(remotePath);
        }

        public void DeleteFiles(IEnumerable<FtpItem> files)
        {
            foreach (FtpItem file in files)
            {
                if (file != null && !string.IsNullOrEmpty(file.Name))
                {
                    if (file.ItemType == FtpItemType.Directory)
                    {
                        DeleteDirectory(file.FullPath);
                    }
                    else if (file.ItemType == FtpItemType.File)
                    {
                        DeleteFile(file.FullPath);
                    }
                }
            }
        }

        public void DeleteDirectory(string remotePath)
        {
            Connect();

            string filename = FTPHelpers.GetFileName(remotePath);
            if (filename == "." || filename == "..")
            {
                return;
            }

            FtpItemCollection files = GetDirList(remotePath);

            foreach (FtpItem file in files)
            {
                if (file.ItemType == FtpItemType.Directory)
                {
                    DeleteDirectory(file.FullPath);
                }
                else
                {
                    DeleteFile(file.FullPath);
                }
            }

            Client.DeleteDirectory(remotePath);
        }

        public bool SendCommand(string command)
        {
            Connect();

            try
            {
                Client.Quote(command);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
            Disconnect();
            Client.Dispose();
        }
    }
}