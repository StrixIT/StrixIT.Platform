using System.Net;

namespace StrixIT.Platform.Web
{
    public static class HtmlHelpers
    {
        #region Public Methods

        /// <summary>
        /// HTML decodes a text.
        /// </summary>
        /// <param name="text">The text to HTML decode</param>
        /// <param name="replaceDangerousCharacters">
        /// True if dangerous characters should be replaced by html-safe versions, false if not
        /// </param>
        /// <returns>The HTML decoded text</returns>
        public static string HtmlDecode(string text, bool replaceDangerousCharacters = true)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return text;
            }

            var decoded = WebUtility.HtmlDecode(text);

            if (replaceDangerousCharacters)
            {
                decoded = decoded.Replace("&", "&amp;")
                                 .Replace("<", "&lt;")
                                 .Replace(">", "&gt;")
                                 .Replace("\"", "&quot;")
                                 .Replace("\'", "&#39;")
                                 .Replace("/", "&#47;");
            }

            return decoded;
        }

        /// <summary>
        /// HTML encodes a text.
        /// </summary>
        /// <param name="text">The text to HTML encode</param>
        /// <returns>The HTML encoded text</returns>
        public static string HtmlEncode(string text)
        {
            return WebUtility.HtmlEncode(text);
        }

        #endregion Public Methods
    }
}