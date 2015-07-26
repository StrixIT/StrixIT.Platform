#region Apache License

//-----------------------------------------------------------------------
// <copyright file="GetContentEvent.cs" company="StrixIT">
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

using StrixIT.Platform.Core;
using System.Web.Mvc;

namespace StrixIT.Platform.Web
{
    /// <summary>
    /// An event to get custom content for the Html Content helper.
    /// </summary>
    public class GetContentEvent : IPlatformEvent
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GetContentEvent"/> class.
        /// </summary>
        /// <param name="helper">The HtmlHelper used</param>
        /// <param name="options">The display options passed to the content helper</param>
        public GetContentEvent(HtmlHelper helper, DisplayOptions options)
        {
            this.Helper = helper;
            this.Options = options;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets the HtmlHelper to use.
        /// </summary>
        public HtmlHelper Helper { get; private set; }

        /// <summary>
        /// Gets the display options passed to the Content helper.
        /// </summary>
        public DisplayOptions Options { get; private set; }

        /// <summary>
        /// Gets or sets the custom content result.
        /// </summary>
        public MvcHtmlString Result { get; set; }

        #endregion Public Properties
    }
}