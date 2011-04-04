﻿#region License Information (GPL v2)

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
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using HelpersLib;

namespace ZSS.UpdateCheckerLib
{
    public class UpdateChecker
    {
        public string URL { get; private set; }
        public bool CheckBeta { get; private set; }
        public UpdateInfo UpdateInfo { get; private set; }

        private IWebProxy proxy;
        private NewVersionWindowOptions nvwo;

        public UpdateChecker(string url, bool checkBeta, IWebProxy proxy, NewVersionWindowOptions nvwo)
        {
            URL = url;
            CheckBeta = checkBeta;
            this.proxy = proxy;
            this.nvwo = nvwo;
        }

        public string CheckUpdate()
        {
            try
            {
                using (WebClient wc = new WebClient { Proxy = proxy })
                using (MemoryStream ms = new MemoryStream(wc.DownloadData(URL)))
                using (XmlTextReader xml = new XmlTextReader(ms))
                {
                    XDocument xd = XDocument.Load(xml);

                    if (xd != null)
                    {
                        string path = string.Format("Update/{0}/{1}", Application.ProductName, CheckBeta ? "Beta|Stable" : "Stable");
                        XElement xe = xd.GetNode(path);

                        if (xe != null)
                        {
                            UpdateInfo = new UpdateInfo
                            {
                                Version = new Version(xe.GetValue("Version")),
                                URL = xe.GetValue("URL"),
                                Date = DateTime.Parse(xe.GetValue("Date")),
                                Summary = xe.GetValue("Summary")
                            };

                            if (!UpdateInfo.Summary.IsNullOrEmpty() && UpdateInfo.Summary.IsValidUrl())
                            {
                                UpdateInfo.Summary = wc.DownloadString(UpdateInfo.Summary.Trim());
                            }

                            return UpdateInfo.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return "Update failed:\r\n" + ex.ToString();
            }

            return null;
        }

        public void ShowPrompt()
        {
            if (UpdateInfo != null && !UpdateInfo.URL.IsNullOrEmpty() && UpdateInfo.IsUpdateRequired)
            {
                nvwo.Question = string.Format("Do you want to download it now?\n\n{0}", UpdateInfo.ToString());
                nvwo.UpdateInfo = UpdateInfo;
                nvwo.ProjectName = Application.ProductName;

                using (NewVersionWindow ver = new NewVersionWindow(nvwo))
                {
                    if (ver.ShowDialog() == DialogResult.Yes)
                    {
                        Process.Start(UpdateInfo.URL);
                    }
                }
            }
        }
    }
}