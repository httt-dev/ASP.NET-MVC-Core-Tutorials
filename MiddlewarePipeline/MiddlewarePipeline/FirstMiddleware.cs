using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace MiddlewarePipeline
{
    /// <summary>
    /// Dinh nghia mot middleware yeu cau phai co 2 methods :
    /// Khoi tao : nhan vao 1 tham so la RequestDelegate 
    /// Invoke : nhan vao HttpContext 
    /// </summary>
    public class FirstMiddleware
    {
        // RequestDelegate ~ async (HttpContext context) => { }
        // next : delegate phia sau no , duoc goi khi can thiet
        private readonly RequestDelegate _next;
        public FirstMiddleware(RequestDelegate next) 
        {
            _next = next;
        }

        /// <summary>
        /// Duoc goi khi HttpContext di qua pipeline
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {

            Console.WriteLine($"URL : {context.Request.Path}");

            // truyen du lieu giua cac middleware
            context.Items.Add("DataFirstMiddleware",$"<p>{context.Request.Path}</p>");
            // do phia SecondMiddleware co gi header nen khong the goi ham ben duoi tai day => can phai dung context.Items.Add
            //await context.Response.WriteAsync($"<p>{context.Request.Path}</p>");

            // chuyen HttpContext cho cac meddileware phia sau no
            await _next(context);
        }

    }
}
