//-----------------------------------------------------------------------
// <copyright file="IHandlePlatformEvent.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace StrixIT.Platform.Core
{
    /// <summary>
    /// An interface to handle events raised by the platform.
    /// </summary>
    /// <typeparam name="T">The type of the event the implementor handles</typeparam>
    public interface IHandlePlatformEvent<T> where T : IPlatformEvent
    {
        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="args">The event to handle</param>
        void Handle(T args);
    }
}