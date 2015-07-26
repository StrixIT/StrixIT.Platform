#region Apache License

//-----------------------------------------------------------------------
// <copyright file="ICrudService.cs" company="StrixIT">
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

using System.Collections;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// The interface for a standard CRUD service, used with the Base CRUD controller to create a
    /// default, easy to use web interface.
    /// </summary>
    /// <typeparam name="TKey">The type of the entity key</typeparam>
    /// <typeparam name="TModel">The type of the entity dto</typeparam>
    public interface ICrudService<TKey, TModel> where TKey : struct where TModel : class
    {
        #region Public Methods

        /// <summary>
        /// Deletes the object with the specified id.
        /// </summary>
        /// <param name="id">The id of the object to delete</param>
        void Delete(TKey id);

        /// <summary>
        /// Deletes the object with the specified id.
        /// </summary>
        /// <param name="id">The id of the object to delete</param>
        /// <param name="saveChanges">True if the delete should be persisted, false otherwise</param>
        void Delete(TKey id, bool saveChanges);

        /// <summary>
        /// Checks whether an entity with the specified name already exists.
        /// </summary>
        /// <param name="name">The name to check for</param>
        /// <param name="id">The id of the entity to check the name for</param>
        /// <returns>True if an entity with the name already exists, false otherwise</returns>
        bool Exists(string name, TKey? id);

        /// <summary>
        /// Get a view model for an object by it's id. If the id is NULL, an empty model is returned.
        /// </summary>
        /// <param name="id">The object's id, or NULL for an empty model</param>
        /// <returns>The view model</returns>
        TModel Get(TKey? id);

        /// <summary>
        /// Get a list of object view models, using the filter specified.
        /// </summary>
        /// <param name="filter">The filter to use</param>
        /// <returns>The list of view models</returns>
        IEnumerable List(FilterOptions filter);

        /// <summary>
        /// Saves the data of a view model.
        /// </summary>
        /// <param name="model">The model to save the data for</param>
        /// <returns>The result of the save</returns>
        SaveResult<TModel> Save(TModel model);

        /// <summary>
        /// Saves the data of a view model.
        /// </summary>
        /// <param name="model">The model to save the data for</param>
        /// <param name="saveChanges">True if the save should be persisted, false otherwise</param>
        /// <returns>The result of the save</returns>
        SaveResult<TModel> Save(TModel model, bool saveChanges);

        #endregion Public Methods
    }
}