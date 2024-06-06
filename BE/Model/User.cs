using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Web;

namespace BE.Model
{
    public class User
    {
        public string  username { get; set; }
        public string Password { get; set; }
        
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public IEnumerable<Url>? Urls { get; set; }
    }
}