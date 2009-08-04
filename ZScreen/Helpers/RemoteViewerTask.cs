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

namespace ZScreenLib
{
    public class RemoteViewerTask
    {
        public RemoteViewerTask(Jobs job)
        {
            this.Job = job;
        }

        public enum Jobs
        {
            FETCH_LIST,
            VIEW_FILE
        }

        public enum ProgressType
        {
            ADD_FILE_TO_LISTBOX,
            ADDING_FILES_TO_LISTBOX,
            FETCHING_FILE,
            INCREMENT_PROGRESS,
            UPDATE_STATUS_BAR_TEXT,
            UPDATE_PROGRESS_MAX,
            VIEWING_FILE
        }

        public string RemoteFile { get; set; }
        public Jobs Job { get; private set; }
    }
}