//-----------------------------------------------------------------------
// <copyright file="DateTimeExtensions.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// Platform extensions for date time objects.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Checks whether the datetime occurs at or after the start date and before the end date, when specified.
        /// </summary>
        /// <param name="dateToCheck">The datetime to check</param>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        /// <returns>True if the datetime occurs at or after the start date and before the end date, false otherwise</returns>
        public static bool IsInRange(this DateTime dateToCheck, DateTime startDate, DateTime? endDate)
        {
            return dateToCheck >= startDate && (endDate == null || dateToCheck < endDate.Value);
        }
    }
}