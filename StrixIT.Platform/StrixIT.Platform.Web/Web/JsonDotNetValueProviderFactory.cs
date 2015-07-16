//-----------------------------------------------------------------------
// <copyright file="JsonDotNetValueProviderFactory.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StrixIT.Platform.Web
{
    /// <summary>
    /// A Json.NET Value Provider for MVC. Taken from https://json.codeplex.com/discussions/347099.
    /// </summary>
    public sealed class JsonDotNetValueProviderFactory : ValueProviderFactory
    {
        public override IValueProvider GetValueProvider(ControllerContext controllerContext)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }

            if (!controllerContext.HttpContext.Request.ContentType.StartsWith(WebConstants.APPLICATIONJSON, StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            var streamReader = new StreamReader(controllerContext.HttpContext.Request.InputStream);
            var jsonReader = new JsonTextReader(streamReader);

            if (!jsonReader.Read())
            {
                return null;
            }

            var serializer = new JsonSerializer();
            serializer.Converters.Add(new ExpandoObjectConverter());
            serializer.Converters.Add(new IsoDateTimeConverter() { DateTimeStyles = DateTimeStyles.AdjustToUniversal });

            // use JSON.NET to deserialize object to a dynamic (expando) object
            object jsonObject;

            // if we start with a "[", treat this as an array
            if (jsonReader.TokenType == JsonToken.StartArray)
            {
                jsonObject = serializer.Deserialize<List<ExpandoObject>>(jsonReader);
            }
            else
            {
                jsonObject = serializer.Deserialize<ExpandoObject>(jsonReader);
            }

            // create a backing store to hold all properties for this deserialization
            var backingStore = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

            // add all properties to this backing store
            AddToBackingStore(backingStore, string.Empty, jsonObject);

            // return the object in a dictionary value provider so the MVC understands it
            return new DictionaryValueProvider<object>(backingStore, CultureInfo.CurrentCulture);
        }

        private static void AddToBackingStore(Dictionary<string, object> backingStore, string prefix, object value)
        {
            IDictionary<string, object> d = value as IDictionary<string, object>;
            if (d != null)
            {
                foreach (KeyValuePair<string, object> entry in d)
                {
                    AddToBackingStore(backingStore, MakePropertyKey(prefix, entry.Key), entry.Value);
                }
                return;
            }

            IList l = value as IList;
            if (l != null)
            {
                for (int i = 0; i < l.Count; i++)
                {
                    AddToBackingStore(backingStore, MakeArrayKey(prefix, i), l[i]);
                }
                return;
            }

            // primitive
            backingStore[prefix] = value;
        }

        private static string MakeArrayKey(string prefix, int index)
        {
            return prefix + "[" + index.ToString(CultureInfo.InvariantCulture) + "]";
        }

        private static string MakePropertyKey(string prefix, string propertyName)
        {
            return string.IsNullOrEmpty(prefix) ? propertyName : prefix + "." + propertyName;
        }
    }
}