using ZScreenLib;
using UploadersLib;
using System.Windows.Forms;
using System.Diagnostics;

namespace ZScreenGUI
{
    partial class ZScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZScreen));
            this.niTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmEntireScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSelectedWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCropShot = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLastCropShot = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCaptureShape = new System.Windows.Forms.ToolStripMenuItem();
            this.autoScreenshotsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmEditinImageSoftware = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmFileUpload = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmClipboardUpload = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDragDropWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLanguageTranslator = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmScreenColorPicker = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiTabs = new System.Windows.Forms.ToolStripMenuItem();
            this.historyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmViewLocalDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmFTPClient = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmVersionHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmExitZScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmActions = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tmrApp = new System.Windows.Forms.Timer(this.components);
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpMain = new System.Windows.Forms.TabPage();
            this.lblScreenshotDelay = new System.Windows.Forms.Label();
            this.nudScreenshotDelay = new ZScreenGUI.NumericUpDownTimer();
            this.tsLinks = new System.Windows.Forms.ToolStrip();
            this.tsbLinkTutorials = new System.Windows.Forms.ToolStripButton();
            this.tsbLinkBugs = new System.Windows.Forms.ToolStripButton();
            this.tsbLinkHome = new System.Windows.Forms.ToolStripButton();
            this.gbImageSettings = new System.Windows.Forms.GroupBox();
            this.chkShowUploadResults = new System.Windows.Forms.CheckBox();
            this.btnActionsUI = new System.Windows.Forms.Button();
            this.chkShortenURL = new System.Windows.Forms.CheckBox();
            this.chkPerformActions = new System.Windows.Forms.CheckBox();
            this.chkShowWorkflowWizard = new System.Windows.Forms.CheckBox();
            this.ucDestOptions = new ZScreenLib.DestSelector();
            this.pbPreview = new HelpersLib.MyPictureBox();
            this.tpHotkeys = new System.Windows.Forms.TabPage();
            this.hmHotkeys = new HelpersLib.Hotkey.HotkeyManagerControl();
            this.btnResetHotkeys = new System.Windows.Forms.Button();
            this.lblHotkeyStatus = new System.Windows.Forms.Label();
            this.tpMainInput = new System.Windows.Forms.TabPage();
            this.tcCapture = new System.Windows.Forms.TabControl();
            this.tpActivewindow = new System.Windows.Forms.TabPage();
            this.gbCaptureDwm = new System.Windows.Forms.GroupBox();
            this.chkSelectedWindowIncludeShadow = new System.Windows.Forms.CheckBox();
            this.pbActiveWindowDwmBackColor = new System.Windows.Forms.PictureBox();
            this.chkActiveWindowDwmCustomColor = new System.Windows.Forms.CheckBox();
            this.gbCaptureEngine = new System.Windows.Forms.GroupBox();
            this.chkActiveWindowCleanBackground = new System.Windows.Forms.CheckBox();
            this.chkSelectedWindowShowCheckers = new System.Windows.Forms.CheckBox();
            this.cboCaptureEngine = new System.Windows.Forms.ComboBox();
            this.chkShowCursor = new System.Windows.Forms.CheckBox();
            this.gbCaptureGdi = new System.Windows.Forms.GroupBox();
            this.chkActiveWindowTryCaptureChildren = new System.Windows.Forms.CheckBox();
            this.tpSelectedWindow = new System.Windows.Forms.TabPage();
            this.chkSelectedWindowCaptureObjects = new System.Windows.Forms.CheckBox();
            this.nudSelectedWindowHueRange = new System.Windows.Forms.NumericUpDown();
            this.lblSelectedWindowHueRange = new System.Windows.Forms.Label();
            this.nudSelectedWindowRegionStep = new System.Windows.Forms.NumericUpDown();
            this.nudSelectedWindowRegionInterval = new System.Windows.Forms.NumericUpDown();
            this.lblSelectedWindowRegionStep = new System.Windows.Forms.Label();
            this.lblSelectedWindowRegionInterval = new System.Windows.Forms.Label();
            this.cbSelectedWindowDynamicBorderColor = new System.Windows.Forms.CheckBox();
            this.cbSelectedWindowRuler = new System.Windows.Forms.CheckBox();
            this.lblSelectedWindowRegionStyle = new System.Windows.Forms.Label();
            this.cbSelectedWindowStyle = new System.Windows.Forms.ComboBox();
            this.cbSelectedWindowRectangleInfo = new System.Windows.Forms.CheckBox();
            this.lblSelectedWindowBorderColor = new System.Windows.Forms.Label();
            this.nudSelectedWindowBorderSize = new System.Windows.Forms.NumericUpDown();
            this.lblSelectedWindowBorderSize = new System.Windows.Forms.Label();
            this.pbSelectedWindowBorderColor = new System.Windows.Forms.PictureBox();
            this.tpCropShot = new System.Windows.Forms.TabPage();
            this.gbCropEngine = new System.Windows.Forms.GroupBox();
            this.cboCropEngine = new System.Windows.Forms.ComboBox();
            this.gbCropShotMagnifyingGlass = new System.Windows.Forms.GroupBox();
            this.chkCropShowMagnifyingGlass = new System.Windows.Forms.CheckBox();
            this.gbCropDynamicRegionBorderColorSettings = new System.Windows.Forms.GroupBox();
            this.nudCropRegionStep = new System.Windows.Forms.NumericUpDown();
            this.nudCropHueRange = new System.Windows.Forms.NumericUpDown();
            this.cbCropDynamicBorderColor = new System.Windows.Forms.CheckBox();
            this.lblCropRegionInterval = new System.Windows.Forms.Label();
            this.lblCropHueRange = new System.Windows.Forms.Label();
            this.lblCropRegionStep = new System.Windows.Forms.Label();
            this.nudCropRegionInterval = new System.Windows.Forms.NumericUpDown();
            this.gbCropRegion = new System.Windows.Forms.GroupBox();
            this.lblCropRegionStyle = new System.Windows.Forms.Label();
            this.chkRegionHotkeyInfo = new System.Windows.Forms.CheckBox();
            this.chkCropStyle = new System.Windows.Forms.ComboBox();
            this.chkRegionRectangleInfo = new System.Windows.Forms.CheckBox();
            this.gbCropRegionSettings = new System.Windows.Forms.GroupBox();
            this.lblCropBorderSize = new System.Windows.Forms.Label();
            this.cbShowCropRuler = new System.Windows.Forms.CheckBox();
            this.cbCropShowGrids = new System.Windows.Forms.CheckBox();
            this.lblCropBorderColor = new System.Windows.Forms.Label();
            this.pbCropBorderColor = new System.Windows.Forms.PictureBox();
            this.nudCropBorderSize = new System.Windows.Forms.NumericUpDown();
            this.gbCropCrosshairSettings = new System.Windows.Forms.GroupBox();
            this.chkCropDynamicCrosshair = new System.Windows.Forms.CheckBox();
            this.lblCropCrosshairStep = new System.Windows.Forms.Label();
            this.chkCropShowBigCross = new System.Windows.Forms.CheckBox();
            this.pbCropCrosshairColor = new System.Windows.Forms.PictureBox();
            this.lblCropCrosshairInterval = new System.Windows.Forms.Label();
            this.lblCropCrosshairColor = new System.Windows.Forms.Label();
            this.nudCrosshairLineCount = new System.Windows.Forms.NumericUpDown();
            this.nudCropCrosshairInterval = new System.Windows.Forms.NumericUpDown();
            this.nudCrosshairLineSize = new System.Windows.Forms.NumericUpDown();
            this.nudCropCrosshairStep = new System.Windows.Forms.NumericUpDown();
            this.lblCrosshairLineSize = new System.Windows.Forms.Label();
            this.lblCrosshairLineCount = new System.Windows.Forms.Label();
            this.gbCropGridMode = new System.Windows.Forms.GroupBox();
            this.cboCropGridMode = new System.Windows.Forms.CheckBox();
            this.nudCropGridHeight = new System.Windows.Forms.NumericUpDown();
            this.lblGridSizeWidth = new System.Windows.Forms.Label();
            this.lblGridSize = new System.Windows.Forms.Label();
            this.lblGridSizeHeight = new System.Windows.Forms.Label();
            this.nudCropGridWidth = new System.Windows.Forms.NumericUpDown();
            this.tpCropShotLast = new System.Windows.Forms.TabPage();
            this.btnLastCropShotReset = new System.Windows.Forms.Button();
            this.tpCaptureShape = new System.Windows.Forms.TabPage();
            this.pgSurfaceConfig = new System.Windows.Forms.PropertyGrid();
            this.tpFreehandCropShot = new System.Windows.Forms.TabPage();
            this.cbFreehandCropShowRectangleBorder = new System.Windows.Forms.CheckBox();
            this.cbFreehandCropAutoClose = new System.Windows.Forms.CheckBox();
            this.cbFreehandCropAutoUpload = new System.Windows.Forms.CheckBox();
            this.cbFreehandCropShowHelpText = new System.Windows.Forms.CheckBox();
            this.tpCaptureClipboard = new System.Windows.Forms.TabPage();
            this.gbMonitorClipboard = new System.Windows.Forms.GroupBox();
            this.chkMonUrls = new System.Windows.Forms.CheckBox();
            this.chkMonFiles = new System.Windows.Forms.CheckBox();
            this.chkMonImages = new System.Windows.Forms.CheckBox();
            this.chkMonText = new System.Windows.Forms.CheckBox();
            this.tpAdvanced = new System.Windows.Forms.TabPage();
            this.tcAdvanced = new System.Windows.Forms.TabControl();
            this.tpAdvancedDebug = new System.Windows.Forms.TabPage();
            this.rtbDebugLog = new System.Windows.Forms.RichTextBox();
            this.tpAdvancedCore = new System.Windows.Forms.TabPage();
            this.pgAppSettings = new System.Windows.Forms.PropertyGrid();
            this.tpAdvancedSettings = new System.Windows.Forms.TabPage();
            this.pgAppConfig = new System.Windows.Forms.PropertyGrid();
            this.tpAdvancedWorkflow = new System.Windows.Forms.TabPage();
            this.pgWorkflow = new System.Windows.Forms.PropertyGrid();
            this.tpAdvanedUploaders = new System.Windows.Forms.TabPage();
            this.pgUploaders = new System.Windows.Forms.PropertyGrid();
            this.tpAdvancedStats = new System.Windows.Forms.TabPage();
            this.btnOpenZScreenTester = new System.Windows.Forms.Button();
            this.gbStatistics = new System.Windows.Forms.GroupBox();
            this.btnDebugStart = new System.Windows.Forms.Button();
            this.rtbStats = new System.Windows.Forms.RichTextBox();
            this.gbLastSource = new System.Windows.Forms.GroupBox();
            this.btnOpenSourceString = new System.Windows.Forms.Button();
            this.btnOpenSourceText = new System.Windows.Forms.Button();
            this.btnOpenSourceBrowser = new System.Windows.Forms.Button();
            this.tpQueue = new System.Windows.Forms.TabPage();
            this.lvUploads = new HelpersLib.MyListView();
            this.chFilename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chProgress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSpeed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chElapsed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chRemaining = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chUploaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chHost = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chURL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpDestImageBam = new System.Windows.Forms.TabPage();
            this.gbImageBamGalleries = new System.Windows.Forms.GroupBox();
            this.lbImageBamGalleries = new System.Windows.Forms.ListBox();
            this.gbImageBamLinks = new System.Windows.Forms.GroupBox();
            this.chkImageBamContentNSFW = new System.Windows.Forms.CheckBox();
            this.btnImageBamRemoveGallery = new System.Windows.Forms.Button();
            this.btnImageBamCreateGallery = new System.Windows.Forms.Button();
            this.btnImageBamRegister = new System.Windows.Forms.Button();
            this.btnImageBamApiKeysUrl = new System.Windows.Forms.Button();
            this.gbImageBamApiKeys = new System.Windows.Forms.GroupBox();
            this.lblImageBamSecret = new System.Windows.Forms.Label();
            this.txtImageBamSecret = new System.Windows.Forms.TextBox();
            this.lblImageBamKey = new System.Windows.Forms.Label();
            this.txtImageBamApiKey = new System.Windows.Forms.TextBox();
            this.tpUploadText = new System.Windows.Forms.TabPage();
            this.txtTextUploaderContent = new System.Windows.Forms.TextBox();
            this.btnUploadText = new System.Windows.Forms.Button();
            this.btnUploadTextClipboard = new System.Windows.Forms.Button();
            this.btnUploadTextClipboardFile = new System.Windows.Forms.Button();
            this.ttZScreen = new System.Windows.Forms.ToolTip(this.components);
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.msApp = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFileUpload = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiConfigure = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiConfigureFileNaming = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiImageSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiWatermark = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiConfigureActions = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOutputs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiProxy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiVersionHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmTray.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tpMain.SuspendLayout();
            this.tsLinks.SuspendLayout();
            this.gbImageSettings.SuspendLayout();
            this.tpHotkeys.SuspendLayout();
            this.tpMainInput.SuspendLayout();
            this.tcCapture.SuspendLayout();
            this.tpActivewindow.SuspendLayout();
            this.gbCaptureDwm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbActiveWindowDwmBackColor)).BeginInit();
            this.gbCaptureEngine.SuspendLayout();
            this.gbCaptureGdi.SuspendLayout();
            this.tpSelectedWindow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSelectedWindowHueRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSelectedWindowRegionStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSelectedWindowRegionInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSelectedWindowBorderSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSelectedWindowBorderColor)).BeginInit();
            this.tpCropShot.SuspendLayout();
            this.gbCropEngine.SuspendLayout();
            this.gbCropShotMagnifyingGlass.SuspendLayout();
            this.gbCropDynamicRegionBorderColorSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropRegionStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropHueRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropRegionInterval)).BeginInit();
            this.gbCropRegion.SuspendLayout();
            this.gbCropRegionSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCropBorderColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropBorderSize)).BeginInit();
            this.gbCropCrosshairSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCropCrosshairColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCrosshairLineCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropCrosshairInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCrosshairLineSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropCrosshairStep)).BeginInit();
            this.gbCropGridMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropGridHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropGridWidth)).BeginInit();
            this.tpCropShotLast.SuspendLayout();
            this.tpCaptureShape.SuspendLayout();
            this.tpFreehandCropShot.SuspendLayout();
            this.tpCaptureClipboard.SuspendLayout();
            this.gbMonitorClipboard.SuspendLayout();
            this.tpAdvanced.SuspendLayout();
            this.tcAdvanced.SuspendLayout();
            this.tpAdvancedDebug.SuspendLayout();
            this.tpAdvancedCore.SuspendLayout();
            this.tpAdvancedSettings.SuspendLayout();
            this.tpAdvancedWorkflow.SuspendLayout();
            this.tpAdvanedUploaders.SuspendLayout();
            this.tpAdvancedStats.SuspendLayout();
            this.gbStatistics.SuspendLayout();
            this.gbLastSource.SuspendLayout();
            this.tpQueue.SuspendLayout();
            this.tpDestImageBam.SuspendLayout();
            this.gbImageBamGalleries.SuspendLayout();
            this.gbImageBamLinks.SuspendLayout();
            this.gbImageBamApiKeys.SuspendLayout();
            this.msApp.SuspendLayout();
            this.SuspendLayout();
            // 
            // niTray
            // 
            this.niTray.ContextMenuStrip = this.cmTray;
            this.niTray.Icon = ((System.Drawing.Icon)(resources.GetObject("niTray.Icon")));
            this.niTray.Text = "ZScreen";
            this.niTray.Visible = true;
            this.niTray.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.niTray_MouseDoubleClick);
            // 
            // cmTray
            // 
            this.cmTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmEntireScreen,
            this.tsmSelectedWindow,
            this.tsmCropShot,
            this.tsmLastCropShot,
            this.tsmCaptureShape,
            this.autoScreenshotsToolStripMenuItem,
            this.toolStripSeparator1,
            this.tsmEditinImageSoftware,
            this.toolStripSeparator6,
            this.tsmFileUpload,
            this.tsmClipboardUpload,
            this.tsmDragDropWindow,
            this.tsmLanguageTranslator,
            this.tsmScreenColorPicker,
            this.toolStripSeparator4,
            this.tsmiTabs,
            this.historyToolStripMenuItem,
            this.tsmViewLocalDirectory,
            this.tsmFTPClient,
            this.tsmHelp,
            this.toolStripSeparator3,
            this.tsmExitZScreen});
            this.cmTray.Name = "cmTray";
            this.cmTray.Size = new System.Drawing.Size(201, 446);
            // 
            // tsmEntireScreen
            // 
            this.tsmEntireScreen.Image = global::ZScreenGUI.Properties.Resources.monitor;
            this.tsmEntireScreen.Name = "tsmEntireScreen";
            this.tsmEntireScreen.Size = new System.Drawing.Size(200, 22);
            this.tsmEntireScreen.Text = "Entire Screen";
            this.tsmEntireScreen.Click += new System.EventHandler(this.entireScreenToolStripMenuItem_Click);
            // 
            // tsmSelectedWindow
            // 
            this.tsmSelectedWindow.Image = global::ZScreenGUI.Properties.Resources.application_double;
            this.tsmSelectedWindow.Name = "tsmSelectedWindow";
            this.tsmSelectedWindow.Size = new System.Drawing.Size(200, 22);
            this.tsmSelectedWindow.Text = "Selected Window...";
            this.tsmSelectedWindow.Click += new System.EventHandler(this.selectedWindowToolStripMenuItem_Click);
            // 
            // tsmCropShot
            // 
            this.tsmCropShot.Image = global::ZScreenGUI.Properties.Resources.shape_square;
            this.tsmCropShot.Name = "tsmCropShot";
            this.tsmCropShot.Size = new System.Drawing.Size(200, 22);
            this.tsmCropShot.Text = "Crop Shot...";
            this.tsmCropShot.Click += new System.EventHandler(this.rectangularRegionToolStripMenuItem_Click);
            // 
            // tsmLastCropShot
            // 
            this.tsmLastCropShot.Image = global::ZScreenGUI.Properties.Resources.shape_square_go;
            this.tsmLastCropShot.Name = "tsmLastCropShot";
            this.tsmLastCropShot.Size = new System.Drawing.Size(200, 22);
            this.tsmLastCropShot.Text = "Last Crop Shot";
            this.tsmLastCropShot.Click += new System.EventHandler(this.lastRectangularRegionToolStripMenuItem_Click);
            // 
            // tsmCaptureShape
            // 
            this.tsmCaptureShape.Image = global::ZScreenGUI.Properties.Resources.shape_square_edit;
            this.tsmCaptureShape.Name = "tsmCaptureShape";
            this.tsmCaptureShape.Size = new System.Drawing.Size(200, 22);
            this.tsmCaptureShape.Text = "Capture Shape";
            this.tsmCaptureShape.Click += new System.EventHandler(this.tsmFreehandCropShot_Click);
            // 
            // autoScreenshotsToolStripMenuItem
            // 
            this.autoScreenshotsToolStripMenuItem.Image = global::ZScreenGUI.Properties.Resources.images_stack;
            this.autoScreenshotsToolStripMenuItem.Name = "autoScreenshotsToolStripMenuItem";
            this.autoScreenshotsToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.autoScreenshotsToolStripMenuItem.Text = "Auto Capture...";
            this.autoScreenshotsToolStripMenuItem.Click += new System.EventHandler(this.autoScreenshotsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(197, 6);
            // 
            // tsmEditinImageSoftware
            // 
            this.tsmEditinImageSoftware.Image = global::ZScreenGUI.Properties.Resources.picture_edit;
            this.tsmEditinImageSoftware.Name = "tsmEditinImageSoftware";
            this.tsmEditinImageSoftware.Size = new System.Drawing.Size(200, 22);
            this.tsmEditinImageSoftware.Text = "Perform Custom Actions";
            this.tsmEditinImageSoftware.Click += new System.EventHandler(this.tsmEditinImageSoftware_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(197, 6);
            // 
            // tsmFileUpload
            // 
            this.tsmFileUpload.Image = global::ZScreenGUI.Properties.Resources.drive_network;
            this.tsmFileUpload.Name = "tsmFileUpload";
            this.tsmFileUpload.Size = new System.Drawing.Size(200, 22);
            this.tsmFileUpload.Text = "File Upload...";
            this.tsmFileUpload.Click += new System.EventHandler(this.tsmFileUpload_Click);
            // 
            // tsmClipboardUpload
            // 
            this.tsmClipboardUpload.Image = global::ZScreenGUI.Properties.Resources.images;
            this.tsmClipboardUpload.Name = "tsmClipboardUpload";
            this.tsmClipboardUpload.Size = new System.Drawing.Size(200, 22);
            this.tsmClipboardUpload.Text = "Clipboard Upload...";
            this.tsmClipboardUpload.Click += new System.EventHandler(this.tsmUploadFromClipboard_Click);
            // 
            // tsmDragDropWindow
            // 
            this.tsmDragDropWindow.Image = global::ZScreenGUI.Properties.Resources.shape_move_backwards;
            this.tsmDragDropWindow.Name = "tsmDragDropWindow";
            this.tsmDragDropWindow.Size = new System.Drawing.Size(200, 22);
            this.tsmDragDropWindow.Text = "Drag && Drop Window...";
            this.tsmDragDropWindow.Click += new System.EventHandler(this.tsmDropWindow_Click);
            // 
            // tsmLanguageTranslator
            // 
            this.tsmLanguageTranslator.Image = global::ZScreenGUI.Properties.Resources.comments;
            this.tsmLanguageTranslator.Name = "tsmLanguageTranslator";
            this.tsmLanguageTranslator.Size = new System.Drawing.Size(200, 22);
            this.tsmLanguageTranslator.Text = "Language Translator";
            this.tsmLanguageTranslator.Click += new System.EventHandler(this.languageTranslatorToolStripMenuItem_Click);
            // 
            // tsmScreenColorPicker
            // 
            this.tsmScreenColorPicker.Image = global::ZScreenGUI.Properties.Resources.color_wheel;
            this.tsmScreenColorPicker.Name = "tsmScreenColorPicker";
            this.tsmScreenColorPicker.Size = new System.Drawing.Size(200, 22);
            this.tsmScreenColorPicker.Text = "Screen Color Picker...";
            this.tsmScreenColorPicker.Click += new System.EventHandler(this.screenColorPickerToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(197, 6);
            // 
            // tsmiTabs
            // 
            this.tsmiTabs.DoubleClickEnabled = true;
            this.tsmiTabs.Image = global::ZScreenGUI.Properties.Resources.application_edit;
            this.tsmiTabs.Name = "tsmiTabs";
            this.tsmiTabs.Size = new System.Drawing.Size(200, 22);
            this.tsmiTabs.Text = "&Options...";
            this.tsmiTabs.Click += new System.EventHandler(this.tsmSettings_Click);
            // 
            // historyToolStripMenuItem
            // 
            this.historyToolStripMenuItem.Image = global::ZScreenGUI.Properties.Resources.pictures;
            this.historyToolStripMenuItem.Name = "historyToolStripMenuItem";
            this.historyToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.historyToolStripMenuItem.Text = "&History...";
            this.historyToolStripMenuItem.Click += new System.EventHandler(this.historyToolStripMenuItem_Click);
            // 
            // tsmViewLocalDirectory
            // 
            this.tsmViewLocalDirectory.Image = global::ZScreenGUI.Properties.Resources.folder_picture;
            this.tsmViewLocalDirectory.Name = "tsmViewLocalDirectory";
            this.tsmViewLocalDirectory.Size = new System.Drawing.Size(200, 22);
            this.tsmViewLocalDirectory.Text = "Images Directory...";
            this.tsmViewLocalDirectory.Click += new System.EventHandler(this.tsmViewDirectory_Click);
            // 
            // tsmFTPClient
            // 
            this.tsmFTPClient.Image = global::ZScreenGUI.Properties.Resources.server_edit;
            this.tsmFTPClient.Name = "tsmFTPClient";
            this.tsmFTPClient.Size = new System.Drawing.Size(200, 22);
            this.tsmFTPClient.Text = "FTP &Client...";
            this.tsmFTPClient.Click += new System.EventHandler(this.tsmFTPClient_Click);
            // 
            // tsmHelp
            // 
            this.tsmHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmLicense,
            this.tsmVersionHistory,
            this.tsmAbout});
            this.tsmHelp.Image = global::ZScreenGUI.Properties.Resources.help;
            this.tsmHelp.Name = "tsmHelp";
            this.tsmHelp.Size = new System.Drawing.Size(200, 22);
            this.tsmHelp.Text = "&Help";
            // 
            // tsmLicense
            // 
            this.tsmLicense.Image = global::ZScreenGUI.Properties.Resources.note_error;
            this.tsmLicense.Name = "tsmLicense";
            this.tsmLicense.Size = new System.Drawing.Size(169, 22);
            this.tsmLicense.Text = "License...";
            this.tsmLicense.Click += new System.EventHandler(this.tsmLic_Click);
            // 
            // tsmVersionHistory
            // 
            this.tsmVersionHistory.Image = global::ZScreenGUI.Properties.Resources.page_white_text;
            this.tsmVersionHistory.Name = "tsmVersionHistory";
            this.tsmVersionHistory.Size = new System.Drawing.Size(169, 22);
            this.tsmVersionHistory.Text = "&Version History...";
            this.tsmVersionHistory.Click += new System.EventHandler(this.cmVersionHistory_Click);
            // 
            // tsmAbout
            // 
            this.tsmAbout.Image = global::ZScreenGUI.Properties.Resources.information;
            this.tsmAbout.Name = "tsmAbout";
            this.tsmAbout.Size = new System.Drawing.Size(169, 22);
            this.tsmAbout.Text = "About...";
            this.tsmAbout.Click += new System.EventHandler(this.tsmAboutMain_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(197, 6);
            // 
            // tsmExitZScreen
            // 
            this.tsmExitZScreen.Image = global::ZScreenGUI.Properties.Resources.cross;
            this.tsmExitZScreen.Name = "tsmExitZScreen";
            this.tsmExitZScreen.Size = new System.Drawing.Size(200, 22);
            this.tsmExitZScreen.Text = "Exit ZScreen";
            this.tsmExitZScreen.Click += new System.EventHandler(this.tsmExitZScreen_Click);
            // 
            // tsmActions
            // 
            this.tsmActions.Name = "tsmActions";
            this.tsmActions.Size = new System.Drawing.Size(32, 19);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(179, 6);
            // 
            // tmrApp
            // 
            this.tmrApp.Enabled = true;
            this.tmrApp.Interval = 21600000;
            this.tmrApp.Tick += new System.EventHandler(this.tmrApp_Tick);
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpMain);
            this.tcMain.Controls.Add(this.tpHotkeys);
            this.tcMain.Controls.Add(this.tpMainInput);
            this.tcMain.Controls.Add(this.tpAdvanced);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(2, 26);
            this.tcMain.Multiline = true;
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(825, 446);
            this.tcMain.TabIndex = 1;
            this.tcMain.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tcMain_Selecting);
            // 
            // tpMain
            // 
            this.tpMain.AllowDrop = true;
            this.tpMain.BackColor = System.Drawing.Color.White;
            this.tpMain.Controls.Add(this.lblScreenshotDelay);
            this.tpMain.Controls.Add(this.nudScreenshotDelay);
            this.tpMain.Controls.Add(this.tsLinks);
            this.tpMain.Controls.Add(this.gbImageSettings);
            this.tpMain.Controls.Add(this.ucDestOptions);
            this.tpMain.Controls.Add(this.pbPreview);
            this.tpMain.ImageKey = "(none)";
            this.tpMain.Location = new System.Drawing.Point(4, 22);
            this.tpMain.Name = "tpMain";
            this.tpMain.Padding = new System.Windows.Forms.Padding(3);
            this.tpMain.Size = new System.Drawing.Size(817, 420);
            this.tpMain.TabIndex = 0;
            this.tpMain.Text = "Main";
            this.tpMain.DragDrop += new System.Windows.Forms.DragEventHandler(this.tpMain_DragDrop);
            this.tpMain.DragEnter += new System.Windows.Forms.DragEventHandler(this.tpMain_DragEnter);
            this.tpMain.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tpMain_MouseClick);
            this.tpMain.MouseLeave += new System.EventHandler(this.tpMain_MouseLeave);
            // 
            // lblScreenshotDelay
            // 
            this.lblScreenshotDelay.AutoSize = true;
            this.lblScreenshotDelay.Location = new System.Drawing.Point(32, 374);
            this.lblScreenshotDelay.Name = "lblScreenshotDelay";
            this.lblScreenshotDelay.Size = new System.Drawing.Size(94, 13);
            this.lblScreenshotDelay.TabIndex = 2;
            this.lblScreenshotDelay.Text = "Screenshot Delay:";
            // 
            // nudScreenshotDelay
            // 
            this.nudScreenshotDelay.Location = new System.Drawing.Point(136, 368);
            this.nudScreenshotDelay.Margin = new System.Windows.Forms.Padding(4);
            this.nudScreenshotDelay.Name = "nudScreenshotDelay";
            this.nudScreenshotDelay.RealValue = ((long)(0));
            this.nudScreenshotDelay.Size = new System.Drawing.Size(208, 24);
            this.nudScreenshotDelay.TabIndex = 3;
            this.nudScreenshotDelay.Tag = "Test";
            this.nudScreenshotDelay.Time = ZScreenLib.Times.Milliseconds;
            this.ttZScreen.SetToolTip(this.nudScreenshotDelay, "Specify the amount of time to wait before taking a screenshot.");
            this.nudScreenshotDelay.Value = ((long)(0));
            this.nudScreenshotDelay.ValueChanged += new System.EventHandler(this.numericUpDownTimer1_ValueChanged);
            this.nudScreenshotDelay.SelectedIndexChanged += new System.EventHandler(this.nudtScreenshotDelay_SelectedIndexChanged);
            this.nudScreenshotDelay.MouseHover += new System.EventHandler(this.nudtScreenshotDelay_MouseHover);
            // 
            // tsLinks
            // 
            this.tsLinks.AutoSize = false;
            this.tsLinks.BackColor = System.Drawing.Color.White;
            this.tsLinks.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tsLinks.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsLinks.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbLinkTutorials,
            this.tsbLinkBugs,
            this.tsbLinkHome});
            this.tsLinks.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.tsLinks.Location = new System.Drawing.Point(3, 393);
            this.tsLinks.Name = "tsLinks";
            this.tsLinks.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tsLinks.Size = new System.Drawing.Size(811, 24);
            this.tsLinks.TabIndex = 5;
            this.tsLinks.Text = "toolStrip1";
            // 
            // tsbLinkTutorials
            // 
            this.tsbLinkTutorials.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbLinkTutorials.Image = global::ZScreenGUI.Properties.Resources.help;
            this.tsbLinkTutorials.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLinkTutorials.Name = "tsbLinkTutorials";
            this.tsbLinkTutorials.Size = new System.Drawing.Size(68, 21);
            this.tsbLinkTutorials.Text = "Tutorials";
            this.tsbLinkTutorials.Click += new System.EventHandler(this.tsbLinkHelp_Click);
            // 
            // tsbLinkBugs
            // 
            this.tsbLinkBugs.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbLinkBugs.Image = global::ZScreenGUI.Properties.Resources.bug;
            this.tsbLinkBugs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLinkBugs.Name = "tsbLinkBugs";
            this.tsbLinkBugs.Size = new System.Drawing.Size(117, 21);
            this.tsbLinkBugs.Text = "Bugs/Suggestions?";
            this.tsbLinkBugs.Click += new System.EventHandler(this.tsbLinkIssues_Click);
            // 
            // tsbLinkHome
            // 
            this.tsbLinkHome.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbLinkHome.Image = global::ZScreenGUI.Properties.Resources.world_go;
            this.tsbLinkHome.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLinkHome.Name = "tsbLinkHome";
            this.tsbLinkHome.Size = new System.Drawing.Size(81, 21);
            this.tsbLinkHome.Text = "Home Page";
            this.tsbLinkHome.Click += new System.EventHandler(this.tsbLinkHome_Click);
            // 
            // gbImageSettings
            // 
            this.gbImageSettings.Controls.Add(this.chkShowUploadResults);
            this.gbImageSettings.Controls.Add(this.btnActionsUI);
            this.gbImageSettings.Controls.Add(this.chkShortenURL);
            this.gbImageSettings.Controls.Add(this.chkPerformActions);
            this.gbImageSettings.Controls.Add(this.chkShowWorkflowWizard);
            this.gbImageSettings.Location = new System.Drawing.Point(16, 224);
            this.gbImageSettings.Name = "gbImageSettings";
            this.gbImageSettings.Size = new System.Drawing.Size(352, 128);
            this.gbImageSettings.TabIndex = 1;
            this.gbImageSettings.TabStop = false;
            this.gbImageSettings.Text = "Quick Settings";
            // 
            // chkShowUploadResults
            // 
            this.chkShowUploadResults.AutoSize = true;
            this.chkShowUploadResults.Location = new System.Drawing.Point(16, 96);
            this.chkShowUploadResults.Name = "chkShowUploadResults";
            this.chkShowUploadResults.Size = new System.Drawing.Size(245, 17);
            this.chkShowUploadResults.TabIndex = 4;
            this.chkShowUploadResults.Text = "Show Upload Results window after completion";
            this.chkShowUploadResults.UseVisualStyleBackColor = true;
            this.chkShowUploadResults.CheckedChanged += new System.EventHandler(this.chkShowUploadResults_CheckedChanged);
            // 
            // btnActionsUI
            // 
            this.btnActionsUI.AutoSize = true;
            this.btnActionsUI.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnActionsUI.Location = new System.Drawing.Point(264, 18);
            this.btnActionsUI.Name = "btnActionsUI";
            this.btnActionsUI.Size = new System.Drawing.Size(26, 23);
            this.btnActionsUI.TabIndex = 1;
            this.btnActionsUI.Text = "...";
            this.ttZScreen.SetToolTip(this.btnActionsUI, "Configure Actions...");
            this.btnActionsUI.UseVisualStyleBackColor = true;
            this.btnActionsUI.Click += new System.EventHandler(this.btnActionsUI_Click);
            // 
            // chkShortenURL
            // 
            this.chkShortenURL.AutoSize = true;
            this.chkShortenURL.Location = new System.Drawing.Point(16, 72);
            this.chkShortenURL.Name = "chkShortenURL";
            this.chkShortenURL.Size = new System.Drawing.Size(190, 17);
            this.chkShortenURL.TabIndex = 3;
            this.chkShortenURL.Text = "Shorten URL if the URL is too long";
            this.chkShortenURL.UseVisualStyleBackColor = true;
            this.chkShortenURL.CheckedChanged += new System.EventHandler(this.chkShortenURL_CheckedChanged);
            // 
            // chkPerformActions
            // 
            this.chkPerformActions.AutoSize = true;
            this.chkPerformActions.Location = new System.Drawing.Point(16, 24);
            this.chkPerformActions.Name = "chkPerformActions";
            this.chkPerformActions.Size = new System.Drawing.Size(231, 17);
            this.chkPerformActions.TabIndex = 0;
            this.chkPerformActions.Text = "Perform &Actions before reaching destination";
            this.chkPerformActions.UseVisualStyleBackColor = true;
            this.chkPerformActions.CheckedChanged += new System.EventHandler(this.ChkEditorsEnableCheckedChanged);
            // 
            // chkShowWorkflowWizard
            // 
            this.chkShowWorkflowWizard.AutoSize = true;
            this.chkShowWorkflowWizard.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkShowWorkflowWizard.Location = new System.Drawing.Point(16, 48);
            this.chkShowWorkflowWizard.Name = "chkShowWorkflowWizard";
            this.chkShowWorkflowWizard.Size = new System.Drawing.Size(158, 17);
            this.chkShowWorkflowWizard.TabIndex = 2;
            this.chkShowWorkflowWizard.Text = "Prompt for Workflow Wizard";
            this.ttZScreen.SetToolTip(this.chkShowWorkflowWizard, "When enabled a prompt will be displayed when each\r\nscreenshot is taken allowing y" +
                    "ou to manually specify a filename.");
            this.chkShowWorkflowWizard.UseVisualStyleBackColor = true;
            this.chkShowWorkflowWizard.CheckedChanged += new System.EventHandler(this.chkManualNaming_CheckedChanged);
            // 
            // ucDestOptions
            // 
            this.ucDestOptions.Location = new System.Drawing.Point(16, 16);
            this.ucDestOptions.Margin = new System.Windows.Forms.Padding(4);
            this.ucDestOptions.Name = "ucDestOptions";
            this.ucDestOptions.Size = new System.Drawing.Size(352, 200);
            this.ucDestOptions.TabIndex = 0;
            this.ttZScreen.SetToolTip(this.ucDestOptions, "To configure destination options go to Destinations tab");
            // 
            // pbPreview
            // 
            this.pbPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbPreview.BackColor = System.Drawing.Color.White;
            this.pbPreview.DisableViewer = false;
            this.pbPreview.Location = new System.Drawing.Point(392, 3);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new System.Drawing.Size(422, 406);
            this.pbPreview.TabIndex = 4;
            // 
            // tpHotkeys
            // 
            this.tpHotkeys.Controls.Add(this.hmHotkeys);
            this.tpHotkeys.Controls.Add(this.btnResetHotkeys);
            this.tpHotkeys.Controls.Add(this.lblHotkeyStatus);
            this.tpHotkeys.ImageKey = "(none)";
            this.tpHotkeys.Location = new System.Drawing.Point(4, 22);
            this.tpHotkeys.Name = "tpHotkeys";
            this.tpHotkeys.Padding = new System.Windows.Forms.Padding(3);
            this.tpHotkeys.Size = new System.Drawing.Size(817, 420);
            this.tpHotkeys.TabIndex = 1;
            this.tpHotkeys.Text = "Hotkeys";
            this.tpHotkeys.UseVisualStyleBackColor = true;
            // 
            // hmHotkeys
            // 
            this.hmHotkeys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hmHotkeys.Location = new System.Drawing.Point(3, 3);
            this.hmHotkeys.Name = "hmHotkeys";
            this.hmHotkeys.Size = new System.Drawing.Size(811, 391);
            this.hmHotkeys.TabIndex = 0;
            // 
            // btnResetHotkeys
            // 
            this.btnResetHotkeys.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetHotkeys.AutoSize = true;
            this.btnResetHotkeys.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnResetHotkeys.Location = new System.Drawing.Point(674, 336);
            this.btnResetHotkeys.Name = "btnResetHotkeys";
            this.btnResetHotkeys.Size = new System.Drawing.Size(101, 23);
            this.btnResetHotkeys.TabIndex = 1;
            this.btnResetHotkeys.Text = "Reset &All Hotkeys";
            this.btnResetHotkeys.UseVisualStyleBackColor = true;
            // 
            // lblHotkeyStatus
            // 
            this.lblHotkeyStatus.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblHotkeyStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHotkeyStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblHotkeyStatus.Location = new System.Drawing.Point(3, 394);
            this.lblHotkeyStatus.Name = "lblHotkeyStatus";
            this.lblHotkeyStatus.Size = new System.Drawing.Size(811, 23);
            this.lblHotkeyStatus.TabIndex = 2;
            this.lblHotkeyStatus.Text = "Click on a Hotkey to set";
            this.lblHotkeyStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tpMainInput
            // 
            this.tpMainInput.Controls.Add(this.tcCapture);
            this.tpMainInput.ImageKey = "(none)";
            this.tpMainInput.Location = new System.Drawing.Point(4, 22);
            this.tpMainInput.Name = "tpMainInput";
            this.tpMainInput.Padding = new System.Windows.Forms.Padding(3);
            this.tpMainInput.Size = new System.Drawing.Size(817, 420);
            this.tpMainInput.TabIndex = 2;
            this.tpMainInput.Text = "Capture";
            this.tpMainInput.UseVisualStyleBackColor = true;
            // 
            // tcCapture
            // 
            this.tcCapture.Controls.Add(this.tpActivewindow);
            this.tcCapture.Controls.Add(this.tpSelectedWindow);
            this.tcCapture.Controls.Add(this.tpCropShot);
            this.tcCapture.Controls.Add(this.tpCropShotLast);
            this.tcCapture.Controls.Add(this.tpCaptureShape);
            this.tcCapture.Controls.Add(this.tpFreehandCropShot);
            this.tcCapture.Controls.Add(this.tpCaptureClipboard);
            this.tcCapture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcCapture.Location = new System.Drawing.Point(3, 3);
            this.tcCapture.Name = "tcCapture";
            this.tcCapture.SelectedIndex = 0;
            this.tcCapture.Size = new System.Drawing.Size(811, 414);
            this.tcCapture.TabIndex = 0;
            // 
            // tpActivewindow
            // 
            this.tpActivewindow.Controls.Add(this.gbCaptureDwm);
            this.tpActivewindow.Controls.Add(this.gbCaptureEngine);
            this.tpActivewindow.Controls.Add(this.gbCaptureGdi);
            this.tpActivewindow.ImageKey = "application.png";
            this.tpActivewindow.Location = new System.Drawing.Point(4, 22);
            this.tpActivewindow.Name = "tpActivewindow";
            this.tpActivewindow.Padding = new System.Windows.Forms.Padding(3);
            this.tpActivewindow.Size = new System.Drawing.Size(803, 388);
            this.tpActivewindow.TabIndex = 0;
            this.tpActivewindow.Text = "Active Window";
            this.tpActivewindow.UseVisualStyleBackColor = true;
            // 
            // gbCaptureDwm
            // 
            this.gbCaptureDwm.Controls.Add(this.chkSelectedWindowIncludeShadow);
            this.gbCaptureDwm.Controls.Add(this.pbActiveWindowDwmBackColor);
            this.gbCaptureDwm.Controls.Add(this.chkActiveWindowDwmCustomColor);
            this.gbCaptureDwm.Location = new System.Drawing.Point(8, 152);
            this.gbCaptureDwm.Name = "gbCaptureDwm";
            this.gbCaptureDwm.Size = new System.Drawing.Size(512, 80);
            this.gbCaptureDwm.TabIndex = 1;
            this.gbCaptureDwm.TabStop = false;
            this.gbCaptureDwm.Text = "DWM";
            // 
            // chkSelectedWindowIncludeShadow
            // 
            this.chkSelectedWindowIncludeShadow.AutoSize = true;
            this.chkSelectedWindowIncludeShadow.Location = new System.Drawing.Point(16, 24);
            this.chkSelectedWindowIncludeShadow.Name = "chkSelectedWindowIncludeShadow";
            this.chkSelectedWindowIncludeShadow.Size = new System.Drawing.Size(139, 17);
            this.chkSelectedWindowIncludeShadow.TabIndex = 0;
            this.chkSelectedWindowIncludeShadow.Text = "Add drop shadow effect";
            this.ttZScreen.SetToolTip(this.chkSelectedWindowIncludeShadow, "Captures the real window shadow (GDI on Vista & 7), or fake it (DWM, XP)");
            this.chkSelectedWindowIncludeShadow.UseVisualStyleBackColor = true;
            this.chkSelectedWindowIncludeShadow.CheckedChanged += new System.EventHandler(this.cbSelectedWindowIncludeShadow_CheckedChanged);
            // 
            // pbActiveWindowDwmBackColor
            // 
            this.pbActiveWindowDwmBackColor.BackColor = System.Drawing.Color.White;
            this.pbActiveWindowDwmBackColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbActiveWindowDwmBackColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbActiveWindowDwmBackColor.Location = new System.Drawing.Point(304, 40);
            this.pbActiveWindowDwmBackColor.Name = "pbActiveWindowDwmBackColor";
            this.pbActiveWindowDwmBackColor.Size = new System.Drawing.Size(24, 24);
            this.pbActiveWindowDwmBackColor.TabIndex = 127;
            this.pbActiveWindowDwmBackColor.TabStop = false;
            this.pbActiveWindowDwmBackColor.Click += new System.EventHandler(this.pbActiveWindowDwmBackColor_Click);
            // 
            // chkActiveWindowDwmCustomColor
            // 
            this.chkActiveWindowDwmCustomColor.AutoSize = true;
            this.chkActiveWindowDwmCustomColor.Location = new System.Drawing.Point(16, 47);
            this.chkActiveWindowDwmCustomColor.Name = "chkActiveWindowDwmCustomColor";
            this.chkActiveWindowDwmCustomColor.Size = new System.Drawing.Size(255, 17);
            this.chkActiveWindowDwmCustomColor.TabIndex = 1;
            this.chkActiveWindowDwmCustomColor.Text = "Clear \"dirty\" Aero background  with custom color";
            this.chkActiveWindowDwmCustomColor.UseVisualStyleBackColor = true;
            this.chkActiveWindowDwmCustomColor.CheckedChanged += new System.EventHandler(this.chkActiveWindowDwmCustomColor_CheckedChanged);
            // 
            // gbCaptureEngine
            // 
            this.gbCaptureEngine.Controls.Add(this.chkActiveWindowCleanBackground);
            this.gbCaptureEngine.Controls.Add(this.chkSelectedWindowShowCheckers);
            this.gbCaptureEngine.Controls.Add(this.cboCaptureEngine);
            this.gbCaptureEngine.Controls.Add(this.chkShowCursor);
            this.gbCaptureEngine.Location = new System.Drawing.Point(8, 8);
            this.gbCaptureEngine.Name = "gbCaptureEngine";
            this.gbCaptureEngine.Size = new System.Drawing.Size(512, 136);
            this.gbCaptureEngine.TabIndex = 0;
            this.gbCaptureEngine.TabStop = false;
            this.gbCaptureEngine.Text = "Capture Engine of Choice";
            // 
            // chkActiveWindowCleanBackground
            // 
            this.chkActiveWindowCleanBackground.AutoSize = true;
            this.chkActiveWindowCleanBackground.Location = new System.Drawing.Point(16, 80);
            this.chkActiveWindowCleanBackground.Name = "chkActiveWindowCleanBackground";
            this.chkActiveWindowCleanBackground.Size = new System.Drawing.Size(421, 17);
            this.chkActiveWindowCleanBackground.TabIndex = 2;
            this.chkActiveWindowCleanBackground.Text = "Enable transparency (and clear \"dirty\" Aero background in Windows Vista or higher" +
                ")";
            this.ttZScreen.SetToolTip(this.chkActiveWindowCleanBackground, "Clears background area that does not belong to the Active Window");
            this.chkActiveWindowCleanBackground.UseVisualStyleBackColor = true;
            this.chkActiveWindowCleanBackground.CheckedChanged += new System.EventHandler(this.cbSelectedWindowCleanBackground_CheckedChanged);
            // 
            // chkSelectedWindowShowCheckers
            // 
            this.chkSelectedWindowShowCheckers.AutoSize = true;
            this.chkSelectedWindowShowCheckers.Location = new System.Drawing.Point(16, 104);
            this.chkSelectedWindowShowCheckers.Name = "chkSelectedWindowShowCheckers";
            this.chkSelectedWindowShowCheckers.Size = new System.Drawing.Size(242, 17);
            this.chkSelectedWindowShowCheckers.TabIndex = 3;
            this.chkSelectedWindowShowCheckers.Text = "Show checkerboard pattern behind the image";
            this.ttZScreen.SetToolTip(this.chkSelectedWindowShowCheckers, "Useful to visualize transparency");
            this.chkSelectedWindowShowCheckers.UseVisualStyleBackColor = true;
            this.chkSelectedWindowShowCheckers.CheckedChanged += new System.EventHandler(this.cbSelectedWindowShowCheckers_CheckedChanged);
            // 
            // cboCaptureEngine
            // 
            this.cboCaptureEngine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCaptureEngine.FormattingEnabled = true;
            this.cboCaptureEngine.Location = new System.Drawing.Point(16, 24);
            this.cboCaptureEngine.Name = "cboCaptureEngine";
            this.cboCaptureEngine.Size = new System.Drawing.Size(480, 21);
            this.cboCaptureEngine.TabIndex = 0;
            this.cboCaptureEngine.SelectedIndexChanged += new System.EventHandler(this.cboCaptureEngine_SelectedIndexChanged);
            // 
            // chkShowCursor
            // 
            this.chkShowCursor.AutoSize = true;
            this.chkShowCursor.Location = new System.Drawing.Point(16, 56);
            this.chkShowCursor.Name = "chkShowCursor";
            this.chkShowCursor.Size = new System.Drawing.Size(159, 17);
            this.chkShowCursor.TabIndex = 1;
            this.chkShowCursor.Text = "Show Cursor in Screenshots";
            this.ttZScreen.SetToolTip(this.chkShowCursor, "When enabled your mouse cursor icon will be captured \r\nas it appeared when the sc" +
                    "reenshot was taken.");
            this.chkShowCursor.UseVisualStyleBackColor = true;
            this.chkShowCursor.CheckedChanged += new System.EventHandler(this.cbShowCursor_CheckedChanged);
            // 
            // gbCaptureGdi
            // 
            this.gbCaptureGdi.Controls.Add(this.chkActiveWindowTryCaptureChildren);
            this.gbCaptureGdi.Location = new System.Drawing.Point(8, 264);
            this.gbCaptureGdi.Name = "gbCaptureGdi";
            this.gbCaptureGdi.Size = new System.Drawing.Size(512, 56);
            this.gbCaptureGdi.TabIndex = 2;
            this.gbCaptureGdi.TabStop = false;
            this.gbCaptureGdi.Text = "GDI";
            this.gbCaptureGdi.Visible = false;
            // 
            // chkActiveWindowTryCaptureChildren
            // 
            this.chkActiveWindowTryCaptureChildren.AutoSize = true;
            this.chkActiveWindowTryCaptureChildren.Location = new System.Drawing.Point(16, 24);
            this.chkActiveWindowTryCaptureChildren.Name = "chkActiveWindowTryCaptureChildren";
            this.chkActiveWindowTryCaptureChildren.Size = new System.Drawing.Size(235, 17);
            this.chkActiveWindowTryCaptureChildren.TabIndex = 0;
            this.chkActiveWindowTryCaptureChildren.Text = "Capture Child Windows, Tooltips and Menus";
            this.ttZScreen.SetToolTip(this.chkActiveWindowTryCaptureChildren, "Only works when DWM is disabled");
            this.chkActiveWindowTryCaptureChildren.UseVisualStyleBackColor = true;
            this.chkActiveWindowTryCaptureChildren.CheckedChanged += new System.EventHandler(this.chkActiveWindowTryCaptureChilds_CheckedChanged);
            // 
            // tpSelectedWindow
            // 
            this.tpSelectedWindow.Controls.Add(this.chkSelectedWindowCaptureObjects);
            this.tpSelectedWindow.Controls.Add(this.nudSelectedWindowHueRange);
            this.tpSelectedWindow.Controls.Add(this.lblSelectedWindowHueRange);
            this.tpSelectedWindow.Controls.Add(this.nudSelectedWindowRegionStep);
            this.tpSelectedWindow.Controls.Add(this.nudSelectedWindowRegionInterval);
            this.tpSelectedWindow.Controls.Add(this.lblSelectedWindowRegionStep);
            this.tpSelectedWindow.Controls.Add(this.lblSelectedWindowRegionInterval);
            this.tpSelectedWindow.Controls.Add(this.cbSelectedWindowDynamicBorderColor);
            this.tpSelectedWindow.Controls.Add(this.cbSelectedWindowRuler);
            this.tpSelectedWindow.Controls.Add(this.lblSelectedWindowRegionStyle);
            this.tpSelectedWindow.Controls.Add(this.cbSelectedWindowStyle);
            this.tpSelectedWindow.Controls.Add(this.cbSelectedWindowRectangleInfo);
            this.tpSelectedWindow.Controls.Add(this.lblSelectedWindowBorderColor);
            this.tpSelectedWindow.Controls.Add(this.nudSelectedWindowBorderSize);
            this.tpSelectedWindow.Controls.Add(this.lblSelectedWindowBorderSize);
            this.tpSelectedWindow.Controls.Add(this.pbSelectedWindowBorderColor);
            this.tpSelectedWindow.ImageKey = "application_double.png";
            this.tpSelectedWindow.Location = new System.Drawing.Point(4, 22);
            this.tpSelectedWindow.Name = "tpSelectedWindow";
            this.tpSelectedWindow.Size = new System.Drawing.Size(803, 388);
            this.tpSelectedWindow.TabIndex = 1;
            this.tpSelectedWindow.Text = "Selected Window";
            this.tpSelectedWindow.UseVisualStyleBackColor = true;
            // 
            // chkSelectedWindowCaptureObjects
            // 
            this.chkSelectedWindowCaptureObjects.AutoSize = true;
            this.chkSelectedWindowCaptureObjects.Location = new System.Drawing.Point(16, 272);
            this.chkSelectedWindowCaptureObjects.Name = "chkSelectedWindowCaptureObjects";
            this.chkSelectedWindowCaptureObjects.Size = new System.Drawing.Size(231, 17);
            this.chkSelectedWindowCaptureObjects.TabIndex = 14;
            this.chkSelectedWindowCaptureObjects.Text = "Capture control objects within each window";
            this.chkSelectedWindowCaptureObjects.UseVisualStyleBackColor = true;
            this.chkSelectedWindowCaptureObjects.CheckedChanged += new System.EventHandler(this.cbSelectedWindowCaptureObjects_CheckedChanged);
            // 
            // nudSelectedWindowHueRange
            // 
            this.nudSelectedWindowHueRange.Location = new System.Drawing.Point(216, 232);
            this.nudSelectedWindowHueRange.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.nudSelectedWindowHueRange.Name = "nudSelectedWindowHueRange";
            this.nudSelectedWindowHueRange.Size = new System.Drawing.Size(56, 20);
            this.nudSelectedWindowHueRange.TabIndex = 13;
            this.nudSelectedWindowHueRange.ValueChanged += new System.EventHandler(this.nudSelectedWindowHueRange_ValueChanged);
            // 
            // lblSelectedWindowHueRange
            // 
            this.lblSelectedWindowHueRange.AutoSize = true;
            this.lblSelectedWindowHueRange.Location = new System.Drawing.Point(16, 236);
            this.lblSelectedWindowHueRange.Name = "lblSelectedWindowHueRange";
            this.lblSelectedWindowHueRange.Size = new System.Drawing.Size(193, 13);
            this.lblSelectedWindowHueRange.TabIndex = 12;
            this.lblSelectedWindowHueRange.Text = "Dynamic region border color hue range:";
            // 
            // nudSelectedWindowRegionStep
            // 
            this.nudSelectedWindowRegionStep.Location = new System.Drawing.Point(163, 200);
            this.nudSelectedWindowRegionStep.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSelectedWindowRegionStep.Name = "nudSelectedWindowRegionStep";
            this.nudSelectedWindowRegionStep.Size = new System.Drawing.Size(56, 20);
            this.nudSelectedWindowRegionStep.TabIndex = 11;
            this.nudSelectedWindowRegionStep.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSelectedWindowRegionStep.ValueChanged += new System.EventHandler(this.nudSelectedWindowRegionStep_ValueChanged);
            // 
            // nudSelectedWindowRegionInterval
            // 
            this.nudSelectedWindowRegionInterval.Location = new System.Drawing.Point(64, 200);
            this.nudSelectedWindowRegionInterval.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudSelectedWindowRegionInterval.Name = "nudSelectedWindowRegionInterval";
            this.nudSelectedWindowRegionInterval.Size = new System.Drawing.Size(56, 20);
            this.nudSelectedWindowRegionInterval.TabIndex = 9;
            this.nudSelectedWindowRegionInterval.ValueChanged += new System.EventHandler(this.nudSelectedWindowRegionInterval_ValueChanged);
            // 
            // lblSelectedWindowRegionStep
            // 
            this.lblSelectedWindowRegionStep.AutoSize = true;
            this.lblSelectedWindowRegionStep.Location = new System.Drawing.Point(128, 203);
            this.lblSelectedWindowRegionStep.Name = "lblSelectedWindowRegionStep";
            this.lblSelectedWindowRegionStep.Size = new System.Drawing.Size(32, 13);
            this.lblSelectedWindowRegionStep.TabIndex = 10;
            this.lblSelectedWindowRegionStep.Text = "Step:";
            // 
            // lblSelectedWindowRegionInterval
            // 
            this.lblSelectedWindowRegionInterval.AutoSize = true;
            this.lblSelectedWindowRegionInterval.Location = new System.Drawing.Point(16, 203);
            this.lblSelectedWindowRegionInterval.Name = "lblSelectedWindowRegionInterval";
            this.lblSelectedWindowRegionInterval.Size = new System.Drawing.Size(45, 13);
            this.lblSelectedWindowRegionInterval.TabIndex = 8;
            this.lblSelectedWindowRegionInterval.Text = "Interval:";
            // 
            // cbSelectedWindowDynamicBorderColor
            // 
            this.cbSelectedWindowDynamicBorderColor.AutoSize = true;
            this.cbSelectedWindowDynamicBorderColor.Location = new System.Drawing.Point(16, 168);
            this.cbSelectedWindowDynamicBorderColor.Name = "cbSelectedWindowDynamicBorderColor";
            this.cbSelectedWindowDynamicBorderColor.Size = new System.Drawing.Size(158, 17);
            this.cbSelectedWindowDynamicBorderColor.TabIndex = 7;
            this.cbSelectedWindowDynamicBorderColor.Text = "Dynamic region border color";
            this.cbSelectedWindowDynamicBorderColor.UseVisualStyleBackColor = true;
            this.cbSelectedWindowDynamicBorderColor.CheckedChanged += new System.EventHandler(this.cbSelectedWindowDynamicBorderColor_CheckedChanged);
            // 
            // cbSelectedWindowRuler
            // 
            this.cbSelectedWindowRuler.AutoSize = true;
            this.cbSelectedWindowRuler.Location = new System.Drawing.Point(16, 72);
            this.cbSelectedWindowRuler.Name = "cbSelectedWindowRuler";
            this.cbSelectedWindowRuler.Size = new System.Drawing.Size(76, 17);
            this.cbSelectedWindowRuler.TabIndex = 3;
            this.cbSelectedWindowRuler.Text = "Show ruler";
            this.cbSelectedWindowRuler.UseVisualStyleBackColor = true;
            this.cbSelectedWindowRuler.CheckedChanged += new System.EventHandler(this.cbSelectedWindowRuler_CheckedChanged);
            // 
            // lblSelectedWindowRegionStyle
            // 
            this.lblSelectedWindowRegionStyle.AutoSize = true;
            this.lblSelectedWindowRegionStyle.Location = new System.Drawing.Point(16, 20);
            this.lblSelectedWindowRegionStyle.Name = "lblSelectedWindowRegionStyle";
            this.lblSelectedWindowRegionStyle.Size = new System.Drawing.Size(147, 13);
            this.lblSelectedWindowRegionStyle.TabIndex = 0;
            this.lblSelectedWindowRegionStyle.Text = "Selected window region style:";
            // 
            // cbSelectedWindowStyle
            // 
            this.cbSelectedWindowStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSelectedWindowStyle.FormattingEnabled = true;
            this.cbSelectedWindowStyle.Location = new System.Drawing.Point(168, 16);
            this.cbSelectedWindowStyle.Name = "cbSelectedWindowStyle";
            this.cbSelectedWindowStyle.Size = new System.Drawing.Size(208, 21);
            this.cbSelectedWindowStyle.TabIndex = 1;
            this.cbSelectedWindowStyle.SelectedIndexChanged += new System.EventHandler(this.cbSelectedWindowStyle_SelectedIndexChanged);
            // 
            // cbSelectedWindowRectangleInfo
            // 
            this.cbSelectedWindowRectangleInfo.AutoSize = true;
            this.cbSelectedWindowRectangleInfo.Location = new System.Drawing.Point(16, 48);
            this.cbSelectedWindowRectangleInfo.Name = "cbSelectedWindowRectangleInfo";
            this.cbSelectedWindowRectangleInfo.Size = new System.Drawing.Size(267, 17);
            this.cbSelectedWindowRectangleInfo.TabIndex = 2;
            this.cbSelectedWindowRectangleInfo.Text = "Show selected window region coordinates and size";
            this.cbSelectedWindowRectangleInfo.UseVisualStyleBackColor = true;
            this.cbSelectedWindowRectangleInfo.CheckedChanged += new System.EventHandler(this.cbSelectedWindowRectangleInfo_CheckedChanged);
            // 
            // lblSelectedWindowBorderColor
            // 
            this.lblSelectedWindowBorderColor.AutoSize = true;
            this.lblSelectedWindowBorderColor.Location = new System.Drawing.Point(16, 104);
            this.lblSelectedWindowBorderColor.Name = "lblSelectedWindowBorderColor";
            this.lblSelectedWindowBorderColor.Size = new System.Drawing.Size(103, 13);
            this.lblSelectedWindowBorderColor.TabIndex = 4;
            this.lblSelectedWindowBorderColor.Text = "Region border color:";
            // 
            // nudSelectedWindowBorderSize
            // 
            this.nudSelectedWindowBorderSize.Location = new System.Drawing.Point(200, 136);
            this.nudSelectedWindowBorderSize.Name = "nudSelectedWindowBorderSize";
            this.nudSelectedWindowBorderSize.Size = new System.Drawing.Size(56, 20);
            this.nudSelectedWindowBorderSize.TabIndex = 6;
            this.nudSelectedWindowBorderSize.ValueChanged += new System.EventHandler(this.nudSelectedWindowBorderSize_ValueChanged);
            // 
            // lblSelectedWindowBorderSize
            // 
            this.lblSelectedWindowBorderSize.AutoSize = true;
            this.lblSelectedWindowBorderSize.Location = new System.Drawing.Point(16, 139);
            this.lblSelectedWindowBorderSize.Name = "lblSelectedWindowBorderSize";
            this.lblSelectedWindowBorderSize.Size = new System.Drawing.Size(175, 13);
            this.lblSelectedWindowBorderSize.TabIndex = 5;
            this.lblSelectedWindowBorderSize.Text = "Region border size ( 0 = No border )";
            // 
            // pbSelectedWindowBorderColor
            // 
            this.pbSelectedWindowBorderColor.BackColor = System.Drawing.Color.White;
            this.pbSelectedWindowBorderColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbSelectedWindowBorderColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbSelectedWindowBorderColor.Location = new System.Drawing.Point(128, 101);
            this.pbSelectedWindowBorderColor.Name = "pbSelectedWindowBorderColor";
            this.pbSelectedWindowBorderColor.Size = new System.Drawing.Size(56, 20);
            this.pbSelectedWindowBorderColor.TabIndex = 3;
            this.pbSelectedWindowBorderColor.TabStop = false;
            this.pbSelectedWindowBorderColor.Click += new System.EventHandler(this.pbSelectedWindowBorderColor_Click);
            // 
            // tpCropShot
            // 
            this.tpCropShot.Controls.Add(this.gbCropEngine);
            this.tpCropShot.Controls.Add(this.gbCropShotMagnifyingGlass);
            this.tpCropShot.Controls.Add(this.gbCropDynamicRegionBorderColorSettings);
            this.tpCropShot.Controls.Add(this.gbCropRegion);
            this.tpCropShot.Controls.Add(this.gbCropRegionSettings);
            this.tpCropShot.Controls.Add(this.gbCropCrosshairSettings);
            this.tpCropShot.Controls.Add(this.gbCropGridMode);
            this.tpCropShot.ImageKey = "shape_square.png";
            this.tpCropShot.Location = new System.Drawing.Point(4, 22);
            this.tpCropShot.Name = "tpCropShot";
            this.tpCropShot.Padding = new System.Windows.Forms.Padding(3);
            this.tpCropShot.Size = new System.Drawing.Size(803, 388);
            this.tpCropShot.TabIndex = 2;
            this.tpCropShot.Text = "Crop Shot";
            this.tpCropShot.UseVisualStyleBackColor = true;
            // 
            // gbCropEngine
            // 
            this.gbCropEngine.Controls.Add(this.cboCropEngine);
            this.gbCropEngine.Location = new System.Drawing.Point(8, 8);
            this.gbCropEngine.Name = "gbCropEngine";
            this.gbCropEngine.Size = new System.Drawing.Size(352, 56);
            this.gbCropEngine.TabIndex = 0;
            this.gbCropEngine.TabStop = false;
            this.gbCropEngine.Text = "Crop Engine of choice";
            // 
            // cboCropEngine
            // 
            this.cboCropEngine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCropEngine.FormattingEnabled = true;
            this.cboCropEngine.Location = new System.Drawing.Point(8, 24);
            this.cboCropEngine.Name = "cboCropEngine";
            this.cboCropEngine.Size = new System.Drawing.Size(336, 21);
            this.cboCropEngine.TabIndex = 0;
            this.cboCropEngine.SelectedIndexChanged += new System.EventHandler(this.cboCropEngine_SelectedIndexChanged);
            // 
            // gbCropShotMagnifyingGlass
            // 
            this.gbCropShotMagnifyingGlass.Controls.Add(this.chkCropShowMagnifyingGlass);
            this.gbCropShotMagnifyingGlass.Location = new System.Drawing.Point(368, 280);
            this.gbCropShotMagnifyingGlass.Name = "gbCropShotMagnifyingGlass";
            this.gbCropShotMagnifyingGlass.Size = new System.Drawing.Size(392, 56);
            this.gbCropShotMagnifyingGlass.TabIndex = 6;
            this.gbCropShotMagnifyingGlass.TabStop = false;
            this.gbCropShotMagnifyingGlass.Text = "Ease of Access";
            // 
            // chkCropShowMagnifyingGlass
            // 
            this.chkCropShowMagnifyingGlass.AutoSize = true;
            this.chkCropShowMagnifyingGlass.Location = new System.Drawing.Point(16, 24);
            this.chkCropShowMagnifyingGlass.Name = "chkCropShowMagnifyingGlass";
            this.chkCropShowMagnifyingGlass.Size = new System.Drawing.Size(133, 17);
            this.chkCropShowMagnifyingGlass.TabIndex = 0;
            this.chkCropShowMagnifyingGlass.Text = "Show magnifying glass";
            this.chkCropShowMagnifyingGlass.UseVisualStyleBackColor = true;
            this.chkCropShowMagnifyingGlass.CheckedChanged += new System.EventHandler(this.cbCropShowMagnifyingGlass_CheckedChanged);
            // 
            // gbCropDynamicRegionBorderColorSettings
            // 
            this.gbCropDynamicRegionBorderColorSettings.Controls.Add(this.nudCropRegionStep);
            this.gbCropDynamicRegionBorderColorSettings.Controls.Add(this.nudCropHueRange);
            this.gbCropDynamicRegionBorderColorSettings.Controls.Add(this.cbCropDynamicBorderColor);
            this.gbCropDynamicRegionBorderColorSettings.Controls.Add(this.lblCropRegionInterval);
            this.gbCropDynamicRegionBorderColorSettings.Controls.Add(this.lblCropHueRange);
            this.gbCropDynamicRegionBorderColorSettings.Controls.Add(this.lblCropRegionStep);
            this.gbCropDynamicRegionBorderColorSettings.Controls.Add(this.nudCropRegionInterval);
            this.gbCropDynamicRegionBorderColorSettings.Location = new System.Drawing.Point(368, 192);
            this.gbCropDynamicRegionBorderColorSettings.Name = "gbCropDynamicRegionBorderColorSettings";
            this.gbCropDynamicRegionBorderColorSettings.Size = new System.Drawing.Size(392, 80);
            this.gbCropDynamicRegionBorderColorSettings.TabIndex = 4;
            this.gbCropDynamicRegionBorderColorSettings.TabStop = false;
            this.gbCropDynamicRegionBorderColorSettings.Text = "Dynamic Region Border Color Settings";
            // 
            // nudCropRegionStep
            // 
            this.nudCropRegionStep.Location = new System.Drawing.Point(320, 20);
            this.nudCropRegionStep.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCropRegionStep.Name = "nudCropRegionStep";
            this.nudCropRegionStep.Size = new System.Drawing.Size(56, 20);
            this.nudCropRegionStep.TabIndex = 2;
            this.nudCropRegionStep.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCropRegionStep.ValueChanged += new System.EventHandler(this.nudCropRegionStep_ValueChanged);
            // 
            // nudCropHueRange
            // 
            this.nudCropHueRange.Location = new System.Drawing.Point(320, 52);
            this.nudCropHueRange.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.nudCropHueRange.Name = "nudCropHueRange";
            this.nudCropHueRange.Size = new System.Drawing.Size(56, 20);
            this.nudCropHueRange.TabIndex = 6;
            this.nudCropHueRange.ValueChanged += new System.EventHandler(this.nudCropHueRange_ValueChanged);
            // 
            // cbCropDynamicBorderColor
            // 
            this.cbCropDynamicBorderColor.AutoSize = true;
            this.cbCropDynamicBorderColor.Location = new System.Drawing.Point(16, 24);
            this.cbCropDynamicBorderColor.Name = "cbCropDynamicBorderColor";
            this.cbCropDynamicBorderColor.Size = new System.Drawing.Size(65, 17);
            this.cbCropDynamicBorderColor.TabIndex = 3;
            this.cbCropDynamicBorderColor.Text = "Enabled";
            this.cbCropDynamicBorderColor.UseVisualStyleBackColor = true;
            this.cbCropDynamicBorderColor.CheckedChanged += new System.EventHandler(this.cbCropDynamicBorderColor_CheckedChanged);
            // 
            // lblCropRegionInterval
            // 
            this.lblCropRegionInterval.AutoSize = true;
            this.lblCropRegionInterval.Location = new System.Drawing.Point(176, 24);
            this.lblCropRegionInterval.Name = "lblCropRegionInterval";
            this.lblCropRegionInterval.Size = new System.Drawing.Size(45, 13);
            this.lblCropRegionInterval.TabIndex = 0;
            this.lblCropRegionInterval.Text = "Interval:";
            // 
            // lblCropHueRange
            // 
            this.lblCropHueRange.AutoSize = true;
            this.lblCropHueRange.Location = new System.Drawing.Point(256, 56);
            this.lblCropHueRange.Name = "lblCropHueRange";
            this.lblCropHueRange.Size = new System.Drawing.Size(60, 13);
            this.lblCropHueRange.TabIndex = 5;
            this.lblCropHueRange.Text = "Hue range:";
            // 
            // lblCropRegionStep
            // 
            this.lblCropRegionStep.AutoSize = true;
            this.lblCropRegionStep.Location = new System.Drawing.Point(286, 24);
            this.lblCropRegionStep.Name = "lblCropRegionStep";
            this.lblCropRegionStep.Size = new System.Drawing.Size(32, 13);
            this.lblCropRegionStep.TabIndex = 4;
            this.lblCropRegionStep.Text = "Step:";
            // 
            // nudCropRegionInterval
            // 
            this.nudCropRegionInterval.Location = new System.Drawing.Point(224, 20);
            this.nudCropRegionInterval.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudCropRegionInterval.Name = "nudCropRegionInterval";
            this.nudCropRegionInterval.Size = new System.Drawing.Size(56, 20);
            this.nudCropRegionInterval.TabIndex = 1;
            this.nudCropRegionInterval.ValueChanged += new System.EventHandler(this.nudCropRegionInterval_ValueChanged);
            // 
            // gbCropRegion
            // 
            this.gbCropRegion.Controls.Add(this.lblCropRegionStyle);
            this.gbCropRegion.Controls.Add(this.chkRegionHotkeyInfo);
            this.gbCropRegion.Controls.Add(this.chkCropStyle);
            this.gbCropRegion.Controls.Add(this.chkRegionRectangleInfo);
            this.gbCropRegion.Location = new System.Drawing.Point(8, 72);
            this.gbCropRegion.Name = "gbCropRegion";
            this.gbCropRegion.Size = new System.Drawing.Size(352, 120);
            this.gbCropRegion.TabIndex = 2;
            this.gbCropRegion.TabStop = false;
            this.gbCropRegion.Text = "Crop Region Settings";
            // 
            // lblCropRegionStyle
            // 
            this.lblCropRegionStyle.AutoSize = true;
            this.lblCropRegionStyle.Location = new System.Drawing.Point(16, 28);
            this.lblCropRegionStyle.Name = "lblCropRegionStyle";
            this.lblCropRegionStyle.Size = new System.Drawing.Size(88, 13);
            this.lblCropRegionStyle.TabIndex = 0;
            this.lblCropRegionStyle.Text = "Crop region style:";
            // 
            // chkRegionHotkeyInfo
            // 
            this.chkRegionHotkeyInfo.AutoSize = true;
            this.chkRegionHotkeyInfo.Location = new System.Drawing.Point(16, 88);
            this.chkRegionHotkeyInfo.Name = "chkRegionHotkeyInfo";
            this.chkRegionHotkeyInfo.Size = new System.Drawing.Size(200, 17);
            this.chkRegionHotkeyInfo.TabIndex = 3;
            this.chkRegionHotkeyInfo.Text = "Show crop region hotkey instructions";
            this.chkRegionHotkeyInfo.UseVisualStyleBackColor = true;
            this.chkRegionHotkeyInfo.CheckedChanged += new System.EventHandler(this.cbRegionHotkeyInfo_CheckedChanged);
            // 
            // chkCropStyle
            // 
            this.chkCropStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.chkCropStyle.FormattingEnabled = true;
            this.chkCropStyle.Location = new System.Drawing.Point(120, 24);
            this.chkCropStyle.Name = "chkCropStyle";
            this.chkCropStyle.Size = new System.Drawing.Size(216, 21);
            this.chkCropStyle.TabIndex = 1;
            this.chkCropStyle.SelectedIndexChanged += new System.EventHandler(this.cbCropStyle_SelectedIndexChanged);
            // 
            // chkRegionRectangleInfo
            // 
            this.chkRegionRectangleInfo.AutoSize = true;
            this.chkRegionRectangleInfo.Location = new System.Drawing.Point(16, 64);
            this.chkRegionRectangleInfo.Name = "chkRegionRectangleInfo";
            this.chkRegionRectangleInfo.Size = new System.Drawing.Size(209, 17);
            this.chkRegionRectangleInfo.TabIndex = 2;
            this.chkRegionRectangleInfo.Text = "Show crop region coordinates and size";
            this.chkRegionRectangleInfo.UseVisualStyleBackColor = true;
            this.chkRegionRectangleInfo.CheckedChanged += new System.EventHandler(this.cbRegionRectangleInfo_CheckedChanged);
            // 
            // gbCropRegionSettings
            // 
            this.gbCropRegionSettings.Controls.Add(this.lblCropBorderSize);
            this.gbCropRegionSettings.Controls.Add(this.cbShowCropRuler);
            this.gbCropRegionSettings.Controls.Add(this.cbCropShowGrids);
            this.gbCropRegionSettings.Controls.Add(this.lblCropBorderColor);
            this.gbCropRegionSettings.Controls.Add(this.pbCropBorderColor);
            this.gbCropRegionSettings.Controls.Add(this.nudCropBorderSize);
            this.gbCropRegionSettings.Location = new System.Drawing.Point(368, 96);
            this.gbCropRegionSettings.Name = "gbCropRegionSettings";
            this.gbCropRegionSettings.Size = new System.Drawing.Size(392, 88);
            this.gbCropRegionSettings.TabIndex = 3;
            this.gbCropRegionSettings.TabStop = false;
            this.gbCropRegionSettings.Text = "Region Settings";
            // 
            // lblCropBorderSize
            // 
            this.lblCropBorderSize.AutoSize = true;
            this.lblCropBorderSize.Location = new System.Drawing.Point(248, 28);
            this.lblCropBorderSize.Name = "lblCropBorderSize";
            this.lblCropBorderSize.Size = new System.Drawing.Size(62, 13);
            this.lblCropBorderSize.TabIndex = 1;
            this.lblCropBorderSize.Text = "Border size:";
            // 
            // cbShowCropRuler
            // 
            this.cbShowCropRuler.AutoSize = true;
            this.cbShowCropRuler.Location = new System.Drawing.Point(16, 24);
            this.cbShowCropRuler.Name = "cbShowCropRuler";
            this.cbShowCropRuler.Size = new System.Drawing.Size(76, 17);
            this.cbShowCropRuler.TabIndex = 0;
            this.cbShowCropRuler.Text = "Show ruler";
            this.cbShowCropRuler.UseVisualStyleBackColor = true;
            this.cbShowCropRuler.CheckedChanged += new System.EventHandler(this.cbShowCropRuler_CheckedChanged);
            // 
            // cbCropShowGrids
            // 
            this.cbCropShowGrids.AutoSize = true;
            this.cbCropShowGrids.Location = new System.Drawing.Point(16, 48);
            this.cbCropShowGrids.Name = "cbCropShowGrids";
            this.cbCropShowGrids.Size = new System.Drawing.Size(206, 17);
            this.cbCropShowGrids.TabIndex = 3;
            this.cbCropShowGrids.Text = "Show grid when possible in Grid Mode";
            this.cbCropShowGrids.UseVisualStyleBackColor = true;
            this.cbCropShowGrids.CheckedChanged += new System.EventHandler(this.cbCropShowGrids_CheckedChanged);
            // 
            // lblCropBorderColor
            // 
            this.lblCropBorderColor.AutoSize = true;
            this.lblCropBorderColor.Location = new System.Drawing.Point(248, 56);
            this.lblCropBorderColor.Name = "lblCropBorderColor";
            this.lblCropBorderColor.Size = new System.Drawing.Size(67, 13);
            this.lblCropBorderColor.TabIndex = 4;
            this.lblCropBorderColor.Text = "Border color:";
            // 
            // pbCropBorderColor
            // 
            this.pbCropBorderColor.BackColor = System.Drawing.Color.White;
            this.pbCropBorderColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbCropBorderColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbCropBorderColor.Location = new System.Drawing.Point(320, 52);
            this.pbCropBorderColor.Name = "pbCropBorderColor";
            this.pbCropBorderColor.Size = new System.Drawing.Size(56, 20);
            this.pbCropBorderColor.TabIndex = 9;
            this.pbCropBorderColor.TabStop = false;
            this.pbCropBorderColor.Click += new System.EventHandler(this.pbCropBorderColor_Click);
            // 
            // nudCropBorderSize
            // 
            this.nudCropBorderSize.Location = new System.Drawing.Point(320, 24);
            this.nudCropBorderSize.Name = "nudCropBorderSize";
            this.nudCropBorderSize.Size = new System.Drawing.Size(56, 20);
            this.nudCropBorderSize.TabIndex = 2;
            this.nudCropBorderSize.ValueChanged += new System.EventHandler(this.nudCropBorderSize_ValueChanged);
            // 
            // gbCropCrosshairSettings
            // 
            this.gbCropCrosshairSettings.Controls.Add(this.chkCropDynamicCrosshair);
            this.gbCropCrosshairSettings.Controls.Add(this.lblCropCrosshairStep);
            this.gbCropCrosshairSettings.Controls.Add(this.chkCropShowBigCross);
            this.gbCropCrosshairSettings.Controls.Add(this.pbCropCrosshairColor);
            this.gbCropCrosshairSettings.Controls.Add(this.lblCropCrosshairInterval);
            this.gbCropCrosshairSettings.Controls.Add(this.lblCropCrosshairColor);
            this.gbCropCrosshairSettings.Controls.Add(this.nudCrosshairLineCount);
            this.gbCropCrosshairSettings.Controls.Add(this.nudCropCrosshairInterval);
            this.gbCropCrosshairSettings.Controls.Add(this.nudCrosshairLineSize);
            this.gbCropCrosshairSettings.Controls.Add(this.nudCropCrosshairStep);
            this.gbCropCrosshairSettings.Controls.Add(this.lblCrosshairLineSize);
            this.gbCropCrosshairSettings.Controls.Add(this.lblCrosshairLineCount);
            this.gbCropCrosshairSettings.Location = new System.Drawing.Point(8, 200);
            this.gbCropCrosshairSettings.Name = "gbCropCrosshairSettings";
            this.gbCropCrosshairSettings.Size = new System.Drawing.Size(352, 144);
            this.gbCropCrosshairSettings.TabIndex = 5;
            this.gbCropCrosshairSettings.TabStop = false;
            this.gbCropCrosshairSettings.Text = "Crosshair Settings";
            // 
            // chkCropDynamicCrosshair
            // 
            this.chkCropDynamicCrosshair.AutoSize = true;
            this.chkCropDynamicCrosshair.Location = new System.Drawing.Point(16, 48);
            this.chkCropDynamicCrosshair.Name = "chkCropDynamicCrosshair";
            this.chkCropDynamicCrosshair.Size = new System.Drawing.Size(118, 17);
            this.chkCropDynamicCrosshair.TabIndex = 1;
            this.chkCropDynamicCrosshair.Text = "Animated cross-hair";
            this.chkCropDynamicCrosshair.UseVisualStyleBackColor = true;
            this.chkCropDynamicCrosshair.CheckedChanged += new System.EventHandler(this.cbCropDynamicCrosshair_CheckedChanged);
            // 
            // lblCropCrosshairStep
            // 
            this.lblCropCrosshairStep.AutoSize = true;
            this.lblCropCrosshairStep.Location = new System.Drawing.Point(248, 49);
            this.lblCropCrosshairStep.Name = "lblCropCrosshairStep";
            this.lblCropCrosshairStep.Size = new System.Drawing.Size(32, 13);
            this.lblCropCrosshairStep.TabIndex = 4;
            this.lblCropCrosshairStep.Text = "Step:";
            // 
            // chkCropShowBigCross
            // 
            this.chkCropShowBigCross.AutoSize = true;
            this.chkCropShowBigCross.Location = new System.Drawing.Point(16, 24);
            this.chkCropShowBigCross.Name = "chkCropShowBigCross";
            this.chkCropShowBigCross.Size = new System.Drawing.Size(212, 17);
            this.chkCropShowBigCross.TabIndex = 0;
            this.chkCropShowBigCross.Text = "Extend the cross-hair across the screen";
            this.chkCropShowBigCross.UseVisualStyleBackColor = true;
            this.chkCropShowBigCross.CheckedChanged += new System.EventHandler(this.cbCropShowBigCross_CheckedChanged);
            // 
            // pbCropCrosshairColor
            // 
            this.pbCropCrosshairColor.BackColor = System.Drawing.Color.White;
            this.pbCropCrosshairColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbCropCrosshairColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbCropCrosshairColor.Location = new System.Drawing.Point(280, 80);
            this.pbCropCrosshairColor.Name = "pbCropCrosshairColor";
            this.pbCropCrosshairColor.Size = new System.Drawing.Size(56, 20);
            this.pbCropCrosshairColor.TabIndex = 14;
            this.pbCropCrosshairColor.TabStop = false;
            this.pbCropCrosshairColor.Click += new System.EventHandler(this.pbCropCrosshairColor_Click);
            // 
            // lblCropCrosshairInterval
            // 
            this.lblCropCrosshairInterval.AutoSize = true;
            this.lblCropCrosshairInterval.Location = new System.Drawing.Point(136, 49);
            this.lblCropCrosshairInterval.Name = "lblCropCrosshairInterval";
            this.lblCropCrosshairInterval.Size = new System.Drawing.Size(45, 13);
            this.lblCropCrosshairInterval.TabIndex = 2;
            this.lblCropCrosshairInterval.Text = "Interval:";
            // 
            // lblCropCrosshairColor
            // 
            this.lblCropCrosshairColor.AutoSize = true;
            this.lblCropCrosshairColor.Location = new System.Drawing.Point(240, 84);
            this.lblCropCrosshairColor.Name = "lblCropCrosshairColor";
            this.lblCropCrosshairColor.Size = new System.Drawing.Size(34, 13);
            this.lblCropCrosshairColor.TabIndex = 8;
            this.lblCropCrosshairColor.Text = "Color:";
            // 
            // nudCrosshairLineCount
            // 
            this.nudCrosshairLineCount.Location = new System.Drawing.Point(165, 78);
            this.nudCrosshairLineCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudCrosshairLineCount.Name = "nudCrosshairLineCount";
            this.nudCrosshairLineCount.Size = new System.Drawing.Size(56, 20);
            this.nudCrosshairLineCount.TabIndex = 7;
            this.nudCrosshairLineCount.ValueChanged += new System.EventHandler(this.nudCrosshairLineCount_ValueChanged);
            // 
            // nudCropCrosshairInterval
            // 
            this.nudCropCrosshairInterval.Location = new System.Drawing.Point(184, 47);
            this.nudCropCrosshairInterval.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudCropCrosshairInterval.Name = "nudCropCrosshairInterval";
            this.nudCropCrosshairInterval.Size = new System.Drawing.Size(56, 20);
            this.nudCropCrosshairInterval.TabIndex = 3;
            this.nudCropCrosshairInterval.ValueChanged += new System.EventHandler(this.nudCropInterval_ValueChanged);
            // 
            // nudCrosshairLineSize
            // 
            this.nudCrosshairLineSize.Location = new System.Drawing.Point(96, 109);
            this.nudCrosshairLineSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudCrosshairLineSize.Name = "nudCrosshairLineSize";
            this.nudCrosshairLineSize.Size = new System.Drawing.Size(56, 20);
            this.nudCrosshairLineSize.TabIndex = 10;
            this.nudCrosshairLineSize.ValueChanged += new System.EventHandler(this.nudCrosshairLineSize_ValueChanged);
            // 
            // nudCropCrosshairStep
            // 
            this.nudCropCrosshairStep.Location = new System.Drawing.Point(280, 46);
            this.nudCropCrosshairStep.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCropCrosshairStep.Name = "nudCropCrosshairStep";
            this.nudCropCrosshairStep.Size = new System.Drawing.Size(56, 20);
            this.nudCropCrosshairStep.TabIndex = 5;
            this.nudCropCrosshairStep.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCropCrosshairStep.ValueChanged += new System.EventHandler(this.nudCropStep_ValueChanged);
            // 
            // lblCrosshairLineSize
            // 
            this.lblCrosshairLineSize.AutoSize = true;
            this.lblCrosshairLineSize.Location = new System.Drawing.Point(16, 110);
            this.lblCrosshairLineSize.Name = "lblCrosshairLineSize";
            this.lblCrosshairLineSize.Size = new System.Drawing.Size(77, 13);
            this.lblCrosshairLineSize.TabIndex = 9;
            this.lblCrosshairLineSize.Text = "Cross-hair size:";
            // 
            // lblCrosshairLineCount
            // 
            this.lblCrosshairLineCount.AutoSize = true;
            this.lblCrosshairLineCount.Location = new System.Drawing.Point(16, 80);
            this.lblCrosshairLineCount.Name = "lblCrosshairLineCount";
            this.lblCrosshairLineCount.Size = new System.Drawing.Size(145, 13);
            this.lblCrosshairLineCount.TabIndex = 6;
            this.lblCrosshairLineCount.Text = "Number of concentric circles:";
            // 
            // gbCropGridMode
            // 
            this.gbCropGridMode.Controls.Add(this.cboCropGridMode);
            this.gbCropGridMode.Controls.Add(this.nudCropGridHeight);
            this.gbCropGridMode.Controls.Add(this.lblGridSizeWidth);
            this.gbCropGridMode.Controls.Add(this.lblGridSize);
            this.gbCropGridMode.Controls.Add(this.lblGridSizeHeight);
            this.gbCropGridMode.Controls.Add(this.nudCropGridWidth);
            this.gbCropGridMode.Location = new System.Drawing.Point(368, 8);
            this.gbCropGridMode.Name = "gbCropGridMode";
            this.gbCropGridMode.Size = new System.Drawing.Size(392, 80);
            this.gbCropGridMode.TabIndex = 1;
            this.gbCropGridMode.TabStop = false;
            this.gbCropGridMode.Tag = "With Grid Mode you can take screenshots of preset portions of the Screen";
            this.gbCropGridMode.Text = "Grid Mode Settings";
            // 
            // cboCropGridMode
            // 
            this.cboCropGridMode.AutoSize = true;
            this.cboCropGridMode.Location = new System.Drawing.Point(16, 24);
            this.cboCropGridMode.Name = "cboCropGridMode";
            this.cboCropGridMode.Size = new System.Drawing.Size(178, 17);
            this.cboCropGridMode.TabIndex = 0;
            this.cboCropGridMode.Text = "Activate Grid Mode in Crop Shot";
            this.cboCropGridMode.UseVisualStyleBackColor = true;
            this.cboCropGridMode.CheckedChanged += new System.EventHandler(this.cbCropGridMode_CheckedChanged);
            // 
            // nudCropGridHeight
            // 
            this.nudCropGridHeight.Location = new System.Drawing.Point(320, 48);
            this.nudCropGridHeight.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nudCropGridHeight.Name = "nudCropGridHeight";
            this.nudCropGridHeight.Size = new System.Drawing.Size(56, 20);
            this.nudCropGridHeight.TabIndex = 5;
            this.nudCropGridHeight.ValueChanged += new System.EventHandler(this.nudCropGridHeight_ValueChanged);
            // 
            // lblGridSizeWidth
            // 
            this.lblGridSizeWidth.AutoSize = true;
            this.lblGridSizeWidth.Location = new System.Drawing.Point(176, 52);
            this.lblGridSizeWidth.Name = "lblGridSizeWidth";
            this.lblGridSizeWidth.Size = new System.Drawing.Size(35, 13);
            this.lblGridSizeWidth.TabIndex = 2;
            this.lblGridSizeWidth.Text = "Width";
            // 
            // lblGridSize
            // 
            this.lblGridSize.AutoSize = true;
            this.lblGridSize.Location = new System.Drawing.Point(48, 52);
            this.lblGridSize.Name = "lblGridSize";
            this.lblGridSize.Size = new System.Drawing.Size(117, 13);
            this.lblGridSize.TabIndex = 1;
            this.lblGridSize.Text = "Grid Size ( 0 = Disable )";
            // 
            // lblGridSizeHeight
            // 
            this.lblGridSizeHeight.AutoSize = true;
            this.lblGridSizeHeight.Location = new System.Drawing.Point(280, 52);
            this.lblGridSizeHeight.Name = "lblGridSizeHeight";
            this.lblGridSizeHeight.Size = new System.Drawing.Size(38, 13);
            this.lblGridSizeHeight.TabIndex = 4;
            this.lblGridSizeHeight.Text = "Height";
            // 
            // nudCropGridWidth
            // 
            this.nudCropGridWidth.Location = new System.Drawing.Point(216, 48);
            this.nudCropGridWidth.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nudCropGridWidth.Name = "nudCropGridWidth";
            this.nudCropGridWidth.Size = new System.Drawing.Size(56, 20);
            this.nudCropGridWidth.TabIndex = 3;
            this.nudCropGridWidth.ValueChanged += new System.EventHandler(this.nudCropGridSize_ValueChanged);
            // 
            // tpCropShotLast
            // 
            this.tpCropShotLast.Controls.Add(this.btnLastCropShotReset);
            this.tpCropShotLast.Location = new System.Drawing.Point(4, 22);
            this.tpCropShotLast.Name = "tpCropShotLast";
            this.tpCropShotLast.Padding = new System.Windows.Forms.Padding(3);
            this.tpCropShotLast.Size = new System.Drawing.Size(803, 388);
            this.tpCropShotLast.TabIndex = 3;
            this.tpCropShotLast.Text = "Last Crop Shot";
            this.tpCropShotLast.UseVisualStyleBackColor = true;
            // 
            // btnLastCropShotReset
            // 
            this.btnLastCropShotReset.AutoSize = true;
            this.btnLastCropShotReset.Location = new System.Drawing.Point(16, 16);
            this.btnLastCropShotReset.Name = "btnLastCropShotReset";
            this.btnLastCropShotReset.Size = new System.Drawing.Size(165, 23);
            this.btnLastCropShotReset.TabIndex = 0;
            this.btnLastCropShotReset.Text = "Reset Last Crop Shot rectangle";
            this.btnLastCropShotReset.UseVisualStyleBackColor = true;
            this.btnLastCropShotReset.Click += new System.EventHandler(this.btnLastCropShotReset_Click);
            // 
            // tpCaptureShape
            // 
            this.tpCaptureShape.Controls.Add(this.pgSurfaceConfig);
            this.tpCaptureShape.Location = new System.Drawing.Point(4, 22);
            this.tpCaptureShape.Name = "tpCaptureShape";
            this.tpCaptureShape.Padding = new System.Windows.Forms.Padding(3);
            this.tpCaptureShape.Size = new System.Drawing.Size(803, 388);
            this.tpCaptureShape.TabIndex = 4;
            this.tpCaptureShape.Text = "Shape";
            this.tpCaptureShape.UseVisualStyleBackColor = true;
            // 
            // pgSurfaceConfig
            // 
            this.pgSurfaceConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgSurfaceConfig.Location = new System.Drawing.Point(3, 3);
            this.pgSurfaceConfig.Name = "pgSurfaceConfig";
            this.pgSurfaceConfig.Size = new System.Drawing.Size(797, 382);
            this.pgSurfaceConfig.TabIndex = 0;
            // 
            // tpFreehandCropShot
            // 
            this.tpFreehandCropShot.Controls.Add(this.cbFreehandCropShowRectangleBorder);
            this.tpFreehandCropShot.Controls.Add(this.cbFreehandCropAutoClose);
            this.tpFreehandCropShot.Controls.Add(this.cbFreehandCropAutoUpload);
            this.tpFreehandCropShot.Controls.Add(this.cbFreehandCropShowHelpText);
            this.tpFreehandCropShot.ImageKey = "shape_square_edit.png";
            this.tpFreehandCropShot.Location = new System.Drawing.Point(4, 22);
            this.tpFreehandCropShot.Name = "tpFreehandCropShot";
            this.tpFreehandCropShot.Size = new System.Drawing.Size(803, 388);
            this.tpFreehandCropShot.TabIndex = 5;
            this.tpFreehandCropShot.Text = "Freehand Crop Shot";
            this.tpFreehandCropShot.UseVisualStyleBackColor = true;
            // 
            // cbFreehandCropShowRectangleBorder
            // 
            this.cbFreehandCropShowRectangleBorder.AutoSize = true;
            this.cbFreehandCropShowRectangleBorder.Location = new System.Drawing.Point(16, 88);
            this.cbFreehandCropShowRectangleBorder.Name = "cbFreehandCropShowRectangleBorder";
            this.cbFreehandCropShowRectangleBorder.Size = new System.Drawing.Size(229, 17);
            this.cbFreehandCropShowRectangleBorder.TabIndex = 3;
            this.cbFreehandCropShowRectangleBorder.Text = "Show rectangle border and size information";
            this.cbFreehandCropShowRectangleBorder.UseVisualStyleBackColor = true;
            this.cbFreehandCropShowRectangleBorder.CheckedChanged += new System.EventHandler(this.cbFreehandCropShowRectangleBorder_CheckedChanged);
            // 
            // cbFreehandCropAutoClose
            // 
            this.cbFreehandCropAutoClose.AutoSize = true;
            this.cbFreehandCropAutoClose.Location = new System.Drawing.Point(16, 64);
            this.cbFreehandCropAutoClose.Name = "cbFreehandCropAutoClose";
            this.cbFreehandCropAutoClose.Size = new System.Drawing.Size(336, 17);
            this.cbFreehandCropAutoClose.TabIndex = 2;
            this.cbFreehandCropAutoClose.Text = "Use right click to cancel upload instead of cleaning drawn regions";
            this.cbFreehandCropAutoClose.UseVisualStyleBackColor = true;
            this.cbFreehandCropAutoClose.CheckedChanged += new System.EventHandler(this.cbFreehandCropAutoClose_CheckedChanged);
            // 
            // cbFreehandCropAutoUpload
            // 
            this.cbFreehandCropAutoUpload.AutoSize = true;
            this.cbFreehandCropAutoUpload.Location = new System.Drawing.Point(16, 40);
            this.cbFreehandCropAutoUpload.Name = "cbFreehandCropAutoUpload";
            this.cbFreehandCropAutoUpload.Size = new System.Drawing.Size(221, 17);
            this.cbFreehandCropAutoUpload.TabIndex = 1;
            this.cbFreehandCropAutoUpload.Text = "Automatically upload after region is drawn";
            this.cbFreehandCropAutoUpload.UseVisualStyleBackColor = true;
            this.cbFreehandCropAutoUpload.CheckedChanged += new System.EventHandler(this.cbFreehandCropAutoUpload_CheckedChanged);
            // 
            // cbFreehandCropShowHelpText
            // 
            this.cbFreehandCropShowHelpText.AutoSize = true;
            this.cbFreehandCropShowHelpText.Location = new System.Drawing.Point(16, 16);
            this.cbFreehandCropShowHelpText.Name = "cbFreehandCropShowHelpText";
            this.cbFreehandCropShowHelpText.Size = new System.Drawing.Size(96, 17);
            this.cbFreehandCropShowHelpText.TabIndex = 0;
            this.cbFreehandCropShowHelpText.Text = "Show help text";
            this.cbFreehandCropShowHelpText.UseVisualStyleBackColor = true;
            this.cbFreehandCropShowHelpText.CheckedChanged += new System.EventHandler(this.cbFreehandCropShowHelpText_CheckedChanged);
            // 
            // tpCaptureClipboard
            // 
            this.tpCaptureClipboard.Controls.Add(this.gbMonitorClipboard);
            this.tpCaptureClipboard.Location = new System.Drawing.Point(4, 22);
            this.tpCaptureClipboard.Name = "tpCaptureClipboard";
            this.tpCaptureClipboard.Padding = new System.Windows.Forms.Padding(3);
            this.tpCaptureClipboard.Size = new System.Drawing.Size(803, 388);
            this.tpCaptureClipboard.TabIndex = 6;
            this.tpCaptureClipboard.Text = "Clipboard";
            this.tpCaptureClipboard.UseVisualStyleBackColor = true;
            // 
            // gbMonitorClipboard
            // 
            this.gbMonitorClipboard.Controls.Add(this.chkMonUrls);
            this.gbMonitorClipboard.Controls.Add(this.chkMonFiles);
            this.gbMonitorClipboard.Controls.Add(this.chkMonImages);
            this.gbMonitorClipboard.Controls.Add(this.chkMonText);
            this.gbMonitorClipboard.Location = new System.Drawing.Point(8, 8);
            this.gbMonitorClipboard.Name = "gbMonitorClipboard";
            this.gbMonitorClipboard.Size = new System.Drawing.Size(760, 56);
            this.gbMonitorClipboard.TabIndex = 0;
            this.gbMonitorClipboard.TabStop = false;
            this.gbMonitorClipboard.Text = "Monitor Clipboard";
            // 
            // chkMonUrls
            // 
            this.chkMonUrls.AutoSize = true;
            this.chkMonUrls.Location = new System.Drawing.Point(592, 24);
            this.chkMonUrls.Name = "chkMonUrls";
            this.chkMonUrls.Size = new System.Drawing.Size(53, 17);
            this.chkMonUrls.TabIndex = 3;
            this.chkMonUrls.Text = "URLs";
            this.chkMonUrls.UseVisualStyleBackColor = true;
            this.chkMonUrls.CheckedChanged += new System.EventHandler(this.chkMonUrls_CheckedChanged);
            // 
            // chkMonFiles
            // 
            this.chkMonFiles.AutoSize = true;
            this.chkMonFiles.Location = new System.Drawing.Point(424, 24);
            this.chkMonFiles.Name = "chkMonFiles";
            this.chkMonFiles.Size = new System.Drawing.Size(47, 17);
            this.chkMonFiles.TabIndex = 2;
            this.chkMonFiles.Text = "Files";
            this.chkMonFiles.UseVisualStyleBackColor = true;
            this.chkMonFiles.CheckedChanged += new System.EventHandler(this.chkMonFiles_CheckedChanged);
            // 
            // chkMonImages
            // 
            this.chkMonImages.AutoSize = true;
            this.chkMonImages.Location = new System.Drawing.Point(16, 24);
            this.chkMonImages.Name = "chkMonImages";
            this.chkMonImages.Size = new System.Drawing.Size(60, 17);
            this.chkMonImages.TabIndex = 0;
            this.chkMonImages.Text = "Images";
            this.chkMonImages.UseVisualStyleBackColor = true;
            this.chkMonImages.CheckedChanged += new System.EventHandler(this.chkMonImages_CheckedChanged);
            // 
            // chkMonText
            // 
            this.chkMonText.AutoSize = true;
            this.chkMonText.Location = new System.Drawing.Point(200, 24);
            this.chkMonText.Name = "chkMonText";
            this.chkMonText.Size = new System.Drawing.Size(47, 17);
            this.chkMonText.TabIndex = 1;
            this.chkMonText.Text = "Text";
            this.chkMonText.UseVisualStyleBackColor = true;
            this.chkMonText.CheckedChanged += new System.EventHandler(this.chkMonText_CheckedChanged);
            // 
            // tpAdvanced
            // 
            this.tpAdvanced.Controls.Add(this.tcAdvanced);
            this.tpAdvanced.Location = new System.Drawing.Point(4, 22);
            this.tpAdvanced.Name = "tpAdvanced";
            this.tpAdvanced.Padding = new System.Windows.Forms.Padding(3);
            this.tpAdvanced.Size = new System.Drawing.Size(817, 420);
            this.tpAdvanced.TabIndex = 3;
            this.tpAdvanced.Text = "Advanced";
            this.tpAdvanced.UseVisualStyleBackColor = true;
            // 
            // tcAdvanced
            // 
            this.tcAdvanced.Controls.Add(this.tpAdvancedDebug);
            this.tcAdvanced.Controls.Add(this.tpAdvancedCore);
            this.tcAdvanced.Controls.Add(this.tpAdvancedSettings);
            this.tcAdvanced.Controls.Add(this.tpAdvancedWorkflow);
            this.tcAdvanced.Controls.Add(this.tpAdvanedUploaders);
            this.tcAdvanced.Controls.Add(this.tpAdvancedStats);
            this.tcAdvanced.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcAdvanced.Location = new System.Drawing.Point(3, 3);
            this.tcAdvanced.Name = "tcAdvanced";
            this.tcAdvanced.SelectedIndex = 0;
            this.tcAdvanced.Size = new System.Drawing.Size(811, 414);
            this.tcAdvanced.TabIndex = 0;
            this.tcAdvanced.Selected += new System.Windows.Forms.TabControlEventHandler(this.tcAdvanced_Selected);
            // 
            // tpAdvancedDebug
            // 
            this.tpAdvancedDebug.Controls.Add(this.rtbDebugLog);
            this.tpAdvancedDebug.Location = new System.Drawing.Point(4, 22);
            this.tpAdvancedDebug.Name = "tpAdvancedDebug";
            this.tpAdvancedDebug.Padding = new System.Windows.Forms.Padding(3);
            this.tpAdvancedDebug.Size = new System.Drawing.Size(803, 388);
            this.tpAdvancedDebug.TabIndex = 0;
            this.tpAdvancedDebug.Text = "Debug";
            this.tpAdvancedDebug.UseVisualStyleBackColor = true;
            // 
            // rtbDebugLog
            // 
            this.rtbDebugLog.BackColor = System.Drawing.Color.WhiteSmoke;
            this.rtbDebugLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbDebugLog.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.rtbDebugLog.Location = new System.Drawing.Point(3, 3);
            this.rtbDebugLog.Name = "rtbDebugLog";
            this.rtbDebugLog.ReadOnly = true;
            this.rtbDebugLog.Size = new System.Drawing.Size(797, 382);
            this.rtbDebugLog.TabIndex = 0;
            this.rtbDebugLog.Text = "";
            this.rtbDebugLog.WordWrap = false;
            // 
            // tpAdvancedCore
            // 
            this.tpAdvancedCore.Controls.Add(this.pgAppSettings);
            this.tpAdvancedCore.Location = new System.Drawing.Point(4, 22);
            this.tpAdvancedCore.Name = "tpAdvancedCore";
            this.tpAdvancedCore.Padding = new System.Windows.Forms.Padding(3);
            this.tpAdvancedCore.Size = new System.Drawing.Size(803, 388);
            this.tpAdvancedCore.TabIndex = 1;
            this.tpAdvancedCore.Text = "Core";
            this.tpAdvancedCore.UseVisualStyleBackColor = true;
            // 
            // pgAppSettings
            // 
            this.pgAppSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgAppSettings.HelpVisible = false;
            this.pgAppSettings.Location = new System.Drawing.Point(3, 3);
            this.pgAppSettings.Name = "pgAppSettings";
            this.pgAppSettings.Size = new System.Drawing.Size(797, 382);
            this.pgAppSettings.TabIndex = 0;
            this.pgAppSettings.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgAppSettings_PropertyValueChanged);
            // 
            // tpAdvancedSettings
            // 
            this.tpAdvancedSettings.Controls.Add(this.pgAppConfig);
            this.tpAdvancedSettings.Location = new System.Drawing.Point(4, 22);
            this.tpAdvancedSettings.Name = "tpAdvancedSettings";
            this.tpAdvancedSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tpAdvancedSettings.Size = new System.Drawing.Size(803, 388);
            this.tpAdvancedSettings.TabIndex = 2;
            this.tpAdvancedSettings.Text = "Settings";
            this.tpAdvancedSettings.UseVisualStyleBackColor = true;
            // 
            // pgAppConfig
            // 
            this.pgAppConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgAppConfig.Location = new System.Drawing.Point(3, 3);
            this.pgAppConfig.Name = "pgAppConfig";
            this.pgAppConfig.Size = new System.Drawing.Size(797, 382);
            this.pgAppConfig.TabIndex = 0;
            this.pgAppConfig.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgAppConfig_PropertyValueChanged);
            // 
            // tpAdvancedWorkflow
            // 
            this.tpAdvancedWorkflow.Controls.Add(this.pgWorkflow);
            this.tpAdvancedWorkflow.Location = new System.Drawing.Point(4, 22);
            this.tpAdvancedWorkflow.Name = "tpAdvancedWorkflow";
            this.tpAdvancedWorkflow.Padding = new System.Windows.Forms.Padding(3);
            this.tpAdvancedWorkflow.Size = new System.Drawing.Size(803, 388);
            this.tpAdvancedWorkflow.TabIndex = 3;
            this.tpAdvancedWorkflow.Text = "Workflow";
            this.tpAdvancedWorkflow.UseVisualStyleBackColor = true;
            // 
            // pgWorkflow
            // 
            this.pgWorkflow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgWorkflow.Location = new System.Drawing.Point(3, 3);
            this.pgWorkflow.Name = "pgWorkflow";
            this.pgWorkflow.Size = new System.Drawing.Size(797, 382);
            this.pgWorkflow.TabIndex = 0;
            // 
            // tpAdvanedUploaders
            // 
            this.tpAdvanedUploaders.Controls.Add(this.pgUploaders);
            this.tpAdvanedUploaders.Location = new System.Drawing.Point(4, 22);
            this.tpAdvanedUploaders.Name = "tpAdvanedUploaders";
            this.tpAdvanedUploaders.Padding = new System.Windows.Forms.Padding(3);
            this.tpAdvanedUploaders.Size = new System.Drawing.Size(803, 388);
            this.tpAdvanedUploaders.TabIndex = 4;
            this.tpAdvanedUploaders.Text = "Uploaders";
            this.tpAdvanedUploaders.UseVisualStyleBackColor = true;
            // 
            // pgUploaders
            // 
            this.pgUploaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgUploaders.Location = new System.Drawing.Point(3, 3);
            this.pgUploaders.Name = "pgUploaders";
            this.pgUploaders.Size = new System.Drawing.Size(797, 382);
            this.pgUploaders.TabIndex = 0;
            // 
            // tpAdvancedStats
            // 
            this.tpAdvancedStats.Controls.Add(this.btnOpenZScreenTester);
            this.tpAdvancedStats.Controls.Add(this.gbStatistics);
            this.tpAdvancedStats.Controls.Add(this.gbLastSource);
            this.tpAdvancedStats.Location = new System.Drawing.Point(4, 22);
            this.tpAdvancedStats.Name = "tpAdvancedStats";
            this.tpAdvancedStats.Padding = new System.Windows.Forms.Padding(3);
            this.tpAdvancedStats.Size = new System.Drawing.Size(803, 388);
            this.tpAdvancedStats.TabIndex = 5;
            this.tpAdvancedStats.Text = "Statistics";
            this.tpAdvancedStats.UseVisualStyleBackColor = true;
            // 
            // btnOpenZScreenTester
            // 
            this.btnOpenZScreenTester.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenZScreenTester.Location = new System.Drawing.Point(599, 326);
            this.btnOpenZScreenTester.Name = "btnOpenZScreenTester";
            this.btnOpenZScreenTester.Size = new System.Drawing.Size(160, 23);
            this.btnOpenZScreenTester.TabIndex = 2;
            this.btnOpenZScreenTester.Text = "Open ZScreen Tester...";
            this.btnOpenZScreenTester.UseVisualStyleBackColor = true;
            this.btnOpenZScreenTester.Click += new System.EventHandler(this.btnOpenZScreenTester_Click);
            // 
            // gbStatistics
            // 
            this.gbStatistics.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbStatistics.Controls.Add(this.btnDebugStart);
            this.gbStatistics.Controls.Add(this.rtbStats);
            this.gbStatistics.Location = new System.Drawing.Point(8, 8);
            this.gbStatistics.Name = "gbStatistics";
            this.gbStatistics.Size = new System.Drawing.Size(766, 300);
            this.gbStatistics.TabIndex = 0;
            this.gbStatistics.TabStop = false;
            this.gbStatistics.Text = "Statistics";
            // 
            // btnDebugStart
            // 
            this.btnDebugStart.Location = new System.Drawing.Point(16, 24);
            this.btnDebugStart.Name = "btnDebugStart";
            this.btnDebugStart.Size = new System.Drawing.Size(64, 24);
            this.btnDebugStart.TabIndex = 0;
            this.btnDebugStart.Text = "Start";
            this.btnDebugStart.UseVisualStyleBackColor = true;
            this.btnDebugStart.Click += new System.EventHandler(this.btnDebugStart_Click);
            // 
            // rtbStats
            // 
            this.rtbStats.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbStats.BackColor = System.Drawing.Color.WhiteSmoke;
            this.rtbStats.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.rtbStats.Location = new System.Drawing.Point(16, 56);
            this.rtbStats.Name = "rtbStats";
            this.rtbStats.ReadOnly = true;
            this.rtbStats.Size = new System.Drawing.Size(736, 234);
            this.rtbStats.TabIndex = 1;
            this.rtbStats.Text = "";
            this.rtbStats.WordWrap = false;
            // 
            // gbLastSource
            // 
            this.gbLastSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbLastSource.Controls.Add(this.btnOpenSourceString);
            this.gbLastSource.Controls.Add(this.btnOpenSourceText);
            this.gbLastSource.Controls.Add(this.btnOpenSourceBrowser);
            this.gbLastSource.Location = new System.Drawing.Point(8, 318);
            this.gbLastSource.Name = "gbLastSource";
            this.gbLastSource.Size = new System.Drawing.Size(400, 64);
            this.gbLastSource.TabIndex = 1;
            this.gbLastSource.TabStop = false;
            this.gbLastSource.Text = "Last Source";
            // 
            // btnOpenSourceString
            // 
            this.btnOpenSourceString.Enabled = false;
            this.btnOpenSourceString.Location = new System.Drawing.Point(16, 24);
            this.btnOpenSourceString.Name = "btnOpenSourceString";
            this.btnOpenSourceString.Size = new System.Drawing.Size(120, 23);
            this.btnOpenSourceString.TabIndex = 0;
            this.btnOpenSourceString.Text = "Copy to Clipboard";
            this.btnOpenSourceString.UseVisualStyleBackColor = true;
            // 
            // btnOpenSourceText
            // 
            this.btnOpenSourceText.Enabled = false;
            this.btnOpenSourceText.Location = new System.Drawing.Point(144, 24);
            this.btnOpenSourceText.Name = "btnOpenSourceText";
            this.btnOpenSourceText.Size = new System.Drawing.Size(120, 23);
            this.btnOpenSourceText.TabIndex = 1;
            this.btnOpenSourceText.Text = "Open in Text Editor";
            this.btnOpenSourceText.UseVisualStyleBackColor = true;
            // 
            // btnOpenSourceBrowser
            // 
            this.btnOpenSourceBrowser.Enabled = false;
            this.btnOpenSourceBrowser.Location = new System.Drawing.Point(272, 24);
            this.btnOpenSourceBrowser.Name = "btnOpenSourceBrowser";
            this.btnOpenSourceBrowser.Size = new System.Drawing.Size(120, 23);
            this.btnOpenSourceBrowser.TabIndex = 2;
            this.btnOpenSourceBrowser.Text = "Open in Browser";
            this.btnOpenSourceBrowser.UseVisualStyleBackColor = true;
            // 
            // tpQueue
            // 
            this.tpQueue.Controls.Add(this.lvUploads);
            this.tpQueue.Location = new System.Drawing.Point(4, 22);
            this.tpQueue.Name = "tpQueue";
            this.tpQueue.Padding = new System.Windows.Forms.Padding(3);
            this.tpQueue.Size = new System.Drawing.Size(813, 440);
            this.tpQueue.TabIndex = 10;
            this.tpQueue.Text = "Queue";
            this.tpQueue.UseVisualStyleBackColor = true;
            // 
            // lvUploads
            // 
            this.lvUploads.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chFilename,
            this.chStatus,
            this.chProgress,
            this.chSpeed,
            this.chElapsed,
            this.chRemaining,
            this.chUploaderType,
            this.chHost,
            this.chURL});
            this.lvUploads.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvUploads.FullRowSelect = true;
            this.lvUploads.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvUploads.HideSelection = false;
            this.lvUploads.Location = new System.Drawing.Point(3, 3);
            this.lvUploads.Name = "lvUploads";
            this.lvUploads.ShowItemToolTips = true;
            this.lvUploads.Size = new System.Drawing.Size(807, 434);
            this.lvUploads.TabIndex = 4;
            this.lvUploads.UseCompatibleStateImageBehavior = false;
            this.lvUploads.View = System.Windows.Forms.View.Details;
            // 
            // chFilename
            // 
            this.chFilename.Text = "Filename";
            this.chFilename.Width = 150;
            // 
            // chStatus
            // 
            this.chStatus.Text = "Status";
            this.chStatus.Width = 75;
            // 
            // chProgress
            // 
            this.chProgress.Text = "Progress";
            this.chProgress.Width = 149;
            // 
            // chSpeed
            // 
            this.chSpeed.Text = "Speed";
            this.chSpeed.Width = 65;
            // 
            // chElapsed
            // 
            this.chElapsed.Text = "Elapsed";
            this.chElapsed.Width = 50;
            // 
            // chRemaining
            // 
            this.chRemaining.Text = "Remaining";
            this.chRemaining.Width = 50;
            // 
            // chUploaderType
            // 
            this.chUploaderType.Text = "Type";
            this.chUploaderType.Width = 50;
            // 
            // chHost
            // 
            this.chHost.Text = "Host";
            this.chHost.Width = 100;
            // 
            // chURL
            // 
            this.chURL.Text = "URL";
            this.chURL.Width = 225;
            // 
            // tpDestImageBam
            // 
            this.tpDestImageBam.BackColor = System.Drawing.SystemColors.Window;
            this.tpDestImageBam.Controls.Add(this.gbImageBamGalleries);
            this.tpDestImageBam.Controls.Add(this.gbImageBamLinks);
            this.tpDestImageBam.Controls.Add(this.gbImageBamApiKeys);
            this.tpDestImageBam.Location = new System.Drawing.Point(4, 22);
            this.tpDestImageBam.Name = "tpDestImageBam";
            this.tpDestImageBam.Padding = new System.Windows.Forms.Padding(3);
            this.tpDestImageBam.Size = new System.Drawing.Size(791, 404);
            this.tpDestImageBam.TabIndex = 7;
            this.tpDestImageBam.Text = "ImageBam";
            // 
            // gbImageBamGalleries
            // 
            this.gbImageBamGalleries.Controls.Add(this.lbImageBamGalleries);
            this.gbImageBamGalleries.Location = new System.Drawing.Point(8, 112);
            this.gbImageBamGalleries.Name = "gbImageBamGalleries";
            this.gbImageBamGalleries.Size = new System.Drawing.Size(480, 152);
            this.gbImageBamGalleries.TabIndex = 10;
            this.gbImageBamGalleries.TabStop = false;
            this.gbImageBamGalleries.Text = "Galleries";
            // 
            // lbImageBamGalleries
            // 
            this.lbImageBamGalleries.FormattingEnabled = true;
            this.lbImageBamGalleries.Location = new System.Drawing.Point(16, 24);
            this.lbImageBamGalleries.Name = "lbImageBamGalleries";
            this.lbImageBamGalleries.Size = new System.Drawing.Size(440, 108);
            this.lbImageBamGalleries.TabIndex = 0;
            // 
            // gbImageBamLinks
            // 
            this.gbImageBamLinks.Controls.Add(this.chkImageBamContentNSFW);
            this.gbImageBamLinks.Controls.Add(this.btnImageBamRemoveGallery);
            this.gbImageBamLinks.Controls.Add(this.btnImageBamCreateGallery);
            this.gbImageBamLinks.Controls.Add(this.btnImageBamRegister);
            this.gbImageBamLinks.Controls.Add(this.btnImageBamApiKeysUrl);
            this.gbImageBamLinks.Location = new System.Drawing.Point(496, 8);
            this.gbImageBamLinks.Name = "gbImageBamLinks";
            this.gbImageBamLinks.Size = new System.Drawing.Size(206, 256);
            this.gbImageBamLinks.TabIndex = 9;
            this.gbImageBamLinks.TabStop = false;
            this.gbImageBamLinks.Text = "Tasks";
            // 
            // chkImageBamContentNSFW
            // 
            this.chkImageBamContentNSFW.AutoSize = true;
            this.chkImageBamContentNSFW.Location = new System.Drawing.Point(16, 152);
            this.chkImageBamContentNSFW.Name = "chkImageBamContentNSFW";
            this.chkImageBamContentNSFW.Size = new System.Drawing.Size(98, 17);
            this.chkImageBamContentNSFW.TabIndex = 10;
            this.chkImageBamContentNSFW.Text = "NSFW Content";
            this.ttZScreen.SetToolTip(this.chkImageBamContentNSFW, "If you are uploading NSFW (Not Safe for Work) content then tick this checkbox");
            this.chkImageBamContentNSFW.UseVisualStyleBackColor = true;
            // 
            // btnImageBamRemoveGallery
            // 
            this.btnImageBamRemoveGallery.Location = new System.Drawing.Point(16, 120);
            this.btnImageBamRemoveGallery.Name = "btnImageBamRemoveGallery";
            this.btnImageBamRemoveGallery.Size = new System.Drawing.Size(128, 23);
            this.btnImageBamRemoveGallery.TabIndex = 9;
            this.btnImageBamRemoveGallery.Text = "Remove &Gallery";
            this.btnImageBamRemoveGallery.UseVisualStyleBackColor = true;
            // 
            // btnImageBamCreateGallery
            // 
            this.btnImageBamCreateGallery.Location = new System.Drawing.Point(16, 88);
            this.btnImageBamCreateGallery.Name = "btnImageBamCreateGallery";
            this.btnImageBamCreateGallery.Size = new System.Drawing.Size(128, 23);
            this.btnImageBamCreateGallery.TabIndex = 8;
            this.btnImageBamCreateGallery.Text = "Create &Gallery";
            this.btnImageBamCreateGallery.UseVisualStyleBackColor = true;
            // 
            // btnImageBamRegister
            // 
            this.btnImageBamRegister.AutoSize = true;
            this.btnImageBamRegister.Location = new System.Drawing.Point(16, 24);
            this.btnImageBamRegister.Name = "btnImageBamRegister";
            this.btnImageBamRegister.Size = new System.Drawing.Size(169, 27);
            this.btnImageBamRegister.TabIndex = 7;
            this.btnImageBamRegister.Text = "Register at ImageBam...";
            this.btnImageBamRegister.UseVisualStyleBackColor = true;
            // 
            // btnImageBamApiKeysUrl
            // 
            this.btnImageBamApiKeysUrl.AutoSize = true;
            this.btnImageBamApiKeysUrl.Location = new System.Drawing.Point(16, 56);
            this.btnImageBamApiKeysUrl.Name = "btnImageBamApiKeysUrl";
            this.btnImageBamApiKeysUrl.Size = new System.Drawing.Size(151, 27);
            this.btnImageBamApiKeysUrl.TabIndex = 6;
            this.btnImageBamApiKeysUrl.Text = "View your API Keys...";
            this.btnImageBamApiKeysUrl.UseVisualStyleBackColor = true;
            // 
            // gbImageBamApiKeys
            // 
            this.gbImageBamApiKeys.Controls.Add(this.lblImageBamSecret);
            this.gbImageBamApiKeys.Controls.Add(this.txtImageBamSecret);
            this.gbImageBamApiKeys.Controls.Add(this.lblImageBamKey);
            this.gbImageBamApiKeys.Controls.Add(this.txtImageBamApiKey);
            this.gbImageBamApiKeys.Location = new System.Drawing.Point(8, 8);
            this.gbImageBamApiKeys.Name = "gbImageBamApiKeys";
            this.gbImageBamApiKeys.Size = new System.Drawing.Size(480, 96);
            this.gbImageBamApiKeys.TabIndex = 8;
            this.gbImageBamApiKeys.TabStop = false;
            this.gbImageBamApiKeys.Text = "API-Keys";
            // 
            // lblImageBamSecret
            // 
            this.lblImageBamSecret.AutoSize = true;
            this.lblImageBamSecret.Location = new System.Drawing.Point(16, 56);
            this.lblImageBamSecret.Name = "lblImageBamSecret";
            this.lblImageBamSecret.Size = new System.Drawing.Size(41, 13);
            this.lblImageBamSecret.TabIndex = 5;
            this.lblImageBamSecret.Text = "Secret:";
            // 
            // txtImageBamSecret
            // 
            this.txtImageBamSecret.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtImageBamSecret.Location = new System.Drawing.Point(63, 52);
            this.txtImageBamSecret.Name = "txtImageBamSecret";
            this.txtImageBamSecret.Size = new System.Drawing.Size(393, 20);
            this.txtImageBamSecret.TabIndex = 4;
            // 
            // lblImageBamKey
            // 
            this.lblImageBamKey.AutoSize = true;
            this.lblImageBamKey.Location = new System.Drawing.Point(29, 26);
            this.lblImageBamKey.Name = "lblImageBamKey";
            this.lblImageBamKey.Size = new System.Drawing.Size(28, 13);
            this.lblImageBamKey.TabIndex = 3;
            this.lblImageBamKey.Text = "Key:";
            // 
            // txtImageBamApiKey
            // 
            this.txtImageBamApiKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtImageBamApiKey.Location = new System.Drawing.Point(62, 22);
            this.txtImageBamApiKey.Name = "txtImageBamApiKey";
            this.txtImageBamApiKey.Size = new System.Drawing.Size(394, 20);
            this.txtImageBamApiKey.TabIndex = 2;
            // 
            // tpUploadText
            // 
            this.tpUploadText.Location = new System.Drawing.Point(4, 22);
            this.tpUploadText.Name = "tpUploadText";
            this.tpUploadText.Padding = new System.Windows.Forms.Padding(3);
            this.tpUploadText.Size = new System.Drawing.Size(766, 402);
            this.tpUploadText.TabIndex = 0;
            this.tpUploadText.Text = "Upload text";
            this.tpUploadText.UseVisualStyleBackColor = true;
            // 
            // txtTextUploaderContent
            // 
            this.txtTextUploaderContent.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtTextUploaderContent.Location = new System.Drawing.Point(3, 3);
            this.txtTextUploaderContent.Multiline = true;
            this.txtTextUploaderContent.Name = "txtTextUploaderContent";
            this.txtTextUploaderContent.Size = new System.Drawing.Size(760, 357);
            this.txtTextUploaderContent.TabIndex = 1;
            // 
            // btnUploadText
            // 
            this.btnUploadText.Location = new System.Drawing.Point(0, 0);
            this.btnUploadText.Name = "btnUploadText";
            this.btnUploadText.Size = new System.Drawing.Size(75, 23);
            this.btnUploadText.TabIndex = 0;
            // 
            // btnUploadTextClipboard
            // 
            this.btnUploadTextClipboard.Location = new System.Drawing.Point(0, 0);
            this.btnUploadTextClipboard.Name = "btnUploadTextClipboard";
            this.btnUploadTextClipboard.Size = new System.Drawing.Size(75, 23);
            this.btnUploadTextClipboard.TabIndex = 0;
            // 
            // btnUploadTextClipboardFile
            // 
            this.btnUploadTextClipboardFile.Location = new System.Drawing.Point(0, 0);
            this.btnUploadTextClipboardFile.Name = "btnUploadTextClipboardFile";
            this.btnUploadTextClipboardFile.Size = new System.Drawing.Size(75, 23);
            this.btnUploadTextClipboardFile.TabIndex = 0;
            // 
            // ttZScreen
            // 
            this.ttZScreen.AutomaticDelay = 1000;
            this.ttZScreen.AutoPopDelay = 60000;
            this.ttZScreen.InitialDelay = 1000;
            this.ttZScreen.IsBalloon = true;
            this.ttZScreen.ReshowDelay = 200;
            this.ttZScreen.ShowAlways = true;
            // 
            // msApp
            // 
            this.msApp.BackColor = System.Drawing.Color.Transparent;
            this.msApp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.tsmiConfigure,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.msApp.Location = new System.Drawing.Point(2, 2);
            this.msApp.Name = "msApp";
            this.msApp.Size = new System.Drawing.Size(825, 24);
            this.msApp.TabIndex = 0;
            this.msApp.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFileUpload,
            this.toolStripSeparator,
            this.tsmiExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // tsmiFileUpload
            // 
            this.tsmiFileUpload.Image = ((System.Drawing.Image)(resources.GetObject("tsmiFileUpload.Image")));
            this.tsmiFileUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmiFileUpload.Name = "tsmiFileUpload";
            this.tsmiFileUpload.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.tsmiFileUpload.Size = new System.Drawing.Size(151, 22);
            this.tsmiFileUpload.Text = "&Open";
            this.tsmiFileUpload.Click += new System.EventHandler(this.tsmiFileUpload_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(148, 6);
            // 
            // tsmiExit
            // 
            this.tsmiExit.Image = global::ZScreenGUI.Properties.Resources.cross;
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(151, 22);
            this.tsmiExit.Text = "E&xit";
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
            // 
            // tsmiConfigure
            // 
            this.tsmiConfigure.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiConfigureFileNaming,
            this.tsmiImageSettings,
            this.tsmiWatermark,
            this.toolStripSeparator7,
            this.tsmiConfigureActions,
            this.tsmiOutputs});
            this.tsmiConfigure.Name = "tsmiConfigure";
            this.tsmiConfigure.Size = new System.Drawing.Size(66, 20);
            this.tsmiConfigure.Text = "&Configure";
            // 
            // tsmiConfigureFileNaming
            // 
            this.tsmiConfigureFileNaming.Name = "tsmiConfigureFileNaming";
            this.tsmiConfigureFileNaming.Size = new System.Drawing.Size(164, 22);
            this.tsmiConfigureFileNaming.Text = "&File Naming...";
            this.tsmiConfigureFileNaming.Click += new System.EventHandler(this.tsmiConfigureFileNaming_Click);
            // 
            // tsmiImageSettings
            // 
            this.tsmiImageSettings.Name = "tsmiImageSettings";
            this.tsmiImageSettings.Size = new System.Drawing.Size(164, 22);
            this.tsmiImageSettings.Text = "&Image Format...";
            this.tsmiImageSettings.Click += new System.EventHandler(this.tsmiImageSettings_Click);
            // 
            // tsmiWatermark
            // 
            this.tsmiWatermark.Name = "tsmiWatermark";
            this.tsmiWatermark.Size = new System.Drawing.Size(164, 22);
            this.tsmiWatermark.Text = "&Watermark...";
            this.tsmiWatermark.Click += new System.EventHandler(this.tsmiWatermark_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(161, 6);
            // 
            // tsmiConfigureActions
            // 
            this.tsmiConfigureActions.Name = "tsmiConfigureActions";
            this.tsmiConfigureActions.Size = new System.Drawing.Size(164, 22);
            this.tsmiConfigureActions.Text = "&Actions...";
            this.tsmiConfigureActions.Click += new System.EventHandler(this.tsmiConfigureActions_Click);
            // 
            // tsmiOutputs
            // 
            this.tsmiOutputs.Image = global::ZScreenGUI.Properties.Resources.server_edit;
            this.tsmiOutputs.Name = "tsmiOutputs";
            this.tsmiOutputs.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.tsmiOutputs.Size = new System.Drawing.Size(164, 22);
            this.tsmiOutputs.Text = "&Outputs...";
            this.tsmiOutputs.Click += new System.EventHandler(this.tsmiOutputs_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiProxy,
            this.tsmiOptions});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // tsmiProxy
            // 
            this.tsmiProxy.Image = global::ZScreenGUI.Properties.Resources.world_edit;
            this.tsmiProxy.Name = "tsmiProxy";
            this.tsmiProxy.Size = new System.Drawing.Size(167, 22);
            this.tsmiProxy.Text = "&Proxy Settings...";
            this.tsmiProxy.Click += new System.EventHandler(this.tsmiProxy_Click);
            // 
            // tsmiOptions
            // 
            this.tsmiOptions.Image = global::ZScreenGUI.Properties.Resources.application_edit;
            this.tsmiOptions.Name = "tsmiOptions";
            this.tsmiOptions.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.tsmiOptions.Size = new System.Drawing.Size(167, 22);
            this.tsmiOptions.Text = "&Options...";
            this.tsmiOptions.Click += new System.EventHandler(this.tsmiOptions_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiVersionHistory,
            this.toolStripSeparator10,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // tsmiVersionHistory
            // 
            this.tsmiVersionHistory.Image = global::ZScreenGUI.Properties.Resources.page_white_text;
            this.tsmiVersionHistory.Name = "tsmiVersionHistory";
            this.tsmiVersionHistory.Size = new System.Drawing.Size(169, 22);
            this.tsmiVersionHistory.Text = "&Version History...";
            this.tsmiVersionHistory.Click += new System.EventHandler(this.tsmiVersionHistory_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(166, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = global::ZScreenGUI.Properties.Resources.information;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // ZScreen
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(982, 474);
            this.Controls.Add(this.tcMain);
            this.Controls.Add(this.msApp);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msApp;
            this.MinimumSize = new System.Drawing.Size(990, 508);
            this.Name = "ZScreen";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ZScreen";
            this.Deactivate += new System.EventHandler(this.ZScreen_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ZScreen_FormClosing);
            this.Load += new System.EventHandler(this.ZScreen_Load);
            this.Move += new System.EventHandler(this.ZScreen_Move);
            this.Resize += new System.EventHandler(this.ZScreen_Resize);
            this.Controls.SetChildIndex(this.msApp, 0);
            this.Controls.SetChildIndex(this.tcMain, 0);
            this.cmTray.ResumeLayout(false);
            this.tcMain.ResumeLayout(false);
            this.tpMain.ResumeLayout(false);
            this.tpMain.PerformLayout();
            this.tsLinks.ResumeLayout(false);
            this.tsLinks.PerformLayout();
            this.gbImageSettings.ResumeLayout(false);
            this.gbImageSettings.PerformLayout();
            this.tpHotkeys.ResumeLayout(false);
            this.tpHotkeys.PerformLayout();
            this.tpMainInput.ResumeLayout(false);
            this.tcCapture.ResumeLayout(false);
            this.tpActivewindow.ResumeLayout(false);
            this.gbCaptureDwm.ResumeLayout(false);
            this.gbCaptureDwm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbActiveWindowDwmBackColor)).EndInit();
            this.gbCaptureEngine.ResumeLayout(false);
            this.gbCaptureEngine.PerformLayout();
            this.gbCaptureGdi.ResumeLayout(false);
            this.gbCaptureGdi.PerformLayout();
            this.tpSelectedWindow.ResumeLayout(false);
            this.tpSelectedWindow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSelectedWindowHueRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSelectedWindowRegionStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSelectedWindowRegionInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSelectedWindowBorderSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSelectedWindowBorderColor)).EndInit();
            this.tpCropShot.ResumeLayout(false);
            this.gbCropEngine.ResumeLayout(false);
            this.gbCropShotMagnifyingGlass.ResumeLayout(false);
            this.gbCropShotMagnifyingGlass.PerformLayout();
            this.gbCropDynamicRegionBorderColorSettings.ResumeLayout(false);
            this.gbCropDynamicRegionBorderColorSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropRegionStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropHueRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropRegionInterval)).EndInit();
            this.gbCropRegion.ResumeLayout(false);
            this.gbCropRegion.PerformLayout();
            this.gbCropRegionSettings.ResumeLayout(false);
            this.gbCropRegionSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCropBorderColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropBorderSize)).EndInit();
            this.gbCropCrosshairSettings.ResumeLayout(false);
            this.gbCropCrosshairSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCropCrosshairColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCrosshairLineCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropCrosshairInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCrosshairLineSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropCrosshairStep)).EndInit();
            this.gbCropGridMode.ResumeLayout(false);
            this.gbCropGridMode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropGridHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropGridWidth)).EndInit();
            this.tpCropShotLast.ResumeLayout(false);
            this.tpCropShotLast.PerformLayout();
            this.tpCaptureShape.ResumeLayout(false);
            this.tpFreehandCropShot.ResumeLayout(false);
            this.tpFreehandCropShot.PerformLayout();
            this.tpCaptureClipboard.ResumeLayout(false);
            this.gbMonitorClipboard.ResumeLayout(false);
            this.gbMonitorClipboard.PerformLayout();
            this.tpAdvanced.ResumeLayout(false);
            this.tcAdvanced.ResumeLayout(false);
            this.tpAdvancedDebug.ResumeLayout(false);
            this.tpAdvancedCore.ResumeLayout(false);
            this.tpAdvancedSettings.ResumeLayout(false);
            this.tpAdvancedWorkflow.ResumeLayout(false);
            this.tpAdvanedUploaders.ResumeLayout(false);
            this.tpAdvancedStats.ResumeLayout(false);
            this.gbStatistics.ResumeLayout(false);
            this.gbLastSource.ResumeLayout(false);
            this.tpQueue.ResumeLayout(false);
            this.tpDestImageBam.ResumeLayout(false);
            this.gbImageBamGalleries.ResumeLayout(false);
            this.gbImageBamLinks.ResumeLayout(false);
            this.gbImageBamLinks.PerformLayout();
            this.gbImageBamApiKeys.ResumeLayout(false);
            this.gbImageBamApiKeys.PerformLayout();
            this.msApp.ResumeLayout(false);
            this.msApp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.CheckBox chkPerformActions;

        #endregion Windows Form Designer generated code

        internal System.Windows.Forms.NotifyIcon niTray;
        internal System.Windows.Forms.ContextMenuStrip cmTray;
        internal System.Windows.Forms.ToolStripMenuItem tsmExitZScreen;
        internal System.Windows.Forms.ToolStripMenuItem tsmEditinImageSoftware;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        internal System.Windows.Forms.ToolStripMenuItem tsmiTabs;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        internal System.Windows.Forms.ToolStripMenuItem tsmViewLocalDirectory;
        internal System.Windows.Forms.ToolStripMenuItem tsmActions;
        internal System.Windows.Forms.ToolStripMenuItem tsmCropShot;
        internal System.Windows.Forms.ToolStripMenuItem tsmClipboardUpload;
        internal System.Windows.Forms.ToolStripMenuItem tsmEntireScreen;
        internal System.Windows.Forms.ToolStripMenuItem tsmLastCropShot;
        internal System.Windows.Forms.ToolStripMenuItem tsmHelp;
        internal System.Windows.Forms.ToolStripMenuItem tsmLicense;
        internal System.Windows.Forms.ToolStripMenuItem tsmVersionHistory;
        internal System.Windows.Forms.ToolStripMenuItem tsmAbout;
        internal System.Windows.Forms.ToolStripMenuItem tsmLanguageTranslator;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        internal System.Windows.Forms.ToolStripMenuItem tsmSelectedWindow;
        internal System.Windows.Forms.ToolStripMenuItem tsmScreenColorPicker;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        internal System.Windows.Forms.ToolStripMenuItem tsmDragDropWindow;
        internal System.Windows.Forms.ToolStripMenuItem autoScreenshotsToolStripMenuItem;
        internal System.Windows.Forms.Timer tmrApp;
        internal System.Windows.Forms.TabControl tcMain;
        internal System.Windows.Forms.TabPage tpMain;
        internal System.Windows.Forms.GroupBox gbCropGridMode;
        internal System.Windows.Forms.CheckBox cboCropGridMode;
        internal System.Windows.Forms.NumericUpDown nudCropGridHeight;
        internal System.Windows.Forms.Label lblGridSizeWidth;
        internal System.Windows.Forms.Label lblGridSize;
        internal System.Windows.Forms.Label lblGridSizeHeight;
        internal System.Windows.Forms.NumericUpDown nudCropGridWidth;
        internal NumericUpDownTimer nudScreenshotDelay;
        internal System.Windows.Forms.CheckBox chkShowWorkflowWizard;
        internal System.Windows.Forms.CheckBox chkShowCursor;
        internal System.Windows.Forms.TabPage tpHotkeys;
        internal System.Windows.Forms.Label lblHotkeyStatus;
        internal System.Windows.Forms.TabPage tpMainInput;
        internal System.Windows.Forms.TabControl tcCapture;
        internal System.Windows.Forms.TabPage tpCropShot;
        internal System.Windows.Forms.GroupBox gbCropRegionSettings;
        internal System.Windows.Forms.NumericUpDown nudCropHueRange;
        internal System.Windows.Forms.Label lblCropHueRange;
        internal System.Windows.Forms.NumericUpDown nudCropRegionStep;
        internal System.Windows.Forms.NumericUpDown nudCropRegionInterval;
        internal System.Windows.Forms.Label lblCropRegionStep;
        internal System.Windows.Forms.Label lblCropRegionInterval;
        internal System.Windows.Forms.CheckBox cbCropDynamicBorderColor;
        internal System.Windows.Forms.Label lblCropBorderSize;
        internal System.Windows.Forms.CheckBox cbShowCropRuler;
        internal System.Windows.Forms.CheckBox cbCropShowGrids;
        internal System.Windows.Forms.Label lblCropBorderColor;
        internal System.Windows.Forms.PictureBox pbCropBorderColor;
        internal System.Windows.Forms.NumericUpDown nudCropBorderSize;
        internal System.Windows.Forms.GroupBox gbCropCrosshairSettings;
        internal System.Windows.Forms.CheckBox chkCropShowMagnifyingGlass;
        internal System.Windows.Forms.CheckBox chkCropShowBigCross;
        internal System.Windows.Forms.CheckBox chkCropDynamicCrosshair;
        internal System.Windows.Forms.NumericUpDown nudCropCrosshairStep;
        internal System.Windows.Forms.PictureBox pbCropCrosshairColor;
        internal System.Windows.Forms.NumericUpDown nudCropCrosshairInterval;
        internal System.Windows.Forms.Label lblCropCrosshairColor;
        internal System.Windows.Forms.Label lblCropCrosshairStep;
        internal System.Windows.Forms.NumericUpDown nudCrosshairLineCount;
        internal System.Windows.Forms.Label lblCropCrosshairInterval;
        internal System.Windows.Forms.NumericUpDown nudCrosshairLineSize;
        internal System.Windows.Forms.Label lblCrosshairLineSize;
        internal System.Windows.Forms.Label lblCrosshairLineCount;
        internal System.Windows.Forms.Label lblCropRegionStyle;
        internal System.Windows.Forms.CheckBox chkRegionHotkeyInfo;
        internal System.Windows.Forms.ComboBox chkCropStyle;
        internal System.Windows.Forms.CheckBox chkRegionRectangleInfo;
        internal System.Windows.Forms.TabPage tpSelectedWindow;
        internal System.Windows.Forms.NumericUpDown nudSelectedWindowHueRange;
        internal System.Windows.Forms.Label lblSelectedWindowHueRange;
        internal System.Windows.Forms.NumericUpDown nudSelectedWindowRegionStep;
        internal System.Windows.Forms.NumericUpDown nudSelectedWindowRegionInterval;
        internal System.Windows.Forms.Label lblSelectedWindowRegionStep;
        internal System.Windows.Forms.Label lblSelectedWindowRegionInterval;
        internal System.Windows.Forms.CheckBox cbSelectedWindowDynamicBorderColor;
        internal System.Windows.Forms.CheckBox cbSelectedWindowRuler;
        internal System.Windows.Forms.Label lblSelectedWindowRegionStyle;
        internal System.Windows.Forms.ComboBox cbSelectedWindowStyle;
        internal System.Windows.Forms.CheckBox cbSelectedWindowRectangleInfo;
        internal System.Windows.Forms.Label lblSelectedWindowBorderColor;
        internal System.Windows.Forms.NumericUpDown nudSelectedWindowBorderSize;
        internal System.Windows.Forms.Label lblSelectedWindowBorderSize;
        internal System.Windows.Forms.PictureBox pbSelectedWindowBorderColor;
        internal System.Windows.Forms.TabPage tpAdvancedStats;
        internal System.Windows.Forms.GroupBox gbStatistics;
        internal System.Windows.Forms.Button btnDebugStart;
        internal System.Windows.Forms.GroupBox gbLastSource;
        internal System.Windows.Forms.Button btnOpenSourceString;
        internal System.Windows.Forms.Button btnOpenSourceText;
        internal System.Windows.Forms.Button btnOpenSourceBrowser;
        internal System.Windows.Forms.TabPage tpAdvanced;
        internal System.Windows.Forms.PropertyGrid pgAppConfig;
        internal System.Windows.Forms.TabPage tpUploadText;
        internal System.Windows.Forms.TextBox txtTextUploaderContent;
        internal System.Windows.Forms.Button btnUploadText;
        internal System.Windows.Forms.Button btnUploadTextClipboard;
        internal System.Windows.Forms.Button btnUploadTextClipboardFile;
        internal System.Windows.Forms.GroupBox gbImageSettings;
        internal System.Windows.Forms.GroupBox gbCropRegion;
        internal System.Windows.Forms.ToolTip ttZScreen;
        internal System.Windows.Forms.Label lblScreenshotDelay;
        internal System.Windows.Forms.GroupBox gbCropDynamicRegionBorderColorSettings;
        private System.Windows.Forms.ToolStripMenuItem tsmFTPClient;
        private System.Windows.Forms.CheckBox chkSelectedWindowCaptureObjects;
        private System.Windows.Forms.TabPage tpDestImageBam;
        internal System.Windows.Forms.Label lblImageBamSecret;
        internal System.Windows.Forms.TextBox txtImageBamSecret;
        internal System.Windows.Forms.Label lblImageBamKey;
        internal System.Windows.Forms.TextBox txtImageBamApiKey;
        private System.Windows.Forms.Button btnImageBamApiKeysUrl;
        private System.Windows.Forms.GroupBox gbImageBamLinks;
        private System.Windows.Forms.Button btnImageBamRegister;
        private System.Windows.Forms.GroupBox gbImageBamApiKeys;
        private System.Windows.Forms.GroupBox gbImageBamGalleries;
        private System.Windows.Forms.ListBox lbImageBamGalleries;
        private System.Windows.Forms.Button btnImageBamCreateGallery;
        private System.Windows.Forms.Button btnImageBamRemoveGallery;
        private System.Windows.Forms.CheckBox chkImageBamContentNSFW;
        internal System.Windows.Forms.RichTextBox rtbStats;
        private System.Windows.Forms.Button btnOpenZScreenTester;
        private System.Windows.Forms.CheckBox chkActiveWindowCleanBackground;
        private System.Windows.Forms.CheckBox chkSelectedWindowShowCheckers;
        private System.Windows.Forms.CheckBox chkSelectedWindowIncludeShadow;
        private System.Windows.Forms.GroupBox gbMonitorClipboard;
        private System.Windows.Forms.CheckBox chkMonText;
        private System.Windows.Forms.CheckBox chkMonUrls;
        private System.Windows.Forms.CheckBox chkMonFiles;
        private System.Windows.Forms.CheckBox chkMonImages;
        private System.Windows.Forms.CheckBox chkActiveWindowTryCaptureChildren;
        private System.Windows.Forms.TabPage tpActivewindow;
        private System.Windows.Forms.TabPage tpAdvancedDebug;
        private System.Windows.Forms.RichTextBox rtbDebugLog;
        private System.Windows.Forms.Button btnResetHotkeys;
        private System.Windows.Forms.TabPage tpFreehandCropShot;
        private System.Windows.Forms.CheckBox cbFreehandCropShowHelpText;
        private System.Windows.Forms.CheckBox cbFreehandCropAutoUpload;
        private System.Windows.Forms.CheckBox cbFreehandCropAutoClose;
        private System.Windows.Forms.CheckBox cbFreehandCropShowRectangleBorder;
        private System.Windows.Forms.ToolStripMenuItem historyToolStripMenuItem;
        private System.Windows.Forms.TabPage tpCaptureClipboard;
        private System.Windows.Forms.CheckBox chkShortenURL;
        private System.Windows.Forms.ToolStripMenuItem tsmCaptureShape;
        private System.Windows.Forms.ToolStripMenuItem tsmFileUpload;
        private TabControl tcAdvanced;
        private TabPage tpAdvancedSettings;
        private PropertyGrid pgAppSettings;
        internal DestSelector ucDestOptions;
        private CheckBox chkShowUploadResults;
        private TabPage tpCropShotLast;
        private Button btnLastCropShotReset;
        internal GroupBox gbCropShotMagnifyingGlass;
        private TabPage tpAdvancedWorkflow;
        internal PropertyGrid pgWorkflow;
        private TabPage tpAdvancedCore;
        private TabPage tpCaptureShape;
        internal PropertyGrid pgSurfaceConfig;
        internal GroupBox gbCropEngine;
        private ComboBox cboCropEngine;
        private ToolStrip tsLinks;
        private ToolStripButton tsbLinkHome;
        private ToolStripButton tsbLinkBugs;
        private ToolStripButton tsbLinkTutorials;
        private TabPage tpQueue;
        private HelpersLib.MyListView lvUploads;
        private ColumnHeader chFilename;
        private ColumnHeader chStatus;
        private ColumnHeader chProgress;
        private ColumnHeader chSpeed;
        private ColumnHeader chElapsed;
        private ColumnHeader chRemaining;
        private ColumnHeader chUploaderType;
        private ColumnHeader chHost;
        private ColumnHeader chURL;
        private HelpersLib.Hotkey.HotkeyManagerControl hmHotkeys;
        private GroupBox gbCaptureGdi;
        internal GroupBox gbCaptureEngine;
        private ComboBox cboCaptureEngine;
        private HelpersLib.MyPictureBox pbPreview;
        private HelpProvider helpProvider1;
        internal PictureBox pbActiveWindowDwmBackColor;
        private GroupBox gbCaptureDwm;
        private CheckBox chkActiveWindowDwmCustomColor;
        private TabPage tpAdvanedUploaders;
        internal PropertyGrid pgUploaders;
        private Button btnActionsUI;
        private MenuStrip msApp;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem tsmiFileUpload;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripMenuItem tsmiExit;
        private ToolStripMenuItem tsmiConfigure;
        private ToolStripMenuItem tsmiImageSettings;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem tsmiVersionHistory;
        private ToolStripSeparator toolStripSeparator10;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem tsmiWatermark;
        private ToolStripMenuItem tsmiConfigureFileNaming;
        private ToolStripMenuItem tsmiConfigureActions;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripMenuItem tsmiOutputs;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem tsmiOptions;
        private ToolStripMenuItem tsmiProxy;
    }
}