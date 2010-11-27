﻿#region License Information (GPL v2)

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HelpersLib;

namespace UploadersLib
{
    public static class FTPHelpers
    {
        public static string CombineURL(string url1, string url2)
        {
            if (string.IsNullOrEmpty(url1) || string.IsNullOrEmpty(url2))
            {
                if (!string.IsNullOrEmpty(url1))
                {
                    return url1;
                }
                else if (!string.IsNullOrEmpty(url2))
                {
                    return url2;
                }

                return string.Empty;
            }

            if (url1.EndsWith("/"))
            {
                url1 = url1.Substring(0, url1.Length - 1);
            }

            if (url2.StartsWith("/"))
            {
                url2 = url2.Remove(0, 1);
            }

            return url1 + "/" + url2;
        }

        public static string CombineURL(params string[] urls)
        {
            return urls.Aggregate((current, arg) => CombineURL(current, arg));
        }

        public enum SlashType
        {
            Prefix, Suffix
        }

        public static string AddSlash(string url, SlashType slashType)
        {
            return AddSlash(url, slashType, 1);
        }

        public static string AddSlash(string url, SlashType slashType, int count)
        {
            if (slashType == SlashType.Prefix)
            {
                if (url.StartsWith("/"))
                {
                    url = url.Remove(0, 1);
                }

                for (int i = 0; i < count; i++)
                {
                    url = "/" + url;
                }
            }
            else
            {
                if (url.EndsWith("/"))
                {
                    url = url.Substring(0, url.Length - 1);
                }

                for (int i = 0; i < count; i++)
                {
                    url += "/";
                }
            }

            return url;
        }

        public static string GetFileName(string path)
        {
            if (path.Contains('/'))
            {
                path = path.Remove(0, path.LastIndexOf('/') + 1);
            }

            return path;
        }

        public static string GetDirectoryName(string path)
        {
            if (path.Contains('/'))
            {
                path = path.Substring(0, path.LastIndexOf('/'));
            }

            return path;
        }

        public static List<string> GetPaths(string path)
        {
            List<string> result = new List<string>();
            string temp = string.Empty;
            string[] dirs = path.Split('/');
            foreach (string dir in dirs)
            {
                if (!string.IsNullOrEmpty(dir))
                {
                    temp += "/" + dir;
                    result.Add(temp);
                }
            }

            return result;
        }
    }

    public static class FTPLineParser
    {
        private static Regex unixStyle = new Regex(@"^(?<Permissions>(?<Directory>[-dl])(?<OwnerPerm>[-r][-w][-x])(?<GroupPerm>[-r][-w][-x])(?<EveryonePerm>[-r][-w][-x]))\s+(?<FileType>\d+)\s+(?<Owner>\w+)\s+(?<Group>\w+)\s+(?<Size>\d+)\s+(?<Month>\w+)\s+(?<Day>\d{1,2})\s+(?<Year>(?<Hour>\d{1,2}):*(?<Minutes>\d{1,2}))\s+(?<Name>.*)$");
        private static Regex winStyle = new Regex(@"^(?<Month>\d{1,2})-(?<Day>\d{1,2})-(?<Year>\d{1,2})\s+(?<Hour>\d{1,2}):(?<Minutes>\d{1,2})(?<ampm>am|pm)\s+(?<Dir>[<]dir[>])?\s+(?<Size>\d+)?\s+(?<Name>.*)$");

        public static FTPLineResult Parse(string line)
        {
            Match match = unixStyle.Match(line);
            if (match.Success)
            {
                return ParseMatch(match.Groups, ListStyle.Unix);
            }

            throw new Exception("Only support Unix ftp servers.");

            /*
            match = winStyle.Match(line);
            if (match.Success)
            {
                return ParseMatch(match.Groups, ListStyle.Windows);
            }

            throw new Exception("Invalid line format");
            */
        }

        private static FTPLineResult ParseMatch(GroupCollection matchGroups, ListStyle style)
        {
            FTPLineResult result = new FTPLineResult();

            result.Style = style;
            string dirMatch = style == ListStyle.Unix ? "d" : "<dir>";
            result.IsDirectory = matchGroups["Directory"].Value.Equals(dirMatch, StringComparison.InvariantCultureIgnoreCase);
            result.Permissions = matchGroups["Permissions"].Value;
            result.Name = matchGroups["Name"].Value;

            if (!result.IsDirectory)
            {
                result.SetSize(matchGroups["Size"].Value);
            }

            result.Owner = matchGroups["Owner"].Value;
            result.Group = matchGroups["Group"].Value;
            result.SetDateTime(matchGroups["Year"].Value, matchGroups["Month"].Value, matchGroups["Day"].Value);

            return result;
        }
    }

    public enum ListStyle
    {
        Unix,
        Windows
    }

    public class FTPLineResult
    {
        public ListStyle Style;
        public string Name;
        public string Permissions;
        public DateTime DateTime;
        public bool TimeInfo;
        public bool IsDirectory;
        public long Size;
        public string SizeString;
        public string Owner;
        public string Group;
        public bool IsSpecial;

        public void SetSize(string size)
        {
            this.Size = long.Parse(size);
            this.SizeString = this.Size.ToString("N0");
        }

        public void SetDateTime(string year, string month, string day)
        {
            string time = string.Empty;

            if (year.Contains(':'))
            {
                time = year;
                year = FastDateTime.Now.Year.ToString();
                this.TimeInfo = true;
            }

            this.DateTime = DateTime.Parse(string.Format("{0}/{1}/{2} {3}", year, month, day, time));
            this.DateTime = this.DateTime.ToLocalTime();
        }
    }
}