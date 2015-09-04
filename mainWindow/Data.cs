using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mainWindow
{
    public class Data
    {
        private MainWindow window;
        static Double MAXConst = 54354f;
        public Data(MainWindow mainWindow)
        {
            window = mainWindow;
        }
        /// <summary>
        /// Масса
        /// </summary>
        public double Mass { get { return  Double.Parse(window.ltbMass.TextBox); } set { Mass = value; } }
        public double C_y { get { return Double.Parse(window.ltbC_y.TextBox); } set { C_y = value; } }     //коэффициент подъемной силы
        public double P_s { get { return Double.Parse(window.ltbP_s.TextBox); } set { P_s = value; } }    //плотность воздуха
        public double B_a { get { return Double.Parse(window.ltbB_a.TextBox); } set { B_a = value; } }    //Средняя аэродинамическая хорда
        public double Max_number  { get { return  Double.Parse(window.ltbMass.TextBox); } set { Max_number = value; } }
        public double Wing_Square { get { return Double.Parse(window.ltbS.TextBox); } set { Wing_Square = value; } }
        public double N_Max { get { return Double.Parse(window.ltbMaxStress.TextBox); } set { N_Max = value; } }  //Макс приращение прегрузки
        public double Time { get { return Double.Parse(window.ltbTime.TextBox); } set {Time = value; } }
        public double L { get { return Double.Parse(window.ltbL.TextBox); } set { L = value; } }
        public double Velocity { get { return Max_number * MAXConst; } }
        //stage 2
        public double Mu { get; set; }
        public double Xi { get; set; }
        public double KDash { get; set; }
        //stage 3

        public double G  {
            get
            {
                return P_s * Velocity * Velocity;
            }
            set { G = value; }
        }

       
        public double YAlpha
        {
            get
            {
                return -C_y*G*Wing_Square/(Mass*Velocity);
            }
        }

        public double Betha
        {
            get
            {
                return Velocity / L;
            }
        }


        public double countMu()  {
            return Mu = 2f * Mass / (C_y*P_s*Wing_Square*B_a);
        }

        public double coundXi()
        {
            return Xi = B_a / L;
        }

        public double countK()
        {
            return Math.Sqrt(1f - 1.5f * YAlpha / Betha) / (1f - YAlpha / Betha);
        }
    }
}
