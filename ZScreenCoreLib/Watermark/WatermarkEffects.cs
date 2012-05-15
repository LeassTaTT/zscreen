﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;
using GradientTester;
using HelpersLib;
using HelpersLib.GraphicsHelper;

namespace ZScreenCoreLib
{
    public class WatermarkEffects : ScreenshotEffectsHelper
    {
        #region 0 Constructors

        public WatermarkEffects(WatermarkConfig Config)
        {
            this.Config = Config;
        }

        #endregion 0 Constructors

        #region 0 Properties

        WatermarkConfig Config = new WatermarkConfig();

        #endregion 0 Properties

        public static Bitmap AddReflection(Image bmp, int percentage, int transparency)
        {
            Bitmap b = new Bitmap(bmp);
            b.RotateFlip(RotateFlipType.RotateNoneFlipY);
            b = b.Clone(new Rectangle(0, 0, b.Width, (int)(b.Height * ((float)percentage / 100))), PixelFormat.Format32bppArgb);
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            byte alpha;
            int nOffset = bmData.Stride - b.Width * 4;
            transparency.Mid(0, 255);

            unsafe
            {
                byte* p = (byte*)(void*)bmData.Scan0;

                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < b.Width; ++x)
                    {
                        alpha = (byte)(transparency - transparency * (y + 1) / b.Height);
                        if (p[3] > alpha) p[3] = alpha;
                        p += 4;
                    }
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);

            return b;
        }

        /// <summary>Get Image with Watermark</summary>
        public Image ApplyWatermark(Image img, NameParser parser = null)
        {
            if (parser == null)
            {
                parser = new NameParser(NameParserType.Watermark) { IsPreview = true, Picture = img };
            }
            return ApplyWatermark(img, parser, Config.WatermarkMode);
        }

        /// <summary>Get Image with Watermark</summary>
        public Image ApplyWatermark(Image img, NameParser parser, WatermarkType watermarkType)
        {
            switch (watermarkType)
            {
                default:
                case WatermarkType.NONE:
                    return img;
                case WatermarkType.TEXT:
                    return DrawWatermark(img, parser.Convert(Config.WatermarkText));
                case WatermarkType.IMAGE:
                    return DrawImageWatermark(img, Config.WatermarkImageLocation);
            }
        }

        private Image DrawImageWatermark(Image img, string imgPath)
        {
            try
            {
                if (!string.IsNullOrEmpty(imgPath) && File.Exists(imgPath))
                {
                    int offset = (int)Config.WatermarkOffset;
                    Image img2 = Image.FromFile(imgPath);
                    img2 = HelpersLib.GraphicsHelper.Core.ChangeImageSize((Bitmap)img2, (float)Config.WatermarkImageScale);
                    Point imgPos = FindPosition(Config.WatermarkPositionMode, offset, img.Size, img2.Size, 0);
                    if (Config.WatermarkAutoHide && ((img.Width < img2.Width + offset) ||
                        (img.Height < img2.Height + offset)))
                    {
                        return img;
                        //throw new Exception("Image size smaller than watermark size.");
                    }

                    Graphics g = Graphics.FromImage(img);
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.DrawImage(img2, imgPos);
                    if (Config.WatermarkAddReflection)
                    {
                        Bitmap bmp = AddReflection((Bitmap)img2, 50, 200);
                        g.DrawImage(bmp, new Rectangle(imgPos.X, imgPos.Y + img2.Height - 1, bmp.Width, bmp.Height));
                    }
                    if (Config.WatermarkUseBorder)
                    {
                        g.DrawRectangle(new Pen(Color.Black), new Rectangle(imgPos.X, imgPos.Y, img2.Width - 1, img2.Height - 1));
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException(ex, "Error while drwaing image watermark");
            }
            return img;
        }

        private Image DrawWatermark(Image img, string drawText)
        {
            if (!string.IsNullOrEmpty(drawText))
            {
                if (0 == Config.WatermarkFont.Size)
                {
                    ZScreenCoreHelper.ShowFontDialog(Config);
                }
                try
                {
                    int offset = (int)Config.WatermarkOffset;
                    Font font = Config.WatermarkFont;
                    Size textSize = TextRenderer.MeasureText(drawText, font);
                    Size labelSize = new Size(textSize.Width + 10, textSize.Height + 10);
                    Point labelPosition = FindPosition(Config.WatermarkPositionMode, offset, img.Size,
                        new Size(textSize.Width + 10, textSize.Height + 10), 1);
                    if (Config.WatermarkAutoHide && ((img.Width < labelSize.Width + offset) ||
                        (img.Height < labelSize.Height + offset)))
                    {
                        return img;
                        //throw new Exception("Image size smaller than watermark size.");
                    }
                    Rectangle labelRectangle = new Rectangle(Point.Empty, labelSize);
                    GraphicsPath gPath = GraphicsEx.GetRoundedRectangle(labelRectangle, (int)Config.WatermarkCornerRadius);

                    int backTrans = (int)Config.WatermarkBackTrans;
                    int fontTrans = (int)Config.WatermarkFontTrans;
                    Color fontColor = Config.WatermarkFontArgb;
                    Bitmap bmp = new Bitmap(labelRectangle.Width + 1, labelRectangle.Height + 1);
                    Graphics g = Graphics.FromImage(bmp);
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                    LinearGradientBrush brush;
                    if (Config.WatermarkUseCustomGradient)
                    {
                        brush = GradientMaker.CreateGradientBrush(labelRectangle.Size, Config.GradientMakerOptions.GetBrushDataActive());
                    }
                    else
                    {
                        brush = new LinearGradientBrush(labelRectangle, Color.FromArgb(backTrans, Config.WatermarkGradient1Argb),
                            Color.FromArgb(backTrans, Config.WatermarkGradient2Argb), Config.WatermarkGradientType);
                    }
                    g.FillPath(brush, gPath);
                    g.DrawPath(new Pen(Color.FromArgb(backTrans, Config.WatermarkBorderArgb)), gPath);
                    StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                    g.DrawString(drawText, font, new SolidBrush(Color.FromArgb(fontTrans, fontColor)), bmp.Width / 2, bmp.Height / 2, sf);
                    Graphics gImg = Graphics.FromImage(img);
                    gImg.SmoothingMode = SmoothingMode.HighQuality;
                    gImg.DrawImage(bmp, labelPosition);
                    if (Config.WatermarkAddReflection)
                    {
                        Bitmap bmp2 = AddReflection(bmp, 50, 200);
                        gImg.DrawImage(bmp2, new Rectangle(labelPosition.X, labelPosition.Y + bmp.Height - 1, bmp2.Width, bmp2.Height));
                    }
                }
                catch (Exception ex)
                {
                    DebugHelper.WriteException(ex, "Errow while drawing watermark");
                }
            }

            return img;
        }
    }
}