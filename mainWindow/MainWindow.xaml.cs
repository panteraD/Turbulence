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
        ImageShow imageShow = new ImageShow();



        public MainWindow()
        {
            InitializeComponent();
            _data = new Data(this);
            this.DataContext = _data;
            
          
        }


        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
           
            imageShow.Show();
        }

        private void calcMu_Click(object sender, RoutedEventArgs e)
        {
            ltbMu.TextBox = _data.countMu().ToString();
        }

        private void calcXi_Click(object sender, RoutedEventArgs e)
        {
            ltbXi.TextBox = _data.coundXi().ToString();
        }

        private void calcK_Click(object sender, RoutedEventArgs e)
        {
            ltbK.TextBox = _data.countK().ToString();
        }


    }
}
