﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UploadersLib;

namespace ZScreenLib
{
    public class DestSelectorHelper
    {
        private DestSelector ucDestOptions;

        public DestSelectorHelper(DestSelector ds)
        {
            ucDestOptions = ds;
        }

        public void AddEnumDestToMenuWithConfigSettings()
        {
            AddEnumOutputsWithConfigSettings();
            AddEnumClipboardContentWithConfigSettings();
            AddEnumLinkFormatWithConfigSettings();
            AddEnumDestImageToMenuWithConfigSettings();
            AddEnumDestTextToMenuWithConfigSettings();
            AddEnumDestFileToMenuWithConfigSettings();
            AddEnumDestLinkToMenuWithConfigSettings();
        }

        public void AddEnumOutputsWithConfigSettings()
        {
            SetupOutputsWithRuntimeSettings(ucDestOptions.tsddbOutputs, Engine.conf.ConfOutputs);
        }

        public void AddEnumOutputsWithConfigSettings(List<OutputEnum> list)
        {
            SetupOutputsWithRuntimeSettings(ucDestOptions.tsddbOutputs, list);
        }

        private void SetupOutputsWithRuntimeSettings(ToolStripDropDownButton tsddb, List<OutputEnum> list)
        {
            foreach (ToolStripMenuItem tsmi in tsddb.DropDownItems)
            {
                tsmi.Click += new EventHandler(tsmiOutputs_Click);
            }

            ucDestOptions.tsmiClipboard.Tag = OutputEnum.Clipboard;
            ucDestOptions.tsmiClipboard.Checked = list.Contains(OutputEnum.Clipboard);

            ucDestOptions.tsmiFile.Tag = OutputEnum.File;
            ucDestOptions.tsmiFile.Checked = list.Contains(OutputEnum.File);

            ucDestOptions.tsmiPrinter.Tag = OutputEnum.Printer;
            ucDestOptions.tsmiPrinter.Checked = list.Contains(OutputEnum.Printer);

            UpdateToolStripOutputs();
        }

        public void AddEnumLinkFormatWithConfigSettings()
        {
            if (Engine.conf.ConfLinkFormat.Count == 0)
            {
                Engine.conf.ConfLinkFormat.Add((int)ClipboardContentEnum.Data);
            }
            AddEnumLinkFormatWithConfigSettings(ucDestOptions.tsddbLinkFormat, Engine.conf.ConfLinkFormat);
        }

        public void AddEnumLinkFormatWithRuntimeSettings(List<int> list)
        {
            AddEnumLinkFormatWithConfigSettings(ucDestOptions.tsddbLinkFormat, list);
        }

        public void AddEnumLinkFormatWithConfigSettings(ToolStripDropDownButton tsddb, List<int> list)
        {
            if (tsddb.DropDownItems.Count == 0)
            {
                foreach (LinkFormatEnum t in Enum.GetValues(typeof(LinkFormatEnum)))
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(t.GetDescription());
                    tsmi.Tag = t;
                    tsmi.Checked = list.Contains((int)t);
                    tsmi.Click += new EventHandler(tsmiDestLinkFormat_Click);
                    tsddb.DropDownItems.Add(tsmi);
                }
                UpdateToolStripLinkFormat();
            }
        }

        public void AddEnumClipboardContentWithConfigSettings()
        {
            if (Engine.conf.ConfClipboardContent.Count == 0)
            {
                Engine.conf.ConfClipboardContent.Add((int)ClipboardContentEnum.Data);
            }
            AddEnumClipboardContentWithRuntimeSettings(ucDestOptions.tsddbClipboardContent, Engine.conf.ConfClipboardContent);
            ucDestOptions.EnableDisableDestControls();
        }

        public void AddEnumClipboardContentWithRuntimeSettings(List<int> cctList)
        {
            AddEnumClipboardContentWithRuntimeSettings(ucDestOptions.tsddbClipboardContent, cctList);
        }

        public void AddEnumClipboardContentWithRuntimeSettings(ToolStripDropDownButton tsddb, List<int> ClipboardContentType)
        {
            foreach (ClipboardContentEnum t in Enum.GetValues(typeof(ClipboardContentEnum)))
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem(t.GetDescription());
                tsmi.Tag = t;
                tsmi.Checked = ClipboardContentType.Contains((int)t);
                tsmi.Click += new EventHandler(tsmiDestClipboardContent_Click);
                tsddb.DropDownItems.Add(tsmi);
            }
            UpdateToolStripClipboardContent();
        }

        public void AddEnumDestImageToMenuWithConfigSettings()
        {
            AddEnumDestImageToMenuWithRuntimeSettings(ucDestOptions.tsddbDestImage, Engine.conf.MyImageUploaders);
        }

        public void AddEnumDestImageToMenuWithRuntimeSettings(List<int> MyImageUploaders)
        {
            AddEnumDestImageToMenuWithRuntimeSettings(ucDestOptions.tsddbDestImage, MyImageUploaders);
        }

        private void AddEnumDestImageToMenuWithRuntimeSettings(ToolStripDropDownButton tsddb, List<int> MyImageUploaders)
        {
            if (tsddb.DropDownItems.Count == 0)
            {
                foreach (ImageUploaderType t in Enum.GetValues(typeof(ImageUploaderType)))
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(t.GetDescription());
                    tsmi.Tag = t;
                    tsmi.CheckOnClick = true;
                    tsmi.Checked = MyImageUploaders.Contains((int)t);
                    tsmi.Click += new EventHandler(tsmiDestImage_Click);
                    tsddb.DropDownItems.Add(tsmi);
                }
                UpdateToolStripDestImage();
            }
        }

        public void AddEnumDestTextToMenuWithConfigSettings()
        {
            AddEnumDestTextToMenuWithRuntimeSettings(ucDestOptions.tsddDestText, Engine.conf.MyTextUploaders);
        }

        public void AddEnumDestTextToMenuWithRuntimeSettings(List<int> MyTextUploaders)
        {
            AddEnumDestTextToMenuWithRuntimeSettings(ucDestOptions.tsddDestText, MyTextUploaders);
        }

        private void AddEnumDestTextToMenuWithRuntimeSettings(ToolStripDropDownButton tsddb, List<int> MyTextUploaders)
        {
            if (tsddb.DropDownItems.Count == 0)
            {
                foreach (TextUploaderType ut in Enum.GetValues(typeof(TextUploaderType)))
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(ut.GetDescription());
                    tsmi.Tag = ut;
                    tsmi.CheckOnClick = true;
                    tsmi.Checked = MyTextUploaders.Contains((int)ut);
                    tsmi.Click += new EventHandler(tsmiDestText_Click);
                    tsddb.DropDownItems.Add(tsmi);
                }
                UpdateToolStripDestText();
            }
        }

        public void AddEnumDestFileToMenuWithConfigSettings()
        {
            AddEnumDestFileToMenuWithRuntimeSettings(ucDestOptions.tsddDestFile, Engine.conf.MyFileUploaders);
        }

        public void AddEnumDestFileToMenuWithRuntimeSettings(List<int> MyFileUploaders)
        {
            AddEnumDestFileToMenuWithRuntimeSettings(ucDestOptions.tsddDestFile, MyFileUploaders);
        }

        private void AddEnumDestFileToMenuWithRuntimeSettings(ToolStripDropDownButton tsddb, List<int> MyFileUploaders)
        {
            if (tsddb.DropDownItems.Count == 0)
            {
                foreach (FileUploaderType ut in Enum.GetValues(typeof(FileUploaderType)))
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(ut.GetDescription());
                    tsmi.Tag = ut;
                    tsmi.CheckOnClick = true;
                    tsmi.Checked = MyFileUploaders.Contains((int)ut);
                    tsmi.Click += new EventHandler(tsmiDestFiles_Click);
                    tsddb.DropDownItems.Add(tsmi);
                }
                UpdateToolStripDestFile();
            }
        }

        public void AddEnumDestLinkToMenuWithConfigSettings()
        {
            AddEnumDestLinkToMenuWithRuntimeSettings(ucDestOptions.tsddbDestLink, Engine.conf.MyURLShorteners);
        }

        public void AddEnumDestLinkToMenuWithRuntimeSettings(List<int> MyLinkUploaders)
        {
            AddEnumDestLinkToMenuWithRuntimeSettings(ucDestOptions.tsddbDestLink, MyLinkUploaders);
        }

        private void AddEnumDestLinkToMenuWithRuntimeSettings(ToolStripDropDownButton tsddb, List<int> MyLinkUploaders)
        {
            if (tsddb.DropDownItems.Count == 0)
            {
                foreach (UrlShortenerType ut in Enum.GetValues(typeof(UrlShortenerType)))
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(ut.GetDescription());
                    tsmi.Tag = ut;
                    tsmi.Checked = MyLinkUploaders.Contains((int)ut);
                    tsmi.Click += new EventHandler(tsmiDestLinks_Click);
                    tsddb.DropDownItems.Add(tsmi);
                }
                UpdateToolStripDestLink();
            }
        }

        private void UpdateToolStripDest(ToolStripDropDownButton tsdd, string descr)
        {
            List<string> dest = new List<string>();

            foreach (var obj in tsdd.DropDownItems)
            {
                if (obj.GetType() == typeof(ToolStripMenuItem))
                {
                    ToolStripMenuItem gtsmi = obj as ToolStripMenuItem;
                    if (gtsmi.Checked)
                    {
                        dest.Add(((Enum)gtsmi.Tag).GetDescription());
                    }
                }
            }

            if (dest.Count == 0)
            {
                tsdd.Text = descr + ": None";
            }
            else if (dest.Count == 1)
            {
                tsdd.Text = descr + ": " + dest[0];
            }
            else if (dest.Count == 2)
            {
                tsdd.Text = descr + ": " + dest[0] + " and " + dest[1];
            }
            else if (dest.Count == 3)
            {
                tsdd.Text = string.Format("{0}: {1}, {2} and {3}", descr, dest[0], dest[1], dest[2]);
            }
            else if (dest.Count > 3)
            {
                tsdd.Text = string.Format("{0}: {1}, {2} and {3} more", descr, dest[0], dest[1], dest.Count - 2);
            }
        }

        private void UpdateToolStripDestImage()
        {
            UpdateToolStripDest(ucDestOptions.tsddbDestImage, "Image output");
        }

        private void UpdateToolStripDestText()
        {
            UpdateToolStripDest(ucDestOptions.tsddDestText, "Text output");
        }

        private void UpdateToolStripDestFile()
        {
            UpdateToolStripDest(ucDestOptions.tsddDestFile, "File output");
        }

        private void UpdateToolStripDestLink()
        {
            UpdateToolStripDest(ucDestOptions.tsddbDestLink, "URL shortener");
        }

        private void UpdateToolStripClipboardContent()
        {
            UpdateToolStripDest(ucDestOptions.tsddbClipboardContent, "Clipboard content");
        }

        private void UpdateToolStripLinkFormat()
        {
            UpdateToolStripDest(ucDestOptions.tsddbLinkFormat, "URL format");
        }

        private void UpdateToolStripOutputs()
        {
            UpdateToolStripDest(ucDestOptions.tsddbOutputs, "Outputs");
        }

        private void tsmiDestImage_Click(object sender, EventArgs e)
        {
            UpdateToolStripDestImage();
        }

        private void tsmiDestText_Click(object sender, EventArgs e)
        {
            UpdateToolStripDestText();
        }

        private void tsmiDestFiles_Click(object sender, EventArgs e)
        {
            UpdateToolStripDestFile();
        }

        private void tsmiDestLinks_Click(object sender, EventArgs e)
        {
            UpdateToolStripDestLink();
        }

        private void tsmiDestClipboardContent_Click(object sender, EventArgs e)
        {
            UpdateToolStripClipboardContent();
        }

        private void tsmiDestLinkFormat_Click(object sender, EventArgs e)
        {
            UpdateToolStripLinkFormat();
        }

        private void tsmiOutputs_Click(object sender, EventArgs e)
        {
            ucDestOptions.EnableDisableDestControls();
            UpdateToolStripOutputs();
        }
    }
}