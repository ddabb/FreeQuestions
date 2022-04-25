using Newtonsoft.Json;

namespace FreeQuestions
{
    internal class option
    {
        [JsonProperty("code")]
        public string code;
        [JsonProperty("content")]
        public string content;
        [JsonProperty("value")]
        public string value;
    }
}