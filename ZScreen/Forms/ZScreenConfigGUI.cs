﻿using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using HelpersLib;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Taskbar;
using ZScreenCoreLib;
using ZScreenGUI.Properties;
using ZScreenLib;

namespace ZScreenGUI
{
    public partial class ZScreen : ZScreenCoreUI
    {
        private bool _Windows7TaskbarIntegrated = false;

        private void ZScreen_Preconfig()
        {
            // Tab Image List
            tabImageList.ColorDepth = ColorDepth.Depth32Bit;
            tabImageList.Images.Add("application_form", Resources.application_form);
            tabImageList.Images.Add("server", Resources.server);
            tabImageList.Images.Add("keyboard", Resources.keyboard);
            tabImageList.Images.Add("monitor", Resources.monitor);
            tabImageList.Images.Add("picture_edit", Resources.picture_edit);
            tabImageList.Images.Add("comments", Resources.comments);
            tabImageList.Images.Add("application_edit", Resources.application_edit);
            tabImageList.Images.Add("wrench", Resources.wrench);
            tabImageList.Images.Add("info", Resources.info);
            tcMain.ImageList = tabImageList;
            tpMain.ImageKey = "application_form";
            tpHotkeys.ImageKey = "keyboard";
            tpMainInput.ImageKey = "monitor";
            tpAdvanced.ImageKey = "wrench";

            // Watermark Codes Menu
            codesMenu.AutoClose = false;
            codesMenu.Font = new XFont("Lucida Console", 8);
            codesMenu.Opacity = 0.8;
            codesMenu.ShowImageMargin = false;

            niTray.BalloonTipClicked += new EventHandler(niTray_BalloonTipClicked);

            UploadManager.ListViewControl = lvUploads;
        }

        internal void ZScreen_ConfigGUI()
        {
            StaticHelper.WriteLine("Configuring ZScreen GUI via " + new StackFrame(1).GetMethod().Name);

            DisableFeatures();

            pgAppSettings.SelectedObject = Engine.ConfigApp;
            pgAppConfig.SelectedObject = Engine.ConfigUI;
            pgWorkflow.SelectedObject = Engine.ConfigWorkflow;
            pgUploaders.SelectedObject = Engine.ConfigUploaders;

            ZScreen_ConfigGUI_Form();
            ZScreen_ConfigGUI_TrayMenu();

            ZScreen_ConfigGUI_Main();
            ZScreen_ConfigGUI_Hotkeys();
            ZScreen_ConfigGUI_Capture();
            ZScreen_ConfigGUI_Actions();
            ZScreen_ConfigGUI_Options();
            ZScreen_ConfigGUI_Options_History();
        }

        private void ZScreen_ConfigGUI_Form()
        {
            if (Engine.ConfigUI.LockFormSize)
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

        private void ZScreen_ConfigGUI_Main()
        {
            DestSelectorHelper dsh = new DestSelectorHelper(ucDestOptions);
            dsh.AddEnumDestToMenuWithConfigSettings();
            ucDestOptions.ReconfigOutputsUI();
            ucDestOptions.EnableDisableDestControls();

            chkShortenURL.Checked = Engine.ConfigUI.ShortenUrlAfterUpload;
            chkShowWorkflowWizard.Checked = Engine.ConfigUI.PromptForOutputs;
            chkShowUploadResults.Checked = Engine.ConfigUI.ShowUploadResultsWindow;
        }

        private void ZScreen_ConfigGUI_Hotkeys()
        {
            InitHotkeys();
            hmHotkeys.PrepareHotkeys(HotkeyManager);
        }

        private void ZScreen_ConfigGUI_Capture()
        {
            chkShowCursor.Checked = Engine.ConfigWorkflow.DrawCursor;

            ZScreen_ConfigGUI_Capture_CropShot();

            // Selected Window
            if (cbSelectedWindowStyle.Items.Count == 0)
            {
                cbSelectedWindowStyle.Items.AddRange(typeof(RegionStyles).GetEnumDescriptions());
            }

            cbSelectedWindowStyle.SelectedIndex = (int)Engine.ConfigUI.SelectedWindowRegionStyles;
            cbSelectedWindowRectangleInfo.Checked = Engine.ConfigUI.SelectedWindowRectangleInfo;
            cbSelectedWindowRuler.Checked = Engine.ConfigUI.SelectedWindowRuler;
            pbSelectedWindowBorderColor.BackColor = Engine.ConfigUI.SelectedWindowBorderArgb;
            nudSelectedWindowBorderSize.Value = Engine.ConfigUI.SelectedWindowBorderSize;
            cbSelectedWindowDynamicBorderColor.Checked = Engine.ConfigUI.SelectedWindowDynamicBorderColor;
            nudSelectedWindowRegionInterval.Value = Engine.ConfigUI.SelectedWindowRegionInterval;
            nudSelectedWindowRegionStep.Value = Engine.ConfigUI.SelectedWindowRegionStep;
            nudSelectedWindowHueRange.Value = Engine.ConfigUI.SelectedWindowHueRange;
            chkSelectedWindowCaptureObjects.Checked = Engine.ConfigUI.SelectedWindowCaptureObjects;

            // Active Window
            if (cboCaptureEngine.Items.Count == 0)
            {
                foreach (CaptureEngineType engine in Enum.GetValues(typeof(CaptureEngineType)))
                {
                    if (!(engine == CaptureEngineType.DWM && !Engine.HasVista))
                    {
                        cboCaptureEngine.Items.Add(engine.GetDescription());
                    }
                }
                if (!Engine.HasVista && Engine.ConfigWorkflow.CaptureEngineMode2 == CaptureEngineType.DWM)
                {
                    Engine.ConfigWorkflow.CaptureEngineMode2 = CaptureEngineType.GDI;
                }
                cboCaptureEngine.SelectedIndex = (int)Engine.ConfigWorkflow.CaptureEngineMode2;
            }

            chkActiveWindowCleanBackground.Checked = Engine.ConfigWorkflow.ActiveWindowClearBackground;
            chkSelectedWindowIncludeShadow.Checked = Engine.ConfigWorkflow.ActiveWindowIncludeShadows;
            chkActiveWindowTryCaptureChildren.Checked = Engine.ConfigWorkflow.ActiveWindowTryCaptureChildren;
            chkSelectedWindowShowCheckers.Checked = Engine.ConfigWorkflow.ActiveWindowShowCheckers;
            pbActiveWindowDwmBackColor.BackColor = Engine.ConfigWorkflow.ActiveWindowDwmBackColor;
            chkActiveWindowDwmCustomColor.Checked = Engine.ConfigWorkflow.ActiveWindowDwmUseCustomBackground;

            // Freehand Crop Shot
            cbFreehandCropShowHelpText.Checked = Engine.ConfigUI.FreehandCropShowHelpText;
            cbFreehandCropAutoUpload.Checked = Engine.ConfigUI.FreehandCropAutoUpload;
            cbFreehandCropAutoClose.Checked = Engine.ConfigUI.FreehandCropAutoClose;
            cbFreehandCropShowRectangleBorder.Checked = Engine.ConfigUI.FreehandCropShowRectangleBorder;
            pgSurfaceConfig.SelectedObject = Engine.ConfigUI.SurfaceConfig;
        }

        private void ZScreen_ConfigGUI_Capture_CropShot()
        {
            if (cboCropEngine.Items.Count == 0)
            {
                cboCropEngine.Items.AddRange(typeof(CropEngineType).GetEnumDescriptions());
                cboCropEngine.SelectedIndex = (int)Engine.ConfigUI.CropEngineMode;
            }

            // Crop Region Settings
            if (chkCropStyle.Items.Count == 0)
            {
                chkCropStyle.Items.AddRange(typeof(RegionStyles).GetEnumDescriptions());
            }
            chkCropStyle.SelectedIndex = (int)Engine.ConfigUI.CropRegionStyles;
            chkRegionRectangleInfo.Checked = Engine.ConfigUI.CropRegionRectangleInfo;
            chkRegionHotkeyInfo.Checked = Engine.ConfigUI.CropRegionHotkeyInfo;

            // Crosshair Settings
            chkCropDynamicCrosshair.Checked = Engine.ConfigUI.CropDynamicCrosshair;
            nudCropCrosshairInterval.Value = Engine.ConfigUI.CropInterval;
            nudCropCrosshairStep.Value = Engine.ConfigUI.CropStep;
            nudCrosshairLineCount.Value = Engine.ConfigUI.CrosshairLineCount;
            nudCrosshairLineSize.Value = Engine.ConfigUI.CrosshairLineSize;
            pbCropCrosshairColor.BackColor = Engine.ConfigUI.CropCrosshairArgb;
            chkCropShowBigCross.Checked = Engine.ConfigUI.CropShowBigCross;
            chkCropShowMagnifyingGlass.Checked = Engine.ConfigUI.CropShowMagnifyingGlass;

            // Region Settings
            cbShowCropRuler.Checked = Engine.ConfigUI.CropShowRuler;
            cbCropDynamicBorderColor.Checked = Engine.ConfigUI.CropDynamicBorderColor;
            nudCropRegionInterval.Value = Engine.ConfigUI.CropRegionInterval;
            nudCropRegionStep.Value = Engine.ConfigUI.CropRegionStep;
            nudCropHueRange.Value = Engine.ConfigUI.CropHueRange;
            pbCropBorderColor.BackColor = Engine.ConfigUI.CropBorderArgb;
            nudCropBorderSize.Value = Engine.ConfigUI.CropBorderSize;
            cbCropShowGrids.Checked = Engine.ConfigUI.CropShowGrids;

            // Grid Mode Settings
            nudScreenshotDelay.Time = Engine.ConfigUI.ScreenshotDelayTimes;
            nudScreenshotDelay.Value = Engine.ConfigUI.ScreenshotDelayTime;
            cboCropGridMode.Checked = Engine.ConfigUI.CropGridToggle;
            nudCropGridWidth.Value = Engine.ConfigUI.CropGridSize.Width;
            nudCropGridHeight.Value = Engine.ConfigUI.CropGridSize.Height;
        }

        private void ZScreen_ConfigGUI_Actions()
        {
            chkPerformActions.Checked = Engine.ConfigWorkflow.PerformActions;
            chkPerformActions.Enabled = !Engine.ConfigUI.PromptForOutputs;
            tsmEditinImageSoftware.Checked = Engine.ConfigWorkflow.PerformActions;

            if (Engine.ConfigUI.ConfigActions.ActionsApps.Count == 0)
            {
                Software editor = new Software(Engine.zImageAnnotator, Application.ExecutablePath, true, true);
                Engine.ConfigUI.ConfigActions.ActionsApps.Add(editor);
                Software effects = new Software(Engine.zImageEffects, Application.ExecutablePath, true, false);
                Engine.ConfigUI.ConfigActions.ActionsApps.Add(effects);
            }
            else
            {
                Engine.ConfigUI.ConfigActions.ActionsApps.RemoveAll(x => string.IsNullOrEmpty(x.Path) || !File.Exists(x.Path));
            }

            ImageEditorHelper.FindImageEditors();
        }

        private void ZScreen_ConfigGUI_Options()
        {
            // General

            if (IsReady && !Engine.ConfigApp.ShowInTaskbar)
            {
                this.ShowInTaskbar = Engine.ConfigApp.ShowInTaskbar;
            }

            ttZScreen.Active = Engine.ConfigUI.ShowHelpBalloonTips;

            FolderWatcher zWatcher = new FolderWatcher(this);
            zWatcher.FolderPath = Engine.ConfigUI.FolderMonitorPath;
            if (Engine.ConfigUI.FolderMonitoring)
            {
                zWatcher.StartWatching();
            }
            else
            {
                zWatcher.StopWatching();
            }

            // Monitor Clipboard
            chkMonImages.Checked = Engine.ConfigUI.MonitorImages;
            chkMonText.Checked = Engine.ConfigUI.MonitorText;
            chkMonFiles.Checked = Engine.ConfigUI.MonitorFiles;
            chkMonUrls.Checked = Engine.ConfigUI.MonitorUrls;
        }

        private void ZScreen_ConfigGUI_Options_History()
        {
        }

        private void ZScreen_ConfigGUI_TrayMenu()
        {
            if (tsmiTabs.DropDownItems.Count == 0)
            {
                foreach (TabPage tp in tcMain.TabPages)
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(tp.Text + "...");
                    tsmi.Click += new EventHandler(tsmiTab_Click);
                    tsmi.Image = tabImageList.Images[tp.ImageKey];
                    tsmi.Tag = tp.Name;
                    tsmiTabs.DropDownItems.Add(tsmi);
                }
            }
        }

        internal void ZScreen_Windows7onlyTasks()
        {
            if (!Engine.ConfigApp.Windows7TaskbarIntegration)
            {
                if (Engine.zJumpList != null)
                {
                    Engine.zJumpList.ClearAllUserTasks();
                    Engine.zJumpList.Refresh();
                }
            }
            else if (!IsDisposed && !_Windows7TaskbarIntegrated && Engine.ConfigApp.Windows7TaskbarIntegration && this.Handle != IntPtr.Zero && TaskbarManager.IsPlatformSupported && this.ShowInTaskbar)
            {
                try
                {
                    Engine.CheckFileRegistration();

                    Engine.zWindowsTaskbar = TaskbarManager.Instance;
                    Engine.zWindowsTaskbar.ApplicationId = Engine.appId;

                    Engine.zJumpList = JumpList.CreateJumpList();

                    // User Tasks
                    JumpListLink jlCropShot = new JumpListLink(Adapter.ZScreenCliPath(), "Crop Shot");
                    jlCropShot.Arguments = "-cc";
                    jlCropShot.IconReference = new IconReference(Adapter.ResourcePath, 1);
                    Engine.zJumpList.AddUserTasks(jlCropShot);

                    JumpListLink jlSelectedWindow = new JumpListLink(Adapter.ZScreenCliPath(), "Selected Window");
                    jlSelectedWindow.Arguments = "-sw";
                    jlSelectedWindow.IconReference = new IconReference(Adapter.ResourcePath, 2);
                    Engine.zJumpList.AddUserTasks(jlSelectedWindow);

                    JumpListLink jlClipboardUpload = new JumpListLink(Adapter.ZScreenCliPath(), "Clipboard Upload");
                    jlClipboardUpload.Arguments = "-cu";
                    jlClipboardUpload.IconReference = new IconReference(Adapter.ResourcePath, 3);
                    Engine.zJumpList.AddUserTasks(jlClipboardUpload);

                    JumpListLink jlHistory = new JumpListLink(Application.ExecutablePath, "Open History");
                    jlHistory.Arguments = "-hi";
                    jlHistory.IconReference = new IconReference(Adapter.ResourcePath, 4);
                    Engine.zJumpList.AddUserTasks(jlHistory);

                    // Recent Items
                    Engine.zJumpList.KnownCategoryToDisplay = JumpListKnownCategoryType.Recent;

                    // Custom Categories
                    JumpListCustomCategory paths = new JumpListCustomCategory("Paths");

                    JumpListLink imagesJumpListLink = new JumpListLink(FileSystem.GetImagesDir(), "Images");
                    imagesJumpListLink.IconReference = new IconReference(Path.Combine("%windir%", "explorer.exe"), 0);

                    JumpListLink settingsJumpListLink = new JumpListLink(Engine.SettingsDir, "Settings");
                    settingsJumpListLink.IconReference = new IconReference(Path.Combine("%windir%", "explorer.exe"), 0);

                    JumpListLink logsJumpListLink = new JumpListLink(Engine.LogsDir, "Logs");
                    logsJumpListLink.IconReference = new IconReference(Path.Combine("%windir%", "explorer.exe"), 0);

                    paths.AddJumpListItems(imagesJumpListLink, settingsJumpListLink, logsJumpListLink);
                    Engine.zJumpList.AddCustomCategories(paths);

                    // Taskbar Buttons
                    ThumbnailToolBarButton cropShot = new ThumbnailToolBarButton(Resources.shape_square_ico, "Crop Shot");
                    cropShot.Click += new EventHandler<ThumbnailButtonClickedEventArgs>(cropShot_Click);

                    ThumbnailToolBarButton selWindow = new ThumbnailToolBarButton(Resources.application_double_ico, "Selected Window");
                    selWindow.Click += new EventHandler<ThumbnailButtonClickedEventArgs>(selWindow_Click);

                    ThumbnailToolBarButton clipboardUpload = new ThumbnailToolBarButton(Resources.clipboard_upload_ico, "Clipboard Upload");
                    clipboardUpload.Click += new EventHandler<ThumbnailButtonClickedEventArgs>(clipboardUpload_Click);

                    ThumbnailToolBarButton openHistory = new ThumbnailToolBarButton(Resources.pictures_ico, "History");
                    openHistory.Click += new EventHandler<ThumbnailButtonClickedEventArgs>(OpenHistory);

                    if (!IsDisposed)
                    {
                        Engine.zWindowsTaskbar.ThumbnailToolBars.AddButtons(this.Handle, cropShot, selWindow, clipboardUpload, openHistory);
                        Engine.zJumpList.Refresh();
                    }
                    _Windows7TaskbarIntegrated = true;
                    StaticHelper.WriteLine("Integrated into Windows 7 Taskbar");
                }
                catch (Exception ex)
                {
                    StaticHelper.WriteException(ex, "Error while configuring Windows 7 Taskbar");
                }
            }

            StaticHelper.WriteLine(new StackFrame().GetMethod().Name);
        }
    }
}