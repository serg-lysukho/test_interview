using System.Collections.Generic;

namespace InterviewApp.ViewModels.Watchlist
{
    public class UpsertWatchlistItemsViewModel
    {
        public int UserId { get; set; }
        public IEnumerable<string> FilmsId { get; set; }
    }
}
