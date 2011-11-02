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
using HelpersLib;
using Microsoft.Win32;
using ZScreenLib;

namespace ZScreenGUI
{
    public static class ImageEditorHelper
    {
        public static void FindImageEditors()
        {
            SoftwareCheck("Paint", "mspaint.exe", "edit");
            SoftwareCheck("Adobe Photoshop", "Photoshop.exe");
            SoftwareCheck("Paint.NET", "PaintDotNet.exe");
            SoftwareCheck("Irfan View", "i_view32.exe");
            SoftwareCheck("XnView", "xnview.exe");
            SoftwareCheck("Picasa", "PicasaPhotoViewer.exe");
        }

        /// <summary>Registry path: HKEY_CLASSES_ROOT\Applications\{fileName}\shell\{command}\command</summary>
        private static bool SoftwareCheck(string name, string fileName, string command = "open")
        {
            string path = string.Format(@"HKEY_CLASSES_ROOT\Applications\{0}\shell\{1}\command", fileName, command);
            string value = Registry.GetValue(path, null, null) as string;

            if (!string.IsNullOrEmpty(value))
            {
                string filePath = value.ParseQuoteString();

                if (File.Exists(filePath))
                {
                    if (!Software.Exist(Engine.ConfigUI.ConfigActions, name))
                    {
                        Engine.ConfigUI.ConfigActions.ActionsApps.Add(new Software(name, filePath));
                    }

                    return true;
                }
            }

            return false;
        }
    }
}