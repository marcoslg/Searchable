using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Authorize.ServiceGateways.IntegrationTest.Providers
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
