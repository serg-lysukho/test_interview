using System.Text.Json.Serialization;

namespace InterviewApp.BLL.Models.Imdb.FilmInfo
{
    public class FilmInfoModel
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
        
        [JsonPropertyName("contentRating")]
        public string ContentRating { get; set; }

        [JsonPropertyName("imDbRating")]
        public string IMDbRating { get; set; }

        [JsonPropertyName("imDbRatingVotes")]
        public string IMDbRatingVotes { get; set; }

        [JsonPropertyName("metacriticRating")]
        public string MetacriticRating { set; get; }
    }
}
