using StrixIT.Platform.Core;
using StrixIT.Platform.Core.Environment;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace StrixIT.Platform.Framework.Environment
{
    public class DefaultEnvironment : IEnvironment
    {
        #region Private Fields

        private static bool? _cmsActive;
        private static bool? _membershipActive;
        private IConfiguration _configuration;
        private ICultureService _cultures;
        private IMembershipSettings _membershipSettings;
        private HttpServerUtilityBase _server;
        private ISessionService _session;
        private IUserContext _user;

        #endregion Private Fields

        #region Public Constructors

        public DefaultEnvironment(IConfiguration configuration, ICultureService cultures, ISessionService session, IUserContext user, IMembershipSettings membershipSettings, HttpServerUtilityBase server)
        {
            _configuration = configuration;
            _cultures = cultures;
            _session = session;
            _user = user;
            _membershipSettings = membershipSettings;
            _server = server;
        }

        #endregion Public Constructors

        #region Public Properties

        public bool CmsActive
        {
            get
            {
                if (_cmsActive == null)
                {
                    _cmsActive = DependencyInjector.TryGet<ICmsService>() != null;
                }

                return _cmsActive.Value;
            }
        }

        public IConfiguration Configuration
        {
            get
            {
                return _configuration;
            }
        }

        public ICultureService Cultures
        {
            get
            {
                return _cultures;
            }
        }

        public IMembershipSettings Membership
        {
            get
            {
                return _membershipSettings;
            }
        }

        public bool MembershipActive
        {
            get
            {
                if (_membershipActive == null)
                {
                    _membershipActive = DependencyInjector.TryGet<IMembershipService>() != null;
                }

                return _membershipActive.Value;
            }
        }

        public ISessionService Session
        {
            get
            {
                return _session;
            }
        }

        public IUserContext User
        {
            get
            {
                return _user;
            }
        }

        public string WorkingDirectory
        {
            get
            {
                if (HttpRuntime.AppDomainAppVirtualPath != null)
                {
                    return HttpRuntime.AppDomainAppPath;
                }

                // If the HttpRuntime.AppDomainAppVirtualPath is null, we're not running in a web
                // context although the web environment is loaded. This is the case when running
                // code first migration scripts. Use the default environment working directory in
                // that case.
                return Core.Helpers.GetWorkingDirectory();
            }
        }

        #endregion Public Properties

        #region Public Methods

        public string GetVirtualPath(string physicalPath)
        {
            string virtualPath = physicalPath;

            if (physicalPath == null)
            {
                throw new ArgumentNullException("physicalPath");
            }

            bool isPhysical = Regex.Match(physicalPath, @"[a-zA-Z]:\\[(\w+.\\]{1,}").Success;

            if (isPhysical)
            {
                var root = WorkingDirectory;
                var pathInRoot = physicalPath.Replace(root, string.Empty);
                virtualPath = pathInRoot.Replace("\\", "/");

                if (virtualPath.StartsWith("/"))
                {
                    virtualPath = virtualPath.Substring(1);
                }
            }

            return virtualPath;
        }

        public string MapPath(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            if (!Path.IsPathRooted(path))
            {
                path = string.Format("~/{0}", path.Replace("\\", "/"));
            }

            bool isVirtual = !Regex.Match(path, @"[a-zA-Z]:\\[(\w+.\\]{1,}").Success;

            if (isVirtual)
            {
                path = _server.MapPath(path);
            }

            return path;
        }

        #endregion Public Methods
    }
}