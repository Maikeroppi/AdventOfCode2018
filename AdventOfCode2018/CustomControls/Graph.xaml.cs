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

namespace AdventOfCode2018.CustomControls
{
    /// <summary>
    /// Interaction logic for Graph.xaml
    /// </summary>
    public partial class Graph : UserControl
    {
        public Graph()
        {
            InitializeComponent();
        }

        private int[] _values;
        public int[] Values
        {
            set
            {
                _values = value;
                Replot();
            }

            get
            {
                return _values;
            }
        }

        private int _spacing = 5;
        public int Spacing
        {
            set
            {
                _spacing = value;
            }

            get
            {
                return _spacing;
            }
        }

        void Replot()
        {
            _plot.Children.Clear();

            if (_values.Length == 0)
                return;

            int min = _values.Min();
            int max = _values.Max();

            int diff = max - min;
            double scaleFactorY = this.ActualHeight / diff;
            double scaleFactorX = this.ActualWidth / _values.Length;

            double offsetY = Math.Abs(min * scaleFactorY);

            PointCollection points = new PointCollection();
            for (int i = 0; i < _values.Length; ++i)
            {
                double yValue = _values[i] * scaleFactorY;
                Point point = new Point(i * scaleFactorX, offsetY + yValue);
            }

            Polyline graphLine = new Polyline();
            graphLine.StrokeThickness = 1;
            graphLine.Stroke = Brushes.Blue;
            graphLine.Points = points;

            _plot.Children.Add(graphLine);
        }
    }
}
