//-----------------------------------------------------------------------
// <copyright file="IDataSource.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//---------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// The base interface for all data sources.
    /// </summary>
    public interface IDataSource
    {
        /// <summary>
        /// Gets a query for the specified entity type.
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <returns>The query</returns>
        IQueryable<T> Query<T>() where T : class;

        /// <summary>
        /// Saves one or more entities to the data source. To persist them, SaveChanges should be called as well.
        /// </summary>
        /// <typeparam name="T">The type of the entities to save</typeparam>
        /// <param name="entity">The entity or entities to save</param>
        /// <returns>The saved entity or entities</returns>
        T Save<T>(T entity) where T : class;

        /// <summary>
        /// Deletes one or more entities from the data source. To persist the delete, SaveChanges should be called as well.
        /// </summary>
        /// <typeparam name="T">The type of the entities to delete</typeparam>
        /// <param name="entity">The entity or entities to delete</param>
        void Delete<T>(T entity) where T : class;

        /// <summary>
        /// Saves all changes to the data source.
        /// </summary>
        void SaveChanges();
    }
}
