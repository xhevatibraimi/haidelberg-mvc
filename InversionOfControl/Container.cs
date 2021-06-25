using System;
using System.Collections.Generic;

namespace InversionOfControl
{
    public class Container
    {
        private readonly Dictionary<Type, Func<object>> _dependencies = new Dictionary<Type, Func<object>>();

        public void RegisterDependency<TDefinition, TImplementation>(Func<TImplementation> factory)
            where TImplementation : class, TDefinition 
        {
            _dependencies.Add(typeof(TDefinition), () => factory());
        }

        public TDefinition GetImplementation<TDefinition>()
        {
            var factory = _dependencies[typeof(TDefinition)];
            return (TDefinition)factory();
        }
    }
}
