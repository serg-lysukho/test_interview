using System.Text.Json.Serialization;

namespace InterviewApp.BLL.Models.Imdb.WikipediaFilmInfo
{
    public class WikiFilmPlotModel
    {
        [JsonPropertyName("plainText")]
        public string PlainText { get; set; }

        [JsonPropertyName("html")]
        public string Html { get; set; }
    }
}
