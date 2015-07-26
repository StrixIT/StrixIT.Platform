#region Apache License

//-----------------------------------------------------------------------
// <copyright file="SaveResult.cs" company="StrixIT">
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

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// A class to represent the result of a save action.
    /// </summary>
    /// <typeparam name="TModel">The type of the view model the save action is for</typeparam>
    public class SaveResult<TModel> where TModel : class
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveResult{TModel}"/> class.
        /// </summary>
        public SaveResult() : this(false, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveResult{TModel}"/> class.
        /// </summary>
        /// <param name="success">True if the save was successful, false otherwise</param>
        /// <param name="entity">The saved entity</param>
        public SaveResult(bool success, object entity)
        {
            this.Success = success;
            this.Entity = entity;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets the saved entity.
        /// </summary>
        public object Entity { get; set; }

        /// <summary>
        /// Gets or sets the save message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets the model.
        /// </summary>
        public TModel Model { get { return this.Entity.Map<TModel>(); } }

        /// <summary>
        /// Gets or sets a value indicating whether the save was successful;
        /// </summary>
        public bool Success { get; set; }

        #endregion Public Properties
    }
}