﻿namespace ZScreenTesterGUI
{
    partial class TesterGUI
    {
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lvUploaders = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.cmsUploaders = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.testSelectedUploadersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openURLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tcTesters = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnTest = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.cmsUploaders.SuspendLayout();
            this.tcTesters.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvUploaders
            // 
            this.lvUploaders.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvUploaders.ContextMenuStrip = this.cmsUploaders;
            this.lvUploaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvUploaders.FullRowSelect = true;
            this.lvUploaders.GridLines = true;
            this.lvUploaders.Location = new System.Drawing.Point(3, 3);
            this.lvUploaders.Name = "lvUploaders";
            this.lvUploaders.Size = new System.Drawing.Size(604, 574);
            this.lvUploaders.TabIndex = 0;
            this.lvUploaders.UseCompatibleStateImageBehavior = false;
            this.lvUploaders.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 180;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Status";
            this.columnHeader2.Width = 415;
            // 
            // cmsUploaders
            // 
            this.cmsUploaders.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testSelectedUploadersToolStripMenuItem,
            this.openURLToolStripMenuItem,
            this.copyToolStripMenuItem});
            this.cmsUploaders.Name = "cmsUploaders";
            this.cmsUploaders.Size = new System.Drawing.Size(207, 70);
            // 
            // testSelectedUploadersToolStripMenuItem
            // 
            this.testSelectedUploadersToolStripMenuItem.Name = "testSelectedUploadersToolStripMenuItem";
            this.testSelectedUploadersToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.testSelectedUploadersToolStripMenuItem.Text = "Test selected uploaders";
            this.testSelectedUploadersToolStripMenuItem.Click += new System.EventHandler(this.testSelectedUploadersToolStripMenuItem_Click);
            // 
            // openURLToolStripMenuItem
            // 
            this.openURLToolStripMenuItem.Name = "openURLToolStripMenuItem";
            this.openURLToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.openURLToolStripMenuItem.Text = "Open URL";
            this.openURLToolStripMenuItem.Click += new System.EventHandler(this.openURLToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.copyToolStripMenuItem.Text = "Copy URL(s) to clipboard";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // tcTesters
            // 
            this.tcTesters.Controls.Add(this.tabPage1);
            this.tcTesters.Controls.Add(this.tabPage2);
            this.tcTesters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcTesters.Location = new System.Drawing.Point(3, 3);
            this.tcTesters.Name = "tcTesters";
            this.tcTesters.SelectedIndex = 0;
            this.tcTesters.Size = new System.Drawing.Size(618, 606);
            this.tcTesters.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnTest);
            this.tabPage1.Controls.Add(this.lvUploaders);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(610, 580);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Testers";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(512, 4);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(79, 23);
            this.btnTest.TabIndex = 1;
            this.btnTest.Text = "Test all";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtConsole);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(635, 416);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Console";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtConsole
            // 
            this.txtConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConsole.Location = new System.Drawing.Point(3, 3);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.Size = new System.Drawing.Size(629, 410);
            this.txtConsole.TabIndex = 0;
            // 
            // TesterGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 612);
            this.Controls.Add(this.tcTesters);
            this.Name = "TesterGUI";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "ZScreen Tester";
            this.Load += new System.EventHandler(this.TesterGUI_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TesterGUI_FormClosing);
            this.cmsUploaders.ResumeLayout(false);
            this.tcTesters.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvUploaders;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TabControl tcTesters;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.ContextMenuStrip cmsUploaders;
        private System.Windows.Forms.ToolStripMenuItem openURLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.TextBox txtConsole;
        private System.Windows.Forms.ToolStripMenuItem testSelectedUploadersToolStripMenuItem;
    }
}