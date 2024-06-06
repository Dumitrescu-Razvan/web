using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BE.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        
        public IEnumerable<Url> Urls { get; set; }
    }
}