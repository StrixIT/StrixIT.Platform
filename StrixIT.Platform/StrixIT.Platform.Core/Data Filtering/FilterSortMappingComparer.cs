//-----------------------------------------------------------------------
// <copyright file="FilterSortMappingComparer.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// A class to compare FilterSortMaps.
    /// </summary>
    public class FilterSortMappingComparer : EqualityComparer<FilterSortMap>
    {
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
    }
}