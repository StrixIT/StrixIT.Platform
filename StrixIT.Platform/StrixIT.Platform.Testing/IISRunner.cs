#region License
/*
** Code taken from SpecsForMvc to build a project and run it in IIS. Modified it to do the minimum
** required to run an integration test with Selenium.

Original license:

The MIT License

Copyright(c) 2011-2013 Matt Honeycutt - http://trycatchfail.com

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.Build.Utilities;

namespace StrixIT.Platform.Testing
{
    /// <summary>
    /// A class to publish a web aplication and run it in IIS Express.
    /// </summary>
    public class IISRunner : IDisposable
    {
        private IISExpressProcess _iisExpressProcess;
        private string _publishDir;
        private string _intermediateDir;

        public string ProjectPath { get; set; }

        public string Configuration { get; set; }

        public string Platform { get; set; }

        public bool CleanupPublishedFiles { get; set; }

        public string ApplicationHostConfigurationFile { get; set; }

        public string ProjectName { get; private set; }

        public string MSBuildOverride { get; set; }

        public string SolutionPath { get; set; }

        public int? PortNumber { get; set; }

        public string TemporaryDirectoryName { get; set; }

        public string OutputPath { get; set; }

        public string Startup()
        {
            this.ProjectName = !this.ProjectPath.Contains("\\") ? this.ProjectPath : this.ProjectPath.Substring(this.ProjectPath.LastIndexOf("\\") + 1);
            this.ProjectName = this.ProjectName.Replace(".csproj", string.Empty).Replace(".vbproj", string.Empty);
            this._publishDir = Path.Combine(Directory.GetCurrentDirectory(), this.TemporaryDirectoryName ?? "SpecsForMvc.TestSite");
            this._intermediateDir = Path.Combine(Directory.GetCurrentDirectory(), "SpecsForMvc.TempIntermediateDir");
            Dictionary<string, string> properties = new Dictionary<string, string>()
            {
                {
                    "DeployOnBuild",
                    "true"
                },
                {
                    "DeployTarget",
                    "Package"
                },
                {
                    "_PackageTempDir",
                    "\"" + this._publishDir + "\""
                },
                {
                    "BaseIntermediateOutputPath",
                    "\"" + this._intermediateDir + "\\\\\""
                },
                {
                    "AutoParameterizationWebConfigConnectionStrings",
                    "false"
                },
                {
                    "Platform",
                    this.Platform ?? "AnyCPU"
                },
                {
                    "SolutionDir",
                    "\"" + Path.GetDirectoryName(this.SolutionPath) + "\\\\\""
                }
            };

            if (!string.IsNullOrEmpty(this.Configuration))
            {
                properties.Add("Configuration", this.Configuration);
            }

            if (!string.IsNullOrEmpty(this.OutputPath))
            {
                properties.Add("OutputPath", "\"" + this.OutputPath + "\\\\\"");
            }

            this.PublishSite(properties);
            return this.StartIISExpress();
        }

        public void Shutdown()
        {
            if (this._iisExpressProcess == null)
            {
                return;
            }

            this._iisExpressProcess.Stop();

            if (this.CleanupPublishedFiles && Directory.Exists(this._publishDir))
            {
                this.SafelyRemoveDirectory(this._publishDir);
            }

            if (!this.CleanupPublishedFiles || !Directory.Exists(this._intermediateDir))
            {
                return;
            }

            this.SafelyRemoveDirectory(this._intermediateDir);
        }

        private string StartIISExpress()
        {
            this._iisExpressProcess = new IISExpressProcess(this._publishDir);
            this._iisExpressProcess.PortNumber = this.PortNumber;
            this._iisExpressProcess.Start();
            return "http://localhost:" + (object)this._iisExpressProcess.PortNumber;
        }

        private void PublishSite(Dictionary<string, string> properties)
        {
            string str1 = "/p:" + string.Join(";", Enumerable.Select<KeyValuePair<string, string>, string>((IEnumerable<KeyValuePair<string, string>>)properties, (Func<KeyValuePair<string, string>, string>)(kvp => kvp.Key + "=" + kvp.Value))) + " \"" + this.ProjectPath + "\"";
            string str2 = this.MSBuildOverride ?? this.GetPathToBuildToolsFile("msbuild.exe");
            Process process = new Process();

            process.StartInfo = new ProcessStartInfo()
            {
                FileName = str2,
                Arguments = str1,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            process.Start();
            string str3 = process.StandardOutput.ReadToEnd();
            string str4 = process.StandardError.ReadToEnd();
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                Console.WriteLine("***The publish failed.  Please ensure that your project compiles manually with MSBuild.***");
                Console.WriteLine("Here's some information to help you:");
                Console.WriteLine("MSBuild Arguments: ");
                Console.WriteLine(str1);
                Console.WriteLine("---------------------------------------");
                Console.WriteLine("MSBuild Output:");
                Console.WriteLine(str3);
                Console.WriteLine(str4);
                Console.WriteLine("---------------------------------------");
                throw new ApplicationException("Build failed.");
            }
        }

        private void SafelyRemoveDirectory(string targetDirectory)
        {
            try
            {
                Directory.Delete(targetDirectory, true);
            }
            catch (Exception)
            {
                Console.WriteLine("There was a problem deleting {0}.", (object)targetDirectory);
            }
        }

        private string GetPathToBuildToolsFile(string fileName)
        {
            string str = ToolLocationHelper.GetPathToDotNetFramework(TargetDotNetFrameworkVersion.Version40);

            if (str != null)
            {
                str = Path.Combine(str, fileName);

                if (!File.Exists(str))
                {
                    str = (string)null;
                }
            }

            return str;
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool cleanupManaged)
        {
            if (this._iisExpressProcess == null)
            {
                return;
            }

            _iisExpressProcess.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}