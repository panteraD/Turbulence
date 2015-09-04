using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Net;

namespace mainWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Data data;


        public MainWindow()
        {
            InitializeComponent();
            data = new Data(this);
        }

        /*
        public double Mass { get { return Double.Parse(ltbMass.TextBox); } set { Mass = value; } }
        public  double C_y { get { return Double.Parse(ltbC_y.TextBox); } set { C_y = value; } }     //коэффициент подъемной силы
        public  double P_s { get { return Double.Parse(ltbP_s.TextBox); } set { P_s = value; } }    //плотность воздуха
        public  double B_a { get { return Double.Parse(ltbB_a.TextBox); } set { B_a = value; } }    //Средняя аэродинамическая хорда
        public double Max_number { get { return Double.Parse(ltbMass.TextBox); } set { Max_number = value; } }
        public  double Wing_Square { get { return Double.Parse(ltbS.TextBox); } set { Wing_Square = value; } }
        public  double N_Max { get { return Double.Parse(ltbMaxStress.TextBox); } set { N_Max = value; } }  //Макс приращение прегрузки
        public double Time { get { return Double.Parse(ltbTime.TextBox); } set { Time = value; } }
        public  double L { get { return Double.Parse(ltbL.TextBox); } set { L = value; } }
        */

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ImageShow imageShow = new ImageShow();
            imageShow.Show();
        }

        static string DecodeEncodedNonAsciiCharacters(string value)
        {
            return Regex.Replace(
                value,
                @"\\u(?<Value>[a-zA-Z0-9]{4})",
                m => {
                    return ((char)int.Parse(m.Groups["Value"].Value, NumberStyles.HexNumber)).ToString();
                });
        }


        private void calcMu_Click(object sender, RoutedEventArgs e)
        {
            ltbMu.TextBox = data.countMu().ToString();
        }

        private void calcXi_Click(object sender, RoutedEventArgs e)
        {
            ltbXi.TextBox = data.coundXi().ToString();
        }

        private void calcK_Click(object sender, RoutedEventArgs e)
        {
            ltbK.TextBox = data.countK().ToString();
        }
    }
}
