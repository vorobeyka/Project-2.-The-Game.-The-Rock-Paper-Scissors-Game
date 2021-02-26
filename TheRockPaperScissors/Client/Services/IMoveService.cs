using System;
using System.Threading.Tasks;
using TheRockPaperScissors.Client.Game.Enums;

namespace TheRockPaperScissors.Client.Services
{
    public interface IMoveService
    {
        public Task MakeMove(Guid token, Move move);
    }
}
