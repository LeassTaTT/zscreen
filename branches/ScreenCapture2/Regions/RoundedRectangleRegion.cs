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
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using HelpersLib;

namespace ScreenCapture
{
    public class RoundedRectangleRegion : RectangleRegion
    {
        public float Radius { get; set; }

        private int radiusIncrease = 3;

        public RoundedRectangleRegion(Image backgroundImage = null)
            : base(backgroundImage)
        {
            Radius = 25;

            MouseWheel += new MouseEventHandler(RoundedRectangleRegionSurface_MouseWheel);
        }

        private void RoundedRectangleRegionSurface_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                Radius += radiusIncrease;
            }
            else if (e.Delta < 0)
            {
                Radius = Math.Max(0, Radius - radiusIncrease);
            }
        }

        protected override void Draw(Graphics g)
        {
            if (AreaManager.Areas.Count > 0)
            {
                regionPath = new GraphicsPath();

                foreach (Rectangle area in AreaManager.Areas)
                {
                    if (area.Width > 0 && area.Height > 0)
                    {
                        regionPath.AddRoundedRectangle(new Rectangle(area.X, area.Y, area.Width - 1, area.Height - 1), Radius);
                    }
                }

                using (Region region = new Region(regionPath))
                {
                    g.ExcludeClip(region);
                    g.FillRectangle(shadowBrush, 0, 0, Width, Height);
                    //DrawObjects(g);
                    g.ResetClip();
                }

                /*if (AreaManager.IsAreaIntersect())
                {
                    g.FillPath(lightBrush, regionPath);
                }*/

                g.DrawPath(borderPen, regionPath);

                Rectangle totalArea = AreaManager.CombineAreas();
                g.DrawRectangle(borderPen, totalArea.X, totalArea.Y, totalArea.Width - 1, totalArea.Height - 1);
            }
            else
            {
                g.FillRectangle(shadowBrush, 0, 0, Width, Height);
            }
        }
    }
}