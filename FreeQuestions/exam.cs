using Newtonsoft.Json;

namespace FreeQuestions
{
    internal class exam
    {
        [JsonProperty("_id")]
        public string id { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }


        [JsonProperty("enable")]
        public bool enable { get; set; } = true;
    }
}
