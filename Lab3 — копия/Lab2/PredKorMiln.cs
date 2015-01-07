using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class PredKorMiln
    {

        static public List<double> FourPKMiln(double xn, double yn, MathExpression pry, double a, double b, out List<double> values, double h,
            int methodChoice)
        {
            List<double> pryv = new List<double>();
            List<double> points = new List<double>();
            values = new List<double>();
            points.Add(xn);
            values.Add(yn);
            pryv.Add(pry.getValue(xn,yn));
            double xi;
            double yi;
            double k1;
            double k2;
            double k3;
            double k4;
            double eps;
            double pryprv;
            double yipr;
            if (a < xn - 4 * h)
            {
                if (methodChoice == 1)
                {
                    h = h * (-1);
                    for (int i = 1; i <= 3; i++) // Рунге-Кутта
                    {
                        k1 = h * pry.getValue(points.ElementAt(points.Count - 1), values.ElementAt(values.Count - 1));
                        k2 = h * pry.getValue(points.ElementAt(points.Count - 1) + h / 2, values.ElementAt(values.Count - 1) + k1 / 2);
                        k3 = h * pry.getValue(points.ElementAt(points.Count - 1) + h / 2, values.ElementAt(values.Count - 1) + k2 / 2);
                        k4 = h * pry.getValue(points.ElementAt(points.Count - 1) + h, values.ElementAt(values.Count - 1) + k3);
                        yi = values.ElementAt(values.Count - 1) + (k1 + 2 * k2 + 2 * k3 + k4) / 6;
                        xi = xn + i * h;
                        points.Add(xi);
                        values.Add(yi);
                        pryv.Add(pry.getValue(xi, yi));
                    }
                    h = h * (-1);
                }
                else
                {
                    for (int i = 1; i <= 3; i++) // Эйлер
                    {
                        yi = values.ElementAt(values.Count - 1) - h * pry.getValue(points.ElementAt(points.Count - 1), values.ElementAt(values.Count - 1));
                        xi = xn - i * h;
                        points.Add(xi);
                        values.Add(yi);
                        pryv.Add(pry.getValue(xi, yi));
                    }

                    // получены первые 4 точки
                    xi = xn - 4 * h;

                    while (xi >= a)
                    {
                        yipr = values.ElementAt(values.Count - 4) - 4 * h * (2 * pryv.ElementAt(pryv.Count - 3) - pryv.ElementAt(pryv.Count - 2) + 2 * pryv.ElementAt(pryv.Count - 1)) / 3;
                        pryprv = pry.getValue(xi, yipr);
                        yi = values.ElementAt(values.Count - 2) - h * (pryv.ElementAt(pryv.Count - 1) + 4 * pryv.ElementAt(pryv.Count - 1) + pryprv) / 3;
                        //eps = Math.Abs(yi - yipr) / 29;
                        //if (eps > 0.1) // если точность нас не устраивает
                        //{
                        //    xi = xi - h + h*0.95;
                        //    h = h *0.95;
                        //}
                        //else // если точность достигнута
                        //{
                        points.Add(xi);
                        values.Add(yi);
                        pryv.Add(pry.getValue(xi, yi));

                        xi = xi - h;
                        //}

                    }
                }
            }

            points.Reverse();
            values.Reverse();
            pryv.Reverse();

            if (xn + 4 * h < b)
            {
                if (methodChoice == 1)
                {
                    for (int i = 1; i <= 3; i++) // Рунге-Кутта
                    {
                        k1 = h * pry.getValue(points.ElementAt(points.Count - 1), values.ElementAt(values.Count - 1));
                        k2 = h * pry.getValue(points.ElementAt(points.Count - 1) + h / 2, values.ElementAt(values.Count - 1) + k1 / 2);
                        k3 = h * pry.getValue(points.ElementAt(points.Count - 1) + h / 2, values.ElementAt(values.Count - 1) + k2 / 2);
                        k4 = h * pry.getValue(points.ElementAt(points.Count - 1) + h, values.ElementAt(values.Count - 1) + k3);
                        yi = values.ElementAt(values.Count - 1) + (k1 + 2 * k2 + 2 * k3 + k4) / 6;
                        xi = xn + i * h;
                        points.Add(xi);
                        values.Add(yi);
                        pryv.Add(pry.getValue(xi, yi));
                    }
                }
                else
                {
                    for (int i = 1; i <= 3; i++) // Эйлер
                    {
                        yi = values.ElementAt(values.Count - 1) + h * pry.getValue(points.ElementAt(points.Count - 1), values.ElementAt(values.Count - 1));
                        xi = xn + i * h;
                        points.Add(xi);
                        values.Add(yi);
                        pryv.Add(pry.getValue(xi, yi));
                    }
                }
                // получены первые 4 точки
                xi = xn + 4 * h;
                
                while (xi <= b)
                {
                    yipr = values.ElementAt(values.Count - 4) + 4 * h * (2 * pryv.ElementAt(pryv.Count - 3) - pryv.ElementAt(pryv.Count - 2) + 2 * pryv.ElementAt(pryv.Count - 1)) / 3;
                    pryprv = pry.getValue(xi, yipr);
                    yi = values.ElementAt(values.Count - 2) + h * (pryv.ElementAt(pryv.Count - 1) + 4 * pryv.ElementAt(pryv.Count - 1) + pryprv) / 3;
                    points.Add(xi);
                    values.Add(yi);
                    pryv.Add(pry.getValue(xi, yi));
                    xi = xi + h;
                }
            }
            return points;
        }


        // 1го порядка
        static public List<double> FirstPKMiln(double xn, double yn, MathExpression pry, double a, double b, out List<double> values, double h, 
            int methodChoice)
        {
            List<double> pryv = new List<double>();
            List<double> points = new List<double>();
            values = new List<double>();
            points.Add(xn);
            values.Add(yn);
            pryv.Add(pry.getValue(xn, yn));
            double xi;
            double yi;
            double k1;
            double k2;
            double k3;
            double k4;
            double eps;
            double pryprv;
            double yipr;
            if (a < xn -  h)//если есть возможность идти влево
            {
                if (methodChoice == 0)
                {
                    yi = yn - h * pry.getValue(xn, yn);
                    xi = xn - h;
                    points.Add(xi);
                    values.Add(yi);
                    pryv.Add(pry.getValue(xi, yi));
                }
                else // если Рунге-Кутта
                {
                    h = h * (-1);
                    k1 = h * pry.getValue(points.ElementAt(points.Count - 1), values.ElementAt(values.Count - 1));
                    k2 = h * pry.getValue(points.ElementAt(points.Count - 1) + h / 2, values.ElementAt(values.Count - 1) + k1 / 2);
                    k3 = h * pry.getValue(points.ElementAt(points.Count - 1) + h / 2, values.ElementAt(values.Count - 1) + k2 / 2);
                    k4 = h * pry.getValue(points.ElementAt(points.Count - 1) + h, values.ElementAt(values.Count - 1) + k3);
                    yi = values.ElementAt(values.Count - 1) + (k1 + 2 * k2 + 2 * k3 + k4) / 6;
                    xi = xn + h;
                    points.Add(xi);
                    values.Add(yi);
                    pryv.Add(pry.getValue(xi, yi));
                    h = h * (-1);
                }

                // получены первые 1 точки
                xi = xn - 2 * h;

                while (xi >= a)
                {
                    yipr = values.ElementAt(values.Count - 1) + h * pryv.ElementAt(pryv.Count - 1);
                    pryprv = pry.getValue(xi, yipr);
                    yi = values.ElementAt(values.Count - 2) - h * (pryv.ElementAt(pryv.Count - 1) + 4 * pryv.ElementAt(pryv.Count - 1) + pryprv) / 3;
                    points.Add(xi);
                    values.Add(yi);
                    pryv.Add(pry.getValue(xi, yi));
                    xi = xi - h;
                }
            }

            points.Reverse();
            values.Reverse();
            pryv.Reverse();

            if (xn + h < b)
            {
                if (methodChoice == 1)
                {
                        k1 = h * pry.getValue(points.ElementAt(points.Count - 1), values.ElementAt(values.Count - 1));
                        k2 = h * pry.getValue(points.ElementAt(points.Count - 1) + h / 2, values.ElementAt(values.Count - 1) + k1 / 2);
                        k3 = h * pry.getValue(points.ElementAt(points.Count - 1) + h / 2, values.ElementAt(values.Count - 1) + k2 / 2);
                        k4 = h * pry.getValue(points.ElementAt(points.Count - 1) + h, values.ElementAt(values.Count - 1) + k3);
                        yi = values.ElementAt(values.Count - 1) + (k1 + 2 * k2 + 2 * k3 + k4) / 6;
                        xi = xn +  h;
                        points.Add(xi);
                        values.Add(yi);
                        pryv.Add(pry.getValue(xi, yi));
                    
                }
                else
                {
                   
                        yi = values.ElementAt(values.Count - 1) + h * pry.getValue(points.ElementAt(points.Count - 1), values.ElementAt(values.Count - 1));
                        xi = xn +  h;
                        points.Add(xi);
                        values.Add(yi);
                        pryv.Add(pry.getValue(xi, yi));
                    
                }
                // получены первые 4 точки
                xi = xn + 2 * h;

                while (xi <= b)
                {
                    yipr = values.ElementAt(values.Count - 1) +  h * pryv.ElementAt(pryv.Count - 1) ;
                    pryprv = pry.getValue(xi, yipr);
                    yi = values.ElementAt(values.Count - 2) + h * (pryv.ElementAt(pryv.Count - 1) + 4 * pryv.ElementAt(pryv.Count - 1) + pryprv) / 3;
                    points.Add(xi);
                    values.Add(yi);
                    pryv.Add(pry.getValue(xi, yi));
                    xi = xi + h;
                }
            }
            return points;

        }

        // 2го порядка
        static public List<double> SecondPKMiln(double xn, double yn, MathExpression pry, double a, double b, out List<double> values, double h,
            int methodChoice)
        {
            List<double> pryv = new List<double>();
            List<double> points = new List<double>();
            values = new List<double>();
            points.Add(xn);
            values.Add(yn);
            pryv.Add(pry.getValue(xn, yn));
            double xi;
            double yi;
            double k1;
            double k2;
            double k3;
            double k4;
            double eps;
            double pryprv;
            double yipr;

            if (a < xn - 2 * h)
            {
                if (methodChoice == 1)
                {
                    h = h * (-1);
                    for (int i = 1; i <= 1; i++) // Рунге-Кутта
                    {
                        k1 = h * pry.getValue(points.ElementAt(points.Count - 1), values.ElementAt(values.Count - 1));
                        k2 = h * pry.getValue(points.ElementAt(points.Count - 1) + h / 2, values.ElementAt(values.Count - 1) + k1 / 2);
                        k3 = h * pry.getValue(points.ElementAt(points.Count - 1) + h / 2, values.ElementAt(values.Count - 1) + k2 / 2);
                        k4 = h * pry.getValue(points.ElementAt(points.Count - 1) + h, values.ElementAt(values.Count - 1) + k3);
                        yi = values.ElementAt(values.Count - 1) + (k1 + 2 * k2 + 2 * k3 + k4) / 6;
                        xi = xn + i * h;
                        points.Add(xi);
                        values.Add(yi);
                        pryv.Add(pry.getValue(xi, yi));
                    }
                    h = h * (-1);
                }
                else
                {
                    for (int i = 1; i <= 2; i++) // Эйлер
                    {
                        yi = values.ElementAt(values.Count - 1) - h * pry.getValue(points.ElementAt(points.Count - 1), values.ElementAt(values.Count - 1));
                        xi = xn - i * h;
                        points.Add(xi);
                        values.Add(yi);
                        pryv.Add(pry.getValue(xi, yi));
                    }
                }

            }
                // получены первые 4 точки
                xi = xn - 2 * h;
            

            while (xi >=a)
            {
                yipr = values.ElementAt(values.Count - 2) - h * 2 * pryv.ElementAt(values.Count - 1);
                pryprv = pry.getValue(xi, yipr);
                yi = values.ElementAt(values.Count - 2) - h * (pryv.ElementAt(pryv.Count - 1) + 4 * pryv.ElementAt(pryv.Count - 1) + pryprv) / 3;
                eps = Math.Abs(yi - yipr) / 29;
                if (eps > 0.1) // если точность нас не устраивает
                {
                    xi = xi + h - h * 0.95;
                    h = h * 0.95;
                }
                else // если точность достигнута
                {
                    points.Add(xi);
                    values.Add(yi);
                    pryv.Add(pry.getValue(xi, yi));
                    xi = xi - h;
                }

            }

            points.Reverse();
            values.Reverse();
            pryv.Reverse();

            if (xn + 2 * h < b)
            {
                if (methodChoice == 1)
                {
                    for (int i = 1; i <= 1; i++) // Рунге-Кутта
                    {
                        k1 = h * pry.getValue(points.ElementAt(points.Count - 1), values.ElementAt(values.Count - 1));
                        k2 = h * pry.getValue(points.ElementAt(points.Count - 1) + h / 2, values.ElementAt(values.Count - 1) + k1 / 2);
                        k3 = h * pry.getValue(points.ElementAt(points.Count - 1) + h / 2, values.ElementAt(values.Count - 1) + k2 / 2);
                        k4 = h * pry.getValue(points.ElementAt(points.Count - 1) + h, values.ElementAt(values.Count - 1) + k3);
                        yi = values.ElementAt(values.Count - 1) + (k1 + 2 * k2 + 2 * k3 + k4) / 6;
                        xi = xn + i * h;
                        points.Add(xi);
                        values.Add(yi);
                        pryv.Add(pry.getValue(xi, yi));
                    }
                }
                else
                {
                    for (int i = 1; i <= 1; i++) // Эйлер
                    {
                        yi = values.ElementAt(values.Count - 1) + h * pry.getValue(points.ElementAt(points.Count - 1), values.ElementAt(values.Count - 1));
                        xi = xn + i * h;
                        points.Add(xi);
                        values.Add(yi);
                        pryv.Add(pry.getValue(xi, yi));
                    }
                }
                // получены первые 4 точки
                xi = xn +  2*h;

                while (xi <= b)
                {
                    yipr = values.ElementAt(values.Count - 2) + h * 2 * pryv.ElementAt(values.Count - 1);
                    pryprv = pry.getValue(xi, yipr);
                    yi = values.ElementAt(values.Count - 2) + h * (pryv.ElementAt(pryv.Count - 1) + 4 * pryv.ElementAt(pryv.Count - 1) + pryprv) / 3;
                    points.Add(xi);
                    values.Add(yi);
                    pryv.Add(pry.getValue(xi, yi));
                    xi = xi + h;
                }
            }
            
            return points;

        }

        // 3го порядка
        static public List<double> ThirdPKMiln(double xn, double yn, MathExpression pry, double a, double b, out List<double> values, double h,
            int methodChoice )
        {
            List<double> pryv = new List<double>();
            List<double> points = new List<double>();
            values = new List<double>();
            points.Add(xn);
            values.Add(yn);
            pryv.Add(pry.getValue(xn, yn));
            double xi;
            double yi;
            double k1;
            double k2;
            double k3;
            double k4;
            double eps;
            double pryprv;
            double yipr;
            if (a < xn - 3 * h)
            {
                if (methodChoice == 1)
                {
                    h = h * (-1);
                    for (int i = 1; i <= 2; i++) // Рунге-Кутта
                    {
                        k1 = h * pry.getValue(points.ElementAt(points.Count - 1), values.ElementAt(values.Count - 1));
                        k2 = h * pry.getValue(points.ElementAt(points.Count - 1) + h / 2, values.ElementAt(values.Count - 1) + k1 / 2);
                        k3 = h * pry.getValue(points.ElementAt(points.Count - 1) + h / 2, values.ElementAt(values.Count - 1) + k2 / 2);
                        k4 = h * pry.getValue(points.ElementAt(points.Count - 1) + h, values.ElementAt(values.Count - 1) + k3);
                        yi = values.ElementAt(values.Count - 1) + (k1 + 2 * k2 + 2 * k3 + k4) / 6;
                        xi = xn + i * h;
                        points.Add(xi);
                        values.Add(yi);
                        pryv.Add(pry.getValue(xi, yi));
                    }
                    h = h * (-1);
                }
                else
                {
                    for (int i = 1; i <= 2; i++) // Эйлер
                    {
                        yi = values.ElementAt(values.Count - 1) - h * pry.getValue(points.ElementAt(points.Count - 1), values.ElementAt(values.Count - 1));
                        xi = xn - i * h;
                        points.Add(xi);
                        values.Add(yi);
                        pryv.Add(pry.getValue(xi, yi));
                    }

                }
                // получены первые 3 точки
                xi = xn - 3 * h;

                while (xi >= a)
                {
                    yipr = values.ElementAt(values.Count - 3) - 3 * h * (-4 * pryv.ElementAt(pryv.Count - 3) + 9 * pryv.ElementAt(pryv.Count - 2) - 3 * pryv.ElementAt(pryv.Count - 1)) / 2;
                    pryprv = pry.getValue(xi, yipr);
                    yi = values.ElementAt(values.Count - 2) - h * (pryv.ElementAt(pryv.Count - 1) + 4 * pryv.ElementAt(pryv.Count - 1) + pryprv) / 3;
                    eps = Math.Abs(yi - yipr) / 29;
                    if (eps > 0.1) // если точность нас не устраивает
                    {
                        xi = xi + h - h * 0.95;
                        h = h * 0.95;
                    }
                    else // если точность достигнута
                    {
                        points.Add(xi);
                        values.Add(yi);
                        pryv.Add(pry.getValue(xi, yi));
                        xi = xi - h;
                    }

                }
            }

            points.Reverse();
            values.Reverse();
            pryv.Reverse();

            if (xn + 3 * h < b)
            {
                if (methodChoice == 1)
                {
                    for (int i = 1; i <= 2; i++) // Рунге-Кутта
                    {
                        k1 = h * pry.getValue(points.ElementAt(points.Count - 1), values.ElementAt(values.Count - 1));
                        k2 = h * pry.getValue(points.ElementAt(points.Count - 1) + h / 2, values.ElementAt(values.Count - 1) + k1 / 2);
                        k3 = h * pry.getValue(points.ElementAt(points.Count - 1) + h / 2, values.ElementAt(values.Count - 1) + k2 / 2);
                        k4 = h * pry.getValue(points.ElementAt(points.Count - 1) + h, values.ElementAt(values.Count - 1) + k3);
                        yi = values.ElementAt(values.Count - 1) + (k1 + 2 * k2 + 2 * k3 + k4) / 6;
                        xi = xn + i * h;
                        points.Add(xi);
                        values.Add(yi);
                        pryv.Add(pry.getValue(xi, yi));
                    }
                }
                else
                {
                    for (int i = 1; i <= 2; i++) // Эйлер
                    {
                        yi = values.ElementAt(values.Count - 1) + h * pry.getValue(points.ElementAt(points.Count - 1), values.ElementAt(values.Count - 1));
                        xi = xn + i * h;
                        points.Add(xi);
                        values.Add(yi);
                        pryv.Add(pry.getValue(xi, yi));
                    }
                }
                // получены первые 3 точки
                xi = xn + 3 * h;

                while (xi <= b)
                {
                    //yipr = values.ElementAt(values.Count - 3) + 3 * h * (-3 * pryv.ElementAt(pryv.Count - 3) + 8 * pryv.ElementAt(pryv.Count - 2) - 2 * pryv.ElementAt(pryv.Count - 1)) / 4;
                    //yipr = values.ElementAt(values.Count - 3) + 3 * h * (9 * pryv.ElementAt(pryv.Count - 3) +  3 * pryv.ElementAt(pryv.Count - 1)) / 4;
                    //послyipr = values.ElementAt(values.Count - 3) + 3 * h * (-4 * pryv.ElementAt(pryv.Count - 3) + 9* pryv.ElementAt(pryv.Count - 2) - 3 * pryv.ElementAt(pryv.Count - 1)) / 2;
                    yipr = values.ElementAt(values.Count - 3) + 3 * h * ( pryv.ElementAt(pryv.Count - 3) + 3 * pryv.ElementAt(pryv.Count - 1)) / 4;
                    pryprv = pry.getValue(xi, yipr);
                    yi = values.ElementAt(values.Count - 2) + h * (pryv.ElementAt(pryv.Count - 1) + 4 * pryv.ElementAt(pryv.Count - 1) + pryprv) / 3;
                    points.Add(xi);
                    values.Add(yi);
                    pryv.Add(pry.getValue(xi, yi));
                    xi = xi + h;
                }
            }

            
            
            
            
            return points;
        }


    }
}
