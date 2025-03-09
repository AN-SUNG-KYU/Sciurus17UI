using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciurus17.MyLibrary.Math
{
    public static class NumericalAnalysis
    {
        public static double[] EulerMethod(double[] x, double dt, Func<double[],double[]> func)
        {
            double[] nextX = new double[x.Length];

            double[] xdot = func(x);
            for (int i = 0; i < x.Length; i++)
            {
                nextX[i] = x[i] + dt * xdot[i];
            }
            return nextX;
        }
    }
}
