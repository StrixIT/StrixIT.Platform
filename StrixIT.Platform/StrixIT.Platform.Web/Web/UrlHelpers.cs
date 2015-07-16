//-----------------------------------------------------------------------
// <copyright file="UrlHelpers.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;

namespace StrixIT.Platform.Web
{
    /// <summary>
    /// Extension methods for strings.
    /// </summary>
    public static class UrlHelpers
    {
        /// <summary>
        /// Creates a html/url safe string by replacing spaces by '-'and diacritics by basic ascii characters.
        /// </summary>
        /// <param name="text">The text</param>
        /// <returns>The encoded text</returns>
        public static string CreateCleanUrl(string text)
        {
            return CreateCleanUrl(text, null);
        }

        /// <summary>
        /// Creates a html/url safe string by replacing spaces by a replacement character and diacritics by basic ascii characters.
        /// </summary>
        /// <param name="text">The text</param>
        /// <param name="replaceChar">The character used to replace spaces. If omitted, '-' will be used.</param>
        /// <returns>The encoded text</returns>
        public static string CreateCleanUrl(string text, char? replaceChar)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return text;
            }

            if (replaceChar == null)
            {
                replaceChar = '-';
            }

            ResourceManager manager = new ResourceManager(typeof(Core.Resources.Diacritics));
            StringBuilder cleanText = new StringBuilder();

            // Decode the string first to get the original characters, then trim leading and trailing spaces from the path.
            text = Helpers.HtmlDecode(text, false).Trim();
            text = text.Replace(' ', replaceChar.Value);

            for (int i = 0; i < text.Count(); i++)
            {
                char value = text[i];
                if (((int)value) > 127)
                {
                    string baseValue = value.ToString();
                    string encodedValue = manager.GetString(baseValue.ToLower());
                    if (baseValue.Equals(baseValue.ToUpper(CultureInfo.CurrentCulture)))
                    {
                        if (encodedValue != null)
                        {
                            encodedValue = encodedValue.ToUpper(CultureInfo.CurrentCulture);
                        }
                        else
                        {
                            encodedValue = string.Empty;
                        }
                    }

                    cleanText.Append(encodedValue);
                }
                else if (value == replaceChar.Value

                    // Ascii number range
                         || (value >= 48 && value <= 57)

                    // Ascii Upper case range
                         || (value >= 65 && value <= 90)

                    // Ascii lower case range
                         || (value >= 97 && value <= 122))
                {
                    cleanText.Append(value);
                }
            }

            // Remove sequences of the replace char.
            text = Regex.Replace(cleanText.ToString(), "[-]{2,}", replaceChar.Value.ToString());

            // Remove the replacement character when the resulting string starts or ends with it
            if (text.StartsWith(replaceChar.ToString()))
            {
                text = text.Substring(1);
            }

            if (text.EndsWith(replaceChar.ToString()))
            {
                text = text.Substring(0, text.Length - 1);
            }

            return text.ToLowerInvariant();
        }

        /// <summary>
        /// Creates a unique url based on the specified name.
        /// </summary>
        /// <param name="query">The query for the entity type, needed to check the url uniqueness in the data source</param>
        /// <param name="name">The name to create an url from</param>
        /// <param name="idPropertyValue">The id property value of the entity type, needed to ignore the entity itself when checking the uniqueness of the url</param>
        /// <param name="urlProperty">The url property of the entity type</param>
        /// <param name="idProperty">The id property of the entity type, needed to ignore the entity itself when checking the uniqueness of the url</param>
        /// <returns>The unique url created using the specified name</returns>
        public static string CreateUniqueUrl(IQueryable query, string name, object idPropertyValue, string urlProperty = "Url", string idProperty = "Id")
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            string url = CreateCleanUrl(name);

            // Check whether the full path is unique. If not, append an index or increment the existing one.
            if (url != null)
            {
                var possibleMatches = query.Where(string.Format("{0}.ToLower().Contains(@0) AND !{1}.Equals(@1)", urlProperty, idProperty), url, idPropertyValue).OrderBy(urlProperty).Select(urlProperty);

                if (possibleMatches.Count() > 0)
                {
                    int newIndex = 0;

                    foreach (string possibleMatch in possibleMatches)
                    {
                        if (possibleMatch.LastIndexOf('-') == -1)
                        {
                            continue;
                        }

                        string pathPart = possibleMatch.Substring(0, possibleMatch.LastIndexOf('-'));

                        if (!pathPart.Equals(url, StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }

                        string indexPart = possibleMatch.Substring(possibleMatch.LastIndexOf('-') + 1);
                        int index;

                        if (!int.TryParse(indexPart, out index))
                        {
                            continue;
                        }

                        if (index > newIndex)
                        {
                            newIndex = index + 1;
                        }
                    }

                    if (newIndex == 0)
                    {
                        newIndex = 2;
                    }

                    url = url + "-" + newIndex.ToString();
                }
            }

            return url;
        }
    }
}