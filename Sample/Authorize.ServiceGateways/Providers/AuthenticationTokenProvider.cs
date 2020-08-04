using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Viseo.API.Servicegateway.Contracts;

namespace Authorize.ServiceGateways.Providers
{
    public class AuthenticationTokenProvider : IAuthenticationTokenProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public AuthenticationTokenProvider (IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException($"Authorize.ServiceGateways.Providers.AuthenticationTokenProvider ctro: {nameof(contextAccessor)}");
        }
        public async Task<string> GetToken()
        {
            var token = await _contextAccessor.HttpContext.GetTokenAsync("access_token");
            return token;
        }
    }
}
