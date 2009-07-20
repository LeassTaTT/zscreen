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
using System.Xml.Linq;

namespace ZSS.ImageUploadersLib
{
    public static class Extensions
    {
        public static string ElementValue(this XElement xe, string name)
        {
            XElement xeItem = xe.Element(name);
            if (xeItem != null)
            {
                return xeItem.Value;
            }
            else
            {
                return "";
            }
        }

        public static string AttributeValue(this XElement xe, string name)
        {
            XAttribute xeItem = xe.Attribute(name);
            if (xeItem != null)
            {
                return xeItem.Value;
            }
            else
            {
                return "";
            }
        }

        public static string AttributeFirstValue(this XElement xe, params string[] names)
        {
            string value;
            foreach (string name in names)
            {
                value = xe.AttributeValue(name);
                if (!string.IsNullOrEmpty(value)) return value;
            }
            return "";
        }
    }
}