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
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZSS
{
    [Serializable()]
    public class ImageHostingService
    {
        public string Name { get; set; }
        public List<string[]> Arguments { get; set; }
        public string UploadURL { get; set; }
        public string FileForm { get; set; }
        public List<string> RegexpList { get; set; }
        public string Fullimage { get; set; }
        public string Thumbnail { get; set; }
        public List<string> Regexps { get; set; }
        public string LastOperation { get; set; }

        public ImageHostingService()
        {
            this.Arguments = new List<string[]>();
            this.RegexpList = new List<string>();
            this.Regexps = new List<string>();
        }

        public ImageHostingService(string name)
            : this()
        {
            this.Name = name;
        }

        public string ReturnLink(string str)
        {
            string link = "";
            int search;
            for (int i = 0; i < str.Length; i++)
            {
                search = CheckSyntax(str, i, ref link);
                if (search > 0)
                {
                    i = search;
                    continue;
                }
            }
            return link;
        }

        private int CheckSyntax(string str, int i, ref string link)
        {
            int search;
            search = CheckString(str, i);
            if (search > 0)
            {
                link += ReturnString(str, i, search);
                return search;
            }
            search = CheckRegexp(str, i);
            if (search > 0)
            {
                link += ReturnRegexp(str, i, search);
                return search;
            }
            search = CheckConditional(str, i);
            if (search > 0)
            {
                link += ReturnConditional(str, i, search);
                return search;
            }
            return 0;
        }

        private int CheckString(string str, int start)
        {
            if (str[start] == '"')
            {
                for (int i = start + 1; i < str.Length; i++)
                {
                    //Console.Write(str[i].ToString());
                    if (str[i] == '"')
                    {
                        return i;
                    }
                }
                new Exception("Started with \" but not closed with \"");
            }
            return 0;
        }

        private string ReturnString(string str, int start, int end)
        {
            return str.Substring(start + 1, end - start - 1);
        }

        private int CheckRegexp(string str, int start)
        {
            if (str[start] == '$')
            {
                for (int i = start + 1; i < str.Length; i++)
                {
                    //Console.Write(str[i].ToString());
                    if (!char.IsDigit(str[i]))
                    {
                        return i - 1;
                    }
                    if ((i + 1 == str.Length))
                    {
                        return i;
                    }
                }
                new Exception("Something wrong (CheckRegexp)");
            }
            return 0;
        }

        private string ReturnRegexp(string str, int start, int end)
        {
            return Regexps[Convert.ToInt32(str.Substring(start + 1, end - start)) - 1];
        }

        public int CheckConditional(string str, int start)
        {
            int search;
            string link = "";
            if (str[start] == '(')
            {
                for (int i = start + 1; i < str.Length; i++)
                {
                    //Console.Write(str[i].ToString());
                    search = CheckSyntax(str, i, ref link);
                    if (search > 0)
                    {
                        i = search;
                        continue;
                    }
                    if (str[i] == ')')
                    {
                        return i;
                    }
                }
                new Exception("Started with \"(\" but not closed with \")\"");
            }
            return 0;
        }

        public string ReturnConditional(string str, int start, int end)
        {
            int search;
            str = str.Substring(start + 1, end - start - 1);
            string conditional = "", cTrue = "", cFalse = "", link = "", result = "";
            for (int i = 0; i < str.Length; i++)
            {
                //Console.Write(str[i].ToString());
                search = CheckSyntax(str, i, ref link);
                if (search > 0)
                {
                    i = search;
                    continue;
                }
                if (string.IsNullOrEmpty(conditional))
                {
                    if (str[i] == '?')
                    {
                        conditional = str.Substring(0, i);
                        continue;
                    }
                }
                else if (string.IsNullOrEmpty(cTrue))
                {
                    if (str[i] == ':')
                    {
                        cTrue = ReturnLink(str.Substring(conditional.Length + 1, i - conditional.Length - 1));
                        cFalse = ReturnLink(str.Substring(i + 1, str.Length - i - 1));
                        break;
                    }
                }
            }
            string firstStr = "", secondStr = "", operation = "";
            for (int i = 0; i < conditional.Length; i++)
            {
                search = CheckSyntax(conditional, i, ref firstStr);
                if (search > 0)
                {
                    i = search;
                    continue;
                }
                if ((conditional[i] == '=') || (conditional[i] == '!') || (conditional[i] == '<') || (conditional[i] == '>'))
                {
                    firstStr = ReturnLink(conditional.Substring(0, i));
                    secondStr = ReturnLink(conditional.Substring(i + 1, conditional.Length - i - 1));
                    operation = conditional[i].ToString();
                    break;
                }
            }
            bool boolResult = false;
            switch (operation)
            {
                case "=":
                    boolResult = firstStr == secondStr;
                    break;
                case "!":
                    boolResult = firstStr != secondStr;
                    break;
                case "<":
                    boolResult = Convert.ToInt32(firstStr) < Convert.ToInt32(secondStr);
                    break;
                case ">":
                    boolResult = Convert.ToInt32(firstStr) > Convert.ToInt32(secondStr);
                    break;
            }
            if (boolResult)
            {
                result = cTrue;
            }
            else
            {
                result = cFalse;
            }
            LastOperation = "( " + firstStr + " " + operation + " " + secondStr + " ? " + cTrue + " : " + cFalse + " ) = " + result;
            return result;
        }

        public string ReturnLinkOld(string str)
        {
            string link = "";
            try
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] == '"') //String
                    {
                        for (int a = 1; a < str.Length - i; a++)
                        {
                            if (str[i + a] == '"')
                            {
                                link += str.Substring(i + 1, a - 1);
                                i = i + a;
                                break;
                            }
                            if (!(a + 1 < str.Length - i)) //If last char in string
                            {
                                i = i + a;
                            }
                        }
                    }
                    else if (str[i] == '$') //Regexp
                    {
                        for (int a = 1; a < str.Length - i; a++)
                        {
                            if (!char.IsDigit(str[i + a]))
                            {
                                link += Regexps[Convert.ToInt32(str.Substring(i + 1, a - 1)) - 1];
                                i = i + a;
                                break;
                            }
                            if (!(a + 1 < str.Length - i)) //If last char in string
                            {
                                link += Regexps[Convert.ToInt32(str.Substring(i + 1, a)) - 1];
                            }
                        }
                    }
                }
            }
            catch
            {
                return "";
            }
            return link;
        }
    }
}