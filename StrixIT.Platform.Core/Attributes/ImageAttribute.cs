#region Apache License

//-----------------------------------------------------------------------
// <copyright file="ImageAttribute.cs" company="StrixIT">
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

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// An attribute to mark a property as an image property. This will have the platform create a
    /// thumbnail for it.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ImageAttribute : Attribute
    {
        #region Private Fields

        private int _height;
        private string _heightProperty;
        private string _idProperty;
        private bool _keepAspectRatio = true;
        private int _width;
        private string _widthProperty;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageAttribute"/> class.
        /// </summary>
        /// <param name="width">The width of the image</param>
        /// <param name="height">The height of the image</param>
        public ImageAttribute(int width, int height) : this(width, height, true, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageAttribute"/> class.
        /// </summary>
        /// <param name="width">The width of the image</param>
        /// <param name="height">The height of the image</param>
        /// <param name="keepAspectRatio">True if the aspect ratio must be maintained, false otherwise</param>
        public ImageAttribute(int width, int height, bool keepAspectRatio) : this(width, height, keepAspectRatio, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageAttribute"/> class.
        /// </summary>
        /// <param name="width">The width of the image</param>
        /// <param name="height">The height of the image</param>
        /// <param name="keepAspectRatio">True if the aspect ratio must be maintained, false otherwise</param>
        /// <param name="idProperty">The name of the file id property, if not default</param>
        public ImageAttribute(int width, int height, bool keepAspectRatio, string idProperty)
        {
            this._width = width;
            this._height = height;
            this._keepAspectRatio = keepAspectRatio;
            this._idProperty = idProperty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageAttribute"/> class.
        /// </summary>
        /// <param name="widthProperty">The property that returns the width of the image</param>
        /// <param name="heightProperty">The property that returns the height of the image</param>
        public ImageAttribute(string widthProperty, string heightProperty) : this(widthProperty, heightProperty, true, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageAttribute"/> class.
        /// </summary>
        /// <param name="widthProperty">The property that returns the width of the image</param>
        /// <param name="heightProperty">The property that returns the height of the image</param>
        /// <param name="keepAspectRatio">True if the aspect ratio must be maintained, false otherwise</param>
        public ImageAttribute(string widthProperty, string heightProperty, bool keepAspectRatio) : this(widthProperty, heightProperty, keepAspectRatio, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageAttribute"/> class.
        /// </summary>
        /// <param name="widthProperty">The property that returns the width of the image</param>
        /// <param name="heightProperty">The property that returns the height of the image</param>
        /// <param name="keepAspectRatio">True if the aspect ratio must be maintained, false otherwise</param>
        /// <param name="idProperty">The name of the file id property, if not default</param>
        public ImageAttribute(string widthProperty, string heightProperty, bool keepAspectRatio, string idProperty)
        {
            this._widthProperty = widthProperty;
            this._heightProperty = heightProperty;
            this._keepAspectRatio = keepAspectRatio;
            this._idProperty = idProperty;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets the height of the image.
        /// </summary>
        public int Height
        {
            get
            {
                return this._height;
            }
        }

        /// <summary>
        /// Gets the property that returns the height of the image.
        /// </summary>
        public string HeightProperty
        {
            get
            {
                return this._heightProperty;
            }
        }

        /// <summary>
        /// Gets the id property of the file on the entity.
        /// </summary>
        public string IdProperty
        {
            get
            {
                return this._idProperty;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the aspect ratio should be kept.
        /// </summary>
        public bool KeepAspectRatio
        {
            get
            {
                return this._keepAspectRatio;
            }
        }

        /// <summary>
        /// Gets the width of the image.
        /// </summary>
        public int Width
        {
            get
            {
                return this._width;
            }
        }

        /// <summary>
        /// Gets the property that returns the width of the image.
        /// </summary>
        public string WidthProperty
        {
            get
            {
                return this._widthProperty;
            }
        }

        #endregion Public Properties
    }
}