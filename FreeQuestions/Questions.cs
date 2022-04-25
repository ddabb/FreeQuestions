using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FreeQuestions
{
    internal class Questions
    {

        private string _id;
        /// <summary>
        /// 腾讯云数据库的主键
        /// </summary>
        [JsonProperty("_id")]
        public string id
        {
            get
            {
                if (string.IsNullOrEmpty(_id))
                { _id = Guid.NewGuid().ToString().Replace("-", ""); }
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        /// <summary>
        /// 序号 例如01.json 则是01
        /// </summary>
        [JsonProperty("order")]
        public string order { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("typecode")]
        public string typecode { get; set; } = "01";

        [JsonProperty("typename")]
        public string typename { get; set; } = "单选";

        [JsonProperty("comments")]
        public string comments { get; set; }

        /// <summary>
        /// 选项集合
        /// </summary>
        [JsonProperty("options")]
        public List<option> options { get; set; }


        /// <summary>
        /// 大类
        /// </summary>
        [JsonProperty("subjectid")]
        public string subjectid { get; set; }


        /// <summary>
        /// 考试编号id
        /// </summary>
        [JsonProperty("examid")]
        public string examid { get; set; }



    }
}