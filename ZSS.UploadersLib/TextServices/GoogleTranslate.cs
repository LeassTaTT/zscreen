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
using System.Linq;
using Newtonsoft.Json.Linq;

namespace UploadersLib.TextServices
{
    public class GoogleTranslate : Uploader
    {
        private const string APIURL = "https://www.googleapis.com/language/translate/v2";

        private string APIKey;

        public GoogleTranslate(string apiKey)
        {
            APIKey = apiKey;
        }

        public List<GoogleLanguage> GetLanguages()
        {
            string url = APIURL + "/languages";

            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("key", APIKey);
            args.Add("target", "en");

            string response = SendGetRequest(url, args);

            if (!string.IsNullOrEmpty(response))
            {
                try
                {
                    JToken jt = JObject.Parse(response).SelectToken("data.languages");
                    return jt.Select(x => new GoogleLanguage((string)x.SelectToken("language"), (string)x.SelectToken("name"))).ToList();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            return null;
        }

        public GoogleTranslateInfo TranslateText(string text, string targetLanguage, string sourceLanguage = null)
        {
            GoogleTranslateInfo translateInfo = new GoogleTranslateInfo();
            translateInfo.Text = text;
            translateInfo.SourceLanguage = sourceLanguage;
            translateInfo.TargetLanguage = targetLanguage;

            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("key", APIKey);
            if (!string.IsNullOrEmpty(sourceLanguage)) args.Add("source", sourceLanguage);
            args.Add("target", targetLanguage);
            args.Add("q", text);

            string response = SendGetRequest(APIURL, args);

            if (!string.IsNullOrEmpty(response))
            {
                try
                {
                    JToken jt = JObject.Parse(response).SelectToken("data.translations");
                    translateInfo.Result = (string)jt[0]["translatedText"];
                    if (string.IsNullOrEmpty(sourceLanguage))
                    {
                        translateInfo.SourceLanguage = (string)jt[0]["detectedSourceLanguage"];
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            return translateInfo;
        }

        public GoogleTranslateInfo TranslateText(GoogleTranslateInfo info)
        {
            return TranslateText(info.Text, info.TargetLanguage, info.SourceLanguage);
        }
    }

    public class GoogleTranslateInfo
    {
        public string Text { get; set; }
        public string SourceLanguage { get; set; }
        public string TargetLanguage { get; set; }
        public string Result { get; set; }
    }

    public class GoogleLanguage
    {
        public string Language { get; set; }
        public string Name { get; set; }

        public GoogleLanguage() { }

        public GoogleLanguage(string language, string name)
        {
            Language = language;
            Name = name;
        }
    }
}