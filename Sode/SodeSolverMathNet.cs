using System;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.OdeSolvers;

namespace Sode
{
    public class SodeSolverMathNet
    {
        public double[,] SolveRK4(Vector<double> x0, double start, double end, int n, Func<double, Vector<double>, Vector<double>> func)
        {
            var answer = new double[n, 2];
            var res = RungeKutta.FourthOrder(x0, start, end, n, func);
            for (var i = 0; i < n; i++)
            {
                var temp = res[i].ToArray();
                answer[i, 0] = temp[0];
                answer[i, 1] = temp[1];
            }

            return answer;
        }
    }
}
