using Microsoft.AspNetCore.Builder;

namespace Eventures.App.Middlewares
{
    public static class SeedMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustom(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SeedMiddleware>();
        }
    }
}
