using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using GalaSoft.MvvmLight.CommandWpf;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace mainWindow
{
    class ViewModel : INotifyPropertyChanged
    {
        private DataGrid dataGrid;
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
        private PlotModel _plotModel;
        private List<ModelData> _dataPoitsList = new List<ModelData>();

        public List<ModelData> DataPoitsList
        {
            get { return _dataPoitsList; }
            set { _dataPoitsList = value; OnPropertyChanged("DataPoitsLists"); }
        }


        public ModelData Data
        {
            get { return _data; }
            set { _data = value; OnPropertyChanged("Data"); }
        }

      

        public PlotModel PlotModel
        {
            get { return _plotModel; }
            set { _plotModel = value; OnPropertyChanged("PlotModel"); }
        }

        public ViewModel()
        {
            _data = new ModelData();
            InitData(_data);
            _plotModel = new PlotModel();
        
        }

        public DataGrid DataGrid
        {
            get { return dataGrid; }
            set { dataGrid = value; }
        }

        private void InitData(ModelData data)
        {
            data.Height = 10000;
            data.Cy = 3.5;
            data.Ps = 1.167;
            data.MaxNumber = 0.6;
            data.Square = 200;
            data.Mass = 100000;
            data.Ba = 7;
            data.NMax = 1.5;
            data.Time = 100;
            data.L = 300;

            data.KDash = 0.87;
            data.BOne = 1.04;
            data.BTwo = 3.38;
            data.TimeOne = 0.085;
            data.TimeTwo = 0.00005;
        }

        private void AddDataPoint()
        {
            _dataPoitsList.Add(_data.Clone());
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = DataPoitsList;



        }

        public List<DataPoint> GetPoints(String propX, String propY)
        {
            List<DataPoint> list =new List<DataPoint>();
            foreach(ModelData data in _dataPoitsList)
            {
                list.Add(new DataPoint((double)data.GetType().GetProperty(propX).GetValue(data, null), (double)data.GetType().GetProperty(propY).GetValue(data, null)));   
            }
            
            return list;
        }



        private void ShowPMass()
        {
            //TODO: если из всех точек отобрать отличющиеся по массе, но совпадющие по базовым характерисиккам, чтобы не получилось каши
            UpdatePlot("Mass","P","P","масса, кг","зависимость P от массы",false,0);
        }

        public void ShowQMass()
        {

            UpdatePlot("Mass", "Q", "Q", "масса, кг", "зависимость Q от массы",false,0);

        }

        public void ShowPV()
        {
            UpdatePlot("Velocity", "P","P","скорость, м/с", "зависимость P от скорости",true,ModelData.MAXCONVERT);
        }

        public void ShowQV()
        {
            UpdatePlot("Velocity", "Q", "Q", "скорость, м/с", "зависимость Q от скорости", true, ModelData.MAXCONVERT);
        }


        public void ShowPH()
        {
            UpdatePlot("Height", "P", "P", "высота, м", "зависимость P от высоты", false,0);
        }

        public void ShowQH()
        {
            UpdatePlot("Height", "Q", "Q", "высота, м", "зависимость Q от высоты", false, 0);
        }

        #region Plottting methods

        private void UpdatePlot(String xProp, String yProp, String xAxis, String yAxis, String legend, bool dataIndepended, double upperBorder)
        {

            SetUpModel(xAxis, yAxis);
            LoadData(xProp, yProp, legend, dataIndepended, upperBorder);
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

        public void LoadData(String param1, String param2, String legend, bool dataIndependent, double upperBorder)
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
            if (dataIndependent)
            {
                lineSerie.Points.AddRange(_data.GetDepenedncyPointsPv(param1, param2, upperBorder));
            }
            else
            {
                if (this._dataPoitsList.Count <2)
                {
                    MessageBox.Show("Количество добаленных точек меньше двух");
                    return;
                }
                _dataPoitsList.Sort(delegate (ModelData c1, ModelData c2) { return c1.Mass.CompareTo(c2.Mass); });

                lineSerie.Points.AddRange(this.GetPoints(param1, param2));
            }
    
            PlotModel.Series.Add(lineSerie);


        }

#endregion

        private ICommand _calcMuXi;
        private ICommand _calcK;
        private ICommand _calcB;
        private ICommand _calcLambdaZero;
        private ICommand _calcLambdas;
        private ICommand _calcPQ;
        private ICommand _updateGraph;
        private ICommand _initializePlot;
        private ICommand _addDataPoint;
        private ICommand _showpv;
        private ICommand _showqv;
        private ICommand _showph;
        private ICommand _showqh;

        public ICommand CalcMu => _calcMuXi ?? (_calcMuXi = new RelayCommand(delegate { _data.CalcXi(); _data.CalcMu(); }));
        public ICommand CalcK => _calcK ?? (_calcK = new RelayCommand(_data.CalcK));
        public ICommand CalcB => _calcB ?? (_calcB = new RelayCommand(_data.CalcB));
        public ICommand CalcLambdaZero => _calcLambdaZero ?? (_calcLambdaZero = new RelayCommand(_data.CalcLambdaZero));
        public ICommand CalcLambdas => _calcLambdas ?? (_calcLambdas = new RelayCommand(_data.CalcLambdas));
        public ICommand CalcPQ => _calcPQ ?? (_calcPQ = new RelayCommand(_data.CalcPQ));
        public ICommand ShowPMassPlot => _initializePlot ?? (_initializePlot = new RelayCommand(ShowPMass));
        public ICommand ShowQMassPlot => _updateGraph ?? (_updateGraph = new RelayCommand(ShowQMass));
        public ICommand ShowPVPlot => _showpv ?? (_showpv = new RelayCommand(ShowPV));
        public ICommand ShowQVPlot => _showqv ?? (_showqv = new RelayCommand(ShowQV));
        public ICommand ShowPHPlot => _showph ?? (_showph = new RelayCommand(ShowPH));
        public ICommand ShowQHPlot => _showqh ?? (_showqh = new RelayCommand(ShowQH));

        public ICommand AddDataPonit => _addDataPoint ?? (_addDataPoint = new RelayCommand(AddDataPoint));

       





    }
}
