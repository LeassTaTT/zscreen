﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Xml.Serialization;
using FreeImageNetLib;
using GradientTester;
using GraphicsMgrLib;
using HelpersLib;
using HelpersLib.Hotkey;
using UploadersLib;
using ZScreenLib.Helpers;

namespace ZScreenLib
{
    [Serializable]
    public class Workflow
    {
        #region Constructors

        public Workflow()
        {
            this.ID = ZAppHelper.GetRandomAlphanumeric(12);
            this.Description = "New Workflow";
            this.Enabled = true;
            ApplyDefaultValues(this);
        }

        public Workflow(string name)
            : this()
        {
            this.Description = name;
        }

        #endregion Constructors

        #region Properties

        [Browsable(false)]
        public string ID { get; set; }

        [Browsable(false)]
        public string Description { get; set; }

        [Browsable(false)]
        public bool Enabled { get; set; }

        [Browsable(false)]
        public WorkerTask.JobLevel2 Job { get; set; }

        public CaptureEngineType CaptureEngineMode2 = CaptureEngineType.GDI;

        private bool bPerformActions = false;

        public bool PerformActions
        {
            get
            {
                return bPerformActions ||
                 System.Windows.Forms.Control.IsKeyLocked(System.Windows.Forms.Keys.CapsLock) && ImageEditorOnKeyPress == EImageEditorOnKeyPress.CapsLock ||
                 System.Windows.Forms.Control.IsKeyLocked(System.Windows.Forms.Keys.NumLock) && ImageEditorOnKeyPress == EImageEditorOnKeyPress.NumLock ||
                 System.Windows.Forms.Control.IsKeyLocked(System.Windows.Forms.Keys.Scroll) && ImageEditorOnKeyPress == EImageEditorOnKeyPress.ScrollLock;
            }
            set
            {
                bPerformActions = value;
            }
        }

        public bool DrawCursor = false;

        public HotkeySetting Hotkey = new HotkeySetting();

        #endregion Properties

        #region Inputs / Active Window

        public bool ActiveWindowTryCaptureChildren = false;
        public bool ActiveWindowClearBackground = true;
        public bool ActiveWindowIncludeShadows = true;
        public bool ActiveWindowShowCheckers = false;
        public bool ActiveWindowDwmUseCustomBackground = false;
        public XColor ActiveWindowDwmBackColor = Color.White;

        [Category(ComponentModelStrings.ScreenshotsActiveWindow), DefaultValue(false), Description("Freeze active window during capture. WARNING: Do not try this on a Windows process.")]
        public bool ActiveWindowGDIFreezeWindow { get; set; }

        #endregion Inputs / Active Window

        #region Inputs / File Upload

        // Inputs / Animated Images

        [Category(ComponentModelStrings.InputsAnimatedImages), DefaultValue(AnimatedImageFormat.GIF), Description("Animated image type.")]
        public AnimatedImageFormat ImageFormatAnimated { get; set; }

        [Category(ComponentModelStrings.InputsAnimatedImages), DefaultValue(10), Description("Maximum number of frames per animated image. Images will be uploaded individually after this value.")]
        public int ImageAnimatedFramesMax { get; set; }

        [Category(ComponentModelStrings.InputsAnimatedImages), DefaultValue(1), Description("Delay in seconds between each frame of the animated image.")]
        public int ImageAnimatedFramesDelay { get; set; }

        #endregion Inputs / File Upload

        #region Image Manipulation

        // Image Editor
        [Category(ComponentModelStrings.Screenshots), DefaultValue(EImageEditorOnKeyPress.None), Description("Automatically start Image Editor on a key press.")]
        public EImageEditorOnKeyPress ImageEditorOnKeyPress { get; set; }

        // Image Settings

        public EImageFormat ImageFormat = EImageFormat.PNG;
        public FreeImageJpegQualityType ImageJpegQuality = FreeImageJpegQualityType.JPEG_QUALITYSUPERB;
        public FreeImageJpegSubSamplingType ImageJpegSubSampling = FreeImageJpegSubSamplingType.JPEG_SUBSAMPLING_444;
        public FreeImagePngQuality ImagePngCompression = FreeImagePngQuality.PNG_Z_DEFAULT_COMPRESSION;
        public bool ImagePngInterlaced = false;
        public GIFQuality ImageGIFQuality = GIFQuality.Default;
        public FreeImageTiffQuality ImageTiffCompression = FreeImageTiffQuality.TIFF_NONE;
        public int ImageSizeLimit = 512;
        public EImageFormat ImageFormat2 = EImageFormat.JPEG;

        // Image Effects

        public ImageSizeType ImageSizeType = ImageSizeType.DEFAULT;
        public int ImageSizeFixedWidth = 500;
        public int ImageSizeFixedHeight = 500;
        public float ImageSizeRatioPercentage = 50.0f;

        // Screenshots / Bevel

        [Category(ComponentModelStrings.ScreenshotsBevel), DefaultValue(false), Description("Add bevel effect to screenshots.")]
        public bool BevelEffect { get; set; }

        [Category(ComponentModelStrings.ScreenshotsBevel), DefaultValue(15), Description("Bevel effect size.")]
        public int BevelEffectOffset { get; set; }

        [Category(ComponentModelStrings.ScreenshotsBevel), DefaultValue(FilterType.Brightness), Description("Bevel effect filter type.")]
        public FilterType BevelFilterType { get; set; }

        // Screenshots / Reflection

        [Category(ComponentModelStrings.ScreenshotsReflection), DefaultValue(false), Description("Draw reflection bottom of screenshots.")]
        public bool DrawReflection { get; set; }

        [Category(ComponentModelStrings.ScreenshotsReflection), DefaultValue(0), Description("Reflection position will start: Screenshot height + Offset")]
        public int ReflectionOffset { get; set; }

        [Category(ComponentModelStrings.ScreenshotsReflection), DefaultValue(20), Description("Reflection height size relative to screenshot height.")]
        public int ReflectionPercentage { get; set; }

        [Category(ComponentModelStrings.ScreenshotsReflection), DefaultValue(true), Description("Adding skew to reflection from bottom left to bottom right.")]
        public bool ReflectionSkew { get; set; }

        [Category(ComponentModelStrings.ScreenshotsReflection), DefaultValue(25), Description("How much pixel skew left to right.")]
        public int ReflectionSkewSize { get; set; }

        [Category(ComponentModelStrings.ScreenshotsReflection), DefaultValue(255), Description("Reflection transparency start from this value to 0.")]
        public int ReflectionTransparency { get; set; }

        //Screenshots / Border

        [Category(ComponentModelStrings.ScreenshotsBorder), DefaultValue(false), Description("Add border to screenshots.")]
        public bool BorderEffect { get; set; }

        [Category(ComponentModelStrings.ScreenshotsBorder), DefaultValue(1), Description("Border size in px.")]
        public int BorderEffectSize { get; set; }

        [XmlIgnore(), Category(ComponentModelStrings.ScreenshotsBorder), Description("Border Color.")]
        public Color BorderEffectColor { get; set; }

        [XmlIgnore(), Category(ComponentModelStrings.OutputsClipboard), Description("Background color of images captured to clipboard.")]
        public Color ClipboardBackgroundColor { get; set; }

        [XmlElement("BorderEffectColor"), BrowsableAttribute(false)]
        public XColor BorderEffectArgb
        {
            get
            {
                return BorderEffectColor;
            }
            set
            {
                BorderEffectColor = value;
            }
        }

        [XmlElement("ClipboardBackgroundColor"), BrowsableAttribute(false)]
        public XColor ClipboardBackgroundArgb
        {
            get
            {
                return ClipboardBackgroundColor;
            }
            set
            {
                ClipboardBackgroundColor = value;
            }
        }

        //~~~~~~~~~~~~~~~~~~~~~
        //  Watermark
        //~~~~~~~~~~~~~~~~~~~~~

        public WatermarkType WatermarkMode = WatermarkType.NONE;
        public WatermarkPositionType WatermarkPositionMode = WatermarkPositionType.BOTTOM_RIGHT;
        public decimal WatermarkOffset = 5;
        public bool WatermarkAddReflection = false;
        public bool WatermarkAutoHide = true;

        [Category(ComponentModelStrings.InputsClipboard), DefaultValue(false), Description("Do not apply watermark during Clipboard Upload")]
        public bool WatermarkExcludeClipboardUpload { get; set; }

        public string WatermarkText = "%h:%mi";
        public XFont WatermarkFont = new XFont("Arial", 8);
        public XColor WatermarkFontArgb = Color.White;
        public decimal WatermarkFontTrans = 255;
        public decimal WatermarkCornerRadius = 4;
        public XColor WatermarkGradient1Argb = Color.FromArgb(85, 85, 85);
        public XColor WatermarkGradient2Argb = Color.Black;
        public XColor WatermarkBorderArgb = Color.Black;
        public decimal WatermarkBackTrans = 225;
        public LinearGradientMode WatermarkGradientType = LinearGradientMode.Vertical;
        public bool WatermarkUseCustomGradient = false;
        public GradientMakerSettings GradientMakerOptions = new GradientMakerSettings();

        public string WatermarkImageLocation = "";
        public bool WatermarkUseBorder = false;
        public decimal WatermarkImageScale = 100;

        #endregion Image Manipulation

        #region File Naming

        public string ActiveWindowPattern = "%t-%y-%mo-%d_%h.%mi.%s";
        public string EntireScreenPattern = "Screenshot-%y-%mo-%d_%h.%mi.%s";
        public string SaveFolderPattern = "%y-%mo";
        public int MaxNameLength = 100;

        [Category(ComponentModelStrings.FileNaming), DefaultValue(0), Description("Adjust the current Auto-Increment number.")]
        public int AutoIncrement { get; set; }

        [Category(ComponentModelStrings.FileNaming), DefaultValue(false), Description("Overwrite existing file without creating new files.")]
        public bool OverwriteFiles = false;

        #endregion File Naming

        #region Outputs

        [Category(ComponentModelStrings.OutputsClipboard), DefaultValue(true), Description("Always overwrite the clipboard with the screenshot image or url.")]
        public bool ClipboardOverwrite { get; set; }

        public DestConfig DestConfig = new DestConfig();

        #endregion Outputs

        #region Sound Settings

        [Category(ComponentModelStrings.SoundSettings), DefaultValue(false), Description("Enable custom sounds when upload completed.")]
        public bool EnableSounds { get; set; }

        [Category(ComponentModelStrings.SoundSettings), Description("Location of .wav file.")]
        [EditorAttribute(typeof(SoundFileNameEditor), typeof(UITypeEditor))]
        public string SoundPath { get; set; }

        [Category(ComponentModelStrings.SoundSettings), DefaultValue(false), Description("Enable sound when screenshot is taken.")]
        public bool EnableImageSound { get; set; }

        [Category(ComponentModelStrings.SoundSettings), Description("Location of .wav file.\nIf no sound is selected, a default camera click will play")]
        [EditorAttribute(typeof(SoundFileNameEditor), typeof(UITypeEditor))]
        public string SoundImagePath { get; set; }

        #endregion Sound Settings

        #region I/O Methods

        public bool Write(string filePath)
        {
            return SettingsHelper.Save<Workflow>(this, filePath, SerializationType.Xml);
        }

        public static Workflow Read(string filePath)
        {
            // Encrypt passwords
            return SettingsHelper.Load<Workflow>(filePath, SerializationType.Xml);
        }

        #endregion I/O Methods

        #region Helper Methods

        public static void ApplyDefaultValues(object self)
        {
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(self))
            {
                DefaultValueAttribute attr = prop.Attributes[typeof(DefaultValueAttribute)] as DefaultValueAttribute;
                if (attr == null) continue;
                prop.SetValue(self, attr.Value);
            }
        }

        public void Start()
        {
        }

        #endregion Helper Methods
    }
}