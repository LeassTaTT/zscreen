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
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace HelpersLib
{
    public enum ReplacementVariables
    {
        [Description("Title of active window")]
        t,
        [Description("Current year")]
        y,
        [Description("Current month")]
        mo,
        [Description("Current month name (Local language)")]
        mon,
        [Description("Current month name (English)")]
        mon2,
        [Description("Current day")]
        d,
        [Description("Current hour")]
        h,
        [Description("Current minute")]
        mi,
        [Description("Current second")]
        s,
        [Description("Current milisecond")]
        ms,
        [Description("Current week name (Local language)")]
        w,
        [Description("Current week name (English)")]
        w2,
        [Description("Auto increment number")]
        i,
        [Description("Gets AM/PM")]
        pm,
        [Description("Random number 0 to 9")]
        rn,
        [Description("Random alphanumeric char")]
        ra,
        [Description("Gets image width")]
        width,
        [Description("Gets image height")]
        height,
        [Description("User name")]
        un,
        [Description("User login name")]
        uln,
        [Description("Computer name")]
        cn,
        [Description("Application name")]
        app,
        [Description("Application version")]
        ver,
        [Description("New line")]
        n
    }

    public static class ReplacementExtension
    {
        public const char Prefix = '%';

        public static string ToPrefixString(this ReplacementVariables replacement)
        {
            return Prefix + replacement.ToString();
        }
    }

    public enum NameParserType
    {
        Text,
        EntireScreen,
        ActiveWindow,
        Watermark,
        SaveFolder
    }

    public class NameParser : IDisposable
    {
        public NameParserType Type { get; set; }
        public int AutoIncrementNumber { get; set; }
        public bool IsFolderPath { get; set; }
        public bool IsPreview { get; set; }
        public string Host { get; set; }
        public Image Picture { get; set; }
        public DateTime CustomDate { get; set; }
        public string CustomProductName { get; set; }
        public int MaxNameLength { get; set; }

        public NameParser()
        {
            Type = NameParserType.Text;
        }

        public NameParser(NameParserType nameParserType)
        {
            Type = nameParserType;
        }

        public string Convert(string pattern)
        {
            if (string.IsNullOrEmpty(pattern))
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder(pattern);

            #region width, height (If Picture exist)

            string width = string.Empty, height = string.Empty;

            if (Picture != null)
            {
                width = Picture.Width.ToString();
                height = Picture.Height.ToString();
            }

            sb.Replace(ReplacementVariables.width.ToPrefixString(), width);
            sb.Replace(ReplacementVariables.height.ToPrefixString(), height);

            #endregion width, height (If Picture exist)

            #region t (If ActiveWindow or Watermark)

            if (Type == NameParserType.ActiveWindow || Type == NameParserType.Watermark)
            {
                string activeWindow = ZAppHelper.GetForegroundWindowText();

                if (string.IsNullOrEmpty(activeWindow))
                {
                    activeWindow = Application.ProductName;
                }

                sb.Replace(ReplacementVariables.t.ToPrefixString(), activeWindow);
            }
            else
            {
                sb.Replace(ReplacementVariables.t.ToPrefixString(), string.Empty);
            }

            #endregion t (If ActiveWindow or Watermark)

            #region host (If Host exist "FTP")

            if (!string.IsNullOrEmpty(Host))
            {
                sb.Replace("%host", Host);
            }

            #endregion host (If Host exist "FTP")

            #region y, mo, mon, mon2, d

            DateTime dt;

            if (CustomDate != DateTime.MinValue)
            {
                dt = CustomDate;
            }
            else
            {
                dt = FastDateTime.Now;
            }

            sb.Replace(ReplacementVariables.mon2.ToPrefixString(), CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(dt.Month))
                .Replace(ReplacementVariables.mon.ToPrefixString(), CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dt.Month))
                .Replace(ReplacementVariables.y.ToPrefixString(), dt.Year.ToString())
                .Replace(ReplacementVariables.mo.ToPrefixString(), ZAppHelper.AddZeroes(dt.Month))
                .Replace(ReplacementVariables.d.ToPrefixString(), ZAppHelper.AddZeroes(dt.Day));

            #endregion y, mo, mon, mon2, d

            #region h, mi, s, ms, w, w2, pm, i (If not SaveFolder)

            if (Type != NameParserType.SaveFolder)
            {
                string hour;

                if (sb.ToString().Contains(ReplacementVariables.pm.ToPrefixString()))
                {
                    hour = ZAppHelper.HourTo12(dt.Hour);
                }
                else
                {
                    hour = ZAppHelper.AddZeroes(dt.Hour);
                }

                sb.Replace(ReplacementVariables.h.ToPrefixString(), hour)
                     .Replace(ReplacementVariables.mi.ToPrefixString(), ZAppHelper.AddZeroes(dt.Minute))
                     .Replace(ReplacementVariables.s.ToPrefixString(), ZAppHelper.AddZeroes(dt.Second))
                     .Replace(ReplacementVariables.ms.ToPrefixString(), ZAppHelper.AddZeroes(dt.Millisecond, 3))
                     .Replace(ReplacementVariables.w2.ToPrefixString(), CultureInfo.InvariantCulture.DateTimeFormat.GetDayName(dt.DayOfWeek))
                     .Replace(ReplacementVariables.w.ToPrefixString(), CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(dt.DayOfWeek))
                     .Replace(ReplacementVariables.pm.ToPrefixString(), (dt.Hour >= 12 ? "PM" : "AM"));

                if (!IsPreview && sb.ToString().Contains("%i"))
                {
                    AutoIncrementNumber++;
                }

                sb.Replace(ReplacementVariables.i.ToPrefixString(), ZAppHelper.AddZeroes(AutoIncrementNumber, 4));
            }

            #endregion h, mi, s, ms, w, w2, pm, i (If not SaveFolder)

            #region un, uln, cn, app, ver, n

            sb.Replace(ReplacementVariables.un.ToPrefixString(), Environment.UserName);
            sb.Replace(ReplacementVariables.uln.ToPrefixString(), Environment.UserDomainName);
            sb.Replace(ReplacementVariables.cn.ToPrefixString(), Environment.MachineName);

            string productName = string.IsNullOrEmpty(CustomProductName) ? Application.ProductName : CustomProductName;
            sb.Replace(ReplacementVariables.app.ToPrefixString(), productName);
            sb.Replace(ReplacementVariables.ver.ToPrefixString(), Application.ProductVersion);

            if (Type == NameParserType.Watermark)
            {
                sb.Replace(ReplacementVariables.n.ToPrefixString(), "\n");
            }

            #endregion un, uln, cn, app, ver, n

            #region rn, ra

            string result = sb.ToString();

            string rn = ReplacementVariables.rn.ToPrefixString();
            while (result.ReplaceFirst(rn, ZAppHelper.GetRandomChar(ZAppHelper.Numbers).ToString(), out result)) ;

            string ra = ReplacementVariables.ra.ToPrefixString();
            while (result.ReplaceFirst(ra, ZAppHelper.GetRandomChar(ZAppHelper.Alphanumeric).ToString(), out result)) ;

            #endregion rn, ra

            if (Type != NameParserType.Watermark)
            {
                result = ZAppHelper.NormalizeString(result, Type != NameParserType.SaveFolder, IsFolderPath);
            }

            if (MaxNameLength > 0 && (Type == NameParserType.ActiveWindow || Type == NameParserType.EntireScreen) &&
                result.Length > MaxNameLength)
            {
                result = result.Substring(0, MaxNameLength);
            }

            return result;
        }

        public void Dispose()
        {
            if (Picture != null)
            {
                Picture.Dispose();
            }
        }
    }
}