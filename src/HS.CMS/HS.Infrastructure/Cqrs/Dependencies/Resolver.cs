using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HS.Infrastructure.Cqrs.Dependencies
{
    public class Resolver : IResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public Resolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T Resolve<T>()
        {
            return ServiceProviderServiceExtensions.GetService<T>(_serviceProvider);
        }

        public System.Collections.Generic.IEnumerable<T> ResolveAll<T>()
        {
            return ServiceProviderServiceExtensions.GetServices<T>(_serviceProvider);
        }
    }
}
