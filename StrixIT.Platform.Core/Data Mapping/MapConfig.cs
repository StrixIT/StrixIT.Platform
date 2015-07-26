#region Apache License

//-----------------------------------------------------------------------
// <copyright file="MapConfig.cs" company="StrixIT">
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
using System.Linq.Expressions;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// A class to create mapping configurations for use with data mapping.
    /// </summary>
    /// <typeparam name="TSource">The source type for the mapping</typeparam>
    /// <typeparam name="TDestination">The destination type for the mapping</typeparam>
    public class MapConfig<TSource, TDestination>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapConfig{TSource, TDestination}" /> class.
        /// </summary>
        public MapConfig()
        {
            this.MembersToIgnore = new List<Expression<Func<TDestination, object>>>();
            this.MembersToMap = new Dictionary<Expression<Func<TDestination, object>>, Expression<Func<TSource, object>>>();
        }

        /// <summary>
        /// Gets or sets the expression to specify which members to ignore when mapping.
        /// </summary>
        public IList<Expression<Func<TDestination, object>>> MembersToIgnore { get; set; }

        /// <summary>
        /// Gets or sets the expressions to map source members to destination members.
        /// </summary>
        public IDictionary<Expression<Func<TDestination, object>>, Expression<Func<TSource, object>>> MembersToMap { get; set; }

        /// <summary>
        /// Gets or sets the action to invoke when the mapping is done.
        /// </summary>
        public Action<object, object> AfterMapAction { get; set; }
    }
}