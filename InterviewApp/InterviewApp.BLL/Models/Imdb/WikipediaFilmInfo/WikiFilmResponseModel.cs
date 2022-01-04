using System.Text.Json.Serialization;

namespace InterviewApp.BLL.Models.Imdb.WikipediaFilmInfo
{
    public class WikiFilmResponseModel
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("plotShort")]
        public WikiFilmPlotModel PlotShort { get; set; }

        [JsonPropertyName("plotFull")]
        public WikiFilmPlotModel PlotFull { get; set; }

        [JsonPropertyName("errorMessage")]
        public string ErrorMessage { get; set; }
    }
}
