using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;

namespace Lab2
{
    class DrawGraphics
    {
        public static void DrawG (OxyPlot.PlotModel m, List<double> points, List<double> values, OxyColor color )
        {

            LineSeries ls_four = new LineSeries();// график
            ls_four.StrokeThickness = 0.5;
            for (int i = 0; i < points.Count; i++)
            {
                ls_four.Points.Add(new DataPoint(points[i], values[i]));
            }
            m.Series.Add(ls_four);
            
        }
    }
}
