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
        private MainWindowModel _mainWindowModel;

        public MainWindow()
        {
            InitializeComponent();

            
            MainWindowViewModel mwvm= new MainWindowViewModel();
            //DataContext = mwvm;
            _mainWindowModel = mwvm.Data;


        }

        
        private void calcXiMu_Click(object sender, RoutedEventArgs e)
        {
            _mainWindowModel.CalcXi();
            _mainWindowModel.CalcMu();
        }

        private void calcK_Click(object sender, RoutedEventArgs e)
        {
            _mainWindowModel.CalcK();
        }


        private void CalcB_OnClick(object sender, RoutedEventArgs e)
        {
             _mainWindowModel.CalcB();
        }

        private void CalcLambdaZero_Click(object sender, RoutedEventArgs e)
        {
            _mainWindowModel.CalcLambdaZero();
        }

        private void CalcLambdas_OnClick(object sender, RoutedEventArgs e)
        {
            _mainWindowModel.CalcLambdas();   
        }


        private void CalcP_OnClick(object sender, RoutedEventArgs e)
        {
            _mainWindowModel.CalcP();
        }

        
    }
}
