using System;
using System.Windows;
using System.Windows.Media;
using System.IO;
using System.Windows.Xps.Packaging;



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
            //bind dataGrids
            viewModel.DataGridMass = dataGridMass;
            viewModel.DataGridMass2 = dataGridMass2;
            viewModel.DataGridSpeed = dataGridSpeed;
            viewModel.DataGridSpeed2 = dataGridSpeed2;
            
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
