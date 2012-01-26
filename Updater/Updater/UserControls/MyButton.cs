﻿#region License Information (GPL v2)

/*
    ZUploader - A program that allows you to upload images, texts or files
    Copyright (C) 2008-2012 ZScreen Developers

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
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Updater
{
    public class MyButton : Control
    {
        public override string Text
        {
            get
            {
                return text;
            }
            set
            {
                if (value == null)
                {
                    value = "";
                }

                if (text != value)
                {
                    text = value;

                    Refresh();
                }
            }
        }

        private string text;
        private bool isHover;
        private LinearGradientBrush backgroundBrush, backgroundHoverBrush, innerBorderBrush;
        private Pen innerBorderPen, borderPen;
        private Font textFont;

        public MyButton()
        {
            InitializeComponent();
            Prepare();
        }

        private void Prepare()
        {
            ForeColor = Color.White;
            backgroundBrush = new LinearGradientBrush(new Rectangle(2, 2, ClientSize.Width - 4, ClientSize.Height - 4),
                Color.FromArgb(105, 105, 105), Color.FromArgb(65, 65, 65), LinearGradientMode.Vertical);
            backgroundHoverBrush = new LinearGradientBrush(new Rectangle(2, 2, ClientSize.Width - 4, ClientSize.Height - 4),
                Color.FromArgb(115, 115, 115), Color.FromArgb(75, 75, 75), LinearGradientMode.Vertical);
            innerBorderBrush = new LinearGradientBrush(new Rectangle(1, 1, ClientSize.Width - 2, ClientSize.Height - 2),
                Color.FromArgb(125, 125, 125), Color.FromArgb(75, 75, 75), LinearGradientMode.Vertical);
            innerBorderPen = new Pen(innerBorderBrush);
            borderPen = new Pen(Color.FromArgb(30, 30, 30));
            textFont = new Font("Arial", 12);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            Graphics g = pe.Graphics;

            DrawBackground(g);

            if (!string.IsNullOrEmpty(Text))
            {
                DrawText(g);
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            isHover = true;
            Refresh();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            isHover = false;
            Refresh();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Prepare();
        }

        private void DrawBackground(Graphics g)
        {
            if (isHover)
            {
                g.FillRectangle(backgroundHoverBrush, new Rectangle(2, 2, ClientSize.Width - 4, ClientSize.Height - 4));
            }
            else
            {
                g.FillRectangle(backgroundBrush, new Rectangle(2, 2, ClientSize.Width - 4, ClientSize.Height - 4));
            }

            g.DrawRectangle(innerBorderPen, new Rectangle(1, 1, ClientSize.Width - 3, ClientSize.Height - 3));
            g.DrawRectangle(borderPen, new Rectangle(0, 0, ClientSize.Width - 1, ClientSize.Height - 1));
        }

        private void DrawText(Graphics g)
        {
            TextRenderer.DrawText(g, Text, textFont, new Rectangle(ClientRectangle.X, ClientRectangle.Y + 1, ClientRectangle.Width, ClientRectangle.Height + 1), Color.Black);
            TextRenderer.DrawText(g, Text, textFont, ClientRectangle, ForeColor);
        }

        #region Component Designer generated code

        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            if (backgroundBrush != null) backgroundBrush.Dispose();
            if (backgroundHoverBrush != null) backgroundHoverBrush.Dispose();
            if (innerBorderBrush != null) innerBorderBrush.Dispose();
            if (innerBorderPen != null) innerBorderPen.Dispose();
            if (borderPen != null) borderPen.Dispose();
            if (textFont != null) textFont.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }

        #endregion Component Designer generated code
    }
}