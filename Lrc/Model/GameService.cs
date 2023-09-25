using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lrc.Model
{
    internal class GameService
    {

        public readonly List<GameSettings> _settings = new() {
         new GameSettings { Players = 3, Games=100 },
         new GameSettings { Players = 4, Games=100 },
         new GameSettings { Players = 5, Games=100 },
         new GameSettings { Players = 5, Games=1000 },
         new GameSettings { Players = 5, Games=10000 },
         new GameSettings { Players = 5, Games=100000 },
         new GameSettings { Players = 6, Games=100 },
         new GameSettings { Players = 7, Games=100 }

        };


        public Game GetGame(GameSettings gameSettings)
        {

            Game game = new Game(new LDCDiceService(), gameSettings);
            return game;

        }

        public List<GameSettings> GetPresets()
        {
            return _settings;

        }






    }
}
