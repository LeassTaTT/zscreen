﻿using System;
using System.IO;
using HelpersLib;
using UploadersLib.FileUploaders;
using UploadersLib.HelperClasses;

namespace UploadersLib
{
    public sealed class SFTPUploader : FileUploader
    {
        public FTPAccount FTPAccount;
        SFTP sftpClient;
        public bool isInstantiated { get { return sftpClient.isInstantiated; } }

        public SFTPUploader(FTPAccount account)
        {
            FTPAccount = account;
            sftpClient = new SFTP(FTPAccount);
        }

        public override UploadResult Upload(Stream stream, string fileName)
        {
            UploadResult result = new UploadResult();

            sftpClient.ProgressChanged += new Uploader.ProgressEventHandler(x => OnProgressChanged(x));

            fileName = ZAppHelper.ReplaceIllegalChars(fileName, '_');

            while (fileName.IndexOf("__") != -1)
            {
                fileName = fileName.Replace("__", "_");
            }

            try
            {
                stream.Position = 0;
                sftpClient.UploadData(stream, fileName);
            }
            catch (Exception e)
            {
                StaticHelper.WriteException(e);
                Errors.Add(e.Message);
            }

            if (Errors.Count == 0)
            {
                result.URL = FTPAccount.GetUriPath(fileName);
            }

            return result;
        }
    }
}