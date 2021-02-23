using System;

namespace TheRockPaperScissors.Server.Models
{
    public class User
    {
        public Guid Token { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public User(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
