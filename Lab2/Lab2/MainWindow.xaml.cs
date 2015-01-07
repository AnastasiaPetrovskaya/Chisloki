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
using OxyPlot;
using OxyPlot.Series;
using System.Threading;

namespace Lab2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnParse_Click(object sender, RoutedEventArgs e)
        {
            var ex = MathExpression.parseString(txtFunc.Text.Replace(" ", "").ToLower());
            double a = Convert.ToDouble(txtA.Text.Replace(" ", ""));
            double b = Convert.ToDouble(txtB.Text.Replace(" ", ""));
            //a = 0; b = 10;
            //int n = 10;
            int n = Convert.ToInt32(txtN.Text.Replace(" ", ""));
            double[] points = Chebyshev.GetPoints(a, b, n); // получение чебышевских узлов(тчк пстр Л)
            Interpolation inter = new Interpolation(ex, points); // создание многочлена Лагранжа
            PlotModel m = new PlotModel(); // новый график
            plot1.Model = m; 
            LineSeries ls_inter = new LineSeries();// график
            int stepcnt = 10000;
            double step = (b - a) / stepcnt;
            for (double x = a; x <= b; x += step )
            {
                ls_inter.Points.Add(new DataPoint(x, inter.Value(x)));
                
            }
            m.Series.Add(ls_inter);
            LineSeries ls_original = new LineSeries();
            ls_original.Color = OxyColor.FromRgb(255, 0, 0);
            ls_original.StrokeThickness = 1;
            for (double x = a; x <= b; x += step)
            {
                ls_original.Points.Add(new DataPoint(x, ex.getValue(x)));
            }
            m.Series.Add(ls_original);
            plot1.InvalidatePlot();
            m = new PlotModel();
            plot2.Model = m;
            LineSeries ls_diff = new LineSeries();
            for(double x = a; x <= b; x += step)
            {
                ls_diff.Points.Add(new DataPoint(x, Math.Abs(ex.getValue(x) - inter.Value(x))));
            }
            m.Series.Add(ls_diff);
            plot2.InvalidatePlot();
        }

       
    }
}
