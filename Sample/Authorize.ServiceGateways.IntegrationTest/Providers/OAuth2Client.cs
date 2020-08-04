using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Authorize.ServiceGateways.IntegrationTest.Providers
{
    public class OAuth2Client
    {
        private readonly string _authority;
        private readonly string _clientId;

        public OAuth2Client(string address, string clientId)

        {
            _authority = address;
            _clientId = clientId;
        }

        public async Task<TokenResponse> GetToken(string username, string pwd)
        {
            var client = new HttpClient();
            var formData = new Dictionary<string, string>()
            {
                { "grant_type", "password" },
                { "client_id", _clientId },
                { "username", username },
                { "password", pwd },
            };
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_authority}/connect/token")
            {
                Content = new FormUrlEncodedContent(formData)
            };
            var httpResponse = await client.SendAsync(request);
            var result = await ProtocolResponse.FromHttpResponseAsync<TokenResponse>(httpResponse);
            return result;
        }
    }
}
