#region Apache License

//-----------------------------------------------------------------------
// <copyright file="IFileSystemWrapper.cs" company="StrixIT">
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

using System.Collections.Generic;

namespace StrixIT.Platform.Core
{
    /// <summary>
    /// The interface for abstracting away the file system.
    /// </summary>
    public interface IFileSystemWrapper
    {
        #region Public Methods

        /// <summary>
        /// Clears the file delete queue.
        /// </summary>
        void ClearDeleteQueue();

        /// <summary>
        /// Clears the saved file queue.
        /// </summary>
        void ClearSavedQueue();

        /// <summary>
        /// Deletes a file.
        /// </summary>
        /// <param name="fullPath">The full path of the file</param>
        void DeleteFile(string fullPath);

        /// <summary>
        /// Gets an html template for the specified culture. Primarily used for mails, it reads the
        /// files in the specified directory with the specified name and culture extensions and then
        /// extracts the subject and body. It returns the template for the culture found, or the
        /// template for the default culture.
        /// </summary>
        /// <param name="directory">The template directory</param>
        /// <param name="templateName">The template file name</param>
        /// <param name="culture">
        /// The culture to read the template for. If that culture is not found, an error will be
        /// logged and the default culture template returned
        /// </param>
        /// <returns>The list of templates</returns>
        IList<TemplateData> GetHtmlTemplate(string directory, string templateName, string culture);

        /// <summary>
        /// Processes the delete queue and deletes all files marked for deletion. This is used to
        /// delete files only after dependend actions have been taken.
        /// </summary>
        /// <returns>
        /// True if all files in the delete queue were deleted successfully, false otherwise
        /// </returns>
        bool ProcessDeleteQueue();

        /// <summary>
        /// Deletes all files in the saved queue. This is used to delete files from disk when
        /// persisting their associated data to the database fails.
        /// </summary>
        /// <returns>True if all files in the saved queue were deleted successfully, false otherwise</returns>
        bool RemoveFilesInSavedQueue();

        /// <summary>
        /// Saves a file.
        /// </summary>
        /// <param name="fullPath">The full path of the file</param>
        /// <param name="data">The file data as an array of bytes</param>
        /// <returns>True if the file was saved successfully, false otherwise</returns>
        bool SaveFile(string fullPath, byte[] data);

        #endregion Public Methods
    }
}