using System;
using System.Windows;
using MathNet.Numerics.LinearAlgebra;
using Microsoft.Win32;

namespace Sode
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ChooseFileName(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Text documents (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 1
            };
            var filename = string.Empty;

            var result = dialog.ShowDialog();

            if (result == true)
            {
                filename = dialog.FileName;
            }

            FileName.Text = filename;
        }

        private void Calc(object sender, RoutedEventArgs e)
        {
            try
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

                Answer.Text = $@"x1 = {results[n - 1, 0]}{Environment.NewLine}x2 = {results[n - 1, 1]}";

            }
            catch (Exception ex)
            {
                Answer.Text = ex.Message;
            }
        }
    }
}
