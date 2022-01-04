using System.Collections.Generic;
using System.Threading.Tasks;
using InterviewApp.BLL.Models.Imdb.SearchFilm;

namespace InterviewApp.BLL.Services.Interfaces
{
    public interface IFilmService
    {
        Task<List<SearchFilmResultModel>> SearchFilmByNameAsync(string filmName);
        Task GetGenresCountForFilmNamesAsync(IEnumerable<string> filmNames);
    }
}