using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class Chebyshev
    {
        static double[] GetPoints(int n)
        {
            double[] ans = new double[n];
            for(int i = 0; i < n; i++)
            {
                ans[i] = Math.Cos((2.0 * i + 1.0) * Math.PI / (2.0 * n));
            }
            return ans;
        }

        static public double[] GetPoints(double a, double b, int n)
        {
            double[] ans = GetPoints(n);// n корней на отрезк от -1 д0 1
            for(int i = 0; i < n; i++)
                ans[i] = (a + b) / 2.0 + (b - a) * ans[i] / 2.0;
            return ans;
        }
    }
}
