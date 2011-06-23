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
using System.Collections.Generic;
using System.Threading;
using HelpersLib;

namespace HistoryLib
{
    public class HistoryManager
    {
        private XMLManager xml;

        public HistoryManager(string historyPath)
        {
            xml = new XMLManager(historyPath);
        }

        public bool AddHistoryItem(HistoryItem historyItem)
        {
            try
            {
                if (historyItem != null && !string.IsNullOrEmpty(historyItem.Filename) &&
                historyItem.DateTimeUtc != DateTime.MinValue &&
                (!string.IsNullOrEmpty(historyItem.URL) || !string.IsNullOrEmpty(historyItem.Filepath)))
                {
                    return xml.AddHistoryItem(historyItem);
                }
            }
            catch (Exception e)
            {
                StaticHelper.WriteException(e);
            }

            return false;
        }

        public List<HistoryItem> GetHistoryItems()
        {
            try
            {
                return xml.Load();
            }
            catch (Exception e)
            {
                StaticHelper.WriteException(e);
            }

            return new List<HistoryItem>();
        }

        public bool RemoveHistoryItem(HistoryItem historyItem)
        {
            xml.RemoveHistoryItem(historyItem);

            return false;
        }

        public static void AddHistoryItemAsync(string historyPath, HistoryItem historyItem)
        {
            WaitCallback thread = state =>
            {
                HistoryManager history = new HistoryManager(historyPath);
                history.AddHistoryItem(historyItem);
            };

            ThreadPool.QueueUserWorkItem(thread);
        }
    }
}