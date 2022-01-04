using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InterviewApp.BLL.Models.Imdb.SearchFilm
{
    public class SearchFilmResponseModel
    {
        [JsonPropertyName("searchType")]
        public string SearchType { get; set; }

        [JsonPropertyName("expression")]
        public string Expression { get; set; }

        [JsonPropertyName("results")]
        public List<SearchFilmResultModel> Results { get; set; }

        [JsonPropertyName("errorMessage")]
        public string ErrorMessage { get; set; }
    }
}
