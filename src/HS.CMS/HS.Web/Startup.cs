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
using HS.IService.Menus;
using HS.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using NewLife.Reflection;
using System.ComponentModel;
using Exceptionless;

namespace HS.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.。。
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IResolver, Resolver>();

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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IContextFactory _contextFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            
            ExceptionlessClient.Default.Configuration.ApiKey = Configuration.GetSection("Exceptionless:ApiKey").Value;
            ExceptionlessClient.Default.Configuration.ServerUrl = Configuration.GetSection("Exceptionless:ServerUrl").Value;
            ExceptionlessClient.Default.SubmittingEvent += OnSubmittingEvent;
            app.UseExceptionless();

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
            FindAllArea(controllers, _contextFactory);
        }


        private void OnSubmittingEvent(object sender, EventSubmittingEventArgs e)
        {
            //已处理异常不处理
            if (!e.IsUnhandledError) return;

            if(e.Event.IsNotFound())
            {
                e.Cancel = true;
                return;
                            }
        }

        List<string> FindAllArea(Type[] controllers, IContextFactory _contextFactory)
        {

            var list = new List<Type>();
            var typeDic = new Dictionary<string, Type[]>();
            var areaDic = new List<String>();

            //var assembly = Assembly.GetExecutingAssembly().GetTypes();
            //var controllers = assembly.Where(o => o.BaseType.FullName.Contains(typeof(Controller).FullName));
            /* var controllers = typeof(Controller).GetAllSubclasses(false).ToArray();*/
            for (int i = 0; i < controllers.Length; i++)
            {
                var item = controllers[i];
                //获取去域名
                var areaName = item.GetCustomAttributesData()
                    ?.FirstOrDefault(f => f.AttributeType == typeof(AreaAttribute))
                    ?.ConstructorArguments
                    ?.FirstOrDefault()
                    .Value
                    ?.ToString();

                if (string.IsNullOrEmpty(areaName)) continue;

                var asm = item.Assembly.GetExportedTypes();

                if (!list.Contains(item))
                {
                    using (var ctx = _contextFactory.Create())
                    {
                        list.Add(item);
                        var areaModel = ctx.Menus.Where(o => o.Name == areaName).FirstOrDefault();
                        if (!areaDic.Contains(areaName))
                        {
                            areaDic.Add(areaName);
                            //添加菜单0级

                            if (areaModel == null)
                            {
                                //没有菜单则添加
                                Menu menu = new Menu()
                                {
                                    Name = areaName,
                                    ParentId = 0,
                                    Sort = 0,
                                    Url="/"+ areaName
                                };
                                ctx.Menus.Add(menu);
                                ctx.SaveChanges();
                                areaModel = menu;
                            }
                        }
                        var menuName = item.Name.Replace("Controller", "");
                        //读取权限
                        string remark, displayName;
                        object value;
                        object obj = item.CreateInstance();
                        var sort = item.GetValue("MenuOrder").ToInt();
                        var per = ScanActionMenu(item, out remark, out displayName);
                        //子菜单
                        var menuModle = ctx.Menus.Where(o => o.Name == menuName).FirstOrDefault();
                        if (menuModle==null)
                        {
                            //没有菜单则添加
                            Menu menu = new Menu()
                            {
                                Name = menuName,
                                ParentId = areaModel.Id,
                                DisplayName = displayName,
                                Remark = remark,
                                Sort = sort,
                                Permission = per,
                                Url = "/" + areaName + "/" + menuName
                            };
                            ctx.Menus.Add(menu);
                        }
                        else
                        {
                            //存在则读取权限
                            menuModle.Permission = per;
                            menuModle.Remark = remark;
                            menuModle.Sort = sort;
                            menuModle.DisplayName = displayName;
                            ctx.Menus.Update(menuModle);
                        }
                        ctx.SaveChanges();
                    }
                }

            }
            return areaDic.ToList();
        }
        /// <summary>获取可用于生成权限菜单的Action集合</summary>
        /// <param name="menu">该控制器所在菜单</param>
        /// <returns>返回控制器所需要的权限</returns>
        protected String ScanActionMenu(Type type, out string description, out string name)
        {
            var dic = new Dictionary<MethodInfo, Int32>();
            // 添加该类型下的所有Action
            foreach (var method in type.GetMethods())
            {
                if (method.IsStatic || !method.IsPublic) continue;

                if (!method.ReturnType.As<IActionResult>()) continue;

                //if (method.GetCustomAttribute<HttpPostAttribute>() != null) continue;
                if (method.GetCustomAttribute<AllowAnonymousAttribute>() != null) continue;

                var att = method.GetCustomAttribute<EntityAuthorizeAttribute>();
                if (att != null && att.Permission > PermissionFlags.None) dic.Add(method, (Int32)att.Permission);
                //permssion = permssion == 0 ? permssion = att.Permission.ToInt() : permssion | att.Permission.ToInt();//
            }
            string permssion= GetPermssionRemark(dic);
            var s = PermissionFlags.None.GetDescription();
            name = type.GetDisplayName();
            description = type.GetDescription();
            return permssion;
        }

        /// <summary>
        /// 获取权限描述
        /// </summary>
        /// <returns></returns>
        private string GetPermssionRemark(Dictionary<MethodInfo, Int32> dic)
        {
            string permission = "";
            foreach (var item in dic)
            {
                //获取权限描述
                var desc = ((PermissionFlags)item.Value).GetDescription();
                if(desc==null)
                {
                    //如果权限为空 则可能是自定义权限 读取方法描述获取
                    desc = item.Key.GetDisplayName();
                }
                permission = permission == "" ? item.Value + "#" + desc : permission + "," + item.Value + "#" + desc;
            }
            return permission;
        }
    }
}
