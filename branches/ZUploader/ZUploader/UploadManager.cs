﻿using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using UploadersLib;
using ZUploader.Properties;

namespace ZUploader
{
    public static class UploadManager
    {
        public static ImageDestType2 ImageUploader { get; set; }
        public static TextDestType TextUploader { get; set; }
        public static FileUploaderType FileUploader { get; set; }
        public static ListView ListViewControl { get; set; }

        private static List<Task> Tasks = new List<Task>();
        private static int ID;

        private static void StartUpload(Task task)
        {
            Tasks.Add(task);
            task.UploadStarted += new Task.UploadStartedEventHandler(task_UploadStarted);
            task.UploadCompleted += new Task.UploadCompletedEventHandler(task_UploadCompleted);
            task.Start();
        }

        private static void task_UploadStarted(Task sender)
        {
            if (ListViewControl != null)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = sender.TaskID.ToString();
                lvi.SubItems.Add("Upload started");
                lvi.SubItems.Add(string.Empty);
                ListViewControl.Items.Add(lvi);
            }
        }

        private static void task_UploadCompleted(Task sender, Task.UploadCompletedEventArgs e)
        {
            if (ListViewControl != null)
            {
                ListViewItem lvi = ListViewControl.Items[sender.TaskID];
                lvi.SubItems[1].Text = "Upload completed";
                lvi.SubItems[2].Text = e.URL;
                lvi.EnsureVisible();

                if (Settings.Default.ClipboardAutoCopy && !string.IsNullOrEmpty(e.URL))
                {
                    Clipboard.SetText(e.URL);
                }

                Tasks.Remove(sender);
                sender.Dispose();
            }
        }

        public static int GetID()
        {
            return ID++;
        }

        public static void DoClipboardUpload()
        {
            if (Clipboard.ContainsImage())
            {
                using (Image img = Clipboard.GetImage())
                using (MemoryStream ms = new MemoryStream())
                {
                    img.Save(ms, ImageFormat.Png);
                    Task task = new Task(Image.FromStream(ms), Helpers.GetRandomAlphanumeric(10) + ".png");
                    StartUpload(task);
                }
            }
            else if (Clipboard.ContainsText())
            {
                StartUpload(new Task(Clipboard.GetText()));
            }
            else if (Clipboard.ContainsFileDropList())
            {
                StringCollection files = Clipboard.GetFileDropList();

                foreach (string file in files)
                {
                    StartUpload(CheckFile(file));
                }
            }
        }

        private static Task CheckFile(string path)
        {
            Task task;
            string fileName = Path.GetFileName(path);

            using (Stream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                if (Helpers.IsValidTextFile(path))
                {
                    task = new Task(EDataType.Text, stream, fileName);
                }
                else if (Helpers.IsValidImageFile(stream))
                {
                    task = new Task(Image.FromFile(path), fileName);
                }
                else
                {
                    task = new Task(EDataType.Data, stream, fileName);
                }
            }

            return task;
        }
    }
}