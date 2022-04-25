using Newtonsoft.Json;

namespace FreeQuestions
{
    internal class subject
    {
        /// <summary>
        /// 主题编号,例如 001001微信测评1 则是001001
        /// </summary>
        [JsonProperty("_id")]
        public string id { get; set; }
        [JsonProperty("examid")]
        public string examid { get; set; }
        /// <summary>
        /// 主题编号,例如 001001微信测评1 则是微信测评1 
        /// </summary>
        [JsonProperty("name")]
        public string name { get; set; }
        /// <summary>
        /// 总问题个数
        /// </summary>
        [JsonProperty("counts")]
        public int counts { get; set; }

    }
}
