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
        //контейнеры для точек, кторе будут использоваться для пострения графиков
        private DataGrid dataGridMass; 
        private DataGrid dataGridSpeed;
        //binding stuff
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
        private List<ModelData> _dataMassPointsList = new List<ModelData>();
        private List<ModelData> _dataSpeedPointsList = new List<ModelData>();

        public enum SortByWhat
        {
            Mass,
            Speed
        };

        
        

        public List<ModelData> DataMassPointsList
        {
            get { return _dataMassPointsList; }
            set { _dataMassPointsList = value; OnPropertyChanged("DataMassPointsList"); }
        }

        public List<ModelData> DataSpeedPointsList
        {
            get { return _dataSpeedPointsList; }
            set { _dataSpeedPointsList = value; OnPropertyChanged("DataSpeedPointsList"); }
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

        public DataGrid DataGridMass
        {
            get { return dataGridMass; }
            set { dataGridMass = value; }
        }

        public DataGrid DataGridSpeed
        {
            get { return dataGridSpeed; }
            set { dataGridSpeed = value; }
        }


        //TODO: delete 
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

        private void AddDataPointForMassList()
        {
            if (_data.P.Equals(0))
            {
                return;
            }
            _dataMassPointsList.Add(_data.Clone());
            dataGridMass.ItemsSource = null;
            dataGridMass.ItemsSource = DataMassPointsList;
        }

        private void AddDataPointForSpeedList()
        {
            if (_data.P.Equals(0))
            {
                return;
            }
            _dataSpeedPointsList.Add(_data.Clone());
            dataGridSpeed.ItemsSource = null;
            dataGridSpeed.ItemsSource = DataSpeedPointsList;
        }

        //?????
        public List<DataPoint> GetPoints(String propX, String propY, List<ModelData> dataPointsList)
        {
            List<DataPoint> list =new List<DataPoint>();
            foreach(ModelData data in dataPointsList)
            {
                list.Add(new DataPoint((double)data.GetType().GetProperty(propX).GetValue(data, null), (double)data.GetType().GetProperty(propY).GetValue(data, null)));   
            }
            
            return list;
        }



        private void ShowPMass()
        {
            //TODO: если из всех точек отобрать отличющиеся по массе, но совпадющие по базовым характерисиккам, чтобы не получилось каши
            UpdatePlot("Mass","P","P","масса, кг","зависимость P от массы",false,this._dataMassPointsList,SortByWhat.Mass);
        }

        public void ShowQMass()
        {

            UpdatePlot("Mass", "Q", "Q", "масса, кг", "зависимость Q от массы",false, this._dataMassPointsList, SortByWhat.Mass);

        }

        public void ShowPV()
        {
            UpdatePlot("Velocity", "P","P","скорость, м/с", "зависимость P от скорости",false, this._dataSpeedPointsList, SortByWhat.Speed);
        }

        public void ShowQV()
        {
            UpdatePlot("Velocity", "Q", "Q", "скорость, м/с", "зависимость Q от скорости", false, this._dataSpeedPointsList, SortByWhat.Speed);
        }


     

        #region Plottting methods

        private void UpdatePlot(String xProp, String yProp, String xAxis, String yAxis, String legend, bool dataIndepended, List<ModelData> dataPointsList, SortByWhat sortByWhat)
        {

            SetUpAxes(xAxis, yAxis);
            LoadData(xProp, yProp, legend, dataIndepended, dataPointsList,sortByWhat);
        }

        private void SetUpAxes(String xAxis, String yAxis)
        {
           
            PlotModel.Axes.Clear();
            PlotModel.Axes.Add(new LinearAxis(AxisPosition.Left, 0) { Title = xAxis });
            PlotModel.Axes.Add(new LinearAxis(AxisPosition.Bottom, 0) { Title = yAxis });

        }

        public void LoadData(String param1, String param2, String legend, bool dataIndependent, List<ModelData> dataPointsList, SortByWhat sortByWhat)
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
            //если точки можно рассчиать без пользовательских данных
            if (dataIndependent)
            {
                //lineSerie.Points.AddRange(_data.GetDepenedncyPointsPv(param1, param2, upperBorder));
            }
            else
            {
                if (dataPointsList != null && dataPointsList.Count <2)
                {
                    MessageBox.Show("Количество добаленных точек меньше двух");
                    return;
                }

                if (sortByWhat.Equals(SortByWhat.Mass))
                {
                    //сортировка  точек в листе по массе
                    dataPointsList.Sort(delegate (ModelData c1, ModelData c2) { return c1.Mass.CompareTo(c2.Mass); });
                }
                else if (sortByWhat.Equals(SortByWhat.Speed))
                {
                    dataPointsList.Sort(delegate (ModelData c1, ModelData c2) { return c1.Velocity.CompareTo(c2.Velocity); });   
                }

                lineSerie.Points.AddRange(this.GetPoints(param1, param2,dataPointsList));



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
        private ICommand _addDataPointMass;
        private ICommand _addDataPointSpeed;
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
       

        public ICommand AddDataPonitMass => _addDataPointMass ?? (_addDataPointMass = new RelayCommand(AddDataPointForMassList));
        public ICommand AddDataPonitSpeed => _addDataPointSpeed ?? (_addDataPointSpeed = new RelayCommand(AddDataPointForSpeedList));

       





    }
}
