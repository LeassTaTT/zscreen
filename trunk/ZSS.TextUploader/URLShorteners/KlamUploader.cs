﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZSS.TextUploadersLib;
using ZSS.TextUploadersLib.Helpers;

namespace ZSS.TextUploaderLib.URLShorteners
{
    [Serializable]
    public sealed class KlamUploader : TextUploader
    {
        public const string Hostname = "kl.am";
        public const string APIKey = "a4e5a8de710d80db774a8264f4588ffb";

        public override object Settings
        {
            get
            {
                return (object)HostSettings;
            }
            set
            {
                HostSettings = (KlamUploaderSettings)value;
            }
        }

        public KlamUploaderSettings HostSettings = new KlamUploaderSettings();

        public KlamUploader()
        {
            HostSettings.URL = "http://kl.am/api/shorten/";
        }

        public override string Name
        {
            get { return Hostname; }
        }

        public override string TesterString
        {
            get { return "http://code.google.com/p/zscreen"; }
        }

        public override string UploadText(TextFile text)
        {
            if (!string.IsNullOrEmpty(text.LocalString))
            {
                Dictionary<string, string> arguments = new Dictionary<string, string>();
                arguments.Add("url", text.LocalString);
                arguments.Add("format", "text");
                arguments.Add("api_key", APIKey);
                
                return GetResponse2(HostSettings.URL, arguments);
            }
            return "";
        }

        [Serializable]
        public class KlamUploaderSettings
        {
            public string URL { get; set; }
        }
    }
}
