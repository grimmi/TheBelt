using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheBelt
{
    public class ArgumentMapping
    {
        [JsonProperty("from")]
        public string From { get; set; }
        [JsonProperty("to")]
        public string To { get; set; }
    }
}
