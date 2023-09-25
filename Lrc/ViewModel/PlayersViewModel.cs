using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lrc.ViewModel
{
    public class PlayersViewModel
    {
        public ObservableCollection<PlayerViewModel> Players { get; set; } = new();


        public void SetWinners(int index)
        {
            var player = Players.FirstOrDefault(x => x.IsWinner);
            if (player != null)
                player.SetPlayer();
            var winner = Players[index];
            winner.SetWinner();
        
        
        }

    }
}
