//-----------------------------------------------------------------------
// <copyright file="IAuthenticationService.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;

namespace StrixIT.Platform.Web
{
    public interface IAuthenticationService
    {
        LoginUserResult LogOn(string email, string password);
        void LogOff(string email);
        void LogOff(string email, IDictionary<string, object> sessionValues);
    }
}
