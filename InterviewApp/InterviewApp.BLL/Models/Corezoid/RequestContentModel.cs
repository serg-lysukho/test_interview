using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InterviewApp.BLL.Models.Corezoid
{
    public class RequestContentModel
    {
        [JsonPropertyName("returnUrl")]
        public string ReturnUrl { get; set; }

        [JsonPropertyName("imDbApiKey")]
        public string ImdbApiKey { get; set; }

        [JsonPropertyName("filmNames")]
        public IEnumerable<string> FilmNames { get; set; }
    }
}
