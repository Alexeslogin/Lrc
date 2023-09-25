using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;


namespace Lrc.ViewModel
{

    public class GameChartsViewModel : BaseViewModel
    {
        public SeriesCollection SeriesCollection { get; set; } = new();

        public int _xAxis;
        public int XAxis 
        {
            get { return _xAxis; }
            set { _xAxis = value; OnPropertyChanged(nameof(XAxis)); }
        }

        public GameChartsViewModel()
        {
            SeriesCollection.Add(new LineSeries { Title = "Max", Values = new ChartValues<ObservablePoint>(), Fill = new SolidColorBrush(Colors.Transparent) });
            SeriesCollection.Add(new LineSeries { Title = "Min", Values = new ChartValues<ObservablePoint>(), Fill = new SolidColorBrush(Colors.Transparent) });
            SeriesCollection.Add(new LineSeries { Title = "Avg", Values = new ChartValues<ObservablePoint>(), Fill = new SolidColorBrush(Colors.Transparent), PointGeometrySize = 0 });
            SeriesCollection.Add(new LineSeries { Title = "Games", Values = new ChartValues<ObservablePoint>(), Fill = new SolidColorBrush(Colors.Transparent), PointGeometrySize = 0 });
        }



        public void Clear() 
        {
            Application.Current.Dispatcher.BeginInvoke(() =>
            SeriesCollection.ToList().ForEach(x => x.Values.Clear()));

        }

        public void AddPoint(string chartTitle, int x, int y)
        {
            Application.Current.Dispatcher.BeginInvoke(() => 
            {
                var series = SeriesCollection.FirstOrDefault(x => x.Title.ToLower() == chartTitle.ToLower());
                if (series != null)
                {
                    series.Values.Add(new ObservablePoint(x,y));
                    OnPropertyChanged(nameof(SeriesCollection));
                }
            });

        }


        public void FillChart(List<int> data)
        {

            
            SeriesCollection.ToList().ForEach(x => x.Values.Clear());
            OnPropertyChanged(nameof(SeriesCollection));

            var min = data.Min();
            var minIdx = data.IndexOf(min);
            var max = data.Max();
            var maxIdx = data.IndexOf(max);
            var average = data.Average();

            Application.Current.Dispatcher.BeginInvoke(() =>
            {

                var maxSeries = SeriesCollection.FirstOrDefault(x => x.Title == "Max");
                if (maxSeries != null)
                {
                    
                    maxSeries.Values.Add(new ObservablePoint(maxIdx + 1, max));
                }
                var minSeries = SeriesCollection.FirstOrDefault(x => x.Title == "Min");
                if (minSeries != null)
                {

                    minSeries.Values.Add(new ObservablePoint(minIdx + 1, min));
                }
                var gameSeries = SeriesCollection.FirstOrDefault(x => x.Title == "Games");
                if (gameSeries != null)
                {
                    for(int i = 0; i< data.Count; i++)
                    gameSeries.Values.Add(new ObservablePoint(i+1, data[i]));
                }


                OnPropertyChanged(nameof(SeriesCollection));
            });
        }

    }
}
