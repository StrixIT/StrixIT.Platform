﻿#region Apache License

//-----------------------------------------------------------------------
// <copyright file="FilterSortMappingComparer.cs" company="StrixIT">
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

#endregion Apache License

using System;
using System.Collections.Generic;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// A class to compare FilterSortMaps.
    /// </summary>
    public class FilterSortMappingComparer : EqualityComparer<FilterSortMap>
    {
        #region Public Methods

        public override bool Equals(FilterSortMap x, FilterSortMap y)
        {
            if (x == null)
            {
                throw new ArgumentNullException("x");
            }

            if (y == null)
            {
                throw new ArgumentNullException("y");
            }

            return x.FieldToMap.ToLower().Equals(y.FieldToMap.ToLower());
        }

        public override int GetHashCode(FilterSortMap obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            return obj.GetHashCode();
        }

        #endregion Public Methods
    }
}