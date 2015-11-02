using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace mainWindow
{
    public class DrawingCanvas : Canvas
    {

        #region Protected methods


        protected override int VisualChildrenCount
        {
            get { return visuals.Count; }
        }


        protected override Visual GetVisualChild(int index)
        {
            return visuals[index];
        }

        #endregion

        #region Public methods

        public void AddVisual(Visual visual)
        {

            visuals.Add(visual);
            base.AddVisualChild(visual);

        }


        public DrawingVisual GetVisual(Point point)
        {
            HitTestResult hitResult = VisualTreeHelper.HitTest(this, point);
            if (hitResult == null) return null;
            else return hitResult.VisualHit as DrawingVisual;
        }


        public void DeleteVisual(Visual visual)
        {
            foreach (Visual v in visuals)
            {
                if (v == visual)
                {
                    visuals.Remove(visual);
                    base.RemoveVisualChild(visual);
                    base.RemoveLogicalChild(visual);
                    break;
                }
            }
        }

        /// <summary>
        /// Отчистка графика от объектов
        /// </summary>
        public void DeleteAllVisuals()
        {

            int count = visuals.Count;
            if (count == 0) return;

            for (int i = 0; i < count; i++)
            {
                Visual visual = visuals[0];
                visuals.Remove(visual);
                base.RemoveVisualChild(visual);
                base.RemoveLogicalChild(visual);
            }
        }

        #endregion



        #region Private fields

        /// <summary>
        /// Коллекция визуальных объектов
        /// </summary>
        private List<Visual> visuals = new List<Visual>();





        #endregion
    }
}
