using Newtonsoft.Json;

namespace FreeQuestions
{
    internal class option
    {
        [JsonProperty("code")]
        public string code { get; set; }
        [JsonProperty("content")]
        public string content { get; set; }
        [JsonProperty("value")]
        public string value { get; set; }
        [JsonProperty("enable")]
        public bool enable { get; set; } = true;
    }
}