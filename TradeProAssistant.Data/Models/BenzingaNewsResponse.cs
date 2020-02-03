using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeProAssistant.Data.Framework;

namespace TradeProAssistant.Data.Models
{
    public class BenzingaNewsResponse
    {
        [JsonProperty("data")]
        public Dictionary<string, BenzingaNewsStory> Stories { get; set; }

        public List<BenzingaNewsStory> GetStoriesList()
        {
            List<BenzingaNewsStory> list = new List<BenzingaNewsStory>();
            foreach (KeyValuePair<String, BenzingaNewsStory> pair in this.Stories)
            {
                pair.Value.Teaser = pair.Value.Teaser.ScrubHtml();
                pair.Value.Body = pair.Value.Body.ScrubHtml();
                list.Add(pair.Value);
            }

            return list;
        }
    }

    public class BenzingaNewsStory
    {
        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("updated")]
        public DateTime Updated { get; set; }

        [JsonProperty("title")]
        public String Title { get; set; }

        [JsonProperty("teaser")]
        public String Teaser { get; set; }

        [JsonProperty("body")]
        public String Body { get; set; }

        [JsonProperty("url")]
        public String Url { get; set; }

        public bool Important
        {
            get
            {
                return this.Teaser.ToLower().Contains("price target")
                    || this.Body.ToLower().Contains("price target");
            }
        }
    }
}
