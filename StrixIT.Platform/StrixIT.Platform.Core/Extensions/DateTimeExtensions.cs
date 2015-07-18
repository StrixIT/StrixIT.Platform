#region Apache License
//-----------------------------------------------------------------------
// <copyright file="DateTimeExtensions.cs" company="StrixIT">
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
#endregion

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