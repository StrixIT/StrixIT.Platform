//-----------------------------------------------------------------------
// <copyright file="IWebInitializer.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace StrixIT.Platform.Core
{
    /// <summary>
    /// An interface to initialize the web parts of components when the web platform is initialized.
    /// </summary>
    public interface IWebInitializer
    {
        /// <summary>
        /// Initializes the component for the web.
        /// </summary>
        void WebInitialize();
    }
}
