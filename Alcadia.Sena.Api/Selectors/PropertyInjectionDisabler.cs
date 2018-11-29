using LightInject;
using System;
using System.Collections.Generic;

namespace Alcadia.Sena.Api.Selectors
{
    public class PropertyInjectionDisabler : IPropertyDependencySelector
    {
        public IEnumerable<PropertyDependency> Execute(Type type)
        {
            return new PropertyDependency[0];
        }
    }
}
