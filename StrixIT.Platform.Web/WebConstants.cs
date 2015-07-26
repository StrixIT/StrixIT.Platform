#region Apache License

//-----------------------------------------------------------------------
// <copyright file="WebConstants.cs" company="StrixIT">
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

namespace StrixIT.Platform.Web
{
    public static class WebConstants
    {
        public static readonly string APPLICATIONJSON = "application/json";
        public static readonly string RESOURCEREGEX = @"\.\w{2,4}$";
        public static readonly string CULTUREREGEX = "[a-zA-Z]{2}|/[a-zA-Z]{2}-[a-zA-Z]{2}";
        public static readonly string IFRAMEMODEHEADER = "X-Frame-Options";
        public static readonly string APPLICATIONNAME = "ApplicationName";
        public static readonly string PARTIALSCRIPTS = "PartialScripts";
        public static readonly string PARTIALSTYLES = "PartialStyles";
        public static readonly string ADMIN = "admin";
    }
}