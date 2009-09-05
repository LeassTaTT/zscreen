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
using System.Windows.Forms;
using UploadersLib;

namespace ZScreenLib
{
    public partial class DestOptions : Form
    {
        public string Title { get; set; }
        public string InputText { get; set; }
        public WorkerTask Task { get; set; }

        public DestOptions() { }

        public DestOptions(WorkerTask task)
        {
            InitializeComponent();
            this.Task = task;
        }

        private void InputBox_Load(object sender, EventArgs e)
        {
            this.Text = Title;
            txtInputText.Text = InputText;

            // File Uploaders
            if (ucDestOptions.cboFileUploaders.Items.Count == 0)
            {
                ucDestOptions.cboFileUploaders.Items.AddRange(typeof(FileUploaderType).GetDescriptions());
            }
            ucDestOptions.cboFileUploaders.SelectedIndex = (int)Engine.conf.FileDestMode;

            // Image Uploaders
            if (ucDestOptions.cboImageUploaders.Items.Count == 0)
            {
                ucDestOptions.cboImageUploaders.Items.AddRange(typeof(ImageDestType).GetDescriptions());
            }
            ucDestOptions.cboImageUploaders.SelectedIndex = (int)Engine.conf.ImageUploaderType;

            // Text Uploaders
            foreach (TextUploader textUploader in Engine.conf.TextUploadersList)
            {
                if (textUploader != null)
                {
                    ucDestOptions.cboTextUploaders.Items.Add(textUploader);
                }
            }
            if (Adapter.CheckList(Engine.conf.TextUploadersList, Engine.conf.TextUploaderSelected))
            {
                ucDestOptions.cboTextUploaders.SelectedIndex = Engine.conf.TextUploaderSelected;
            }
            ucDestOptions.cboTextUploaders.Enabled = !Engine.conf.PreferFileUploaderForText;

            // URL Shorteners
            foreach (TextUploader textUploader in Engine.conf.UrlShortenersList)
            {
                if (textUploader != null)
                {
                    ucDestOptions.cboURLShorteners.Items.Add(textUploader);
                }
            }
            if (Adapter.CheckList(Engine.conf.UrlShortenersList, Engine.conf.UrlShortenerSelected))
            {
                ucDestOptions.cboURLShorteners.SelectedIndex = Engine.conf.UrlShortenerSelected;
            }

            // Dest Selector Events
            ucDestOptions.cboFileUploaders.SelectedIndexChanged += new EventHandler(cboFileUploaders_SelectedIndexChanged);
            ucDestOptions.cboImageUploaders.SelectedIndexChanged += new EventHandler(cboImageUploaders_SelectedIndexChanged);
            ucDestOptions.cboTextUploaders.SelectedIndexChanged += new EventHandler(cboTextUploaders_SelectedIndexChanged);
            ucDestOptions.cboURLShorteners.SelectedIndexChanged += new EventHandler(cboURLShorteners_SelectedIndexChanged);
        }

        void cboURLShorteners_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Adapter.CheckList(Engine.conf.UrlShortenersList, ucDestOptions.cboURLShorteners.SelectedIndex))
            {
                Task.MyTextUploader = Engine.conf.UrlShortenersList[ucDestOptions.cboURLShorteners.SelectedIndex];
            }
        }

        void cboTextUploaders_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Adapter.CheckList(Engine.conf.TextUploadersList, ucDestOptions.cboTextUploaders.SelectedIndex))
            {
                Task.MyTextUploader = Engine.conf.TextUploadersList[ucDestOptions.cboTextUploaders.SelectedIndex];
            }
        }

        private void cboFileUploaders_SelectedIndexChanged(object sender, EventArgs e)
        {
            Task.MyFileUploader = (FileUploaderType)ucDestOptions.cboFileUploaders.SelectedIndex;
        }

        private void cboImageUploaders_SelectedIndexChanged(object sender, EventArgs e)
        {
            ImageDestType sdt = (ImageDestType)ucDestOptions.cboImageUploaders.SelectedIndex;
            Task.MyImageUploader = sdt;
        }

        private void InputBox_Shown(object sender, EventArgs e)
        {
            this.BringToFront();
            txtInputText.Focus();
            txtInputText.SelectionLength = txtInputText.Text.Length;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtInputText.Text))
            {
                InputText = txtInputText.Text;
                this.DialogResult = DialogResult.OK;
                this.Hide();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Hide();
        }

        public DestSelector ucDestOptions;
        private GroupBox gbFileName;

        #region Windows Form Designer generated code

        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtInputText = new System.Windows.Forms.TextBox();
            this.ucDestOptions = new ZScreenLib.DestSelector();
            this.gbFileName = new System.Windows.Forms.GroupBox();
            this.gbFileName.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(224, 240);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(73, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(304, 240);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtInputText
            // 
            this.txtInputText.Location = new System.Drawing.Point(16, 24);
            this.txtInputText.Name = "txtInputText";
            this.txtInputText.Size = new System.Drawing.Size(328, 20);
            this.txtInputText.TabIndex = 2;
            // 
            // ucDestOptions
            // 
            this.ucDestOptions.Location = new System.Drawing.Point(8, 80);
            this.ucDestOptions.MaximumSize = new System.Drawing.Size(378, 145);
            this.ucDestOptions.Name = "ucDestOptions";
            this.ucDestOptions.Size = new System.Drawing.Size(378, 145);
            this.ucDestOptions.TabIndex = 3;
            // 
            // gbFileName
            // 
            this.gbFileName.Controls.Add(this.txtInputText);
            this.gbFileName.Location = new System.Drawing.Point(16, 16);
            this.gbFileName.Name = "gbFileName";
            this.gbFileName.Size = new System.Drawing.Size(360, 56);
            this.gbFileName.TabIndex = 4;
            this.gbFileName.TabStop = false;
            this.gbFileName.Text = "Specify File Name";
            // 
            // DestOptions
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 279);
            this.Controls.Add(this.gbFileName);
            this.Controls.Add(this.ucDestOptions);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DestOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Destination Options";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.InputBox_Load);
            this.Shown += new System.EventHandler(this.InputBox_Shown);
            this.gbFileName.ResumeLayout(false);
            this.gbFileName.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtInputText;

        #endregion
    }
}