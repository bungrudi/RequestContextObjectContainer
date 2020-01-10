using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RequestContextObjectContainer.Middlewares;
using RequestContextObjectContainer.Services;

namespace RequestContextObjectContainer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<HttpContextContainer>();
            services.AddScoped<DummyMainWS>();
            services.AddScoped<DummyVisualModelWS>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseHttpContextMiddleWare();

            var handler = new DummyRequestHandler();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", handler.HandleRequestAsync);
            });
        }
    }

    public class DummyRequestHandler
    {
        public async Task HandleRequestAsync(HttpContext context)
        {
            await context.Response.WriteAsync(context.RequestServices.GetService<DummyMainWS>().DoSomething());
            await context.Response.WriteAsync("\r\n");
            await context.Response.WriteAsync(
                context.RequestServices.GetService<DummyVisualModelWS>().DoSomethingElse());
        }
    }
    
    public class HttpContextImpl
    {
        public HttpContext Context { get; }

        public string Message { get; set; }
        
        public HttpContextImpl(HttpContext context)
        {
            this.Context = context;
        }
    }

    public class HttpContextContainer
    {
        public HttpContextImpl Current { get; set; }
    }
}