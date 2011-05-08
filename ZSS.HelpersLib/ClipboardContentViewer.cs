﻿using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HelpersLib
{
    public partial class ClipboardContentViewer : Form
    {
        public bool IsClipboardEmpty { get; private set; }
        public bool DontShowThisWindow { get; private set; }

        public ClipboardContentViewer()
        {
            InitializeComponent();
        }

        private void ClipboardContentViewer_Load(object sender, EventArgs e)
        {
            pbClipboard.Visible = txtClipboard.Visible = lbClipboard.Visible = false;

            if (Clipboard.ContainsImage())
            {
                Image img = Clipboard.GetImage();
                pbClipboard.Image = img;
                lblQuestion.Text = string.Format("Content type: Bitmap (Image), Size: {0}x{1}", img.Width, img.Height);
                pbClipboard.Visible = true;
            }
            else if (Clipboard.ContainsText())
            {
                string text = Clipboard.GetText();
                lblQuestion.Text = "Content type: Text, Length: " + text.Length;
                txtClipboard.Text = text;
                txtClipboard.Visible = true;
            }
            else if (Clipboard.ContainsFileDropList())
            {
                string[] files = Clipboard.GetFileDropList().OfType<string>().ToArray();
                lblQuestion.Text = "Content type: File, Count: " + files.Length;
                lbClipboard.Items.AddRange(files);
                lbClipboard.Visible = true;
            }
            else
            {
                lblQuestion.Text = "Clipboard is empty or contains unknown data.";
                IsClipboardEmpty = true;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void cbDontShowThisWindow_CheckedChanged(object sender, EventArgs e)
        {
            DontShowThisWindow = cbDontShowThisWindow.Checked;
        }
    }
}