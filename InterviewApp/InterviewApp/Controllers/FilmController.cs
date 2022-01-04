using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using InterviewApp.BLL.Services.Interfaces;
using InterviewApp.ViewModels.Imdb.SearchFilm;
using Microsoft.AspNetCore.Mvc;

namespace InterviewApp.Controllers
{

    public class FilmController : ApiControllerBase
    {
        private readonly IFilmService _filmService;

        public FilmController(IMapper mapper,
            IFilmService filmService) : base(mapper)
        {
            _filmService = filmService;
        }

        [HttpGet]
        public async Task<IActionResult> SearchFilmByName(string filmName)
        {
            var searchResultModels = await _filmService.SearchFilmByNameAsync(filmName);
            var searchResultViewModels = Mapper.Map<List<SearchFilmResultViewModel>>(searchResultModels);

            return Ok(searchResultViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> GetGenresCountForFilmNames(IEnumerable<string> filmNames)
        {
            await _filmService.GetGenresCountForFilmNamesAsync(filmNames);

            return Ok();
        }
    }
}
