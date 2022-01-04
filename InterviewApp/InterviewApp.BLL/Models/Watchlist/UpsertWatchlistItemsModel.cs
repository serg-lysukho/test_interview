using System.Collections.Generic;

namespace InterviewApp.BLL.Models.Watchlist
{
    public class UpsertWatchlistItemsModel
    {
        public int UserId { get; set; }
        public IEnumerable<string> FilmsId { get; set; }
    }
}
