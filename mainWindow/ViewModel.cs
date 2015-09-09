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
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace mainWindow
{
    class ViewModel : INotifyPropertyChanged
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

        private ModelData _data;
        private ModelGraph _graph;
        private PlotModel _plotModel;



        public ModelData Data
        {
            get { return _data; }
            set { _data = value; OnPropertyChanged("Data"); }
        }

        public ModelGraph Graph
        {
            get { return _graph; }
            set { _graph = value; OnPropertyChanged("Graph"); }
        }

        public PlotModel PlotModel
        {
            get { return _plotModel; }
            set { _plotModel = value; OnPropertyChanged("PlotModel"); }
        }

        public ViewModel()
        {
            _data = new ModelData();
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
            _plotModel = new PlotModel();




        }

        /// <summary>
        /// Ints plot by clock
        /// </summary>
        private void InitPlot()
        {

            UpdatePlot("Mass","P","P","масса, кг","зависимость P от массы");
        }

        public void Doshit()
        {

            UpdatePlot("Mass", "Q", "Q", "масса, кг", "зависимость Q от массы");

        }

        private void UpdatePlot(String xProp, String yProp, String xAxis, String yAxis, String legend)
        {

            SetUpModel(xAxis, yAxis);
            LoadData(xProp, yProp, legend);
        }

        private void SetUpModel(String xAxis, String yAxis)
        {
            /*
            PlotModel.LegendTitle = legend;
            PlotModel.LegendOrientation = LegendOrientation.Horizontal;
            PlotModel.LegendPlacement = LegendPlacement.Outside;
            PlotModel.LegendPosition = LegendPosition.TopRight;
            PlotModel.LegendBackground = OxyColor.FromAColor(200, OxyColors.White);
            PlotModel.LegendBorder = OxyColors.Black;
            */
            PlotModel.Axes.Clear();
            PlotModel.Axes.Add(new LinearAxis(AxisPosition.Left, 0) { Title = xAxis });
            PlotModel.Axes.Add(new LinearAxis(AxisPosition.Bottom, 0) { Title = yAxis });


        }

        public void LoadData(String param1, String param2, String legend)
        {
            if (PlotModel != null)
            {
                PlotModel.Series.Clear();
            }
            else
            {
                return;
            }
            var lineSerie = new LineSeries
            {
                StrokeThickness = 2,
                MarkerSize = 3,
                CanTrackerInterpolatePoints = false,
                Title = legend,
                Smooth = false,
            };
            lineSerie.Points.AddRange(_data.GetDepenedncyPointsPv(param1, param2));
            PlotModel.Series.Add(lineSerie);


        }



        private ICommand _calcMuXi;
        private ICommand _calcK;
        private ICommand _calcB;
        private ICommand _calcLambdaZero;
        private ICommand _calcLambdas;
        private ICommand _calcPQ;
        private ICommand _updateGraph;
        private ICommand _initializePlot;

        public ICommand CalcMu => _calcMuXi ?? (_calcMuXi = new RelayCommand(delegate { _data.CalcXi(); _data.CalcMu(); }));
        public ICommand CalcK => _calcK ?? (_calcK = new RelayCommand(_data.CalcK));
        public ICommand CalcB => _calcB ?? (_calcB = new RelayCommand(_data.CalcB));
        public ICommand CalcLambdaZero => _calcLambdaZero ?? (_calcLambdaZero = new RelayCommand(_data.CalcLambdaZero));
        public ICommand CalcLambdas => _calcLambdas ?? (_calcLambdas = new RelayCommand(_data.CalcLambdas));
        public ICommand CalcPQ => _calcPQ ?? (_calcPQ = new RelayCommand(_data.CalcPQ));
        public ICommand InitializePlot => _initializePlot ?? (_initializePlot = new RelayCommand(InitPlot));
        public ICommand UpdateGraphCommand => _updateGraph ?? (_updateGraph = new RelayCommand(Doshit));

       





    }
}
