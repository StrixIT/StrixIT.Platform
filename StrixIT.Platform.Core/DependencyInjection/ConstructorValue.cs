using System;
using System.Linq.Expressions;

namespace StrixIT.Platform.Core.DependencyInjection
{
    public class ConstructorValue<T>
    {
        #region Public Constructors

        public ConstructorValue(string name, T value)
        {
            Name = name;
            Value = value;
        }

        public ConstructorValue(string name, Expression<Func<T>> objectFactory)
        {
            Name = name;
            ObjectFactory = objectFactory;
        }

        #endregion Public Constructors

        #region Public Properties

        public string Name { get; private set; }
        public Expression<Func<T>> ObjectFactory { get; private set; }
        public T Value { get; private set; }

        #endregion Public Properties
    }
}