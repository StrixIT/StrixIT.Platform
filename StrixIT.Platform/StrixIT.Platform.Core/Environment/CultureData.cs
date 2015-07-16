//-----------------------------------------------------------------------
// <copyright file="CultureData.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//---------------------------------------------------------------------
namespace StrixIT.Platform.Core
{
    /// <summary>
    /// A class to hold culture data.
    /// </summary>
    public class CultureData
    {
        /// <summary>
        /// Gets or sets the culture code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the culture name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the culture native name.
        /// </summary>
        public string NativeName { get; set; }
    }
}