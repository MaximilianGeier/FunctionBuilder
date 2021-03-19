using FunctionBuilder.Logic;
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

namespace FunctionBuilder.wpfGUI
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

        public void InputButton_Click(object sender, RoutedEventArgs e)
        {
            string equation = Rpn.StartRpn(InputBox.Text);
            OutputResult.Text = equation;
            Temp(equation);
        }

        private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawCanvas();
        }

        private void DrawCanvas()
        {
            CanvasDraw.Children.Clear();
            var xLine = new Line();
            var yLine = new Line();
            var xArrow = new Polygon();
            var yArrow = new Polygon();
            var width = CanvasDraw.ActualWidth;
            var height = CanvasDraw.ActualHeight;

            xLine.X1 = 0;
            xLine.Y1 = height / 2;
            xLine.X2 = width;
            xLine.Y2 = height / 2;
            xLine.Stroke = Brushes.Black;
            xLine.StrokeThickness = 3;

            yLine.X1 = width / 2;
            yLine.Y1 = 0;
            yLine.X2 = width / 2;
            yLine.Y2 = height;
            yLine.Stroke = Brushes.Black;
            yLine.StrokeThickness = 2;

            xArrow.Points.Add(new Point(width, height / 2));
            xArrow.Points.Add(new Point(width - 5, (height / 2) - 5));
            xArrow.Points.Add(new Point(width - 5, (height / 2) + 5));
            xArrow.Fill = Brushes.Black;

            yArrow.Points.Add(new Point(width / 2, 0));
            yArrow.Points.Add(new Point((width / 2) - 5, 5));
            yArrow.Points.Add(new Point((width / 2) + 5, 5));
            yArrow.Fill = Brushes.Black;

            CanvasDraw.Children.Add(xLine);
            CanvasDraw.Children.Add(xArrow);
            CanvasDraw.Children.Add(yLine);
            CanvasDraw.Children.Add(yArrow);
        }

        private void Temp(string equation)
        {
            var width = CanvasDraw.ActualWidth;
            var height = CanvasDraw.ActualHeight;

            double xMinDouble = Convert.ToInt32(xMin.Text);
            double yMinDouble = Convert.ToInt32(xMax.Text);
            var ListX = new List<string>();
            var ListY = new List<string>();
            string str = equation;
            Sheet.OutputNumbers(1, xMinDouble, yMinDouble, str, out ListX, out ListY);

            var function = new Polyline();
            function.Stroke = Brushes.Red;
            function.StrokeThickness = 2;
            var fPoint = new Point();
            for (int i = 0; i < ListX.Count; i++)
            {
                fPoint.X = (width / 2) + Convert.ToDouble(ListX[i]);
                fPoint.Y = (height / 2) - Convert.ToDouble(ListY[i]);
                function.Points.Add(fPoint);
            }
            CanvasDraw.Children.Add(function);
        }
    }
}
