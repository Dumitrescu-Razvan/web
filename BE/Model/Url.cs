using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace BE.Model{ 
    public class Url
    {
        public string url { get; set; }

        public string description { get; set; }

        public string category { get; set; }
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public int UserId { get; set; } 

    }
}