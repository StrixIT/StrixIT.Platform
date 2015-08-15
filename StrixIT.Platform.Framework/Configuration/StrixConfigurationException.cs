using System;
using System.Runtime.Serialization;

namespace StrixIT.Platform.Framework
{
    [Serializable]
    public class StrixConfigurationException : Exception
    {
        #region Public Constructors

        public StrixConfigurationException()
        {
        }

        public StrixConfigurationException(string message) : base(message)
        {
        }

        public StrixConfigurationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        #endregion Public Constructors

        #region Protected Constructors

        protected StrixConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        #endregion Protected Constructors
    }
}