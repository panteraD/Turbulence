﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;

namespace mainWindow
{
    class Artist
    {
        private Canvas drawingCanvas;
        //private ImageSource _planeGreenImgaImageSource;
       // private ImageSource _planeYellowImageSource;
        //private ImageSource _planeRedImageSource;
        private BitmapImage _planeGreenImage;
        private BitmapImage _planeYellowImage;
        private BitmapImage _planeRedBitmapImage;




        public Artist(DrawingCanvas canvas)
        {
            drawingCanvas = canvas;

                _planeGreenImage = new BitmapImage(new Uri("pack://application:,,,/mainWindow;component/Images/planeGreen.png"));
        }
        /// <summary>
        /// Returns drawing visual instance of plane
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public DrawingVisual DrawPlane(Point position)
        {
            DrawingVisual visual = new DrawingVisual();
            using (DrawingContext dc = visual.RenderOpen())
            {

                dc.DrawImage(_planeGreenImage, new Rect(position.X,position.Y,_planeGreenImage.PixelWidth/4,_planeGreenImage.PixelHeight/4));
            }

            return visual;
        }

        public DrawingVisual DrawLine(Point firstPoint, Point secondPoint)
        {
            DrawingVisual vis = new DrawingVisual();

            using (DrawingContext dc = vis.RenderOpen())
            {
                Pen pen = new Pen(Brushes.Black, 2);

                dc.DrawLine(pen, firstPoint, secondPoint);

            }

            return vis;
        }

    }
}
