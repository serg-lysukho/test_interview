using System.Text.Json.Serialization;

namespace InterviewApp.BLL.Models.Corezoid
{
    public class RequestBodyModel
    {
        [JsonPropertyName("type")]
        public string OperationType { get; set; }

        [JsonPropertyName("obj")]
        public string OperatedObjectType { get; set; }

        [JsonPropertyName("conv_id")]
        public int ProcessId { get; set; }

        [JsonPropertyName("data")]
        public RequestContentModel ProcessParameters { get; set; }
    }
}
