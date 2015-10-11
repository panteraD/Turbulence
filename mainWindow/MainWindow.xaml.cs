﻿using System;
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
using System.Timers;

using System.Timers;
using System.Windows.Threading;


namespace mainWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Artist _drawingArtist;
        private ViewModel viewModel;
        private Timer _timer;
        private int offset = 0;
        private PlaneVisual plane =new  PlaneVisual();
        private static Random random =new Random();
        DispatcherTimer timer = new DispatcherTimer();
        public MainWindow()
        {
            viewModel = new ViewModel();
            _drawingArtist = new Artist(DrawingCanvasGrid);
            DataContext = viewModel;
            CompositionTarget.Rendering += CompositionTargetRendering;
            InitializeComponent();


        }


        private void CompositionTargetRendering(object sender, EventArgs e)
        {
            Plot1.InvalidatePlot(true);
        }

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //draw axes
            DrawingCanvasGrid.AddVisual(_drawingArtist.DrawLine(new Point(50,500),new Point(1500,500) )); //x Axis
            DrawingCanvasGrid.AddVisual(_drawingArtist.DrawLine(new Point(50, 0),new Point(50,1000) )); //y Axis
            offset = 0;
            
            timer.Tick += new EventHandler(Draw);
            timer.Interval = new TimeSpan(0, 0,0,0,700);
            timer.Start();


        }

        private void Draw(object sender, EventArgs e)
        {
            if (offset == 1000)
            {
                timer.Stop();
            }
            this.Dispatcher.BeginInvoke(new Action(delegate()
            {
                DrawingVisual visual = _drawingArtist.DrawPlane(new Point(offset, random.Next(-50, 50)+500));
                DrawingCanvasGrid.DeleteVisual(plane.Visual);
                plane.Visual = visual;
                offset += 50;
                DrawingCanvasGrid.AddVisual(visual);
            }));

        }
    }
}
