#region Apache License
//-----------------------------------------------------------------------
// <copyright file="LogErrorAttribute.cs" company="StrixIT">
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
#endregion

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