﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using ScreenCapture;
using ZUploaderPluginBase;
using ZUploaderScreenshotPlugin.Properties;

namespace ZUploaderScreenshotPlugin
{
    public class ScreenshotPlugin : ZUploaderPlugin
    {
        public override string Name
        {
            get { return "Screenshot Plugin"; }
        }

        public override Version Version
        {
            get { return new Version(1, 0); }
        }

        public override string Author
        {
            get { return "Jaex"; }
        }

        public override string Description
        {
            get { return "Fullscreen, active window, rectangle, rounded rectangle, ellipse, triangle, diamond, polygon, freehand captures"; }
        }

        public ScreenshotDestination Destination { get; set; }

        private delegate Image ScreenCaptureDelegate();

        public override void Initialize()
        {
            Destination = ScreenshotDestination.Upload;

            #region Controls

            ToolStripMenuItem tsmiFullscreen = new ToolStripMenuItem();
            tsmiFullscreen.Text = "Fullscreen";
            tsmiFullscreen.Image = Resources.Fullscreen;
            tsmiFullscreen.Click += new EventHandler(tsmiFullscreen_Click);

            ToolStripMenuItem tsmiRectangle = new ToolStripMenuItem();
            tsmiRectangle.Text = "Rectangle";
            tsmiRectangle.Image = Resources.Rectangle;
            tsmiRectangle.Click += new EventHandler(tsmiRectangle_Click);

            ToolStripMenuItem tsmiRoundedRectangle = new ToolStripMenuItem();
            tsmiRoundedRectangle.Text = "Rounded Rectangle";
            tsmiRoundedRectangle.Image = Resources.RoundedRectangle;
            tsmiRoundedRectangle.Click += new EventHandler(tsmiRoundedRectangle_Click);

            ToolStripMenuItem tsmiEllipse = new ToolStripMenuItem();
            tsmiEllipse.Text = "Ellipse";
            tsmiEllipse.Image = Resources.Ellipse;
            tsmiEllipse.Click += new EventHandler(tsmiEllipse_Click);

            ToolStripMenuItem tsmiTriangle = new ToolStripMenuItem();
            tsmiTriangle.Text = "Triangle";
            tsmiTriangle.Image = Resources.Triangle;
            tsmiTriangle.Click += new EventHandler(tsmiTriangle_Click);

            ToolStripMenuItem tsmiDiamond = new ToolStripMenuItem();
            tsmiDiamond.Text = "Diamond";
            tsmiDiamond.Image = Resources.Diamond;
            tsmiDiamond.Click += new EventHandler(tsmiDiamond_Click);

            ToolStripMenuItem tsmiPolygon = new ToolStripMenuItem();
            tsmiPolygon.Text = "Polygon";
            tsmiPolygon.Image = Resources.Polygon;
            tsmiPolygon.Click += new EventHandler(tsmiPolygon_Click);

            ToolStripMenuItem tsmiFreeHand = new ToolStripMenuItem();
            tsmiFreeHand.Text = "FreeHand";
            tsmiFreeHand.Image = Resources.FreeHand;
            tsmiFreeHand.Click += new EventHandler(tsmiFreeHand_Click);

            #endregion Controls

            #region Hotkeys

            tsmiFullscreen.ShortcutKeyDisplayString = "PrintScreen";
            Host.RegisterPluginHotkey(Keys.PrintScreen, () => CaptureScreen(false));

            tsmiRectangle.ShortcutKeyDisplayString = "Ctrl + PrintScreen";
            Host.RegisterPluginHotkey(Keys.Control | Keys.PrintScreen, () => CaptureRegion(new RectangleRegion(), false));

            tsmiRoundedRectangle.ShortcutKeyDisplayString = "Ctrl + Shift + R";
            Host.RegisterPluginHotkey(Keys.Control | Keys.Shift | Keys.R, () => CaptureRegion(new RoundedRectangleRegion(), false));

            tsmiEllipse.ShortcutKeyDisplayString = "Ctrl + Shift + E";
            Host.RegisterPluginHotkey(Keys.Control | Keys.Shift | Keys.E, () => CaptureRegion(new EllipseRegion(), false));

            tsmiTriangle.ShortcutKeyDisplayString = "Ctrl + Shift + T";
            Host.RegisterPluginHotkey(Keys.Control | Keys.Shift | Keys.T, () => CaptureRegion(new TriangleRegion(), false));

            tsmiDiamond.ShortcutKeyDisplayString = "Ctrl + Shift + D";
            Host.RegisterPluginHotkey(Keys.Control | Keys.Shift | Keys.D, () => CaptureRegion(new DiamondRegion(), false));

            tsmiPolygon.ShortcutKeyDisplayString = "Ctrl + Shift + P";
            Host.RegisterPluginHotkey(Keys.Control | Keys.Shift | Keys.P, () => CaptureRegion(new PolygonRegion(), false));

            tsmiFreeHand.ShortcutKeyDisplayString = "Shift + PrintScreen";
            Host.RegisterPluginHotkey(Keys.Shift | Keys.PrintScreen, () => CaptureRegion(new FreeHandRegion(), false));

            Host.RegisterPluginHotkey(Keys.Alt | Keys.PrintScreen, () => CaptureActiveWindow(false));

            #endregion Hotkeys

            Host.AddPluginButton(tsmiFullscreen);
            Host.AddPluginButton(tsmiRectangle);
            Host.AddPluginButton(tsmiRoundedRectangle);
            Host.AddPluginButton(tsmiEllipse);
            Host.AddPluginButton(tsmiTriangle);
            Host.AddPluginButton(tsmiDiamond);
            Host.AddPluginButton(tsmiPolygon);
            Host.AddPluginButton(tsmiFreeHand);
        }

        private void Capture(ScreenCaptureDelegate capture, bool autoHideForm = true)
        {
            if (autoHideForm)
            {
                Host.Hide();
                Thread.Sleep(250);
            }

            Image img = null;

            try
            {
                img = capture();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            finally
            {
                if (autoHideForm)
                {
                    Host.Show();
                }

                if (img != null)
                {
                    switch (Destination)
                    {
                        default:
                        case ScreenshotDestination.Upload:
                            Host.UploadImage(img);
                            break;
                        case ScreenshotDestination.Clipboard:
                            Clipboard.SetImage(img);
                            break;
                    }
                }
            }
        }

        private void CaptureScreen(bool autoHideForm = true)
        {
            Capture(Helpers.GetScreenshot, autoHideForm);
        }

        private void CaptureActiveWindow(bool autoHideForm = true)
        {
            Capture(Helpers.GetActiveWindowScreenshot, autoHideForm);
        }

        private void CaptureRegion(Surface surface, bool autoHideForm = true)
        {
            Capture(() =>
            {
                Image img = null, screenshot = Helpers.GetScreenshot();

                surface.LoadBackground(screenshot);

                if (surface.ShowDialog() == DialogResult.OK)
                {
                    img = surface.GetRegionImage();
                }

                return img;
            }, autoHideForm);
        }

        private void tsmiFullscreen_Click(object sender, EventArgs e)
        {
            CaptureScreen();
        }

        private void tsmiRectangle_Click(object sender, EventArgs e)
        {
            CaptureRegion(new RectangleRegion());
        }

        private void tsmiRoundedRectangle_Click(object sender, EventArgs e)
        {
            CaptureRegion(new RoundedRectangleRegion());
        }

        private void tsmiEllipse_Click(object sender, EventArgs e)
        {
            CaptureRegion(new EllipseRegion());
        }

        private void tsmiTriangle_Click(object sender, EventArgs e)
        {
            CaptureRegion(new TriangleRegion());
        }

        private void tsmiDiamond_Click(object sender, EventArgs e)
        {
            CaptureRegion(new DiamondRegion());
        }

        private void tsmiPolygon_Click(object sender, EventArgs e)
        {
            CaptureRegion(new PolygonRegion());
        }

        private void tsmiFreeHand_Click(object sender, EventArgs e)
        {
            CaptureRegion(new FreeHandRegion());
        }
    }
}