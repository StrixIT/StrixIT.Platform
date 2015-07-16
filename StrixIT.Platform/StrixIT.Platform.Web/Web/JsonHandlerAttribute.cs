//-----------------------------------------------------------------------
// <copyright file="JsonHandlerAttribute.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Web.Mvc;

namespace StrixIT.Platform.Web
{
    /// <summary>
    /// A filter to return a Json.NET result.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class JsonHandlerAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            var jsonResult = filterContext.Result as JsonResult;

            if (jsonResult != null)
            {
                filterContext.Result = new JsonNetResult(jsonResult.Data, jsonResult.ContentType, jsonResult.ContentEncoding, jsonResult.JsonRequestBehavior);
            }

            base.OnActionExecuted(filterContext);
        }
    }
}
