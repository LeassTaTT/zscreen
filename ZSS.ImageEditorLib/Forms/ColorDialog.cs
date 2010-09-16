#region License Information (GPL v2)

/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2009  Brandon Zimmerman

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

#region Source code: Greenshot (GPL)

/*
    This file originated from the Greenshot project (GPL). It may or may not have been modified.
    Please do not contact Greenshot about errors with this code. Instead contact the creators of this program.
    URL: http://greenshot.sourceforge.net/
    Code (CVS): http://greenshot.cvs.sourceforge.net/viewvc/greenshot/
*/

#endregion Source code: Greenshot (GPL)

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Greenshot
{
    /// <summary>
    /// Description of ColorDialog.
    /// </summary>
    public partial class ColorDialog
    {
        private ColorDialog()
        {
            this.SuspendLayout();
            InitializeComponent();

            this.SuspendLayout();
            this.createColorPalette(5, 5, 15, 15);
            this.createLastUsedColorButtonRow(5, 190, 15, 15);
            this.ResumeLayout();
        }

        public static ColorDialog GetInstance()
        {
            if (uniqueInstance == null)
            {
                uniqueInstance = new ColorDialog();
            }
            return uniqueInstance;
        }

        private static ColorDialog uniqueInstance;
        private List<Button> colorButtons = new List<Button>();
        private List<Button> recentColorButtons = new List<Button>();
        private List<Color> recentColors = new List<Color>();
        private ToolTip toolTip = new ToolTip();
        private bool updateInProgress = false;

        public Color[] RecentColors
        {
            get { return recentColors.ToArray(); }
            set { for (int i = 0; i < value.Length; i++) { recentColors.Add(value[i]); } updateRecentColorsButtonRow(); }
        }

        public Color Color
        {
            get { return colorPanel.BackColor; }
            set { previewColor(value, this); }
        }

        #region User interface generation

        private void createColorPalette(int x, int y, int w, int h)
        {
            this.createColorButtonColumn(255, 0, 0, x, y, w, h, 11);
            x += w;
            this.createColorButtonColumn(255, 255 / 2, 0, x, y, w, h, 11);
            x += w;
            this.createColorButtonColumn(255, 255, 0, x, y, w, h, 11);
            x += w;
            this.createColorButtonColumn(255 / 2, 255, 0, x, y, w, h, 11);
            x += w;
            this.createColorButtonColumn(0, 255, 0, x, y, w, h, 11);
            x += w;
            this.createColorButtonColumn(0, 255, 255 / 2, x, y, w, h, 11);
            x += w;
            this.createColorButtonColumn(0, 255, 255, x, y, w, h, 11);
            x += w;
            this.createColorButtonColumn(0, 255 / 2, 255, x, y, w, h, 11);
            x += w;
            this.createColorButtonColumn(0, 0, 255, x, y, w, h, 11);
            x += w;
            this.createColorButtonColumn(255 / 2, 0, 255, x, y, w, h, 11);
            x += w;
            this.createColorButtonColumn(255, 0, 255, x, y, w, h, 11);
            x += w;
            this.createColorButtonColumn(255, 0, 255 / 2, x, y, w, h, 11);
            x += w + 5;
            this.createColorButtonColumn(255 / 2, 255 / 2, 255 / 2, x, y, w, h, 11);

            this.Controls.AddRange(this.colorButtons.ToArray());
        }

        private void createColorButtonColumn(int red, int green, int blue, int x, int y, int w, int h, int shades)
        {
            int shadedColorsNum = (shades - 1) / 2;
            for (int i = 0; i <= shadedColorsNum; i++)
            {
                colorButtons.Add(createColorButton(red * i / shadedColorsNum, green * i / shadedColorsNum, blue * i / shadedColorsNum, x, y + i * h, w, h));
                if (i > 0) colorButtons.Add(createColorButton(red + (255 - red) * i / shadedColorsNum, green + (255 - green) * i / shadedColorsNum, blue + (255 - blue) * i / shadedColorsNum, x, y + (i + shadedColorsNum) * h, w, h));
            }
        }

        private Button createColorButton(int red, int green, int blue, int x, int y, int w, int h)
        {
            return createColorButton(Color.FromArgb(255, red, green, blue), x, y, w, h);
        }

        private Button createColorButton(Color color, int x, int y, int w, int h)
        {
            Button b = new Button();
            b.BackColor = color;
            b.FlatAppearance.BorderSize = 0;
            b.FlatStyle = FlatStyle.Flat;
            b.Location = new Point(x, y);
            b.Size = new Size(w, h);
            b.TabStop = false;
            b.Click += new System.EventHandler(colorButtonClick);
            toolTip.SetToolTip(b, ColorTranslator.ToHtml(color) + " | R:" + color.R + ", G:" + color.G + ", B:" + color.B);
            return b;
        }

        private void createLastUsedColorButtonRow(int x, int y, int w, int h)
        {
            for (int i = 0; i < 12; i++)
            {
                Button b = this.createColorButton(Color.Transparent, x, y, w, h);
                b.Enabled = false;
                recentColorButtons.Add(b);
                x += w;
            }
            this.Controls.AddRange(this.recentColorButtons.ToArray());
        }

        #endregion User interface generation

        #region Update user interface

        private void updateRecentColorsButtonRow()
        {
            for (int i = 0; i < recentColors.Count && i < 12; i++)
            {
                recentColorButtons[i].BackColor = recentColors[i];
                recentColorButtons[i].Enabled = true;
            }
        }

        private void previewColor(Color c, Control trigger)
        {
            updateInProgress = true;
            this.colorPanel.BackColor = c;
            if (trigger != this.textBoxHtmlColor)
            {
                this.textBoxHtmlColor.Text = System.Drawing.ColorTranslator.ToHtml(c);
            }
            else
            {
                if (!this.textBoxHtmlColor.Text.StartsWith("#"))
                {
                    int selStart = this.textBoxHtmlColor.SelectionStart;
                    int selLength = this.textBoxHtmlColor.SelectionLength;
                    this.textBoxHtmlColor.Text = "#" + this.textBoxHtmlColor.Text;
                    this.textBoxHtmlColor.Select(selStart + 1, selLength + 1);
                }
            }
            if (trigger != this.textBoxRed && trigger != this.textBoxGreen && trigger != this.textBoxBlue && trigger != this.textBoxAlpha)
            {
                this.textBoxRed.Text = c.R.ToString();
                this.textBoxGreen.Text = c.G.ToString();
                this.textBoxBlue.Text = c.B.ToString();
                this.textBoxAlpha.Text = c.A.ToString();
            }
            updateInProgress = false;
        }

        private void addToRecentColors(Color c)
        {
            recentColors.Remove(c);
            recentColors.Insert(0, c);
            if (recentColors.Count > 12) recentColors.RemoveRange(12, recentColors.Count - 12);
            updateRecentColorsButtonRow();
        }

        #endregion Update user interface

        #region Textbox event handlers

        private void TextBoxHexadecimalTextChanged(object sender, System.EventArgs e)
        {
            if (updateInProgress) return;
            TextBox tb = (TextBox)sender;
            string t = tb.Text.Replace("#", "");
            int i = 0;
            Int32.TryParse(t, System.Globalization.NumberStyles.AllowHexSpecifier, Thread.CurrentThread.CurrentCulture, out i);
            Color c = Color.FromArgb(i);
            Color opaqueColor = Color.FromArgb(255, c.R, c.G, c.B);
            previewColor(opaqueColor, tb);
        }

        private void TextBoxRGBTextChanged(object sender, System.EventArgs e)
        {
            if (updateInProgress) return;
            TextBox tb = (TextBox)sender;
            previewColor(Color.FromArgb(getColorPartIntFromString(textBoxAlpha.Text), getColorPartIntFromString(textBoxRed.Text), getColorPartIntFromString(textBoxGreen.Text), getColorPartIntFromString(textBoxBlue.Text)), tb);
        }

        private void TextBoxGotFocus(object sender, System.EventArgs e)
        {
            this.textBoxHtmlColor.SelectAll();
        }

        private void TextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Enter)
            {
                addToRecentColors(colorPanel.BackColor);
            }
        }

        #endregion Textbox event handlers

        #region Button event handlers

        private void colorButtonClick(object sender, System.EventArgs e)
        {
            Button b = (Button)sender;
            previewColor(b.BackColor, b);
        }

        private void btnTransparentClick(object sender, System.EventArgs e)
        {
            colorButtonClick(sender, e);
        }

        private void BtnApplyClick(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Hide();
            addToRecentColors(colorPanel.BackColor);
        }

        #endregion Button event handlers

        #region Helper functions

        private int getColorPartIntFromString(string s)
        {
            int ret = 0;
            Int32.TryParse(s, out ret);
            if (ret < 0) ret = 0;
            else if (ret > 255) ret = 255;
            return ret;
        }

        #endregion Helper functions
    }
}