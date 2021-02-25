using System;
using TheRockPaperScissors.Client.Menu;
using TheRockPaperScissors.Client.Services;
using System.Threading.Tasks;
using TheRockPaperScissors.Client.Game.Enums;
using System.Text;

namespace TheRockPaperScissors.Client.Game
{
    public class GameModes
    {
        private readonly MenuDesign _menuDesign = new MenuDesign();

        public Task<GameResult> Play(Guid player, GameType gametype)
        {
            throw new NotImplementedException();
        }
    }
}
