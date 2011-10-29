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

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Xml.Serialization;
using GradientTester;
using GraphicsMgrLib;
using HelpersLib;
using HelpersLib.Hotkey;
using ScreenCapture;
using UploadersLib;
using UploadersLib.HelperClasses;
using ZScreenLib.Helpers;
using ZSS.IndexersLib;
using ZSS.UpdateCheckerLib;

namespace ZScreenLib
{
    [XmlRoot("Settings")]
    public class XMLSettings
    {
        #region Settings

        //~~~~~~~~~~~~~~~~~~~~~
        //  Misc Settings
        //~~~~~~~~~~~~~~~~~~~~~

        public bool FirstRun = true;

        //~~~~~~~~~~~~~~~~~~~~~
        //  Main
        //~~~~~~~~~~~~~~~~~~~~~

        public List<int> MyImageUploaders = new List<int>();
        public List<int> MyFileUploaders = new List<int>();
        public List<int> MyTextUploaders = new List<int>();
        public List<int> MyURLShorteners = new List<int>();
        public List<int> ConfClipboardContent = new List<int>();
        public List<int> ConfOutputs = new List<int>();
        public List<int> ConfLinkFormat = new List<int>();

        public long ScreenshotDelayTime = 0;
        public Times ScreenshotDelayTimes = Times.Seconds;
        public bool PromptForOutputs = false;

        [Category(ComponentModelStrings.OutputsClipboard), DefaultValue(true), Description("Show Clipboard Content Viewer before uploading Clipboard Content using the Main tab.")]
        public bool ShowClipboardContentViewer { get; set; }

        //~~~~~~~~~~~~~~~~~~~~~
        //  Destinations
        //~~~~~~~~~~~~~~~~~~~~~

        // Printer

        public PrintSettings PrintSettings = new PrintSettings();

        // TinyPic

        [Category(ComponentModelStrings.OutputsRemoteImage), DefaultValue(false), Description("Switch from TinyPic to ImageShack if the image dimensions are greater than 1600 pixels.")]
        public bool TinyPicSizeCheck { get; set; }

        // Twitter

        public bool TwitterEnabled = false;
        public TwitterClientSettings TwitterClientConfig = new TwitterClientSettings();

        //~~~~~~~~~~~~~~~~~~~~~
        //  Hotkeys
        //~~~~~~~~~~~~~~~~~~~~~
        public HotkeySetting HotkeyEntireScreen2 = new HotkeySetting(Keys.PrintScreen);
        public HotkeySetting HotkeyActiveWindow2 = new HotkeySetting(Keys.Alt | Keys.PrintScreen);
        public HotkeySetting HotkeyRectangleRegion2 = new HotkeySetting(Keys.Control | Keys.PrintScreen);

        public HotkeySetting RectangleRegionLast2 = new HotkeySetting();
        public HotkeySetting HotkeySelectedWindow2 = new HotkeySetting(Keys.Shift | Keys.PrintScreen);
        public HotkeySetting HotkeyFreeHandRegion2 = new HotkeySetting(Keys.Control | Keys.Shift | Keys.PrintScreen);

        public HotkeySetting HotkeyClipboardUpload2 = new HotkeySetting(Keys.Control | Keys.PageUp);
        public HotkeySetting HotkeyAutoCapture2 = new HotkeySetting();
        public HotkeySetting HotkeyDropWindow2 = new HotkeySetting();

        public HotkeySetting HotkeyScreenColorPicker2 = new HotkeySetting();
        public HotkeySetting HotkeyTwitterClient2 = new HotkeySetting();
        public HotkeySetting HotkeyCaptureRectangeRegionClipboard2 = new HotkeySetting(Keys.Control | Keys.Alt | Keys.PrintScreen);

        //~~~~~~~~~~~~~~~~~~~~~
        //  Capture
        //~~~~~~~~~~~~~~~~~~~~~

        // Crop Shot

        public RegionStyles CropRegionStyles = RegionStyles.BACKGROUND_REGION_BRIGHTNESS;
        public bool CropRegionRectangleInfo = true;
        public bool CropRegionHotkeyInfo = true;

        public bool CropDynamicCrosshair = true;
        public int CropInterval = 25;
        public int CropStep = 1;
        public int CrosshairLineCount = 2;
        public int CrosshairLineSize = 25;
        public XColor CropCrosshairArgb = Color.Black;
        public bool CropShowBigCross = true;
        public bool CropShowMagnifyingGlass = true;

        public bool CropShowRuler = true;
        public bool CropDynamicBorderColor = true;
        public decimal CropRegionInterval = 75;
        public decimal CropRegionStep = 5;
        public decimal CropHueRange = 50;
        public XColor CropBorderArgb = Color.FromArgb(255, 0, 255);
        public decimal CropBorderSize = 1;
        public bool CropShowGrids = false;

        public Rectangle LastRegion = Rectangle.Empty;
        public Rectangle LastCapture = Rectangle.Empty;

        [Category(ComponentModelStrings.Screenshots), DefaultValue(false), Description("Make the corners rounded")]
        public bool ImageAddRoundedCorners { get; set; }

        [Category(ComponentModelStrings.Screenshots), DefaultValue(false), Description("Add a shadow (if the screenshot is big enough)")]
        public bool ImageAddShadow { get; set; }

        public bool CropGridToggle = false;
        public Size CropGridSize = new Size(100, 100);

        // Selected Window

        public RegionStyles SelectedWindowRegionStyles = RegionStyles.REGION_BRIGHTNESS;
        public bool SelectedWindowRectangleInfo = true;
        public bool SelectedWindowRuler = true;
        public XColor SelectedWindowBorderArgb = Color.FromArgb(255, 0, 255);
        public decimal SelectedWindowBorderSize = 2;
        public bool SelectedWindowDynamicBorderColor = true;
        public decimal SelectedWindowRegionInterval = 75;
        public decimal SelectedWindowRegionStep = 5;
        public decimal SelectedWindowHueRange = 50;
        public bool SelectedWindowCaptureObjects = true;

        // Freehand Crop Shot

        public bool FreehandCropShowHelpText = true;
        public bool FreehandCropAutoUpload = false;
        public bool FreehandCropAutoClose = false;
        public bool FreehandCropShowRectangleBorder = false;

        public SurfaceOptions SurfaceConfig = new SurfaceOptions();

        // Interaction

        public bool CompleteSound = true;
        public bool ShowBalloonTip = true;
        public bool BalloonTipOpenLink = true;
        public bool ShowUploadDuration = true;
        public decimal FlashTrayCount = 2;
        public bool CaptureEntireScreenOnError = false;
        public bool CloseDropBox = false;
        public Point LastDropBoxPosition = Point.Empty;

        [Category(ComponentModelStrings.FileNaming), DefaultValue(false), Description("Prompt to save the image in a different location")]
        public bool ShowSaveFileDialogImages { get; set; }

        public bool MakeJPGBackgroundWhite = true;

        //~~~~~~~~~~~~~~~~~~~~~
        //  Editors
        //~~~~~~~~~~~~~~~~~~~~~
        public List<Software> ActionsApps = new List<Software>();
        public Software ImageEditor = null;

        //~~~~~~~~~~~~~~~~~~~~~
        //  HTTP
        //~~~~~~~~~~~~~~~~~~~~~

        // Image Uploaders
        [Category(ComponentModelStrings.OutputsRemoteImage), DefaultValue(true), Description("Automatically switch to File Uploader if a user copies (Clipboard Upload) or drags a non-Image.")]
        public bool AutoSwitchFileUploader { get; set; }

        [Category(ComponentModelStrings.OutputsRemoteImage), DefaultValue(2), Description("Number of Retries if image uploading fails.")]
        public int ErrorRetryCount { get; set; }

        [Category(ComponentModelStrings.OutputsRemoteImage), DefaultValue(false), Description("Retry with between TinyPic and ImageShack if the TinyPic or ImageShack fails the first attempt.")]
        public bool ImageUploadRetryOnFail { get; set; }

        [Category(ComponentModelStrings.OutputsRemoteImage), DefaultValue(false), Description("Randomly select a valid destination when instead of retrying between ImageShack and TinyPic.")]
        public bool ImageUploadRandomRetryOnFail { get; set; }

        [Category(ComponentModelStrings.OutputsRemoteImage), DefaultValue(false), Description("Retry with another Image Uploader if the Image Uploader fails the first attempt.")]
        public bool ImageUploadRetryOnTimeout { get; set; }

        [Category(ComponentModelStrings.OutputsRemoteImage), DefaultValue(30000), Description("Change the Image Uploader if the upload times out by this amount of milliseconds.")]
        public int UploadDurationLimit { get; set; }

        // Indexer

        public IndexerConfig IndexerConfig = new IndexerConfig();

        //~~~~~~~~~~~~~~~~~~~~~
        //  History
        //~~~~~~~~~~~~~~~~~~~~~

        public int HistoryMaxNumber = 100;
        public bool HistorySave = true;

        //~~~~~~~~~~~~~~~~~~~~~
        //  Options
        //~~~~~~~~~~~~~~~~~~~~~

        // General - Program
        public bool ShowHelpBalloonTips = true;

        [Category(ComponentModelStrings.App), DefaultValue(false), Description("Lock Main Window size to the minimum possible size and disable resizing.")]
        public bool LockFormSize { get; set; }

        public bool AutoSaveSettings = true;

        // General - Monitor Clipboard

        public bool MonitorImages = false;
        public bool MonitorText = false;
        public bool MonitorFiles = false;
        public bool MonitorUrls = false;

        // General - Check Updates

        public bool CheckUpdates = true;
        public bool CheckUpdatesBeta = false;
        public ReleaseChannelType ReleaseChannel = ReleaseChannelType.Stable;

        //~~~~~~~~~~~~~~~
        // Proxy Settings
        //~~~~~~~~~~~~~~~

        public List<ProxyInfo> ProxyList = new List<ProxyInfo>();
        public int ProxySelected = 0;
        public ProxyInfo ProxyActive = null;
        public ProxyConfigType ProxyConfig = ProxyConfigType.NoProxy;

        //~~~~~~~~~
        // Paths
        //~~~~~~~~~

        public bool DeleteLocal = false;

        //~~~~~~~~~~~~~~~~~~~~~
        //  Auto Capture
        //~~~~~~~~~~~~~~~~~~~~~

        public AutoScreenshotterJobs AutoCaptureScreenshotTypes = AutoScreenshotterJobs.TAKE_SCREENSHOT_SCREEN;
        public long AutoCaptureDelayTime = 10000;
        public Times AutoCaptureDelayTimes = Times.Seconds;
        public bool AutoCaptureAutoMinimize = false;
        public bool AutoCaptureWaitUploads = true;

        [Category(ComponentModelStrings.InputsAutoCapture), DefaultValue(false), Description("Automatically start capturing after loading Auto Capture")]
        public bool AutoCaptureExecute { get; set; }

        #region Properties for PropertyGrid

        public XMLSettings()
        {
            ApplyDefaultValues(this);
        }

        // Destinations / FTP

        [Category(ComponentModelStrings.OutputsRemoteFTP), DefaultValue(50), Description("Screenshots cache size in MiB for the FTP Client.")]
        public int ScreenshotCacheSize { get; set; }

        [Category(ComponentModelStrings.OutputsRemoteFTP), DefaultValue(false), Description("Allows you to choose the FTP account before uploading.")]
        public bool ShowFTPSettingsBeforeUploading { get; set; }

        // Options / General

        [Category(ComponentModelStrings.OutputsClipboard), DefaultValue(false), Description("Show Clipboard Mode Chooser after upload is complete")]
        public bool ShowUploadResultsWindow { get; set; }

        [Category(ComponentModelStrings.App), DefaultValue(true), Description("Showing upload progress percentage in tray icon")]
        public bool ShowTrayUploadProgress { get; set; }

        [Category(ComponentModelStrings.App), DefaultValue(true), Description("Write debug information into a log file.")]
        public bool WriteDebugFile { get; set; }

        [Category(ComponentModelStrings.App), DefaultValue(false), Description("Use SetProcessWorkingSetSize when ZScreen window is closed (minimized to tray) or idle.")]
        public bool EnableAutoMemoryTrim { get; set; }

        [Category(ComponentModelStrings.App), DefaultValue(false), Description("Enables keyboard hook timer which reactivating keyboard hook every 5 seconds.")]
        public bool EnableKeyboardHookTimer { get; set; }

        // Options / Interaction

        [Category(ComponentModelStrings.InputsURLShorteners), DefaultValue(true),
        Description("If you use Clipboard Upload and the clipboard contains a URL then the URL will be shortened instead of performing a text upload.")]
        public bool ShortenUrlUsingClipboardUpload { get; set; }

        [Category(ComponentModelStrings.InputsURLShorteners), DefaultValue(80),
        Description("ShortenUrlAfterUpload will only be activated if the length of a URL exceeds this value. To always shorten a URL set this value to 0.")]
        public int ShortenUrlAfterUploadAfter { get; set; }

        [Category(ComponentModelStrings.InputsURLShorteners), DefaultValue(false), Description("Optionally shorten the URL after completing a task.")]
        public bool ShortenUrlAfterUpload { get; set; }

        [Category(ComponentModelStrings.OutputsClipboard), DefaultValue("%link"), Description("Customizes clipboard text \n Valid Variables: %link, %size, %name and any in (Options -> File Naming)")]
        public string ClipboardFormat { get; set; }

        [Category(ComponentModelStrings.OutputsClipboard), DefaultValue(false), Description("Enable ClipboardFormat for shortened URLs generated for long URLs captured using Clipboard Monitor or Clipboard Upload. This means that ClipboardFormat will still continue to work for shortened URLs of screenshot captures.")]
        public bool EnableClipboardFormatForLongURLs { get; set; }

        [Category(ComponentModelStrings.OutputsClipboard), DefaultValue(false), Description("When multiple upload locations are configured in Outputs, application will append each URL to clipboard.")]
        public bool ClipboardAppendMultipleLinks { get; set; }

        // Options / Paths

        [Category(ComponentModelStrings.AppPaths), DefaultValue(true), Description("Periodically backup application settings.")]
        public bool BackupApplicationSettings { get; set; }

        [Category(ComponentModelStrings.AppPaths), Description("Custom Images directory where screenshots and pictures will be stored locally.")]
        [EditorAttribute(typeof(FolderNameEditor), typeof(UITypeEditor))]
        public string CustomImagesDir { get; set; }

        [Category(ComponentModelStrings.AppPaths), DefaultValue(false), Description("Use Custom Images directory.")]
        public bool UseCustomImagesDir { get; set; }

        // Options / Watch Folder

        [Category(ComponentModelStrings.InputsWatchFolder), DefaultValue(false), Description("Automatically upload files saved in to this folder.")]
        public bool FolderMonitoring { get; set; }

        [Category(ComponentModelStrings.InputsWatchFolder), Description("Folder monitor path where files automatically get uploaded.")]
        [EditorAttribute(typeof(FolderNameEditor), typeof(UITypeEditor))]
        public string FolderMonitorPath { get; set; }

        // Screenshots / General

        [Category(ComponentModelStrings.Screenshots), DefaultValue(-10), Description("Region style setting. Must be between these values: -100, 100")]
        public int BackgroundRegionBrightnessValue { get; set; }

        [Category(ComponentModelStrings.Screenshots), DefaultValue(100), Description("Region style setting. Must be between these values: 0, 255")]
        public int BackgroundRegionTransparentValue { get; set; }

        [Category(ComponentModelStrings.Screenshots), DefaultValue(false), Description("Show output to the user as soon as at least one output is ready e.g. copy image to clipboard until URL is retrieved.")]
        public bool ShowOutputsAsap { get; set; }

        [Category(ComponentModelStrings.ScreenshotsRegion), DefaultValue(15), Description("Region style setting. Must be between these values: -100, 100")]
        public int RegionBrightnessValue { get; set; }

        [Category(ComponentModelStrings.ScreenshotsRegion), DefaultValue(75), Description("Region style setting. Must be between these values: 0, 255")]
        public int RegionTransparentValue { get; set; }

        [Category(ComponentModelStrings.Screenshots), DefaultValue(CropEngineType.Cropv1), Description("Choose the method of Crop")]
        public CropEngineType CropEngineMode { get; set; }

        [Category(ComponentModelStrings.Screenshots), DefaultValue(false), Description("Don't display the crosshair and use the cross mouse cursor instead.")]
        public bool UseHardwareCursor { get; set; }

        #endregion Properties for PropertyGrid

        #endregion Settings

        #region I/O Methods

        public bool Write()
        {
            return Write(Engine.SettingsFilePath);
        }

        public bool Write(string filePath)
        {
            return SettingsHelper.Save(this, filePath, SerializationType.Xml);
        }

        public static XMLSettings Read()
        {
            string settingsFile = Engine.IsPortable ? Engine.GetPreviousSettingsFile(Engine.SettingsDir) : Engine.SettingsFilePath;
            if (!File.Exists(settingsFile))
            {
                if (File.Exists(Engine.ConfigApp.XMLSettingsPath))
                {
                    // Step 2 - Attempt to read previous Application Version specific Settings file
                    settingsFile = Engine.ConfigApp.XMLSettingsPath;
                }
                else
                {
                    // Step 3 - Attempt to read conventional Settings file
                    settingsFile = Engine.SettingsFilePath;
                }
                StaticHelper.WriteLine("Using " + settingsFile);
            }

            if (File.Exists(settingsFile) && settingsFile != Engine.SettingsFilePath)
            {
                File.Copy(settingsFile, Engine.SettingsFilePath);                 // Update AppSettings.xml
            }

            Engine.ConfigApp.XMLSettingsPath = Engine.SettingsFilePath;

            return Read(Engine.ConfigApp.XMLSettingsPath);
        }

        public static XMLSettings Read(string filePath)
        {
            return SettingsHelper.Load<XMLSettings>(filePath, SerializationType.Xml);
        }

        #endregion I/O Methods

        #region Other methods

        public static void ApplyDefaultValues(object self)
        {
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(self))
            {
                DefaultValueAttribute attr = prop.Attributes[typeof(DefaultValueAttribute)] as DefaultValueAttribute;
                if (attr == null) continue;
                prop.SetValue(self, attr.Value);
            }
        }

        public object GetFieldValue(string name)
        {
            FieldInfo fieldInfo = this.GetType().GetField(name);
            if (fieldInfo != null) return fieldInfo.GetValue(this);
            return null;
        }

        public bool SetFieldValue(string name, object value)
        {
            FieldInfo fieldInfo = this.GetType().GetField(name);
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(this, value);
                return true;
            }
            return false;
        }

        #endregion Other methods
    }
}