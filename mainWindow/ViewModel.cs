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
        private DataGrid dataGridMass;
        private DataGrid dataGridMass2;
        private DataGrid dataGridSpeed;
        private DataGrid dataGridSpeed2;
        #region binding stuff
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
        private ModelData _data;
        private PlotModel _plotModel;
        private PointsDummy _pointsDummyMass;
        private PointsDummy _pointsDummySpeed;
        private List<ModelData> _dataMassPointsList = new List<ModelData>();
        private List<ModelData> _dataSpeedPointsList = new List<ModelData>();

        public enum SortByWhat
        {
            Mass,
            Speed
        };




        #region binding properties

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

        public PointsDummy PointsDummyMass
        {
            get { return _pointsDummyMass; }
            set { _pointsDummyMass = value; OnPropertyChanged("PointsDummyMass"); }
        }

        public PointsDummy PointsDummySpeed
        {
            get { return _pointsDummySpeed; }
            set { _pointsDummySpeed = value; OnPropertyChanged("PointsDummySpeed"); }
        }

        #endregion

        public ViewModel()
        {
            _data = new ModelData();
            //TESTING
            // InitData(_data);
            _plotModel = new PlotModel();
            _pointsDummyMass = new PointsDummy();
            _pointsDummySpeed = new PointsDummy();

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


        public DataGrid DataGridMass2
        {
            get { return dataGridMass2; }
            set { dataGridMass2 = value; }
        }

        public DataGrid DataGridSpeed2
        {
            get { return dataGridSpeed2; }
            set { dataGridSpeed2 = value; }
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

        private void AddDataPointForMassList()
        {
            if (_data.P.Equals(0))
            {
                return;
            }
            _dataMassPointsList.Add(_data.Clone());
            dataGridMass.ItemsSource = null;
            dataGridMass.ItemsSource = DataMassPointsList;
            dataGridMass2.ItemsSource = null;
            dataGridMass2.ItemsSource = DataMassPointsList;
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
            dataGridSpeed2.ItemsSource = null;
            dataGridSpeed2.ItemsSource = DataSpeedPointsList;
        }


        public List<DataPoint> GetXYPointsFromAll(String propX, String propY, List<ModelData> dataPointsList)
        {
            List<DataPoint> list = new List<DataPoint>();
            foreach (ModelData data in dataPointsList)
            {
                list.Add(new DataPoint((double)data.GetType().GetProperty(propX).GetValue(data, null), (double)data.GetType().GetProperty(propY).GetValue(data, null)));
            }

            return list;
        }



        private void ShowPMass()
        {
            UpdatePlot("Mass", "P", "P", "m, кг", "зависимость P от массы", false, this._dataMassPointsList, SortByWhat.Mass);
        }

        public void ShowQMass()
        {

            UpdatePlot("Mass", "Q", "Q", "m, кг", "зависимость Q от массы", false, this._dataMassPointsList, SortByWhat.Mass);

        }

        public void ShowPV()
        {
            UpdatePlot("Velocity", "P", "P", "V, м/с", "зависимость P от скорости", false, this._dataSpeedPointsList, SortByWhat.Speed);
        }

        public void ShowQV()
        {
            UpdatePlot("Velocity", "Q", "Q", "V, м/с", "зависимость Q от скорости", false, this._dataSpeedPointsList, SortByWhat.Speed);
        }



#region fast points calc
        //sorry for that =)
        public void Calc5Mass()
        {
            //copy some data
            PointsDummyMass.ModelData1 = _data.CopyDataForMassCalc(PointsDummyMass.ModelData1);
            PointsDummyMass.ModelData2 = _data.CopyDataForMassCalc(PointsDummyMass.ModelData2);
            PointsDummyMass.ModelData3 = _data.CopyDataForMassCalc(PointsDummyMass.ModelData3);
            PointsDummyMass.ModelData4 = _data.CopyDataForMassCalc(PointsDummyMass.ModelData4);
            PointsDummyMass.ModelData5 = _data.CopyDataForMassCalc(PointsDummyMass.ModelData5);

            //do calculation
            PointsDummyMass.ModelData1.CalcMu();
            PointsDummyMass.ModelData2.CalcMu();
            PointsDummyMass.ModelData3.CalcMu();
            PointsDummyMass.ModelData4.CalcMu();
            PointsDummyMass.ModelData5.CalcMu();

            PointsDummyMass.ModelData1.CalcXi();
            PointsDummyMass.ModelData2.CalcXi();
            PointsDummyMass.ModelData3.CalcXi();
            PointsDummyMass.ModelData4.CalcXi();
            PointsDummyMass.ModelData5.CalcXi();
        }

        public void Calc5KDash()
        {
            PointsDummyMass.ModelData1.CalcK();
            PointsDummyMass.ModelData2.CalcK();
            PointsDummyMass.ModelData3.CalcK();
            PointsDummyMass.ModelData4.CalcK();
            PointsDummyMass.ModelData5.CalcK();

            PointsDummyMass.ModelData1.CalcB();
            PointsDummyMass.ModelData2.CalcB();
            PointsDummyMass.ModelData3.CalcB();
            PointsDummyMass.ModelData4.CalcB();
            PointsDummyMass.ModelData5.CalcB();

            PointsDummyMass.ModelData1.CalcLambdaZero();
            PointsDummyMass.ModelData2.CalcLambdaZero();
            PointsDummyMass.ModelData3.CalcLambdaZero();
            PointsDummyMass.ModelData4.CalcLambdaZero();
            PointsDummyMass.ModelData5.CalcLambdaZero();

            PointsDummyMass.ModelData1.CalcLambdas();
            PointsDummyMass.ModelData2.CalcLambdas();
            PointsDummyMass.ModelData3.CalcLambdas();
            PointsDummyMass.ModelData4.CalcLambdas();
            PointsDummyMass.ModelData5.CalcLambdas();

            PointsDummyMass.ModelData1.CalcPQ();
            PointsDummyMass.ModelData2.CalcPQ();
            PointsDummyMass.ModelData3.CalcPQ();
            PointsDummyMass.ModelData4.CalcPQ();
            PointsDummyMass.ModelData5.CalcPQ();

            DataMassPointsList.Clear();
            DataMassPointsList.Add(PointsDummyMass.ModelData1);
            DataMassPointsList.Add(PointsDummyMass.ModelData2);
            DataMassPointsList.Add(PointsDummyMass.ModelData3);
            DataMassPointsList.Add(PointsDummyMass.ModelData4);
            DataMassPointsList.Add(PointsDummyMass.ModelData5);

            dataGridMass.ItemsSource = null;
            dataGridMass.ItemsSource = DataMassPointsList;
            dataGridMass2.ItemsSource = null;
            dataGridMass2.ItemsSource = DataMassPointsList;
        }

        public void Calc5Speed()
        {
            PointsDummySpeed.ModelData1 = _data.CopyDataForSpeedCalc(PointsDummySpeed.ModelData1);
            PointsDummySpeed.ModelData2 = _data.CopyDataForSpeedCalc(PointsDummySpeed.ModelData2);
            PointsDummySpeed.ModelData3 = _data.CopyDataForSpeedCalc(PointsDummySpeed.ModelData3);
            PointsDummySpeed.ModelData4 = _data.CopyDataForSpeedCalc(PointsDummySpeed.ModelData4);
            PointsDummySpeed.ModelData5 = _data.CopyDataForSpeedCalc(PointsDummySpeed.ModelData5);


            PointsDummySpeed.ModelData1.CalcALL();
            PointsDummySpeed.ModelData2.CalcALL();
            PointsDummySpeed.ModelData3.CalcALL();
            PointsDummySpeed.ModelData4.CalcALL();
            PointsDummySpeed.ModelData5.CalcALL();

            DataSpeedPointsList.Clear();
            DataSpeedPointsList.Add(PointsDummySpeed.ModelData1);
            DataSpeedPointsList.Add(PointsDummySpeed.ModelData2);
            DataSpeedPointsList.Add(PointsDummySpeed.ModelData3);
            DataSpeedPointsList.Add(PointsDummySpeed.ModelData4);
            DataSpeedPointsList.Add(PointsDummySpeed.ModelData5);
            
            dataGridSpeed.ItemsSource = null;
            dataGridSpeed.ItemsSource = DataSpeedPointsList;
            dataGridSpeed2.ItemsSource = null;
            dataGridSpeed2.ItemsSource = DataSpeedPointsList;
        }

        #endregion




        #region Plottting methods

        private void UpdatePlot(String xProp, String yProp, String xAxis, String yAxis, String legend, bool dataIndepended, List<ModelData> dataPointsList, SortByWhat sortByWhat)
        {

            LoadData(xProp, yProp, legend, dataIndepended, dataPointsList, sortByWhat);
            SetUpAxes(xAxis, yAxis);
        }

        private void SetUpAxes(String xAxisTitle, String yAxisTitle)
        {

            PlotModel.Axes.Clear();
            var xAxis = new OxyPlot.Axes.LinearAxis()
            {
                Position = AxisPosition.Left,
                Title = xAxisTitle,
                TitlePosition = 1

            };

            var yAxis = new OxyPlot.Axes.LinearAxis()
            {
                Position = AxisPosition.Bottom,
                Title = yAxisTitle,
                TitlePosition = 1
              
            };

    
            
           PlotModel.Axes.Add(xAxis);
           PlotModel.Axes.Add(yAxis); 
           
            //PlotModel.Axes.Add(new LinearAxis(AxisPosition.Left, "X") { Title = xAxisTitle });
            //PlotModel.Axes.Add(new LinearAxis(AxisPosition.Bottom, "Y") { Title = yAxisTitle });

            PlotModel.PlotMargins = new OxyThickness(40, 40, 40, 40);
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
                if (dataPointsList != null && dataPointsList.Count < 2)
                {
                    MessageBox.Show("Количество добаленных точек меньше двух");
                    new Tuple<double, double>(0, 0);
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
            
                lineSerie.Points.AddRange(this.GetXYPointsFromAll(param1, param2, dataPointsList));
            }

            PlotModel.Series.Add(lineSerie);
        }

        #endregion

        #region click handlers
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
        //для построения графиков
        private ICommand _put5Mass;
        private ICommand _put5Kdash;
        private ICommand _put5Speed;

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

        public ICommand Put5Mass => _put5Mass ?? (_put5Mass = new RelayCommand(Calc5Mass));
        public ICommand Put5Kdash => _put5Kdash ?? (_put5Kdash = new RelayCommand(Calc5KDash));
        public ICommand Put5Speed => _put5Speed ?? (_put5Speed = new RelayCommand(Calc5Speed));

        #endregion
    }
}
