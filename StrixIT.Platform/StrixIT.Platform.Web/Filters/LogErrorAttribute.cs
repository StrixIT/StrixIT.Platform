//-----------------------------------------------------------------------
// <copyright file="LogErrorAttribute.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Web.Mvc;
using StrixIT.Platform.Core;

namespace StrixIT.Platform.Web
{
    /// <summary>
    /// This attribute is used to log errors that are thrown by mvc controllers.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class LogErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (!filterContext.ExceptionHandled)
            {
                var exception = filterContext.Exception;
                var request = filterContext.HttpContext.Request;
                var response = filterContext.HttpContext.Response;
                Logger.Log(exception.Message, exception, LogLevel.Fatal);

                if (request.IsAjaxRequest())
                {
                    filterContext.ExceptionHandled = true;
                    response.StatusCode = 500;
                    response.TrySkipIisCustomErrors = true;
                }
                else if (Helpers.CustomErrorsEnabled(request))
                {
                    filterContext.ExceptionHandled = true;
                    Helpers.Redirect(response, "Error");
                }
            }
        }
    }
}