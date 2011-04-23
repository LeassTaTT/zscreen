﻿namespace HistoryLib
{
    partial class HistoryForm
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
            if (history != null) history.Dispose();

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HistoryForm));
            this.cmsHistory = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenURL = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenThumbnailURL = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenDeletionURL = new System.Windows.Forms.ToolStripMenuItem();
            this.tssOpen1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyURL = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyThumbnailURL = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyDeletionURL = new System.Windows.Forms.ToolStripMenuItem();
            this.tssCopy1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiCopyFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyText = new System.Windows.Forms.ToolStripMenuItem();
            this.tssCopy2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiCopyHTMLLink = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyHTMLImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyHTMLLinkedImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tssCopy3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiCopyForumLink = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyForumImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyForumLinkedImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tssCopy4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiCopyFilePath = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyFileName = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyFileNameWithExtension = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteFromHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteLocalFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteFromHistoryAndLocalFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMoreInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.dtpFilterFrom = new System.Windows.Forms.DateTimePicker();
            this.cbDateFilter = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFilterTo = new System.Windows.Forms.DateTimePicker();
            this.btnApplyFilters = new System.Windows.Forms.Button();
            this.txtFilenameFilter = new System.Windows.Forms.TextBox();
            this.cbFilenameFilterMethod = new System.Windows.Forms.ComboBox();
            this.cbFilenameFilterCulture = new System.Windows.Forms.ComboBox();
            this.cbFilenameFilter = new System.Windows.Forms.CheckBox();
            this.cbFilenameFilterCase = new System.Windows.Forms.CheckBox();
            this.ssMain = new System.Windows.Forms.StatusStrip();
            this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.gbFilters = new System.Windows.Forms.GroupBox();
            this.txtHostFilter = new System.Windows.Forms.TextBox();
            this.cbTypeFilterSelection = new System.Windows.Forms.ComboBox();
            this.cbHostFilter = new System.Windows.Forms.CheckBox();
            this.cbTypeFilter = new System.Windows.Forms.CheckBox();
            this.btnRemoveFilters = new System.Windows.Forms.Button();
            this.btnRefreshList = new System.Windows.Forms.Button();
            this.btnCopyURL = new System.Windows.Forms.Button();
            this.btnOpenURL = new System.Windows.Forms.Button();
            this.btnOpenLocalFile = new System.Windows.Forms.Button();
            this.lvHistory = new HelpersLib.Custom_Controls.MyListView();
            this.chDateTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chFilename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chHost = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chURL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbSelectedHistoryItem = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pbThumbnail = new HistoryLib.Custom_Controls.MyPictureBox();
            this.cmsHistory.SuspendLayout();
            this.ssMain.SuspendLayout();
            this.gbFilters.SuspendLayout();
            this.gbSelectedHistoryItem.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // cmsHistory
            //
            this.cmsHistory.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpen,
            this.tsmiCopy,
            this.tsmiDelete,
            this.tsmiMoreInfo});
            this.cmsHistory.Name = "cmsHistory";
            this.cmsHistory.ShowImageMargin = false;
            this.cmsHistory.Size = new System.Drawing.Size(102, 92);
            //
            // tsmiOpen
            //
            this.tsmiOpen.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpenURL,
            this.tsmiOpenThumbnailURL,
            this.tsmiOpenDeletionURL,
            this.tssOpen1,
            this.tsmiOpenFile,
            this.tsmiOpenFolder});
            this.tsmiOpen.Name = "tsmiOpen";
            this.tsmiOpen.Size = new System.Drawing.Size(101, 22);
            this.tsmiOpen.Text = "Open";
            //
            // tsmiOpenURL
            //
            this.tsmiOpenURL.Name = "tsmiOpenURL";
            this.tsmiOpenURL.Size = new System.Drawing.Size(156, 22);
            this.tsmiOpenURL.Text = "URL";
            this.tsmiOpenURL.Click += new System.EventHandler(this.tsmiOpenURL_Click);
            //
            // tsmiOpenThumbnailURL
            //
            this.tsmiOpenThumbnailURL.Name = "tsmiOpenThumbnailURL";
            this.tsmiOpenThumbnailURL.Size = new System.Drawing.Size(156, 22);
            this.tsmiOpenThumbnailURL.Text = "Thumbnail URL";
            this.tsmiOpenThumbnailURL.Click += new System.EventHandler(this.tsmiOpenThumbnailURL_Click);
            //
            // tsmiOpenDeletionURL
            //
            this.tsmiOpenDeletionURL.Name = "tsmiOpenDeletionURL";
            this.tsmiOpenDeletionURL.Size = new System.Drawing.Size(156, 22);
            this.tsmiOpenDeletionURL.Text = "Deletion URL";
            this.tsmiOpenDeletionURL.Click += new System.EventHandler(this.tsmiOpenDeletionURL_Click);
            //
            // tssOpen1
            //
            this.tssOpen1.Name = "tssOpen1";
            this.tssOpen1.Size = new System.Drawing.Size(153, 6);
            //
            // tsmiOpenFile
            //
            this.tsmiOpenFile.Name = "tsmiOpenFile";
            this.tsmiOpenFile.Size = new System.Drawing.Size(156, 22);
            this.tsmiOpenFile.Text = "File";
            this.tsmiOpenFile.Click += new System.EventHandler(this.tsmiOpenFile_Click);
            //
            // tsmiOpenFolder
            //
            this.tsmiOpenFolder.Name = "tsmiOpenFolder";
            this.tsmiOpenFolder.Size = new System.Drawing.Size(156, 22);
            this.tsmiOpenFolder.Text = "Folder";
            this.tsmiOpenFolder.Click += new System.EventHandler(this.tsmiOpenFolder_Click);
            //
            // tsmiCopy
            //
            this.tsmiCopy.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCopyURL,
            this.tsmiCopyThumbnailURL,
            this.tsmiCopyDeletionURL,
            this.tssCopy1,
            this.tsmiCopyFile,
            this.tsmiCopyImage,
            this.tsmiCopyText,
            this.tssCopy2,
            this.tsmiCopyHTMLLink,
            this.tsmiCopyHTMLImage,
            this.tsmiCopyHTMLLinkedImage,
            this.tssCopy3,
            this.tsmiCopyForumLink,
            this.tsmiCopyForumImage,
            this.tsmiCopyForumLinkedImage,
            this.tssCopy4,
            this.tsmiCopyFilePath,
            this.tsmiCopyFileName,
            this.tsmiCopyFileNameWithExtension,
            this.tsmiCopyFolder});
            this.tsmiCopy.Name = "tsmiCopy";
            this.tsmiCopy.Size = new System.Drawing.Size(101, 22);
            this.tsmiCopy.Text = "Copy";
            //
            // tsmiCopyURL
            //
            this.tsmiCopyURL.Name = "tsmiCopyURL";
            this.tsmiCopyURL.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyURL.Text = "URL";
            this.tsmiCopyURL.Click += new System.EventHandler(this.tsmiCopyURL_Click);
            //
            // tsmiCopyThumbnailURL
            //
            this.tsmiCopyThumbnailURL.Name = "tsmiCopyThumbnailURL";
            this.tsmiCopyThumbnailURL.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyThumbnailURL.Text = "Thumbnail URL";
            this.tsmiCopyThumbnailURL.Click += new System.EventHandler(this.tsmiCopyThumbnailURL_Click);
            //
            // tsmiCopyDeletionURL
            //
            this.tsmiCopyDeletionURL.Name = "tsmiCopyDeletionURL";
            this.tsmiCopyDeletionURL.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyDeletionURL.Text = "Deletion URL";
            this.tsmiCopyDeletionURL.Click += new System.EventHandler(this.tsmiCopyDeletionURL_Click);
            //
            // tssCopy1
            //
            this.tssCopy1.Name = "tssCopy1";
            this.tssCopy1.Size = new System.Drawing.Size(230, 6);
            //
            // tsmiCopyFile
            //
            this.tsmiCopyFile.Name = "tsmiCopyFile";
            this.tsmiCopyFile.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyFile.Text = "File";
            this.tsmiCopyFile.Click += new System.EventHandler(this.tsmiCopyFile_Click);
            //
            // tsmiCopyImage
            //
            this.tsmiCopyImage.Name = "tsmiCopyImage";
            this.tsmiCopyImage.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyImage.Text = "Image (Bitmap)";
            this.tsmiCopyImage.Click += new System.EventHandler(this.tsmiCopyImage_Click);
            //
            // tsmiCopyText
            //
            this.tsmiCopyText.Name = "tsmiCopyText";
            this.tsmiCopyText.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyText.Text = "Text";
            this.tsmiCopyText.Click += new System.EventHandler(this.tsmiCopyText_Click);
            //
            // tssCopy2
            //
            this.tssCopy2.Name = "tssCopy2";
            this.tssCopy2.Size = new System.Drawing.Size(230, 6);
            //
            // tsmiCopyHTMLLink
            //
            this.tsmiCopyHTMLLink.Name = "tsmiCopyHTMLLink";
            this.tsmiCopyHTMLLink.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyHTMLLink.Text = "HTML link";
            this.tsmiCopyHTMLLink.Click += new System.EventHandler(this.tsmiCopyHTMLLink_Click);
            //
            // tsmiCopyHTMLImage
            //
            this.tsmiCopyHTMLImage.Name = "tsmiCopyHTMLImage";
            this.tsmiCopyHTMLImage.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyHTMLImage.Text = "HTML image";
            this.tsmiCopyHTMLImage.Click += new System.EventHandler(this.tsmiCopyHTMLImage_Click);
            //
            // tsmiCopyHTMLLinkedImage
            //
            this.tsmiCopyHTMLLinkedImage.Name = "tsmiCopyHTMLLinkedImage";
            this.tsmiCopyHTMLLinkedImage.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyHTMLLinkedImage.Text = "HTML linked image";
            this.tsmiCopyHTMLLinkedImage.Click += new System.EventHandler(this.tsmiCopyHTMLLinkedImage_Click);
            //
            // tssCopy3
            //
            this.tssCopy3.Name = "tssCopy3";
            this.tssCopy3.Size = new System.Drawing.Size(230, 6);
            //
            // tsmiCopyForumLink
            //
            this.tsmiCopyForumLink.Name = "tsmiCopyForumLink";
            this.tsmiCopyForumLink.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyForumLink.Text = "Forum (BBCode) link";
            this.tsmiCopyForumLink.Click += new System.EventHandler(this.tsmiCopyForumLink_Click);
            //
            // tsmiCopyForumImage
            //
            this.tsmiCopyForumImage.Name = "tsmiCopyForumImage";
            this.tsmiCopyForumImage.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyForumImage.Text = "Forum (BBCode) image";
            this.tsmiCopyForumImage.Click += new System.EventHandler(this.tsmiCopyForumImage_Click);
            //
            // tsmiCopyForumLinkedImage
            //
            this.tsmiCopyForumLinkedImage.Name = "tsmiCopyForumLinkedImage";
            this.tsmiCopyForumLinkedImage.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyForumLinkedImage.Text = "Forum (BBCode) linked image";
            this.tsmiCopyForumLinkedImage.Click += new System.EventHandler(this.tsmiCopyForumLinkedImage_Click);
            //
            // tssCopy4
            //
            this.tssCopy4.Name = "tssCopy4";
            this.tssCopy4.Size = new System.Drawing.Size(230, 6);
            //
            // tsmiCopyFilePath
            //
            this.tsmiCopyFilePath.Name = "tsmiCopyFilePath";
            this.tsmiCopyFilePath.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyFilePath.Text = "File path";
            this.tsmiCopyFilePath.Click += new System.EventHandler(this.tsmiCopyFilePath_Click);
            //
            // tsmiCopyFileName
            //
            this.tsmiCopyFileName.Name = "tsmiCopyFileName";
            this.tsmiCopyFileName.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyFileName.Text = "File name";
            this.tsmiCopyFileName.Click += new System.EventHandler(this.tsmiCopyFileName_Click);
            //
            // tsmiCopyFileNameWithExtension
            //
            this.tsmiCopyFileNameWithExtension.Name = "tsmiCopyFileNameWithExtension";
            this.tsmiCopyFileNameWithExtension.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyFileNameWithExtension.Text = "File name with extension";
            this.tsmiCopyFileNameWithExtension.Click += new System.EventHandler(this.tsmiCopyFileNameWithExtension_Click);
            //
            // tsmiCopyFolder
            //
            this.tsmiCopyFolder.Name = "tsmiCopyFolder";
            this.tsmiCopyFolder.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyFolder.Text = "Folder";
            this.tsmiCopyFolder.Click += new System.EventHandler(this.tsmiCopyFolder_Click);
            //
            // tsmiDelete
            //
            this.tsmiDelete.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDeleteFromHistory,
            this.tsmiDeleteLocalFile,
            this.tsmiDeleteFromHistoryAndLocalFile});
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(101, 22);
            this.tsmiDelete.Text = "Delete";
            //
            // tsmiDeleteFromHistory
            //
            this.tsmiDeleteFromHistory.Name = "tsmiDeleteFromHistory";
            this.tsmiDeleteFromHistory.Size = new System.Drawing.Size(220, 22);
            this.tsmiDeleteFromHistory.Text = "From history";
            this.tsmiDeleteFromHistory.Click += new System.EventHandler(this.tsmiDeleteFromHistory_Click);
            //
            // tsmiDeleteLocalFile
            //
            this.tsmiDeleteLocalFile.Name = "tsmiDeleteLocalFile";
            this.tsmiDeleteLocalFile.Size = new System.Drawing.Size(220, 22);
            this.tsmiDeleteLocalFile.Text = "Local file...";
            this.tsmiDeleteLocalFile.Click += new System.EventHandler(this.tsmiDeleteLocalFile_Click);
            //
            // tsmiDeleteFromHistoryAndLocalFile
            //
            this.tsmiDeleteFromHistoryAndLocalFile.Name = "tsmiDeleteFromHistoryAndLocalFile";
            this.tsmiDeleteFromHistoryAndLocalFile.Size = new System.Drawing.Size(220, 22);
            this.tsmiDeleteFromHistoryAndLocalFile.Text = "From history and local file...";
            this.tsmiDeleteFromHistoryAndLocalFile.Click += new System.EventHandler(this.tsmiDeleteFromHistoryAndLocalFile_Click);
            //
            // tsmiMoreInfo
            //
            this.tsmiMoreInfo.Name = "tsmiMoreInfo";
            this.tsmiMoreInfo.Size = new System.Drawing.Size(101, 22);
            this.tsmiMoreInfo.Text = "More info";
            this.tsmiMoreInfo.Click += new System.EventHandler(this.tsmiMoreInfo_Click);
            //
            // dtpFilterFrom
            //
            this.dtpFilterFrom.Location = new System.Drawing.Point(56, 46);
            this.dtpFilterFrom.Name = "dtpFilterFrom";
            this.dtpFilterFrom.Size = new System.Drawing.Size(239, 20);
            this.dtpFilterFrom.TabIndex = 1;
            //
            // cbDateFilter
            //
            this.cbDateFilter.AutoSize = true;
            this.cbDateFilter.Location = new System.Drawing.Point(16, 24);
            this.cbDateFilter.Name = "cbDateFilter";
            this.cbDateFilter.Size = new System.Drawing.Size(74, 17);
            this.cbDateFilter.TabIndex = 2;
            this.cbDateFilter.Text = "Date filter:";
            this.cbDateFilter.UseVisualStyleBackColor = true;
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "From:";
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "To:";
            //
            // dtpFilterTo
            //
            this.dtpFilterTo.Location = new System.Drawing.Point(56, 70);
            this.dtpFilterTo.Name = "dtpFilterTo";
            this.dtpFilterTo.Size = new System.Drawing.Size(239, 20);
            this.dtpFilterTo.TabIndex = 5;
            //
            // btnApplyFilters
            //
            this.btnApplyFilters.Location = new System.Drawing.Point(8, 48);
            this.btnApplyFilters.Name = "btnApplyFilters";
            this.btnApplyFilters.Size = new System.Drawing.Size(136, 24);
            this.btnApplyFilters.TabIndex = 7;
            this.btnApplyFilters.Text = "Apply filters";
            this.btnApplyFilters.UseVisualStyleBackColor = true;
            this.btnApplyFilters.Click += new System.EventHandler(this.btnApplyFilters_Click);
            //
            // txtFilenameFilter
            //
            this.txtFilenameFilter.Location = new System.Drawing.Point(16, 120);
            this.txtFilenameFilter.Name = "txtFilenameFilter";
            this.txtFilenameFilter.Size = new System.Drawing.Size(176, 20);
            this.txtFilenameFilter.TabIndex = 8;
            //
            // cbFilenameFilterMethod
            //
            this.cbFilenameFilterMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilenameFilterMethod.FormattingEnabled = true;
            this.cbFilenameFilterMethod.Items.AddRange(new object[] {
            "Contains",
            "Starts with",
            "Exact match"});
            this.cbFilenameFilterMethod.Location = new System.Drawing.Point(200, 120);
            this.cbFilenameFilterMethod.Name = "cbFilenameFilterMethod";
            this.cbFilenameFilterMethod.Size = new System.Drawing.Size(96, 21);
            this.cbFilenameFilterMethod.TabIndex = 10;
            //
            // cbFilenameFilterCulture
            //
            this.cbFilenameFilterCulture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilenameFilterCulture.FormattingEnabled = true;
            this.cbFilenameFilterCulture.Items.AddRange(new object[] {
            "Current culture",
            "Invariant culture (English)",
            "Ordinal (English)"});
            this.cbFilenameFilterCulture.Location = new System.Drawing.Point(16, 144);
            this.cbFilenameFilterCulture.Name = "cbFilenameFilterCulture";
            this.cbFilenameFilterCulture.Size = new System.Drawing.Size(176, 21);
            this.cbFilenameFilterCulture.TabIndex = 11;
            //
            // cbFilenameFilter
            //
            this.cbFilenameFilter.AutoSize = true;
            this.cbFilenameFilter.Location = new System.Drawing.Point(16, 96);
            this.cbFilenameFilter.Name = "cbFilenameFilter";
            this.cbFilenameFilter.Size = new System.Drawing.Size(93, 17);
            this.cbFilenameFilter.TabIndex = 12;
            this.cbFilenameFilter.Text = "Filename filter:";
            this.cbFilenameFilter.UseVisualStyleBackColor = true;
            //
            // cbFilenameFilterCase
            //
            this.cbFilenameFilterCase.AutoSize = true;
            this.cbFilenameFilterCase.Location = new System.Drawing.Point(200, 146);
            this.cbFilenameFilterCase.Name = "cbFilenameFilterCase";
            this.cbFilenameFilterCase.Size = new System.Drawing.Size(94, 17);
            this.cbFilenameFilterCase.TabIndex = 13;
            this.cbFilenameFilterCase.Text = "Case sensitive";
            this.cbFilenameFilterCase.UseVisualStyleBackColor = true;
            //
            // ssMain
            //
            this.ssMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslStatus});
            this.ssMain.Location = new System.Drawing.Point(0, 612);
            this.ssMain.Name = "ssMain";
            this.ssMain.Size = new System.Drawing.Size(825, 22);
            this.ssMain.TabIndex = 15;
            this.ssMain.Text = "statusStrip1";
            //
            // tsslStatus
            //
            this.tsslStatus.Name = "tsslStatus";
            this.tsslStatus.Size = new System.Drawing.Size(39, 17);
            this.tsslStatus.Text = "Status";
            //
            // gbFilters
            //
            this.gbFilters.Controls.Add(this.txtHostFilter);
            this.gbFilters.Controls.Add(this.cbTypeFilterSelection);
            this.gbFilters.Controls.Add(this.cbHostFilter);
            this.gbFilters.Controls.Add(this.cbTypeFilter);
            this.gbFilters.Controls.Add(this.dtpFilterFrom);
            this.gbFilters.Controls.Add(this.label1);
            this.gbFilters.Controls.Add(this.cbFilenameFilter);
            this.gbFilters.Controls.Add(this.label2);
            this.gbFilters.Controls.Add(this.cbFilenameFilterCase);
            this.gbFilters.Controls.Add(this.cbDateFilter);
            this.gbFilters.Controls.Add(this.dtpFilterTo);
            this.gbFilters.Controls.Add(this.cbFilenameFilterCulture);
            this.gbFilters.Controls.Add(this.txtFilenameFilter);
            this.gbFilters.Controls.Add(this.cbFilenameFilterMethod);
            this.gbFilters.Location = new System.Drawing.Point(168, 8);
            this.gbFilters.Name = "gbFilters";
            this.gbFilters.Size = new System.Drawing.Size(312, 232);
            this.gbFilters.TabIndex = 16;
            this.gbFilters.TabStop = false;
            this.gbFilters.Text = "Filters";
            //
            // txtHostFilter
            //
            this.txtHostFilter.Location = new System.Drawing.Point(112, 200);
            this.txtHostFilter.Name = "txtHostFilter";
            this.txtHostFilter.Size = new System.Drawing.Size(184, 20);
            this.txtHostFilter.TabIndex = 17;
            //
            // cbTypeFilterSelection
            //
            this.cbTypeFilterSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTypeFilterSelection.FormattingEnabled = true;
            this.cbTypeFilterSelection.Items.AddRange(new object[] {
            "Image",
            "File",
            "Text"});
            this.cbTypeFilterSelection.Location = new System.Drawing.Point(112, 174);
            this.cbTypeFilterSelection.Name = "cbTypeFilterSelection";
            this.cbTypeFilterSelection.Size = new System.Drawing.Size(96, 21);
            this.cbTypeFilterSelection.TabIndex = 16;
            //
            // cbHostFilter
            //
            this.cbHostFilter.AutoSize = true;
            this.cbHostFilter.Location = new System.Drawing.Point(16, 202);
            this.cbHostFilter.Name = "cbHostFilter";
            this.cbHostFilter.Size = new System.Drawing.Size(73, 17);
            this.cbHostFilter.TabIndex = 15;
            this.cbHostFilter.Text = "Host filter:";
            this.cbHostFilter.UseVisualStyleBackColor = true;
            //
            // cbTypeFilter
            //
            this.cbTypeFilter.AutoSize = true;
            this.cbTypeFilter.Location = new System.Drawing.Point(16, 176);
            this.cbTypeFilter.Name = "cbTypeFilter";
            this.cbTypeFilter.Size = new System.Drawing.Size(90, 17);
            this.cbTypeFilter.TabIndex = 14;
            this.cbTypeFilter.Text = "File type filter:";
            this.cbTypeFilter.UseVisualStyleBackColor = true;
            //
            // btnRemoveFilters
            //
            this.btnRemoveFilters.Location = new System.Drawing.Point(8, 72);
            this.btnRemoveFilters.Name = "btnRemoveFilters";
            this.btnRemoveFilters.Size = new System.Drawing.Size(136, 24);
            this.btnRemoveFilters.TabIndex = 17;
            this.btnRemoveFilters.Text = "Remove filters";
            this.btnRemoveFilters.UseVisualStyleBackColor = true;
            this.btnRemoveFilters.Click += new System.EventHandler(this.btnRemoveFilters_Click);
            //
            // btnRefreshList
            //
            this.btnRefreshList.Location = new System.Drawing.Point(8, 24);
            this.btnRefreshList.Name = "btnRefreshList";
            this.btnRefreshList.Size = new System.Drawing.Size(136, 24);
            this.btnRefreshList.TabIndex = 17;
            this.btnRefreshList.Text = "Refresh list";
            this.btnRefreshList.UseVisualStyleBackColor = true;
            this.btnRefreshList.Click += new System.EventHandler(this.btnRefreshList_Click);
            //
            // btnCopyURL
            //
            this.btnCopyURL.Location = new System.Drawing.Point(8, 24);
            this.btnCopyURL.Name = "btnCopyURL";
            this.btnCopyURL.Size = new System.Drawing.Size(136, 24);
            this.btnCopyURL.TabIndex = 18;
            this.btnCopyURL.Text = "Copy URL";
            this.btnCopyURL.UseVisualStyleBackColor = true;
            this.btnCopyURL.Click += new System.EventHandler(this.btnCopyURL_Click);
            //
            // btnOpenURL
            //
            this.btnOpenURL.Location = new System.Drawing.Point(8, 48);
            this.btnOpenURL.Name = "btnOpenURL";
            this.btnOpenURL.Size = new System.Drawing.Size(136, 24);
            this.btnOpenURL.TabIndex = 18;
            this.btnOpenURL.Text = "Open URL";
            this.btnOpenURL.UseVisualStyleBackColor = true;
            this.btnOpenURL.Click += new System.EventHandler(this.btnOpenURL_Click);
            //
            // btnOpenLocalFile
            //
            this.btnOpenLocalFile.Location = new System.Drawing.Point(8, 72);
            this.btnOpenLocalFile.Name = "btnOpenLocalFile";
            this.btnOpenLocalFile.Size = new System.Drawing.Size(136, 24);
            this.btnOpenLocalFile.TabIndex = 18;
            this.btnOpenLocalFile.Text = "Open local file";
            this.btnOpenLocalFile.UseVisualStyleBackColor = true;
            this.btnOpenLocalFile.Click += new System.EventHandler(this.btnOpenLocalFile_Click);
            //
            // lvHistory
            //
            this.lvHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDateTime,
            this.chFilename,
            this.chType,
            this.chHost,
            this.chURL});
            this.lvHistory.FullRowSelect = true;
            this.lvHistory.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvHistory.HideSelection = false;
            this.lvHistory.Location = new System.Drawing.Point(8, 248);
            this.lvHistory.Name = "lvHistory";
            this.lvHistory.Size = new System.Drawing.Size(808, 355);
            this.lvHistory.TabIndex = 0;
            this.lvHistory.UseCompatibleStateImageBehavior = false;
            this.lvHistory.View = System.Windows.Forms.View.Details;
            this.lvHistory.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvHistory_ItemSelectionChanged);
            this.lvHistory.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvHistory_MouseDoubleClick);
            this.lvHistory.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lvHistory_MouseUp);
            //
            // chDateTime
            //
            this.chDateTime.Text = "Date & time";
            this.chDateTime.Width = 122;
            //
            // chFilename
            //
            this.chFilename.Text = "Filename";
            this.chFilename.Width = 172;
            //
            // chType
            //
            this.chType.Text = "Type";
            this.chType.Width = 56;
            //
            // chHost
            //
            this.chHost.Text = "Host";
            this.chHost.Width = 95;
            //
            // chURL
            //
            this.chURL.Text = "URL";
            this.chURL.Width = 330;
            //
            // gbSelectedHistoryItem
            //
            this.gbSelectedHistoryItem.Controls.Add(this.btnCopyURL);
            this.gbSelectedHistoryItem.Controls.Add(this.btnOpenURL);
            this.gbSelectedHistoryItem.Controls.Add(this.btnOpenLocalFile);
            this.gbSelectedHistoryItem.Location = new System.Drawing.Point(8, 120);
            this.gbSelectedHistoryItem.Name = "gbSelectedHistoryItem";
            this.gbSelectedHistoryItem.Size = new System.Drawing.Size(152, 104);
            this.gbSelectedHistoryItem.TabIndex = 18;
            this.gbSelectedHistoryItem.TabStop = false;
            this.gbSelectedHistoryItem.Text = "Selected history item";
            //
            // groupBox1
            //
            this.groupBox1.Controls.Add(this.btnRefreshList);
            this.groupBox1.Controls.Add(this.btnApplyFilters);
            this.groupBox1.Controls.Add(this.btnRemoveFilters);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(152, 104);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General";
            //
            // pbThumbnail
            //
            this.pbThumbnail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbThumbnail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbThumbnail.Location = new System.Drawing.Point(488, 8);
            this.pbThumbnail.Name = "pbThumbnail";
            this.pbThumbnail.Size = new System.Drawing.Size(328, 232);
            this.pbThumbnail.TabIndex = 14;
            //
            // HistoryForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 634);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbSelectedHistoryItem);
            this.Controls.Add(this.ssMain);
            this.Controls.Add(this.gbFilters);
            this.Controls.Add(this.pbThumbnail);
            this.Controls.Add(this.lvHistory);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HistoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HistoryFormTest";
            this.Shown += new System.EventHandler(this.HistoryForm_Shown);
            this.cmsHistory.ResumeLayout(false);
            this.ssMain.ResumeLayout(false);
            this.ssMain.PerformLayout();
            this.gbFilters.ResumeLayout(false);
            this.gbFilters.PerformLayout();
            this.gbSelectedHistoryItem.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion Windows Form Designer generated code

        private HelpersLib.Custom_Controls.MyListView lvHistory;
        private System.Windows.Forms.ColumnHeader chFilename;
        private System.Windows.Forms.ColumnHeader chDateTime;
        private System.Windows.Forms.ColumnHeader chType;
        private System.Windows.Forms.ColumnHeader chHost;
        private System.Windows.Forms.ColumnHeader chURL;
        private System.Windows.Forms.ContextMenuStrip cmsHistory;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpen;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyURL;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyThumbnailURL;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyDeletionURL;
        private System.Windows.Forms.ToolStripSeparator tssCopy1;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyImage;
        private System.Windows.Forms.ToolStripSeparator tssCopy2;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyHTMLLink;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyHTMLImage;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyHTMLLinkedImage;
        private System.Windows.Forms.ToolStripSeparator tssCopy3;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyForumLink;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyForumImage;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyForumLinkedImage;
        private System.Windows.Forms.ToolStripSeparator tssCopy4;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyFilePath;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyFileName;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyFileNameWithExtension;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyFolder;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyText;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteFromHistory;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteLocalFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenURL;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenThumbnailURL;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenDeletionURL;
        private System.Windows.Forms.ToolStripSeparator tssOpen1;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenFolder;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteFromHistoryAndLocalFile;
        private System.Windows.Forms.DateTimePicker dtpFilterFrom;
        private System.Windows.Forms.CheckBox cbDateFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFilterTo;
        private System.Windows.Forms.Button btnApplyFilters;
        private System.Windows.Forms.ToolStripMenuItem tsmiMoreInfo;
        private System.Windows.Forms.TextBox txtFilenameFilter;
        private System.Windows.Forms.ComboBox cbFilenameFilterMethod;
        private System.Windows.Forms.ComboBox cbFilenameFilterCulture;
        private System.Windows.Forms.CheckBox cbFilenameFilter;
        private System.Windows.Forms.CheckBox cbFilenameFilterCase;
        private Custom_Controls.MyPictureBox pbThumbnail;
        private System.Windows.Forms.StatusStrip ssMain;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatus;
        private System.Windows.Forms.GroupBox gbFilters;
        private System.Windows.Forms.Button btnRemoveFilters;
        private System.Windows.Forms.Button btnRefreshList;
        private System.Windows.Forms.Button btnCopyURL;
        private System.Windows.Forms.Button btnOpenURL;
        private System.Windows.Forms.Button btnOpenLocalFile;
        private System.Windows.Forms.ComboBox cbTypeFilterSelection;
        private System.Windows.Forms.CheckBox cbHostFilter;
        private System.Windows.Forms.CheckBox cbTypeFilter;
        private System.Windows.Forms.TextBox txtHostFilter;
        private System.Windows.Forms.GroupBox gbSelectedHistoryItem;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}