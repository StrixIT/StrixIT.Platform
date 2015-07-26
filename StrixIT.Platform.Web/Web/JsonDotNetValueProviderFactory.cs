#region Apache License

//-----------------------------------------------------------------------
// <copyright file="JsonDotNetValueProviderFactory.cs" company="StrixIT">
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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Web.Mvc;

namespace StrixIT.Platform.Web
{
    /// <summary>
    /// A Json.NET Value Provider for MVC. Taken from https://json.codeplex.com/discussions/347099.
    /// </summary>
    public sealed class JsonDotNetValueProviderFactory : ValueProviderFactory
    {
        #region Public Methods

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

        #endregion Public Methods

        #region Private Methods

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

        #endregion Private Methods
    }
}