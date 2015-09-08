using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace mainWindow
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private MainWindowModel _data;
        private MainWindowGraph _graph;


        public MainWindowModel Data
        {
            get { return _data; }
            set { _data = value; OnPropertyChanged("Data"); }
        }

        public MainWindowGraph Graph
        {
            get { return _graph; }
            set { _graph = value; OnPropertyChanged("Graph"); }
        }

        public MainWindowViewModel()
        {
            _data= new MainWindowModel();
            _data.Height = 10000;
            _data.Cy = 3.5;
            _data.Ps = 1.167;
            _data.MaxNumber = 0.6;
            _data.Square = 200;
            _data.Mass = 100000;
            _data.Ba = 7;
            _data.NMax = 1.5;
            _data.Time = 100;
            _data.L = 300;

            _data.KDash = 0.87;
            _data.BOne = 1.04;
            _data.BTwo = 3.38;
            _data.TimeOne = 0.085;
            _data.TimeTwo = 0.0005;

            _graph = new MainWindowGraph();
            MainWindowGraph mainWindowGraph = new MainWindowGraph();
           // _graph.Points = _data.GetDepenedncyPointsPv("Mass", "Q");
        }

        private ICommand _calcMu;

        public ICommand CalcMu => _calcMu ?? (_calcMu = new RelayCommand(_data.CalcMu));
    }
}
