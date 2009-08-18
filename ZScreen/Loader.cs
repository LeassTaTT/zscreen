﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using ZScreenLib;
using System.Windows.Forms;
using System.Threading;

namespace ZSS
{
    public static class Loader
    {
        // internal static ZSS.Forms.SplashScreen Splash = null;
        public static WorkerPrimary Worker;
        public static WorkerSecondary Worker2;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Splash = new ZSS.Forms.SplashScreen();
            //AppDomain.CurrentDomain.AssemblyLoad += new AssemblyLoadEventHandler(CurrentDomain_AssemblyLoad);
            //Splash.Show();

            string[] args = Environment.GetCommandLineArgs();

            if (args.Length > 2 && args[1] == "/doc")
            {
                string filePath = string.Join(" ", args, 2, args.Length - 2);
                if (File.Exists(filePath))
                {
                    Process.Start(filePath);
                }
            }
            else
            {
                RunZScreen();
            }
        }

        private static void RunZScreen()
        {
            try
            {
                Program.Load(true);
                Application.Run(new ZScreen());
            }
            catch (Exception ex)
            {
                FileSystem.AppendDebug(ex);
            }
            finally
            {
                Program.Unload();
            }
        }

        //static void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        //{
        //    Splash.AsmLoads.Enqueue("Loading " + args.LoadedAssembly.GetName().Name);
        //}
    }
}