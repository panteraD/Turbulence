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
using System.ComponentModel;
using System.Net;

namespace mainWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Data _data;
        private ImageShow _imageShow = null;


        public MainWindow()
        {
            InitializeComponent();
            _data = new Data(this);
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
            this.DataContext = _data;
                 
        }

        /// <summary>
        /// Shows one instance of windows with Graph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (_imageShow == null)
            {
                _imageShow = new ImageShow();
                _imageShow.Closed += (a, b) => _imageShow = null;
                _imageShow.Show();
            }
            else
            {

                _imageShow.Show();
            }
        }

        private void calcMu_Click(object sender, RoutedEventArgs e)
        {
            _data.CountMu();
        }

        private void calcXi_Click(object sender, RoutedEventArgs e)
        {
            _data.CoundXi();
        }

        private void calcK_Click(object sender, RoutedEventArgs e)
        {
            _data.CountK();
        }


    }
}
