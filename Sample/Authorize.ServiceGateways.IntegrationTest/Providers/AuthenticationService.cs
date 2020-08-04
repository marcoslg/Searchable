using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Authorize.ServiceGateways.IntegrationTest.Providers
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly OAuth2Client _client;
        private readonly string _username;
        private readonly string _pwd;
        public AuthenticationService(IConfiguration config)
        {
            var authority = config.GetValue<string>("Authority");
            var clientId = config.GetValue<string>("ClientId");           
            _username = config.GetValue<string>("Username");
            _pwd = config.GetValue<string>("Password"); 
            _client = new OAuth2Client(authority, clientId);
        }

        public async Task<AuthenticateResult> AuthenticateAsync(HttpContext context, string scheme)
        {
            var token = await _client.GetToken(_username, _pwd);

            var claimIdentity = new ClaimsIdentity();
            var prop = new AuthenticationProperties(new Dictionary<string, string>() {
                { ".Token.access_token", token.AccessToken}
            });
            return AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(claimIdentity), prop, scheme));
        }

        public Task ChallengeAsync(HttpContext context, string scheme, AuthenticationProperties properties)
        {
            throw new NotImplementedException();
        }

        public Task ForbidAsync(HttpContext context, string scheme, AuthenticationProperties properties)
        {
            throw new NotImplementedException();
        }

        public Task SignInAsync(HttpContext context, string scheme, ClaimsPrincipal principal, AuthenticationProperties properties)
        {
            throw new NotImplementedException();
        }

        public Task SignOutAsync(HttpContext context, string scheme, AuthenticationProperties properties)
        {
            throw new NotImplementedException();
        }
    }
}
