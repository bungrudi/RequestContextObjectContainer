using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace RequestContextObjectContainer.Middlewares
{
    public class HttpContextMiddleware
    {
        private readonly RequestDelegate _next;

        public HttpContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            HttpContextContainer ctxContainer = (HttpContextContainer) context.RequestServices.GetService(typeof(HttpContextContainer));
            ctxContainer.Current = new HttpContextImpl(context);
            ctxContainer.Current.Message = Guid.NewGuid().ToString();
            await _next.Invoke(context);
        }
    }

    public static class HttpContextMiddlewareExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseHttpContextMiddleWare(this IApplicationBuilder app)
        {
            return app.UseMiddleware<HttpContextMiddleware>();
        }

    }
}