using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrixIT.Platform.Core
{
    public interface IViewModel
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether the user can delete this entity.
        /// </summary>
        bool CanDelete { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user can edit this entity.
        /// </summary>
        bool CanEdit { get; set; }

        /// <summary>
        /// Gets or sets the entity type the data transfer object is for.
        /// </summary>
        Type EntityType { get; }

        #endregion Public Properties
    }
}