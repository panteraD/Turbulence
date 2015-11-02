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
        private ViewModel viewModel;
        public MainWindow()
        {
            viewModel = new ViewModel();
            DataContext = viewModel;
            CompositionTarget.Rendering += CompositionTargetRendering;
            InitializeComponent();

        
        }


        private void CompositionTargetRendering(object sender, EventArgs e)
        {
            Plot1.InvalidatePlot(true);
        }
    }
}
