#region Apache License

//-----------------------------------------------------------------------
// <copyright file="BaseCrudController.cs" company="StrixIT">
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

using StrixIT.Platform.Core;
using System;
using System.ComponentModel;
using System.Web;
using System.Web.Mvc;

namespace StrixIT.Platform.Web
{
    /// <summary>
    /// The base CRUD controller for the StrixIT Platform.
    /// </summary>
    /// <typeparam name="TKey">The type of the primary key of the entity</typeparam>
    /// <typeparam name="TModel">The type of the view model of the entity</typeparam>
    public abstract class BaseCrudController<TKey, TModel> : BaseController
        where TKey : struct
        where TModel : class, IViewModel
    {
        #region Protected Fields

        /// <summary>
        /// The CRUD service used. Exposed as a protected field to be able to use an upcast in
        /// specialized controllers more easily.
        /// </summary>
        protected ICrudService<TKey, TModel> _service;

        #endregion Protected Fields

        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCrudController{TKey,TModel}"/> class.
        /// </summary>
        /// <param name="service">The CRUD service to use</param>
        protected BaseCrudController(IEnvironment environment, ICrudService<TKey, TModel> service)
            : base(environment)
        {
            this._service = service;
        }

        #endregion Protected Constructors

        #region Public Methods

        /// <summary>
        /// Checks whether an entity with the same name already exists.
        /// </summary>
        /// <param name="value">The name to check for</param>
        /// <param name="id">The id of the current entity</param>
        /// <returns>
        /// True if no entity other than the one the id was supplied for with the current name
        /// exists, false otherwise
        /// </returns>
        [HttpPost]
        public virtual JsonResult CheckName(string value, TKey? id)
        {
            return new JsonStatusResult() { Success = !this._service.Exists(value, id) };
        }

        /// <summary>
        /// Deletes an entity from the database.
        /// </summary>
        /// <param name="id">The entity id</param>
        /// <returns>True if the delete was successful, false otherwise</returns>
        public virtual JsonResult Delete(TKey id)
        {
            var model = this._service.Get(id);
            JsonStatusResult result = new JsonStatusResult();

            if (model == null)
            {
                result.Message = string.Format(Core.Resources.DefaultInterface.ItemNotFound, typeof(TModel).Name.ToLower());
                return result;
            }

            try
            {
                this._service.Delete(id);
                result.Success = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ex, LogLevel.Error);

                if (typeof(TModel).HasProperty("Name"))
                {
                    result.Message = string.Format(Core.Resources.DefaultInterface.ErrorDeletingItemWithName, typeof(TModel).Name.ToLower(), model.GetPropertyValue("Name"));
                }
                else
                {
                    result.Message = string.Format(Core.Resources.DefaultInterface.ErrorDeletingItem, typeof(TModel).Name.ToLower());
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the details view.
        /// </summary>
        /// <returns>The details view</returns>
        public virtual ActionResult Details()
        {
            ViewBag.ModelType = typeof(TModel);
            return this.View("Details");
        }

        /// <summary>
        /// Gets the edit view.
        /// </summary>
        /// <returns>The edit view</returns>
        public virtual ActionResult Edit()
        {
            ViewBag.ModelType = typeof(TModel);
            this.SetReturnUrl(typeof(TModel));
            return this.View("Edit");
        }

        [HttpPost]
        public virtual JsonResult Edit(TModel model)
        {
            var result = new JsonStatusResult();

            if (model == null)
            {
                return null;
            }

            var saveResult = this._service.Save(model);

            if (!saveResult.Success)
            {
                if (typeof(TModel).HasProperty("Name"))
                {
                    result.Message = string.Format(Core.Resources.DefaultInterface.ErrorSavingItemWithName, typeof(TModel).Name.ToLower(), model.GetPropertyValue("Name"));
                }
                else
                {
                    result.Message = string.Format(Core.Resources.DefaultInterface.ErrorSavingItem, typeof(TModel).Name.ToLower());
                }
            }
            else
            {
                result.Data = saveResult.Model;
                result.Success = true;
            }

            return result;
        }

        /// <summary>
        /// Gets a view model for an entity using the entity's id.
        /// </summary>
        /// <param name="id">The id of the entity to get the view model for</param>
        /// <returns>The view model</returns>
        public virtual ActionResult Get(string id)
        {
            var key = this.GetKey(id);
            return this.Json(this._service.Get(key));
        }

        /// <summary>
        /// Gets the Index view, which has the template for the content list.
        /// </summary>
        /// <returns>The index view</returns>
        public abstract ActionResult Index();

        /// <summary>
        /// Gets a JSON list of view models.
        /// </summary>
        /// <param name="options">The filter to use</param>
        /// <returns>The list of view models</returns>
        public virtual ActionResult List(FilterOptions options)
        {
            if (typeof(TModel).HasProperty("SortOrder") && options != null && options.Sort.IsEmpty())
            {
                options.Sort.Add(new SortField { Field = "SortOrder", Dir = "desc" });
            }

            var entries = this._service.List(options);
            return this.Json(entries.DataRecords(options));
        }

        #endregion Public Methods

        #region Protected Methods

        protected TKey? GetKey(string id)
        {
            var converter = TypeDescriptor.GetConverter(typeof(TKey?));
            var key = (TKey?)converter.ConvertFromInvariantString(id);
            return key;
        }

        protected void SetReturnUrl(Type type)
        {
            string returnUrl = this.Request.UrlReferrer != null ? this.Request.UrlReferrer.ToString() : null;
            var qscollection = HttpUtility.ParseQueryString(this.Request.Url.Query);
            var hash = qscollection["url"];

            if (returnUrl != null)
            {
                returnUrl = returnUrl.ToLower().IndexOf(string.Format("{0}", type.Name.ToLower().Replace("viewmodel", string.Empty))) == -1
                            && returnUrl.ToLower().IndexOf(string.Format("{0}/index", type.Name.ToLower().Replace("viewmodel", string.Empty))) == -1
                            && returnUrl.ToLower().IndexOf(string.Format("{0}/view", type.Name.ToLower().Replace("viewmodel", string.Empty))) == -1 ? returnUrl : null;
            }

            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = returnUrl + "#" + hash;
            }

            this.ViewBag.ReturnUrl = returnUrl;
        }

        #endregion Protected Methods
    }
}