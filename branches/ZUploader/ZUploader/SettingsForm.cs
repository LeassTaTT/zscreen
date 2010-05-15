﻿using System;
using System.Windows.Forms;

namespace ZUploader
{
    public partial class SettingsForm : Form
    {
        private bool loaded;

        public SettingsForm()
        {
            InitializeComponent();
            pgFTPSettings.SelectedObject = Program.Settings.FTPAccount;
            pgProxy.SelectedObject = Program.Settings.ProxySettings;
            cbClipboardAutoCopy.Checked = Program.Settings.ClipboardAutoCopy;
            cbAutoPlaySound.Checked = Program.Settings.AutoPlaySound;
            cbShellContextMenu.Checked = ShellContextMenu.Check();
        }

        private void FTPSettingsForm_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            loaded = true;
        }

        private void pgFTPSettings_SelectedObjectsChanged(object sender, EventArgs e)
        {
            pgFTPSettings.SelectedObject = Program.Settings.FTPAccount;
        }

        private void cbClipboardAutoCopy_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.ClipboardAutoCopy = cbClipboardAutoCopy.Checked;
        }

        private void cbAutoPlaySound_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.AutoPlaySound = cbAutoPlaySound.Checked;
        }

        private void cbShellContextMenu_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                if (cbShellContextMenu.Checked)
                {
                    ShellContextMenu.Register();
                }
                else
                {
                    ShellContextMenu.Unregister();
                }
            }
        }
    }
}