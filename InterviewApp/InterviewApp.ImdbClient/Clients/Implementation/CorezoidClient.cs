using System;
using System.Net.Http;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using InterviewApp.ApiClients.Clients.Interfaces;
using InterviewApp.ApiClients.Constants.Corezoid;
using InterviewApp.Configuration;

namespace InterviewApp.ApiClients.Clients.Implementation
{
    public class CorezoidClient : HttpClientBase, ICorezoidClient
    {
        private readonly IInterviewAppConfiguration _configurationProvider;

        public CorezoidClient(IHttpClientFactory httpClientFactory,
            IInterviewAppConfiguration configurationProvider) : base(httpClientFactory)
        {
            _configurationProvider = configurationProvider;
        }

        public Task<HttpResponseMessage> SearchByNameAsync(string jsonContent)
        {
            var apiLogin = _configurationProvider.CorezoidApiLogin;
            var apiSecret = _configurationProvider.CorezoidApiKey;
            var gmtUnixTime = ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds();
            var paramsToEncryption = string.Concat(gmtUnixTime, apiSecret, jsonContent, apiSecret);
            var signature = Hash(paramsToEncryption);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post,
                $"{CorezoidEndpointConstants.BaseUri}/{apiLogin}/{gmtUnixTime}/{signature}");
            
            httpRequestMessage.Content = new StringContent(jsonContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            
            return SendRequestAsync(httpRequestMessage, _configurationProvider.CorezoidClientName);
        }
        
        private static string Hash(string input)
        {
            using var sha1 = new SHA1Managed();
            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sb = new StringBuilder(hash.Length * 2);

            foreach (var @byte in hash)
            {
                sb.Append(@byte.ToString("x2"));
            }

            return sb.ToString().Replace("-", string.Empty);
        }
    }
}
