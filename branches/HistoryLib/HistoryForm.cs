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
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using HelpersLib;

namespace HistoryLib
{
    public partial class HistoryForm : Form
    {
        public string DatabasePath { get; private set; }
        public int MaxItemCount { get; set; }

        private HistoryManager history;
        private HistoryItemManager him;
        private HistoryItem[] allHistoryItems;

        public HistoryForm(string databasePath, int maxItemCount, string title)
        {
            InitializeComponent();
            DatabasePath = databasePath;
            MaxItemCount = maxItemCount;
            this.Text = title;
            ResetControls();
            cbFilenameFilterMethod.SelectedIndex = 0; // Contains
            cbFilenameFilterCulture.SelectedIndex = 1; // Invariant culture
            cbTypeFilterSelection.SelectedIndex = 0; // Image
            cbFilenameFilterCulture.Items[0] = string.Format("Current culture ({0})", CultureInfo.CurrentCulture.Parent.EnglishName);
            pbThumbnail.LoadingImage = LoadImageFromResources("Loading.gif");
            lvHistory.AutoResizeLastColumn();
        }

        private Image LoadImageFromResources(string imageName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream("HistoryLib.Images." + imageName);
            return Image.FromStream(stream);
        }

        private void RefreshHistoryItems()
        {
            if (history == null)
            {
                history = new HistoryManager(DatabasePath);
            }

            allHistoryItems = GetHistoryItems();
            ApplyFiltersAndAdd();
        }

        private HistoryItem[] GetHistoryItems()
        {
            IEnumerable<HistoryItem> historyItems = history.GetHistoryItems().Reverse();

            if (MaxItemCount > -1)
            {
                historyItems = historyItems.Take(MaxItemCount);
            }

            return historyItems.ToArray();
        }

        private void ApplyFiltersAndAdd()
        {
            if (allHistoryItems.Length > 0)
            {
                AddHistoryItems(ApplyFilters(allHistoryItems));
            }
        }

        private HistoryItem[] ApplyFilters(HistoryItem[] historyItems)
        {
            IEnumerable<HistoryItem> result = historyItems.AsEnumerable();

            if (cbTypeFilter.Checked)
            {
                string type = cbTypeFilterSelection.Text;

                result = result.Where(x => x.Type == type);
            }

            if (cbHostFilter.Checked)
            {
                string host = txtHostFilter.Text;

                result = result.Where(x => x.Host.IndexOf(host, StringComparison.InvariantCultureIgnoreCase) >= 0);
            }

            string filenameFilter = txtFilenameFilter.Text;
            if (cbFilenameFilter.Checked && !string.IsNullOrEmpty(filenameFilter))
            {
                StringComparison rule = GetStringRule();

                if (cbFilenameFilterMethod.SelectedIndex == 0) // Contains
                {
                    result = result.Where(x => x.Filename.IndexOf(filenameFilter, rule) >= 0);
                }
                else if (cbFilenameFilterMethod.SelectedIndex == 1) // Starts with
                {
                    result = result.Where(x => x.Filename.StartsWith(filenameFilter, rule));
                }
                else if (cbFilenameFilterMethod.SelectedIndex == 2) // Exact match
                {
                    result = result.Where(x => x.Filename.Equals(filenameFilter, rule));
                }
            }

            if (cbDateFilter.Checked)
            {
                DateTime fromDate = dtpFilterFrom.Value.Date;
                DateTime toDate = dtpFilterTo.Value.Date;

                result = from hi in result
                         let date = FastDateTime.ToLocalTime(hi.DateTimeUtc).Date
                         where date >= fromDate && date <= toDate
                         select hi;
            }

            return result.ToArray();
        }

        private StringComparison GetStringRule()
        {
            bool caseSensitive = cbFilenameFilterCase.Checked;

            switch (cbFilenameFilterCulture.SelectedIndex)
            {
                case 0:
                    return caseSensitive ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase;
                case 1:
                    return caseSensitive ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase;
                case 3:
                    return caseSensitive ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase;
            }

            return StringComparison.InvariantCultureIgnoreCase;
        }

        private void AddHistoryItems(HistoryItem[] historyItems)
        {
            UpdateItemCount(historyItems);

            lvHistory.SuspendLayout();
            lvHistory.Items.Clear();

            HistoryItem hi;

            for (int i = 0; i < historyItems.Length; i++)
            {
                hi = historyItems[i];
                ListViewItem lvi = new ListViewItem(hi.DateTimeUtc.ToLocalTime().ToString());
                lvi.SubItems.Add(hi.Filename);
                lvi.SubItems.Add(hi.Type);
                lvi.SubItems.Add(hi.Host);
                lvi.SubItems.Add(hi.URL);
                lvi.Tag = hi;
                lvHistory.Items.Add(lvi);
            }

            lvHistory.AutoResizeLastColumn();
            lvHistory.Focus();
            lvHistory.ResumeLayout(true);
        }

        private void UpdateItemCount(HistoryItem[] historyItems)
        {
            StringBuilder status = new StringBuilder();

            status.Append("Total: " + allHistoryItems.Length);

            if (allHistoryItems.Length > historyItems.Length)
            {
                status.Append(", Filtered: " + historyItems.Length);
            }

            var types = from hi in historyItems
                        group hi by hi.Type into t
                        let count = t.Count()
                        orderby t.Key
                        select string.Format(", {0}: {1}", t.Key, count);

            foreach (string type in types)
            {
                status.Append(type);
            }

            tsslStatus.Text = status.ToString();
        }

        private bool UpdateControls()
        {
            HistoryItem hi = GetSelectedHistoryItem();

            if (hi != null)
            {
                him = new HistoryItemManager(hi);
                UpdateButtons();
                UpdateMenu();
                UpdatePictureBox();

                return true;
            }

            ResetControls();

            return false;
        }

        private void UpdateButtons()
        {
            btnCopyURL.Enabled = him.IsURLExist;
            btnOpenURL.Enabled = him.IsURLExist;
            btnOpenLocalFile.Enabled = him.IsFileExist;
        }

        private void UpdateMenu()
        {
            cmsHistory.Enabled = true;

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
            tsmiCopyHTMLImage.Enabled = him.IsImageURL;
            tsmiCopyHTMLLinkedImage.Enabled = him.IsImageURL && him.IsThumbnailURLExist;

            tsmiCopyForumLink.Enabled = him.IsURLExist;
            tsmiCopyForumImage.Enabled = him.IsImageURL && him.IsURLExist;
            tsmiCopyForumLinkedImage.Enabled =  him.IsImageURL && him.IsThumbnailURLExist;

            tsmiCopyFilePath.Enabled = him.IsFilePathValid;
            tsmiCopyFileName.Enabled =  him.IsFilePathValid;
            tsmiCopyFileNameWithExtension.Enabled = him.IsFilePathValid;
            tsmiCopyFolder.Enabled =  him.IsFilePathValid;

            // Delete
            tsmiDeleteLocalFile.Enabled = him.IsFileExist;
        }

        private void UpdatePictureBox()
        {
            pbThumbnail.Reset();

            if (him.HistoryItem.Type == "Image")
            {
                pbThumbnail.LoadImage(him.HistoryItem.Filepath, him.HistoryItem.URL);
            }
        }

        private void ResetControls()
        {
            // Buttons
            btnCopyURL.Enabled = false;
            btnOpenURL.Enabled = false;
            btnOpenLocalFile.Enabled = false;

            // Menu
            cmsHistory.Enabled = false;

            // PictureBox
            pbThumbnail.Reset();
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

        #region Form events

        private void HistoryForm_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            RefreshHistoryItems();
            this.BringToFront();
            this.Activate();
        }

        private void btnRefreshList_Click(object sender, EventArgs e)
        {
            RefreshHistoryItems();
        }

        private void btnCopyURL_Click(object sender, EventArgs e)
        {
            him.CopyURL();
        }

        private void btnOpenURL_Click(object sender, EventArgs e)
        {
            him.OpenURL();
        }

        private void btnOpenLocalFile_Click(object sender, EventArgs e)
        {
            him.OpenFile();
        }

        private void btnApplyFilters_Click(object sender, EventArgs e)
        {
            ApplyFiltersAndAdd();
        }

        private void btnRemoveFilters_Click(object sender, EventArgs e)
        {
            AddHistoryItems(allHistoryItems);
        }

        private void lvHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        private void lvHistory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (him != null)
            {
                him.OpenURL();
            }
        }

        #endregion Form events

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

        private void tsmiMoreInfo_Click(object sender, EventArgs e)
        {
            him.MoreInfo();
        }

        #endregion Right click menu events
    }
}