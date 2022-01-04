using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InterviewApp.BLL.Models.Corezoid
{
    public class Root
    {
        [JsonPropertyName("ops")]
        public IEnumerable<RequestBodyModel> RequestBodies { get; set; }
    }
}
