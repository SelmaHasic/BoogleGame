using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoogleGame.Models
{
    public class In
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public List<string> Words { get; set; }
    }
}
