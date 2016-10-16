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

namespace CSc_4820_hmwk4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public List<Brush> ColorsList { get; set; }
        public Brush SelectedColor { get; set; }

        public int numOfShapes;


        private Point? _start = null;

        public MainWindow()
        {
            InitializeComponent();

            ColorsList = new List<Brush>()
            {
                Brushes.Red,
                Brushes.Orange,
                Brushes.Yellow,
                Brushes.Green,
                Brushes.Blue,
                Brushes.Purple,
                Brushes.Pink,
                Brushes.Brown,
                Brushes.White,
                Brushes.Black,
            };
            
            DataContext = this;
            numOfShapes = 1;
        }

        private void resetMenu_Click(object sender, RoutedEventArgs e)
        {
            drawCanvas.Children.Clear();
            numOfShapes = 0;
        }

        private void exitMenu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void keyDownMethod(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.X && Keyboard.Modifiers == ModifierKeys.Control)
            {
                drawCanvas.Children.Clear();
                numOfShapes = 0;
            }

            if(e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (_start == null) _start = Mouse.GetPosition((UIElement)sender);
            Path path1 = new Path();
            path1.Fill = (Brush)fillColorComboBox.SelectedValue;
            path1.Stroke = (Brush)strokeColorComboBox.SelectedValue;
            path1.StrokeThickness = Convert.ToDouble(strokeThickTextBox.Text);
            drawCanvas.Children.Add(path1);
            path1.Data = new GeometryGroup();
            var pg = path1.Data as GeometryGroup;
            pg.FillRule = FillRule.Nonzero;
            if(shapeComboBox.SelectedIndex == 0)
            {
                pg.Children.Add(new RectangleGeometry());
            }
            else
            {
                pg.Children.Add(new EllipseGeometry());
            }
            
            
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (_start == null) return;
            var p = Mouse.GetPosition((UIElement)sender);
            var path1 = drawCanvas.Children[numOfShapes] as Path;
            var gg = path1.Data as GeometryGroup;
            gg.FillRule = FillRule.Nonzero;
            
            if (shapeComboBox.SelectedIndex == 0)
            {
                var pg = gg.Children.Last() as RectangleGeometry;
                pg.Rect = new Rect(_start.Value, p);
            }
            else
            {
                var pg = gg.Children.Last() as EllipseGeometry;
                pg.Center = _start.Value;
                pg.RadiusX = Math.Abs(_start.Value.X - p.X);
                pg.RadiusY = Math.Abs(_start.Value.Y - p.Y);
            }

        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (_start == null) return;
            var p = Mouse.GetPosition((UIElement)sender);
            var path1 = drawCanvas.Children[numOfShapes] as Path;
            var gg = path1.Data as GeometryGroup;
            gg.FillRule = FillRule.Nonzero;

            if (shapeComboBox.SelectedIndex == 0)
            {
                var pg = gg.Children.Last() as RectangleGeometry;
                pg.Rect = new Rect(_start.Value, p);
            }
            else
            {
                var pg = gg.Children.Last() as EllipseGeometry;
                pg.Center = _start.Value;
                pg.RadiusX = Math.Abs(_start.Value.X - p.X);
                pg.RadiusY = Math.Abs(_start.Value.Y - p.Y);
            }

            _start = null;
            numOfShapes++;
            Console.WriteLine("UP!!");
        }

    }
}
