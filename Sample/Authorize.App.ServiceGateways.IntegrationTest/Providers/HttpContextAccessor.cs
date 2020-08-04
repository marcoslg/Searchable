using Microsoft.AspNetCore.Http;
using System;

namespace Authorize.App.ServiceGateways.IntegrationTest.Providers
{
    public class HttpContextAccessor : IHttpContextAccessor
    {
        public HttpContext HttpContext { get; set; }

        public HttpContextAccessor(IServiceProvider serviceProvider)
        {
            HttpContext = new DefaultHttpContext()
            {
                RequestServices = serviceProvider
            };

        }
    }
}
