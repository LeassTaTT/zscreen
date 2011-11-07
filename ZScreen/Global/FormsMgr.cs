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

using System.IO;
using System.Windows.Forms;
using ZScreenGUI.Properties;
using ZScreenLib;

namespace ZScreenGUI
{
    public static class FormsMgr
    {
        private static AboutBox AboutWindow = null;
        private static TextViewer VersionHistoryWindow = null;
        private static TextViewer LicenseWindow = null;

        public static void ShowLicense()
        {
            if (LicenseWindow == null || LicenseWindow.IsDisposed)
            {
                LicenseWindow = FillTextViewer(LicenseWindow, "License", "license.txt");
            }

            if (LicenseWindow != null)
            {
                LicenseWindow.Activate();
                LicenseWindow.Show();
            }
        }

        public static void ShowVersionHistory()
        {
            if (VersionHistoryWindow == null || VersionHistoryWindow.IsDisposed)
            {
                VersionHistoryWindow = FillTextViewer(VersionHistoryWindow, "Version History", "VersionHistory.txt");
            }

            if (VersionHistoryWindow != null)
            {
                VersionHistoryWindow.Activate();
                VersionHistoryWindow.Show();
            }
        }

        public static void ShowAboutWindow()
        {
            if (AboutWindow == null || AboutWindow.IsDisposed)
            {
                AboutWindow = new AboutBox();
            }

            AboutWindow.Activate();
            AboutWindow.Show();
        }

        private static TextViewer FillTextViewer(TextViewer viewer, string title, string manifestFileName)
        {
            string txt = FileSystem.GetTextFromFile(Path.Combine(Application.StartupPath, manifestFileName));
            if (txt == string.Empty)
            {
                txt = FileSystem.GetText(manifestFileName);
            }

            if (txt != string.Empty)
            {
                viewer = new TextViewer(string.Format("{0} - {1}", Application.ProductName, title), txt) { Icon = Resources.zss_main };
            }

            return viewer;
        }
    }
}