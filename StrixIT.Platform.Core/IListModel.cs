using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrixIT.Platform.Core
{
    public interface IListModel
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the entity type the data transfer object is for.
        /// </summary>
        Type EntityType { get; }

        #endregion Public Properties
    }
}