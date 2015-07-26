//-----------------------------------------------------------------------
// <copyright file="FileSystemWrapper.cs" company="StrixIT">
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace StrixIT.Platform.Core
{
    public class FileSystemWrapper : IFileSystemWrapper
    {
        #region Private Fields

        private List<string> _deleteQueue = new List<string>();
        private List<string> _savedQueue = new List<string>();

        #endregion Private Fields

        #region Public Methods

        public void ClearDeleteQueue()
        {
            this._deleteQueue.Clear();
        }

        public void ClearSavedQueue()
        {
            this._savedQueue.Clear();
        }

        public void DeleteFile(string fullPath)
        {
            if (string.IsNullOrWhiteSpace(fullPath))
            {
                throw new ArgumentException("Invalid full path");
            }

            this._deleteQueue.Add(fullPath);
        }

        public IList<TemplateData> GetHtmlTemplate(string directory, string templateName, string culture)
        {
            var baseTemplatePath = string.Format("{0}*.html", templateName);
            var results = new List<TemplateData>();
            var fileNames = Directory.GetFiles(directory, baseTemplatePath).Where(f => f.ToLower().Split('\\').Last() == templateName.ToLower() + ".html" || f.ToLower().Split('\\').Last().StartsWith(templateName.ToLower() + "_"));

            foreach (var file in fileNames)
            {
                if (!System.IO.File.Exists(file))
                {
                    Logger.Log(string.Format("No {0} template found.", templateName), LogLevel.Error);
                    return results;
                }

                var allText = System.IO.File.ReadAllText(file);
                var templateCulture = file.Split('\\').Last().Contains("_") ? file.Split('\\').Last().Split('_').Last().Split('.').First() : null;

                string subject = null;
                var subjectLineMatch = Regex.Match(allText, "<subject>(.+?)</subject>");

                if (subjectLineMatch != null && subjectLineMatch.Length > 1)
                {
                    subject = subjectLineMatch.Groups[1].ToString();
                    allText = allText.Replace(subjectLineMatch.ToString(), string.Empty);

                    if (allText.StartsWith("\r\n"))
                    {
                        allText = allText.Substring(2);
                    }
                    else if (allText.StartsWith("\n"))
                    {
                        allText = allText.Substring(1);
                    }
                }
                else
                {
                    Logger.Log(string.Format("No subject found in mail {0} for culture {1}.", templateName, templateCulture), LogLevel.Info);
                }

                results.Add(new TemplateData(templateCulture, subject, allText));
            }

            // Filter the list for the required culture.
            if (!string.IsNullOrWhiteSpace(culture))
            {
                var cultureTemplate = results.FirstOrDefault(r => r.Culture != null && r.Culture.ToLower() == culture.ToLower());

                if (cultureTemplate == null)
                {
                    // If the required culture template is not found, return the default culture template.
                    Logger.Log(string.Format("No {0} template found for culture {1}.", templateName, culture), LogLevel.Error);
                    cultureTemplate = results.FirstOrDefault(r => r.Culture == null);
                }

                if (cultureTemplate == null)
                {
                    return new List<TemplateData>();
                }
                else
                {
                    cultureTemplate.Culture = StrixPlatform.DefaultCultureCode;
                    return new List<TemplateData>() { cultureTemplate };
                }
            }

            foreach (var template in results)
            {
                if (string.IsNullOrWhiteSpace(template.Culture))
                {
                    template.Culture = StrixPlatform.DefaultCultureCode;
                }
            }

            return results;
        }

        public bool ProcessDeleteQueue()
        {
            return this.ProcessQueue(this._deleteQueue);
        }

        public bool RemoveFilesInSavedQueue()
        {
            return this.ProcessQueue(this._savedQueue);
        }

        public bool SaveFile(string fullPath, byte[] data)
        {
            if (string.IsNullOrWhiteSpace(fullPath))
            {
                throw new ArgumentException("Invalid full path");
            }

            if (data == null || data.Length == 0)
            {
                throw new ArgumentException("Invalid data");
            }

            bool result = true;
            fullPath = StrixPlatform.Environment.MapPath(fullPath);
            string fileDirectory = Path.GetDirectoryName(fullPath);

            if (!Directory.Exists(fileDirectory))
            {
                Directory.CreateDirectory(fileDirectory);
            }

            Stream outputStream = null;

            try
            {
                outputStream = GetStream(fullPath);
                outputStream.Write(data, 0, data.Length);
                this._savedQueue.Add(fullPath);
            }
            catch (Exception ex)
            {
                Logger.Log(string.Format("An error occurred while saving file {0}.", fullPath), ex, LogLevel.Error);
                throw;
            }
            finally
            {
                if (outputStream != null)
                {
                    outputStream.Dispose();
                }
            }

            return result;
        }

        #endregion Public Methods

        #region Private Methods

        private static Stream GetStream(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            FileStream outputStream = fileInfo.OpenWrite();
            return outputStream;
        }

        private bool ProcessQueue(List<string> queue)
        {
            bool result = true;

            foreach (string file in queue)
            {
                try
                {
                    System.IO.File.Delete(file);
                }
                catch (Exception ex)
                {
                    Logger.Log(string.Format("An error occurred while deleting file {0}", file), ex, LogLevel.Error);
                    throw;
                }
            }

            queue.Clear();
            return result;
        }

        #endregion Private Methods
    }
}