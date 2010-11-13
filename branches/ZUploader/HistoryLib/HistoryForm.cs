﻿#region License Information (GPL v2)

/*
    ZUploader - A program that allows you to upload images, texts or files
    Copyright (C) 2010 ZScreen Developers

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
using System.Windows.Forms;

namespace HistoryLib
{
    public partial class HistoryForm : Form
    {
        public string DatabasePath { get; private set; }

        private HistoryManager history;
        private HistoryItemManager him;

        public HistoryForm(string databasePath)
        {
            InitializeComponent();
            DatabasePath = databasePath;
        }

        private void HistoryForm_Shown(object sender, EventArgs e)
        {
            RefreshHistoryItems();
        }

        private void HistoryForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (history != null) history.Dispose();
        }

        private void RefreshHistoryItems()
        {
            if (history == null)
            {
                history = new HistoryManager(DatabasePath);
            }

            HistoryItem[] historyItems = history.GetHistoryItems();
            AddHistoryItems(historyItems);
        }

        private void AddHistoryItems(HistoryItem[] historyItems)
        {
            lvHistory.SuspendLayout();

            lvHistory.Items.Clear();

            foreach (HistoryItem hi in historyItems)
            {
                ListViewItem lvi = new ListViewItem(hi.DateTimeLocalString);
                lvi.SubItems.Add(hi.Filename);
                lvi.SubItems.Add(hi.Type);
                lvi.SubItems.Add(hi.Host);
                lvi.SubItems.Add(hi.URL);
                lvi.Tag = hi;
                lvHistory.Items.Add(lvi);
            }

            lvHistory.ResumeLayout(true);
        }

        private void cmsHistory_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel =  !UpdateHistoryMenu();
        }

        private bool UpdateHistoryMenu()
        {
            HistoryItem hi = GetSelectedHistoryItem();

            if (hi != null)
            {
                him = new HistoryItemManager(hi);

                // Open
                tsmiOpenURL.Enabled = him.IsURLExist;
                tsmiOpenThumbnailURL.Enabled = him.IsThumbnailURLExist;
                tsmiOpenDeletionURL.Enabled = him.IsDeletionURLExist;

                tsmiOpenFile.Enabled = him.IsFileExist;
                tsmiOpenFolder.Enabled = him.IsFolderExist;

                // Copy
                tsmiCopyURL.Enabled = him.IsURLExist;
                tsmiCopyThumbnailURL.Enabled = him.IsThumbnailURLExist;
                tsmiCopyDeletionURL.Enabled = him.IsDeletionURLExist;

                tsmiCopyFile.Enabled = him.IsFileExist;
                tsmiCopyImage.Enabled = him.IsImageFile;
                tsmiCopyText.Enabled = him.IsTextFile;

                tsmiCopyHTMLLink.Enabled = him.IsURLExist;
                tsmiCopyHTMLImage.Enabled = him.IsURLExist;
                tsmiCopyHTMLLinkedImage.Enabled = him.IsURLExist && him.IsThumbnailURLExist;

                tsmiCopyForumLink.Enabled = him.IsURLExist;
                tsmiCopyForumImage.Enabled =  him.IsURLExist;
                tsmiCopyForumLinkedImage.Enabled =  him.IsURLExist && him.IsThumbnailURLExist;

                tsmiCopyFilePath.Enabled = him.IsFilePathValid;
                tsmiCopyFileName.Enabled =  him.IsFilePathValid;
                tsmiCopyFileNameWithExtension.Enabled = him.IsFilePathValid;
                tsmiCopyFolder.Enabled =  him.IsFilePathValid;

                // Delete
                tsmiDeleteLocalFile.Enabled = him.IsFileExist;

                return true;
            }

            return false;
        }

        private HistoryItem GetSelectedHistoryItem()
        {
            if (lvHistory.SelectedItems.Count > 0)
            {
                return lvHistory.SelectedItems[0].Tag as HistoryItem;
            }

            return null;
        }

        private void RemoveSelectedHistoryItem()
        {
            if (lvHistory.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lvHistory.SelectedItems[0];
                HistoryItem hi = lvi.Tag as HistoryItem;

                if (hi != null)
                {
                    history.RemoveHistoryItem(hi);
                    lvHistory.Items.Remove(lvi);
                }
            }
        }

        #region Right click menu events

        private void tsmiOpenURL_Click(object sender, EventArgs e)
        {
            him.OpenURL();
        }

        private void tsmiOpenThumbnailURL_Click(object sender, EventArgs e)
        {
            him.OpenThumbnailURL();
        }

        private void tsmiOpenDeletionURL_Click(object sender, EventArgs e)
        {
            him.OpenDeletionURL();
        }

        private void tsmiOpenFile_Click(object sender, EventArgs e)
        {
            him.OpenFile();
        }

        private void tsmiOpenFolder_Click(object sender, EventArgs e)
        {
            him.OpenFolder();
        }

        private void tsmiCopyURL_Click(object sender, EventArgs e)
        {
            him.CopyURL();
        }

        private void tsmiCopyThumbnailURL_Click(object sender, EventArgs e)
        {
            him.CopyThumbnailURL();
        }

        private void tsmiCopyDeletionURL_Click(object sender, EventArgs e)
        {
            him.CopyDeletionURL();
        }

        private void tsmiCopyFile_Click(object sender, EventArgs e)
        {
            him.CopyFile();
        }

        private void tsmiCopyImage_Click(object sender, EventArgs e)
        {
            him.CopyImage();
        }

        private void tsmiCopyText_Click(object sender, EventArgs e)
        {
            him.CopyText();
        }

        private void tsmiCopyHTMLLink_Click(object sender, EventArgs e)
        {
            him.CopyHTMLLink();
        }

        private void tsmiCopyHTMLImage_Click(object sender, EventArgs e)
        {
            him.CopyHTMLImage();
        }

        private void tsmiCopyHTMLLinkedImage_Click(object sender, EventArgs e)
        {
            him.CopyHTMLLinkedImage();
        }

        private void tsmiCopyForumLink_Click(object sender, EventArgs e)
        {
            him.CopyForumLink();
        }

        private void tsmiCopyForumImage_Click(object sender, EventArgs e)
        {
            him.CopyForumImage();
        }

        private void tsmiCopyForumLinkedImage_Click(object sender, EventArgs e)
        {
            him.CopyForumLinkedImage();
        }

        private void tsmiCopyFilePath_Click(object sender, EventArgs e)
        {
            him.CopyFilePath();
        }

        private void tsmiCopyFileName_Click(object sender, EventArgs e)
        {
            him.CopyFileName();
        }

        private void tsmiCopyFileNameWithExtension_Click(object sender, EventArgs e)
        {
            him.CopyFileNameWithExtension();
        }

        private void tsmiCopyFolder_Click(object sender, EventArgs e)
        {
            him.CopyFolder();
        }

        private void tsmiDeleteFromHistory_Click(object sender, EventArgs e)
        {
            RemoveSelectedHistoryItem();
        }

        private void tsmiDeleteLocalFile_Click(object sender, EventArgs e)
        {
            him.DeleteLocalFile();
        }

        private void tsmiDeleteFromHistoryAndLocalFile_Click(object sender, EventArgs e)
        {
            RemoveSelectedHistoryItem();
            him.DeleteLocalFile();
        }

        private void tsmiRefresh_Click(object sender, EventArgs e)
        {
            RefreshHistoryItems();
        }

        #endregion Right click menu events
    }
}