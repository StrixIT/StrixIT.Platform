#region Apache License

//-----------------------------------------------------------------------
// <copyright file="JsonStatusResult.cs" company="StrixIT">
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

using System.Dynamic;
using System.Web.Mvc;

namespace StrixIT.Platform.Web
{
    /// <summary>
    /// A class to present Json post reponse results in a uniform way.
    /// </summary>
    public class JsonStatusResult : JsonResult
    {
        private dynamic _resultData = new ExpandoObject();

        public JsonStatusResult()
        {
            this._resultData.Success = false;
            this._resultData.Message = null;
            this._resultData.Data = null;
            base.Data = this._resultData;
        }

        /// <summary>
        /// Gets or sets the result data.
        /// </summary>
        public new object Data
        {
            get
            {
                return this._resultData;
            }

            set
            {
                this._resultData.Data = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the post request was successful.
        /// </summary>
        public bool Success
        {
            get
            {
                return this._resultData.Success;
            }

            set
            {
                this._resultData.Success = value;
            }
        }

        /// <summary>
        /// Gets or sets the message. If an error occurred on the server while processing the post request, an error message
        /// is stored here.
        /// </summary>
        public string Message
        {
            get
            {
                return this._resultData.Message;
            }

            set
            {
                this._resultData.Message = value;
            }
        }
    }
}