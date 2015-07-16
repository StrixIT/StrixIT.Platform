//-----------------------------------------------------------------------
// <copyright file="BindModelEvent.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Web.Mvc;
using StrixIT.Platform.Core;

namespace StrixIT.Platform.Web
{
    /// <summary>
    /// An event triggered when a model is bound to a view model by ASP.NET MVC.
    /// </summary>
    public class BindModelEvent : IPlatformEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BindModelEvent" /> class.
        /// </summary>
        /// <param name="controllerContext">The controller context for the bind event</param>
        /// <param name="modelBindingContext">The model binding context for the bind event</param>
        public BindModelEvent(ControllerContext controllerContext, ModelBindingContext modelBindingContext)
        {
            this.ControllerContext = controllerContext;
            this.ModelBindingContext = modelBindingContext;
        }

        /// <summary>
        ///  Gets the controller context for the bind event.
        /// </summary>
        public ControllerContext ControllerContext { get; private set; }

        /// <summary>
        ///  Gets the model binding context for the bind event.
        /// </summary>
        public ModelBindingContext ModelBindingContext { get; private set; }

        /// <summary>
        ///  Gets or sets a value indicating whether the model is bound by the event handler.
        /// </summary>
        public bool IsBound { get; set; }

        /// <summary>
        ///  Gets or sets the result value when the event is handled by the event handler.
        /// </summary>
        public object Result { get; set; }
    }
}