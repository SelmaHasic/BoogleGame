using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoogleGame.Models
{
    public class Out
    {
        public string UserName { get; set; }
        public int Score { get; set; }
        public List<string> InvalidWords { get; set; }
    }
}
