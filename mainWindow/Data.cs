using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mainWindow
{
    public class Data : INotifyPropertyChanged
    {
        private MainWindow window;
        static Double MAXCONVERT = 340.3f;

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

        public Data(MainWindow mainWindow)
        {
            window = mainWindow;
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
      


        #endregion

        #region Mathematical Calculations

        public void CountMu()
        {
            Mu = 2f * Mass / (Cy * Ps * Square * Ba);
        }

        public void CoundXi()
        {
            Xi = Ba / L;
        }

        
        public void CalcG()
        {
            G = Ps * Velocity * Velocity;
        }
 

        public void CalcYAlpha()
        {

            YAlpha = -Cy * G * Square / (Mass * Velocity);

        }

        public void CalcBetha()
        {


            Betha = Velocity / L;

        }

        public void CountK()
        {
            CalcG();
            CalcBetha();
            CalcYAlpha();
            K= Math.Sqrt(1f - 1.5f * YAlpha / Betha) / (1f - YAlpha / Betha);
        }
        #endregion
    }
}
