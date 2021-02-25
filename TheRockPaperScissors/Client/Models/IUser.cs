using System;

namespace TheRockPaperScissors.Client.Models
{
    public interface IUser
    {
        Guid Id { get; set; }

        string Login { get; set; }

        string Password { get; set; }
    }
}
