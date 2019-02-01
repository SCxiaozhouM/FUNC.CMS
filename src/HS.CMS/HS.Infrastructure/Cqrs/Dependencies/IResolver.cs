using System;
using System.Collections.Generic;
using System.Text;

namespace HS.Infrastructure.Cqrs.Dependencies
{
    public interface IResolver
    {
        T Resolve<T>();
        System.Collections.Generic.IEnumerable<T> ResolveAll<T>();
    }
}
