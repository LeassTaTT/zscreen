﻿namespace ZScreenLib
{
    partial class WorkflowWizard
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
        protected void InitializeComponent()
        {
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpJob = new System.Windows.Forms.TabPage();
            this.gbTask = new System.Windows.Forms.GroupBox();
            this.cboTask = new System.Windows.Forms.ComboBox();
            this.gbName = new System.Windows.Forms.GroupBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.chkUseHotkey = new System.Windows.Forms.CheckBox();
            this.hmcHotkeys = new HelpersLib.Hotkey.HotkeyManagerControl();
            this.tpTasks = new System.Windows.Forms.TabPage();
            this.gbTasks = new System.Windows.Forms.GroupBox();
            this.chkTaskImageResize = new System.Windows.Forms.CheckBox();
            this.chkTaskImageFileFormat = new System.Windows.Forms.CheckBox();
            this.tpImageFileFormat = new System.Windows.Forms.TabPage();
            this.gbPictureQuality = new System.Windows.Forms.GroupBox();
            this.cboJpgSubSampling = new System.Windows.Forms.ComboBox();
            this.cboJpgQuality = new System.Windows.Forms.ComboBox();
            this.cboGIFQuality = new System.Windows.Forms.ComboBox();
            this.lblGIFQuality = new System.Windows.Forms.Label();
            this.nudSwitchAfter = new System.Windows.Forms.NumericUpDown();
            this.lblQuality = new System.Windows.Forms.Label();
            this.cboSwitchFormat = new System.Windows.Forms.ComboBox();
            this.lblFileFormat = new System.Windows.Forms.Label();
            this.cboFileFormat = new System.Windows.Forms.ComboBox();
            this.lblKB = new System.Windows.Forms.Label();
            this.lblAfter = new System.Windows.Forms.Label();
            this.lblSwitchTo = new System.Windows.Forms.Label();
            this.tpImageResize = new System.Windows.Forms.TabPage();
            this.gbImageSize = new System.Windows.Forms.GroupBox();
            this.lblImageSizeFixedAutoScale = new System.Windows.Forms.Label();
            this.rbImageSizeDefault = new System.Windows.Forms.RadioButton();
            this.lblImageSizeFixedHeight = new System.Windows.Forms.Label();
            this.rbImageSizeFixed = new System.Windows.Forms.RadioButton();
            this.lblImageSizeFixedWidth = new System.Windows.Forms.Label();
            this.txtImageSizeRatio = new System.Windows.Forms.TextBox();
            this.lblImageSizeRatioPercentage = new System.Windows.Forms.Label();
            this.txtImageSizeFixedWidth = new System.Windows.Forms.TextBox();
            this.rbImageSizeRatio = new System.Windows.Forms.RadioButton();
            this.txtImageSizeFixedHeight = new System.Windows.Forms.TextBox();
            this.tpOutputs = new System.Windows.Forms.TabPage();
            this.gbRemoteLocations = new System.Windows.Forms.GroupBox();
            this.chkSendspace = new System.Windows.Forms.CheckBox();
            this.chkUploadFTP = new System.Windows.Forms.CheckBox();
            this.chkUploadDropbox = new System.Windows.Forms.CheckBox();
            this.gbSaveToFile = new System.Windows.Forms.GroupBox();
            this.txtSaveFolder = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.chkSaveFile = new System.Windows.Forms.CheckBox();
            this.btnOutputsConfig = new System.Windows.Forms.Button();
            this.chkPrinter = new System.Windows.Forms.CheckBox();
            this.chkUpload = new System.Windows.Forms.CheckBox();
            this.chkClipboard = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tcMain.SuspendLayout();
            this.tpJob.SuspendLayout();
            this.gbTask.SuspendLayout();
            this.gbName.SuspendLayout();
            this.tpTasks.SuspendLayout();
            this.gbTasks.SuspendLayout();
            this.tpImageFileFormat.SuspendLayout();
            this.gbPictureQuality.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSwitchAfter)).BeginInit();
            this.tpImageResize.SuspendLayout();
            this.gbImageSize.SuspendLayout();
            this.tpOutputs.SuspendLayout();
            this.gbRemoteLocations.SuspendLayout();
            this.gbSaveToFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpJob);
            this.tcMain.Controls.Add(this.tpTasks);
            this.tcMain.Controls.Add(this.tpImageFileFormat);
            this.tcMain.Controls.Add(this.tpImageResize);
            this.tcMain.Controls.Add(this.tpOutputs);
            this.tcMain.Location = new System.Drawing.Point(8, 8);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(624, 344);
            this.tcMain.TabIndex = 0;
            // 
            // tpJob
            // 
            this.tpJob.Controls.Add(this.gbTask);
            this.tpJob.Controls.Add(this.gbName);
            this.tpJob.Controls.Add(this.chkUseHotkey);
            this.tpJob.Controls.Add(this.hmcHotkeys);
            this.tpJob.Location = new System.Drawing.Point(4, 22);
            this.tpJob.Name = "tpJob";
            this.tpJob.Padding = new System.Windows.Forms.Padding(3);
            this.tpJob.Size = new System.Drawing.Size(616, 318);
            this.tpJob.TabIndex = 0;
            this.tpJob.Text = "Job";
            this.tpJob.UseVisualStyleBackColor = true;
            // 
            // gbTask
            // 
            this.gbTask.Controls.Add(this.cboTask);
            this.gbTask.Location = new System.Drawing.Point(8, 72);
            this.gbTask.Name = "gbTask";
            this.gbTask.Size = new System.Drawing.Size(584, 56);
            this.gbTask.TabIndex = 10;
            this.gbTask.TabStop = false;
            this.gbTask.Text = "Task";
            // 
            // cboTask
            // 
            this.cboTask.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTask.FormattingEnabled = true;
            this.cboTask.Location = new System.Drawing.Point(8, 24);
            this.cboTask.Name = "cboTask";
            this.cboTask.Size = new System.Drawing.Size(360, 21);
            this.cboTask.TabIndex = 0;
            this.cboTask.SelectedIndexChanged += new System.EventHandler(this.cboTask_SelectedIndexChanged);
            // 
            // gbName
            // 
            this.gbName.Controls.Add(this.txtName);
            this.gbName.Location = new System.Drawing.Point(8, 8);
            this.gbName.Name = "gbName";
            this.gbName.Size = new System.Drawing.Size(584, 56);
            this.gbName.TabIndex = 9;
            this.gbName.TabStop = false;
            this.gbName.Text = "Description";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(8, 24);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(560, 20);
            this.txtName.TabIndex = 7;
            // 
            // chkUseHotkey
            // 
            this.chkUseHotkey.AutoSize = true;
            this.chkUseHotkey.Location = new System.Drawing.Point(24, 160);
            this.chkUseHotkey.Name = "chkUseHotkey";
            this.chkUseHotkey.Size = new System.Drawing.Size(183, 17);
            this.chkUseHotkey.TabIndex = 7;
            this.chkUseHotkey.Text = "Enable a hotkey to run this profile";
            this.chkUseHotkey.UseVisualStyleBackColor = true;
            // 
            // hmcHotkeys
            // 
            this.hmcHotkeys.Location = new System.Drawing.Point(16, 144);
            this.hmcHotkeys.Name = "hmcHotkeys";
            this.hmcHotkeys.Size = new System.Drawing.Size(576, 32);
            this.hmcHotkeys.TabIndex = 11;
            // 
            // tpTasks
            // 
            this.tpTasks.Controls.Add(this.gbTasks);
            this.tpTasks.Location = new System.Drawing.Point(4, 22);
            this.tpTasks.Name = "tpTasks";
            this.tpTasks.Padding = new System.Windows.Forms.Padding(3);
            this.tpTasks.Size = new System.Drawing.Size(616, 318);
            this.tpTasks.TabIndex = 6;
            this.tpTasks.Text = "Tasks";
            this.tpTasks.UseVisualStyleBackColor = true;
            // 
            // gbTasks
            // 
            this.gbTasks.Controls.Add(this.chkTaskImageResize);
            this.gbTasks.Controls.Add(this.chkTaskImageFileFormat);
            this.gbTasks.Location = new System.Drawing.Point(8, 8);
            this.gbTasks.Name = "gbTasks";
            this.gbTasks.Size = new System.Drawing.Size(600, 296);
            this.gbTasks.TabIndex = 2;
            this.gbTasks.TabStop = false;
            this.gbTasks.Text = "I want to...";
            // 
            // chkTaskImageResize
            // 
            this.chkTaskImageResize.AutoSize = true;
            this.chkTaskImageResize.Location = new System.Drawing.Point(16, 24);
            this.chkTaskImageResize.Name = "chkTaskImageResize";
            this.chkTaskImageResize.Size = new System.Drawing.Size(89, 17);
            this.chkTaskImageResize.TabIndex = 0;
            this.chkTaskImageResize.Text = "Resize image";
            this.chkTaskImageResize.UseVisualStyleBackColor = true;
            this.chkTaskImageResize.CheckedChanged += new System.EventHandler(this.chkTaskImageResize_CheckedChanged);
            // 
            // chkTaskImageFileFormat
            // 
            this.chkTaskImageFileFormat.AutoSize = true;
            this.chkTaskImageFileFormat.Location = new System.Drawing.Point(16, 48);
            this.chkTaskImageFileFormat.Name = "chkTaskImageFileFormat";
            this.chkTaskImageFileFormat.Size = new System.Drawing.Size(142, 17);
            this.chkTaskImageFileFormat.TabIndex = 1;
            this.chkTaskImageFileFormat.Text = "Change image file format";
            this.chkTaskImageFileFormat.UseVisualStyleBackColor = true;
            this.chkTaskImageFileFormat.CheckedChanged += new System.EventHandler(this.chkTaskImageFileFormat_CheckedChanged);
            // 
            // tpImageFileFormat
            // 
            this.tpImageFileFormat.Controls.Add(this.gbPictureQuality);
            this.tpImageFileFormat.Location = new System.Drawing.Point(4, 22);
            this.tpImageFileFormat.Name = "tpImageFileFormat";
            this.tpImageFileFormat.Size = new System.Drawing.Size(616, 318);
            this.tpImageFileFormat.TabIndex = 5;
            this.tpImageFileFormat.Text = "File Format";
            this.tpImageFileFormat.UseVisualStyleBackColor = true;
            // 
            // gbPictureQuality
            // 
            this.gbPictureQuality.BackColor = System.Drawing.Color.Transparent;
            this.gbPictureQuality.Controls.Add(this.cboJpgSubSampling);
            this.gbPictureQuality.Controls.Add(this.cboJpgQuality);
            this.gbPictureQuality.Controls.Add(this.cboGIFQuality);
            this.gbPictureQuality.Controls.Add(this.lblGIFQuality);
            this.gbPictureQuality.Controls.Add(this.nudSwitchAfter);
            this.gbPictureQuality.Controls.Add(this.lblQuality);
            this.gbPictureQuality.Controls.Add(this.cboSwitchFormat);
            this.gbPictureQuality.Controls.Add(this.lblFileFormat);
            this.gbPictureQuality.Controls.Add(this.cboFileFormat);
            this.gbPictureQuality.Controls.Add(this.lblKB);
            this.gbPictureQuality.Controls.Add(this.lblAfter);
            this.gbPictureQuality.Controls.Add(this.lblSwitchTo);
            this.gbPictureQuality.Location = new System.Drawing.Point(8, 8);
            this.gbPictureQuality.Name = "gbPictureQuality";
            this.gbPictureQuality.Size = new System.Drawing.Size(592, 288);
            this.gbPictureQuality.TabIndex = 116;
            this.gbPictureQuality.TabStop = false;
            this.gbPictureQuality.Text = "Picture Quality";
            // 
            // cboJpgSubSampling
            // 
            this.cboJpgSubSampling.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboJpgSubSampling.FormattingEnabled = true;
            this.cboJpgSubSampling.Location = new System.Drawing.Point(16, 128);
            this.cboJpgSubSampling.Name = "cboJpgSubSampling";
            this.cboJpgSubSampling.Size = new System.Drawing.Size(416, 21);
            this.cboJpgSubSampling.TabIndex = 120;
            // 
            // cboJpgQuality
            // 
            this.cboJpgQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboJpgQuality.FormattingEnabled = true;
            this.cboJpgQuality.Location = new System.Drawing.Point(16, 96);
            this.cboJpgQuality.Name = "cboJpgQuality";
            this.cboJpgQuality.Size = new System.Drawing.Size(416, 21);
            this.cboJpgQuality.TabIndex = 119;
            // 
            // cboGIFQuality
            // 
            this.cboGIFQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGIFQuality.FormattingEnabled = true;
            this.cboGIFQuality.Items.AddRange(new object[] {
            "Grayscale",
            "4 bit (16 colors)",
            "8 bit (256 colors)"});
            this.cboGIFQuality.Location = new System.Drawing.Point(16, 184);
            this.cboGIFQuality.Name = "cboGIFQuality";
            this.cboGIFQuality.Size = new System.Drawing.Size(98, 21);
            this.cboGIFQuality.TabIndex = 118;
            // 
            // lblGIFQuality
            // 
            this.lblGIFQuality.AutoSize = true;
            this.lblGIFQuality.Location = new System.Drawing.Point(16, 168);
            this.lblGIFQuality.Name = "lblGIFQuality";
            this.lblGIFQuality.Size = new System.Drawing.Size(62, 13);
            this.lblGIFQuality.TabIndex = 117;
            this.lblGIFQuality.Text = "GIF Quality:";
            // 
            // nudSwitchAfter
            // 
            this.nudSwitchAfter.Location = new System.Drawing.Point(125, 40);
            this.nudSwitchAfter.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.nudSwitchAfter.Name = "nudSwitchAfter";
            this.nudSwitchAfter.Size = new System.Drawing.Size(72, 20);
            this.nudSwitchAfter.TabIndex = 116;
            this.nudSwitchAfter.Value = new decimal(new int[] {
            350,
            0,
            0,
            0});
            // 
            // lblQuality
            // 
            this.lblQuality.AutoSize = true;
            this.lblQuality.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblQuality.Location = new System.Drawing.Point(16, 80);
            this.lblQuality.Name = "lblQuality";
            this.lblQuality.Size = new System.Drawing.Size(72, 13);
            this.lblQuality.TabIndex = 108;
            this.lblQuality.Text = "JPEG Quality:";
            // 
            // cboSwitchFormat
            // 
            this.cboSwitchFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSwitchFormat.FormattingEnabled = true;
            this.cboSwitchFormat.Location = new System.Drawing.Point(232, 40);
            this.cboSwitchFormat.Name = "cboSwitchFormat";
            this.cboSwitchFormat.Size = new System.Drawing.Size(98, 21);
            this.cboSwitchFormat.TabIndex = 9;
            // 
            // lblFileFormat
            // 
            this.lblFileFormat.AutoSize = true;
            this.lblFileFormat.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblFileFormat.Location = new System.Drawing.Point(16, 24);
            this.lblFileFormat.Name = "lblFileFormat";
            this.lblFileFormat.Size = new System.Drawing.Size(61, 13);
            this.lblFileFormat.TabIndex = 97;
            this.lblFileFormat.Text = "File Format:";
            // 
            // cboFileFormat
            // 
            this.cboFileFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFileFormat.FormattingEnabled = true;
            this.cboFileFormat.Location = new System.Drawing.Point(16, 40);
            this.cboFileFormat.Name = "cboFileFormat";
            this.cboFileFormat.Size = new System.Drawing.Size(98, 21);
            this.cboFileFormat.TabIndex = 6;
            // 
            // lblKB
            // 
            this.lblKB.AutoSize = true;
            this.lblKB.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblKB.Location = new System.Drawing.Point(197, 44);
            this.lblKB.Name = "lblKB";
            this.lblKB.Size = new System.Drawing.Size(23, 13);
            this.lblKB.TabIndex = 95;
            this.lblKB.Text = "KiB";
            // 
            // lblAfter
            // 
            this.lblAfter.AutoSize = true;
            this.lblAfter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblAfter.Location = new System.Drawing.Point(125, 24);
            this.lblAfter.Name = "lblAfter";
            this.lblAfter.Size = new System.Drawing.Size(88, 13);
            this.lblAfter.TabIndex = 93;
            this.lblAfter.Text = "After: (0 disables)";
            // 
            // lblSwitchTo
            // 
            this.lblSwitchTo.AutoSize = true;
            this.lblSwitchTo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSwitchTo.Location = new System.Drawing.Point(235, 23);
            this.lblSwitchTo.Name = "lblSwitchTo";
            this.lblSwitchTo.Size = new System.Drawing.Size(54, 13);
            this.lblSwitchTo.TabIndex = 92;
            this.lblSwitchTo.Text = "Switch to:";
            // 
            // tpImageResize
            // 
            this.tpImageResize.Controls.Add(this.gbImageSize);
            this.tpImageResize.Location = new System.Drawing.Point(4, 22);
            this.tpImageResize.Name = "tpImageResize";
            this.tpImageResize.Padding = new System.Windows.Forms.Padding(3);
            this.tpImageResize.Size = new System.Drawing.Size(616, 318);
            this.tpImageResize.TabIndex = 4;
            this.tpImageResize.Text = "Resize";
            this.tpImageResize.UseVisualStyleBackColor = true;
            // 
            // gbImageSize
            // 
            this.gbImageSize.Controls.Add(this.lblImageSizeFixedAutoScale);
            this.gbImageSize.Controls.Add(this.rbImageSizeDefault);
            this.gbImageSize.Controls.Add(this.lblImageSizeFixedHeight);
            this.gbImageSize.Controls.Add(this.rbImageSizeFixed);
            this.gbImageSize.Controls.Add(this.lblImageSizeFixedWidth);
            this.gbImageSize.Controls.Add(this.txtImageSizeRatio);
            this.gbImageSize.Controls.Add(this.lblImageSizeRatioPercentage);
            this.gbImageSize.Controls.Add(this.txtImageSizeFixedWidth);
            this.gbImageSize.Controls.Add(this.rbImageSizeRatio);
            this.gbImageSize.Controls.Add(this.txtImageSizeFixedHeight);
            this.gbImageSize.Location = new System.Drawing.Point(8, 8);
            this.gbImageSize.Name = "gbImageSize";
            this.gbImageSize.Size = new System.Drawing.Size(584, 120);
            this.gbImageSize.TabIndex = 125;
            this.gbImageSize.TabStop = false;
            this.gbImageSize.Text = "Image Size";
            // 
            // lblImageSizeFixedAutoScale
            // 
            this.lblImageSizeFixedAutoScale.AutoSize = true;
            this.lblImageSizeFixedAutoScale.Location = new System.Drawing.Point(352, 60);
            this.lblImageSizeFixedAutoScale.Name = "lblImageSizeFixedAutoScale";
            this.lblImageSizeFixedAutoScale.Size = new System.Drawing.Size(152, 13);
            this.lblImageSizeFixedAutoScale.TabIndex = 128;
            this.lblImageSizeFixedAutoScale.Text = "0 height or width for auto scale";
            // 
            // rbImageSizeDefault
            // 
            this.rbImageSizeDefault.AutoSize = true;
            this.rbImageSizeDefault.Location = new System.Drawing.Point(16, 24);
            this.rbImageSizeDefault.Name = "rbImageSizeDefault";
            this.rbImageSizeDefault.Size = new System.Drawing.Size(110, 17);
            this.rbImageSizeDefault.TabIndex = 127;
            this.rbImageSizeDefault.TabStop = true;
            this.rbImageSizeDefault.Text = "Image size default";
            this.rbImageSizeDefault.UseVisualStyleBackColor = true;
            // 
            // lblImageSizeFixedHeight
            // 
            this.lblImageSizeFixedHeight.AutoSize = true;
            this.lblImageSizeFixedHeight.Location = new System.Drawing.Point(232, 59);
            this.lblImageSizeFixedHeight.Name = "lblImageSizeFixedHeight";
            this.lblImageSizeFixedHeight.Size = new System.Drawing.Size(61, 13);
            this.lblImageSizeFixedHeight.TabIndex = 126;
            this.lblImageSizeFixedHeight.Text = "Height (px):";
            // 
            // rbImageSizeFixed
            // 
            this.rbImageSizeFixed.AutoSize = true;
            this.rbImageSizeFixed.Location = new System.Drawing.Point(16, 56);
            this.rbImageSizeFixed.Name = "rbImageSizeFixed";
            this.rbImageSizeFixed.Size = new System.Drawing.Size(103, 17);
            this.rbImageSizeFixed.TabIndex = 123;
            this.rbImageSizeFixed.TabStop = true;
            this.rbImageSizeFixed.Text = "Image size fixed:";
            this.rbImageSizeFixed.UseVisualStyleBackColor = true;
            // 
            // lblImageSizeFixedWidth
            // 
            this.lblImageSizeFixedWidth.AutoSize = true;
            this.lblImageSizeFixedWidth.Location = new System.Drawing.Point(120, 59);
            this.lblImageSizeFixedWidth.Name = "lblImageSizeFixedWidth";
            this.lblImageSizeFixedWidth.Size = new System.Drawing.Size(58, 13);
            this.lblImageSizeFixedWidth.TabIndex = 125;
            this.lblImageSizeFixedWidth.Text = "Width (px):";
            // 
            // txtImageSizeRatio
            // 
            this.txtImageSizeRatio.Location = new System.Drawing.Point(120, 87);
            this.txtImageSizeRatio.Name = "txtImageSizeRatio";
            this.txtImageSizeRatio.Size = new System.Drawing.Size(32, 20);
            this.txtImageSizeRatio.TabIndex = 116;
            this.txtImageSizeRatio.Text = "100";
            this.txtImageSizeRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblImageSizeRatioPercentage
            // 
            this.lblImageSizeRatioPercentage.AutoSize = true;
            this.lblImageSizeRatioPercentage.Location = new System.Drawing.Point(159, 91);
            this.lblImageSizeRatioPercentage.Name = "lblImageSizeRatioPercentage";
            this.lblImageSizeRatioPercentage.Size = new System.Drawing.Size(15, 13);
            this.lblImageSizeRatioPercentage.TabIndex = 118;
            this.lblImageSizeRatioPercentage.Text = "%";
            // 
            // txtImageSizeFixedWidth
            // 
            this.txtImageSizeFixedWidth.Location = new System.Drawing.Point(184, 56);
            this.txtImageSizeFixedWidth.Name = "txtImageSizeFixedWidth";
            this.txtImageSizeFixedWidth.Size = new System.Drawing.Size(40, 20);
            this.txtImageSizeFixedWidth.TabIndex = 119;
            this.txtImageSizeFixedWidth.Text = "2500";
            this.txtImageSizeFixedWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // rbImageSizeRatio
            // 
            this.rbImageSizeRatio.AutoSize = true;
            this.rbImageSizeRatio.Location = new System.Drawing.Point(16, 88);
            this.rbImageSizeRatio.Name = "rbImageSizeRatio";
            this.rbImageSizeRatio.Size = new System.Drawing.Size(101, 17);
            this.rbImageSizeRatio.TabIndex = 122;
            this.rbImageSizeRatio.TabStop = true;
            this.rbImageSizeRatio.Text = "Image size ratio:";
            this.rbImageSizeRatio.UseVisualStyleBackColor = true;
            // 
            // txtImageSizeFixedHeight
            // 
            this.txtImageSizeFixedHeight.Location = new System.Drawing.Point(296, 56);
            this.txtImageSizeFixedHeight.Name = "txtImageSizeFixedHeight";
            this.txtImageSizeFixedHeight.Size = new System.Drawing.Size(40, 20);
            this.txtImageSizeFixedHeight.TabIndex = 120;
            this.txtImageSizeFixedHeight.Text = "2500";
            this.txtImageSizeFixedHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tpOutputs
            // 
            this.tpOutputs.Controls.Add(this.gbRemoteLocations);
            this.tpOutputs.Controls.Add(this.gbSaveToFile);
            this.tpOutputs.Controls.Add(this.chkSaveFile);
            this.tpOutputs.Controls.Add(this.btnOutputsConfig);
            this.tpOutputs.Controls.Add(this.chkPrinter);
            this.tpOutputs.Controls.Add(this.chkUpload);
            this.tpOutputs.Controls.Add(this.chkClipboard);
            this.tpOutputs.Location = new System.Drawing.Point(4, 22);
            this.tpOutputs.Name = "tpOutputs";
            this.tpOutputs.Padding = new System.Windows.Forms.Padding(3);
            this.tpOutputs.Size = new System.Drawing.Size(616, 318);
            this.tpOutputs.TabIndex = 2;
            this.tpOutputs.Text = "Outputs";
            this.tpOutputs.UseVisualStyleBackColor = true;
            // 
            // gbRemoteLocations
            // 
            this.gbRemoteLocations.Controls.Add(this.chkSendspace);
            this.gbRemoteLocations.Controls.Add(this.chkUploadFTP);
            this.gbRemoteLocations.Controls.Add(this.chkUploadDropbox);
            this.gbRemoteLocations.Location = new System.Drawing.Point(8, 168);
            this.gbRemoteLocations.Name = "gbRemoteLocations";
            this.gbRemoteLocations.Size = new System.Drawing.Size(576, 48);
            this.gbRemoteLocations.TabIndex = 6;
            this.gbRemoteLocations.TabStop = false;
            this.gbRemoteLocations.Text = "Upload to Remote Locations";
            // 
            // chkSendspace
            // 
            this.chkSendspace.Location = new System.Drawing.Point(272, 16);
            this.chkSendspace.Name = "chkSendspace";
            this.chkSendspace.Size = new System.Drawing.Size(104, 24);
            this.chkSendspace.TabIndex = 6;
            this.chkSendspace.Text = "Sendspace";
            this.chkSendspace.UseVisualStyleBackColor = true;
            // 
            // chkUploadFTP
            // 
            this.chkUploadFTP.Location = new System.Drawing.Point(144, 16);
            this.chkUploadFTP.Name = "chkUploadFTP";
            this.chkUploadFTP.Size = new System.Drawing.Size(104, 24);
            this.chkUploadFTP.TabIndex = 5;
            this.chkUploadFTP.Text = "FTP Server";
            this.chkUploadFTP.UseVisualStyleBackColor = true;
            // 
            // chkUploadDropbox
            // 
            this.chkUploadDropbox.Location = new System.Drawing.Point(16, 16);
            this.chkUploadDropbox.Name = "chkUploadDropbox";
            this.chkUploadDropbox.Size = new System.Drawing.Size(104, 24);
            this.chkUploadDropbox.TabIndex = 4;
            this.chkUploadDropbox.Text = "Dropbox";
            this.chkUploadDropbox.UseVisualStyleBackColor = true;
            // 
            // gbSaveToFile
            // 
            this.gbSaveToFile.Controls.Add(this.txtSaveFolder);
            this.gbSaveToFile.Controls.Add(this.btnBrowse);
            this.gbSaveToFile.Location = new System.Drawing.Point(8, 96);
            this.gbSaveToFile.Name = "gbSaveToFile";
            this.gbSaveToFile.Size = new System.Drawing.Size(576, 64);
            this.gbSaveToFile.TabIndex = 5;
            this.gbSaveToFile.TabStop = false;
            this.gbSaveToFile.Text = "When taking a screenshot, save the file to a preconfigured location";
            // 
            // txtSaveFolder
            // 
            this.txtSaveFolder.Location = new System.Drawing.Point(16, 24);
            this.txtSaveFolder.Name = "txtSaveFolder";
            this.txtSaveFolder.Size = new System.Drawing.Size(456, 20);
            this.txtSaveFolder.TabIndex = 7;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(480, 20);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(80, 24);
            this.btnBrowse.TabIndex = 6;
            this.btnBrowse.Text = "&Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // chkSaveFile
            // 
            this.chkSaveFile.Location = new System.Drawing.Point(16, 64);
            this.chkSaveFile.Name = "chkSaveFile";
            this.chkSaveFile.Size = new System.Drawing.Size(184, 24);
            this.chkSaveFile.TabIndex = 4;
            this.chkSaveFile.Text = "Save to file";
            this.chkSaveFile.UseVisualStyleBackColor = true;
            this.chkSaveFile.CheckedChanged += new System.EventHandler(this.chkSaveFile_CheckedChanged);
            // 
            // btnOutputsConfig
            // 
            this.btnOutputsConfig.Location = new System.Drawing.Point(8, 8);
            this.btnOutputsConfig.Name = "btnOutputsConfig";
            this.btnOutputsConfig.Size = new System.Drawing.Size(336, 24);
            this.btnOutputsConfig.TabIndex = 0;
            this.btnOutputsConfig.Text = "Outputs Configuration... ( Dropbox, FTP, SendSpace etc. )";
            this.btnOutputsConfig.UseVisualStyleBackColor = true;
            this.btnOutputsConfig.Click += new System.EventHandler(this.btnOutputsConfig_Click);
            // 
            // chkPrinter
            // 
            this.chkPrinter.Location = new System.Drawing.Point(256, 64);
            this.chkPrinter.Name = "chkPrinter";
            this.chkPrinter.Size = new System.Drawing.Size(184, 24);
            this.chkPrinter.TabIndex = 3;
            this.chkPrinter.Text = "Send to Printer";
            this.chkPrinter.UseVisualStyleBackColor = true;
            // 
            // chkUpload
            // 
            this.chkUpload.Location = new System.Drawing.Point(256, 40);
            this.chkUpload.Name = "chkUpload";
            this.chkUpload.Size = new System.Drawing.Size(184, 24);
            this.chkUpload.TabIndex = 2;
            this.chkUpload.Text = "Upload to Remote Locations";
            this.chkUpload.UseVisualStyleBackColor = true;
            // 
            // chkClipboard
            // 
            this.chkClipboard.Location = new System.Drawing.Point(16, 40);
            this.chkClipboard.Name = "chkClipboard";
            this.chkClipboard.Size = new System.Drawing.Size(184, 24);
            this.chkClipboard.TabIndex = 1;
            this.chkClipboard.Text = "Copy Image or Text to Clipboard";
            this.chkClipboard.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(440, 360);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(104, 24);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&Save && Close";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(552, 360);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // WorkflowWizard
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 394);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tcMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(664, 420);
            this.Name = "WorkflowWizard";
            this.Text = "Configure Workflow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProfileWizard_FormClosing);
            this.Load += new System.EventHandler(this.WorkflowWizard_Load);
            this.tcMain.ResumeLayout(false);
            this.tpJob.ResumeLayout(false);
            this.tpJob.PerformLayout();
            this.gbTask.ResumeLayout(false);
            this.gbName.ResumeLayout(false);
            this.gbName.PerformLayout();
            this.tpTasks.ResumeLayout(false);
            this.gbTasks.ResumeLayout(false);
            this.gbTasks.PerformLayout();
            this.tpImageFileFormat.ResumeLayout(false);
            this.gbPictureQuality.ResumeLayout(false);
            this.gbPictureQuality.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSwitchAfter)).EndInit();
            this.tpImageResize.ResumeLayout(false);
            this.gbImageSize.ResumeLayout(false);
            this.gbImageSize.PerformLayout();
            this.tpOutputs.ResumeLayout(false);
            this.gbRemoteLocations.ResumeLayout(false);
            this.gbSaveToFile.ResumeLayout(false);
            this.gbSaveToFile.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpJob;
        private System.Windows.Forms.ComboBox cboTask;
        private System.Windows.Forms.TabPage tpOutputs;
        private System.Windows.Forms.TabPage tpImageResize;
        private System.Windows.Forms.CheckBox chkPrinter;
        private System.Windows.Forms.CheckBox chkUpload;
        private System.Windows.Forms.CheckBox chkClipboard;
        private System.Windows.Forms.Button btnOutputsConfig;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkUseHotkey;
        private System.Windows.Forms.CheckBox chkSaveFile;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.GroupBox gbSaveToFile;
        private System.Windows.Forms.TextBox txtSaveFolder;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.GroupBox gbRemoteLocations;
        private System.Windows.Forms.CheckBox chkSendspace;
        private System.Windows.Forms.CheckBox chkUploadFTP;
        private System.Windows.Forms.CheckBox chkUploadDropbox;
        private System.Windows.Forms.TabPage tpImageFileFormat;
        protected HelpersLib.Hotkey.HotkeyManagerControl hmcHotkeys;
        private System.Windows.Forms.TabPage tpTasks;
        private System.Windows.Forms.CheckBox chkTaskImageFileFormat;
        private System.Windows.Forms.CheckBox chkTaskImageResize;
        private System.Windows.Forms.GroupBox gbImageSize;
        private System.Windows.Forms.Label lblImageSizeFixedAutoScale;
        private System.Windows.Forms.RadioButton rbImageSizeDefault;
        private System.Windows.Forms.Label lblImageSizeFixedHeight;
        private System.Windows.Forms.RadioButton rbImageSizeFixed;
        private System.Windows.Forms.Label lblImageSizeFixedWidth;
        private System.Windows.Forms.TextBox txtImageSizeRatio;
        private System.Windows.Forms.Label lblImageSizeRatioPercentage;
        private System.Windows.Forms.TextBox txtImageSizeFixedWidth;
        private System.Windows.Forms.RadioButton rbImageSizeRatio;
        private System.Windows.Forms.TextBox txtImageSizeFixedHeight;
        internal System.Windows.Forms.GroupBox gbPictureQuality;
        private System.Windows.Forms.ComboBox cboJpgSubSampling;
        private System.Windows.Forms.ComboBox cboJpgQuality;
        private System.Windows.Forms.ComboBox cboGIFQuality;
        private System.Windows.Forms.Label lblGIFQuality;
        internal System.Windows.Forms.NumericUpDown nudSwitchAfter;
        internal System.Windows.Forms.Label lblQuality;
        internal System.Windows.Forms.ComboBox cboSwitchFormat;
        internal System.Windows.Forms.Label lblFileFormat;
        internal System.Windows.Forms.ComboBox cboFileFormat;
        internal System.Windows.Forms.Label lblKB;
        internal System.Windows.Forms.Label lblAfter;
        internal System.Windows.Forms.Label lblSwitchTo;
        private System.Windows.Forms.GroupBox gbTasks;
        protected System.Windows.Forms.GroupBox gbName;
        protected System.Windows.Forms.GroupBox gbTask;
    }
}