﻿#region License Information (GPL v2)

/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2011 ZScreen Developers

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
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using GradientTester;
using HelpersLib;
using HelpersLib.CLI;
using ScreenCapture;
using UploadersAPILib;
using UploadersLib;
using UploadersLib.HelperClasses;
using ZScreenGUI.Properties;
using ZScreenGUI.UserControls;
using ZScreenLib;
using ZSS.ColorsLib;
using ZSS.FTPClientLib;
using ZSS.UpdateCheckerLib;

namespace ZScreenGUI
{
    public partial class ZScreen : ZScreenCoreUI
    {
        #region Variables

        public CloseMethod CloseMethod;

        private int mHadFocusAt;
        private TextBox mHadFocus;
        private ContextMenuStrip codesMenu = new ContextMenuStrip();
        private DebugHelper mDebug = null;
        private ImageList tabImageList = new ImageList();

        #endregion Variables

        #region ZScreen Form Events

        public ZScreen()
        {
            InitializeComponent();
            base.tsCoreMainTab.Visible = true;

            pbPreview.DisableViewer = true;
            pbPreview.LoadImage(Resources.ZScreen_256, PictureBoxSizeMode.CenterImage);
            pbPreview.SetNote("You can also Drag n Drop files or a directory on to anywhere in this page.");

            this.Icon = Resources.zss_main;
            this.WindowState = Engine.AppConf.ShowMainWindow ? FormWindowState.Normal : FormWindowState.Minimized;

            BackgroundWorker bwConfig = new BackgroundWorker();
            bwConfig.DoWork += new DoWorkEventHandler(bwConfig_DoWork);
            bwConfig.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwConfig_RunWorkerCompleted);
            bwConfig.RunWorkerAsync();
        }

        private void ZScreen_Load(object sender, EventArgs e)
        {
            #region Windows Size/Location

            if (this.WindowState == FormWindowState.Normal)
            {
                if (Engine.AppConf.WindowLocation.IsEmpty)
                {
                    Engine.AppConf.WindowLocation = this.Location;
                }

                if (Engine.AppConf.WindowSize.IsEmpty)
                {
                    Engine.AppConf.WindowSize = this.Size;
                }

                Rectangle screenRect = CaptureHelpers.GetScreenBounds();
                screenRect.Inflate(-100, -100);
                if (screenRect.IntersectsWith(new Rectangle(Engine.AppConf.WindowLocation, Engine.AppConf.WindowSize)))
                {
                    this.Size = Engine.AppConf.WindowSize;
                    this.Location = Engine.AppConf.WindowLocation;
                }
            }

            if (Engine.AppConf.ShowMainWindow)
            {
                if (Engine.AppConf.WindowState == FormWindowState.Maximized)
                {
                    this.WindowState = FormWindowState.Maximized;
                }
                else
                {
                    this.WindowState = FormWindowState.Normal;
                }
                ShowInTaskbar = Engine.AppConf.ShowInTaskbar;
            }
            else if (Engine.AppConf.ShowInTaskbar && Engine.AppConf.WindowButtonActionClose == WindowButtonAction.MinimizeToTaskbar)
            {
                this.WindowState = FormWindowState.Minimized;
            }
            else
            {
                Hide();
            }

            if (IsReady)
            {
                if (Engine.conf.SaveFormSizePosition)
                {
                    Engine.AppConf.WindowLocation = this.Location;
                    Engine.AppConf.WindowSize = this.Size;
                }
                else
                {
                    Engine.AppConf.WindowLocation = Point.Empty;
                    Engine.AppConf.WindowSize = Size.Empty;
                }
            }

            #endregion Windows Size/Location

            LoggerTimer timer = Engine.EngineLogger.StartTimer(new StackFrame().GetMethod().Name + " started");

            Engine.zHandle = this.Handle;

            if (Engine.IsMultipleInstance)
            {
                niTray.ShowBalloonTip(2000, Engine.GetProductName(), string.Format("Another instance of {0} is already running...", Application.ProductName), ToolTipIcon.Warning);
                niTray.BalloonTipClicked += new EventHandler(niTray2_BalloonTipClicked);
            }

            ZScreen_Preconfig();

            mDebug = new DebugHelper();
            mDebug.GetDebugInfo += new StringEventHandler(debug_GetDebugInfo);

            SetToolTip(nudScreenshotDelay);

            CreateCodesMenu();

            new RichTextBoxMenu(rtbDebugLog, true);
            new RichTextBoxMenu(rtbStats, true);

            Application.Idle += new EventHandler(Application_Idle);

            timer.WriteLineTime(new StackFrame().GetMethod().Name + " finished");
        }

        private void bwConfig_DoWork(object sender, DoWorkEventArgs e)
        {
            Engine.LoadSettings();
        }

        private void bwConfig_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LoggerTimer timer = Engine.EngineLogger.StartTimer(new StackFrame().GetMethod().Name + " started");

            this.Text = Engine.GetProductName();
            this.niTray.Text = this.Text;

            Uploader.ProxySettings = Adapter.CheckProxySettings();

            ZScreen_ConfigGUI();

            if (Engine.conf.CheckUpdates)
            {
                CheckUpdates();
            }

            PerformOnlineTasks();

            CleanCache();

            if (Engine.conf.ProxyConfig != ProxyConfigType.NoProxy && Uploader.ProxySettings.ProxyActive != null)
            {
                StaticHelper.WriteLine("Proxy Settings: " + Uploader.ProxySettings.ProxyActive.ToString());
            }

            if (Engine.conf.BackupApplicationSettings)
            {
                FileSystem.BackupSettings();
            }

            UpdateHotkeys(false);

            if (Engine.conf.FirstRun)
            {
                if (Engine.HasAero)
                {
                    cboCaptureEngine.SelectedIndex = (int)CaptureEngineType.DWM;
                }

                ShowWindow();

                Engine.conf.FirstRun = false;
            }

            timer.WriteLineTime(new StackFrame().GetMethod().Name + " finished");
            StaticHelper.WriteLine("ZScreen startup time: {0} ms", Engine.StartTimer.ElapsedMilliseconds);

            UseCommandLineArg(Loader.CommandLineArg);

            if (Engine.AppConf.Windows7TaskbarIntegration && Engine.HasWindows7)
            {
                ZScreen_Windows7onlyTasks();
            }

            IsReady = true;

            Engine.IsClipboardUploading = false;
            tmrClipboardMonitor.Tick += new EventHandler(tmrClipboardMonitor_Tick);
        }

        private void ZScreen_Resize(object sender, EventArgs e)
        {
            if (IsReady)
            {
                Engine.AppConf.WindowState = WindowState;

                if (WindowState == FormWindowState.Normal)
                {
                    if (Engine.conf.SaveFormSizePosition)
                    {
                        Engine.AppConf.WindowLocation = Location;
                        Engine.AppConf.WindowSize = Size;
                    }
                }

                Refresh();
            }
        }

        private void ZScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Save Destinations
            if (Engine.conf != null)
            {
                Adapter.SaveMenuConfigToList(ucDestOptions.tsddbOutputs, Engine.conf.ConfOutputs);
                Adapter.SaveMenuConfigToList(ucDestOptions.tsddbLinkFormat, Engine.conf.ConfLinkFormat);
                Adapter.SaveMenuConfigToList(ucDestOptions.tsddbClipboardContent, Engine.conf.ConfClipboardContent);
                Adapter.SaveMenuConfigToList(ucDestOptions.tsddbDestImage, Engine.conf.MyImageUploaders);
                Adapter.SaveMenuConfigToList(ucDestOptions.tsddbDestFile, Engine.conf.MyFileUploaders);
                Adapter.SaveMenuConfigToList(ucDestOptions.tsddbDestText, Engine.conf.MyTextUploaders);
                Adapter.SaveMenuConfigToList(ucDestOptions.tsddbDestLink, Engine.conf.MyURLShorteners);
            }

            // If UserClosing && ZScreenCloseReason.None then this means close button pressed in title bar
            if (e.CloseReason == CloseReason.UserClosing && CloseMethod == CloseMethod.None)
            {
                if (Engine.AppConf.WindowButtonActionClose == WindowButtonAction.ExitApplication)
                {
                    CloseMethod = CloseMethod.CloseButton;
                }
                else if (Engine.AppConf.WindowButtonActionClose == WindowButtonAction.MinimizeToTaskbar)
                {
                    WindowState = FormWindowState.Minimized;
                    e.Cancel = true;
                }
                else if (Engine.AppConf.WindowButtonActionClose == WindowButtonAction.MinimizeToTray)
                {
                    Hide();
                    DelayedTrimMemoryUse();
                    if (Engine.conf.AutoSaveSettings) Engine.WriteSettingsAsync();
                    e.Cancel = true;
                }
            }

            // If really ZScreen is closing
            if (!e.Cancel)
            {
                StaticHelper.WriteLine("ZScreen_FormClosing - CloseReason: {0}, CloseMethod: {1}", e.CloseReason, CloseMethod);
                Engine.WriteSettings();
                Engine.TurnOff();
            }
        }

        #endregion ZScreen Form Events

        private void btnRegCodeImageShack_Click(object sender, EventArgs e)
        {
            StaticHelper.LoadBrowser("http://profile.imageshack.us/prefs");
        }

        private void btnGalleryImageShack_Click(object sender, EventArgs e)
        {
            StaticHelper.LoadBrowser("http://my.imageshack.us/v_images.php");
        }

        public bool UseCommandLineArg(string arg)
        {
            if (!string.IsNullOrEmpty(arg))
            {
                StaticHelper.WriteLine("CommandLine: " + arg);
                CLIManagerRegex cli = new CLIManagerRegex();

                cli.Commands = new List<CLICommandRegex>()
                {
                    new CLICommandRegex("fu|fileupload", filePath => UploadUsingFileSystem(filePath)),
                    new CLICommandRegex("cu|clipboardupload", () => UploadUsingClipboard()),
                    new CLICommandRegex("fs|fullscreen", () => CaptureEntireScreen()),
                    new CLICommandRegex("cc|crop", () => CaptureRectRegion()),
                    new CLICommandRegex("sw|selectedwindow", () => CaptureSelectedWindow()),
                    new CLICommandRegex("hi|history", () => OpenHistory()),
                    new CLICommandRegex("ac|autocapture", () => ShowAutoCapture())
                };

                cli.FilePathAction = filePath => UploadUsingFileSystem(filePath);

                return cli.Parse(arg);
            }

            return false;
        }

        private void cbRegionRectangleInfo_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.CropRegionRectangleInfo = chkRegionRectangleInfo.Checked;
            chkCropShowMagnifyingGlass.Enabled = chkRegionRectangleInfo.Checked;
        }

        private void cbRegionHotkeyInfo_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.CropRegionHotkeyInfo = chkRegionHotkeyInfo.Checked;
        }

        #region Trim memory

        private System.Timers.Timer timerTrimMemory;
        Object trimMemoryLock = new Object();

        /// <summary>
        /// Trim memory working set after a few seconds, unless this method is called again in the mean time (optimization)
        /// </summary>
        private void DelayedTrimMemoryUse()
        {
            if (Engine.conf != null && Engine.conf.EnableAutoMemoryTrim)
            {
                try
                {
                    lock (trimMemoryLock)
                    {
                        if (timerTrimMemory == null)
                        {
                            timerTrimMemory = new System.Timers.Timer();
                            timerTrimMemory.AutoReset = false;
                            timerTrimMemory.Interval = 10000;
                            timerTrimMemory.Elapsed += new System.Timers.ElapsedEventHandler(timerTrimMemory_Elapsed);
                        }
                        else
                        {
                            timerTrimMemory.Stop();
                        }

                        timerTrimMemory.Start();
                    }
                }
                catch (Exception ex)
                {
                    StaticHelper.WriteException(ex, "Error in DelayedTrimMemoryUse");
                }
            }
        }

        private void timerTrimMemory_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock (trimMemoryLock)
            {
                if (timerTrimMemory != null)
                {
                    timerTrimMemory.Stop();
                    timerTrimMemory.Close();
                }

                NativeMethods.TrimMemoryUse();
            }
        }

        #endregion Trim memory

        private void UpdateGuiEditors(object sender)
        {
            if (IsReady)
            {
                if (sender.GetType() == lbSoftware.GetType())
                {
                    // the checked state needs to be inversed for some weird reason to get it working properly
                    if (Engine.conf.ActionsApps.HasValidIndex(lbSoftware.SelectedIndex))
                    {
                        Engine.conf.ActionsApps[lbSoftware.SelectedIndex].Enabled = !lbSoftware.GetItemChecked(lbSoftware.SelectedIndex);
                        ToolStripMenuItem tsm = tsmEditinImageSoftware.DropDownItems[lbSoftware.SelectedIndex] as ToolStripMenuItem;
                        tsm.Checked = Engine.conf.ActionsApps[lbSoftware.SelectedIndex].Enabled;
                    }
                }
                else if (sender.GetType() == typeof(ToolStripMenuItem))
                {
                    ToolStripMenuItem tsm = sender as ToolStripMenuItem;
                    int sel = (int)tsm.Tag;
                    if (Engine.conf.ActionsApps.HasValidIndex(sel))
                    {
                        Engine.conf.ActionsApps[sel].Enabled = tsm.Checked;
                        lbSoftware.SetItemChecked(lbSoftware.SelectedIndex, tsm.Checked);
                    }
                }
            }
        }

        /// <summary>
        /// Browse for an applicatoin
        /// </summary>
        /// <returns>Software</returns>
        private Software BrowseApplication()
        {
            Software temp = null;

            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "Executable Files (*.exe)|*.exe|All Files (*.*)|*.*";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    temp = new Software();
                    temp.Name = Path.GetFileNameWithoutExtension(dlg.FileName);
                    temp.Path = dlg.FileName;
                }
            }

            return temp;
        }

        private void tsmSettings_Click(object sender, EventArgs e)
        {
            ShowWindow();
        }

        private void tsmViewDirectory_Click(object sender, EventArgs e)
        {
            ShowDirectory(FileSystem.GetImagesDir());
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
                name = name.Replace(beginning, string.Empty);
                code = "%" + name.ToLower();

                if (mHadFocus != null)
                {
                    mHadFocus.Text = mHadFocus.Text.Insert(mHadFocusAt, code);
                    mHadFocus.Focus();
                    mHadFocus.Select(mHadFocusAt + code.Length, 0);
                }
            }
        }

        private void ShowWindow()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
            this.BringToFront();
        }

        private void tsmLic_Click(object sender, EventArgs e)
        {
            FormsMgr.ShowLicense();
        }

        private void chkManualNaming_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.PromptForOutputs = chkShowWorkflowWizard.Checked;
            if (chkShowWorkflowWizard.Checked)
            {
                chkPerformActions.Checked = false;
            }
            chkPerformActions.Enabled = !chkShowWorkflowWizard.Checked;
        }

        private void clipboardUpload_Click(object sender, EventArgs e)
        {
            UploadUsingClipboard();
        }

        private void selWindow_Click(object sender, EventArgs e)
        {
            CaptureSelectedWindow();
        }

        private void tsmAboutMain_Click(object sender, EventArgs e)
        {
            FormsMgr.ShowAboutWindow();
        }

        public void cbStartWin_CheckedChanged(object sender, EventArgs e)
        {
            RegistryHelper.SetStartWithWindows(chkStartWin.Checked);
        }

        private void nudFlashIconCount_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.FlashTrayCount = nudFlashIconCount.Value;
        }

        private void btnSettingsExport_Click(object sender, EventArgs e)
        {
            AppSettingsExport();
        }

        private void btnSettingsImport_Click(object sender, EventArgs e)
        {
            AppSettingsImport();
        }

        private void AddImageSoftwareToList(Software temp)
        {
            if (temp != null)
            {
                Engine.conf.ActionsApps.Add(temp);
                lbSoftware.Items.Add(temp);
                lbSoftware.SelectedIndex = lbSoftware.Items.Count - 1;
                RewriteImageEditorsRightClickMenu();
            }
        }

        private void btnAddImageSoftware_Click(object sender, EventArgs e)
        {
            Software temp = BrowseApplication();
            if (temp != null)
            {
                AddImageSoftwareToList(temp);
            }
        }

        private void btnDeleteImageSoftware_Click(object sender, EventArgs e)
        {
            int sel = lbSoftware.SelectedIndex;

            if (sel != -1)
            {
                Engine.conf.ActionsApps.RemoveAt(sel);

                lbSoftware.Items.RemoveAt(sel);

                if (lbSoftware.Items.Count > 0)
                {
                    lbSoftware.SelectedIndex = (sel > 0) ? (sel - 1) : 0;
                }
            }

            RewriteImageEditorsRightClickMenu();
        }

        private void SetActiveImageSoftware()
        {
            Engine.conf.ImageEditor = Engine.conf.ActionsApps[lbSoftware.SelectedIndex];
        }

        private void ShowImageEditorsSettings()
        {
            if (lbSoftware.SelectedItem != null)
            {
                Software app = GetImageSoftware(lbSoftware.SelectedItem.ToString());
                if (app != null)
                {
                    Engine.conf.ActionsApps[lbSoftware.SelectedIndex].Enabled = lbSoftware.GetItemChecked(lbSoftware.SelectedIndex);
                    pgEditorsImage.SelectedObject = app;
                    pgEditorsImage.Enabled = !app.Protected;
                    btnActionsRemove.Enabled = !app.Protected;
                    SetActiveImageSoftware();
                }
            }
        }

        private void mImageEditorMenuClose_Tick(object sender, EventArgs e)
        {
            tsmEditinImageSoftware.DropDown.Close();
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            DelayedTrimMemoryUse();
        }

        private void lbSoftware_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowImageEditorsSettings();
        }

        private void cbDeleteLocal_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.DeleteLocal = chkDeleteLocal.Checked;
        }

        private void txtActiveWindow_TextChanged(object sender, EventArgs e)
        {
            Engine.Workflow.ActiveWindowPattern = txtActiveWindow.Text;
            NameParser parser = new NameParser(NameParserType.ActiveWindow)
            {
                CustomProductName = Engine.GetProductName(),
                IsPreview = true,
                MaxNameLength = Engine.Workflow.MaxNameLength
            };
            lblActiveWindowPreview.Text = parser.Convert(Engine.Workflow.ActiveWindowPattern);
        }

        private void txtEntireScreen_TextChanged(object sender, EventArgs e)
        {
            Engine.Workflow.EntireScreenPattern = txtEntireScreen.Text;
            NameParser parser = new NameParser(NameParserType.EntireScreen)
            {
                CustomProductName = Engine.GetProductName(),
                IsPreview = true,
                MaxNameLength = Engine.Workflow.MaxNameLength
            };
            lblEntireScreenPreview.Text = parser.Convert(Engine.Workflow.EntireScreenPattern);
        }

        private void cbShowPopup_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.ShowBalloonTip = cbShowPopup.Checked;
        }

        private void LoadSettingsDefault()
        {
            Engine.conf = new XMLSettings();
            ZScreen_ConfigGUI();
            Engine.conf.FirstRun = false;
        }

        private void btnDeleteSettings_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to revert settings to default values?", Application.ProductName,
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                LoadSettingsDefault();
            }
        }

        private void cropShot_Click(object sender, EventArgs e)
        {
            CaptureRectRegion();
        }

        private void ShowMainWindow()
        {
            if (this.IsHandleCreated)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                NativeMethods.ActivateWindow(this.Handle);
            }
        }

        private void cbCropStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.CropRegionStyles = (RegionStyles)chkCropStyle.SelectedIndex;
        }

        private void pbCropBorderColor_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Engine.conf.CropBorderArgb);
        }

        private void nudCropBorderSize_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.CropBorderSize = nudCropBorderSize.Value;
        }

        private void llblBugReports_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StaticHelper.LoadBrowser(ZLinks.URL_ISSUES);
        }

        private void cbCompleteSound_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.CompleteSound = cbCompleteSound.Checked;
        }

        private void cbCheckUpdates_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.CheckUpdates = chkCheckUpdates.Checked;
        }

        private void txtWatermarkText_TextChanged(object sender, EventArgs e)
        {
            Engine.Workflow.WatermarkText = txtWatermarkText.Text;
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

        #region Codes Menu

        private void CreateCodesMenu()
        {
            var variables = Enum.GetValues(typeof(ReplacementVariables)).Cast<ReplacementVariables>().
                Select(x => new { Name = ReplacementExtension.Prefix + Enum.GetName(typeof(ReplacementVariables), x), Description = x.GetDescription() });

            foreach (var variable in variables)
            {
                ToolStripMenuItem tsi = new ToolStripMenuItem { Text = string.Format("{0} - {1}", variable.Name, variable.Description), Tag = variable.Name };
                tsi.Click += watermarkCodeMenu_Click;
                codesMenu.Items.Add(tsi);
            }

            CodesMenuCloseEvents();
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

        #endregion Codes Menu

        private void watermarkCodeMenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsi = (ToolStripMenuItem)sender;
            int oldPos = txtWatermarkText.SelectionStart;
            string appendText;
            if (oldPos > 0 && txtWatermarkText.Text[txtWatermarkText.SelectionStart - 1] == ReplacementExtension.Prefix)
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
            Engine.Workflow.DrawCursor = chkShowCursor.Checked;
        }

        private void btnWatermarkFont_Click(object sender, EventArgs e)
        {
            DialogResult result = Adapter.ShowFontDialog();
            if (result == DialogResult.OK)
            {
                pbWatermarkFontColor.BackColor = Engine.Workflow.WatermarkFontArgb;
                lblWatermarkFont.Text = FontToString();
                TestWatermark();
            }
        }

        private string FontToString()
        {
            return FontToString(Engine.Workflow.WatermarkFont, Engine.Workflow.WatermarkFontArgb);
        }

        private string FontToString(Font font, Color color)
        {
            return "Name: " + font.Name + " - Size: " + font.Size + " - Style: " + font.Style + " - Color: " +
                color.R + "," + color.G + "," + color.B;
        }

        private void nudWatermarkOffset_ValueChanged(object sender, EventArgs e)
        {
            Engine.Workflow.WatermarkOffset = nudWatermarkOffset.Value;
            TestWatermark();
        }

        private void nudWatermarkBackTrans_ValueChanged(object sender, EventArgs e)
        {
            Engine.Workflow.WatermarkBackTrans = nudWatermarkBackTrans.Value;
            trackWatermarkBackgroundTrans.Value = (int)nudWatermarkBackTrans.Value;
        }

        private void entireScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.Sleep(300);
            CaptureEntireScreen();
        }

        private void selectedWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.Sleep(300);
            CaptureSelectedWindow();
        }

        private void rectangularRegionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.Sleep(300);
            CaptureRectRegion();
        }

        private void lastRectangularRegionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.Sleep(300);
            CaptureRectRegionLast();
        }

        private void tsmFreehandCropShot_Click(object sender, EventArgs e)
        {
            Thread.Sleep(300);
            CaptureFreeHandRegion();
        }

        private void autoScreenshotsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAutoCapture();
        }

        private void tsmFileUpload_Click(object sender, EventArgs e)
        {
            FileUpload();
        }

        private void tsmUploadFromClipboard_Click(object sender, EventArgs e)
        {
            ClipboardUpload();
        }

        private void tsmDropWindow_Click(object sender, EventArgs e)
        {
            ShowDropWindow();
        }

        private void languageTranslatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartWorkerTranslator();
        }

        private void screenColorPickerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowScreenColorPicker();
        }

        private void pbWatermarkGradient1_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Engine.Workflow.WatermarkGradient1Argb);
            TestWatermark();
        }

        private void pbWatermarkGradient2_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Engine.Workflow.WatermarkGradient2Argb);
            TestWatermark();
        }

        private void pbWatermarkBorderColor_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Engine.Workflow.WatermarkBorderArgb);
            TestWatermark();
        }

        private void TestWatermark()
        {
            using (Bitmap bmp = Resources.main.Clone(new Rectangle(62, 33, 199, 140), PixelFormat.Format32bppArgb))
            {
                Bitmap bmp2 = new Bitmap(pbWatermarkShow.ClientRectangle.Width, pbWatermarkShow.ClientRectangle.Height);
                Graphics g = Graphics.FromImage(bmp2);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new Rectangle(0, 0, pbWatermarkShow.ClientRectangle.Width, pbWatermarkShow.ClientRectangle.Height));
                pbWatermarkShow.Image = new ImageEffects(Engine.Workflow).ApplyWatermark(bmp2);
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
            SelectColor((PictureBox)sender, ref Engine.Workflow.WatermarkFontArgb);
            lblWatermarkFont.Text = FontToString();
            TestWatermark();
        }

        private void SelectColor(Control pb, ref XColor color)
        {
            DialogColor dColor = new DialogColor(pb.BackColor);
            if (dColor.ShowDialog() == DialogResult.OK)
            {
                pb.BackColor = dColor.Color;
                color = dColor.Color;
            }
        }

        private void cbWatermarkPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.Workflow.WatermarkPositionMode = (WatermarkPositionType)chkWatermarkPosition.SelectedIndex;
            TestWatermark();
        }

        private void nudWatermarkFontTrans_ValueChanged(object sender, EventArgs e)
        {
            Engine.Workflow.WatermarkFontTrans = nudWatermarkFontTrans.Value;
            trackWatermarkFontTrans.Value = (int)nudWatermarkFontTrans.Value;
        }

        private void nudWatermarkCornerRadius_ValueChanged(object sender, EventArgs e)
        {
            Engine.Workflow.WatermarkCornerRadius = nudWatermarkCornerRadius.Value;
            TestWatermark();
        }

        private void cbWatermarkGradientType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.Workflow.WatermarkGradientType = (LinearGradientMode)cbWatermarkGradientType.SelectedIndex;
            TestWatermark();
        }

        private void btnViewLocalDirectory_Click(object sender, EventArgs e)
        {
            ShowDirectory(FileSystem.GetImagesDir());
        }

        private void btnViewRemoteDirectory_Click(object sender, EventArgs e)
        {
            ShowDirectory(Engine.LogsDir);
        }

        private void cbOpenMainWindow_CheckedChanged(object sender, EventArgs e)
        {
            Engine.AppConf.ShowMainWindow = chkOpenMainWindow.Checked;
        }

        private void llWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StaticHelper.LoadBrowser(ZLinks.URL_WEBSITE);
        }

        private void llProjectPage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StaticHelper.LoadBrowser(ZLinks.URL_WIKIPAGES);
        }

        private void ZScreen_Deactivate(object sender, EventArgs e)
        {
            codesMenu.Close();
            ucDestOptions.DropDownMenusClose();
        }

        private void txtWatermarkText_MouseDown(object sender, MouseEventArgs e)
        {
            CheckForCodes(sender);
        }

        private void chkBalloonTipOpenLink_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.BalloonTipOpenLink = chkBalloonTipOpenLink.Checked;
        }

        private void cmVersionHistory_Click(object sender, EventArgs e)
        {
            FormsMgr.ShowVersionHistory();
        }

        #region Language Translator

        #endregion Language Translator

        private void ProxySetup(IEnumerable<ProxyInfo> accs)
        {
            if (accs != null)
            {
                ucProxyAccounts.AccountsList.Items.Clear();
                Engine.conf.ProxyList = new List<ProxyInfo>();
                Engine.conf.ProxyList.AddRange(accs);
                foreach (ProxyInfo acc in Engine.conf.ProxyList)
                {
                    ucProxyAccounts.AccountsList.Items.Add(acc);
                }
            }
        }

        private ProxyInfo GetSelectedProxy()
        {
            ProxyInfo acc = null;
            if (ucProxyAccounts.AccountsList.SelectedIndex != -1 && Engine.conf.ProxyList.Count >= ucProxyAccounts.AccountsList.Items.Count)
            {
                acc = Engine.conf.ProxyList[ucProxyAccounts.AccountsList.SelectedIndex];
            }

            return acc;
        }

        private FTPAccount GetSelectedFTPforImages()
        {
            FTPAccount acc = null;
            if (Adapter.CheckFTPAccounts(Engine.Workflow.ConfigOutputs.FTPSelectedImage))
            {
                acc = Engine.Workflow.ConfigOutputs.FTPAccountList[Engine.Workflow.ConfigOutputs.FTPSelectedImage];
            }

            return acc;
        }

        private void cbSelectedWindowRectangleInfo_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.SelectedWindowRectangleInfo = cbSelectedWindowRectangleInfo.Checked;
        }

        private void pbSelectedWindowBorderColor_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Engine.conf.SelectedWindowBorderArgb);
        }

        private void nudSelectedWindowBorderSize_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.SelectedWindowBorderSize = nudSelectedWindowBorderSize.Value;
        }

        private void btnCheckUpdate_Click(object sender, EventArgs e)
        {
            CheckUpdates();
        }

        private void cbShowUploadDuration_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.ShowUploadDuration = cbShowUploadDuration.Checked;
        }

        /// <summary>
        /// Searches for an Image Software in settings and returns it
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static Software GetImageSoftware(string name)
        {
            foreach (Software app in Engine.conf.ActionsApps)
            {
                if (app != null && app.Name != null)
                {
                    if (app.Name.Equals(name))
                    {
                        return app;
                    }
                }
            }

            return null;
        }

        private void cbSelectedWindowStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.SelectedWindowRegionStyles = (RegionStyles)cbSelectedWindowStyle.SelectedIndex;
        }

        private void nudCropGridSize_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.CropGridSize.Width = (int)nudCropGridWidth.Value;
        }

        private void nudCropGridHeight_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.CropGridSize.Height = (int)nudCropGridHeight.Value;
        }

        private void cbCropShowGrids_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.CropShowGrids = cbCropShowGrids.Checked;
        }

        private void nudHistoryMaxItems_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.HistoryMaxNumber = (int)nudHistoryMaxItems.Value;
        }

        private void cbCloseDropBox_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.CloseDropBox = cbCloseDropBox.Checked;
        }

        private void btnResetIncrement_Click(object sender, EventArgs e)
        {
            Engine.Workflow.AutoIncrement = 0;
        }

        private void cbHistorySave_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.HistorySave = cbHistorySave.Checked;
        }

        private void pbCropCrosshairColor_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Engine.conf.CropCrosshairArgb);
        }

        private void chkCaptureFallback_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.CaptureEntireScreenOnError = chkCaptureFallback.Checked;
        }

        private void cbCropDynamicCrosshair_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.CropDynamicCrosshair = chkCropDynamicCrosshair.Checked;
        }

        private void nudCrosshairLineCount_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.CrosshairLineCount = (int)nudCrosshairLineCount.Value;
        }

        private void nudCrosshairLineSize_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.CrosshairLineSize = (int)nudCrosshairLineSize.Value;
        }

        private void nudCropInterval_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.CropInterval = (int)nudCropCrosshairInterval.Value;
        }

        private void nudCropStep_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.CropStep = (int)nudCropCrosshairStep.Value;
        }

        private void cbCropShowBigCross_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.CropShowBigCross = chkCropShowBigCross.Checked;
        }

        private void cbShowCropRuler_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.CropShowRuler = cbShowCropRuler.Checked;
        }

        private void cbSelectedWindowRuler_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.SelectedWindowRuler = cbSelectedWindowRuler.Checked;
        }

        private void cbCropDynamicBorderColor_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.CropDynamicBorderColor = cbCropDynamicBorderColor.Checked;
        }

        private void nudCropRegionInterval_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.CropRegionInterval = nudCropRegionInterval.Value;
        }

        private void nudCropRegionStep_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.CropRegionStep = nudCropRegionStep.Value;
        }

        private void nudCropHueRange_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.CropHueRange = nudCropHueRange.Value;
        }

        private void cbSelectedWindowDynamicBorderColor_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.SelectedWindowDynamicBorderColor = cbSelectedWindowDynamicBorderColor.Checked;
        }

        private void nudSelectedWindowRegionInterval_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.SelectedWindowRegionInterval = nudSelectedWindowRegionInterval.Value;
        }

        private void nudSelectedWindowRegionStep_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.SelectedWindowRegionStep = nudSelectedWindowRegionStep.Value;
        }

        private void nudSelectedWindowHueRange_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.SelectedWindowHueRange = nudSelectedWindowHueRange.Value;
        }

        private void cbCropGridMode_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.CropGridToggle = cboCropGridMode.Checked;
        }

        private void txtWatermarkImageLocation_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(txtWatermarkImageLocation.Text))
            {
                Engine.Workflow.WatermarkImageLocation = txtWatermarkImageLocation.Text;
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

        private void debug_GetDebugInfo(object sender, string e)
        {
            if (this.Visible)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(e);
                rtbStats.Text = sb.ToString();
            }
        }

        private void btnDebugStart_Click(object sender, EventArgs e)
        {
            if (mDebug.DebugTimer.Enabled)
            {
                btnDebugStart.Text = "Start";
            }
            else
            {
                btnDebugStart.Text = "Pause";
            }

            mDebug.DebugTimer.Enabled = !mDebug.DebugTimer.Enabled;
        }

        private void tsmMain_Click(object sender, EventArgs e)
        {
            ShowWindow();
        }

        private void btnGalleryTinyPic_Click(object sender, EventArgs e)
        {
            StaticHelper.LoadBrowser("http://tinypic.com/yourstuff.php");
        }

        private void cbWatermarkUseBorder_CheckedChanged(object sender, EventArgs e)
        {
            Engine.Workflow.WatermarkUseBorder = cbWatermarkUseBorder.Checked;
            TestWatermark();
        }

        private void cbWatermarkAddReflection_CheckedChanged(object sender, EventArgs e)
        {
            Engine.Workflow.WatermarkAddReflection = cbWatermarkAddReflection.Checked;
            TestWatermark();
        }

        private void btnBrowseRootDir_Click(object sender, EventArgs e)
        {
            string oldRootDir = txtRootFolder.Text;
            string dirNew = Adapter.GetDirPathUsingFolderBrowser("Configure Root directory...");

            if (!string.IsNullOrEmpty(dirNew))
            {
                Engine.SetRootFolder(dirNew);
                txtRootFolder.Text = Engine.AppConf.RootDir;
                FileSystem.MoveDirectory(oldRootDir, txtRootFolder.Text);
                ZScreen_ConfigGUI_Options_Paths();
                Engine.conf = XMLSettings.Read();
                ZScreen_ConfigGUI();
            }
        }

        private void btnViewRootDir_Click(object sender, EventArgs e)
        {
            ShowDirectory(txtRootFolder.Text);
        }

        private void nudWatermarkImageScale_ValueChanged(object sender, EventArgs e)
        {
            Engine.Workflow.WatermarkImageScale = nudWatermarkImageScale.Value;
            TestWatermark();
        }

        private void trackWatermarkFontTrans_Scroll(object sender, EventArgs e)
        {
            Engine.Workflow.WatermarkFontTrans = trackWatermarkFontTrans.Value;
            nudWatermarkFontTrans.Value = Engine.Workflow.WatermarkFontTrans;
            TestWatermark();
        }

        private void trackWatermarkBackgroundTrans_Scroll(object sender, EventArgs e)
        {
            Engine.Workflow.WatermarkBackTrans = trackWatermarkBackgroundTrans.Value;
            nudWatermarkBackTrans.Value = Engine.Workflow.WatermarkBackTrans;
            TestWatermark();
        }

        private void cbWatermarkAutoHide_CheckedChanged(object sender, EventArgs e)
        {
            Engine.Workflow.WatermarkAutoHide = cbWatermarkAutoHide.Checked;
            TestWatermark();
        }

        private void cboWatermarkType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.Workflow.WatermarkMode = (WatermarkType)cboWatermarkType.SelectedIndex;
            TestWatermark();
            tcWatermark.Enabled = Engine.Workflow.WatermarkMode != WatermarkType.NONE;
        }

        private void cbCropShowMagnifyingGlass_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.CropShowMagnifyingGlass = chkCropShowMagnifyingGlass.Checked;
        }

        private void numericUpDownTimer1_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.ScreenshotDelayTime = nudScreenshotDelay.Value;
        }

        private void nudtScreenshotDelay_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.ScreenshotDelayTimes = nudScreenshotDelay.Time;
        }

        /// <summary>
        /// Method to periodically (every 6 hours) perform online tasks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrApp_Tick(object sender, EventArgs e)
        {
            PerformOnlineTasks();
        }

        private void pgAppConfig_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            ZScreen_ConfigGUI();
        }

        private void pgEditorsImage_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            Software temp = Engine.conf.ActionsApps[lbSoftware.SelectedIndex];
            lbSoftware.Items[lbSoftware.SelectedIndex] = temp;
            Engine.conf.ActionsApps[lbSoftware.SelectedIndex] = temp;
            RewriteImageEditorsRightClickMenu();
        }

        private void cbShowHelpBalloonTips_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.ShowHelpBalloonTips = cbShowHelpBalloonTips.Checked;
            ttZScreen.Active = Engine.conf.ShowHelpBalloonTips;
        }

        private void tsmFTPClient_Click(object sender, EventArgs e)
        {
            OpenFTPClient();
        }

        private void cbSelectedWindowCaptureObjects_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.SelectedWindowCaptureObjects = chkSelectedWindowCaptureObjects.Checked;
        }

        private void cbSelectedWindowCleanBackground_CheckedChanged(object sender, EventArgs e)
        {
            Engine.Workflow.ActiveWindowClearBackground = chkActiveWindowCleanBackground.Checked;
            UpdateAeroGlassConfig();
        }

        private void UpdateAeroGlassConfig()
        {
            gbCaptureGdi.Enabled = Engine.Workflow.CaptureEngineMode == CaptureEngineType.GDI;
            gbCaptureDwm.Enabled = Engine.Workflow.CaptureEngineMode == CaptureEngineType.DWM && !chkActiveWindowCleanBackground.Checked;
            gbCaptureGdiDwm.Enabled = Engine.Workflow.CaptureEngineMode != CaptureEngineType.Hybrid;

            // Disable Show Checkers option if Clean Background is disabled
            if (!chkActiveWindowCleanBackground.Checked)
            {
                chkSelectedWindowShowCheckers.Checked = false;
            }
            chkSelectedWindowShowCheckers.Enabled = chkActiveWindowCleanBackground.Checked;

            // Disable Capture children option if DWM is enabled
            if (cboCaptureEngine.SelectedIndex == (int)CaptureEngineType.DWM)
            {
                chkActiveWindowTryCaptureChildren.Checked = false;
            }
            chkActiveWindowTryCaptureChildren.Enabled = cboCaptureEngine.SelectedIndex != (int)CaptureEngineType.DWM;

            // With GDI, corner-clearing cannot be disabled when both "clean background" and "include shadow" are enabled
            if (chkSelectedWindowShowCheckers.Enabled = chkActiveWindowCleanBackground.Checked ||
                !chkActiveWindowCleanBackground.Checked || !chkSelectedWindowIncludeShadow.Checked)
            {
                chkSelectedWindowCleanTransparentCorners.Enabled = true;
            }
            else
            {
                chkSelectedWindowCleanTransparentCorners.Enabled = false;
                chkSelectedWindowCleanTransparentCorners.Checked = true;
            }
        }

        private void cbSelectedWindowCleanTransparentCorners_CheckedChanged(object sender, EventArgs e)
        {
            Engine.Workflow.ActiveWindowCleanTransparentCorners = chkSelectedWindowCleanTransparentCorners.Checked;
            UpdateAeroGlassConfig();
        }

        private void cbAutoSaveSettings_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.AutoSaveSettings = cbAutoSaveSettings.Checked;
        }

        private void nudtScreenshotDelay_MouseHover(object sender, EventArgs e)
        {
            ttZScreen.Show(ttZScreen.GetToolTip(nudScreenshotDelay), this);
        }

        private void txtImagesFolderPattern_TextChanged(object sender, EventArgs e)
        {
            Engine.Workflow.SaveFolderPattern = txtImagesFolderPattern.Text;
            lblImagesFolderPatternPreview.Text = new NameParser(NameParserType.SaveFolder).Convert(Engine.Workflow.SaveFolderPattern);
            txtImagesDir.Text = Engine.ImagesDir;
        }

        private void txtWatermarkText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && codesMenu.Visible)
            {
                codesMenu.Close();
            }
        }

        private void btnMoveImageFiles_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileSystem.ManageImageFolders(Engine.RootImagesDir))
                {
                    MessageBox.Show("Files successfully moved to save folders.");
                }
            }
            catch (Exception ex)
            {
                StaticHelper.WriteException(ex, "Error while moving image files");
                MessageBox.Show(ex.Message);
            }
        }

        private void chkWindows7TaskbarIntegration_CheckedChanged(object sender, EventArgs e)
        {
            if (IsReady)
            {
                if (chkWindows7TaskbarIntegration.Checked)
                {
                    Engine.AppConf.ShowInTaskbar = true; // Application requires to be shown in Taskbar for Windows 7 Integration
                }
                Engine.AppConf.Windows7TaskbarIntegration = chkWindows7TaskbarIntegration.Checked;
                ZScreen_Windows7onlyTasks();
            }
        }

        public void OpenFTPClient()
        {
            if (Engine.Workflow.ConfigOutputs.FTPAccountList.Count > 0)
            {
                FTPAccount acc = Engine.Workflow.ConfigOutputs.FTPAccountList[Engine.Workflow.ConfigOutputs.FTPSelectedImage] as FTPAccount;
                if (acc != null)
                {
                    if (acc.Protocol == FTPProtocol.SFTP)
                    {
                        MessageBox.Show("Sorry, this doesn't support SFTP.", "Sorry!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    FTPClient2 ftpClient = new FTPClient2(acc) { Icon = this.Icon };
                    ftpClient.Show();
                }
            }
        }

        private void btnFTPOpenClient_Click(object sender, EventArgs e)
        {
            OpenFTPClient();
        }

        private void chkShellExt_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShellExt.Checked)
            {
                RegistryHelper.RegisterShellContextMenu();
            }
            else
            {
                RegistryHelper.UnregisterShellContextMenu();
            }
        }

        private void chkTwitterEnable_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.TwitterEnabled = chkTwitterEnable.Checked;
        }

        private void btnFtpHelp_Click(object sender, EventArgs e)
        {
            StaticHelper.LoadBrowser("http://code.google.com/p/zscreen/wiki/FTPAccounts");
        }

        private void btnOpenZScreenTester_Click(object sender, EventArgs e)
        {
            new ZScreenTesterGUI.TesterGUI().ShowDialog();
        }

        private void nudMaxNameLength_ValueChanged(object sender, EventArgs e)
        {
            Engine.Workflow.MaxNameLength = (int)nudMaxNameLength.Value;
        }

        private void SetToolTip(Control original)
        {
            SetToolTip(original, original);
        }

        private void SetToolTip(Control original, Control next)
        {
            ttZScreen.SetToolTip(next, ttZScreen.GetToolTip(original));
            foreach (Control c in next.Controls)
            {
                SetToolTip(original, c);
            }
        }

        private void btnSelectGradient_Click(object sender, EventArgs e)
        {
            using (GradientMaker gradient = new GradientMaker(Engine.Workflow.GradientMakerOptions))
            {
                gradient.Icon = this.Icon;
                if (gradient.ShowDialog() == DialogResult.OK)
                {
                    Engine.Workflow.GradientMakerOptions = gradient.Options;
                    TestWatermark();
                }
            }
        }

        private void cbUseCustomGradient_CheckedChanged(object sender, EventArgs e)
        {
            Engine.Workflow.WatermarkUseCustomGradient = cboUseCustomGradient.Checked;
            gbGradientMakerBasic.Enabled = !cboUseCustomGradient.Checked;
            TestWatermark();
        }

        private void cbSelectedWindowIncludeShadow_CheckedChanged(object sender, EventArgs e)
        {
            Engine.Workflow.ActiveWindowIncludeShadows = chkSelectedWindowIncludeShadow.Checked;
            UpdateAeroGlassConfig();
        }

        private void cbSelectedWindowShowCheckers_CheckedChanged(object sender, EventArgs e)
        {
            Engine.Workflow.ActiveWindowShowCheckers = chkSelectedWindowShowCheckers.Checked;
        }

        private void chkMonImages_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.MonitorImages = chkMonImages.Checked;
        }

        private void chkMonText_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.MonitorText = chkMonText.Checked;
        }

        private void chkMonFiles_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.MonitorFiles = chkMonFiles.Checked;
        }

        private void chkMonUrls_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.MonitorUrls = chkMonUrls.Checked;
        }

        private void chkActiveWindowTryCaptureChilds_CheckedChanged(object sender, EventArgs e)
        {
            Engine.Workflow.ActiveWindowTryCaptureChildren = chkActiveWindowTryCaptureChildren.Checked;
        }

        private void ChkEditorsEnableCheckedChanged(object sender, EventArgs e)
        {
            Engine.Workflow.PerformActions = chkPerformActions.Checked;
        }

        private void tsmEditinImageSoftware_CheckedChanged(object sender, EventArgs e)
        {
            chkPerformActions.Checked = tsmEditinImageSoftware.Checked;
        }

        private void LbSoftwareItemCheck(object sender, ItemCheckEventArgs e)
        {
            UpdateGuiEditors(sender);
        }

        private void txtDebugLog_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            StaticHelper.LoadBrowser(e.LinkText);
        }

        private void cbCloseButtonAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.AppConf.WindowButtonActionClose = (WindowButtonAction)cboCloseButtonAction.SelectedIndex;
        }

        private void cbMinimizeButtonAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.AppConf.WindowButtonActionMinimize = (WindowButtonAction)cboMinimizeButtonAction.SelectedIndex;
        }

        private void LbSoftwareMouseClick(object sender, MouseEventArgs e)
        {
            int sel = lbSoftware.IndexFromPoint(e.X, e.Y);
            if (sel != -1)
            {
                // The following lines have been commented out because of unusual check/uncheck behavior
                // MessageBox.Show(lbSoftware.GetItemChecked(sel).ToString());
                // lbSoftware.SetItemChecked(sel, !lbSoftware.GetItemChecked(sel));
            }
        }

        private void BtnBrowseImagesDirClick(object sender, EventArgs e)
        {
            string oldDir = txtImagesDir.Text;
            string dirNew = Path.Combine(Adapter.GetDirPathUsingFolderBrowser("Configure Custom Images Directory..."), "Images");

            if (!string.IsNullOrEmpty(dirNew))
            {
                Engine.conf.UseCustomImagesDir = true;
                Engine.conf.CustomImagesDir = dirNew;
                FileSystem.MoveDirectory(oldDir, txtImagesDir.Text);
                ZScreen_ConfigGUI_Options_Paths();
            }
        }

        private void editInPicnikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Edit in Picnik in the new History
            //Process.Start(string.Format("http://www.picnik.com/service/?_import={0}&_apikey={1}",
            //                    HttpUtility.UrlEncode(hi.RemotePath), Engine.PicnikKey));
        }

        private void cbFreehandCropShowHelpText_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.FreehandCropShowHelpText = cbFreehandCropShowHelpText.Checked;
        }

        private void cbFreehandCropAutoUpload_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.FreehandCropAutoUpload = cbFreehandCropAutoUpload.Checked;
        }

        private void cbFreehandCropAutoClose_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.FreehandCropAutoClose = cbFreehandCropAutoClose.Checked;
        }

        private void cbFreehandCropShowRectangleBorder_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.FreehandCropShowRectangleBorder = cbFreehandCropShowRectangleBorder.Checked;
        }

        private void cboProxyConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.ProxyConfig = (ProxyConfigType)cboProxyConfig.SelectedIndex;
            if (IsReady)
            {
                Uploader.ProxySettings = Adapter.CheckProxySettings();
            }
        }

        private void historyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenHistory(sender, e);
        }

        private void tpSourceFileSystem_DragDrop(object sender, DragEventArgs e)
        {
            string[] ddfilePaths = (string[])e.Data.GetData(DataFormats.FileDrop, true);
            UploadUsingFileSystem(ddfilePaths);
        }

        private void tpSourceFileSystem_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        public override void FileUpload()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Multiselect = true;
                ofd.Title = "Upload files...";
                ofd.Filter = "All Files (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    UploadUsingFileSystem(ofd.FileNames);
                }
            }
        }

        public override void ClipboardUpload()
        {
            if (Engine.conf.ShowClipboardContentViewer)
            {
                using (ClipboardContentViewer ccv = new ClipboardContentViewer())
                {
                    if (ccv.ShowDialog() == DialogResult.OK && !ccv.IsClipboardEmpty)
                    {
                        UploadUsingClipboard();
                    }

                    Engine.conf.ShowClipboardContentViewer = !ccv.DontShowThisWindow;
                }
            }
            else
            {
                UploadUsingClipboard();
            }
        }

        private void tpMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void tpMain_DragDrop(object sender, DragEventArgs e)
        {
            string[] fileDirPaths = (string[])e.Data.GetData(DataFormats.FileDrop, true);
            List<string> ddMainGUIfiles = new List<string>();
            foreach (string fdp in fileDirPaths)
            {
                if (File.Exists(fdp))
                {
                    ddMainGUIfiles.Add(fdp);
                }
                else if (Directory.Exists(fdp))
                {
                    ddMainGUIfiles.AddRange(Directory.GetFiles(fdp, "*.*", SearchOption.AllDirectories));
                }
            }
            UploadUsingFileSystem(ddMainGUIfiles.ToArray());
        }

        private void chkShortenURL_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.ShortenUrlAfterUpload = chkShortenURL.Checked;
        }

        private void cboReleaseChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.ReleaseChannel = (ReleaseChannelType)cboReleaseChannel.SelectedIndex;
        }

        private void btnClearHistory_Click(object sender, EventArgs e)
        {
            if (File.Exists(Engine.HistoryPath))
            {
                if (MessageBox.Show("Do you really want to delete History?\r\nHistory file path: " + Engine.HistoryPath, "ZScreen - History",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    File.Delete(Engine.HistoryPath);
                }
            }
        }

        private void tcMain_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == tpAdvanced && tcAdvanced.SelectedTab == tpAdvancedDebug)
            {
                rtbDebugLog.Text = Engine.EngineLogger.ToString();
            }
        }

        private void tcAdvanced_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tpAdvancedDebug)
            {
                rtbDebugLog.Text = Engine.EngineLogger.ToString();
            }
        }

        private void pgAppSettings_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (IsReady)
            {
                ZScreen_ConfigGUI_Options_Paths();

                if (!string.IsNullOrEmpty(Engine.AppConf.WorkflowConfigCustomPath))
                {
                    Engine.Workflow.ConfigOutputs = UploadersConfig.Load(Engine.AppConf.WorkflowConfigCustomPath);
                }
            }
        }

        private void chkShowUploadResults_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.ShowUploadResultsWindow = chkShowUploadResults.Checked;
        }

        private void tsbDonate_Click(object sender, EventArgs e)
        {
            StaticHelper.LoadBrowser(ZLinks.URL_DONATE_ZS);
        }

        private void tsbDonate_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void tsbDonate_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void btnLastCropShotReset_Click(object sender, EventArgs e)
        {
            Engine.conf.LastCapture = Rectangle.Empty;
            Engine.conf.LastRegion = Rectangle.Empty;
        }

        private void chkOverwriteFiles_CheckedChanged(object sender, EventArgs e)
        {
            Engine.Workflow.OverwriteFiles = chkOverwriteFiles.Checked;
        }

        private void btnOutputsConfigImport_Click(object sender, EventArgs e)
        {
            OutputsConfigImport();
        }

        private void btnOutputsConfigExport_Click(object sender, EventArgs e)
        {
            OutputsConfigExport();
        }

        private void tpMain_MouseClick(object sender, MouseEventArgs e)
        {
            ucDestOptions.DropDownMenusClose();
        }

        private void cboCropEngine_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.CropEngineMode = (CropEngineType)cboCropEngine.SelectedIndex;
            gbCropRegion.Visible = Engine.conf.CropEngineMode == CropEngineType.Cropv1;
            gbCropCrosshairSettings.Visible = Engine.conf.CropEngineMode == CropEngineType.Cropv1;
            gbCropDynamicRegionBorderColorSettings.Visible = Engine.conf.CropEngineMode == CropEngineType.Cropv1;
            gbCropGridMode.Visible = Engine.conf.CropEngineMode == CropEngineType.Cropv1;
            gbCropRegionSettings.Visible = Engine.conf.CropEngineMode == CropEngineType.Cropv1;
            gbCropShotMagnifyingGlass.Visible = Engine.conf.CropEngineMode == CropEngineType.Cropv1;
        }

        private void tsbLinkHome_Click(object sender, EventArgs e)
        {
            StaticHelper.LoadBrowser(ZLinks.URL_WEBSITE);
        }

        private void tsbLinkIssues_Click(object sender, EventArgs e)
        {
            StaticHelper.LoadBrowser(ZLinks.URL_ISSUES);
        }

        private void tsbLinkHelp_Click(object sender, EventArgs e)
        {
            StaticHelper.LoadBrowser(ZLinks.URL_WIKIPAGES);
        }

        private void tpMain_MouseLeave(object sender, EventArgs e)
        {
            ucDestOptions.DropDownMenusClose();
        }

        private void btnUploadersConfigImport_Click(object sender, EventArgs e)
        {
            UploadersConfigImport();
        }

        private void btnUploadersConfigExport_Click(object sender, EventArgs e)
        {
            UploadersConfigExport();
        }

        private void cboCaptureEngine_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.Workflow.CaptureEngineMode = (CaptureEngineType)cboCaptureEngine.SelectedIndex;
            UpdateAeroGlassConfig();
        }

        public void tsddbSelectedWindow_DropDownOpening(object sender, EventArgs e)
        {
            CaptureSelectedWindowGetList();
        }

        public override void CaptureSelectedWindowGetList()
        {
            tsddbCoreSelectedWindow.DropDownItems.Clear();

            WindowsList windowsList = new WindowsList();
            List<WindowInfo> windows = windowsList.GetVisibleWindowsList();

            foreach (WindowInfo window in windows)
            {
                string title = window.Text.Truncate(50);
                ToolStripItem tsiSelectedWindow = tsddbCoreSelectedWindow.DropDownItems.Add(title);
                tsiSelectedWindow.Click += new EventHandler(tsiSelectedWindow_Click);

                using (Icon icon = window.Icon)
                {
                    if (icon != null)
                    {
                        tsiSelectedWindow.Image = icon.ToBitmap();
                    }
                }

                tsiSelectedWindow.Tag = window;
            }
        }

        private void tsiSelectedWindow_Click(object sender, EventArgs e)
        {
            ToolStripItem tsi = (ToolStripItem)sender;
            WindowInfo wi = tsi.Tag as WindowInfo;
            if (wi != null)
            {
                CaptureSelectedWindowFromList(wi.Handle);
            }
        }

        private void btnWorkflowConfig_Click(object sender, EventArgs e)
        {
            WorkflowWizardGUIOptions wfwgui = new WorkflowWizardGUIOptions()
            {
                ShowQualityTab = true,
                ShowResizeTab = true
            };
            WorkflowWizard wfw = new WorkflowWizard(new WorkerTask(Engine.Workflow), wfwgui) { Icon = this.Icon };
            wfw.Show();
        }

        private void pbActiveWindowDwmBackColor_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Engine.Workflow.ActiveWindowDwmBackColor);
        }

        private void chkActiveWindowDwmCustomColor_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkActiveWindowDwmCustomColor.Checked)
            {
                pbActiveWindowDwmBackColor.BackColor = Color.White;
                Engine.Workflow.ActiveWindowDwmBackColor = Color.White;
            }
        }
    }
}