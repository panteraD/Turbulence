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
using System.IO;
using System.IO.Packaging;
using System.Net;
using System.Reflection;
using System.Timers;

using System.Timers;
using System.Windows.Threading;
using System.Windows.Xps.Packaging;
using WPFPdfViewer;


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
           
            CompositionTarget.Rendering += CompositionTargetRendering;
            InitializeComponent();

            viewModel = new ViewModel();

            DataContext = viewModel;

            viewModel.DataGrid = dataGrid;
           
   


            string taskDocName = "Task.xps";
            string theoryDocName = "Theory.xps";

            XpsDocument taskXpsDocument= new XpsDocument(System.IO.Path.Combine(Environment.CurrentDirectory, taskDocName),FileAccess.Read);
            XpsDocument theoryXpsDocument = new XpsDocument(System.IO.Path.Combine(Environment.CurrentDirectory, theoryDocName), FileAccess.Read);

            TaskDocumentViewer.Document = taskXpsDocument.GetFixedDocumentSequence();
            TheoryDocumentViewer.Document = theoryXpsDocument.GetFixedDocumentSequence();



           




        }


        private void CompositionTargetRendering(object sender, EventArgs e)
        {
            Plot1.InvalidatePlot(true);
        }

       
    }
}
