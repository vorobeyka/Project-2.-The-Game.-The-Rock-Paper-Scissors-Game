using System;
using TheRockPaperScissors.Client.Menu;
using System.Collections.Generic;
using System.Text;

namespace TheRockPaperScissors.Client.Game
{
    public class GameModes
    {
        private readonly MenuDesign MenuDesign = new MenuDesign();

        public GameResult PlayInTrainingMode(Guid player)
        {
            MenuDesign.WriteHeader("TRAINING MODE");
            Game game = new Game(player);
            throw new NotImplementedException();
        }

        public GameResult PlayInPrivateMode(Guid player)
        {
            MenuDesign.WriteHeader("PRIVATE MODE");
            Game game = new Game(player);
            throw new NotImplementedException();
        }

        public GameResult PlayInPublicMode(Guid player)
        {
            MenuDesign.WriteHeader("PUBLIC MODE");
            Game game = new Game(player);
            throw new NotImplementedException();
        }
    }
}
