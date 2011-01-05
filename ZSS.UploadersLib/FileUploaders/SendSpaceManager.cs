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
using System.Collections.Generic;
using HelpersLib;

namespace UploadersLib.FileUploaders
{
    public static class SendSpaceManager
    {
        public static string Token;
        public static string SessionKey;
        public static DateTime LastSessionKey;
        public static AcctType AccountType;
        public static string Username;
        public static string Password;
        public static SendSpace.UploadInfo UploadInfo;

        public static List<string> PrepareUploadInfo(string username, string password)
        {
            SendSpace sendSpace = new SendSpace();

            try
            {
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    AccountType = AcctType.Anonymous;

                    UploadInfo = sendSpace.AnonymousUploadGetInfo();
                    if (UploadInfo == null) throw new Exception("UploadInfo is null.");
                }
                else
                {
                    AccountType = AcctType.User;
                    Username = username;
                    Password = password;

                    if (string.IsNullOrEmpty(Token))
                    {
                        Token = sendSpace.AuthCreateToken();
                        if (string.IsNullOrEmpty(Token)) throw new Exception("Token is null or empty.");
                    }
                    if (string.IsNullOrEmpty(SessionKey) || (FastDateTime.Now - LastSessionKey).Minutes > 30)
                    {
                        SessionKey = sendSpace.AuthLogin(Token, username, password).SessionKey;
                        if (string.IsNullOrEmpty(Token)) throw new Exception("SessionKey is null or empty.");
                        LastSessionKey = FastDateTime.Now;
                    }
                    UploadInfo = sendSpace.UploadGetInfo(SessionKey);
                    if (UploadInfo == null) throw new Exception("UploadInfo is null.");
                }
            }
            catch (Exception e)
            {
                if (sendSpace.Errors.Count > 0)
                {
                    Console.WriteLine(sendSpace.ToErrorString());
                }
                else
                {
                    Console.WriteLine(e.ToString());
                }
            }

            return sendSpace.Errors;
        }
    }
}