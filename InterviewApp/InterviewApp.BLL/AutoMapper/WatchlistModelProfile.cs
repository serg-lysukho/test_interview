using AutoMapper;
using InterviewApp.BLL.Models.Watchlist;
using InterviewApp.DAL.DataModels.Watchlist;

namespace InterviewApp.BLL.AutoMapper
{
    public class WatchlistModelProfile : Profile
    {
        public WatchlistModelProfile()
        {
            CreateMap<UpsertWatchlistItemsModel, UpsertWatchlistItemsDataModel>();
            CreateMap<WatchlistItemDataModel, WatchlistItemModel>();
        }
    }
}
