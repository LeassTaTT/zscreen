﻿#region License Information (GPL v2)

/*
    ZUploader - A program that allows you to upload images, texts or files
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
using System.Drawing;
using System.Windows.Forms;

namespace ScreenCapture
{
    public partial class RegionCapturePreview : Form
    {
        public Image Result
        {
            get
            {
                return result;
            }
            private set
            {
                result = value;

                if (result != null)
                {
                    pbResult.Image = result;
                    Text = string.Format("Region Capture: {0}x{1}", result.Width, result.Height);
                }
            }
        }

        public SurfaceOptions SurfaceConfig { get; set; }

        private Image screenshot;
        private Image result;

        public RegionCapturePreview()
            : this(new SurfaceOptions())
        {
        }

        public RegionCapturePreview(SurfaceOptions surfaceConfig)
        {
            InitializeComponent();

            screenshot = Screenshot.CaptureFullscreen();
            SurfaceConfig = surfaceConfig;

            cbDrawBorder.Checked = surfaceConfig.DrawBorder;
            cbDrawChecker.Checked = surfaceConfig.DrawChecker;
            cbIsFixedSize.Checked = surfaceConfig.IsFixedSize;
            nudFixedWidth.Value = surfaceConfig.FixedSize.Width;
            nudFixedHeight.Value = surfaceConfig.FixedSize.Height;
            cbQuickCrop.Checked = surfaceConfig.QuickCrop;
        }

        private void CaptureRegion(Surface surface)
        {
            pbResult.Image = null;

            surface.Config = SurfaceConfig;
            surface.SurfaceImage = screenshot;
            surface.Prepare();

            if (surface.ShowDialog() == DialogResult.OK)
            {
                Result = surface.GetRegionImage();
            }

            surface.Dispose();
        }

        private void tsbFullscreen_Click(object sender, EventArgs e)
        {
            Result = screenshot;
        }

        private void tsbWindowRectangle_Click(object sender, EventArgs e)
        {
            RectangleRegion rectangleRegion = new RectangleRegion();
            rectangleRegion.AreaManager.WindowCaptureMode = true;
            CaptureRegion(rectangleRegion);
        }

        private void tsbRectangle_Click(object sender, EventArgs e)
        {
            CaptureRegion(new RectangleRegion());
        }

        private void tsbRoundedRectangle_Click(object sender, EventArgs e)
        {
            CaptureRegion(new RoundedRectangleRegion());
        }

        private void tsbEllipse_Click(object sender, EventArgs e)
        {
            CaptureRegion(new EllipseRegion());
        }

        private void tsbTriangle_Click(object sender, EventArgs e)
        {
            CaptureRegion(new TriangleRegion());
        }

        private void tsbDiamond_Click(object sender, EventArgs e)
        {
            CaptureRegion(new DiamondRegion());
        }

        private void tsbPolygon_Click(object sender, EventArgs e)
        {
            CaptureRegion(new PolygonRegion());
        }

        private void tsbFreeHand_Click(object sender, EventArgs e)
        {
            CaptureRegion(new FreeHandRegion());
        }

        private void RegionCapturePreview_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Result != null)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        private void cbDrawBorder_CheckedChanged(object sender, EventArgs e)
        {
            SurfaceConfig.DrawBorder = cbDrawBorder.Checked;
        }

        private void cbDrawChecker_CheckedChanged(object sender, EventArgs e)
        {
            SurfaceConfig.DrawChecker = cbDrawChecker.Checked;
        }

        private void cbIsFixedSize_CheckedChanged(object sender, EventArgs e)
        {
            SurfaceConfig.IsFixedSize = cbIsFixedSize.Checked;
        }

        private void nudFixedWidth_ValueChanged(object sender, EventArgs e)
        {
            SurfaceConfig.FixedSize = new Size((int)nudFixedWidth.Value, SurfaceConfig.FixedSize.Height);
        }

        private void nudFixedHeight_ValueChanged(object sender, EventArgs e)
        {
            SurfaceConfig.FixedSize = new Size(SurfaceConfig.FixedSize.Width, (int)nudFixedHeight.Value);
        }

        private void cbQuickCrop_CheckedChanged(object sender, EventArgs e)
        {
            SurfaceConfig.QuickCrop = cbQuickCrop.Checked;
        }

        private void btnClipboardCopy_Click(object sender, EventArgs e)
        {
            if (Result != null)
            {
                Clipboard.SetImage(Result);
            }
        }
    }
}