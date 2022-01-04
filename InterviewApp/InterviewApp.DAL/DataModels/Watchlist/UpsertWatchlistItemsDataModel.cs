using System.Collections.Generic;

namespace InterviewApp.DAL.DataModels.Watchlist
{
    public class UpsertWatchlistItemsDataModel
    {
        public int UserId { get; set; }
        public IEnumerable<string> FilmsId { get; set; }
    }
}
