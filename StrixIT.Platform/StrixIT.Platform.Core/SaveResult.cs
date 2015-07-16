//-----------------------------------------------------------------------
// <copyright file="SaveResult.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace StrixIT.Platform.Core
{
    /// <summary>
    /// A class to represent the result of a save action.
    /// </summary>
    /// <typeparam name="TModel">The type of the view model the save action is for</typeparam>
    public class SaveResult<TModel> where TModel : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SaveResult{TModel}" /> class.
        /// </summary>
        public SaveResult() : this(false, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveResult{TModel}" /> class.
        /// </summary>
        /// <param name="success">True if the save was successful, false otherwise</param>
        /// <param name="entity">The saved entity</param>
        public SaveResult(bool success, object entity)
        {
            this.Success = success;
            this.Entity = entity;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the save was successful;
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the saved entity.
        /// </summary>
        public object Entity { get; set; }

        /// <summary>
        /// Gets the model.
        /// </summary>
        public TModel Model { get { return this.Entity.Map<TModel>(); } }

        /// <summary>
        /// Gets or sets the save message.
        /// </summary>
        public string Message { get; set; }
    }
}