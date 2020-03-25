using System;
using MathNet.Numerics.LinearAlgebra;
using Xunit;

namespace Sode.tests
{
    public class UnitTest1
    {
        private const double Eps = 0.01;

        [Fact]
        public void Test1Variant()
        {
            var rk4 = new SodeSolverMathNet();
            var x0 = Vector<double>.Build.DenseOfArray(new[] { 1.0, 1.0 });
            const int n = 11;
            var results = rk4.SolveRK4(x0, 0, 0.11, n, (t, z) =>
            {
                var a = z.ToArray();
                var x = a[0];
                var y = a[1];

                return Vector<double>.Build.Dense(new[]
                    {x * (1 - Math.Sqrt(x * x + y * y)) - y, y * (1 - Math.Sqrt(x * x + y * y)) + x});

            });

            var realResults = new[] { 0.8476, 1.058 };
            var compare = (Math.Abs(results[n - 1, 0] - realResults[0]) < Eps) && (Math.Abs(results[n - 1, 1] - realResults[1]) < Eps);

            Assert.True(compare);
        }

        [Fact]
        public void Test2Variant()
        {
            var rk4 = new SodeSolverMathNet();
            var x0 = Vector<double>.Build.DenseOfArray(new[] { 1.0, 3.0 });
            const int n = 11;
            var results = rk4.SolveRK4(x0, 0, 0.11, n, (t, z) =>
            {
                var a = z.ToArray();
                var x = a[0];
                var y = a[1];

                return Vector<double>.Build.Dense(new[]
                    {2 * (x - x * y), -(y - x * y)});

            });

            var realResults = new[] { 0.647, 2.9379 };
            var compare = (Math.Abs(results[n - 1, 0] - realResults[0]) < Eps) && (Math.Abs(results[n - 1, 1] - realResults[1]) < Eps);

            Assert.True(compare);
        }

        [Fact]
        public void Test5Variant()
        {
            var rk4 = new SodeSolverMathNet();
            var x0 = Vector<double>.Build.DenseOfArray(new[] { 2.0, 0.0 });
            const int n = 11;
            var results = rk4.SolveRK4(x0, 0, 0.11, n, (t, z) =>
            {
                var a = z.ToArray();
                var x = a[0];
                var y = a[1];

                return Vector<double>.Build.Dense(new[]
                    {y, (1 - x * x) * y - x});
            });

            var realResults = new[] { 1.9891, -0.1872 };
            var compare = (Math.Abs(results[n - 1, 0] - realResults[0]) < Eps) && (Math.Abs(results[n - 1, 1] - realResults[1]) < Eps);

            Assert.True(compare);
        }
    }
}
