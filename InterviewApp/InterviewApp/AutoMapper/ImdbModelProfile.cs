using AutoMapper;
using InterviewApp.BLL.Models.Imdb.SearchFilm;
using InterviewApp.ViewModels.Imdb.SearchFilm;

namespace InterviewApp.AutoMapper
{
    public class ImdbModelProfile : Profile
    {
        public ImdbModelProfile()
        {
            CreateMap<SearchFilmResultModel, SearchFilmResultViewModel>();
        }
    }
}
