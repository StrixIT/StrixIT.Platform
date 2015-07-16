//-----------------------------------------------------------------------
// <copyright file="LoginUserResult.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace StrixIT.Platform.Web
{
    public class LoginUserResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether the login was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the message to display when the login was not successful.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the preferred culture of the user to use after logging in.
        /// </summary>
        public string PreferredCulture { get; set; }
    }
}
