using Lrc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace Lrc.ViewModel
{
    public class GameSettingsViewModel : BaseViewModel
    {   


        public GameSettingsViewModel() 
        {
        
        
        }




        private int _games;

        public event SettingsChanged OnPlayerChanged;
        public event SettingsChanged OnGameChanged;

        public int Games 
        {
            get { return _games;  } 
            set 
            { 
                _games = value; 
                OnPropertyChanged(nameof(Games));
                if (OnGameChanged != null)
                {
                    var gameChanged = OnGameChanged;
                    gameChanged(this, new EventArgs());
                }
            }
        }

        public int _players { get; set; }
        public int Players
        {
            get { return _players; }
            set 
            { 
                _players = value; 
                OnPropertyChanged(nameof(Players));
                if (OnPlayerChanged != null)
                {
                    var playersChanged = OnPlayerChanged;
                    OnPlayerChanged(this, new EventArgs());
                }
            }
        }

    }

    public delegate void SettingsChanged(object source, EventArgs eventArgs);
}
