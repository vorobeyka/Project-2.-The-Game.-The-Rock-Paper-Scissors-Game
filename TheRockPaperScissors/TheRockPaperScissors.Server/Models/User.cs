using System;
using System.ComponentModel.DataAnnotations;

namespace TheRockPaperScissors.Server.Models
{
    public class User
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 6)]
        public string Login { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 3)]
        public string Password { get; set; }

/*        public User(string login, string password)
        {
            Login = login;
            Password = password;
        }*/
    }
}
