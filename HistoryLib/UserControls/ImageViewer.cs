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
using System.Drawing;
using System.Windows.Forms;
using HelpersLib;

namespace HistoryLib.Custom_Controls
{
    public class ImageViewer : Form
    {
        private Image screenshot;

        public ImageViewer(string path)
        {
            this.screenshot = Image.FromFile(path);
            InitializeComponent();
        }

        public ImageViewer(Image image)
        {
            this.screenshot = (Image)image.Clone();
            InitializeComponent();
        }

        private void ShowScreenshot_Load(object sender, EventArgs e)
        {
            if (this.Bounds.Width > this.BackgroundImage.Width && this.Bounds.Height > this.BackgroundImage.Height)
            {
                this.BackgroundImageLayout = ImageLayout.Center;
            }
            else
            {
                this.BackgroundImageLayout = ImageLayout.Zoom;
            }
        }

        private void ShowScreenshot_MouseDown(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void ShowScreenshot_Shown(object sender, EventArgs e)
        {
            this.BringToFront();
            this.Activate();
        }

        private void ShowScreenshot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                this.Close();
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            screenshot.Dispose();
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.BackColor = Color.FloralWhite;
            this.BackgroundImage = screenshot;
            this.Bounds = ZAppHelper.GetScreenBounds();
            this.Cursor = Cursors.Hand;
            this.DoubleBuffered = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Text = "Image Viewer";
            this.WindowState = FormWindowState.Maximized;

            this.Deactivate += new System.EventHandler(this.ShowScreenshot_Deactivate);
            this.Load += new System.EventHandler(this.ShowScreenshot_Load);
            this.Shown += new System.EventHandler(this.ShowScreenshot_Shown);
            this.Leave += new System.EventHandler(this.ShowScreenshot_Leave);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ShowScreenshot_MouseDown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ShowScreenshot_KeyDown);

            this.ResumeLayout(false);
        }

        #endregion Windows Form Designer generated code

        private void ShowScreenshot_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowScreenshot_Leave(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}