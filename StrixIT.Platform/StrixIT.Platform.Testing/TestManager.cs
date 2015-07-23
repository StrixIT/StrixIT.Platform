#region Apache License
//-----------------------------------------------------------------------
// <copyright file="TestManager.cs" company="StrixIT">
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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using StrixIT.Platform.Core;

namespace StrixIT.Platform.Testing
{
    /// <summary>
    /// This class manages integration test runs.
    /// </summary>
    public class TestManager
    {
        private static IISRunner _basicHost;
        private static RemoteWebDriver _driver;
        private static List<DbContext> _dataContexts = new List<DbContext>();

        /// <summary>
        /// Gets the base url of the site during the integration test run.
        /// </summary>
        public static string BaseUrl { get; private set; }

        /// <summary>
        /// Gets or sets the email to use by default when authenticating.
        /// </summary>
        public static string AuthenticationEmail { get; set; }

        /// <summary>
        /// Gets or sets the password to use by default when authenticating.
        /// </summary>
        public static string AuthenticationPassword { get; set; }

        /// <summary>
        /// Gets the Selenium Web Driver active for the integration tests.
        /// </summary>
        public static RemoteWebDriver Browser
        {
            get
            {
                return _driver;
            }
        }

        /// <summary>
        /// Gets a read-only collection of data contexts involved in the integration tests. When the tests are done, the databases that
        /// back these contexts are dropped.
        /// </summary>
        public static ReadOnlyCollection<DbContext> DataContexts
        {
            get
            {
                return _dataContexts.AsReadOnly();
            }
        }

        /// <summary>
        /// Adds a data context to the list of contexts for which the databases are dropped when the tests are done.
        /// </summary>
        /// <param name="context">The data context to add</param>
        public static void UseDataContext(DbContext context)
        {
            if (!_dataContexts.Any(c => c.GetType().Equals(context.GetType())))
            {
                _dataContexts.Add(context);
            }
        }

        /// <summary>
        /// Setup an integration test run for the web project specified by building the target project using the
        /// specified configuration, launching IIS Express and starting the web driver for the requested browser.
        /// </summary>
        /// <param name="projectName">The test project name</param>
        /// <param name="browser">The browser to use</param>
        /// <param name="configuration">The build configuration to use</param>
        public static void SetupTestRun(string projectName, TestBrowser browser, string configuration = "Test")
        {
            StrixPlatform.Environment = new DefaultEnvironment();
            var projectDirectory = new DirectoryInfo(GetProjectDirectory(projectName));
            _basicHost = new IISRunner();
            _basicHost.ProjectPath = projectDirectory.EnumerateFiles("*.csproj").First().FullName;
            _basicHost.SolutionPath = projectDirectory.Parent.EnumerateFiles("*.sln").First().FullName;
            _basicHost.CleanupPublishedFiles = true;
            _basicHost.Configuration = configuration;

            switch (browser)
            {
                case TestBrowser.Chrome:
                    {
                        _driver = new ChromeDriver();
                    }
                    break;

                case TestBrowser.FireFox:
                    {
                        _driver = new FirefoxDriver();
                    }
                    break;

                case TestBrowser.InternetExplorer:
                    {
                        _driver = new InternetExplorerDriver();
                    }
                    break;
            }

            BaseUrl = _basicHost.Startup();
            _driver.Url = BaseUrl;
        }

        /// <summary>
        /// Cleans up an integration test run, closing the browser and dropping the test databases.
        /// </summary>
        public static void TearDownTestRun()
        {
            _driver.Quit();
            _driver = null;
            _basicHost.Shutdown();

            foreach (var context in DataContexts)
            {
                context.Database.Delete();
            }
        }

        private static string GetProjectDirectory(string projectName)
        {
            DirectoryInfo directoryInfo;

            for (directoryInfo = new DirectoryInfo(Environment.CurrentDirectory); directoryInfo.GetFiles("*.sln").Length == 0; directoryInfo = directoryInfo.Parent)
            {
                if (directoryInfo.Parent == null)
                {
                    throw new InvalidOperationException(string.Format("Unable to find solution file, traversed up to '{0}'.  Your test runner may be using shadow-copy to create a clone of your working directory.  The Project.Named method does not currently support this behavior.  You must manually specify the path to the project to be tested instead.", (object)directoryInfo.FullName));
                }
            }

            return Path.Combine(directoryInfo.FullName, projectName);
        }
    }
}