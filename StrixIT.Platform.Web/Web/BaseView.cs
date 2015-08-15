using StrixIT.Platform.Core;
using System.Web.Mvc;

namespace StrixIT.Platform.Web
{
    public class BasePage<T> : WebViewPage<T>
    {
        #region Private Fields

        private IConfiguration _config;
        private IUserContext _user;

        #endregion Private Fields

        #region Public Constructors

        public BasePage()
        {
            _user = DependencyInjector.TryGet<IUserContext>();
            _config = DependencyInjector.Get<IConfiguration>();
        }

        #endregion Public Constructors

        #region Public Properties

        public IConfiguration Configuration
        {
            get
            {
                return _config;
            }
        }

        public new IUserContext User
        {
            get
            {
                return _user;
            }
        }

        #endregion Public Properties

        #region Public Methods

        public override void Execute()
        {
        }

        #endregion Public Methods
    }
}