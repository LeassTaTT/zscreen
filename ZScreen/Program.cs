﻿#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2009  Brandon Zimmerman

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
#endregion

using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Threading;
using ZSS.Properties;

namespace ZSS
{
    static class Program
    {
        private static readonly string LocalAppDataFolder = System.IO.Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Application.ProductName);

        private static readonly string XMLFileName = "Settings.xml";
        private static readonly string HistoryFileName = "History.xml";
        private static readonly string OldXMLFilePath = Path.Combine(LocalAppDataFolder, XMLFileName);
        private static readonly string OldXMLPortableFile = Path.Combine(Application.StartupPath, XMLFileName);
        private static readonly string PortableRootFolder = Path.Combine(Application.StartupPath, Application.ProductName);

        public static string RootAppFolder { get; set; }

        public static string CacheDir { get; set; }
        public static string FilesDir { get; set; }
        public static string ImagesDir { get; set; }
        public static string SettingsDir { get; set; }
        public static string TempDir { get; set; }
        public static string TextDir { get; set; }

        private static string[] AppDirs; 

        internal static string DefaultXMLFilePath;
        private static string XMLPortableFile;

        private static string DefaultRootAppFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), Application.ProductName);

        public const string URL_ISSUES = "http://code.google.com/p/zscreen/issues/entry";
        public const string URL_PROJECTPAGE = "http://code.google.com/p/zscreen/";
        public const string URL_WEBSITE = "http://brandonz.net/projects/zscreen/";

        public const string IMAGESHACK_KEY = "78EHNOPS04e77bc6df1cc0c5fc2e92e11c7b4a1a";
        public const string TINYPIC_ID = "e2aabb8d555322fa";
        public const string TINYPIC_KEY = "00a68ed73ddd54da52dc2d5803fa35ee";

        public static string[] zImageFileTypes = { "png", "jpg", "gif", "bmp", "tif", "emf", "wmf", "ico" };
        public static string[] zTextFileTypes = { "txt", "log" };

        public static McoreSystem.AppInfo mAppInfo = new McoreSystem.AppInfo(Application.ProductName,
            Application.ProductVersion, McoreSystem.AppInfo.SoftwareCycle.Beta, false);
        public static bool MultipleInstance = false;
        private static string mProductName = Application.ProductName;

        public static void SetRootFolder(string dp)
        {
            Settings.Default.RootDir = dp;
            RootAppFolder = dp;
        }

        /// <summary>
        /// Function to update Default Folder Paths based on Root folder
        /// </summary>
        public static void InitializeDefaultFolderPaths()
        {
            CacheDir = Path.Combine(RootAppFolder, "Cache");
            FilesDir = Path.Combine(RootAppFolder, "Files");
            ImagesDir = Path.Combine(RootAppFolder, "Images");
            SettingsDir = Path.Combine(RootAppFolder, "Settings");
            TextDir = Path.Combine(RootAppFolder, "Text");
            TempDir = Path.Combine(RootAppFolder, "Temp");

            AppDirs = new string[] { CacheDir, FilesDir, ImagesDir, SettingsDir, TempDir, TextDir };

            foreach (string dp in AppDirs)
            {
                if (!Directory.Exists(dp))
                {
                    Directory.CreateDirectory(dp);
                }
            }

            DefaultXMLFilePath = Path.Combine(SettingsDir, XMLFileName);
            XMLPortableFile = Path.Combine(SettingsDir, XMLFileName);
        }

        public static string XMLSettingsFile
        {
            get
            {

                if (!Directory.Exists(SettingsDir))
                {
                    Directory.CreateDirectory(SettingsDir);
                }

                if (File.Exists(OldXMLPortableFile))
                {
                    if (!File.Exists(XMLPortableFile))
                    {
                        File.Move(OldXMLPortableFile, XMLPortableFile);
                    }
                    return XMLPortableFile;                               // Portable
                }
                else if (File.Exists(OldXMLFilePath))
                {
                    if (!File.Exists(DefaultXMLFilePath))
                    {
                        File.Move(OldXMLFilePath, DefaultXMLFilePath);
                    }
                    return DefaultXMLFilePath;                            // v1.x
                }
                else
                {
                    return DefaultXMLFilePath;                            // v2.x
                }
            }
        }

        public static string HistoryFile
        {
            get
            {
                return Path.Combine(SettingsDir, HistoryFileName);
            }
        }

        public static XMLSettings conf = null;

        public const string EXT_FTP_ACCOUNTS = "zfa";
        public static readonly string FILTER_ACCOUNTS = string.Format("ZScreen FTP Accounts(*.{0})|*.{0}", EXT_FTP_ACCOUNTS);
        public const string FILTER_IMAGE_HOSTING_SERVICES = "ZScreen Image Uploaders(*.zihs)|*.zihs";
        public const string FILTER_SETTINGS = "ZScreen XML Settings(*.xml)|*.xml";

        public static Rectangle LastRegion = Rectangle.Empty;
        public static Rectangle LastCapture = Rectangle.Empty;

        private static ZScreen ZScreenWindow;

        private static Mutex mAppMutex = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
       
            if (String.IsNullOrEmpty(Settings.Default.RootDir))
            {
                Settings.Default.Upgrade();
                Settings.Default.RootDir = DefaultRootAppFolder;
            }

            if (Directory.Exists(PortableRootFolder))
            {
                RootAppFolder = PortableRootFolder;
                mProductName += " Portable";
                mAppInfo.AppName = mProductName;
            }
            else
            {
                RootAppFolder = Settings.Default.RootDir;
            }

            InitializeDefaultFolderPaths();
            conf = XMLSettings.Read();

            bool bGrantedOwnership;
            try
            {
                mAppMutex = new Mutex(true, @"Global\0167D1A0-6054-42f5-BA2A-243648899A6B", out bGrantedOwnership);
            }
            catch (UnauthorizedAccessException)
            {
                bGrantedOwnership = false;
            }

            if (!bGrantedOwnership)
            {
                MultipleInstance = true;
                mProductName += "*";
                mAppInfo.AppName = mProductName;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ZScreenWindow = new ZScreen();

            if (conf.WindowSize.Height == 0 || conf.WindowSize.Width == 0)
            {
                conf.WindowSize = ZScreenWindow.Size;
            }

            User32.m_Proc = ZScreenWindow.ScreenshotUsingHotkeys;

            ZScreenWindow.m_hID = User32.setHook();

            Application.Run(ZScreenWindow);

            User32.UnhookWindowsHookEx(ZScreenWindow.m_hID);
        }

        public static void ConfigureDirs()
        {
            // Settings         
            if (Program.conf.SettingsDir != SettingsDir)
            {
                conf.SettingsDir = SettingsDir;
            }
            if (!Directory.Exists(conf.SettingsDir))
            {
                Directory.CreateDirectory(SettingsDir);
            }
            // Images
            if (conf.ImagesDir != ImagesDir)
            {
                conf.ImagesDir = ImagesDir;
            }
            if (!Directory.Exists(conf.ImagesDir))
            {
                Directory.CreateDirectory(ImagesDir);
            }
            // Text
            if (conf.TextDir != TextDir)
            {
                conf.TextDir = TextDir;
            }
            if (!Directory.Exists(conf.TextDir))
            {
                Directory.CreateDirectory(TextDir);
            }
            // Cache
            if (Program.conf.CacheDir != CacheDir)
            {
                conf.CacheDir = CacheDir;
            }
            if (!Directory.Exists(Program.conf.CacheDir))
            {
                Directory.CreateDirectory(Program.conf.CacheDir);
            }
            // Temp
            if (Program.conf.TempDir != TempDir)
            {
                conf.TempDir = TempDir;
            }
            if (!Directory.Exists(Program.conf.TempDir))
            {
                Directory.CreateDirectory(Program.conf.TempDir);
            }
        }

        public static bool CheckFTPAccounts(ref ZSS.Tasks.MainAppTask task)
        {
            if (Program.conf.FTPAccountList.Count > 0 && Program.conf.FTPselected != -1 &&
                Program.conf.FTPAccountList.Count > Program.conf.FTPselected)
            {
                return true;
            }
            else
            {
                task.Errors.Add("An FTP account does not exist or not selected properly.");
                return false;
            }
        }
    }
}