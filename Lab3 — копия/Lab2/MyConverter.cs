using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;

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
using System.Windows.Data;


namespace Lab2
{
    class MyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            // Возвращаем строку в формате 123.456.789 руб.
            //return ((double)value).ToString("#,###", culture) + " руб.";
            //if (Convert.value == true)
            
            bool tmp;
            tmp = false;
            string p = (string)parameter;
            if (p == "0")
            {
                if ((int)value == 0)
                {
                    tmp = true;
                    return tmp ? Visibility.Visible : Visibility.Collapsed;
                }
                else
                    return tmp ? Visibility.Visible : Visibility.Collapsed;
            }
            else //if (p == 1)
            {
                if ((int)value == 1)
                {
                    tmp = true;
                    return tmp ? Visibility.Visible : Visibility.Collapsed;
                }
                else
                    return tmp ? Visibility.Visible : Visibility.Collapsed;
            }
           

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }


    }
}

