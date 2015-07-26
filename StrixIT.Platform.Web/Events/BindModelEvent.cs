#region Apache License

//-----------------------------------------------------------------------
// <copyright file="BindModelEvent.cs" company="StrixIT">
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
using System.Web.Mvc;

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