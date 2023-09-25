using Lrc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Lrc.Model;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Windows;
using System.Threading;

namespace Lrc.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {

       CancellationTokenSource cancellationTokenSource;
        
       private GameService _gameService;

       public ICommand PlayCommand { get; set; }

       public ICommand StopCommand { get; set; }

       public ObservableCollection<GameSettings> Settings { get; set; }
       
       private GameSettings _selectedSettings;
       public GameSettings SelectetSettings
       {
            get { return _selectedSettings; }

            set 
             { 
                _selectedSettings = value;   
                GameSettings.Players = _selectedSettings.Players;
                GameSettings.Games = _selectedSettings.Games;  
            }
       }
       
       public PlayersViewModel Players { get; set; }

       private GameSettingsViewModel _gameSettings;
       
       public GameSettingsViewModel GameSettings
        {
         get 
         { 
            return _gameSettings; 
         }
         set { 
               _gameSettings = value;
                OnPropertyChanged(nameof(GameSettings)); }

        }

       public GameChartsViewModel GameCharts { get; set; }

       private bool _isRunning;
       public bool IsRunning
       {
            get { return _isRunning; }
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    OnPropertyChanged(nameof(IsRunning));
                }
            }
        }

        public MainWindowViewModel()
       {
            PlayCommand = new RelayCommand(Play);
            StopCommand = new RelayCommand(Stop);
            _gameService = new GameService();
           
            Init();
       }

        private void Init()
        {   
            Players = new PlayersViewModel();
            
            Players = new();
           
            GameSettings = new GameSettingsViewModel();
            GameSettings.OnPlayerChanged += GameSettings_OnPlayerChanged;
            GameSettings.OnGameChanged += GameSettings_OnGameChanged;
            Settings = new ObservableCollection<GameSettings>(_gameService.GetPresets());
            SelectetSettings = Settings.First();
            GameCharts = new GameChartsViewModel();
            
            SetPlayers();
            SetChartAxis();
            
        }

        private void GameSettings_OnGameChanged(object source, EventArgs eventArgs)
        {
            SetChartAxis();
        }

        private void GameSettings_OnPlayerChanged(object source, EventArgs eventArgs)
        {
            SetPlayers();
        }

        public async void Play(object parameter)
        {
            //var game = _gameService.GetGame(new GameSettings { Games = GameSettings.Games, Players = GameSettings.Players });
            //game.Play();
            //var turns = game.Turns;
            //var winner = game.GetWinner();
            //var index = game.Players.ToList().IndexOf(winner);


            //GameCharts.FillChart(turns.ToList());
            //Players.SetWinners(index);

            cancellationTokenSource = new CancellationTokenSource();
            
            try
            {
                await Task.Run(() => PlayTask(parameter, cancellationTokenSource.Token), cancellationTokenSource.Token);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Game was canceled.");

            }


        }

        private void PlayTask(object parameter, CancellationToken cancellationToken)
        {
            var game = _gameService.GetGame(new GameSettings { Games = GameSettings.Games, Players = GameSettings.Players });

            game.Play();
            var turns = game.Turns.ToList();
            var winner = game.GetWinner();
            var index = game.Players.ToList().IndexOf(winner);
            GameCharts.Clear();
           


            for (int i = 0; i < turns.Count(); i++)
            {
                Thread.Sleep(100);
                cancellationToken.ThrowIfCancellationRequested();
                GameCharts.AddPoint("Games", i + 1, turns[i]);

                GameCharts.AddPoint("Avg", i + 1, (int)turns.Average());

            }

            var min = turns.Min();
            GameCharts.AddPoint("Min", turns.IndexOf(min)+1, (int)min);
            
            var max = turns.Max();
            GameCharts.AddPoint("Max", turns.IndexOf(max) + 1, (int)max);


            //GameCharts.FillChart(turns.ToList());
            Dispatcher.CurrentDispatcher.Invoke(()=> Players.SetWinners(index));
        }


        public void Stop(object parameter)
        {
            if (cancellationTokenSource != null)
            {
                // Cancel the operation by canceling the token
                cancellationTokenSource.Cancel();
            }

        }



        private void SetPlayers()
        {

            Players.Players.Clear();
            for (int i = 0; i < GameSettings.Players; i++)
            {
                Players.Players.Add(new PlayerViewModel { Name = $"Player {i + 1}" });
            }

           
        
        }

        private void SetChartAxis()
        {
            if (GameCharts != null)
            {
                GameCharts.Clear();
                GameCharts.XAxis = GameSettings.Games;
            }
        }

    }
}
