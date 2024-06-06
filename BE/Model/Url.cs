using System;

namespace BE.Model{ 
    public class Url
    {
        public int Id { get; set; }
        public string url { get; set; }

        public string description { get; set; }

        public string category { get; set; }

        public int UserId { get; set; } 

        public User user { get; set; }

    }
}