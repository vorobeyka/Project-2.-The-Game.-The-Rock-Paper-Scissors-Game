using System;
using System.Threading.Tasks;
using TheRockPaperScissors.Client.Game.Enums;

namespace TheRockPaperScissors.Client.Services
{
    public interface IGameConnectService
    {
        public Task<string> CreateGame(Guid token, GameType gameType);

        public Task<string> ConnectToPrivate(Guid token, GameType gameType, string id);
    }
}
