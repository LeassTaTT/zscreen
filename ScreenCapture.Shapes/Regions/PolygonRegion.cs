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

using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using HelpersLib;

namespace ScreenCapture
{
    public class PolygonRegion : Surface
    {
        private List<NodeObject> nodes;
        private bool isAreaCreated;
        private Rectangle currentArea;

        public PolygonRegion(Image backgroundImage = null)
            : base(backgroundImage)
        {
            nodes = new List<NodeObject>();

            MouseDown += new MouseEventHandler(PolygonRegionSurface_MouseDown);
        }

        private void PolygonRegionSurface_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (DrawableObjects.Cast<NodeObject>().Any(node => node.IsMouseHover || node.IsDragging))
                {
                    return;
                }

                if (nodes.Count == 0)
                {
                    CreateNode();
                }

                CreateNode();

                isAreaCreated = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (isAreaCreated)
                {
                    foreach (NodeObject node in nodes)
                    {
                        if (node.IsMouseHover)
                        {
                            nodes.Remove(node);
                            DrawableObjects.Remove(node);
                            return;
                        }
                    }

                    isAreaCreated = false;
                    nodes.Clear();
                    DrawableObjects.Clear();
                }
                else
                {
                    Close(false);
                }
            }
        }

        protected override void Update()
        {
            base.Update();

            for (int i = nodes.Count - 1; i >= 0; i--)
            {
                if (nodes[i].Visible && nodes[i].IsDragging)
                {
                    ActivateNode(nodes[i]);
                    break;
                }
            }

            if (nodes.Count > 2)
            {
                RectangleF rect = regionFillPath.GetBounds();
                currentArea = new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width + 1, (int)rect.Height + 1);
            }
        }

        protected override void Draw(Graphics g)
        {
            regionFillPath = new GraphicsPath();

            for (int i = 0; i < nodes.Count - 1; i++)
            {
                regionFillPath.AddLine(nodes[i].Position, nodes[i + 1].Position);
            }

            if (nodes.Count > 2)
            {
                regionFillPath.CloseFigure();

                using (Region region = new Region(regionFillPath))
                {
                    g.ExcludeClip(region);
                    g.FillRectangle(shadowBrush, 0, 0, Width, Height);
                    g.ResetClip();
                }

                g.DrawRectangleProper(borderPen, currentArea);
            }
            else
            {
                g.FillRectangle(shadowBrush, 0, 0, Width, Height);
            }

            if (nodes.Count > 1)
            {
                g.DrawPath(borderPen, regionFillPath);
            }

            base.Draw(g);
        }

        private NodeObject CreateNode()
        {
            NodeObject newNode = new NodeObject(borderPen, nodeBackgroundBrush);
            ActivateNode(newNode);
            nodes.Add(newNode);
            DrawableObjects.Add(newNode);
            return newNode;
        }

        private void ActivateNode(NodeObject node)
        {
            node.Position = InputManager.MousePosition0Based;
            node.Visible = true;
            node.IsDragging = true;
        }
    }
}