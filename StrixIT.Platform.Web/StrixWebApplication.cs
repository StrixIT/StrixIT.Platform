#region Apache License

//-----------------------------------------------------------------------
// <copyright file="StrixWebApplication.cs" company="StrixIT">
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
using StrixIT.Platform.Core;
using StrixIT.Platform.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

[assembly: PreApplicationStartMethod(typeof(WebAssemblyLoader), "LoadAssemblies")]

namespace StrixIT.Platform.Web
{
    public class StrixWebApplication : HttpApplication
    {
        #region Private Fields

        private static FileSystemWatcher _fileWatcher;
        private static bool _isRestarting = false;

        #endregion Private Fields

        #region Protected Methods

        protected void Application_AuthorizeRequest()
        {
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var httpService = DependencyInjector.Get<IHttpService>();
            httpService.CompressRequest();
        }

        protected void Application_End(object sender, EventArgs e)
        {
            TearDownFileWatcher();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var error = Server.GetLastError();
            Response.Filter.Dispose();
            Logger.Log(error.Message, error, LogLevel.Fatal);

            if (new HttpRequestWrapper(Request).IsAjaxRequest())
            {
                Server.ClearError();
                Response.StatusCode = 500;
                Response.TrySkipIisCustomErrors = true;
                return;
            }

            var environment = DependencyInjector.Get<IEnvironment>();

            if (environment.Configuration.CustomErrorsEnabled)
            {
                if (Response.StatusCode != 401)
                {
                    Server.ClearError();
                    string redirectUrl;

                    if (environment.Cultures.CurrentCultureCode.ToLower() == environment.Cultures.DefaultCultureCode.ToLower())
                    {
                        redirectUrl = string.Format("~/{0}", "Error");
                    }
                    else
                    {
                        redirectUrl = string.Format("~/{0}/{1}", environment.Cultures.CurrentCultureCode, "Error");
                    }

                    Response.Redirect(redirectUrl);
                }
            }
        }

        protected void Application_PostAcquireRequestState(object sender, EventArgs e)
        {
            var httpService = DependencyInjector.Get<IHttpService>();
            httpService.SetCultureForRequest();
        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected void Application_PreSendRequestHeaders(object source, EventArgs e)
        {
            var httpService = DependencyInjector.Get<IHttpService>();
            httpService.SetResponseHeaders();
        }

        protected void Session_End(object sender, EventArgs e)
        {
            var authenticationService = DependencyInjector.TryGet<IAuthenticationService>();

            if (authenticationService != null)
            {
                var session = this.Session;
                var email = (string)session[PlatformConstants.CURRENTUSEREMAIL];
                var dictionary = GetSessionDictionary(new HttpSessionStateWrapper(session));
                authenticationService.LogOff(email, dictionary);
            }
        }

        #endregion Protected Methods

        #region Private Methods

        protected void SetupFileWatcher()
        {
            var environment = DependencyInjector.Get<IEnvironment>();
            _fileWatcher = new FileSystemWatcher(Path.Combine(environment.WorkingDirectory, "Areas"));
            _fileWatcher.IncludeSubdirectories = true;
            _fileWatcher.Created += new FileSystemEventHandler(RestartApp);
            _fileWatcher.Deleted += new FileSystemEventHandler(RestartApp);
            _fileWatcher.Changed += new FileSystemEventHandler(RestartApp);
            _fileWatcher.Renamed += new RenamedEventHandler(RestartApp);
            _fileWatcher.EnableRaisingEvents = true;
        }

        // Todo: this code is also present in the Framework SessionService. Refactor to one
        //       location? Problem is that I must pass the session here.
        private static IDictionary<string, object> GetSessionDictionary(HttpSessionStateBase session)
        {
            var dictionary = new Dictionary<string, object>();
            var valuesToExclude = new string[] { PlatformConstants.CURRENTUSER.ToLower(), PlatformConstants.CURRENTUSEREMAIL.ToLower(), PlatformConstants.CURRENTUSERGROUPS.ToLower() };

            foreach (var key in session.Keys)
            {
                if (!valuesToExclude.Contains(key.ToString().ToLower()))
                {
                    var value = session[(string)key];
                    dictionary.Add((string)key, JsonConvert.SerializeObject(value));
                }
            }

            return dictionary;
        }

        private static void RestartApp(object source, FileSystemEventArgs e)
        {
            if (!_isRestarting)
            {
                Logger.Log("One or more files in the Areas folder have been created, changed, deleted or renamed. Restarting the application.");
                var cachePath = Path.Combine(HttpRuntime.CodegenDir, "UserCache");

                try
                {
                    Logger.Log("Delete the cache data for areas and routes.");
                    Directory.Delete(cachePath, true);
                }
                catch
                {
                    Logger.Log(string.Format("An error occurred while trying to delete the cache data for areas and routes at {0}.", cachePath));
                }

                Logger.Log("Trigger a restart by touching the web.config or the bin folder.");

                // Idea to restart taken from Orchard's DefaultHostEnvironment class (http://orchardproject.net/download).
                bool success = TryWriteBinFolder() || TryWriteWebConfig();
                _isRestarting = true;
            }
        }

        private static void TearDownFileWatcher()
        {
            _fileWatcher.Dispose();
        }

        private static bool TryWriteBinFolder()
        {
            try
            {
                var binMarker = HostingEnvironment.MapPath("~/bin/HostRestart");
                var markerPath = Path.Combine(binMarker, "marker.txt");
                Directory.CreateDirectory(binMarker);

                using (var stream = File.CreateText(markerPath))
                {
                    stream.WriteLine("Restart on '{0}'", DateTime.UtcNow);
                    stream.Flush();
                }

                File.Delete(markerPath);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool TryWriteWebConfig()
        {
            try
            {
                // In medium trust, "UnloadAppDomain" is not supported. Touch web.config to force an
                // AppDomain restart.
                File.SetLastWriteTimeUtc(HostingEnvironment.MapPath("~/web.config"), DateTime.UtcNow);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion Private Methods
    }
}