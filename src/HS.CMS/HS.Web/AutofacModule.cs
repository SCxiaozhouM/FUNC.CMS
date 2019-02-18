using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using HS.Data;
using HS.IService;

namespace HS.Web
{
    public class AutofacModule:Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var dataAssembly = typeof(IDataProvider).GetTypeInfo().Assembly;

            builder.RegisterAssemblyTypes(dataAssembly).AsImplementedInterfaces();

        }
    }
}
