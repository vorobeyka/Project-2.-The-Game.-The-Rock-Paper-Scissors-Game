using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheRockPaperScissors.Server.Models
{
    public class User
    {
        public Statistics Statistics { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 4)]
        public string Login { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
