//-----------------------------------------------------------------------
// <copyright file="AdminAreaRegistration.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Web.Mvc;

namespace StrixIT.Platform.Web
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return WebConstants.ADMIN;
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
        }
    }
}