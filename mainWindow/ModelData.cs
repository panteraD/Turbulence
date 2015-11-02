using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;

namespace mainWindow
{
    public class ModelData : INotifyPropertyChanged
    {
        private MainWindow window;
        public static Double MAXCONVERT = 340.3f;
        private static Double GFORCE = 9.81;
        private static Double NU = 0.4; //only for M < 1

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        #region Constructors

        public ModelData()
        {

        }

        public ModelData Clone()
        {
            ModelData newData = new ModelData();
            newData.Mass = _mass;
            newData.Cy = _c_y;
            newData.Ps = _p_s;
            newData.Ba = _b_a;
            newData.MaxNumber = _max_number;
            newData.Square = _square;
            newData.NMax = _n_max;
            newData.Time = _time;
            newData.TimeOne = _time1;
            newData.TimeTwo = _time2;
            newData.L = _l;
            newData.Velocity = _velocity;
            newData.Height = _height;
            newData.Mu = _mu;
            newData.Xi = _xi;
            newData.KDash = _kdash;
            newData.G = _g;
            newData.Betha = _betha;
            newData.YAlpha = _yalpha;
            newData.K = _k;
            newData.B = _b;
            newData.LambdaZero = _lambdaZero;
            newData.LambdaOne = _lambdaOne;
            newData.LambdaTwo = _lambdaTwo;
            newData.BOne = _bOne;
            newData.BTwo = _bTwo;
            newData.P = _p;
            newData.Q = _q;
            return newData;
        }

        #endregion

        #region private_fileds
        private double _mass;
        private double _c_y;
        private double _p_s;
        private double _b_a;
        private double _max_number;
        private double _square;
        private double _n_max;
        private double _time;
        private double _l;
        private double _velocity;
        private double _height;
        private double _mu;
        private double _xi;
        private double _kdash;
        private double _g;
        private double _betha;
        private double _yalpha;
        private double _k;
        private double _b;
        private double _lambdaZero;
        private double _bOne;
        private double _bTwo;
        private double _lambdaOne;
        private double _lambdaTwo;
        private double _time1;
        private double _time2;
        private double _p;
        private double _q;
        #endregion


        #region Properties

        public double Mass { get { return _mass; } set { _mass = value; OnPropertyChanged("Mass"); } }
        /// <summary>
        /// коэффициент подъемной силы
        /// </summary>
        public double Cy { get { return _c_y; } set { _c_y = value; OnPropertyChanged("Cy"); } }
        /// <summary>
        /// Плотность воздуха
        /// </summary>
        public double Ps { get { return _p_s; } set { _p_s = value; OnPropertyChanged("Ps"); } }
        /// <summary>
        /// Средняя аэродимаческая хорда
        /// </summary>  
        public double Ba { get { return _b_a; } set { _b_a = value; OnPropertyChanged("Ba"); } }
        public double MaxNumber
        {
            get { return _max_number; }
            set
            {
                _max_number = value;
                Velocity = value * MAXCONVERT; OnPropertyChanged("Max_Number");
            }
        }
        public double Square { get { return _square; } set { _square = value; OnPropertyChanged("Square"); } }
        public double NMax { get { return _n_max; } set { _n_max = value; OnPropertyChanged("NMax"); } }  //Макс приращение прегрузки
        public double Time { get { return _time; } set { _time = value; OnPropertyChanged("Time"); } }
        /// <summary>
        /// Масштаб турбулентности
        /// </summary>
        public double L { get { return _l; } set { _l = value; OnPropertyChanged("L"); } }
        public double Velocity { get { return _velocity; } set { _velocity = value; OnPropertyChanged("Velocity"); } }
        public double Height { get { return _height; } set { _height = value; OnPropertyChanged("Height"); } }

        //stage 2 (Calculated)
        public double Mu { get { return _mu; } set { _mu = value; OnPropertyChanged("Mu"); } }
        public double Xi { get { return _xi; } set { _xi = value; OnPropertyChanged("Xi"); } }
        public double KDash { get { return _kdash; } set { _kdash = value; OnPropertyChanged("KDash"); } }

        //stage 3
        public double G { get { return _g; } set { _g = value; OnPropertyChanged("G"); } }
        public double Betha { get { return _betha; } set { _betha = value; OnPropertyChanged("Betha"); } }
        public double YAlpha { get { return _yalpha; } set { _yalpha = value; OnPropertyChanged("YAlpha"); } }
        public double K { get { return _k; } set { _k = value; OnPropertyChanged("K"); } }

        //Stage 4
        public double B { get { return _b; } set { _b = value; OnPropertyChanged("B"); } }

        //Stage 5
        public double LambdaZero { get { return _lambdaZero; } set { _lambdaZero = value; OnPropertyChanged("LambdaZero"); } }
        public double BOne { get { return _bOne; } set { _bOne = value; OnPropertyChanged("BOne"); } }
        public double BTwo { get { return _bTwo; } set { _bTwo = value; OnPropertyChanged("BTwo"); } }
        public double LambdaOne { get { return _lambdaOne; } set { _lambdaOne = value; OnPropertyChanged("LambdaOne"); } }
        public double LambdaTwo { get { return _lambdaTwo; } set { _lambdaTwo = value; OnPropertyChanged("LambdaTwo"); } }
        public double TimeOne { get { return _time1; } set { _time1 = value; OnPropertyChanged("TimeOne"); } }
        public double TimeTwo { get { return _time2; } set { _time2 = value; OnPropertyChanged("TimeTwo"); } }
        public double P { get { return _p; } set { _p = value; OnPropertyChanged("P"); } }
        public double Q { get { return _q; } set { _q = value; OnPropertyChanged("Q"); } }









        #endregion

        #region Mathematical Calculations

        public void CalcMu()
        {
            Mu = 2f * Mass / (Cy * Ps * Square * Ba);
        }

        public void CalcXi()
        {
            Xi = Ba / L;
        }


        public void CalcG()
        {
            G = Ps * Velocity * Velocity / 2f;
        }


        public void CalcYAlpha()
        {
            YAlpha = -Cy * G * Square / (Mass * Velocity);
        }

        public void CalcBetha()
        {
            Betha = Velocity / L;
        }

        public void CalcK()
        {
            CalcG();
            CalcBetha();
            CalcYAlpha();
            K = Math.Sqrt(1f - 1.5f * YAlpha / Betha) / (1f - YAlpha / Betha);
        }

        public void CalcB()
        {
            B = K * KDash * Cy * Ps * Velocity * Square / (2f * Mass * GFORCE);
        }

        public void CalcLambdaZero()
        {
            LambdaZero = Velocity * NU * 1000f / (Math.Sqrt(Ba * L) * K * KDash);

        }

        public void CalcLambdas()
        {
            LambdaOne = LambdaZero * Math.Exp(-NMax / (B * BOne));
            LambdaTwo = LambdaZero * Math.Exp(-NMax / (B * BTwo));
        }

        public void CalcPQ()
        {
            P = Math.Exp(-(TimeOne * LambdaOne + TimeTwo * LambdaTwo) * Time);
            Q = 1f - P;
        }

        public void CalcALL()
        {
            CalcMu(); CalcXi(); CalcG(); CalcYAlpha(); CalcBetha(); CalcK();
            CalcB(); CalcLambdaZero(); CalcLambdas(); CalcPQ();
        }
        #endregion


        public List<DataPoint> GetDepenedncyPointsPv(String propX, String propY, Double upperBorder)
        {
            List<DataPoint> list = new List<DataPoint>();
            ModelData copy = this.Clone();

            double oldValue = (double)this.GetType().GetProperty(propX).GetValue(this, null);
            double inc = oldValue / 100;
            this.GetType().GetProperty(propX).SetValue(this, 0);
            for (int i = 0; (double)this.GetType().GetProperty(propX).GetValue(this, null) < upperBorder && i< 400; i++)
            {
                CalcALL();
                list.Add(new DataPoint((double)this.GetType().GetProperty(propX).GetValue(this, null), (double)this.GetType().GetProperty(propY).GetValue(this, null)));
                this.GetType().GetProperty(propX).SetValue(this, (double)this.GetType().GetProperty(propX).GetValue(this, null) + inc);
            }
            this.GetType().GetProperty(propX).SetValue(this, oldValue);
            return list;
        }


    }
}
