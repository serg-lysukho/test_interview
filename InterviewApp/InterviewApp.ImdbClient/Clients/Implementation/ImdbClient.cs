using System.Net.Http;
using System.Threading.Tasks;
using InterviewApp.ApiClients.Clients.Interfaces;
using InterviewApp.ApiClients.Constants.Imdb;
using InterviewApp.Configuration;

namespace InterviewApp.ApiClients.Clients.Implementation
{
    public class ImdbClient : HttpClientBase, IImdbClient
    {
        private readonly IInterviewAppConfiguration _configurationProvider;

        public ImdbClient(IHttpClientFactory httpClientFactory, 
            IInterviewAppConfiguration configurationProvider) : base(httpClientFactory)
        {
            _configurationProvider = configurationProvider;
        }

        public Task<HttpResponseMessage> SearchByNameAsync(string filmName)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get,
                $"{ImdbEndpointConstants.Search}/{_configurationProvider.ImdbApiKey}/{filmName}");

            return SendRequestAsync(httpRequestMessage, _configurationProvider.ImdbClientName);
        }

        public Task<HttpResponseMessage> GetFilmInfoAsync(string imdbId)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get,
                $"{ImdbEndpointConstants.Title}/{_configurationProvider.ImdbApiKey}/{imdbId}");

            return SendRequestAsync(httpRequestMessage, _configurationProvider.ImdbClientName);
        }

        public Task<HttpResponseMessage> GetFilmPosterAsync(string imdbId)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get,
                $"{ImdbEndpointConstants.Poster}/{_configurationProvider.ImdbApiKey}/{imdbId}");

            return SendRequestAsync(httpRequestMessage, _configurationProvider.ImdbClientName);
        }

        public Task<HttpResponseMessage> GetDescriptionFromWikipediaAsync(string imdbId)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get,
                $"{ImdbEndpointConstants.Wikipedia}/{_configurationProvider.ImdbApiKey}/{imdbId}");
            
            return SendRequestAsync(httpRequestMessage, _configurationProvider.ImdbClientName);
        }
    }
}
