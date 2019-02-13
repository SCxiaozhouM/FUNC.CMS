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
        public void ConfigureServices(IServiceCollection services)
        {
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            app.UseMvc();
        }
    }
}
