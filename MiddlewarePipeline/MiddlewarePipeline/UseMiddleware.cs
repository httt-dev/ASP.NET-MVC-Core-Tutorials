using Microsoft.AspNetCore.Builder;

namespace MiddlewarePipeline
{
    public static class UseMiddleware
    {
        public static void UseFirstMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<FirstMiddleware>();
            
        }

        public static void UseSecondMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<SecondMiddleware>();

        }
    }
}
