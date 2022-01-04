using System.Collections.Generic;
using System.Threading.Tasks;
using InterviewApp.DAL.DataModels.Watchlist;

namespace InterviewApp.DAL.Repositories.Interfaces
{
    public interface IWatchlistRepository
    {
        Task CreateWatchlistItemsAsync(UpsertWatchlistItemsDataModel watchlistItem);
        Task<List<WatchlistItemDataModel>> GetWatchlistItemsForUserAsync(int userId);
        Task MarkFilmsAsWatchedAsync(UpsertWatchlistItemsDataModel watchlistItem);
        Task<List<UserWatchlistItemDataModel>> GetAllUnwatchedFilmsAsync();
    }
}