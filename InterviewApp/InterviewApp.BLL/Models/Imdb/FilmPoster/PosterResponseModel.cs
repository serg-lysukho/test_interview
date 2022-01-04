using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InterviewApp.BLL.Models.Imdb.FilmPoster
{
    public class PosterResponseModel
    {
        [JsonPropertyName("posters")]
        public List<FilmPosterModel> Posters { get; set; }

        [JsonPropertyName("backdors")]
        public List<FilmPosterModel> Backdors { get; set; }

        [JsonPropertyName("errorMessage")]
        public string ErrorMessage { get; set; }
    }
}
