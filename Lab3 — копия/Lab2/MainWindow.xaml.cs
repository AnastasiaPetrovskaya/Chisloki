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
using System.Globalization;
using System.Windows.Controls;
//using System.Windows.Data;

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
            ListOfMethods = new List<MethodOfSolve>();
            ListOfMethods.Add(new MethodOfSolve(PredKorMiln.FirstPKMiln));
            ListOfMethods.Add(new MethodOfSolve(PredKorMiln.SecondPKMiln));
            ListOfMethods.Add(new MethodOfSolve(PredKorMiln.ThirdPKMiln));
            ListOfMethods.Add(new MethodOfSolve(PredKorMiln.FourPKMiln));
            ListOfMethods.Add(new MethodOfSolve(Miln.FirstMiln));
            ListOfMethods.Add(new MethodOfSolve(Miln.SecondMiln));
            ListOfMethods.Add(new MethodOfSolve(Miln.ThirdMiln));
            ListOfMethods.Add(new MethodOfSolve(Miln.FourMiln));
            ListOfCheckB = new List<CheckBox>();
            ListOfCheckB.Add(PRFirstCB);
            ListOfCheckB.Add(PRSecondCB);
            ListOfCheckB.Add(PRThirdCB);
            ListOfCheckB.Add(PRFourthCB);
            ListOfCheckB.Add(MfirstCB);
            ListOfCheckB.Add(MsecondCB);
            ListOfCheckB.Add(MthirdCB);
            ListOfCheckB.Add(MfourthCB);
            ListForCompare = new List<Tuple<CheckBox, MethodOfSolve>>();
            Pair = new Tuple<CheckBox, MethodOfSolve>(PRFirstCB, PredKorMiln.FirstPKMiln);
            ListForCompare.Add(Pair);
            Pair = new Tuple<CheckBox, MethodOfSolve>(PRSecondCB, PredKorMiln.SecondPKMiln);
            ListForCompare.Add(Pair);
            Pair = new Tuple<CheckBox, MethodOfSolve>(PRThirdCB, PredKorMiln.ThirdPKMiln);
            ListForCompare.Add(Pair);
            Pair = new Tuple<CheckBox, MethodOfSolve>(PRFourthCB, PredKorMiln.FourPKMiln);
            ListForCompare.Add(Pair);
            Pair = new Tuple<CheckBox, MethodOfSolve>(MfirstCB, Miln.FirstMiln);
            ListForCompare.Add(Pair);
            Pair = new Tuple<CheckBox, MethodOfSolve>(MsecondCB, Miln.SecondMiln);
            ListForCompare.Add(Pair);
            Pair = new Tuple<CheckBox, MethodOfSolve>(MthirdCB, Miln.ThirdMiln);
            ListForCompare.Add(Pair);
            Pair = new Tuple<CheckBox, MethodOfSolve>(MfourthCB, Miln.FourMiln);
            ListForCompare.Add(Pair);
            ListForSolve = new List<Tuple<RadioButton, MethodOfSolve>>();
            PairRB = new Tuple<RadioButton, MethodOfSolve>(PRFirst, PredKorMiln.FirstPKMiln);
            ListForSolve.Add(PairRB);
            PairRB = new Tuple<RadioButton, MethodOfSolve>(PRSecond, PredKorMiln.SecondPKMiln);
            ListForSolve.Add(PairRB);
            PairRB = new Tuple<RadioButton, MethodOfSolve>(PRThird, PredKorMiln.ThirdPKMiln);
            ListForSolve.Add(PairRB);
            PairRB = new Tuple<RadioButton, MethodOfSolve>(PRFourth, PredKorMiln.FourPKMiln);
            ListForSolve.Add(PairRB);
            PairRB = new Tuple<RadioButton, MethodOfSolve>(Mfirst, Miln.FirstMiln);
            ListForSolve.Add(PairRB);
            PairRB = new Tuple<RadioButton, MethodOfSolve>(Msecond, Miln.SecondMiln);
            ListForSolve.Add(PairRB);
            PairRB = new Tuple<RadioButton, MethodOfSolve>(Mthird, Miln.ThirdMiln);
            ListForSolve.Add(PairRB);
            PairRB = new Tuple<RadioButton, MethodOfSolve>(Mfourth, Miln.FourMiln);
            ListForSolve.Add(PairRB);
        }

        delegate List<double> MethodOfSolve(double xn, double yn, MathExpression ex, double a, double b, out List<double> values, double h, 
            int ChoiceMethod);
        List<MethodOfSolve> ListOfMethods;
        List<CheckBox> ListOfCheckB;
        Tuple<CheckBox,MethodOfSolve> Pair;
        Tuple<RadioButton, MethodOfSolve> PairRB;
        List<Tuple<CheckBox, MethodOfSolve>> ListForCompare;
        List<Tuple<RadioButton, MethodOfSolve>> ListForSolve;
        //List<RadioButton> ListOfRB;

        private void btnParse_Click(object sender, RoutedEventArgs e)
        {
            var ex = MathExpression.parseString(txtFunc.Text.Replace(" ", "").ToLower());
            double a = Convert.ToDouble(txtA.Text.Replace(" ", ""));
            double b = Convert.ToDouble(txtB.Text.Replace(" ", ""));
            double xn = Convert.ToDouble(txtxO.Text.Replace(" ", ""));
            double yn = Convert.ToDouble(txtyO.Text.Replace(" ", ""));
            List<double> values = new List<double>() ;
            double h = 0.05;
            List<double> points = new List<double>();
            PlotModel pm = new PlotModel();
            plot1.Model = pm;
            Random n = new Random();
            if (cmbox.SelectedIndex ==0)
            {
                foreach (Tuple<RadioButton, MethodOfSolve> tmp in ListForSolve)
                {
                    if (tmp.Item1.IsChecked == true)
                    {
                        points = tmp.Item2(xn, yn, ex, a, b, out values, h, cmbboxMethod.SelectedIndex);
                    }
                }
                DrawGraphics.DrawG(pm, points, values, OxyColor.FromRgb((byte)n.Next(255), (byte)n.Next(255), (byte)n.Next(255)));
            }
            else if (cmbox.SelectedIndex ==1)
            {
                foreach (Tuple<CheckBox,MethodOfSolve> tmp  in ListForCompare )
                {
                    if (tmp.Item1.IsChecked == true)
                    {
                        points = tmp.Item2(xn, yn, ex, a, b, out values, h, cmbboxMethod.SelectedIndex);

                        DrawGraphics.DrawG(pm, points, values, OxyColor.FromRgb((byte)n.Next(255), (byte)n.Next(255), (byte)n.Next(255)));
                    }
                }
            }

            plot1.InvalidatePlot();
        }

        private void butComparePKM_Click(object sender, RoutedEventArgs e)
           //сравнение всего
        {
            foreach (CheckBox cb in ListOfCheckB)
                cb.IsChecked = true;
            //var ex = MathExpression.parseString(txtFunc.Text.Replace(" ", "").ToLower());
            //double a = Convert.ToDouble(txtA.Text.Replace(" ", ""));
            //double b = Convert.ToDouble(txtB.Text.Replace(" ", ""));
            //double xn = Convert.ToDouble(txtxO.Text.Replace(" ", ""));
            //double yn = Convert.ToDouble(txtyO.Text.Replace(" ", ""));
            //List<double> values = new List<double>() ;
            //double h = 0.05;
            //List<double> points = new List<double>();
            //PlotModel pm = new PlotModel();
            //plot1.Model = pm;
            //Random n = new Random();
            //foreach (MethodOfSolve m in ListOfMethods)
            //{
            //    points = m(xn, yn, ex, a, b, out values, h, cmbboxMethod.SelectedIndex);

            //    DrawGraphics.DrawG(pm, points, values, OxyColor.FromRgb((byte)n.Next(255), (byte)n.Next(255), (byte)n.Next(255)));
            //}
            //plot1.InvalidatePlot();            
        }

        private void cmbox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
            //недоступность кноопки сравнить все для нажатия
        {
            if (cmbox.SelectedIndex == 1)
                butComparePKM.IsEnabled = true;
            else
                butComparePKM.IsEnabled = false;
        }


 

    }
}
