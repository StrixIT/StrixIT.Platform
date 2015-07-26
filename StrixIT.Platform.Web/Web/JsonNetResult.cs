#region Apache License

//-----------------------------------------------------------------------
// <copyright file="JsonNetResult.cs" company="StrixIT">
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

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Globalization;
using System.Text;
using System.Web.Mvc;

namespace StrixIT.Platform.Web
{
    public class JsonNetResult : ActionResult
    {
        #region Public Constructors

        public JsonNetResult(object data, string contentType, Encoding contentEncoding) : this(data, contentType, contentEncoding, JsonRequestBehavior.DenyGet)
        {
        }

        public JsonNetResult(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior requestBehavior)
        {
            this.Data = data;
            this.ContentType = contentType;
            this.ContentEncoding = contentEncoding;
            this.RequestBehavior = requestBehavior;
            this.SerializerSettings = new JsonSerializerSettings();
            this.SerializerSettings.Converters.Add(new StringEnumConverter());
            this.SerializerSettings.Converters.Add(new IsoDateTimeConverter() { DateTimeStyles = DateTimeStyles.AdjustToUniversal });
            this.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        #endregion Public Constructors

        #region Public Properties

        public Encoding ContentEncoding { get; private set; }

        public string ContentType { get; private set; }

        public object Data { get; private set; }

        public Formatting Formatting { get; set; }
        public JsonRequestBehavior RequestBehavior { get; private set; }

        public JsonSerializerSettings SerializerSettings { get; set; }

        #endregion Public Properties

        #region Public Methods

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var response = context.HttpContext.Response;

            if (response.StatusCode == 401)
            {
                return;
            }

            var httpMethod = context.HttpContext.Request.HttpMethod;

            if (this.RequestBehavior == JsonRequestBehavior.DenyGet && string.Equals(httpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("This data is only available for POST requests");
            }

            response.ContentType = !string.IsNullOrEmpty(this.ContentType) ? this.ContentType : WebConstants.APPLICATIONJSON;

            if (this.ContentEncoding != null)
            {
                response.ContentEncoding = this.ContentEncoding;
            }

            if (this.Data != null)
            {
                JsonTextWriter writer = new JsonTextWriter(response.Output) { Formatting = Formatting };
                JsonSerializer serializer = JsonSerializer.Create(this.SerializerSettings);

                serializer.Serialize(writer, this.Data);
                writer.Flush();
            }
        }

        #endregion Public Methods
    }
}