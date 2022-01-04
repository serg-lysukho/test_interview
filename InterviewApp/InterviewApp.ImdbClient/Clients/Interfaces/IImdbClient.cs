using System.Net.Http;
using System.Threading.Tasks;

namespace InterviewApp.ApiClients.Clients.Interfaces
{
    public interface IImdbClient
    {
        Task<HttpResponseMessage> SearchByNameAsync(string filmName);
        Task<HttpResponseMessage> GetFilmInfoAsync(string imdbId);
        Task<HttpResponseMessage> GetFilmPosterAsync(string imdbId);
        Task<HttpResponseMessage> GetDescriptionFromWikipediaAsync(string imdbId);
    }
}