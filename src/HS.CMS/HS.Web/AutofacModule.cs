using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using HS.Data;
using HS.Infrastructure.Command;
using HS.Infrastructure.Log;
using HS.IService;
using HS.Web.Features;
using Microsoft.AspNetCore.Mvc;

namespace HS.Web
{
    public class AutofacModule:Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var dataAssembly = typeof(IDataProvider).GetTypeInfo().Assembly;
            var IControllerType = typeof(ControllerBase);
            builder.RegisterAssemblyTypes(IControllerType.GetTypeInfo().Assembly).Where(t =>
                            IControllerType.IsAssignableFrom(t) && t != IControllerType).PropertiesAutowired();

            builder.RegisterAssemblyTypes(dataAssembly).AsImplementedInterfaces();
            builder.RegisterType<CommandInvokerFactory>().AsImplementedInterfaces().PropertiesAutowired();
            //services.AddTransient<ICommandInvokerFactory>(serviceProvider =>
            //{
            //    return new CommandInvokerFactory(serviceProvider);
            //});
        }
    }
}
