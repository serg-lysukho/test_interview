using System.Collections.Generic;
using System.Threading.Tasks;
using InterviewApp.BLL.Models.EmailMessages;
using InterviewApp.BLL.Models.Watchlist;

namespace InterviewApp.BLL.Services.Interfaces
{
    public interface IWatchlistService
    {
        Task CreateWatchlistItemsAsync(UpsertWatchlistItemsModel watchlistItem);
        Task<List<WatchlistItemModel>> GetWatchlistItemsForUserAsync(int userId);
        Task MarkFilmsAsWatchedAsync(UpsertWatchlistItemsModel watchlistItem);
        Task<List<FilmEmailNotificationModel>> GetMostRatedFilmBetweenUnwatchedAndSendEmailAsync();
    }
}