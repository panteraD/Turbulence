using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot.Series;

namespace mainWindow
{
    using System.Collections.Generic;

    using OxyPlot;

    public class MainWindowGraph
    {
        /*
        public MainViewModel()
        {
            this.MyModel = new PlotModel { Title = "Example 1" };
            this.MyModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
        }

        public PlotModel MyModel { get; private set; }
        */

        public MainWindowGraph()
        {
            this.Title = "P from M";
            this.Model = new PlotModel();
            LineSeries lineSeries = new LineSeries();
            lineSeries.ItemsSource = Points;
            this.Model.Series.Add(lineSeries);
           
            this.Points = new List<DataPoint>
                              {
                                  new DataPoint(0, 4),
                                  new DataPoint(10, 13),
                                  new DataPoint(20, 15),
                                  new DataPoint(30, 16),
                                  new DataPoint(40, 12),
                                  new DataPoint(50, 12)
                              };
        }

        public string Title { get; set; }

        public PlotModel Model { get; set; }


        

        public IList<DataPoint> Points { get; set; }
    }
}
