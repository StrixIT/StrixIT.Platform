//-----------------------------------------------------------------------
// <copyright file="IInitializer.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace StrixIT.Platform.Core
{
    /// <summary>
    /// The initializer interface, used to initialize components on application startup.
    /// </summary>
    public interface IInitializer
    {
        /// <summary>
        /// Initializes the component.
        /// </summary>
        void Initialize();
    }
}
