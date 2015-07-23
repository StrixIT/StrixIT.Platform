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
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace StrixIT.Platform.Testing
{
    /// <summary>
    /// A class to create a process in IIS Express.
    /// </summary>
    public class IISExpressProcess : IDisposable
    {
        private readonly string _pathToSite;
        private Process _iisProcess;

        public IISExpressProcess(string pathToSite)
        {
            this._pathToSite = pathToSite;
        }

        public int? PortNumber { get; set; }

        public void Start()
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo()
            {
                ErrorDialog = false,
                CreateNoWindow = true,
                UseShellExecute = false
            };

            if (!this.PortNumber.HasValue)
            {
                this.CaptureAvailablePortNumber();
            }

            processStartInfo.Arguments = string.Format("/path:\"{0}\" /port:{1}", (object)this._pathToSite, (object)this.PortNumber);

            string path = (!string.IsNullOrEmpty(processStartInfo.EnvironmentVariables["programfiles(x86)"]) ? processStartInfo.EnvironmentVariables["programfiles(x86)"] : processStartInfo.EnvironmentVariables["programfiles"]) + "\\IIS Express\\iisexpress.exe";
            
            if (!System.IO.File.Exists(path))
            {
                throw new FileNotFoundException(string.Format("Did not find iisexpress.exe at {0}. Ensure that IIS Express is installed to the default location.", (object)path));
            }

            processStartInfo.FileName = path;
            this._iisProcess = new Process();
            this._iisProcess.StartInfo = processStartInfo;
            this._iisProcess.Start();
        }

        public void Stop()
        {
            if (this._iisProcess == null || this._iisProcess.HasExited)
            {
                return;
            }

            EndProcess();
        }

        private void CaptureAvailablePortNumber()
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                socket.Bind((EndPoint)new IPEndPoint(IPAddress.Loopback, 0));
                this.PortNumber = new int?(((IPEndPoint)socket.LocalEndPoint).Port);
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool cleanupManaged)
        {
            if (this._iisProcess == null)
            {
                return;
            }

            EndProcess();
            GC.SuppressFinalize(this);
        }

        private void EndProcess()
        {
            this._iisProcess.CloseMainWindow();
            this._iisProcess.Kill();
            this._iisProcess.Dispose();
            this._iisProcess = (Process)null;
        }
    }
}