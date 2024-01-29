using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MiddlewarePipeline
{
    /// <summary>
    /// Trien khai middleware su dung IMiddleware
    /// </summary>
    public class SecondMiddleware : IMiddleware
    {
        /// <summary>
        ///  Kiem tra Url :
        ///     + Neu la "/xxx.html" 
        ///         - Khong goi middleware phia sau 
        ///         - Ban khong duoc truy cap
        ///         - Header - SecondMiddleware : Ban khong duoc truy cap
        ///     nguoc lai 
        ///         - Header - SecondMiddleware : Ban duoc truy cap
        ///         - Chuyen HttpContext cho middleware phia sau
        ///         
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if(context.Request.Path == "/xxx.html")
            {

                context.Response.Headers.Add("SecondMiddleware", "Ban khong duoc truy cap");
                var dataFromFirstMiddleware = context.Items["DataFirstMiddleware"];
                if(dataFromFirstMiddleware !=null)
                {
                    await context.Response.WriteAsync((string)dataFromFirstMiddleware);
                }
                await context.Response.WriteAsync("Ban khong duoc truy cap");  // khong goi middleware tiep theo
                // neu thiet lap header sau khi write du lieu thi se bi error =< can chinh sua ca FirstMiddleware
                // context.Response.Headers.Add("SecondMiddleware", "Ban khong duoc truy cap"); 
            }
            else
            {
                context.Response.Headers.Add("SecondMiddleware", "Ban duoc truy cap");

                var dataFromFirstMiddleware = context.Items["DataFirstMiddleware"];
                if(dataFromFirstMiddleware != null)
                {
                    await context.Response.WriteAsync((string)dataFromFirstMiddleware);
                }

                await next(context);
            }
        }
    }
}
