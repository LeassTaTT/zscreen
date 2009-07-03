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
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Greenshot.Helpers;
using ZSS.ColorsLib;
using ZSS.Forms;
using ZSS.Global;
using ZSS.Helpers;
using ZSS.ImageUploaderLib;
using ZSS.ImageUploaderLib.Helpers;
using ZSS.Properties;
using ZSS.Tasks;
using ZSS.TextUploaderLib.URLShorteners;
using ZSS.TextUploadersLib;
using ZSS.UpdateCheckerLib;

namespace ZSS
{
    public partial class ZScreen : Form
    {
        #region Variables

        private bool mGuiIsReady, mClose;

        private int mHadFocusAt;
        private TextBox mHadFocus;

        private ContextMenuStrip codesMenu = new ContextMenuStrip();

        private Debug debug;
        private List<int> mLogoRandomList = new List<int>(5);

        #endregion

        public ZScreen()
        {
            InitializeComponent();

            SetFormSettings();
            UpdateGuiControls();

            Program.Worker = new WorkerPrimary(this);
            Program.Worker2 = new WorkerSecondary(this);
            Program.Worker2.PerformOnlineTasks();

            Program.ZScreenKeyboardHook.KeyDownEvent += new KeyEventHandler(Program.Worker.ScreenshotUsingHotkeys);

            if (Program.conf.CheckUpdates) Program.Worker2.CheckUpdates();
        }

        private void SetFormSettings()
        {
            this.Icon = Resources.zss_main;
            this.Text = Program.mAppInfo.GetApplicationTitle(McoreSystem.AppInfo.VersionDepth.MajorMinorBuildRevision);
            this.niTray.Text = this.Text;
            this.lblLogo.Text = this.Text;

            if (Program.conf.WindowSize.Height == 0 || Program.conf.WindowSize.Width == 0)
            {
                Program.conf.WindowSize = this.Size;
            }

            // Accounts
            ucFTPAccounts.btnAdd.Click += new EventHandler(FTPAccountAddButton_Click);
            ucFTPAccounts.btnRemove.Click += new EventHandler(FTPAccountsRemoveButton_Click);
            ucFTPAccounts.btnTest.Click += new EventHandler(FTPAccountsTestButton_Click);
            ucFTPAccounts.AccountsList.SelectedIndexChanged += new EventHandler(FTPAccountsList_SelectedIndexChanged);

            // Textloaders
            ucUrlShorteners.btnItemAdd.Click += new EventHandler(UrlShortenersAddButton_Click);
            ucUrlShorteners.btnItemRemove.Click += new EventHandler(UrlShortenersRemoveButton_Click);
            ucUrlShorteners.MyCollection.SelectedIndexChanged += new EventHandler(UrlShorteners_SelectedIndexChanged);
            ucUrlShorteners.btnItemTest.Click += new EventHandler(UrlShortenerTestButton_Click);

            ucTextUploaders.btnItemAdd.Click += new EventHandler(TextUploadersAddButton_Click);
            ucTextUploaders.btnItemRemove.Click += new EventHandler(TextUploadersRemoveButton_Click);
            ucTextUploaders.MyCollection.SelectedIndexChanged += new EventHandler(TextUploaders_SelectedIndexChanged);
            ucTextUploaders.btnItemTest.Click += new EventHandler(TextUploaderTestButton_Click);

            niTray.BalloonTipClicked += new EventHandler(niTray_BalloonTipClicked);
        }

        private void ZScreen_Load(object sender, EventArgs e)
        {
            FileSystem.AppendDebug("Started ZScreen");
            FileSystem.AppendDebug(string.Format("Root Folder: {0}", Program.RootAppFolder));

            tcAccounts.TabPages.Remove(tpMindTouch);

            AddToClipboardByDoubleClick(tpHistory);

            // Context Menu
            tsmDestinations.Text = tpDestinations.Text;
            tsmImageHosting.Text = tpImageHosting.Text;
            tsmTextServices.Text = tpTextServices.Text;

            // Window Behaviour
            if (Program.conf.ActionsToolbarMode)
            {
                this.Hide();
                Program.Worker.ShowActionsToolbar(false);
            }
            else
            {
                if (Program.conf.OpenMainWindow)
                {
                    WindowState = FormWindowState.Normal;
                    Size = Program.conf.WindowSize;
                    ShowInTaskbar = Program.conf.ShowInTaskbar;
                }
                else
                {
                    Hide();
                }
            }

            Program.Worker2.CleanCache();
            StartDebug();

            FillClipboardCopyMenu();
            FillClipboardMenu();

            CreateCodesMenu();

            dgvHotkeys.BackgroundColor = Color.FromArgb(tpHotkeys.BackColor.R, tpHotkeys.BackColor.G, tpHotkeys.BackColor.B);

            niTray.Visible = true;
        }

        private void SetupScreen()
        {
            #region Global

            //~~~~~~~~~~~~~~~~~~~~~
            //  Global
            //~~~~~~~~~~~~~~~~~~~~~

            confApp.SelectedObject = Program.conf;
            txtRootFolder.Text = Program.RootAppFolder;
            UpdateGuiControlsPaths();

            #endregion

            #region Main

            //~~~~~~~~~~~~~~~~~~~~~
            //  Main
            //~~~~~~~~~~~~~~~~~~~~~

            if (cboImagesDest.Items.Count == 0)
            {
                cboImagesDest.Items.AddRange(typeof(ImageDestType).GetDescriptions());
            }
            cboImagesDest.SelectedIndex = (int)Program.conf.ScreenshotDestMode;
            if (cboClipboardTextMode.Items.Count == 0)
            {
                cboClipboardTextMode.Items.AddRange(typeof(ClipboardUriType).GetDescriptions());
            }
            cboClipboardTextMode.SelectedIndex = (int)Program.conf.ClipboardUriMode;
            nudtScreenshotDelay.Time = Program.conf.ScreenshotDelayTimes;
            nudtScreenshotDelay.Value = Program.conf.ScreenshotDelayTime;
            chkManualNaming.Checked = Program.conf.ManualNaming;
            cbShowCursor.Checked = Program.conf.ShowCursor;
            cboCropGridMode.Checked = Program.conf.CropGridToggle;
            nudCropGridWidth.Value = Program.conf.CropGridSize.Width;
            nudCropGridHeight.Value = Program.conf.CropGridSize.Height;

            #endregion

            #region Hotkeys

            //~~~~~~~~~~~~~~~~~~~~~
            //  Hotkeys
            //~~~~~~~~~~~~~~~~~~~~~

            UpdateHotkeysDGV();

            #endregion

            #region Capture

            //~~~~~~~~~~~~~~~~~~~~~
            //  Capture
            //~~~~~~~~~~~~~~~~~~~~~

            // Crop Shot
            if (cbCropStyle.Items.Count == 0)
            {
                cbCropStyle.Items.AddRange(typeof(RegionStyles).GetDescriptions());
            }
            cbCropStyle.SelectedIndex = (int)Program.conf.CropRegionStyles;
            cbRegionRectangleInfo.Checked = Program.conf.CropRegionRectangleInfo;
            cbRegionHotkeyInfo.Checked = Program.conf.CropRegionHotkeyInfo;

            cbCropDynamicCrosshair.Checked = Program.conf.CropDynamicCrosshair;
            nudCropCrosshairInterval.Value = Program.conf.CropInterval;
            nudCropCrosshairStep.Value = Program.conf.CropStep;
            nudCrosshairLineCount.Value = Program.conf.CrosshairLineCount;
            nudCrosshairLineSize.Value = Program.conf.CrosshairLineSize;
            pbCropCrosshairColor.BackColor = XMLSettings.DeserializeColor(Program.conf.CropCrosshairColor);
            cbCropShowBigCross.Checked = Program.conf.CropShowBigCross;
            cbCropShowMagnifyingGlass.Checked = Program.conf.CropShowMagnifyingGlass;

            cbShowCropRuler.Checked = Program.conf.CropShowRuler;
            cbCropDynamicBorderColor.Checked = Program.conf.CropDynamicBorderColor;
            nudCropRegionInterval.Value = Program.conf.CropRegionInterval;
            nudCropRegionStep.Value = Program.conf.CropRegionStep;
            nudCropHueRange.Value = Program.conf.CropHueRange;
            pbCropBorderColor.BackColor = XMLSettings.DeserializeColor(Program.conf.CropBorderColor);
            nudCropBorderSize.Value = Program.conf.CropBorderSize;
            cbCropShowGrids.Checked = Program.conf.CropShowGrids;

            // Selected Window
            if (cbSelectedWindowStyle.Items.Count == 0)
            {
                cbSelectedWindowStyle.Items.AddRange(typeof(RegionStyles).GetDescriptions());
            }
            cbSelectedWindowStyle.SelectedIndex = (int)Program.conf.SelectedWindowRegionStyles;
            cbSelectedWindowFront.Checked = Program.conf.SelectedWindowFront;
            cbSelectedWindowRectangleInfo.Checked = Program.conf.SelectedWindowRectangleInfo;
            cbSelectedWindowRuler.Checked = Program.conf.SelectedWindowRuler;
            pbSelectedWindowBorderColor.BackColor = XMLSettings.DeserializeColor(Program.conf.SelectedWindowBorderColor);
            nudSelectedWindowBorderSize.Value = Program.conf.SelectedWindowBorderSize;
            cbSelectedWindowDynamicBorderColor.Checked = Program.conf.SelectedWindowDynamicBorderColor;
            nudSelectedWindowRegionInterval.Value = Program.conf.SelectedWindowRegionInterval;
            nudSelectedWindowRegionStep.Value = Program.conf.SelectedWindowRegionStep;
            nudSelectedWindowHueRange.Value = Program.conf.SelectedWindowHueRange;
            cbSelectedWindowAddBorder.Checked = Program.conf.SelectedWindowAddBorder;

            // Interaction
            nudFlashIconCount.Value = Program.conf.FlashTrayCount;
            chkCaptureFallback.Checked = Program.conf.CaptureEntireScreenOnError;
            cbShowPopup.Checked = Program.conf.ShowBalloonTip;
            chkBalloonTipOpenLink.Checked = Program.conf.BalloonTipOpenLink;
            cbShowUploadDuration.Checked = Program.conf.ShowUploadDuration;
            cbCompleteSound.Checked = Program.conf.CompleteSound;
            cbCloseDropBox.Checked = Program.conf.CloseDropBox;
            cbCloseQuickActions.Checked = Program.conf.CloseQuickActions;

            // Naming Conventions
            txtActiveWindow.Text = Program.conf.NamingActiveWindow;
            txtEntireScreen.Text = Program.conf.NamingEntireScreen;

            // Watermark
            if (cboWatermarkType.Items.Count == 0)
            {
                cboWatermarkType.Items.AddRange(typeof(WatermarkType).GetDescriptions());
            }
            cboWatermarkType.SelectedIndex = (int)Program.conf.WatermarkMode;
            if (cbWatermarkPosition.Items.Count == 0)
            {
                cbWatermarkPosition.Items.AddRange(typeof(WatermarkPositionType).GetDescriptions());
            }
            cbWatermarkPosition.SelectedIndex = (int)Program.conf.WatermarkPositionMode;
            nudWatermarkOffset.Value = Program.conf.WatermarkOffset;
            cbWatermarkAddReflection.Checked = Program.conf.WatermarkAddReflection;
            cbWatermarkAutoHide.Checked = Program.conf.WatermarkAutoHide;

            txtWatermarkText.Text = Program.conf.WatermarkText;
            pbWatermarkFontColor.BackColor = XMLSettings.DeserializeColor(Program.conf.WatermarkFontColor);
            lblWatermarkFont.Text = FontToString();
            nudWatermarkFontTrans.Value = Program.conf.WatermarkFontTrans;
            trackWatermarkFontTrans.Value = (int)Program.conf.WatermarkFontTrans;
            nudWatermarkCornerRadius.Value = Program.conf.WatermarkCornerRadius;
            pbWatermarkGradient1.BackColor = XMLSettings.DeserializeColor(Program.conf.WatermarkGradient1);
            pbWatermarkGradient2.BackColor = XMLSettings.DeserializeColor(Program.conf.WatermarkGradient2);
            pbWatermarkBorderColor.BackColor = XMLSettings.DeserializeColor(Program.conf.WatermarkBorderColor);
            nudWatermarkBackTrans.Value = Program.conf.WatermarkBackTrans;
            trackWatermarkBackgroundTrans.Value = (int)Program.conf.WatermarkBackTrans;
            if (cbWatermarkGradientType.Items.Count == 0)
            {
                cbWatermarkGradientType.Items.AddRange(Enum.GetNames(typeof(LinearGradientMode)));
            }
            cbWatermarkGradientType.SelectedIndex = (int)Program.conf.WatermarkGradientType;

            txtWatermarkImageLocation.Text = Program.conf.WatermarkImageLocation;
            cbWatermarkUseBorder.Checked = Program.conf.WatermarkUseBorder;
            nudWatermarkImageScale.Value = Program.conf.WatermarkImageScale;

            TestWatermark();

            // Quality
            if (cbFileFormat.Items.Count == 0) cbFileFormat.Items.AddRange(Program.zImageFileTypes);
            cbFileFormat.SelectedIndex = Program.conf.FileFormat;
            nudImageQuality.Value = Program.conf.ImageQuality;
            nudSwitchAfter.Value = Program.conf.SwitchAfter;
            if (cbSwitchFormat.Items.Count == 0) cbSwitchFormat.Items.AddRange(Program.zImageFileTypes);
            cbSwitchFormat.SelectedIndex = Program.conf.SwitchFormat;

            #endregion

            ///////////////////////////////////
            // Text Uploader Settings
            ///////////////////////////////////

            if (Program.conf.TextUploadersList.Count == 0)
            {
                Program.conf.TextUploadersList = new List<TextUploader> { new Paste2Uploader(), new PastebinUploader(), new SlexyUploader() };
            }
            if (Program.conf.UrlShortenersList.Count == 0)
            {
                Program.conf.UrlShortenersList = new List<TextUploader> { new ThreelyUploader(), new TinyURLUploader() };
            }

            ucTextUploaders.MyCollection.Items.Clear();
            cboTextDest.Items.Clear();
            foreach (TextUploader textUploader in Program.conf.TextUploadersList)
            {
                if (textUploader != null)
                {
                    ucTextUploaders.MyCollection.Items.Add(textUploader);
                    cboTextDest.Items.Add(textUploader);
                }
            }
            if (Program.conf.SelectedTextUploader > -1 && Program.conf.SelectedTextUploader < ucTextUploaders.MyCollection.Items.Count)
            {
                ucTextUploaders.MyCollection.SelectedIndex = Program.conf.SelectedTextUploader;
                cboTextDest.SelectedIndex = Program.conf.SelectedTextUploader;
            }

            ucTextUploaders.Templates.Items.Clear();
            ucTextUploaders.Templates.Items.AddRange(typeof(TextDestType).GetDescriptions());
            ucTextUploaders.Templates.SelectedIndex = 1;

            if (Program.conf.TextEditors.Count == 0)
            {
                Program.conf.TextEditors.Add(new Software("Notepad", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "notepad.exe"), true));
            }

            ///////////////////////////////////
            // URL Shorteners Settings
            ///////////////////////////////////

            ucUrlShorteners.MyCollection.Items.Clear();
            foreach (TextUploader textUploader in Program.conf.UrlShortenersList)
            {
                if (textUploader != null)
                {
                    ucUrlShorteners.MyCollection.Items.Add(textUploader);
                }
            }
            if (Program.conf.SelectedUrlShortener > -1 && Program.conf.SelectedUrlShortener < ucUrlShorteners.MyCollection.Items.Count)
            {
                ucUrlShorteners.MyCollection.SelectedIndex = Program.conf.SelectedUrlShortener;
            }
            ucUrlShorteners.Templates.Items.Clear();
            ucUrlShorteners.Templates.Items.AddRange((typeof(UrlShortenerType).GetDescriptions()));
            ucUrlShorteners.Templates.SelectedIndex = 0;

            ///////////////////////////////////
            // FTP Settings
            ///////////////////////////////////

            if (Program.conf.FTPAccountList == null || Program.conf.FTPAccountList.Count == 0)
            {
                FTPSetup(new List<FTPAccount>());
            }
            else
            {
                FTPSetup(Program.conf.FTPAccountList);
                if (ucFTPAccounts.AccountsList.Items.Count > 0)
                {
                    ucFTPAccounts.AccountsList.SelectedIndex = Program.conf.FTPSelected;
                }
            }
            chkEnableThumbnail.Checked = Program.conf.FTPCreateThumbnail;
            cbAutoSwitchFTP.Checked = Program.conf.AutoSwitchFTP;

            ///////////////////////////////////
            // HTTP Settings
            ///////////////////////////////////

            chkRememberTinyPicUserPass.Checked = Program.conf.RememberTinyPicUserPass;
            txtUserNameImageShack.Text = Program.conf.ImageShackUserName;
            txtImageShackRegistrationCode.Text = Program.conf.ImageShackRegistrationCode;
            txtTinyPicShuk.Text = Program.conf.TinyPicShuk;
            nudErrorRetry.Value = Program.conf.ErrorRetryCount;
            cboAutoChangeUploadDestination.Checked = Program.conf.AutoChangeUploadDestination;
            nudUploadDurationLimit.Value = Program.conf.UploadDurationLimit;

            txtTwitPicUserName.Text = Program.conf.TwitPicUserName;
            txtTwitPicPassword.Text = Program.conf.TwitPicPassword;
            if (cboTwitPicUploadMode.Items.Count == 0)
            {
                cboTwitPicUploadMode.Items.AddRange(typeof(TwitPicUploadType).GetDescriptions());
            }
            cboTwitPicUploadMode.SelectedIndex = (int)Program.conf.TwiPicUploadMode;
            if (cboUploadMode.Items.Count == 0)
            {
                cboUploadMode.Items.AddRange(typeof(UploadMode).GetDescriptions());
            }
            cboUploadMode.SelectedIndex = (int)Program.conf.UploadMode;
            chkImageUploadRetry.Checked = Program.conf.ImageUploadRetry;
            cbClipboardTranslate.Checked = Program.conf.ClipboardTranslate;
            cbAutoTranslate.Checked = Program.conf.AutoTranslate;
            txtAutoTranslate.Text = Program.conf.AutoTranslateLength.ToString();
            cbAddFailedScreenshot.Checked = Program.conf.AddFailedScreenshot;
            cbTinyPicSizeCheck.Checked = Program.conf.TinyPicSizeCheck;

            #region "Image Editors"

            ///////////////////////////////////
            // Image Software Settings
            ///////////////////////////////////

            Software disabled = new Software(Program.DISABLED_IMAGE_EDITOR, "", true);
            Software editor = new Software(Program.ZSCREEN_IMAGE_EDITOR, Application.ExecutablePath, true);
            Software paint = new Software("Paint", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "mspaint.exe"));

            if (Program.conf.ImageEditors.Count == 0)
            {
                Program.conf.ImageEditors.Add(disabled);
                Program.conf.ImageEditors.Add(editor);
                Program.conf.ImageEditors.Add(paint);
            }
            RegistryMgr.FindImageEditors();

            if (Program.conf.ImageEditor == null)
            {
                Program.conf.ImageEditor = Program.conf.ImageEditors[0];
            }

            lbImageSoftware.Items.Clear();

            foreach (Software app in Program.conf.ImageEditors)
            {
                if (!String.IsNullOrEmpty(app.Name))
                {
                    lbImageSoftware.Items.Add(app.Name);
                }
            }

            if (Adapter.ImageSoftwareEnabled())
            {
                int i = lbImageSoftware.Items.IndexOf(Program.conf.ImageEditor.Name);
                if (i != -1)
                {
                    lbImageSoftware.SelectedIndex = i;
                }
            }
            else
            {
                int i = lbImageSoftware.Items.IndexOf(Program.DISABLED_IMAGE_EDITOR);
                if (i != -1)
                {
                    lbImageSoftware.SelectedIndex = i;
                }
            }

            chkImageEditorAutoSave.Checked = Program.conf.ImageEditorAutoSave;

            #endregion

            ///////////////////////////////////
            // Advanced Settings
            ///////////////////////////////////

            cbStartWin.Checked = RegistryMgr.CheckStartWithWindows();
            cbOpenMainWindow.Checked = Program.conf.OpenMainWindow;
            cbShowTaskbar.Checked = Program.conf.ShowInTaskbar;
            cbLockFormSize.Checked = Program.conf.LockFormSize;
            cbShowHelpBalloonTips.Checked = Program.conf.ShowHelpBalloonTips;
            if (cboUpdateCheckType.Items.Count == 0)
            {
                cboUpdateCheckType.Items.AddRange(typeof(UpdateCheckType).GetDescriptions());
            }
            cboUpdateCheckType.SelectedIndex = (int)Program.conf.UpdateCheckType;
            cbCheckUpdates.Checked = Program.conf.CheckUpdates;
            cbCheckExperimental.Enabled = Program.conf.CheckUpdates;
            nudCacheSize.Value = Program.conf.ScreenshotCacheSize;
            cbDeleteLocal.Checked = Program.conf.DeleteLocal;
            cbCheckExperimental.Checked = Program.conf.CheckExperimental;

            ///////////////////////////////////
            // Image MyCollection
            ///////////////////////////////////

            lbUploader.Items.Clear();
            if (Program.conf.ImageUploadersList == null)
            {
                Program.conf.ImageUploadersList = new List<ImageHostingService>();
                LoadImageUploaders(new ImageHostingService());
            }
            else
            {
                List<ImageHostingService> iUploaders = Program.conf.ImageUploadersList;
                foreach (ImageHostingService iUploader in iUploaders)
                {
                    lbUploader.Items.Add(iUploader.Name);
                }
                if (lbUploader.Items.Count > 0)
                {
                    lbUploader.SelectedIndex = Program.conf.ImageUploaderSelected;
                }
                if (lbUploader.SelectedIndex != -1)
                {
                    LoadImageUploaders(Program.conf.ImageUploadersList[lbUploader.SelectedIndex]);
                }
            }

            ///////////////////////////////////
            // History
            ///////////////////////////////////

            cbHistorySave.Checked = Program.conf.HistorySave;
            if (cbHistoryListFormat.Items.Count == 0)
            {
                cbHistoryListFormat.Items.AddRange(typeof(HistoryListFormat).GetDescriptions());
            }
            cbHistoryListFormat.SelectedIndex = (int)Program.conf.HistoryListFormat;
            cbShowHistoryTooltip.Checked = Program.conf.HistoryShowTooltips;
            cbHistoryAddSpace.Checked = Program.conf.HistoryAddSpace;
            cbHistoryReverseList.Checked = Program.conf.HistoryReverseList;
            LoadHistoryItems();
            nudHistoryMaxItems.Value = Program.conf.HistoryMaxNumber;
        }

        private void UpdateGuiControlsPaths()
        {
            Program.InitializeDefaultFolderPaths();
            txtImagesDir.Text = Program.ImagesDir;
            txtCacheDir.Text = Program.CacheDir;
            txtSettingsDir.Text = Program.SettingsDir;
        }

    
        #region "GUI Methods"

        private void cbCloseQuickActions_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.CloseQuickActions = cbCloseQuickActions.Checked;
        }


        #endregion

        #region "Event Handlers"

        private void LoadHistoryItems()
        {
            lbHistory.Items.Clear();
            HistoryManager history = HistoryManager.Read();
            for (int i = 0; i < history.HistoryItems.Count && i < Program.conf.HistoryMaxNumber; i++)
            {
                lbHistory.Items.Add(history.HistoryItems[i]);
            }
            if (lbHistory.Items.Count > 0)
            {
                lbHistory.SelectedIndex = 0;
            }
            if (mGuiIsReady)
            {
                Program.Worker.UpdateGuiControlsHistory();
            }
        }

        private void exitZScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mClose = true;
            Close();
        }

        private void cbRegionRectangleInfo_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.CropRegionRectangleInfo = cbRegionRectangleInfo.Checked;
        }

        private void cbRegionHotkeyInfo_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.CropRegionHotkeyInfo = cbRegionHotkeyInfo.Checked;
        }

        private void niTray_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowMainWindow();
        }

        private void tsmQuickOptions_Click(object sender, EventArgs e)
        {
            Program.Worker.ShowQuickOptions();
        }

        private void btnRegCodeImageShack_Click(object sender, EventArgs e)
        {
            Process.Start("http://my.imageshack.us/registration/");
        }

        private void nErrorRetry_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.ErrorRetryCount = nudErrorRetry.Value;
        }

        private void btnGalleryImageShack_Click(object sender, EventArgs e)
        {
            Process.Start("http://my.imageshack.us/v_images.php");
        }

        private void ZScreen_Resize(object sender, EventArgs e)
        {
            if (mGuiIsReady)
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    if (!Program.conf.ShowInTaskbar)
                    {
                        this.Hide();
                        if (Program.conf.AutoSaveSettings) WriteSettings();
                    }
                }
                else if (this.WindowState == FormWindowState.Normal)
                {
                    Program.conf.WindowSize = this.Size;
                    this.ShowInTaskbar = Program.conf.ShowInTaskbar;
                    this.Refresh();
                }
            }
        }

        private void ZScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            /* 
             * Sometimes Settings.xml write delays cause a small pause when user press the close button
             * Noticing this is avoided by this.WindowState = FormWindowState.Minimized; 
            */
            this.WindowState = FormWindowState.Minimized;
            WriteSettings();

            if (!mClose && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
            else
            {
                FileSystem.AppendDebug("Closed " + Application.ProductName + "\n");
                FileSystem.WriteDebugFile();
            }
        }

        #endregion

        private void WriteSettings()
        {
            Program.conf.Save();
            Program.Worker.SaveHistoryItems();
            Settings.Default.Save();
            Console.WriteLine("Settings written to file.");
        }

        private void RewriteImageEditorsRightClickMenu()
        {
            if (Program.conf.ImageEditors != null)
            {
                tsmEditinImageSoftware.DropDownDirection = ToolStripDropDownDirection.Right;

                tsmEditinImageSoftware.DropDownItems.Clear();

                List<Software> imgs = Program.conf.ImageEditors;

                //tsm.TextDirection = ToolStripTextDirection.Horizontal;
                tsmEditinImageSoftware.DropDownDirection = ToolStripDropDownDirection.Right;

                for (int x = 0; x < imgs.Count; x++)
                {
                    ToolStripMenuItem tsm = new ToolStripMenuItem { Text = imgs[x].Name, CheckOnClick = true };
                    tsm.Click += new EventHandler(RightClickIsItemClick);
                    tsmEditinImageSoftware.DropDownItems.Add(tsm);
                    if (imgs[x].Name == Program.DISABLED_IMAGE_EDITOR)
                    {
                        tsmEditinImageSoftware.DropDownItems.Add(new ToolStripSeparator());
                    }
                }

                //check the active ftpUpload account

                if (Adapter.ImageSoftwareEnabled())
                {
                    CheckCorrectIsRightClickMenu(Program.conf.ImageEditor.Name);
                }
                else
                {
                    CheckCorrectIsRightClickMenu(Program.DISABLED_IMAGE_EDITOR);
                }
                tsmEditinImageSoftware.DropDownDirection = ToolStripDropDownDirection.Right;

                //show drop down menu in the correct place if menu is selected
                if (tsmEditinImageSoftware.Selected)
                {
                    tsmEditinImageSoftware.DropDown.Hide();
                    tsmEditinImageSoftware.DropDown.Show();
                }
            }
        }

        private void DisableImageSoftwareClick(object sender, EventArgs e)
        {
            //cbRunImageSoftware.Checked = false;

            //select "Disabled"
            lbImageSoftware.SelectedIndex = 0;

            CheckCorrectIsRightClickMenu(tsmEditinImageSoftware.DropDownItems[0].Text); //disabled
            //rewriteISRightClickMenu();
        }

        private void RightClickIsItemClick(object sender, EventArgs e)
        {
            ToolStripMenuItem tsm = (ToolStripMenuItem)sender;

            Program.conf.ImageEditor = GetImageSoftware(tsm.Text); //Program.conf.ImageSoftwareList[(int)tsm.Tag];

            if (lbImageSoftware.Items.IndexOf(tsm.Text) >= 0)
                lbImageSoftware.SelectedItem = tsm.Text;

            //Turn on Image Software
            //cbRunImageSoftware.Checked = true;

            //rewriteISRightClickMenu();
        }

        private void CheckCorrectIsRightClickMenu(string txt)
        {
            ToolStripMenuItem tsm;

            for (int x = 0; x < tsmEditinImageSoftware.DropDownItems.Count; x++)
            {
                //if (tsmImageSoftware.DropDownItems[x].GetType() == typeof(ToolStripMenuItem))
                if (tsmEditinImageSoftware.DropDownItems[x] is ToolStripMenuItem)
                {
                    tsm = (ToolStripMenuItem)tsmEditinImageSoftware.DropDownItems[x];

                    if (tsm.Text == txt)
                    {
                        tsm.CheckState = CheckState.Checked;
                    }
                    else
                    {
                        tsm.CheckState = CheckState.Unchecked;
                    }
                }
            }
        }

        private void RewriteCustomUploaderRightClickMenu()
        {
            if (Program.conf.ImageUploadersList != null)
            {
                List<ImageHostingService> lUploaders = Program.conf.ImageUploadersList;
                ToolStripMenuItem tsm;
                tsmDestCustomHTTP.DropDownDirection = ToolStripDropDownDirection.Right;
                tsmDestCustomHTTP.DropDownItems.Clear();

                for (int i = 0; i < lUploaders.Count; i++)
                {
                    tsm = new ToolStripMenuItem { CheckOnClick = true, Tag = i, Text = lUploaders[i].Name };
                    tsm.Click += rightClickIHS_Click;
                    tsmDestCustomHTTP.DropDownItems.Add(tsm);
                }

                CheckCorrectMenuItemClicked(ref tsmDestCustomHTTP, Program.conf.ImageUploaderSelected);

                tsmDestCustomHTTP.DropDownDirection = ToolStripDropDownDirection.Right;

                //show drop down menu in the correct place if menu is selected
                if (tsmDestCustomHTTP.Selected)
                {
                    tsmDestCustomHTTP.DropDown.Hide();
                    tsmDestCustomHTTP.DropDown.Show();
                }
            }
        }

        private void rightClickIHS_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsm = (ToolStripMenuItem)sender;
            lbUploader.SelectedIndex = (int)tsm.Tag;
        }

        private void FillClipboardCopyMenu()
        {
            tsmCopyCbHistory.DropDownDirection = ToolStripDropDownDirection.Right;
            tsmCopyCbHistory.DropDownItems.Clear();

            ToolStripMenuItem tsm;
            int x = 0;
            foreach (ClipboardUriType cui in Enum.GetValues(typeof(ClipboardUriType)))
            {
                tsm = new ToolStripMenuItem { Tag = x++, Text = cui.GetDescription() };
                tsm.Click += clipboardCopyHistory_Click;
                tsmCopyCbHistory.DropDownItems.Add(tsm);
            }
        }

        private void clipboardCopyHistory_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsm = (ToolStripMenuItem)sender;
            SetClipboardFromHistory((ClipboardUriType)tsm.Tag);
        }

        private void FillClipboardMenu()
        {
            tsmCopytoClipboardMode.DropDownDirection = ToolStripDropDownDirection.Right;
            tsmCopytoClipboardMode.DropDownItems.Clear();

            ToolStripMenuItem tsm;
            int x = 0;
            foreach (ClipboardUriType cui in Enum.GetValues(typeof(ClipboardUriType)))
            {
                tsm = new ToolStripMenuItem { Tag = x++, CheckOnClick = true, Text = cui.GetDescription() };
                tsm.Click += new EventHandler(ClipboardModeClick);
                tsmCopytoClipboardMode.DropDownItems.Add(tsm);
            }

            CheckCorrectMenuItemClicked(ref tsmCopytoClipboardMode, (int)Program.conf.ClipboardUriMode);
            tsmCopytoClipboardMode.DropDownDirection = ToolStripDropDownDirection.Right;
        }

        private void ClipboardModeClick(object sender, EventArgs e)
        {
            ToolStripMenuItem tsm = (ToolStripMenuItem)sender;
            Program.conf.ClipboardUriMode = (ClipboardUriType)tsm.Tag;
            CheckCorrectMenuItemClicked(ref tsmCopytoClipboardMode, (int)Program.conf.ClipboardUriMode);
            cboClipboardTextMode.SelectedIndex = (int)Program.conf.ClipboardUriMode;
        }

        private void RewriteFTPRightClickMenu()
        {
            if (Program.conf.FTPAccountList != null)
            {
                tsmDestFTP.DropDownDirection = ToolStripDropDownDirection.Right;
                tsmDestFTP.DropDownItems.Clear();
                List<FTPAccount> accs = Program.conf.FTPAccountList;
                ToolStripMenuItem tsm;
                //tsm.TextDirection = ToolStripTextDirection.Horizontal;
                tsmDestFTP.DropDownDirection = ToolStripDropDownDirection.Right;

                for (int x = 0; x < accs.Count; x++)
                {
                    tsm = new ToolStripMenuItem { Tag = x, CheckOnClick = true, Text = accs[x].Name };
                    tsm.Click += rightClickFTPItem_Click;
                    tsmDestFTP.DropDownItems.Add(tsm);
                }

                //check the active ftpUpload account
                CheckCorrectMenuItemClicked(ref tsmDestFTP, Program.conf.FTPSelected);
                tsmDestFTP.DropDownDirection = ToolStripDropDownDirection.Right;

                //show drop down menu in the correct place if menu is selected
                if (tsmDestFTP.Selected)
                {
                    tsmDestFTP.DropDown.Hide();
                    tsmDestFTP.DropDown.Show();
                }
            }
        }

        private void rightClickFTPItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsm = (ToolStripMenuItem)sender;

            //Program.conf.FTPselected = (int)tsm.Tag;

            ucFTPAccounts.AccountsList.SelectedIndex = (int)tsm.Tag;

            //rewriteFTPRightClickMenu();
        }

        private void CheckCorrectMenuItemClicked(ref ToolStripMenuItem mi, int index)
        {
            ToolStripMenuItem tsm;

            for (int x = 0; x < mi.DropDownItems.Count; x++)
            {
                tsm = (ToolStripMenuItem)mi.DropDownItems[x];

                if (index == x)
                {
                    tsm.CheckState = CheckState.Checked;
                }
                else
                {
                    tsm.CheckState = CheckState.Unchecked;
                }
            }
        }

        private void btnBrowseImageSoftware_Click(object sender, EventArgs e)
        {
            if (lbImageSoftware.SelectedIndex > -1)
            {
                Software temp = BrowseImageSoftware();
                if (temp != null)
                {
                    lbImageSoftware.Items[lbImageSoftware.SelectedIndex] = temp;
                    Program.conf.ImageEditors[lbImageSoftware.SelectedIndex] = temp;
                    ShowImageEditorsSettings();
                }
            }
        }

        /// <summary>
        /// Browse for an Image Editor
        /// </summary>
        /// <returns>Image Editor</returns>
        private Software BrowseImageSoftware()
        {
            Software temp = null;

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Executable files (*.exe)|*.exe";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                temp = new Software();
                temp.Name = Path.GetFileNameWithoutExtension(dlg.FileName);
                temp.Path = dlg.FileName;
            }

            return temp;

        }

        private void tsmSettings_Click(object sender, EventArgs e)
        {
            BringUpMenu();
        }

        private void tsmViewDirectory_Click(object sender, EventArgs e)
        {
            ShowDirectory(Program.ImagesDir);
        }

        private void ShowDirectory(string dir)
        {
            Process.Start("explorer.exe", dir);
        }

        private void tsmViewRemote_Click(object sender, EventArgs e)
        {
            if (Program.conf.FTPAccountList.Count > 0)
            {
                ViewRemote vr = new ViewRemote();
                vr.Show();
            }
        }

        private void txtActiveWindow_Leave(object sender, EventArgs e)
        {
            mHadFocus = (TextBox)sender;
            mHadFocusAt = ((TextBox)sender).SelectionStart;
        }

        private void txtEntireScreen_Leave(object sender, EventArgs e)
        {
            mHadFocus = (TextBox)sender;
            mHadFocusAt = ((TextBox)sender).SelectionStart;
        }

        private void btnCodes_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            const string beginning = "btnCodes";
            string name = b.Name, code;

            if (name.Contains(beginning))
            {
                name = name.Replace(beginning, "");
                code = "%" + name.ToLower();

                if (mHadFocus != null)
                {
                    mHadFocus.Text = mHadFocus.Text.Insert(mHadFocusAt, code);
                    mHadFocus.Focus();
                    mHadFocus.Select(mHadFocusAt + code.Length, 0);
                }
            }
        }

        private void BringUpMenu()
        {
            Show();
            WindowState = FormWindowState.Normal;
            this.Activate();
            this.BringToFront();
        }

        private void tsm_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsm = (ToolStripMenuItem)sender;

            TabPage sel = tpMain;

            if (tsm == tsmHotkeys)
                sel = tpHotkeys;
            else if (tsm == tsmCapture)
                sel = tpScreenshots;
            else if (tsm == tsmEditors)
                sel = tpEditors;
            else if (tsm == tsmDestinations)
                sel = tpDestinations;
            else if (tsm == tsmImageHosting)
                sel = tpImageHosting;
            else if (tsm == tsmTextServices)
                sel = tpTextServices;
            else if (tsm == tsmHistory)
                sel = tpHistory;
            else if (tsm == tsmTranslator)
                sel = tpTranslator;
            else if (tsm == tsmOptions)
                sel = tpOptions;

            tcApp.SelectedTab = sel;

            BringUpMenu();
            tcApp.Focus();
        }

        private void btnBrowseConfig_Click(object sender, EventArgs e)
        {
            ShowDirectory(Program.SettingsDir);
        }

        private void tsmLic_Click(object sender, EventArgs e)
        {
            FormsMgr.ShowLicense();
        }

        private void chkManualNaming_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.ManualNaming = chkManualNaming.Checked;
        }

        private void ZScreen_Shown(object sender, EventArgs e)
        {
            mGuiIsReady = true;
            if (lbHistory.Items.Count > 0)
            {
                lbHistory.SelectedIndex = 0;
            }

            // Show settings if never ran before
            if (!Program.conf.RunOnce)
            {
                Show();
                WindowState = FormWindowState.Normal;
                this.Activate();
                this.BringToFront();
                Program.conf.RunOnce = true;
            }

            if (Program.conf.BackupFTPSettings)
            {
                FileSystem.BackupFTPSettings();
            }
            if (Program.conf.BackupApplicationSettings)
            {
                FileSystem.BackupAppSettings();
            }
        }

        private void AddToClipboardByDoubleClick(Control tp)
        {
            Control ctl = tp.GetNextControl(tp, true);
            while (ctl != null)
            {
                if (ctl.GetType() == typeof(TextBox))
                {
                    ctl.DoubleClick += TextBox_DoubleClick;
                }
                ctl = tp.GetNextControl(ctl, true);
            }
        }

        void TextBox_DoubleClick(object sender, EventArgs e)
        {
            TextBox tb = ((TextBox)sender);
            if (!string.IsNullOrEmpty(tb.Text))
            {
                Clipboard.SetText(tb.Text);
            }
        }

        private void tsmAboutMain_Click(object sender, EventArgs e)
        {
            FormsMgr.ShowAboutWindow();
        }

        public void cbStartWin_CheckedChanged(object sender, EventArgs e)
        {
            RegistryMgr.SetStartWithWindows(cbStartWin.Checked);
        }

        private void nudFlashIconCount_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.FlashTrayCount = nudFlashIconCount.Value;
        }

        private void nudCacheSize_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.ScreenshotCacheSize = nudCacheSize.Value;
        }

        private void txtCacheDir_TextChanged(object sender, EventArgs e)
        {
            Program.CacheDir = txtCacheDir.Text;
        }

        private void btnSettingsExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog { Filter = Program.FILTER_SETTINGS };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Program.conf.Save(dlg.FileName);
            }
        }

        private void btnSettingsImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog { Filter = Program.FILTER_SETTINGS };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                XMLSettings temp = XMLSettings.Read(dlg.FileName);
                if (temp.RunOnce)
                {
                    Program.conf = temp;
                    SetupScreen();
                }
            }
        }

        private void AddImageSoftwareToList(Software temp)
        {
            if (temp != null)
            {
                Program.conf.ImageEditors.Add(temp);
                lbImageSoftware.Items.Add(temp);
                lbImageSoftware.SelectedIndex = lbImageSoftware.Items.Count - 1;
            }
        }

        private void btnAddImageSoftware_Click(object sender, EventArgs e)
        {
            Software temp = BrowseImageSoftware();
            if (temp != null)
            {
                AddImageSoftwareToList(temp);
            }
        }

        private void btnDeleteImageSoftware_Click(object sender, EventArgs e)
        {
            int sel = lbImageSoftware.SelectedIndex;

            if (sel != -1)
            {
                Program.conf.ImageEditors.RemoveAt(sel);

                lbImageSoftware.Items.RemoveAt(sel);

                if (lbImageSoftware.Items.Count > 0)
                {
                    lbImageSoftware.SelectedIndex = (sel > 0) ? (sel - 1) : 0;
                }
            }

            RewriteImageEditorsRightClickMenu();
        }

        private void cboScreenshotDest_SelectedIndexChanged(object sender, EventArgs e)
        {
            ImageDestType sdt = (ImageDestType)cboImagesDest.SelectedIndex;
            Program.conf.ScreenshotDestMode = sdt;
            cboClipboardTextMode.Enabled = sdt != ImageDestType.CLIPBOARD && sdt != ImageDestType.FILE;

            switch (sdt)
            {
                case ImageDestType.CLIPBOARD:
                    CheckSendToMenu(tsmDestClipboard);
                    break;
                case ImageDestType.FILE:
                    CheckSendToMenu(tsmDestFile);
                    break;
                case ImageDestType.FTP:
                    CheckSendToMenu(tsmDestFTP);
                    break;
                case ImageDestType.IMAGESHACK:
                    CheckSendToMenu(tsmDestImageShack);
                    break;
                case ImageDestType.TINYPIC:
                    CheckSendToMenu(tsmDestTinyPic);
                    break;
                case ImageDestType.CUSTOM_UPLOADER:
                    CheckSendToMenu(tsmDestCustomHTTP);
                    break;
            }
        }

        private void CheckSendToMenu(ToolStripMenuItem item)
        {
            CheckToolStripMenuItem(tsmSendImageTo, item);
        }

        private void CheckToolStripMenuItem(ToolStripDropDownItem parent, ToolStripMenuItem item)
        {
            foreach (ToolStripMenuItem tsmi in parent.DropDownItems)
            {
                tsmi.Checked = tsmi == item;
            }

            tsmCopytoClipboardMode.Enabled = cboImagesDest.SelectedIndex != (int)ImageDestType.CLIPBOARD &&
                cboImagesDest.SelectedIndex != (int)ImageDestType.FILE;
        }

        private void tsmDestClipboard_Click(object sender, EventArgs e)
        {
            cboImagesDest.SelectedIndex = (int)ImageDestType.CLIPBOARD;
        }

        private void tsmDestFile_Click(object sender, EventArgs e)
        {
            cboImagesDest.SelectedIndex = (int)ImageDestType.FILE;
        }

        private void tsmDestFTP_Click(object sender, EventArgs e)
        {
            cboImagesDest.SelectedIndex = (int)ImageDestType.FTP;
        }

        private void tsmDestImageShack_Click(object sender, EventArgs e)
        {
            cboImagesDest.SelectedIndex = (int)ImageDestType.IMAGESHACK;
        }

        private void tsmDestTinyPic_Click(object sender, EventArgs e)
        {
            cboImagesDest.SelectedIndex = (int)ImageDestType.TINYPIC;
        }

        private void tsmDestCustomHTTP_Click(object sender, EventArgs e)
        {
            cboImagesDest.SelectedIndex = (int)ImageDestType.CUSTOM_UPLOADER;
        }

        private void SetActiveImageSoftware()
        {
            Program.conf.ImageEditor = Program.conf.ImageEditors[lbImageSoftware.SelectedIndex];
            RewriteImageEditorsRightClickMenu();
        }

        private void ShowImageEditorsSettings()
        {
            if (lbImageSoftware.SelectedItem != null)
            {
                Software app = GetImageSoftware(lbImageSoftware.SelectedItem.ToString());
                if (app != null)
                {
                    btnBrowseImageEditor.Enabled = !app.Protected;
                    pgEditorsImage.SelectedObject = app;
                    pgEditorsImage.Enabled = app.Name != Program.DISABLED_IMAGE_EDITOR;
                    btnRemoveImageEditor.Enabled = !app.Protected;

                    gbImageEditorSettings.Visible = app.Name == Program.ZSCREEN_IMAGE_EDITOR;

                    SetActiveImageSoftware();
                }
            }

        }

        private void lbImageSoftware_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowImageEditorsSettings();
        }

        private void cboClipboardTextMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.ClipboardUriMode = (ClipboardUriType)cboClipboardTextMode.SelectedIndex;
            UpdateClipboardTextTrayMenu();
        }

        private void UpdateClipboardTextTrayMenu()
        {

            foreach (ToolStripMenuItem tsmi in tsmCopytoClipboardMode.DropDownItems)
            {
                tsmi.Checked = false;
            }
            CheckCorrectMenuItemClicked(ref tsmCopytoClipboardMode, (int)Program.conf.ClipboardUriMode);
        }

        private void txtFileDirectory_TextChanged(object sender, EventArgs e)
        {
            Program.ImagesDir = txtImagesDir.Text;
        }

        private void cbDeleteLocal_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.DeleteLocal = cbDeleteLocal.Checked;
        }

        private void txtActiveWindow_TextChanged(object sender, EventArgs e)
        {
            Program.conf.NamingActiveWindow = txtActiveWindow.Text;
            lblActiveWindowPreview.Text = NameParser.Convert(NameParser.NameType.ActiveWindow, true);
        }

        private void txtEntireScreen_TextChanged(object sender, EventArgs e)
        {
            Program.conf.NamingEntireScreen = txtEntireScreen.Text;
            lblEntireScreenPreview.Text = NameParser.Convert(NameParser.NameType.EntireScreen, true);
        }

        private void cmbFileFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.FileFormat = cbFileFormat.SelectedIndex;
        }

        private void txtImageQuality_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.ImageQuality = nudImageQuality.Value;
        }

        private void cmbSwitchFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.SwitchFormat = cbSwitchFormat.SelectedIndex;
        }

        private void txtImageShackRegistrationCode_TextChanged(object sender, EventArgs e)
        {
            if (mGuiIsReady)
            {
                Program.conf.ImageShackRegistrationCode = txtImageShackRegistrationCode.Text;
            }
        }

        private void cbShowPopup_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.ShowBalloonTip = cbShowPopup.Checked;
        }

        /// <summary>
        /// Updates all the GUI Controls in ZScreen by deserializing the Settings.xml; 
        /// Loads default Settings.xml if fails to load any control
        /// </summary>
        private void UpdateGuiControls()
        {
            //try
            //{
            SetupScreen();
            CheckFormSettings();
            //}
            //catch (Exception ex)
            //{
            //    FileSystem.AppendDebug(ex.ToString());
            //    if (MessageBox.Show("Error occured while loading settings. Do you like to load Default settings?.\n\n" + ex.ToString(),
            //        Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            //    {
            //        LoadSettingsDefault();
            //    }
            //}
        }

        private void LoadSettingsDefault()
        {
            Program.conf = new XMLSettings();
            SetupScreen();
            Program.conf.RunOnce = true;
            Program.conf.Save();
        }

        private void btnDeleteSettings_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to revert settings to default values?", Application.ProductName,
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                LoadSettingsDefault();
            }
        }

        private void ShowMainWindow()
        {
            if (this.IsHandleCreated)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                User32.ActivateWindow(this.Handle);
            }
        }

        private void niTray_BalloonTipClicked(object sender, EventArgs e)
        {
            if (Program.conf.BalloonTipOpenLink)
            {
                try
                {
                    NotifyIcon ni = (NotifyIcon)sender;
                    if (ni.Tag != null)
                    {
                        MainAppTask t = (MainAppTask)niTray.Tag;
                        string cbString;
                        switch (t.Job)
                        {
                            case MainAppTask.Jobs.LANGUAGE_TRANSLATOR:
                                cbString = t.TranslationInfo.Result.TranslatedText;
                                if (!string.IsNullOrEmpty(cbString))
                                {
                                    Clipboard.SetText(cbString);
                                }
                                break;
                            default:
                                switch (t.ImageDestCategory)
                                {
                                    case ImageDestType.FILE:
                                    case ImageDestType.CLIPBOARD:
                                        cbString = t.LocalFilePath;
                                        if (!string.IsNullOrEmpty(cbString))
                                        {
                                            Process.Start(cbString);
                                        }
                                        break;
                                    default:
                                        cbString = t.RemoteFilePath;
                                        if (!string.IsNullOrEmpty(cbString))
                                        {
                                            Process.Start(cbString);
                                        }
                                        break;
                                }
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    FileSystem.AppendDebug(ex.ToString());
                }
            }
        }

        #region Image MyCollection

        private void btnUploaderAdd_Click(object sender, EventArgs e)
        {
            if (txtUploader.Text != "")
            {
                ImageHostingService iUploader = GetUploaderFromFields();
                Program.conf.ImageUploadersList.Add(iUploader);
                lbUploader.Items.Add(iUploader.Name);
                lbUploader.SelectedIndex = lbUploader.Items.Count - 1;
            }
        }

        private void btnUploaderRemove_Click(object sender, EventArgs e)
        {
            if (lbUploader.SelectedIndex != -1)
            {
                int selected = lbUploader.SelectedIndex;
                Program.conf.ImageUploadersList.RemoveAt(selected);
                lbUploader.Items.RemoveAt(selected);
                LoadImageUploaders(new ImageHostingService());
            }
        }

        private ImageHostingService GetUploaderFromFields()
        {
            ImageHostingService iUploader = new ImageHostingService(txtUploader.Text);
            foreach (ListViewItem lvItem in lvArguments.Items)
            {
                iUploader.Arguments.Add(new[] { lvItem.Text, lvItem.SubItems[1].Text });
            }
            iUploader.UploadURL = txtUploadURL.Text;
            iUploader.FileForm = txtFileForm.Text;
            foreach (ListViewItem lvItem in lvRegexps.Items)
            {
                iUploader.RegexpList.Add(lvItem.Text);
            }
            iUploader.Fullimage = txtFullImage.Text;
            iUploader.Thumbnail = txtThumbnail.Text;
            return iUploader;
        }

        private void btnArgAdd_Click(object sender, EventArgs e)
        {
            if (txtArg1.Text != "")
            {
                lvArguments.Items.Add(txtArg1.Text).SubItems.Add(txtArg2.Text);
                txtArg1.Text = "";
                txtArg2.Text = "";
                txtArg1.Focus();
            }
        }

        private void btnArgEdit_Click(object sender, EventArgs e)
        {
            if (lvArguments.SelectedItems.Count > 0 && txtArg1.Text != "")
            {
                lvArguments.SelectedItems[0].Text = txtArg1.Text;
                lvArguments.SelectedItems[0].SubItems[1].Text = txtArg2.Text;
            }
        }

        private void btnArgRemove_Click(object sender, EventArgs e)
        {
            if (lvArguments.SelectedItems.Count > 0)
            {
                lvArguments.SelectedItems[0].Remove();
            }
        }

        private void btnRegexpAdd_Click(object sender, EventArgs e)
        {
            if (txtRegexp.Text != "")
            {
                if (txtRegexp.Text.StartsWith("!tag"))
                {
                    lvRegexps.Items.Add(String.Format("(?<={0}>).*(?=</{0})",
                        txtRegexp.Text.Substring(4, txtRegexp.Text.Length - 4).Trim()));
                }
                else
                {
                    lvRegexps.Items.Add(txtRegexp.Text);
                }
                txtRegexp.Text = "";
                txtRegexp.Focus();
            }
        }

        private void btnRegexpEdit_Click(object sender, EventArgs e)
        {
            if (lvRegexps.SelectedItems.Count > 0 && txtRegexp.Text != "")
            {
                lvRegexps.SelectedItems[0].Text = txtRegexp.Text;
            }
        }

        private void btnRegexpRemove_Click(object sender, EventArgs e)
        {
            if (lvRegexps.SelectedItems.Count > 0)
            {
                lvRegexps.SelectedItems[0].Remove();
            }
        }

        private void lbUploader_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbUploader.SelectedIndex != -1)
            {
                LoadImageUploaders(Program.conf.ImageUploadersList[lbUploader.SelectedIndex]);
                Program.conf.ImageUploaderSelected = lbUploader.SelectedIndex;
                RewriteCustomUploaderRightClickMenu();
            }
        }

        private void LoadImageUploaders(ImageHostingService imageUploader)
        {
            txtArg1.Text = "";
            txtArg2.Text = "";
            lvArguments.Items.Clear();
            foreach (string[] args in imageUploader.Arguments)
            {
                lvArguments.Items.Add(args[0]).SubItems.Add(args[1]);
            }
            txtUploadURL.Text = imageUploader.UploadURL;
            txtFileForm.Text = imageUploader.FileForm;
            txtRegexp.Text = "";
            lvRegexps.Items.Clear();
            foreach (string regexp in imageUploader.RegexpList)
            {
                lvRegexps.Items.Add(regexp);
            }
            txtFullImage.Text = imageUploader.Fullimage;
            txtThumbnail.Text = imageUploader.Thumbnail;
        }

        private void btnUploadersUpdate_Click(object sender, EventArgs e)
        {
            if (lbUploader.SelectedIndex != -1)
            {
                ImageHostingService iUploader = GetUploaderFromFields();
                iUploader.Name = lbUploader.SelectedItem.ToString();
                Program.conf.ImageUploadersList[lbUploader.SelectedIndex] = iUploader;
            }
            RewriteCustomUploaderRightClickMenu();
        }

        private void btnUploadersClear_Click(object sender, EventArgs e)
        {
            LoadImageUploaders(new ImageHostingService());
        }

        private void btUploadersTest_Click(object sender, EventArgs e)
        {
            if (lbUploader.SelectedIndex != -1)
            {
                btnUploadersTest.Enabled = false;
                Program.Worker.StartWorkerScreenshots(MainAppTask.Jobs.CUSTOM_UPLOADER_TEST);
            }
        }

        private void btnUploaderExport_Click(object sender, EventArgs e)
        {
            if (Program.conf.ImageUploadersList != null)
            {
                SaveFileDialog dlg = new SaveFileDialog
                {
                    FileName = string.Format("{0}-{1}-uploaders", Application.ProductName, DateTime.Now.ToString("yyyyMMdd")),
                    Filter = Program.FILTER_IMAGE_HOSTING_SERVICES
                };
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ImageHostingServiceManager ihsm = new ImageHostingServiceManager
                    {
                        ImageHostingServices = Program.conf.ImageUploadersList
                    };
                    ihsm.Save(dlg.FileName);
                }
            }
        }

        private void ImportImageUploaders(string fp)
        {
            ImageHostingServiceManager tmp = ImageHostingServiceManager.Read(fp);
            if (tmp != null)
            {
                lbUploader.Items.Clear();
                Program.conf.ImageUploadersList = new List<ImageHostingService>();
                Program.conf.ImageUploadersList.AddRange(tmp.ImageHostingServices);
                foreach (ImageHostingService iHostingService in Program.conf.ImageUploadersList)
                {
                    lbUploader.Items.Add(iHostingService.Name);
                }
            }
        }

        private void btnUploaderImport_Click(object sender, EventArgs e)
        {
            if (Program.conf.ImageUploadersList == null)
                Program.conf.ImageUploadersList = new List<ImageHostingService>();

            OpenFileDialog dlg = new OpenFileDialog { Filter = Program.FILTER_IMAGE_HOSTING_SERVICES };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ImportImageUploaders(dlg.FileName);
            }
        }

        private void btnOpenSourceText_Click(object sender, EventArgs e)
        {
            OpenLastSource(ImageFileManager.SourceType.TEXT);
        }

        private void btnOpenSourceBrowser_Click(object sender, EventArgs e)
        {
            OpenLastSource(ImageFileManager.SourceType.HTML);
        }

        private void btnOpenSourceString_Click(object sender, EventArgs e)
        {
            OpenLastSource(ImageFileManager.SourceType.STRING);
        }

        private void OpenLastSource(ImageFileManager.SourceType sType)
        {
            OpenSource(ClipboardManager.GetLastImageUpload(), sType);
        }

        private bool OpenSource(ImageFileManager ifm, ImageFileManager.SourceType sType)
        {
            string path = ifm.GetSource(Program.TempDir, sType);
            if (!string.IsNullOrEmpty(path))
            {
                if (sType == ImageFileManager.SourceType.TEXT || sType == ImageFileManager.SourceType.HTML)
                {
                    Process.Start(path);
                    return true;
                }
                if (sType == ImageFileManager.SourceType.STRING)
                {
                    Clipboard.SetText(path);
                    return true;
                }
            }
            return false;
        }

        private void txtUploadersLog_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        #endregion

        private void dgvHotkeys_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignore clicks
            if (e.RowIndex < 0 || e.ColumnIndex != dgvHotkeys.Columns[1].Index)
            {
                return;
            }

            Program.Worker.mSetHotkeys = true;
            Program.Worker.mHKSelectedRow = e.RowIndex;

            lblHotkeyStatus.Text = "Press the keys you would like to use... Press enter when done setting all desired Hotkeys.";
        }

        private void UpdateHotkeysDGV()
        {
            dgvHotkeys.Rows.Clear();

            AddHotkey("Entire Screen");
            AddHotkey("Active Window");
            AddHotkey("Crop Shot");
            AddHotkey("Selected Window");
            AddHotkey("Clipboard Upload");
            AddHotkey("Last Crop Shot");
            AddHotkey("Auto Capture");
            AddHotkey("Actions Toolbar");
            AddHotkey("Quick Options");
            AddHotkey("Drop Window");
            AddHotkey("Language Translator");
            AddHotkey("Screen Color Picker");

            dgvHotkeys.Refresh();
        }

        private void AddHotkey(string name)
        {
            object obj = Program.conf.GetFieldValue("Hotkey" + name.Replace(" ", ""));
            if (obj != null && obj.GetType() == typeof(Keys))
            {
                dgvHotkeys.Rows.Add(name, ((Keys)obj).ToSpecialString());
            }
        }

        private void dgvHotkeys_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //txtActiveHelp.Text = dgvHotkeys.Rows[e.RowIndex].Cells[0].Value + ": allows you to ";

                switch (e.RowIndex)
                {
                    case 0: //active window
                        //txtActiveHelp.Text += "capture a window that is currently highlighted and send it your selected destination.";
                        break;
                    case 1: //selected window
                        //txtActiveHelp.Text += "capture a window by selecting a window from the mouse and send it your selected destination.";
                        break;
                    case 2: //entire screen
                        //txtActiveHelp.Text += "capture everything present on your screen including taskbar, start menu, etc and send it your selected destination";
                        break;
                    case 3: //crop shot
                        //txtActiveHelp.Text += "capture a specified region of your screen and send it to your selected destination";
                        break;
                    case 4: //last crop shot
                        //txtActiveHelp.Text += "capture the specified region from crop shot another time";
                        break;
                    case 5: //clipboard upload
                        //txtActiveHelp.Text += "send files from your file system to your selected destination.";
                        break;
                    case 7: // quick options
                        //txtActiveHelp.Text += "quickly select the destination you would like to send images via a small pop up form.";
                        break;
                    case 8: // drop window
                        //txtActiveHelp.Text += "display a Drop Window so can drag and drop image files from Windows Explorer to upload.";
                        break;
                    case 9: // language translator
                        //txtActiveHelp.Text += "translate the text that is in your clipboard from one language to another. See HTTP -> Language Translator for settings.";
                        break;
                }
            }
        }

        private void dgvHotkeys_Leave(object sender, EventArgs e)
        {
            Program.Worker.QuitSettingHotkeys();
        }

        private void ZScreen_Leave(object sender, EventArgs e)
        {
            Program.Worker.QuitSettingHotkeys();
        }

        private void dgvHotkeys_MouseLeave(object sender, EventArgs e)
        {
            Program.Worker.QuitSettingHotkeys();
        }

        private void btnRegCodeTinyPic_Click(object sender, EventArgs e)
        {
            UserPassBox ub = new UserPassBox("Enter TinyPic Email Address and Password",
                string.IsNullOrEmpty(Program.conf.TinyPicUserName) ? "someone@gmail.com" :
                Program.conf.TinyPicUserName, Program.conf.TinyPicPassword) { Icon = this.Icon };
            ub.ShowDialog();
            if (ub.DialogResult == DialogResult.OK)
            {
                TinyPicUploader tpu = new TinyPicUploader(Program.TINYPIC_ID, Program.TINYPIC_KEY, UploadMode.API);
                txtTinyPicShuk.Text = tpu.UserAuth(ub.UserName, ub.Password);
                if (Program.conf.RememberTinyPicUserPass)
                {
                    Program.conf.TinyPicUserName = ub.UserName;
                    Program.conf.TinyPicPassword = ub.Password;
                }
            }
            this.BringToFront();
        }

        private void txtTinyPicShuk_TextChanged(object sender, EventArgs e)
        {
            Program.conf.TinyPicShuk = txtTinyPicShuk.Text;
        }

        private void CheckFormSettings()
        {
            if (Program.conf.LockFormSize)
            {
                if (this.FormBorderStyle != FormBorderStyle.FixedSingle)
                {
                    this.FormBorderStyle = FormBorderStyle.FixedSingle;
                    this.Size = this.MinimumSize;
                }
            }
            else
            {
                if (this.FormBorderStyle != FormBorderStyle.Sizable)
                {
                    this.FormBorderStyle = FormBorderStyle.Sizable;
                    this.Size = this.MinimumSize;
                }
            }
        }

        private void cbCropStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.CropRegionStyles = (RegionStyles)cbCropStyle.SelectedIndex;
        }

        private void pbCropBorderColor_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Program.conf.CropBorderColor);
        }

        private void nudCropBorderSize_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.CropBorderSize = nudCropBorderSize.Value;
        }

        private void llblBugReports_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Program.URL_ISSUES);
        }

        private void cbCompleteSound_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.CompleteSound = cbCompleteSound.Checked;
        }

        private void cbCheckUpdates_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.CheckUpdates = cbCheckUpdates.Checked;
            cbCheckExperimental.Enabled = Program.conf.CheckUpdates;
        }

        private void txtActiveHelp_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void SetClipboardFromHistory(ClipboardUriType type)
        {
            if (lbHistory.SelectedIndex != -1)
            {
                List<string> screenshots = new List<string>();
                for (int i = 0; i < lbHistory.SelectedItems.Count; i++)
                {
                    HistoryItem hi = (HistoryItem)lbHistory.SelectedItems[i];
                    if (hi.ScreenshotManager != null)
                    {
                        screenshots.Add(hi.ScreenshotManager.GetUrlByType(type));
                    }
                }
                if (screenshots.Count > 0)
                {
                    if (Program.conf.HistoryReverseList)
                    {
                        screenshots.Reverse();
                    }
                    StringBuilder sb = new StringBuilder();
                    if (Program.conf.HistoryAddSpace)
                    {
                        sb.AppendLine();
                    }
                    for (int i = 0; i < screenshots.Count; i++)
                    {
                        sb.Append(screenshots[i]);
                        if (i < lbHistory.SelectedItems.Count - 1)
                        {
                            sb.AppendLine();
                        }
                    }
                    string result = sb.ToString();
                    if (!string.IsNullOrEmpty(result))
                    {
                        Clipboard.SetText(result);
                    }
                }
            }
        }

        private void lbHistory_MouseDown(object sender, MouseEventArgs e)
        {
            cmsHistory.Enabled = lbHistory.Items.Count > 0;

            int i = lbHistory.IndexFromPoint(e.X, e.Y);

            if (i >= 0 && i < lbHistory.Items.Count)
            {
                lbHistory.SelectedIndex = i;
            }

            lbHistory.Refresh();
        }

        private void lbHistory_DoubleClick(object sender, EventArgs e)
        {
            if (lbHistory.SelectedIndex > -1)
            {
                HistoryItem hi = (HistoryItem)lbHistory.SelectedItem;
                if (!string.IsNullOrEmpty(hi.RemotePath)) Process.Start(((HistoryItem)lbHistory.SelectedItem).RemotePath);
            }
        }

        private void lbHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbHistory.SelectedIndex > -1)
            {
                HistoryItem hi = (HistoryItem)lbHistory.SelectedItem;

                if (hi != null)
                {
                    bool checkLocal = !string.IsNullOrEmpty(hi.LocalPath) && File.Exists(hi.LocalPath);
                    bool checkRemote = !string.IsNullOrEmpty(hi.RemotePath);
                    bool checkImage = checkLocal && FileSystem.IsValidImageFile(hi.LocalPath);
                    bool checkText = checkLocal && FileSystem.IsValidText(hi.LocalPath);

                    tsmCopyCbHistory.Enabled = checkRemote;
                    browseURLToolStripMenuItem.Enabled = checkRemote;
                    btnHistoryCopyLink.Enabled = checkRemote;
                    btnHistoryBrowseURL.Enabled = checkRemote;
                    btnHistoryOpenLocalFile.Enabled = checkLocal;
                    btnHistoryCopyImage.Enabled = checkImage;
                    pbPreview.Visible = checkImage;
                    txtPreview.Visible = checkText;

                    if (FileSystem.IsValidImageFile(hi.LocalPath))
                    {
                        if (checkLocal)
                        {
                            pbPreview.ImageLocation = hi.LocalPath;
                        }
                        else if (checkRemote)
                        {
                            pbPreview.ImageLocation = hi.RemotePath;
                        }
                    }
                    else if (FileSystem.IsValidText(hi.LocalPath))
                    {
                        txtPreview.Text = File.ReadAllText(hi.LocalPath);
                    }
                    txtHistoryLocalPath.Text = hi.LocalPath;
                    txtHistoryRemotePath.Text = hi.RemotePath;
                    lblHistoryScreenshot.Text = string.Format("{0} ({1})", hi.JobName, hi.DestinationName);
                }

                if (Program.conf.HistoryShowTooltips && hi != null)
                {
                    ttZScreen.SetToolTip(lbHistory, hi.GetStatistics());
                    ttZScreen.SetToolTip(pbPreview, hi.GetStatistics());
                }

            }
        }

        private void btnScreenshotOpen_Click(object sender, EventArgs e)
        {
            OpenLocalFile();
        }

        private void OpenLocalFile()
        {
            if (lbHistory.SelectedItem != null)
            {
                HistoryItem hi = (HistoryItem)lbHistory.SelectedItem;
                if (!string.IsNullOrEmpty(hi.LocalPath))
                {
                    Process.Start(hi.LocalPath);
                }
            }
        }

        private void OpenRemoteFile()
        {
            if (lbHistory.SelectedItem != null)
            {
                HistoryItem hi = (HistoryItem)lbHistory.SelectedItem;
                if (!string.IsNullOrEmpty(hi.RemotePath))
                {
                    Process.Start(hi.RemotePath);
                }
            }
        }

        private void btnScreenshotBrowse_Click(object sender, EventArgs e)
        {
            OpenRemoteFile();
        }

        private void txtWatermarkText_TextChanged(object sender, EventArgs e)
        {
            Program.conf.WatermarkText = txtWatermarkText.Text;
            TestWatermark();
        }

        private void CheckForCodes(object checkObject)
        {
            TextBox textBox = (TextBox)checkObject;
            if (codesMenu.Items.Count > 0)
            {
                codesMenu.Show(textBox, new Point(textBox.Width + 1, 0));
            }
        }

        private void CreateCodesMenu()
        {
            codesMenu.AutoClose = false;
            codesMenu.Font = new Font("Lucida Console", 8);
            codesMenu.Opacity = 0.8;
            codesMenu.ShowImageMargin = false;
            for (int i = 0; i < NameParser.replacementVars.Length; i++)
            {
                ToolStripMenuItem tsi = new ToolStripMenuItem
                {
                    Text = NameParser.replacementVars[i].PadRight(3, ' ') + " - " + NameParser.replacementDescriptions[i],
                    Tag = NameParser.replacementVars[i]
                };
                tsi.Click += watermarkCodeMenu_Click;
                codesMenu.Items.Add(tsi);
            }
            CodesMenuCloseEvents();
        }

        private void watermarkCodeMenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsi = (ToolStripMenuItem)sender;
            int oldPos = txtWatermarkText.SelectionStart;
            string appendText;
            if (oldPos > 0 && txtWatermarkText.Text[txtWatermarkText.SelectionStart - 1] == NameParser.prefix[0])
            {
                appendText = tsi.Tag.ToString().TrimStart('%');
                txtWatermarkText.Text =
                    txtWatermarkText.Text.Insert(txtWatermarkText.SelectionStart, appendText);
                txtWatermarkText.Select(oldPos + appendText.Length, 0);
            }
            else
            {
                appendText = tsi.Tag.ToString();
                txtWatermarkText.Text =
                    txtWatermarkText.Text.Insert(txtWatermarkText.SelectionStart, appendText);
                txtWatermarkText.Select(oldPos + appendText.Length, 0);
            }
        }

        private void cbShowCursor_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.ShowCursor = cbShowCursor.Checked;
        }

        private void btnWatermarkFont_Click(object sender, EventArgs e)
        {
            try
            {
                FontDialog fDialog = new FontDialog
                {
                    ShowColor = true,
                    Font = XMLSettings.DeserializeFont(Program.conf.WatermarkFont),
                    Color = XMLSettings.DeserializeColor(Program.conf.WatermarkFontColor)
                };
                if (fDialog.ShowDialog() == DialogResult.OK)
                {
                    Program.conf.WatermarkFont = XMLSettings.SerializeFont(fDialog.Font);
                    Program.conf.WatermarkFontColor = XMLSettings.SerializeColor(fDialog.Color);
                    pbWatermarkFontColor.BackColor = XMLSettings.DeserializeColor(Program.conf.WatermarkFontColor);
                    lblWatermarkFont.Text = FontToString();
                }
                TestWatermark();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private string FontToString()
        {
            return FontToString(XMLSettings.DeserializeFont(Program.conf.WatermarkFont),
                 XMLSettings.DeserializeColor(Program.conf.WatermarkFontColor));
        }

        private string FontToString(Font font, Color color)
        {
            return "Name: " + font.Name + " - Size: " + font.Size + " - Style: " + font.Style + " - Color: " +
                color.R + "," + color.G + "," + color.B;
        }

        private void nudWatermarkOffset_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.WatermarkOffset = nudWatermarkOffset.Value;
            TestWatermark();
        }

        private void nudWatermarkBackTrans_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.WatermarkBackTrans = nudWatermarkBackTrans.Value;
            trackWatermarkBackgroundTrans.Value = (int)nudWatermarkBackTrans.Value;
        }

        private void entireScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.Sleep(300);
            Program.Worker.StartBW_EntireScreen();
        }

        private void selectedWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.Sleep(300);
            Program.Worker.StartBW_SelectedWindow();
        }

        private void rectangularRegionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.Sleep(300);
            Program.Worker.StartBW_CropShot();
        }

        private void lastRectangularRegionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.Sleep(300);
            Program.Worker.StartBW_LastCropShot();
        }

        private void tsmDropWindow_Click(object sender, EventArgs e)
        {
            Program.Worker.ShowDropWindow();
        }

        private void tsmUploadFromClipboard_Click(object sender, EventArgs e)
        {
            Program.Worker.UploadUsingClipboard();
        }

        private void languageTranslatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.Worker.StartWorkerTranslator();
        }

        private void screenColorPickerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.Worker.ScreenColorPicker();
        }

        private void pbWatermarkGradient1_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Program.conf.WatermarkGradient1);
            TestWatermark();
        }

        private void pbWatermarkGradient2_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Program.conf.WatermarkGradient2);
            TestWatermark();
        }

        private void pbWatermarkBorderColor_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Program.conf.WatermarkBorderColor);
            TestWatermark();
        }

        private void TestWatermark()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(ZScreen));
            using (Bitmap bmp = new Bitmap((Image)(resources.GetObject("pbLogo.Image"))).
                Clone(new Rectangle(62, 33, 199, 140), PixelFormat.Format32bppArgb))
            {
                Bitmap bmp2 = new Bitmap(pbWatermarkShow.ClientRectangle.Width, pbWatermarkShow.ClientRectangle.Height);
                Graphics g = Graphics.FromImage(bmp2);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new Rectangle(0, 0, pbWatermarkShow.ClientRectangle.Width, pbWatermarkShow.ClientRectangle.Height));
                pbWatermarkShow.Image = WatermarkMaker.GetImage(bmp2);
            }
        }

        private void txtWatermarkText_Leave(object sender, EventArgs e)
        {
            if (codesMenu.Visible)
            {
                codesMenu.Close();
            }
        }

        private void pbWatermarkFontColor_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Program.conf.WatermarkFontColor);
            lblWatermarkFont.Text = FontToString();
            TestWatermark();
        }

        private void SelectColor(Control pb, ref string setting)
        {
            DialogColor dColor = new DialogColor(pb.BackColor);
            if (dColor.ShowDialog() == DialogResult.OK)
            {
                pb.BackColor = dColor.Color;
                setting = XMLSettings.SerializeColor(dColor.Color);
            }
        }

        private void cbWatermarkPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.WatermarkPositionMode = (WatermarkPositionType)cbWatermarkPosition.SelectedIndex;
            TestWatermark();
        }

        private void nudWatermarkFontTrans_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.WatermarkFontTrans = nudWatermarkFontTrans.Value;
            trackWatermarkFontTrans.Value = (int)nudWatermarkFontTrans.Value;
        }

        private void nudWatermarkCornerRadius_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.WatermarkCornerRadius = nudWatermarkCornerRadius.Value;
            TestWatermark();
        }

        private void cbWatermarkGradientType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.WatermarkGradientType = (LinearGradientMode)cbWatermarkGradientType.SelectedIndex;
            TestWatermark();
        }

        private void CopyImageFromHistory()
        {
            if (lbHistory.SelectedIndex != -1)
            {
                HistoryItem hi = (HistoryItem)lbHistory.SelectedItem;
                if (!string.IsNullOrEmpty(hi.LocalPath))
                {
                    using (Image img = GraphicsMgr.GetImageSafely(hi.LocalPath))
                    {
                        if (img != null)
                        {
                            Clipboard.SetImage(img);
                        }
                    }
                }
            }
        }

        private void CopyLinkFromHistory()
        {
            if (lbHistory.SelectedIndex != -1)
            {
                HistoryItem hi = (HistoryItem)lbHistory.SelectedItem;
                if (!string.IsNullOrEmpty(hi.RemotePath))
                {
                    Clipboard.SetText(hi.RemotePath);
                }
            }
        }

        private void copyImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyImageFromHistory();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lbHistory.SelectedIndex != -1)
            {
                List<HistoryItem> temp = new List<HistoryItem>();
                foreach (HistoryItem hi in lbHistory.SelectedItems)
                {
                    temp.Add(hi);
                }
                foreach (HistoryItem hi in temp)
                {
                    lbHistory.Items.Remove(hi);
                    if (File.Exists(hi.LocalPath))
                    {
                        Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(hi.LocalPath,
                            Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
                    }
                }
            }
        }

        private void btnViewLocalDirectory_Click(object sender, EventArgs e)
        {
            ShowDirectory(Program.ImagesDir);
        }

        private void btnViewRemoteDirectory_Click(object sender, EventArgs e)
        {
            ShowDirectory(Program.CacheDir);
        }

        private void cbOpenMainWindow_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.OpenMainWindow = cbOpenMainWindow.Checked;
        }

        private void cbShowTaskbar_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.ShowInTaskbar = cbShowTaskbar.Checked;
            if (mGuiIsReady)
            {
                this.ShowInTaskbar = Program.conf.ShowInTaskbar;
            }
        }

        private void llWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Program.URL_WEBSITE);
        }

        private void llProjectPage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Program.URL_PROJECTPAGE);
        }

        private void ZScreen_Deactivate(object sender, EventArgs e)
        {
            codesMenu.Close();
        }

        private void txtWatermarkText_MouseDown(object sender, MouseEventArgs e)
        {
            CheckForCodes(sender);
        }

        private void CodesMenuCloseEvents()
        {
            tpWatermark.MouseClick += new MouseEventHandler(CodesMenuCloseEvent);
            foreach (Control cntrl in tpWatermark.Controls)
            {
                if (cntrl.GetType() == typeof(GroupBox))
                {
                    cntrl.MouseClick += new MouseEventHandler(CodesMenuCloseEvent);
                }
            }
        }

        private void CodesMenuCloseEvent(object sender, MouseEventArgs e)
        {
            codesMenu.Close();
        }

        private void openLocalFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenLocalFile();
        }

        private void browseURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenRemoteFile();
        }

        private void chkBalloonTipOpenLink_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.BalloonTipOpenLink = chkBalloonTipOpenLink.Checked;
        }

        private void cmVersionHistory_Click(object sender, EventArgs e)
        {
            FormsMgr.ShowVersionHistory();
        }

        #region Language Translator



        private void btnTranslate_Click(object sender, EventArgs e)
        {
            btnTranslateMethod();
        }

        private void btnTranslateMethod()
        {
            Program.Worker.StartBW_LanguageTranslator(new GoogleTranslate.TranslationInfo(txtTranslateText.Text,
                GoogleTranslate.FindLanguage(Program.conf.FromLanguage, Program.mGTranslator.LanguageOptions.SourceLangList),
                GoogleTranslate.FindLanguage(Program.conf.ToLanguage, Program.mGTranslator.LanguageOptions.TargetLangList)));
        }


        private void cbFromLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.FromLanguage = Program.mGTranslator.LanguageOptions.SourceLangList[cbFromLanguage.SelectedIndex].Value;
        }

        private void cbToLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.ToLanguage = Program.mGTranslator.LanguageOptions.TargetLangList[cbToLanguage.SelectedIndex].Value;
        }

        private void cbClipboardTranslate_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.ClipboardTranslate = cbClipboardTranslate.Checked;
        }

        private void txtTranslateText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnTranslateMethod();
            }
        }

        #endregion

        private void cboUploadMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.UploadMode = (UploadMode)cboUploadMode.SelectedIndex;
            gbImageShack.Enabled = Program.conf.UploadMode == UploadMode.API;
            gbTinyPic.Enabled = Program.conf.UploadMode == UploadMode.API;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HistoryItem hi = (HistoryItem)lbHistory.SelectedItem;
            OpenSource(hi.ScreenshotManager, ImageFileManager.SourceType.TEXT);
        }

        private void openSourceInDefaultWebBrowserHTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HistoryItem hi = (HistoryItem)lbHistory.SelectedItem;
            OpenSource(hi.ScreenshotManager, ImageFileManager.SourceType.HTML);
        }

        private void copySourceToClipboardStringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HistoryItem hi = (HistoryItem)lbHistory.SelectedItem;
            OpenSource(hi.ScreenshotManager, ImageFileManager.SourceType.STRING);
        }

        private void cmsRetryUpload_Click(object sender, EventArgs e)
        {
            Program.Worker.HistoryRetryUpload((HistoryItem)lbHistory.SelectedItem);
        }

        private void pbHistoryThumb_Click(object sender, EventArgs e)
        {
            HistoryItem hi = (HistoryItem)lbHistory.SelectedItem;
            if (hi != null && File.Exists(hi.LocalPath))
            {
                if (hi.ScreenshotManager != null)
                {
                    if (FileSystem.IsValidImageFile(hi.LocalPath))
                    {
                        ShowScreenshot sc = new ShowScreenshot();
                        if (hi.ScreenshotManager.GetImage() != null)
                        {
                            sc.BackgroundImage = Image.FromFile(hi.LocalPath);
                            sc.ShowDialog();
                        }
                    }
                }
                else
                {
                    Process.Start(hi.LocalPath);
                }
            }
        }

        private void btnCopyStats_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lblDebugInfo.Text);
        }

        private void cbImageUploadRetry_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.ImageUploadRetry = chkImageUploadRetry.Checked;
        }

        #region FTP

        private void FTPSetup(IEnumerable<FTPAccount> accs)
        {
            if (accs != null)
            {
                ucFTPAccounts.AccountsList.Items.Clear();
                Program.conf.FTPAccountList = new List<FTPAccount>();
                Program.conf.FTPAccountList.AddRange(accs);
                foreach (FTPAccount acc in Program.conf.FTPAccountList)
                {
                    ucFTPAccounts.AccountsList.Items.Add(acc);
                }
            }
        }

        private void FTPAccountsRemoveButton_Click(object sender, EventArgs e)
        {
            int sel = ucFTPAccounts.AccountsList.SelectedIndex;

            if (sel != -1)
            {
                Program.conf.FTPAccountList.RemoveAt(sel);

                ucFTPAccounts.AccountsList.Items.RemoveAt(sel);

                if (ucFTPAccounts.AccountsList.Items.Count > 0)
                {
                    ucFTPAccounts.AccountsList.SelectedIndex = (sel > 0) ? (sel - 1) : 0;
                }
            }
        }

        private void FTPAccountsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sel = ucFTPAccounts.AccountsList.SelectedIndex;
            Program.conf.FTPSelected = sel;
            if (Program.conf.FTPAccountList != null && sel != -1 && sel < Program.conf.FTPAccountList.Count && Program.conf.FTPAccountList[sel] != null)
            {
                FTPAccount acc = Program.conf.FTPAccountList[sel];
                ucFTPAccounts.SettingsGrid.SelectedObject = acc;
                RewriteFTPRightClickMenu();
            }
        }

        private void FTPAccountAddButton_Click(object sender, EventArgs e)
        {
            FTPAccount acc = new FTPAccount("New Account");
            Program.conf.FTPAccountList.Add(acc);
            ucFTPAccounts.AccountsList.Items.Add(acc);
            ucFTPAccounts.AccountsList.SelectedIndex = ucFTPAccounts.AccountsList.Items.Count - 1;
        }

        private void btnExportAccounts_Click(object sender, EventArgs e)
        {
            if (Program.conf.FTPAccountList != null)
            {
                SaveFileDialog dlg = new SaveFileDialog
                {
                    FileName = string.Format("{0}-{1}-accounts", Application.ProductName, DateTime.Now.ToString("yyyyMMdd")),
                    Filter = Program.FILTER_ACCOUNTS
                };
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    FTPAccountManager fam = new FTPAccountManager(Program.conf.FTPAccountList);
                    fam.Save(dlg.FileName);
                }
            }
        }

        private void btnAccsImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog { Filter = Program.FILTER_ACCOUNTS };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                FTPAccountManager fam = FTPAccountManager.Read(dlg.FileName);
                FTPSetup(fam.FTPAccounts);
            }
        }

        private FTPAccount GetSelectedFTP()
        {
            FTPAccount acc = new FTPAccount("New Account");
            if (ucFTPAccounts.AccountsList.SelectedIndex != -1 && Program.conf.FTPAccountList.Count >= ucFTPAccounts.AccountsList.Items.Count)
            {
                acc = Program.conf.FTPAccountList[ucFTPAccounts.AccountsList.SelectedIndex];
            }
            return acc;
        }

        private void FTPAccountsTestButton_Click(object sender, EventArgs e)
        {
            string msg = "";

            FTPAccount acc = GetSelectedFTP();

            try
            {
                FTP ftp = new FTP(ref acc);
                if (ftp.ListDirectory() != null)
                {
                    msg = "Success"; //Success
                }
                else
                {
                    msg = "FTP Settings are not set correctly. Make sure your FTP Path exists.";
                }
            }
            catch (Exception t)
            {
                msg = t.Message;
            }

            if (!string.IsNullOrEmpty(msg))
            {
                MessageBox.Show(msg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void chkEnableThumbnail_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.FTPCreateThumbnail = chkEnableThumbnail.Checked;
        }

        private void cbAutoSwitchFTP_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.AutoSwitchFTP = cbAutoSwitchFTP.Checked;
        }

        #endregion

        private void cbSelectedWindowFront_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.SelectedWindowFront = cbSelectedWindowFront.Checked;
        }

        private void cbSelectedWindowRectangleInfo_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.SelectedWindowRectangleInfo = cbSelectedWindowRectangleInfo.Checked;
        }

        private void pbSelectedWindowBorderColor_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Program.conf.SelectedWindowBorderColor);
        }

        private void nudSelectedWindowBorderSize_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.SelectedWindowBorderSize = nudSelectedWindowBorderSize.Value;
        }

        private void cbCheckExperimental_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.CheckExperimental = cbCheckExperimental.Checked;
        }

        private void btnCheckUpdate_Click(object sender, EventArgs e)
        {
            Program.Worker2.CheckUpdates();
        }

        private void cbAddFailedScreenshot_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.AddFailedScreenshot = cbAddFailedScreenshot.Checked;
        }

        private void cbShowUploadDuration_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.ShowUploadDuration = cbShowUploadDuration.Checked;
        }

        /// <summary>
        /// Searches for an Image Software in settings and returns it
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static Software GetImageSoftware(string name)
        {
            foreach (Software app in Program.conf.ImageEditors)
            {
                if (app != null && app.Name != null)
                {
                    if (app.Name.Equals(name))
                        return app;
                }

            }
            return null;
        }

        private void cbSelectedWindowStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.SelectedWindowRegionStyles = (RegionStyles)cbSelectedWindowStyle.SelectedIndex;
        }

        private void nudCropGridSize_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.CropGridSize.Width = (int)nudCropGridWidth.Value;
        }

        private void nudCropGridHeight_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.CropGridSize.Height = (int)nudCropGridHeight.Value;
        }

        private void cbCropShowGrids_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.CropShowGrids = cbCropShowGrids.Checked;
        }

        private void cboUpdateCheckType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.UpdateCheckType = (UpdateCheckType)cboUpdateCheckType.SelectedIndex;
        }

        private void cbAddSpace_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.HistoryAddSpace = cbHistoryAddSpace.Checked;
        }

        private void cbReverse_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.HistoryReverseList = cbHistoryReverseList.Checked;
        }

        private void nudHistoryMaxItems_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.HistoryMaxNumber = (int)nudHistoryMaxItems.Value;
            if (mGuiIsReady)
            {
                Program.Worker.CheckHistoryItems();
                Program.Worker.SaveHistoryItems();
            }
        }

        private void cbCloseDropBox_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.CloseDropBox = cbCloseDropBox.Checked;
        }

        private void btnHistoryClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to clear the History List?", this.Text, MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                lbHistory.Items.Clear();
                Program.Worker.CheckHistoryItems();
                Program.Worker.SaveHistoryItems();
            }
        }

        private void tsmQuickActions_Click(object sender, EventArgs e)
        {
            Program.Worker.ShowActionsToolbar(true);
        }



        private void chkRememberTinyPicUserPass_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.RememberTinyPicUserPass = chkRememberTinyPicUserPass.Checked;
        }

        private void btnResetIncrement_Click(object sender, EventArgs e)
        {
            Program.conf.AutoIncrement = 0;
        }

        private void btnImageCopy_Click(object sender, EventArgs e)
        {
            CopyImageFromHistory();
        }

        private void lbHistory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                for (int i = lbHistory.Items.Count - 1; i >= 0; i--)
                {
                    lbHistory.SetSelected(i, true);
                }
            }
        }

        private void btnCopyLink_Click(object sender, EventArgs e)
        {
            CopyLinkFromHistory();
        }

        private void cbHistoryListFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.HistoryListFormat = (HistoryListFormat)cbHistoryListFormat.SelectedIndex;
            LoadHistoryItems();
        }

        private void cbShowHistoryTooltip_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.HistoryShowTooltips = cbShowHistoryTooltip.Checked;
        }

        private void cbHistorySave_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.HistorySave = cbHistorySave.Checked;
        }

        private void pbCropCrosshairColor_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Program.conf.CropCrosshairColor);
        }

        private void chkCaptureFallback_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.CaptureEntireScreenOnError = chkCaptureFallback.Checked;
        }

        private void nudSwitchAfter_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.SwitchAfter = nudSwitchAfter.Value;
        }

        private void cbCropDynamicCrosshair_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.CropDynamicCrosshair = cbCropDynamicCrosshair.Checked;
        }

        private void nudCrosshairLineCount_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.CrosshairLineCount = (int)nudCrosshairLineCount.Value;
        }

        private void nudCrosshairLineSize_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.CrosshairLineSize = (int)nudCrosshairLineSize.Value;
        }

        private void nudCropInterval_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.CropInterval = (int)nudCropCrosshairInterval.Value;
        }

        private void nudCropStep_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.CropStep = (int)nudCropCrosshairStep.Value;
        }

        private void cbCropShowBigCross_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.CropShowBigCross = cbCropShowBigCross.Checked;
        }

        private void cbShowCropRuler_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.CropShowRuler = cbShowCropRuler.Checked;
        }

        private void cbSelectedWindowRuler_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.SelectedWindowRuler = cbSelectedWindowRuler.Checked;
        }

        private void cbCropDynamicBorderColor_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.CropDynamicBorderColor = cbCropDynamicBorderColor.Checked;
        }

        private void nudCropRegionInterval_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.CropRegionInterval = nudCropRegionInterval.Value;
        }

        private void nudCropRegionStep_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.CropRegionStep = nudCropRegionStep.Value;
        }

        private void nudCropHueRange_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.CropHueRange = nudCropHueRange.Value;
        }

        private void cbSelectedWindowDynamicBorderColor_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.SelectedWindowDynamicBorderColor = cbSelectedWindowDynamicBorderColor.Checked;
        }

        private void nudSelectedWindowRegionInterval_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.SelectedWindowRegionInterval = nudSelectedWindowRegionInterval.Value;
        }

        private void nudSelectedWindowRegionStep_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.SelectedWindowRegionStep = nudSelectedWindowRegionStep.Value;
        }

        private void nudSelectedWindowHueRange_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.SelectedWindowHueRange = nudSelectedWindowHueRange.Value;
        }

        private void cbCropGridMode_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.CropGridToggle = cboCropGridMode.Checked;
        }

        private void cbTinyPicSizeCheck_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.TinyPicSizeCheck = cbTinyPicSizeCheck.Checked;
        }

        private void txtWatermarkImageLocation_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(txtWatermarkImageLocation.Text))
            {
                Program.conf.WatermarkImageLocation = txtWatermarkImageLocation.Text;
                TestWatermark();
            }
        }

        private void btwWatermarkBrowseImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };
            if (fd.ShowDialog() == DialogResult.OK)
            {
                txtWatermarkImageLocation.Text = fd.FileName;
            }
        }

        private void cbAutoChangeUploadDestination_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.AutoChangeUploadDestination = cboAutoChangeUploadDestination.Checked;
        }

        private void nudUploadDurationLimit_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.UploadDurationLimit = nudUploadDurationLimit.Value;
        }

        private void StartDebug()
        {
            debug = new Debug();
            debug.GetDebugInfo += new StringEventHandler(debug_GetDebugInfo);
        }

        private void debug_GetDebugInfo(object sender, string e)
        {
            if (this.Visible)
            {
                lblDebugInfo.Text = e;
            }
        }

        private void btnDebugStart_Click(object sender, EventArgs e)
        {
            if (debug.DebugTimer.Enabled)
            {
                btnDebugStart.Text = "Start";
            }
            else
            {
                btnDebugStart.Text = "Pause";
            }
            debug.DebugTimer.Enabled = !debug.DebugTimer.Enabled;
        }

        private void tsmMain_Click(object sender, EventArgs e)
        {
            BringUpMenu();
        }

        private void btnGalleryTinyPic_Click(object sender, EventArgs e)
        {
            Process.Start("http://tinypic.com/yourstuff.php");
        }

        private void cbWatermarkUseBorder_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.WatermarkUseBorder = cbWatermarkUseBorder.Checked;
            TestWatermark();
        }

        private void cbWatermarkAddReflection_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.WatermarkAddReflection = cbWatermarkAddReflection.Checked;
            TestWatermark();
        }

        private void btnBrowseRootDir_Click(object sender, EventArgs e)
        {
            string oldRootDir = txtRootFolder.Text;
            FolderBrowserDialog dlg = new FolderBrowserDialog { ShowNewFolderButton = true };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Program.SetRootFolder(dlg.SelectedPath);
                txtRootFolder.Text = Settings.Default.RootDir;
            }
            FileSystem.MoveDirectory(oldRootDir, txtRootFolder.Text);
            UpdateGuiControlsPaths();
            Program.conf = XMLSettings.Read();
            SetupScreen();
        }

        private void btnViewRootDir_Click(object sender, EventArgs e)
        {
            ShowDirectory(txtRootFolder.Text);
        }

        private void nudWatermarkImageScale_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.WatermarkImageScale = nudWatermarkImageScale.Value;
            TestWatermark();
        }

        private void trackWatermarkFontTrans_Scroll(object sender, EventArgs e)
        {
            Program.conf.WatermarkFontTrans = trackWatermarkFontTrans.Value;
            nudWatermarkFontTrans.Value = Program.conf.WatermarkFontTrans;
            TestWatermark();
        }

        private void trackWatermarkBackgroundTrans_Scroll(object sender, EventArgs e)
        {
            Program.conf.WatermarkBackTrans = trackWatermarkBackgroundTrans.Value;
            nudWatermarkBackTrans.Value = Program.conf.WatermarkBackTrans;
            TestWatermark();
        }

        private void cbWatermarkAutoHide_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.WatermarkAutoHide = cbWatermarkAutoHide.Checked;
            TestWatermark();
        }

        private void cboWatermarkType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.WatermarkMode = (WatermarkType)cboWatermarkType.SelectedIndex;
            TestWatermark();
        }

        private void cbSelectedWindowAddBorder_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.SelectedWindowAddBorder = cbSelectedWindowAddBorder.Checked;
        }

        private void cbCropShowMagnifyingGlass_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.CropShowMagnifyingGlass = cbCropShowMagnifyingGlass.Checked;
        }

        private void pbLogo_MouseEnter(object sender, EventArgs e)
        {

            Bitmap bmp = new Bitmap(Properties.Resources.main);
            Random rand = new Random();

            if (mLogoRandomList.Count == 0)
            {
                List<int> numbers = new List<int>() { 1, 2, 3, 4 };

                int count = numbers.Count;

                for (int x = 0; x < count; x++)
                {
                    int r = rand.Next(0, numbers.Count - 1);
                    mLogoRandomList.Add(numbers[r]);
                    numbers.RemoveAt(r);
                }
            }

            switch (mLogoRandomList[0])
            {
                case 1:
                    pbLogo.Image = ColorMatrices.ApplyColorMatrix(bmp, ColorMatrices.InverseFilter());
                    break;
                case 2:
                    pbLogo.Image = ColorMatrices.ApplyColorMatrix(bmp, ColorMatrices.GrayscaleFilter());
                    break;
                case 3:
                    pbLogo.Image = ColorMatrices.ApplyColorMatrix(bmp, ColorMatrices.GrayscaleFilter());
                    pbLogo.Image = ColorMatrices.ApplyColorMatrix(bmp, ColorMatrices.InverseFilter());
                    break;
                case 4:
                    pbLogo.Image = ColorMatrices.ApplyColorMatrix(bmp, ColorMatrices.InverseFilter());
                    pbLogo.Image = ColorMatrices.ApplyColorMatrix(bmp, ColorMatrices.SaturationFilter(rand.Next(0, 501) - 250));
                    break;
            }

            mLogoRandomList.RemoveAt(0);
        }

        private void pbLogo_MouseLeave(object sender, EventArgs e)
        {
            pbLogo.Image = new Bitmap((Image)new ComponentResourceManager(typeof(ZScreen)).GetObject(("pbLogo.Image")));
        }

        private void autoScreenshotsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.Worker.ShowAutoCapture();
        }

        private void numericUpDownTimer1_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.ScreenshotDelayTime = nudtScreenshotDelay.Value;
        }

        private void nudtScreenshotDelay_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.ScreenshotDelayTimes = nudtScreenshotDelay.Time;
        }

        private void lblToLanguage_MouseDown(object sender, MouseEventArgs e)
        {
            if (cbToLanguage.SelectedIndex > -1)
            {
                cbToLanguage.DoDragDrop(Program.conf.ToLanguage, DragDropEffects.Move);
            }
        }

        private void btnTranslateTo1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text) && e.AllowedEffect == DragDropEffects.Move)
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void btnTranslateTo1_DragDrop(object sender, DragEventArgs e)
        {
            GoogleTranslate.GTLanguage lang = GoogleTranslate.FindLanguage(e.Data.GetData(DataFormats.Text).ToString(),
               Program.mGTranslator.LanguageOptions.TargetLangList);
            Program.conf.ToLanguage2 = lang.Value;
            btnTranslateTo1.Text = "To " + lang.Name;
        }

        private void btnTranslateTo1_Click(object sender, EventArgs e)
        {
            Program.Worker.TranslateTo1();

        }

        private void cbLockFormSize_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.LockFormSize = cbLockFormSize.Checked;
            CheckFormSettings();
        }

        /// <summary>
        /// Method to periodically (every 6 hours) perform online tasks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrApp_Tick(object sender, EventArgs e)
        {
            Program.Worker2.PerformOnlineTasks();
        }

        private void confApp_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            this.UpdateGuiControls();
        }

        private void TextUploadersAddButton_Click(object sender, EventArgs e)
        {
            if (ucTextUploaders.Templates.SelectedIndex > -1)
            {
                string name = ucTextUploaders.Templates.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(name))
                {
                    TextUploader textUploader = Adapter.FindTextUploader(name);
                    if (textUploader != null)
                    {
                        Program.conf.TextUploadersList.Add(textUploader);
                        ucTextUploaders.MyCollection.Items.Add(textUploader);
                        cboTextDest.Items.Add(textUploader);
                    }
                    ucTextUploaders.MyCollection.SelectedIndex = ucTextUploaders.MyCollection.Items.Count - 1;
                }
            }
        }

        private void TextUploadersRemoveButton_Click(object sender, EventArgs e)
        {
            if (ucTextUploaders.MyCollection.Items.Count > 0)
            {
                int index = ucTextUploaders.MyCollection.SelectedIndex;
                Program.conf.TextUploadersList.RemoveAt(index);
                ucTextUploaders.MyCollection.Items.RemoveAt(index);
                cboTextDest.Items.RemoveAt(index);
                ucTextUploaders.MyCollection.SelectedIndex = ucTextUploaders.MyCollection.Items.Count - 1;
            }
        }

        private void TestUploaderText(TextUploader uploader)
        {
            if (uploader != null)
            {
                string name = uploader.ToString();
                string testString = uploader.TesterString;

                if (!string.IsNullOrEmpty(name))
                {
                    MainAppTask task = Program.Worker.GetWorkerText(MainAppTask.Jobs.UPLOAD_FROM_CLIPBOARD);
                    task.MyText = testString;
                    task.MakeTinyURL = false; // preventing Error: TinyURL redirects to a TinyURL.
                    task.MyTextUploader = uploader;
                    task.RunWorker();
                }
            }
            else
            {
                MessageBox.Show("Select a Text Uploader.");
            }
        }

        private void TextUploaderTestButton_Click(object sender, EventArgs e)
        {
            TextUploader uploader = (TextUploader)ucTextUploaders.MyCollection.SelectedItem;
            TestUploaderText(uploader);
        }

        private void pgFTPSettings_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            ucFTPAccounts.AccountsList.Items[ucFTPAccounts.AccountsList.SelectedIndex] = Program.conf.FTPAccountList[ucFTPAccounts.AccountsList.SelectedIndex];
            RewriteFTPRightClickMenu();
        }

        private void pgEditorsImage_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            Software temp = Program.conf.ImageEditors[lbImageSoftware.SelectedIndex];
            lbImageSoftware.Items[lbImageSoftware.SelectedIndex] = temp;
            Program.conf.ImageEditors[lbImageSoftware.SelectedIndex] = temp;
            CheckCorrectIsRightClickMenu(temp.Name);
            RewriteImageEditorsRightClickMenu();
        }

        private void TextUploaders_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ucTextUploaders.MyCollection.SelectedItems.Count > 0)
            {
                TextUploader textUploader = (TextUploader)ucTextUploaders.MyCollection.SelectedItem;

                Program.conf.TextUploaderActive = textUploader;

                if (mGuiIsReady)
                {
                    Program.conf.SelectedTextUploader = ucTextUploaders.MyCollection.SelectedIndex;
                    cboTextDest.SelectedIndex = ucTextUploaders.MyCollection.SelectedIndex;
                }

                bool hasOptions = textUploader != null;
                ucTextUploaders.SettingsGrid.Visible = hasOptions;

                if (hasOptions)
                {
                    ucTextUploaders.SettingsGrid.SelectedObject = textUploader.Settings;
                }
            }

        }

        private void cboTextDest_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mGuiIsReady)
            {
                ucTextUploaders.MyCollection.SelectedIndex = cboTextDest.SelectedIndex;
                Program.conf.SelectedTextUploader = cboTextDest.SelectedIndex;
            }
        }

        private void cbAutoTranslate_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.AutoTranslate = cbAutoTranslate.Checked;
        }

        private void txtAutoTranslate_TextChanged(object sender, EventArgs e)
        {
            int number;
            if (int.TryParse(txtAutoTranslate.Text, out number))
            {
                Program.conf.AutoTranslateLength = number;
            }
        }

        private void UrlShortenerTestButton_Click(object sender, EventArgs e)
        {
            this.TestUploaderText((TextUploader)ucUrlShorteners.MyCollection.SelectedItem);
        }

        private void UrlShorteners_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ucUrlShorteners.MyCollection.SelectedItems.Count > 0)
            {

                Program.conf.UrlShortenerActive = (TextUploader)ucUrlShorteners.MyCollection.SelectedItem;
                Program.conf.SelectedUrlShortener = ucUrlShorteners.MyCollection.SelectedIndex;

                TextUploader textUploader = (TextUploader)ucUrlShorteners.MyCollection.SelectedItem;
                bool hasOptions = textUploader != null;
                ucUrlShorteners.SettingsGrid.Visible = hasOptions;

                if (hasOptions)
                {
                    ucUrlShorteners.SettingsGrid.SelectedObject = ((TextUploader)textUploader).Settings;
                }

            }
        }

        /// <summary>
        /// Method to remove a Link Shorteners from the List of Link Shorteners
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UrlShortenersRemoveButton_Click(object sender, EventArgs e)
        {
            if (ucUrlShorteners.MyCollection.Items.Count > 0)
            {
                int index = ucUrlShorteners.MyCollection.SelectedIndex;
                ucUrlShorteners.MyCollection.Items.RemoveAt(index);
                Program.conf.UrlShortenersList.RemoveAt(index);
                ucUrlShorteners.MyCollection.SelectedIndex = ucUrlShorteners.MyCollection.Items.Count - 1;
            }
        }

        /// <summary>
        /// Method to add a Link Shorteners to the List of Link Shorteners
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UrlShortenersAddButton_Click(object sender, EventArgs e)
        {
            if (ucUrlShorteners.Templates.SelectedIndex > -1)
            {
                string name = (string)ucUrlShorteners.Templates.SelectedItem;
                if (!string.IsNullOrEmpty(name))
                {
                    TextUploader textUploader = Adapter.FindUrlShortener(name);
                    if (textUploader != null)
                    {
                        ucUrlShorteners.MyCollection.Items.Add(textUploader);
                        Program.conf.UrlShortenersList.Add(textUploader);
                    }
                    ucUrlShorteners.MyCollection.SelectedIndex = ucUrlShorteners.MyCollection.Items.Count - 1;
                }
            }
        }

        private void cbShowHelpBalloonTips_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.ShowHelpBalloonTips = cbShowHelpBalloonTips.Checked;
        }

        private void chkImageEditorAutoSave_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.ImageEditorAutoSave = chkImageEditorAutoSave.Checked;
        }

        private void btnImageShackProfile_Click(object sender, EventArgs e)
        {
            Process.Start("http://profile.imageshack.us/user/" + txtUserNameImageShack.Text);
        }

        private void chkPublicImageShack_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.ImageShackShowImagesInPublic = chkPublicImageShack.Checked;
        }

        private void txtUserNameImageShack_TextChanged(object sender, EventArgs e)
        {
            Program.conf.ImageShackUserName = txtUserNameImageShack.Text;
        }

        private void llblHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Program.URL_HELP);
        }

        private void ucTextUploaders_Load(object sender, EventArgs e)
        {
            TextUploaders_SelectedIndexChanged(sender, e);
        }

        private void ucUrlShorteners_Load(object sender, EventArgs e)
        {
            UrlShorteners_SelectedIndexChanged(sender, e);
        }

        private void txtTwitPicUserName_TextChanged(object sender, EventArgs e)
        {
            if (mGuiIsReady)
            {
                Program.conf.TwitPicUserName = txtTwitPicUserName.Text;
            }
        }

        private void txtTwitPicPassword_TextChanged(object sender, EventArgs e)
        {
            if (mGuiIsReady)
            {
                Program.conf.TwitPicPassword = txtTwitPicPassword.Text;
            }
        }

        private void cboTwitPicUploadMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.TwiPicUploadMode = (TwitPicUploadType)cboTwitPicUploadMode.SelectedIndex;
        }

        private void tcApp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Program.conf.AutoSaveSettings) new Thread(WriteSettings).Start();
        }
    }
}