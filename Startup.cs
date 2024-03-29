using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Practice
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // app.UseHttpsRedirection();
            // app.UseStaticFiles();

            // app.UseRouting();

            // app.UseAuthorization();
            app.Use(async (context,next)=>
            {
                logger.LogInformation("Middleware:1:In coming request");
                await next();
                logger.LogInformation("Middleware-1: Outgoing request");
            }
            );
             app.Use(async (context,next)=>
            {
                logger.LogInformation("Middleware:1:In coming request");
                await next();
                logger.LogInformation("Middleware-1: Outgoing response");
            }
            );

             app.Use(async (context,next)=>
            {
                logger.LogInformation("Middleware:2:In coming request");
                await next();
                logger.LogInformation("Middleware-2: Outgoing response");
            }
            );
             app.Use(async (context,next)=>
            {
                logger.LogInformation("Middleware:3:In coming request");
                await next();
                logger.LogInformation("Middleware-3: Outgoing response");
            }
            );

            app.Run(async (context) =>
        {
            await context.Response.WriteAsync(Configuration["Mykey"]);
        });
            // app.UseEndpoints(endpoints =>
            // {
            //     endpoints.MapRazorPages();
            // });
        }
    }
}
