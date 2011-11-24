﻿namespace ZUploader
{
    partial class SettingsForm
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
            this.cbClipboardAutoCopy = new System.Windows.Forms.CheckBox();
            this.cbAutoPlaySound = new System.Windows.Forms.CheckBox();
            this.pgProxy = new System.Windows.Forms.PropertyGrid();
            this.tcSettings = new System.Windows.Forms.TabControl();
            this.tpGeneral = new System.Windows.Forms.TabPage();
            this.lblGeneralSeparator = new System.Windows.Forms.Label();
            this.cbStartWithWindows = new System.Windows.Forms.CheckBox();
            this.cbShowTray = new System.Windows.Forms.CheckBox();
            this.cbURLShortenAfterUpload = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOpenZUploaderPath = new System.Windows.Forms.Button();
            this.lblGeneralInfo = new System.Windows.Forms.Label();
            this.cbShellContextMenu = new System.Windows.Forms.CheckBox();
            this.tpUpload = new System.Windows.Forms.TabPage();
            this.btnLoadUploadersConfig = new System.Windows.Forms.Button();
            this.btnBrowseCustomUploadersConfigPath = new System.Windows.Forms.Button();
            this.txtCustomUploadersConfigPath = new System.Windows.Forms.TextBox();
            this.cbUseCustomUploadersConfigPath = new System.Windows.Forms.CheckBox();
            this.lblUploadLimitHint = new System.Windows.Forms.Label();
            this.nudUploadLimit = new System.Windows.Forms.NumericUpDown();
            this.lblUploadLimit = new System.Windows.Forms.Label();
            this.lblBufferSize = new System.Windows.Forms.Label();
            this.lblBufferSizeInfo = new System.Windows.Forms.Label();
            this.cbBufferSize = new System.Windows.Forms.ComboBox();
            this.tpHotkeys = new System.Windows.Forms.TabPage();
            this.hmHotkeys = new HelpersLib.Hotkey.HotkeyManagerControl();
            this.tpImage = new System.Windows.Forms.TabPage();
            this.lblImageInfo = new System.Windows.Forms.Label();
            this.lblUseImageFormat2AfterHint = new System.Windows.Forms.Label();
            this.lblImageJPEGQualityHint = new System.Windows.Forms.Label();
            this.cbImageGIFQuality = new System.Windows.Forms.ComboBox();
            this.cbImageFormat2 = new System.Windows.Forms.ComboBox();
            this.lblImageFormat2 = new System.Windows.Forms.Label();
            this.lblUseImageFormat2After = new System.Windows.Forms.Label();
            this.nudUseImageFormat2After = new System.Windows.Forms.NumericUpDown();
            this.nudImageJPEGQuality = new System.Windows.Forms.NumericUpDown();
            this.lblImageGIFQuality = new System.Windows.Forms.Label();
            this.lblImageJPEGQuality = new System.Windows.Forms.Label();
            this.cbImageFormat = new System.Windows.Forms.ComboBox();
            this.lblImageFormat = new System.Windows.Forms.Label();
            this.tpClipboardUpload = new System.Windows.Forms.TabPage();
            this.lblClipboardUploadInfo = new System.Windows.Forms.Label();
            this.lblNameFormatPatternPreview = new System.Windows.Forms.Label();
            this.lblNameFormatPattern = new System.Windows.Forms.Label();
            this.btnNameFormatPatternHelp = new System.Windows.Forms.Button();
            this.txtNameFormatPattern = new System.Windows.Forms.TextBox();
            this.tpCapture = new System.Windows.Forms.TabPage();
            this.cbCaptureTransparent = new System.Windows.Forms.CheckBox();
            this.gbSurfaceSettings = new System.Windows.Forms.GroupBox();
            this.lblFixedShapeSizeHeight = new System.Windows.Forms.Label();
            this.lblFixedShapeSizeWidth = new System.Windows.Forms.Label();
            this.nudFixedShapeSizeHeight = new System.Windows.Forms.NumericUpDown();
            this.nudFixedShapeSizeWidth = new System.Windows.Forms.NumericUpDown();
            this.cbDrawBorder = new System.Windows.Forms.CheckBox();
            this.cbFixedShapeSize = new System.Windows.Forms.CheckBox();
            this.cbDrawCheckerboard = new System.Windows.Forms.CheckBox();
            this.cbQuickCrop = new System.Windows.Forms.CheckBox();
            this.cbShowCursor = new System.Windows.Forms.CheckBox();
            this.tpHistory = new System.Windows.Forms.TabPage();
            this.lblHistoryInfo = new System.Windows.Forms.Label();
            this.nudHistoryMaxItemCount = new System.Windows.Forms.NumericUpDown();
            this.lblHistoryMaxItemCount = new System.Windows.Forms.Label();
            this.btnBrowseCustomHistoryPath = new System.Windows.Forms.Button();
            this.txtCustomHistoryPath = new System.Windows.Forms.TextBox();
            this.cbUseCustomHistoryPath = new System.Windows.Forms.CheckBox();
            this.cbHistorySave = new System.Windows.Forms.CheckBox();
            this.tpProxy = new System.Windows.Forms.TabPage();
            this.btnAutofillProxy = new System.Windows.Forms.Button();
            this.tpDebug = new System.Windows.Forms.TabPage();
            this.txtDebugLog = new System.Windows.Forms.TextBox();
            this.tcSettings.SuspendLayout();
            this.tpGeneral.SuspendLayout();
            this.tpUpload.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUploadLimit)).BeginInit();
            this.tpHotkeys.SuspendLayout();
            this.tpImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUseImageFormat2After)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudImageJPEGQuality)).BeginInit();
            this.tpClipboardUpload.SuspendLayout();
            this.tpCapture.SuspendLayout();
            this.gbSurfaceSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFixedShapeSizeHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFixedShapeSizeWidth)).BeginInit();
            this.tpHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHistoryMaxItemCount)).BeginInit();
            this.tpProxy.SuspendLayout();
            this.tpDebug.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbClipboardAutoCopy
            // 
            this.cbClipboardAutoCopy.AutoSize = true;
            this.cbClipboardAutoCopy.Location = new System.Drawing.Point(16, 112);
            this.cbClipboardAutoCopy.Name = "cbClipboardAutoCopy";
            this.cbClipboardAutoCopy.Size = new System.Drawing.Size(254, 17);
            this.cbClipboardAutoCopy.TabIndex = 0;
            this.cbClipboardAutoCopy.Text = "Copy URL to clipboard after upload is completed";
            this.cbClipboardAutoCopy.UseVisualStyleBackColor = true;
            this.cbClipboardAutoCopy.CheckedChanged += new System.EventHandler(this.cbClipboardAutoCopy_CheckedChanged);
            // 
            // cbAutoPlaySound
            // 
            this.cbAutoPlaySound.AutoSize = true;
            this.cbAutoPlaySound.Location = new System.Drawing.Point(16, 160);
            this.cbAutoPlaySound.Name = "cbAutoPlaySound";
            this.cbAutoPlaySound.Size = new System.Drawing.Size(327, 17);
            this.cbAutoPlaySound.TabIndex = 1;
            this.cbAutoPlaySound.Text = "Play sound when capture is made and after upload is completed";
            this.cbAutoPlaySound.UseVisualStyleBackColor = true;
            this.cbAutoPlaySound.CheckedChanged += new System.EventHandler(this.cbAutoPlaySound_CheckedChanged);
            // 
            // pgProxy
            // 
            this.pgProxy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgProxy.Location = new System.Drawing.Point(5, 5);
            this.pgProxy.Name = "pgProxy";
            this.pgProxy.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pgProxy.Size = new System.Drawing.Size(513, 303);
            this.pgProxy.TabIndex = 0;
            this.pgProxy.ToolbarVisible = false;
            // 
            // tcSettings
            // 
            this.tcSettings.Controls.Add(this.tpGeneral);
            this.tcSettings.Controls.Add(this.tpUpload);
            this.tcSettings.Controls.Add(this.tpHotkeys);
            this.tcSettings.Controls.Add(this.tpImage);
            this.tcSettings.Controls.Add(this.tpClipboardUpload);
            this.tcSettings.Controls.Add(this.tpCapture);
            this.tcSettings.Controls.Add(this.tpHistory);
            this.tcSettings.Controls.Add(this.tpProxy);
            this.tcSettings.Controls.Add(this.tpDebug);
            this.tcSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcSettings.Location = new System.Drawing.Point(3, 3);
            this.tcSettings.Name = "tcSettings";
            this.tcSettings.SelectedIndex = 0;
            this.tcSettings.Size = new System.Drawing.Size(531, 355);
            this.tcSettings.TabIndex = 0;
            // 
            // tpGeneral
            // 
            this.tpGeneral.Controls.Add(this.lblGeneralSeparator);
            this.tpGeneral.Controls.Add(this.cbStartWithWindows);
            this.tpGeneral.Controls.Add(this.cbShowTray);
            this.tpGeneral.Controls.Add(this.cbURLShortenAfterUpload);
            this.tpGeneral.Controls.Add(this.label1);
            this.tpGeneral.Controls.Add(this.btnOpenZUploaderPath);
            this.tpGeneral.Controls.Add(this.lblGeneralInfo);
            this.tpGeneral.Controls.Add(this.cbShellContextMenu);
            this.tpGeneral.Controls.Add(this.cbClipboardAutoCopy);
            this.tpGeneral.Controls.Add(this.cbAutoPlaySound);
            this.tpGeneral.Location = new System.Drawing.Point(4, 22);
            this.tpGeneral.Name = "tpGeneral";
            this.tpGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tpGeneral.Size = new System.Drawing.Size(523, 329);
            this.tpGeneral.TabIndex = 0;
            this.tpGeneral.Text = "General";
            this.tpGeneral.UseVisualStyleBackColor = true;
            // 
            // lblGeneralSeparator
            // 
            this.lblGeneralSeparator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblGeneralSeparator.Location = new System.Drawing.Point(16, 96);
            this.lblGeneralSeparator.Name = "lblGeneralSeparator";
            this.lblGeneralSeparator.Size = new System.Drawing.Size(475, 2);
            this.lblGeneralSeparator.TabIndex = 9;
            this.lblGeneralSeparator.Text = "label2";
            // 
            // cbStartWithWindows
            // 
            this.cbStartWithWindows.AutoSize = true;
            this.cbStartWithWindows.Location = new System.Drawing.Point(16, 40);
            this.cbStartWithWindows.Name = "cbStartWithWindows";
            this.cbStartWithWindows.Size = new System.Drawing.Size(170, 17);
            this.cbStartWithWindows.TabIndex = 8;
            this.cbStartWithWindows.Text = "Start ZUploader with Windows";
            this.cbStartWithWindows.UseVisualStyleBackColor = true;
            this.cbStartWithWindows.CheckedChanged += new System.EventHandler(this.cbStartWithWindows_CheckedChanged);
            // 
            // cbShowTray
            // 
            this.cbShowTray.AutoSize = true;
            this.cbShowTray.Location = new System.Drawing.Point(16, 16);
            this.cbShowTray.Name = "cbShowTray";
            this.cbShowTray.Size = new System.Drawing.Size(149, 17);
            this.cbShowTray.TabIndex = 4;
            this.cbShowTray.Text = "Show ZUploader tray icon";
            this.cbShowTray.UseVisualStyleBackColor = true;
            this.cbShowTray.CheckedChanged += new System.EventHandler(this.cbShowTray_CheckedChanged);
            // 
            // cbURLShortenAfterUpload
            // 
            this.cbURLShortenAfterUpload.AutoSize = true;
            this.cbURLShortenAfterUpload.Location = new System.Drawing.Point(16, 136);
            this.cbURLShortenAfterUpload.Name = "cbURLShortenAfterUpload";
            this.cbURLShortenAfterUpload.Size = new System.Drawing.Size(240, 17);
            this.cbURLShortenAfterUpload.TabIndex = 2;
            this.cbURLShortenAfterUpload.Text = "Use URL Shortener after upload is completed";
            this.cbURLShortenAfterUpload.UseVisualStyleBackColor = true;
            this.cbURLShortenAfterUpload.CheckedChanged += new System.EventHandler(this.cbURLShortenAfterUpload_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 232);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "This folder has settings, history database and log files";
            // 
            // btnOpenZUploaderPath
            // 
            this.btnOpenZUploaderPath.Location = new System.Drawing.Point(16, 200);
            this.btnOpenZUploaderPath.Name = "btnOpenZUploaderPath";
            this.btnOpenZUploaderPath.Size = new System.Drawing.Size(176, 23);
            this.btnOpenZUploaderPath.TabIndex = 5;
            this.btnOpenZUploaderPath.Text = "Open ZUploader personal folder";
            this.btnOpenZUploaderPath.UseVisualStyleBackColor = true;
            this.btnOpenZUploaderPath.Click += new System.EventHandler(this.btnOpenZUploaderPath_Click);
            // 
            // lblGeneralInfo
            // 
            this.lblGeneralInfo.BackColor = System.Drawing.Color.DimGray;
            this.lblGeneralInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGeneralInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblGeneralInfo.ForeColor = System.Drawing.Color.White;
            this.lblGeneralInfo.Location = new System.Drawing.Point(3, 297);
            this.lblGeneralInfo.Name = "lblGeneralInfo";
            this.lblGeneralInfo.Size = new System.Drawing.Size(517, 29);
            this.lblGeneralInfo.TabIndex = 7;
            this.lblGeneralInfo.Text = "Shell context menu is Windows Explorer right click menu for files and folders.";
            this.lblGeneralInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbShellContextMenu
            // 
            this.cbShellContextMenu.AutoSize = true;
            this.cbShellContextMenu.Location = new System.Drawing.Point(16, 64);
            this.cbShellContextMenu.Name = "cbShellContextMenu";
            this.cbShellContextMenu.Size = new System.Drawing.Size(285, 17);
            this.cbShellContextMenu.TabIndex = 3;
            this.cbShellContextMenu.Text = "Show \"Upload using ZUploader\" in Shell context menu";
            this.cbShellContextMenu.UseVisualStyleBackColor = true;
            this.cbShellContextMenu.CheckedChanged += new System.EventHandler(this.cbShellContextMenu_CheckedChanged);
            // 
            // tpUpload
            // 
            this.tpUpload.Controls.Add(this.btnLoadUploadersConfig);
            this.tpUpload.Controls.Add(this.btnBrowseCustomUploadersConfigPath);
            this.tpUpload.Controls.Add(this.txtCustomUploadersConfigPath);
            this.tpUpload.Controls.Add(this.cbUseCustomUploadersConfigPath);
            this.tpUpload.Controls.Add(this.lblUploadLimitHint);
            this.tpUpload.Controls.Add(this.nudUploadLimit);
            this.tpUpload.Controls.Add(this.lblUploadLimit);
            this.tpUpload.Controls.Add(this.lblBufferSize);
            this.tpUpload.Controls.Add(this.lblBufferSizeInfo);
            this.tpUpload.Controls.Add(this.cbBufferSize);
            this.tpUpload.Location = new System.Drawing.Point(4, 22);
            this.tpUpload.Name = "tpUpload";
            this.tpUpload.Size = new System.Drawing.Size(523, 313);
            this.tpUpload.TabIndex = 1;
            this.tpUpload.Text = "Upload";
            this.tpUpload.UseVisualStyleBackColor = true;
            // 
            // btnLoadUploadersConfig
            // 
            this.btnLoadUploadersConfig.Location = new System.Drawing.Point(432, 38);
            this.btnLoadUploadersConfig.Name = "btnLoadUploadersConfig";
            this.btnLoadUploadersConfig.Size = new System.Drawing.Size(75, 23);
            this.btnLoadUploadersConfig.TabIndex = 3;
            this.btnLoadUploadersConfig.Text = "Load";
            this.btnLoadUploadersConfig.UseVisualStyleBackColor = true;
            this.btnLoadUploadersConfig.Click += new System.EventHandler(this.btnLoadUploadersConfig_Click);
            // 
            // btnBrowseCustomUploadersConfigPath
            // 
            this.btnBrowseCustomUploadersConfigPath.Location = new System.Drawing.Point(352, 38);
            this.btnBrowseCustomUploadersConfigPath.Name = "btnBrowseCustomUploadersConfigPath";
            this.btnBrowseCustomUploadersConfigPath.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseCustomUploadersConfigPath.TabIndex = 2;
            this.btnBrowseCustomUploadersConfigPath.Text = "Browse...";
            this.btnBrowseCustomUploadersConfigPath.UseVisualStyleBackColor = true;
            this.btnBrowseCustomUploadersConfigPath.Click += new System.EventHandler(this.btnBrowseCustomUploadersConfigPath_Click);
            // 
            // txtCustomUploadersConfigPath
            // 
            this.txtCustomUploadersConfigPath.Location = new System.Drawing.Point(16, 40);
            this.txtCustomUploadersConfigPath.Name = "txtCustomUploadersConfigPath";
            this.txtCustomUploadersConfigPath.Size = new System.Drawing.Size(328, 20);
            this.txtCustomUploadersConfigPath.TabIndex = 1;
            this.txtCustomUploadersConfigPath.TextChanged += new System.EventHandler(this.txtCustomUploadersConfigPath_TextChanged);
            // 
            // cbUseCustomUploadersConfigPath
            // 
            this.cbUseCustomUploadersConfigPath.AutoSize = true;
            this.cbUseCustomUploadersConfigPath.Location = new System.Drawing.Point(16, 16);
            this.cbUseCustomUploadersConfigPath.Name = "cbUseCustomUploadersConfigPath";
            this.cbUseCustomUploadersConfigPath.Size = new System.Drawing.Size(201, 17);
            this.cbUseCustomUploadersConfigPath.TabIndex = 0;
            this.cbUseCustomUploadersConfigPath.Text = "Use custom uploader config file path:";
            this.cbUseCustomUploadersConfigPath.UseVisualStyleBackColor = true;
            this.cbUseCustomUploadersConfigPath.CheckedChanged += new System.EventHandler(this.cbUseCustomUploadersConfigPath_CheckedChanged);
            // 
            // lblUploadLimitHint
            // 
            this.lblUploadLimitHint.AutoSize = true;
            this.lblUploadLimitHint.Location = new System.Drawing.Point(216, 80);
            this.lblUploadLimitHint.Name = "lblUploadLimitHint";
            this.lblUploadLimitHint.Size = new System.Drawing.Size(90, 13);
            this.lblUploadLimitHint.TabIndex = 6;
            this.lblUploadLimitHint.Text = "0 - 25 (0 disables)";
            // 
            // nudUploadLimit
            // 
            this.nudUploadLimit.Location = new System.Drawing.Point(152, 76);
            this.nudUploadLimit.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.nudUploadLimit.Name = "nudUploadLimit";
            this.nudUploadLimit.Size = new System.Drawing.Size(56, 20);
            this.nudUploadLimit.TabIndex = 5;
            this.nudUploadLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudUploadLimit.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudUploadLimit.ValueChanged += new System.EventHandler(this.nudUploadLimit_ValueChanged);
            // 
            // lblUploadLimit
            // 
            this.lblUploadLimit.AutoSize = true;
            this.lblUploadLimit.Location = new System.Drawing.Point(16, 80);
            this.lblUploadLimit.Name = "lblUploadLimit";
            this.lblUploadLimit.Size = new System.Drawing.Size(128, 13);
            this.lblUploadLimit.TabIndex = 4;
            this.lblUploadLimit.Text = "Simultaneous upload limit:";
            // 
            // lblBufferSize
            // 
            this.lblBufferSize.AutoSize = true;
            this.lblBufferSize.Location = new System.Drawing.Point(16, 112);
            this.lblBufferSize.Name = "lblBufferSize";
            this.lblBufferSize.Size = new System.Drawing.Size(59, 13);
            this.lblBufferSize.TabIndex = 7;
            this.lblBufferSize.Text = "Buffer size:";
            // 
            // lblBufferSizeInfo
            // 
            this.lblBufferSizeInfo.AutoSize = true;
            this.lblBufferSizeInfo.Location = new System.Drawing.Point(152, 112);
            this.lblBufferSizeInfo.Name = "lblBufferSizeInfo";
            this.lblBufferSizeInfo.Size = new System.Drawing.Size(23, 13);
            this.lblBufferSizeInfo.TabIndex = 9;
            this.lblBufferSizeInfo.Text = "KiB";
            // 
            // cbBufferSize
            // 
            this.cbBufferSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBufferSize.FormattingEnabled = true;
            this.cbBufferSize.Location = new System.Drawing.Point(80, 108);
            this.cbBufferSize.Name = "cbBufferSize";
            this.cbBufferSize.Size = new System.Drawing.Size(64, 21);
            this.cbBufferSize.TabIndex = 8;
            this.cbBufferSize.SelectedIndexChanged += new System.EventHandler(this.cbBufferSize_SelectedIndexChanged);
            // 
            // tpHotkeys
            // 
            this.tpHotkeys.Controls.Add(this.hmHotkeys);
            this.tpHotkeys.Location = new System.Drawing.Point(4, 22);
            this.tpHotkeys.Name = "tpHotkeys";
            this.tpHotkeys.Size = new System.Drawing.Size(523, 313);
            this.tpHotkeys.TabIndex = 2;
            this.tpHotkeys.Text = "Hotkeys";
            this.tpHotkeys.UseVisualStyleBackColor = true;
            // 
            // hmHotkeys
            // 
            this.hmHotkeys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hmHotkeys.Location = new System.Drawing.Point(0, 0);
            this.hmHotkeys.Name = "hmHotkeys";
            this.hmHotkeys.Size = new System.Drawing.Size(523, 313);
            this.hmHotkeys.TabIndex = 0;
            // 
            // tpImage
            // 
            this.tpImage.Controls.Add(this.lblImageInfo);
            this.tpImage.Controls.Add(this.lblUseImageFormat2AfterHint);
            this.tpImage.Controls.Add(this.lblImageJPEGQualityHint);
            this.tpImage.Controls.Add(this.cbImageGIFQuality);
            this.tpImage.Controls.Add(this.cbImageFormat2);
            this.tpImage.Controls.Add(this.lblImageFormat2);
            this.tpImage.Controls.Add(this.lblUseImageFormat2After);
            this.tpImage.Controls.Add(this.nudUseImageFormat2After);
            this.tpImage.Controls.Add(this.nudImageJPEGQuality);
            this.tpImage.Controls.Add(this.lblImageGIFQuality);
            this.tpImage.Controls.Add(this.lblImageJPEGQuality);
            this.tpImage.Controls.Add(this.cbImageFormat);
            this.tpImage.Controls.Add(this.lblImageFormat);
            this.tpImage.Location = new System.Drawing.Point(4, 22);
            this.tpImage.Name = "tpImage";
            this.tpImage.Padding = new System.Windows.Forms.Padding(3);
            this.tpImage.Size = new System.Drawing.Size(523, 313);
            this.tpImage.TabIndex = 3;
            this.tpImage.Text = "Image";
            this.tpImage.UseVisualStyleBackColor = true;
            // 
            // lblImageInfo
            // 
            this.lblImageInfo.BackColor = System.Drawing.Color.DimGray;
            this.lblImageInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblImageInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblImageInfo.ForeColor = System.Drawing.Color.White;
            this.lblImageInfo.Location = new System.Drawing.Point(3, 281);
            this.lblImageInfo.Name = "lblImageInfo";
            this.lblImageInfo.Size = new System.Drawing.Size(517, 29);
            this.lblImageInfo.TabIndex = 12;
            this.lblImageInfo.Text = "These settings are for clipboard upload. Images that are stored in clipboard are " +
    "added as bitmap.";
            this.lblImageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUseImageFormat2AfterHint
            // 
            this.lblUseImageFormat2AfterHint.AutoSize = true;
            this.lblUseImageFormat2AfterHint.Location = new System.Drawing.Point(288, 112);
            this.lblUseImageFormat2AfterHint.Name = "lblUseImageFormat2AfterHint";
            this.lblUseImageFormat2AfterHint.Size = new System.Drawing.Size(124, 13);
            this.lblUseImageFormat2AfterHint.TabIndex = 9;
            this.lblUseImageFormat2AfterHint.Text = "KiB  0 - 5000 (0 disables)";
            // 
            // lblImageJPEGQualityHint
            // 
            this.lblImageJPEGQualityHint.AutoSize = true;
            this.lblImageJPEGQualityHint.Location = new System.Drawing.Point(168, 48);
            this.lblImageJPEGQualityHint.Name = "lblImageJPEGQualityHint";
            this.lblImageJPEGQualityHint.Size = new System.Drawing.Size(40, 13);
            this.lblImageJPEGQualityHint.TabIndex = 4;
            this.lblImageJPEGQualityHint.Text = "0 - 100";
            // 
            // cbImageGIFQuality
            // 
            this.cbImageGIFQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbImageGIFQuality.FormattingEnabled = true;
            this.cbImageGIFQuality.Items.AddRange(new object[] {
            "Default (Fast)",
            "256 colors (8 bit)",
            "16 colors (4 bit)",
            "Grayscale"});
            this.cbImageGIFQuality.Location = new System.Drawing.Point(104, 76);
            this.cbImageGIFQuality.Name = "cbImageGIFQuality";
            this.cbImageGIFQuality.Size = new System.Drawing.Size(120, 21);
            this.cbImageGIFQuality.TabIndex = 6;
            this.cbImageGIFQuality.SelectedIndexChanged += new System.EventHandler(this.cbImageGIFQuality_SelectedIndexChanged);
            // 
            // cbImageFormat2
            // 
            this.cbImageFormat2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbImageFormat2.FormattingEnabled = true;
            this.cbImageFormat2.Items.AddRange(new object[] {
            "PNG",
            "JPEG",
            "GIF",
            "BMP",
            "TIFF"});
            this.cbImageFormat2.Location = new System.Drawing.Point(104, 140);
            this.cbImageFormat2.Name = "cbImageFormat2";
            this.cbImageFormat2.Size = new System.Drawing.Size(56, 21);
            this.cbImageFormat2.TabIndex = 11;
            this.cbImageFormat2.SelectedIndexChanged += new System.EventHandler(this.cbImageFormat2_SelectedIndexChanged);
            // 
            // lblImageFormat2
            // 
            this.lblImageFormat2.AutoSize = true;
            this.lblImageFormat2.Location = new System.Drawing.Point(16, 144);
            this.lblImageFormat2.Name = "lblImageFormat2";
            this.lblImageFormat2.Size = new System.Drawing.Size(80, 13);
            this.lblImageFormat2.TabIndex = 10;
            this.lblImageFormat2.Text = "Image format 2:";
            // 
            // lblUseImageFormat2After
            // 
            this.lblUseImageFormat2After.AutoSize = true;
            this.lblUseImageFormat2After.Location = new System.Drawing.Point(16, 112);
            this.lblUseImageFormat2After.Name = "lblUseImageFormat2After";
            this.lblUseImageFormat2After.Size = new System.Drawing.Size(198, 13);
            this.lblUseImageFormat2After.TabIndex = 7;
            this.lblUseImageFormat2After.Text = "Image size limit for use \"Image format 2\":";
            // 
            // nudUseImageFormat2After
            // 
            this.nudUseImageFormat2After.Location = new System.Drawing.Point(224, 108);
            this.nudUseImageFormat2After.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nudUseImageFormat2After.Name = "nudUseImageFormat2After";
            this.nudUseImageFormat2After.Size = new System.Drawing.Size(56, 20);
            this.nudUseImageFormat2After.TabIndex = 8;
            this.nudUseImageFormat2After.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudUseImageFormat2After.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nudUseImageFormat2After.ValueChanged += new System.EventHandler(this.nudUseImageFormat2After_ValueChanged);
            // 
            // nudImageJPEGQuality
            // 
            this.nudImageJPEGQuality.Location = new System.Drawing.Point(104, 44);
            this.nudImageJPEGQuality.Name = "nudImageJPEGQuality";
            this.nudImageJPEGQuality.Size = new System.Drawing.Size(56, 20);
            this.nudImageJPEGQuality.TabIndex = 3;
            this.nudImageJPEGQuality.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudImageJPEGQuality.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudImageJPEGQuality.ValueChanged += new System.EventHandler(this.nudImageJPEGQuality_ValueChanged);
            // 
            // lblImageGIFQuality
            // 
            this.lblImageGIFQuality.AutoSize = true;
            this.lblImageGIFQuality.Location = new System.Drawing.Point(16, 80);
            this.lblImageGIFQuality.Name = "lblImageGIFQuality";
            this.lblImageGIFQuality.Size = new System.Drawing.Size(60, 13);
            this.lblImageGIFQuality.TabIndex = 5;
            this.lblImageGIFQuality.Text = "GIF quality:";
            // 
            // lblImageJPEGQuality
            // 
            this.lblImageJPEGQuality.AutoSize = true;
            this.lblImageJPEGQuality.Location = new System.Drawing.Point(16, 48);
            this.lblImageJPEGQuality.Name = "lblImageJPEGQuality";
            this.lblImageJPEGQuality.Size = new System.Drawing.Size(70, 13);
            this.lblImageJPEGQuality.TabIndex = 2;
            this.lblImageJPEGQuality.Text = "JPEG quality:";
            // 
            // cbImageFormat
            // 
            this.cbImageFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbImageFormat.FormattingEnabled = true;
            this.cbImageFormat.Items.AddRange(new object[] {
            "PNG",
            "JPEG",
            "GIF",
            "BMP",
            "TIFF"});
            this.cbImageFormat.Location = new System.Drawing.Point(104, 12);
            this.cbImageFormat.Name = "cbImageFormat";
            this.cbImageFormat.Size = new System.Drawing.Size(56, 21);
            this.cbImageFormat.TabIndex = 1;
            this.cbImageFormat.SelectedIndexChanged += new System.EventHandler(this.cbImageFormat_SelectedIndexChanged);
            // 
            // lblImageFormat
            // 
            this.lblImageFormat.AutoSize = true;
            this.lblImageFormat.Location = new System.Drawing.Point(16, 16);
            this.lblImageFormat.Name = "lblImageFormat";
            this.lblImageFormat.Size = new System.Drawing.Size(71, 13);
            this.lblImageFormat.TabIndex = 0;
            this.lblImageFormat.Text = "Image format:";
            // 
            // tpClipboardUpload
            // 
            this.tpClipboardUpload.Controls.Add(this.lblClipboardUploadInfo);
            this.tpClipboardUpload.Controls.Add(this.lblNameFormatPatternPreview);
            this.tpClipboardUpload.Controls.Add(this.lblNameFormatPattern);
            this.tpClipboardUpload.Controls.Add(this.btnNameFormatPatternHelp);
            this.tpClipboardUpload.Controls.Add(this.txtNameFormatPattern);
            this.tpClipboardUpload.Location = new System.Drawing.Point(4, 22);
            this.tpClipboardUpload.Name = "tpClipboardUpload";
            this.tpClipboardUpload.Padding = new System.Windows.Forms.Padding(3);
            this.tpClipboardUpload.Size = new System.Drawing.Size(523, 313);
            this.tpClipboardUpload.TabIndex = 4;
            this.tpClipboardUpload.Text = "Clipboard upload";
            this.tpClipboardUpload.UseVisualStyleBackColor = true;
            // 
            // lblClipboardUploadInfo
            // 
            this.lblClipboardUploadInfo.BackColor = System.Drawing.Color.DimGray;
            this.lblClipboardUploadInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblClipboardUploadInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblClipboardUploadInfo.ForeColor = System.Drawing.Color.White;
            this.lblClipboardUploadInfo.Location = new System.Drawing.Point(3, 281);
            this.lblClipboardUploadInfo.Name = "lblClipboardUploadInfo";
            this.lblClipboardUploadInfo.Size = new System.Drawing.Size(517, 29);
            this.lblClipboardUploadInfo.TabIndex = 4;
            this.lblClipboardUploadInfo.Text = "Clipboard upload automatically detects the data type and selects the upload servi" +
    "ce accordingly.";
            this.lblClipboardUploadInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNameFormatPatternPreview
            // 
            this.lblNameFormatPatternPreview.AutoSize = true;
            this.lblNameFormatPatternPreview.Location = new System.Drawing.Point(16, 72);
            this.lblNameFormatPatternPreview.Name = "lblNameFormatPatternPreview";
            this.lblNameFormatPatternPreview.Size = new System.Drawing.Size(48, 13);
            this.lblNameFormatPatternPreview.TabIndex = 3;
            this.lblNameFormatPatternPreview.Text = "Preview:";
            // 
            // lblNameFormatPattern
            // 
            this.lblNameFormatPattern.AutoSize = true;
            this.lblNameFormatPattern.Location = new System.Drawing.Point(16, 16);
            this.lblNameFormatPattern.Name = "lblNameFormatPattern";
            this.lblNameFormatPattern.Size = new System.Drawing.Size(362, 13);
            this.lblNameFormatPattern.TabIndex = 0;
            this.lblNameFormatPattern.Text = "Clipboard upload name pattern for image and text (Not image file or text file):";
            // 
            // btnNameFormatPatternHelp
            // 
            this.btnNameFormatPatternHelp.Location = new System.Drawing.Point(440, 39);
            this.btnNameFormatPatternHelp.Name = "btnNameFormatPatternHelp";
            this.btnNameFormatPatternHelp.Size = new System.Drawing.Size(24, 23);
            this.btnNameFormatPatternHelp.TabIndex = 2;
            this.btnNameFormatPatternHelp.Text = "?";
            this.btnNameFormatPatternHelp.UseVisualStyleBackColor = true;
            this.btnNameFormatPatternHelp.Click += new System.EventHandler(this.btnNameFormatPatternHelp_Click);
            // 
            // txtNameFormatPattern
            // 
            this.txtNameFormatPattern.Location = new System.Drawing.Point(16, 40);
            this.txtNameFormatPattern.Name = "txtNameFormatPattern";
            this.txtNameFormatPattern.Size = new System.Drawing.Size(416, 20);
            this.txtNameFormatPattern.TabIndex = 1;
            this.txtNameFormatPattern.TextChanged += new System.EventHandler(this.txtNameFormatPattern_TextChanged);
            // 
            // tpCapture
            // 
            this.tpCapture.Controls.Add(this.cbCaptureTransparent);
            this.tpCapture.Controls.Add(this.gbSurfaceSettings);
            this.tpCapture.Controls.Add(this.cbShowCursor);
            this.tpCapture.Location = new System.Drawing.Point(4, 22);
            this.tpCapture.Name = "tpCapture";
            this.tpCapture.Padding = new System.Windows.Forms.Padding(5);
            this.tpCapture.Size = new System.Drawing.Size(523, 313);
            this.tpCapture.TabIndex = 5;
            this.tpCapture.Text = "Capture";
            this.tpCapture.UseVisualStyleBackColor = true;
            // 
            // cbCaptureTransparent
            // 
            this.cbCaptureTransparent.AutoSize = true;
            this.cbCaptureTransparent.Location = new System.Drawing.Point(16, 40);
            this.cbCaptureTransparent.Name = "cbCaptureTransparent";
            this.cbCaptureTransparent.Size = new System.Drawing.Size(256, 17);
            this.cbCaptureTransparent.TabIndex = 1;
            this.cbCaptureTransparent.Text = "Capture with transparency (For window captures)";
            this.cbCaptureTransparent.UseVisualStyleBackColor = true;
            this.cbCaptureTransparent.CheckedChanged += new System.EventHandler(this.cbCaptureTransparent_CheckedChanged);
            // 
            // gbSurfaceSettings
            // 
            this.gbSurfaceSettings.Controls.Add(this.lblFixedShapeSizeHeight);
            this.gbSurfaceSettings.Controls.Add(this.lblFixedShapeSizeWidth);
            this.gbSurfaceSettings.Controls.Add(this.nudFixedShapeSizeHeight);
            this.gbSurfaceSettings.Controls.Add(this.nudFixedShapeSizeWidth);
            this.gbSurfaceSettings.Controls.Add(this.cbDrawBorder);
            this.gbSurfaceSettings.Controls.Add(this.cbFixedShapeSize);
            this.gbSurfaceSettings.Controls.Add(this.cbDrawCheckerboard);
            this.gbSurfaceSettings.Controls.Add(this.cbQuickCrop);
            this.gbSurfaceSettings.Location = new System.Drawing.Point(16, 72);
            this.gbSurfaceSettings.Name = "gbSurfaceSettings";
            this.gbSurfaceSettings.Size = new System.Drawing.Size(488, 176);
            this.gbSurfaceSettings.TabIndex = 2;
            this.gbSurfaceSettings.TabStop = false;
            this.gbSurfaceSettings.Text = "Shape capture settings";
            // 
            // lblFixedShapeSizeHeight
            // 
            this.lblFixedShapeSizeHeight.AutoSize = true;
            this.lblFixedShapeSizeHeight.Location = new System.Drawing.Point(40, 148);
            this.lblFixedShapeSizeHeight.Name = "lblFixedShapeSizeHeight";
            this.lblFixedShapeSizeHeight.Size = new System.Drawing.Size(41, 13);
            this.lblFixedShapeSizeHeight.TabIndex = 6;
            this.lblFixedShapeSizeHeight.Text = "Height:";
            // 
            // lblFixedShapeSizeWidth
            // 
            this.lblFixedShapeSizeWidth.AutoSize = true;
            this.lblFixedShapeSizeWidth.Location = new System.Drawing.Point(40, 124);
            this.lblFixedShapeSizeWidth.Name = "lblFixedShapeSizeWidth";
            this.lblFixedShapeSizeWidth.Size = new System.Drawing.Size(38, 13);
            this.lblFixedShapeSizeWidth.TabIndex = 4;
            this.lblFixedShapeSizeWidth.Text = "Width:";
            // 
            // nudFixedShapeSizeHeight
            // 
            this.nudFixedShapeSizeHeight.Location = new System.Drawing.Point(96, 144);
            this.nudFixedShapeSizeHeight.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nudFixedShapeSizeHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudFixedShapeSizeHeight.Name = "nudFixedShapeSizeHeight";
            this.nudFixedShapeSizeHeight.Size = new System.Drawing.Size(56, 20);
            this.nudFixedShapeSizeHeight.TabIndex = 7;
            this.nudFixedShapeSizeHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudFixedShapeSizeHeight.ValueChanged += new System.EventHandler(this.nudFixedShapeSizeHeight_ValueChanged);
            // 
            // nudFixedShapeSizeWidth
            // 
            this.nudFixedShapeSizeWidth.Location = new System.Drawing.Point(96, 120);
            this.nudFixedShapeSizeWidth.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nudFixedShapeSizeWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudFixedShapeSizeWidth.Name = "nudFixedShapeSizeWidth";
            this.nudFixedShapeSizeWidth.Size = new System.Drawing.Size(56, 20);
            this.nudFixedShapeSizeWidth.TabIndex = 5;
            this.nudFixedShapeSizeWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudFixedShapeSizeWidth.ValueChanged += new System.EventHandler(this.nudFixedShapeSizeWidth_ValueChanged);
            // 
            // cbDrawBorder
            // 
            this.cbDrawBorder.AutoSize = true;
            this.cbDrawBorder.Location = new System.Drawing.Point(16, 24);
            this.cbDrawBorder.Name = "cbDrawBorder";
            this.cbDrawBorder.Size = new System.Drawing.Size(170, 17);
            this.cbDrawBorder.TabIndex = 0;
            this.cbDrawBorder.Text = "Draw border around the shape";
            this.cbDrawBorder.UseVisualStyleBackColor = true;
            this.cbDrawBorder.CheckedChanged += new System.EventHandler(this.cbDrawBorder_CheckedChanged);
            // 
            // cbFixedShapeSize
            // 
            this.cbFixedShapeSize.AutoSize = true;
            this.cbFixedShapeSize.Location = new System.Drawing.Point(16, 96);
            this.cbFixedShapeSize.Name = "cbFixedShapeSize";
            this.cbFixedShapeSize.Size = new System.Drawing.Size(107, 17);
            this.cbFixedShapeSize.TabIndex = 3;
            this.cbFixedShapeSize.Text = "Fixed shape size:";
            this.cbFixedShapeSize.UseVisualStyleBackColor = true;
            this.cbFixedShapeSize.CheckedChanged += new System.EventHandler(this.cbFixedShapeSize_CheckedChanged);
            // 
            // cbDrawCheckerboard
            // 
            this.cbDrawCheckerboard.AutoSize = true;
            this.cbDrawCheckerboard.Location = new System.Drawing.Point(16, 48);
            this.cbDrawCheckerboard.Name = "cbDrawCheckerboard";
            this.cbDrawCheckerboard.Size = new System.Drawing.Size(287, 17);
            this.cbDrawCheckerboard.TabIndex = 1;
            this.cbDrawCheckerboard.Text = "Draw checkerboard pattern replacing transparent areas";
            this.cbDrawCheckerboard.UseVisualStyleBackColor = true;
            this.cbDrawCheckerboard.CheckedChanged += new System.EventHandler(this.cbDrawCheckerboard_CheckedChanged);
            // 
            // cbQuickCrop
            // 
            this.cbQuickCrop.AutoSize = true;
            this.cbQuickCrop.Location = new System.Drawing.Point(16, 72);
            this.cbQuickCrop.Name = "cbQuickCrop";
            this.cbQuickCrop.Size = new System.Drawing.Size(455, 17);
            this.cbQuickCrop.TabIndex = 2;
            this.cbQuickCrop.Text = "Complete capture as soon as the mouse button is released, except when capturing p" +
    "olygon";
            this.cbQuickCrop.UseVisualStyleBackColor = true;
            this.cbQuickCrop.CheckedChanged += new System.EventHandler(this.cbQuickCrop_CheckedChanged);
            // 
            // cbShowCursor
            // 
            this.cbShowCursor.AutoSize = true;
            this.cbShowCursor.Location = new System.Drawing.Point(16, 16);
            this.cbShowCursor.Name = "cbShowCursor";
            this.cbShowCursor.Size = new System.Drawing.Size(156, 17);
            this.cbShowCursor.TabIndex = 0;
            this.cbShowCursor.Text = "Show cursor in screenshots";
            this.cbShowCursor.UseVisualStyleBackColor = true;
            this.cbShowCursor.CheckedChanged += new System.EventHandler(this.cbShowCursor_CheckedChanged);
            // 
            // tpHistory
            // 
            this.tpHistory.Controls.Add(this.lblHistoryInfo);
            this.tpHistory.Controls.Add(this.nudHistoryMaxItemCount);
            this.tpHistory.Controls.Add(this.lblHistoryMaxItemCount);
            this.tpHistory.Controls.Add(this.btnBrowseCustomHistoryPath);
            this.tpHistory.Controls.Add(this.txtCustomHistoryPath);
            this.tpHistory.Controls.Add(this.cbUseCustomHistoryPath);
            this.tpHistory.Controls.Add(this.cbHistorySave);
            this.tpHistory.Location = new System.Drawing.Point(4, 22);
            this.tpHistory.Name = "tpHistory";
            this.tpHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tpHistory.Size = new System.Drawing.Size(523, 313);
            this.tpHistory.TabIndex = 6;
            this.tpHistory.Text = "History";
            this.tpHistory.UseVisualStyleBackColor = true;
            // 
            // lblHistoryInfo
            // 
            this.lblHistoryInfo.BackColor = System.Drawing.Color.DimGray;
            this.lblHistoryInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHistoryInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblHistoryInfo.ForeColor = System.Drawing.Color.White;
            this.lblHistoryInfo.Location = new System.Drawing.Point(3, 281);
            this.lblHistoryInfo.Name = "lblHistoryInfo";
            this.lblHistoryInfo.Size = new System.Drawing.Size(517, 29);
            this.lblHistoryInfo.TabIndex = 6;
            this.lblHistoryInfo.Text = "ZUploader uses XML to store history items.";
            this.lblHistoryInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nudHistoryMaxItemCount
            // 
            this.nudHistoryMaxItemCount.Location = new System.Drawing.Point(216, 92);
            this.nudHistoryMaxItemCount.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudHistoryMaxItemCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nudHistoryMaxItemCount.Name = "nudHistoryMaxItemCount";
            this.nudHistoryMaxItemCount.Size = new System.Drawing.Size(80, 20);
            this.nudHistoryMaxItemCount.TabIndex = 5;
            this.nudHistoryMaxItemCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudHistoryMaxItemCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nudHistoryMaxItemCount.ValueChanged += new System.EventHandler(this.nudHistoryMaxItemCount_ValueChanged);
            // 
            // lblHistoryMaxItemCount
            // 
            this.lblHistoryMaxItemCount.AutoSize = true;
            this.lblHistoryMaxItemCount.Location = new System.Drawing.Point(16, 96);
            this.lblHistoryMaxItemCount.Name = "lblHistoryMaxItemCount";
            this.lblHistoryMaxItemCount.Size = new System.Drawing.Size(192, 13);
            this.lblHistoryMaxItemCount.TabIndex = 4;
            this.lblHistoryMaxItemCount.Text = "Max item count for filtering (-1 disables):";
            // 
            // btnBrowseCustomHistoryPath
            // 
            this.btnBrowseCustomHistoryPath.Location = new System.Drawing.Point(392, 62);
            this.btnBrowseCustomHistoryPath.Name = "btnBrowseCustomHistoryPath";
            this.btnBrowseCustomHistoryPath.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseCustomHistoryPath.TabIndex = 3;
            this.btnBrowseCustomHistoryPath.Text = "Browse...";
            this.btnBrowseCustomHistoryPath.UseVisualStyleBackColor = true;
            this.btnBrowseCustomHistoryPath.Click += new System.EventHandler(this.btnBrowseCustomHistoryPath_Click);
            // 
            // txtCustomHistoryPath
            // 
            this.txtCustomHistoryPath.Location = new System.Drawing.Point(16, 64);
            this.txtCustomHistoryPath.Name = "txtCustomHistoryPath";
            this.txtCustomHistoryPath.Size = new System.Drawing.Size(368, 20);
            this.txtCustomHistoryPath.TabIndex = 2;
            this.txtCustomHistoryPath.TextChanged += new System.EventHandler(this.txtCustomHistoryPath_TextChanged);
            // 
            // cbUseCustomHistoryPath
            // 
            this.cbUseCustomHistoryPath.AutoSize = true;
            this.cbUseCustomHistoryPath.Location = new System.Drawing.Point(16, 40);
            this.cbUseCustomHistoryPath.Name = "cbUseCustomHistoryPath";
            this.cbUseCustomHistoryPath.Size = new System.Drawing.Size(158, 17);
            this.cbUseCustomHistoryPath.TabIndex = 1;
            this.cbUseCustomHistoryPath.Text = "Use custom history file path:";
            this.cbUseCustomHistoryPath.UseVisualStyleBackColor = true;
            this.cbUseCustomHistoryPath.CheckedChanged += new System.EventHandler(this.cbUseCustomHistoryPath_CheckedChanged);
            // 
            // cbHistorySave
            // 
            this.cbHistorySave.AutoSize = true;
            this.cbHistorySave.Location = new System.Drawing.Point(16, 16);
            this.cbHistorySave.Name = "cbHistorySave";
            this.cbHistorySave.Size = new System.Drawing.Size(118, 17);
            this.cbHistorySave.TabIndex = 0;
            this.cbHistorySave.Text = "Enable history save";
            this.cbHistorySave.UseVisualStyleBackColor = true;
            this.cbHistorySave.CheckedChanged += new System.EventHandler(this.cbHistorySave_CheckedChanged);
            // 
            // tpProxy
            // 
            this.tpProxy.Controls.Add(this.btnAutofillProxy);
            this.tpProxy.Controls.Add(this.pgProxy);
            this.tpProxy.Location = new System.Drawing.Point(4, 22);
            this.tpProxy.Name = "tpProxy";
            this.tpProxy.Padding = new System.Windows.Forms.Padding(5);
            this.tpProxy.Size = new System.Drawing.Size(523, 313);
            this.tpProxy.TabIndex = 7;
            this.tpProxy.Text = "Proxy";
            this.tpProxy.UseVisualStyleBackColor = true;
            // 
            // btnAutofillProxy
            // 
            this.btnAutofillProxy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAutofillProxy.Location = new System.Drawing.Point(432, 272);
            this.btnAutofillProxy.Name = "btnAutofillProxy";
            this.btnAutofillProxy.Size = new System.Drawing.Size(75, 23);
            this.btnAutofillProxy.TabIndex = 1;
            this.btnAutofillProxy.Text = "Autofill";
            this.btnAutofillProxy.UseVisualStyleBackColor = true;
            this.btnAutofillProxy.Click += new System.EventHandler(this.btnAutofillProxy_Click);
            // 
            // tpDebug
            // 
            this.tpDebug.Controls.Add(this.txtDebugLog);
            this.tpDebug.Location = new System.Drawing.Point(4, 22);
            this.tpDebug.Name = "tpDebug";
            this.tpDebug.Padding = new System.Windows.Forms.Padding(3);
            this.tpDebug.Size = new System.Drawing.Size(523, 313);
            this.tpDebug.TabIndex = 8;
            this.tpDebug.Text = "Debug";
            this.tpDebug.UseVisualStyleBackColor = true;
            // 
            // txtDebugLog
            // 
            this.txtDebugLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDebugLog.Location = new System.Drawing.Point(3, 3);
            this.txtDebugLog.Multiline = true;
            this.txtDebugLog.Name = "txtDebugLog";
            this.txtDebugLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDebugLog.Size = new System.Drawing.Size(517, 307);
            this.txtDebugLog.TabIndex = 0;
            this.txtDebugLog.WordWrap = false;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 361);
            this.Controls.Add(this.tcSettings);
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ZUploader - Settings";
            this.Shown += new System.EventHandler(this.SettingsForm_Shown);
            this.Resize += new System.EventHandler(this.SettingsForm_Resize);
            this.tcSettings.ResumeLayout(false);
            this.tpGeneral.ResumeLayout(false);
            this.tpGeneral.PerformLayout();
            this.tpUpload.ResumeLayout(false);
            this.tpUpload.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUploadLimit)).EndInit();
            this.tpHotkeys.ResumeLayout(false);
            this.tpImage.ResumeLayout(false);
            this.tpImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUseImageFormat2After)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudImageJPEGQuality)).EndInit();
            this.tpClipboardUpload.ResumeLayout(false);
            this.tpClipboardUpload.PerformLayout();
            this.tpCapture.ResumeLayout(false);
            this.tpCapture.PerformLayout();
            this.gbSurfaceSettings.ResumeLayout(false);
            this.gbSurfaceSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFixedShapeSizeHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFixedShapeSizeWidth)).EndInit();
            this.tpHistory.ResumeLayout(false);
            this.tpHistory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHistoryMaxItemCount)).EndInit();
            this.tpProxy.ResumeLayout(false);
            this.tpDebug.ResumeLayout(false);
            this.tpDebug.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.PropertyGrid pgProxy;

        #endregion Windows Form Designer generated code

        private System.Windows.Forms.CheckBox cbClipboardAutoCopy;
        private System.Windows.Forms.CheckBox cbAutoPlaySound;
        private System.Windows.Forms.TabControl tcSettings;
        private System.Windows.Forms.TabPage tpGeneral;
        private System.Windows.Forms.TabPage tpProxy;
        private System.Windows.Forms.CheckBox cbShellContextMenu;
        private System.Windows.Forms.TabPage tpHistory;
        private System.Windows.Forms.TextBox txtCustomHistoryPath;
        private System.Windows.Forms.CheckBox cbUseCustomHistoryPath;
        private System.Windows.Forms.CheckBox cbHistorySave;
        private System.Windows.Forms.Button btnBrowseCustomHistoryPath;
        private System.Windows.Forms.TabPage tpImage;
        private System.Windows.Forms.ComboBox cbImageFormat;
        private System.Windows.Forms.Label lblImageFormat;
        private System.Windows.Forms.ComboBox cbImageGIFQuality;
        private System.Windows.Forms.ComboBox cbImageFormat2;
        private System.Windows.Forms.Label lblImageFormat2;
        private System.Windows.Forms.Label lblUseImageFormat2After;
        private System.Windows.Forms.NumericUpDown nudUseImageFormat2After;
        private System.Windows.Forms.NumericUpDown nudImageJPEGQuality;
        private System.Windows.Forms.Label lblImageGIFQuality;
        private System.Windows.Forms.Label lblImageJPEGQuality;
        private System.Windows.Forms.Label lblUseImageFormat2AfterHint;
        private System.Windows.Forms.Label lblImageJPEGQualityHint;
        private System.Windows.Forms.Label lblHistoryMaxItemCount;
        private System.Windows.Forms.NumericUpDown nudHistoryMaxItemCount;
        private System.Windows.Forms.Label lblImageInfo;
        private System.Windows.Forms.Label lblHistoryInfo;
        private System.Windows.Forms.Label lblGeneralInfo;
        private System.Windows.Forms.TabPage tpDebug;
        private System.Windows.Forms.TextBox txtDebugLog;
        private System.Windows.Forms.Button btnNameFormatPatternHelp;
        private System.Windows.Forms.TextBox txtNameFormatPattern;
        private System.Windows.Forms.Label lblNameFormatPattern;
        private System.Windows.Forms.Label lblNameFormatPatternPreview;
        private System.Windows.Forms.TabPage tpClipboardUpload;
        private System.Windows.Forms.Label lblClipboardUploadInfo;
        private System.Windows.Forms.TabPage tpUpload;
        private System.Windows.Forms.ComboBox cbBufferSize;
        private System.Windows.Forms.Label lblBufferSize;
        private System.Windows.Forms.Label lblBufferSizeInfo;
        private System.Windows.Forms.Button btnOpenZUploaderPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblUploadLimitHint;
        private System.Windows.Forms.NumericUpDown nudUploadLimit;
        private System.Windows.Forms.Label lblUploadLimit;
        private System.Windows.Forms.CheckBox cbURLShortenAfterUpload;
        private System.Windows.Forms.Button btnBrowseCustomUploadersConfigPath;
        private System.Windows.Forms.TextBox txtCustomUploadersConfigPath;
        private System.Windows.Forms.CheckBox cbUseCustomUploadersConfigPath;
        private System.Windows.Forms.Button btnLoadUploadersConfig;
        private System.Windows.Forms.Button btnAutofillProxy;
        private System.Windows.Forms.CheckBox cbShowTray;
        private System.Windows.Forms.TabPage tpCapture;
        private System.Windows.Forms.TabPage tpHotkeys;
        private HelpersLib.Hotkey.HotkeyManagerControl hmHotkeys;
        private System.Windows.Forms.CheckBox cbShowCursor;
        private System.Windows.Forms.CheckBox cbDrawBorder;
        private System.Windows.Forms.CheckBox cbDrawCheckerboard;
        private System.Windows.Forms.CheckBox cbQuickCrop;
        private System.Windows.Forms.CheckBox cbFixedShapeSize;
        private System.Windows.Forms.GroupBox gbSurfaceSettings;
        private System.Windows.Forms.Label lblFixedShapeSizeWidth;
        private System.Windows.Forms.NumericUpDown nudFixedShapeSizeHeight;
        private System.Windows.Forms.NumericUpDown nudFixedShapeSizeWidth;
        private System.Windows.Forms.Label lblFixedShapeSizeHeight;
        private System.Windows.Forms.CheckBox cbCaptureTransparent;
        private System.Windows.Forms.Label lblGeneralSeparator;
        private System.Windows.Forms.CheckBox cbStartWithWindows;
    }
}