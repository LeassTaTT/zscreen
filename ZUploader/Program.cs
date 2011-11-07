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
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using HelpersLib;
using SingleInstanceApplication;
using UploadersAPILib;
using UploadersLib;

namespace ZUploader
{
    internal static class Program
    {
        private static readonly string ApplicationName = Application.ProductName;

        #region Paths

        private static readonly string DefaultPersonalPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), ApplicationName);
        private static readonly string PortablePersonalPath = Path.Combine(Application.StartupPath, ApplicationName);

        private static readonly string SettingsFileName = ApplicationName + "Settings.xml";
        private static readonly string HistoryFileName = "UploadersHistory.xml";
        private static readonly string UploadersConfigFileName = "UploadersConfig.xml";
        private static readonly string LogFileName = ApplicationName + "Log-{0}-{1}.txt";

        public static string PersonalPath
        {
            get
            {
                if (IsPortable)
                {
                    return PortablePersonalPath;
                }
                else
                {
                    return DefaultPersonalPath;
                }
            }
        }

        public static string SettingsFilePath
        {
            get
            {
                return Path.Combine(PersonalPath, SettingsFileName);
            }
        }

        public static string HistoryFilePath
        {
            get
            {
                if (Settings != null && Settings.UseCustomHistoryPath && !string.IsNullOrEmpty(Program.Settings.CustomHistoryPath))
                {
                    return Settings.CustomHistoryPath;
                }
                else
                {
                    return Path.Combine(PersonalPath, HistoryFileName);
                }
            }
        }

        public static string UploadersConfigFilePath
        {
            get
            {
                if (Settings != null && Settings.UseCustomUploadersConfigPath && !string.IsNullOrEmpty(Program.Settings.CustomUploadersConfigPath))
                {
                    return Settings.CustomUploadersConfigPath;
                }
                else
                {
                    return Path.Combine(PersonalPath, UploadersConfigFileName);
                }
            }
        }

        public static string LogFilePath
        {
            get
            {
                DateTime now = FastDateTime.Now;
                return Path.Combine(PersonalPath, string.Format(LogFileName, now.Year, now.Month));
            }
        }

        #endregion Paths

        public static Settings Settings { get; private set; }
        public static UploadersConfig UploadersConfig { get; private set; }
        public static bool IsMultiInstance { get; private set; }
        public static bool IsPortable { get; private set; }
        public static bool IsSilentRun { get; private set; }
        public static Stopwatch StartTimer { get; private set; }

        public static Logger MyLogger { get; private set; }

        public static string Title
        {
            get
            {
                string title = string.Format("{0} {1} r{2}", ApplicationName, Application.ProductVersion, AppRevision);
                if (IsPortable) title += " Portable";
                return title;
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

        public static MainForm mainForm;

        [STAThread]
        private static void Main(string[] args)
        {
            StartTimer = Stopwatch.StartNew();

            IsMultiInstance = CLIHelper.CheckArgs(args, "m", "multi");

            if (!IsMultiInstance && !ApplicationInstanceManager.CreateSingleInstance(SingleInstanceCallback))
            {
                return;
            }

            IsSilentRun = CLIHelper.CheckArgs(args, "s", "silent");
            IsPortable = Directory.Exists(PortablePersonalPath);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!IsSilentRun)
            {
                SplashForm.ShowSplash();
            }

            MyLogger = new Logger();
            StaticHelper.MyLogger = MyLogger;
            MyLogger.WriteLine("{0} {1} r{2} started", Application.ProductName, Application.ProductVersion, AppRevision);
            MyLogger.WriteLine("Operating system: " + Environment.OSVersion.VersionString);
            MyLogger.WriteLine("CommandLine: " + string.Join(" ", args));
            MyLogger.WriteLine("IsMultiInstance: " + IsMultiInstance);
            MyLogger.WriteLine("IsSilentRun: " + IsSilentRun);
            MyLogger.WriteLine("IsPortable: " + IsPortable);

            Thread settingThread = new Thread(() =>
            {
                LoadSettings();
                LoadUploadersConfig();
            });
            settingThread.Start();

            MyLogger.WriteLine("new MainForm() started");
            mainForm = new MainForm();
            MyLogger.WriteLine("new MainForm() finished");

            settingThread.Join();

            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.Run(mainForm);

            Settings.Save();
            UploadersConfig.Write(UploadersConfigFilePath);

            MyLogger.WriteLine("ZUploader closing");
            MyLogger.SaveLog(LogFilePath);
        }

        public static void LoadSettings()
        {
            Settings = Settings.Load();
        }

        public static void LoadUploadersConfig()
        {
            UploadersConfig = UploadersConfig.Read(UploadersConfigFilePath);
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            OnError(e.Exception);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            OnError((Exception)e.ExceptionObject);
        }

        private static void OnError(Exception e)
        {
            new ErrorForm(Application.ProductName, e, MyLogger, LogFilePath, ZLinks.URL_ISSUES).ShowDialog();
        }

        private static void SingleInstanceCallback(object sender, InstanceCallbackEventArgs args)
        {
            if (WaitFormLoad(5000))
            {
                Action d = () =>
                {
                    if (mainForm.WindowState == FormWindowState.Minimized)
                    {
                        mainForm.WindowState = FormWindowState.Normal;
                    }

                    mainForm.BringToFront();
                    mainForm.Activate();
                    mainForm.UseCommandLineArgs(args.CommandLineArgs);
                };

                mainForm.Invoke(d);
            }
        }

        private static bool WaitFormLoad(int wait)
        {
            Stopwatch timer = Stopwatch.StartNew();

            while (timer.ElapsedMilliseconds < wait)
            {
                if (mainForm != null && mainForm.IsReady) return true;

                Thread.Sleep(10);
            }

            return false;
        }
    }
}