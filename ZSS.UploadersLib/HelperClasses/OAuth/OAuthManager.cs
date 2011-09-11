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
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using HelpersLib;

namespace UploadersLib.HelperClasses
{
    public static class OAuthManager
    {
        private const string ParameterConsumerKey = "oauth_consumer_key";
        private const string ParameterSignatureMethod = "oauth_signature_method";
        private const string ParameterSignature = "oauth_signature";
        private const string ParameterTimestamp = "oauth_timestamp";
        private const string ParameterNonce = "oauth_nonce";
        private const string ParameterVersion = "oauth_version";
        private const string ParameterToken = "oauth_token";
        private const string ParameterTokenSecret = "oauth_token_secret";
        private const string ParameterVerifier = "oauth_verifier";
        private const string ParameterCallback = "oauth_callback";

        private const string PlainTextSignatureType = "PLAINTEXT";
        private const string HMACSHA1SignatureType = "HMAC-SHA1";
        private const string RSASHA1SignatureType = "RSA-SHA1";

        public static string GenerateQuery(string url, Dictionary<string, string> args, HttpMethod httpMethod, OAuthInfo oauth)
        {
            if (string.IsNullOrEmpty(oauth.ConsumerKey) || string.IsNullOrEmpty(oauth.ConsumerSecret))
            {
                throw new Exception("ConsumerKey or ConsumerSecret empty.");
            }

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add(ParameterVersion, oauth.OAuthVersion);
            parameters.Add(ParameterNonce, GenerateNonce());
            parameters.Add(ParameterTimestamp, GenerateTimestamp());
            parameters.Add(ParameterSignatureMethod, HMACSHA1SignatureType);
            parameters.Add(ParameterConsumerKey, oauth.ConsumerKey);

            string secret = null;

            if (!string.IsNullOrEmpty(oauth.UserToken) && !string.IsNullOrEmpty(oauth.UserSecret))
            {
                secret = oauth.UserSecret;
                parameters.Add(ParameterToken, oauth.UserToken);
            }
            else if (!string.IsNullOrEmpty(oauth.AuthToken) && !string.IsNullOrEmpty(oauth.AuthSecret))
            {
                secret = oauth.AuthSecret;
                parameters.Add(ParameterToken, oauth.AuthToken);

                if (!string.IsNullOrEmpty(oauth.AuthVerifier))
                {
                    parameters.Add(ParameterVerifier, oauth.AuthVerifier);
                }
            }

            if (args != null)
            {
                foreach (KeyValuePair<string, string> arg in args)
                {
                    parameters.Add(arg.Key, arg.Value);
                }
            }

            string normalizedUrl = NormalizeUrl(url);
            string normalizedParameters = NormalizeParameters(parameters);
            string signatureBase = GenerateSignatureBase(httpMethod, normalizedUrl, normalizedParameters);
            string signature = GenerateSignature(signatureBase, oauth.ConsumerSecret, secret);

            normalizedParameters += "&" + ParameterSignature + "=" + signature;

            return normalizedUrl + "?" + normalizedParameters;
        }

        public static string GetAuthorizationURL(string requestTokenResponse, OAuthInfo oauth, string authorizeURL, string callback = null)
        {
            string url = null;

            NameValueCollection args = HttpUtility.ParseQueryString(requestTokenResponse);

            if (args[ParameterToken] != null)
            {
                oauth.AuthToken = args[ParameterToken];
                url = string.Format("{0}?{1}={2}", authorizeURL, ParameterToken, oauth.AuthToken);

                if (!string.IsNullOrEmpty(callback))
                {
                    url += string.Format("&{0}={1}", ParameterCallback, ZAppHelper.URLEncode(callback));
                }

                if (args[ParameterTokenSecret] != null)
                {
                    oauth.AuthSecret = args[ParameterTokenSecret];
                }
            }

            return url;
        }

        public static bool ParseAccessTokenResponse(string accessTokenResponse, OAuthInfo oauth)
        {
            NameValueCollection args = HttpUtility.ParseQueryString(accessTokenResponse);

            if (args[ParameterToken] != null)
            {
                oauth.UserToken = args[ParameterToken];

                if (args[ParameterTokenSecret] != null)
                {
                    oauth.UserSecret = args[ParameterTokenSecret];

                    return true;
                }
            }

            return false;
        }

        private static string GenerateSignatureBase(HttpMethod httpMethod, string normalizedUrl, string normalizedParameters)
        {
            StringBuilder signatureBase = new StringBuilder();
            signatureBase.AppendFormat("{0}&", httpMethod.ToString().ToUpperInvariant());
            signatureBase.AppendFormat("{0}&", ZAppHelper.URLEncode(normalizedUrl));
            signatureBase.AppendFormat("{0}", ZAppHelper.URLEncode(normalizedParameters));
            return signatureBase.ToString();
        }

        private static string GenerateSignature(string signatureBase, string consumerSecret, string userSecret = null)
        {
            using (HMACSHA1 hmacsha1 = new HMACSHA1())
            {
                string key = string.Format("{0}&{1}", Uri.EscapeDataString(consumerSecret),
                    string.IsNullOrEmpty(userSecret) ? string.Empty : Uri.EscapeDataString(userSecret));

                hmacsha1.Key = Encoding.ASCII.GetBytes(key);

                byte[] dataBuffer = Encoding.ASCII.GetBytes(signatureBase);
                byte[] hashBytes = hmacsha1.ComputeHash(dataBuffer);

                string signature = Convert.ToBase64String(hashBytes);

                return ZAppHelper.URLEncode(signature);
            }
        }

        private static string GenerateTimestamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        private static string GenerateNonce()
        {
            return ZAppHelper.GetRandomAlphanumeric(12);
        }

        private static string NormalizeUrl(string url)
        {
            Uri uri;

            if (Uri.TryCreate(url, UriKind.Absolute, out uri))
            {
                string port = string.Empty;

                if (uri.Scheme == "http" && uri.Port != 80 ||
                    uri.Scheme == "https" && uri.Port != 443 ||
                    uri.Scheme == "ftp" && uri.Port != 20)
                {
                    port = ":" + uri.Port;
                }

                url = uri.Scheme + "://" + uri.Host + port + uri.AbsolutePath;
            }

            return url;
        }

        private static string NormalizeParameters(Dictionary<string, string> parameters)
        {
            return string.Join("&", parameters.OrderBy(x => x.Key).ThenBy(x => x.Value).Select(x => x.Key + "=" + ZAppHelper.URLEncode(x.Value)).ToArray());
        }
    }
}