using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterviewApp.DAL.DataModels.Watchlist;
using InterviewApp.DAL.Entities.Watchlist;
using InterviewApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InterviewApp.DAL.Repositories.Implementation
{
    public class WatchlistRepository : IWatchlistRepository
    {
        private readonly DbSet<WatchlistItem> _watchlistItems;

        public WatchlistRepository(InterviewAppDbContext dbContext)
        {
            _watchlistItems = dbContext.WatchlistItems;
        }

        public Task CreateWatchlistItemsAsync(UpsertWatchlistItemsDataModel watchlistItem)
        {
            var watchlistItemEntities = watchlistItem.FilmsId
                .Select(id => new WatchlistItem
                {
                    FilmId = id,
                    UserId = watchlistItem.UserId
                });

            return _watchlistItems.AddRangeAsync(watchlistItemEntities);
        }

        public async Task<List<WatchlistItemDataModel>> GetWatchlistItemsForUserAsync(int userId)
        {
            var watchlistItemDataModels = await _watchlistItems
                .Where(w => w.UserId == userId)
                .Select(w => new WatchlistItemDataModel
                {
                    FilmId = w.FilmId,
                    IsWatched = w.IsWatched
                }).ToListAsync();

            return watchlistItemDataModels;
        }

        public async Task MarkFilmsAsWatchedAsync(UpsertWatchlistItemsDataModel watchlistItem)
        {
            var watchlistItemEntity = await _watchlistItems
                .Where(w => 
                    w.UserId == watchlistItem.UserId && 
                    watchlistItem.FilmsId.Contains(w.FilmId))
                .ToListAsync();

            watchlistItemEntity.ForEach(w => w.IsWatched = true);
        }

        public async Task<List<UserWatchlistItemDataModel>> GetAllUnwatchedFilmsAsync()
        {
            var watchlistItemDataModels = await _watchlistItems
                .Where(w => !w.IsWatched)
                .Select(w => new UserWatchlistItemDataModel
                {
                    UserId = w.UserId,
                    FilmId = w.FilmId
                }).ToListAsync();

            return watchlistItemDataModels;
        }
    }
}
