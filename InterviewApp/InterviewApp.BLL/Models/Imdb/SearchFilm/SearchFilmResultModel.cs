using System.Text.Json.Serialization;

namespace InterviewApp.BLL.Models.Imdb.SearchFilm
{
    public class SearchFilmResultModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("resultType")]
        public string ResultType { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
