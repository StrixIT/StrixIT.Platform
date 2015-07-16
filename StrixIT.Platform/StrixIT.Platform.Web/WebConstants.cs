//-----------------------------------------------------------------------
// <copyright file="WebConstants.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
