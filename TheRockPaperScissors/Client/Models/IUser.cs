using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRockPaperScissors.Client.Models
{
    internal interface IUser
    {
        Guid Id { get; set; }
        string Login { get; set; }
        string Password { get; set; }
        
    }
}
