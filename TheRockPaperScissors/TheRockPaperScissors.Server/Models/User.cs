using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheRockPaperScissors.Server.Models
{
    public class User
    {
        [Required]
        [StringLength(64, MinimumLength = 3)]
        public string Login { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
