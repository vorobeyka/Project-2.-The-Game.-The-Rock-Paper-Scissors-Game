using System;

namespace TheRockPaperScissors.Client.Models
{
    public class User : IUser
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public User(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public User(Guid id, string login, string password)
        {
            Id = id;
            Login = login;
            Password = password;
        }
    }
}
