using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;

namespace ACs.NHibernate.Next
{
    public static class HttpContextHelper
    {

        private static IHttpContextAccessor _httpContextAccessor;
        public static IApplicationBuilder UseStaticContext(this IApplicationBuilder builder)
        {
            UseStaticContext((IHttpContextAccessor)builder.ApplicationServices.GetService(typeof(IHttpContextAccessor)));
            return builder;

        }
        public static void UseStaticContext(IHttpContextAccessor builder)
        {
            _httpContextAccessor = builder;
        }

        public static HttpContext Current => _httpContextAccessor.HttpContext;
    }
}

