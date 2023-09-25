using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Lrc.Model
{
    public class Player
    {

        public int Id { get; private set; }
        public string Name { get; private set; }

        public int Chips { get; private set; }

        public int Turns { get; private set; }

        public int Wins { get; private set; }



        public Player(int id, string name, int chips)
        {
            Id = id;
            Name = name;
            Chips = chips;
           
        }

        public void TakeChip()
        {
           if(Chips>0)
                { Chips--; }
        }

        public void PassChip(Player recipient)
        {
            if (Chips > 0)
            {
                Chips--;
                recipient.Chips++;
            }
        }

        public void PlayerTurns()
        {
            Turns++;
        }

        public void Reset(int chips)
        {
            Chips = chips;
            Turns = 0;
        }

        public bool IsOutChips()
        {
            return Chips <= 0;
        }

        public void AddWin()
        { 
           Wins++;
        }

    }
}
