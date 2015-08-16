using StrixIT.Platform.Core;
using StrixIT.Platform.Core.Environment;
using System.Web.Mvc;

namespace StrixIT.Platform.Web
{
    public class BasePage<T> : WebViewPage<T>
    {
        #region Private Fields

        private IEnvironment _environment;

        #endregion Private Fields

        #region Public Constructors

        public BasePage()
        {
            _environment = DependencyInjector.Get<IEnvironment>();
        }

        #endregion Public Constructors

        #region Public Properties

        public IEnvironment Platform
        {
            get
            {
                return _environment;
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