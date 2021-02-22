using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRockPaperScissors.Client.Models
{
    internal class User : IUser
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public User(Guid id, string login, string password)
        {
            Id = id;
            Login = login;
            Password = password;
        }
    }
}
