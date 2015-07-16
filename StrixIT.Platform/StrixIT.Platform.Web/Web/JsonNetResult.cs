//-----------------------------------------------------------------------
// <copyright file="JsonNetResult.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Globalization;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace StrixIT.Platform.Web
{
    public class JsonNetResult : ActionResult
    {
        public JsonNetResult(object data, string contentType, Encoding contentEncoding) : this(data, contentType, contentEncoding, JsonRequestBehavior.DenyGet) { }

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

        public Encoding ContentEncoding { get; private set; }

        public string ContentType { get; private set; }

        public object Data { get; private set; }

        public JsonRequestBehavior RequestBehavior { get; private set; }

        public JsonSerializerSettings SerializerSettings { get; set; }

        public Formatting Formatting { get; set; }

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
    }
}