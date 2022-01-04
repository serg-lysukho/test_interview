namespace InterviewApp.DAL.Entities.Watchlist
{
    public class WatchlistItem
    {
        public int UserId { get; set; }
        public string FilmId { get; set; }
        public bool IsWatched { get; set; }
    }
}
