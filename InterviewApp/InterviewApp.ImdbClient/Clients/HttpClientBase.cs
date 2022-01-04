using System.Net.Http;
using System.Threading.Tasks;

namespace InterviewApp.ApiClients.Clients
{
    public class HttpClientBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpClientBase(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        protected Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage httpRequestMessage, string clientName)
        {
            var httpClient = _httpClientFactory.CreateClient(clientName);

            var httpResponseMessage = httpClient.SendAsync(httpRequestMessage);

            return httpResponseMessage;
        }
    }
}
