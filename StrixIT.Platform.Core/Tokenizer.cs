#region Apache License

//-----------------------------------------------------------------------
// <copyright file="Tokenizer.cs" company="StrixIT">
// Copyright 2015 StrixIT. Author R.G. Schurgers MA MSc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

#endregion Apache License

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// The tokenizer to replace tokens in texts.
    /// </summary>
    public static class Tokenizer
    {
        /// <summary>
        /// The tokens registered.
        /// </summary>
        private static ConcurrentDictionary<string, Func<string>> _registeredTokens = new ConcurrentDictionary<string, Func<string>>();

        /// <summary>
        /// Registers a function to replace a value for a token.
        /// </summary>
        /// <param name="name">The token (like [[TOKENNAME]])</param>
        /// <param name="tokenFunction">The function to get a replacement value for the token</param>
        public static void RegisterToken(string name, Func<string> tokenFunction)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null or whitespace", "name");
            }

            if (tokenFunction == null)
            {
                throw new ArgumentNullException("tokenFunction");
            }

            if (!_registeredTokens.ContainsKey(name.ToLower()))
            {
                _registeredTokens.GetOrAdd(name.ToLower(), tokenFunction);
            }
        }

        /// <summary>
        /// Registers a replacement value for a token.
        /// </summary>
        /// <param name="name">The token (like [[TOKENNAME]])</param>
        /// <param name="token">The replacement value</param>
        public static void RegisterToken(string name, string token)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null or whitespace", "name");
            }

            if (!_registeredTokens.ContainsKey(name.ToLower()))
            {
                var function = new Func<string>(() => { return token; });
                _registeredTokens.GetOrAdd(name.ToLower(), function);
            }
        }

        /// <summary>
        /// Replaces the tokens in a text using the registered tokens and the additional tokens specified.
        /// </summary>
        /// <param name="value">The text to replace the tokens in</param>
        /// <param name="tokens">The additional tokens to use, if any</param>
        /// <returns>The text with the tokens replaced by their replacement values</returns>
        public static string ReplaceTokens(string value, IDictionary<string, string> tokens)
        {
            string pattern = @"\[\[\w{0,}\|{0,1}\w{0,}\]\]";

            while (Regex.IsMatch(value, pattern))
            {
                bool tokensReplaced = false;
                var matches = Regex.Matches(value, pattern);

                foreach (Match match in matches)
                {
                    string token = null;

                    if (tokens != null && tokens.ContainsKey(match.Value))
                    {
                        token = tokens[match.Value];
                    }
                    else
                    {
                        if (_registeredTokens.ContainsKey(match.Value.ToLower()))
                        {
                            token = _registeredTokens[match.Value.ToLower()]();
                        }
                    }

                    if (token != null)
                    {
                        value = value.Replace(match.Value, token);
                        tokensReplaced = true;
                    }
                }

                if (!tokensReplaced)
                {
                    break;
                }
            }

            return value;
        }
    }
}