namespace InterviewApp.BLL.Models.EmailMessages
{
    public class FilmEmailNotificationModel
    {
        public int UserId { get; set; }
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public double ImdbRating { get; set; }
        public string PosterLink { get; set; }
        public string ShortDescriptionFromWiki { get; set; }
    }
}
