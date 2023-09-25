using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Lrc.ViewModel
{
    public  class PlayerViewModel :BaseViewModel
    {

        private String _name;
        
        private bool _isWinner;

        public string Name 
        {
            get { return _name;  }
            set { _name = value; OnPropertyChanged(nameof(Name));  }
        }

        public bool IsWinner
        {
            get { return _isWinner; }
            set { _isWinner = value; OnPropertyChanged(nameof(IsWinner));  }
        }
        

        BitmapImage _playerImage = (BitmapImage)Application.Current.Resources["Player"];
       
        public BitmapImage PlayerImage 
        {
            get { return _playerImage; }
            set { _playerImage = value; OnPropertyChanged(nameof(PlayerImage)); }
        
        
        }

        public void SetWinner()
        { 
           
            
           PlayerImage = (BitmapImage)Application.Current.Resources["Winner"];
           IsWinner = true;
        }

        public void SetPlayer()
        {
            PlayerImage = (BitmapImage)Application.Current.Resources["Player"];
            IsWinner = false;
        }

    }
}
