using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FreeQuestions
{
    internal class exam
    {
        [JsonProperty("_id")]
        public string id { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }
    }
}
