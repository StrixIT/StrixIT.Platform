#region Apache License

//-----------------------------------------------------------------------
// <copyright file="DriverExtensions.cs" company="StrixIT">
// Copyright 2015 StrixIT, author R.G. Schurgers MA MSc.
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

using OpenQA.Selenium.Remote;

namespace StrixIT.Platform.Testing
{
    /// <summary>
    /// Extensions for the Selenium Web Driver to make integration testing easier.
    /// </summary>
    public static class DriverExtensions
    {
        #region Public Methods

        /// <summary>
        /// Check whether the browser is at the specified url.
        /// </summary>
        /// <param name="driver">The web driver</param>
        /// <param name="url">The url to check for</param>
        /// <returns>True if the browser is at the specified url, false otherwise</returns>
        public static bool IsAt(this RemoteWebDriver driver, string url)
        {
            if (url == "/")
            {
                url = string.Empty;
            }
            else
            {
                url = "/" + url;
            }

            var currentUrl = driver.Url.EndsWith("/") ? driver.Url.Substring(0, driver.Url.Length - 1) : driver.Url;
            return currentUrl == TestManager.BaseUrl + url;
        }

        /// <summary>
        /// Log off.
        /// </summary>
        /// <param name="driver">The web drive</param>
        public static void LogOff(this RemoteWebDriver driver)
        {
            driver.NavigateTo("Account/Logoff");
        }

        /// <summary>
        /// Log on using the default credentials for the integration test run.
        /// </summary>
        /// <param name="driver">The web drive</param>
        public static void LogOn(this RemoteWebDriver driver)
        {
            LogOn(driver, TestManager.AuthenticationEmail, TestManager.AuthenticationPassword);
        }

        /// <summary>
        /// Log on using the specified credentials for the integration test run.
        /// </summary>
        /// <param name="driver">The web drive</param>
        /// <param name="email">The email to use</param>
        /// <param name="password">The password to use</param>
        public static void LogOn(this RemoteWebDriver driver, string email, string password)
        {
            driver.NavigateTo("Account/Logoff");
            driver.NavigateTo("Account/Login");
            driver.FindElementById("Email").SendKeys(email);
            driver.FindElementById("Password").SendKeys(password);
            driver.FindElementByTagName("form").Submit();
        }

        /// <summary>
        /// Navigate to the specified url
        /// </summary>
        /// <param name="driver">The web driver</param>
        /// <param name="url">The url to navigate to</param>
        public static void NavigateTo(this RemoteWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(TestManager.BaseUrl + "/" + url);
        }

        #endregion Public Methods
    }
}