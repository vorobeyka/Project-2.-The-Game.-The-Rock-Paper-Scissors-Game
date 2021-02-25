using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheRockPaperScissors.Server.Enums;

namespace TheRockPaperScissors.Server.Models
{
    public class Round
    {
        public string Id { get; set; }
        public Move Move { get; set; }
    }
}
