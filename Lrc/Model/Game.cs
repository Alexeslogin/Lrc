using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lrc.Model
{
    public class Game
    {  
        private const int chips = 3;

        private List<Player> _players = new();
        
        private IDiceService _diceService;
        
        private List<int> _turns = new();
        private int _gameCount;

        private GameSettings _gameSettings;
       
        public IEnumerable<Player> Players { get { return _players.AsReadOnly(); } }

        public IEnumerable<int> Turns { get { return _turns.AsReadOnly(); } }

     
        public Game(IDiceService diceService, GameSettings settings)
        { 
             _diceService = diceService;
            _gameSettings = settings;
            SetPlayers();
        }

        private void  SetPlayers()
        {
            for (int i = 0; i < _gameSettings.Players; i++ )
            {
                AddPlayer(new Player(i + 1, $"Player{i+1}",chips));
            }
        
        }

        public void AddPlayer(Player player)
        {
            _players.Add(player);
        }

        public bool HasWinner()
        {   
            return _players.Where(x => !x.IsOutChips()).Count() == 1;
        }

        public void Play()
        {
            for (int i = 0; i < _gameSettings.Games; ++i)
            {
                _players.ForEach(p => p.Reset(3));
                
                while (!HasWinner())
                {  
                    foreach (var player in _players)
                    {
                        if (HasWinner())
                        {
                            var winner = _players.MaxBy(x => x.Chips);
                            player.AddWin(); 
                            break;
                        }

                        if (!player.IsOutChips())
                        {
                            player.PlayerTurns();
                            var dices = _diceService.GetDiceResult(player.Chips < 3 ? player.Chips : 3);
                            foreach (var dice in dices)
                            {
                                switch (dice)
                                {
                                    case 'C':
                                        player.TakeChip();
                                        break;
                                    case 'L':
                                        player.PassChip(_players[(_players.IndexOf(player) + _players.Count - 1) % _players.Count]);
                                        break;
                                    case 'R':
                                        player.PassChip(_players[(_players.IndexOf(player) + 1) % _players.Count]);
                                        break;
                                    default:
                                        break;

                                }

                            }

                        }
                    }
                }
                _turns.Add(_players.Sum(x => x.Turns));
              }
        }

        public Player? GetWinner()
        {

            return _players.MaxBy(x => x.Wins);
            //if (!HasWinner()) return null;
            //return _players.FirstOrDefault(x => !x.IsOutChips());
        }





        

    }
}
