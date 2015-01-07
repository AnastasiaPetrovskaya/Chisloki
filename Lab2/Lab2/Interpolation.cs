using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class Interpolation
    {
        int n;
        double[] points;
        double[] values;

        double Li(double x, int i)
        {
            double ans = 1;
            for(int j = 0; j <= n; j++)
            {
                if (i != j)
                    ans *= (x - points[j]) / (points[i] - points[j]);
            }
            return ans;
        }

        public double Value(double x)
        {
            double ans = 0;
            for (int i = 0; i <= n; i++)
                ans += values[i] * Li(x, i);
            return ans;
        }

        public Interpolation(MathExpression f, double[] points)
        {
            this.points = (double[])points.Clone();
            n = points.Length - 1;
            values = new double[n + 1];
            for (int i = 0; i <= n; i++)
                values[i] = f.getValue(points[i]);
        }
    }
}
