using Newtonsoft.Json;

namespace OnlineOrdering.Common.Models.Errors
{
    public class Error
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string FieldName { get; set; }

        public string Message { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
    }
}
