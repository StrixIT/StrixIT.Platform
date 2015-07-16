using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace StrixIT.Platform.Web
{
    public class PlatformModelBinder : IModelBinder
    {
        private IModelBinder _defaultBinder;

        public PlatformModelBinder(IModelBinder defaultBinder)
        {
            _defaultBinder = defaultBinder;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }

            object resultObject;

            if (controllerContext.HttpContext.Request.ContentType.StartsWith(WebConstants.APPLICATIONJSON, StringComparison.OrdinalIgnoreCase)
                && !bindingContext.ModelType.Equals(typeof(string)))
            {
                string modelString;
                var stream = controllerContext.HttpContext.Request.InputStream;
                stream.Seek(0, SeekOrigin.Begin);

                using (var reader = new StreamReader(stream))
                {
                    modelString = reader.ReadToEnd();
                }

                if (string.IsNullOrEmpty(modelString))
                {
                    return null;
                }

                resultObject = JsonConvert.DeserializeObject(modelString, bindingContext.ModelType);
            }
            else
            {
                resultObject = _defaultBinder.BindModel(controllerContext, bindingContext);
            }

            resultObject = EncodeStringProperties(resultObject);

            return resultObject;
        }

        private object EncodeStringProperties(object resultObject)
        {
            if (resultObject == null)
            {
                return resultObject;
            }

            var type = resultObject.GetType();

            if (type.Equals(typeof(string)))
            {
                return WebUtility.HtmlEncode((string)resultObject);
            }

            // Loop through all object properties and encode all string values.
            foreach (var property in resultObject.GetType().GetProperties())
            {
                if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType) && !property.PropertyType.Equals(typeof(string)))
                {
                    var enumerable = property.GetValue(resultObject) as IEnumerable;

                    if (enumerable != null)
                    {
                        foreach (var item in enumerable)
                        {
                            EncodeStringProperties(item);
                        }
                    }
                }
                else if (property.PropertyType.IsClass && !property.PropertyType.IsAbstract)
                {
                    EncodeStringProperties(property.GetValue(resultObject));
                }
            }

            return resultObject;
        }
    }
}