using AutoMapper;
using InterviewApp.BLL.Models.Watchlist;
using InterviewApp.ViewModels.Watchlist;

namespace InterviewApp.AutoMapper
{
    public class WatchlistModelProfile : Profile
    {
        public WatchlistModelProfile()
        {
            CreateMap<UpsertWatchlistItemsViewModel, UpsertWatchlistItemsModel>();
            CreateMap<WatchlistItemModel, WatchlistItemViewModel>();
        }
    }
}