using System.Net.Http;
using System.Threading.Tasks;

namespace InterviewApp.ApiClients.Clients.Interfaces
{
    public interface ICorezoidClient
    {
        Task<HttpResponseMessage> SearchByNameAsync(string jsonContent);
    }
}