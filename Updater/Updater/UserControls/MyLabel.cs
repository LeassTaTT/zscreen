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

using System.Drawing;
using System.Windows.Forms;

namespace Updater
{
    public class MyLabel : Control
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
        private Font textFont;

        public MyLabel()
        {
            InitializeComponent();
            Prepare();
        }

        private void Prepare()
        {
            BackColor = Color.Transparent;
            ForeColor = Color.White;
            textFont = new Font("Arial", 12);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;

            if (!string.IsNullOrEmpty(Text))
            {
                DrawText(g);
            }
        }

        private void DrawText(Graphics g)
        {
            TextFormatFlags tff = TextFormatFlags.Left | TextFormatFlags.Top;
            TextRenderer.DrawText(g, Text, textFont, new Rectangle(ClientRectangle.X, ClientRectangle.Y + 1, ClientRectangle.Width, ClientRectangle.Height + 1),
                Color.Black, tff);
            TextRenderer.DrawText(g, Text, textFont, ClientRectangle, ForeColor, tff);
        }

        #region Component Designer generated code

        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            if (textFont != null) textFont.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        #endregion Component Designer generated code
    }
}