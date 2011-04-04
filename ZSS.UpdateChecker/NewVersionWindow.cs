#region License Information (GPL v2)

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

using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ZSS.UpdateCheckerLib
{
    public partial class NewVersionWindow : Form
    {
        public NewVersionWindowOptions Options { get; private set; }

        public NewVersionWindow(NewVersionWindowOptions options)
        {
            InitializeComponent();

            Options = options;

            if (Options.MyIcon != null) Icon = Options.MyIcon;
            if (Options.MyImage != null) pbApp.Image = Options.MyImage;

            lblVer.Text = Options.Question;

            if (!string.IsNullOrEmpty(Options.UpdateInfo.Summary))
            {
                txtVer.Text = Options.UpdateInfo.Summary;
            }

            Text = string.Format("{0} {1} is available", Options.ProjectName, Options.UpdateInfo.Version.ToString());

            BringToFront();
            Activate();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }

        private void TxtVerLinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void NewVersionWindow_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Rectangle rect = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height);

            using (LinearGradientBrush brush = new LinearGradientBrush(rect, Color.Black, Color.FromArgb(50, 50, 50), LinearGradientMode.Vertical))
            {
                brush.SetSigmaBellShape(0.20f);
                g.FillRectangle(brush, rect);
            }
        }
    }

    public class NewVersionWindowOptions
    {
        public Icon MyIcon { get; set; }
        public Image MyImage { get; set; }
        public string Question { get; set; }
        public string ProjectName { get; set; }
        public UpdateInfo UpdateInfo { get; set; }
    }
}