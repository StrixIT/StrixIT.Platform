//-----------------------------------------------------------------------
// <copyright file="JsonStatusResult.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
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