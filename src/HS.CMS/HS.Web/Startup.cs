using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HS.Data;
using HS.Data.Extensions;
using HS.Data.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using HS.Infrastructure.Cqrs.Dependencies;
//using Weapsy.Cqrs.Dependencies;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Http;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Internal;
using System.Reflection;
using HS.Infrastructure.Command;
using HS.Data.Command.Menu;

namespace HS.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }
        public ICommandInvokerFactory _commandInvokerFactory { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.。。
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.TryAddSingleton <IHttpContextAccessor,HttpContextAccessor>();
            services.AddTransient<IResolver, Resolver>();
            services.AddTransient<IContextFactory, ContextFactory>();

            //data配置
            services.Configure<HS.Data.Configuration.Data>(C =>
            {
                C.Provider = (Data.Configuration.DataProvider)Enum.Parse(
                    typeof(Data.Configuration.DataProvider), Configuration.GetSection("Data")["Provider"]);
            });

            //ConnectionStrings配置
            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));

            //注册ef
            services.AddEntityFramework(Configuration);
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var builder = new ContainerBuilder();

           
            builder.RegisterModule(new AutofacModule());
            builder.Populate(services);
            var container = builder.Build();
            return container.Resolve<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ICommandInvokerFactory commandInvokerFactory)
        {
            _commandInvokerFactory = commandInvokerFactory;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            #region 路由注册

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            // 配置MVC选项
            app.UseRouter(routes =>
            {
                if (routes.DefaultHandler == null) routes.DefaultHandler = app.ApplicationServices.GetRequiredService<MvcRouteHandler>();

                // 区域路由注册
                routes.MapRoute(
                    name: "CubeAreas",
                    template: "{area=Admin}/{controller=User}/{action=Index}/{id?}"
                );
            });
            #endregion



            //assembly 下的 控制器
            //获取区域下的控制器
            var controllers = Assembly.GetExecutingAssembly().GetTypes().Where(o => o.CustomAttributes.Where(f => f.AttributeType == typeof(AreaAttribute)).Any()).ToArray();
            var types = FindAllArea(controllers);
        }


        List<string> FindAllArea(Type[] controllers)
        {
           
            var list = new List<Type>();
            var typeDic = new Dictionary<string,Type[]>();
            var areaDic = new List<String>();
            //var assembly = Assembly.GetExecutingAssembly().GetTypes();
            //var controllers = assembly.Where(o => o.BaseType.FullName.Contains(typeof(Controller).FullName));
            /* var controllers = typeof(Controller).GetAllSubclasses(false).ToArray();*/
            foreach (var item in controllers)
            {
                //获取去域名
                var areaName = item.GetCustomAttributesData()
                    ?.FirstOrDefault(f => f.AttributeType == typeof(AreaAttribute))
                    ?.ConstructorArguments
                    ?.FirstOrDefault()
                    .Value
                    ?.ToString();
                if (string.IsNullOrEmpty(areaName)) continue;
                // var asm = item.Assembly.GetExportedTypes();

                //if (!list.Contains(item))
                //{

                //    if (!areaDic.Contains(areaName))
                //    {
                //        areaDic.Add(areaName);
                //    }
                //}
                //AddMenuCommand command = new AddMenuCommand()
                //{
                //     Name=
                //}
                //_commandInvokerFactory.Handle<AddMenuCommand, CommandResult>(command);
            }
            return areaDic.ToList();
        }


    }
}
