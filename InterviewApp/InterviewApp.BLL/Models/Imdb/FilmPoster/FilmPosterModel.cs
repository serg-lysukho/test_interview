using System.Text.Json.Serialization;

namespace InterviewApp.BLL.Models.Imdb.FilmPoster
{
    public class FilmPosterModel
    {
        [JsonPropertyName("link")]
        public string Link { get; set; }

        [JsonPropertyName("aspectRation")]
        public double AspectRation { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }
    }
}
