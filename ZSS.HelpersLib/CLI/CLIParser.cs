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

using System.Collections.Generic;

namespace HelpersLib.CLI
{
    public class CLIParser
    {
        private string input;
        private int index;

        public List<string> Parse(string text)
        {
            input = text;

            List<string> commands = new List<string>();

            string command = null;

            for (index = 0; index < input.Length; index++)
            {
                command = ParseUntilWhiteSpace();

                if (!string.IsNullOrEmpty(command))
                {
                    commands.Add(command);
                }
            }

            return commands;
        }

        private string ParseUntilWhiteSpace()
        {
            int start = index;

            for (; index < input.Length; index++)
            {
                if (char.IsWhiteSpace(input[index]))
                {
                    return input.Substring(start, index - start);
                }
                else if (input[index] == '"' && (index + 1) < input.Length)
                {
                    return ParseUntilDoubleQuotes();
                }
            }

            return input.Substring(start, index - start);
        }

        private string ParseUntilDoubleQuotes()
        {
            int start = ++index;

            for (; index < input.Length; index++)
            {
                if (input[index] == '"')
                {
                    return input.Substring(start, index - start);
                }
            }

            return input.Substring(start, index - start);
        }
    }
}