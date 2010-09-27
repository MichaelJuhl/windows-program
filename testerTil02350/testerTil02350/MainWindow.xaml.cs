using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace testerTil02350
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



        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            statusBar1.ItemsSource = "";
        }

        private void expander1_Expanded(object sender, RoutedEventArgs e)
        {
            statusBar1.ItemsSource = "Fill out Properties";
        }

        private void expander2_Expanded(object sender, RoutedEventArgs e)
        {
            statusBar1.ItemsSource = "Fill out Properties";
        }

        private void button_Click_Arrow(object sender, RoutedEventArgs e)
        {

        }

        private void button_Click_Note(object sender, RoutedEventArgs e)
        {

        }





        // The selected element (i.e. the element being dragged.)
        private UIElement _draggedElement;

        // The original position of the element and the position where the (mouse-) drag started.
        private Point _originalElementPos, _dragStart;
        





        private void surface_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Get the mouse position from the event-arguments.
            Point point = e.GetPosition(canvas1);

            // Perform a hit-test for the above point relative to the canvas.
            HitTestResult result = VisualTreeHelper.HitTest(canvas1, point);

           
            
            // Check if we:
            //  - Pressed the left mouse button
            //  - If we pressed on top of an object
            //  - ...and if that object happens to NOT be the canvas.
            if (e.LeftButton == MouseButtonState.Pressed && result != null && result.VisualHit != canvas1)
            {
                // Get the element we "hit"...
                _draggedElement = (UIElement)result.VisualHit;

                // ...and register its original position
                _originalElementPos = new Point()
                {
                    X = Canvas.GetLeft(_draggedElement),
                    Y = Canvas.GetTop(_draggedElement)
                };

                // ...as well as the start of the drag
                _dragStart = point;
            }
           
            // If we pressed the right mouse button then...
            else if (e.RightButton == MouseButtonState.Pressed && radioButton1.IsChecked == true)
            {
/*
                // Create the object
                Rectangle obj = new Rectangle()
                {
                    Width = 100,
                    Height = 100,
                    Stroke = Brushes.Black,
                    StrokeThickness = 2,
                    Fill = Brushes.CornflowerBlue
                };

                // ...add it to the canvas
                canvas1.Children.Add(obj);

                // ...and position it where the click was registered.
                Canvas.SetLeft(obj, point.X);
                Canvas.SetTop(obj, point.Y);
                */
                string[] attrib = new string[] { "et", "to", "tre" };

                string[] method = new string[] { "first", "second", "third" };

                string name;
                name ="name";

                Klasse kl = new Klasse(name, attrib, method, point.X, point.Y);

                StackPanel stack = new StackPanel();
                StackPanel stackAtt = new StackPanel();
                StackPanel stackMet = new StackPanel();

                Label labelTop = new Label();
                Label labelAtt = new Label();
                Label labelMet = new Label();
                //Label labelAt1 = new Label();
                //Label labelMe1 = new Label();

                Expander expAtt = new Expander();
                Expander expMet = new Expander();

                //labelAt1.Content = "AttribNavn";
                //labelMe1.Content = "MetodeNavn";
            
                              
                    //labelTop.Content = textBox1.Text;

                    labelTop.Content = kl.Name;
                    labelTop.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                    labelTop.FontSize = 15;
                    labelTop.FontWeight = FontWeights.Bold;


                    foreach (string i in attrib)
                    {
                        Label labelAt1 = new Label();
                        labelAt1.Content = i;
                        stackAtt.Children.Add(labelAt1);
                    }

                    foreach (string i in method)
                    {
                        Label labelMe1 = new Label();
                        labelMe1.Content = i;
                        stackMet.Children.Add(labelMe1);
                    }

                expAtt.Header = "Attributter";
                expAtt.Content = stackAtt;

                expMet.Header = "Metoder";
                expMet.Content = stackMet;

                stack.Children.Add(labelTop);
                stack.Children.Add(expAtt);
                stack.Children.Add(expMet);

                LinearGradientBrush Gradient = new LinearGradientBrush();
                Gradient.StartPoint = new Point(0.5, 0);
                Gradient.EndPoint = new Point(0.5, 1);
                Gradient.GradientStops.Add(new GradientStop(Colors.Blue, 0.0));
                Gradient.GradientStops.Add(new GradientStop(Colors.LightBlue, 1.0));

                stack.Background = Gradient;

                canvas1.Children.Add(stack);
                
                Canvas.SetTop(stack, point.Y);
                Canvas.SetLeft(stack, point.X);

              

            }
        }

        private void surface_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _draggedElement = null;
        }

        private void surface_MouseLeave(object sender, MouseEventArgs e)
        {
            _draggedElement = null;
        }

        private void surface_MouseMove(object sender, MouseEventArgs e)
        {
            // If an object is current being dragged:
            if (_draggedElement != null)
            {
                // Get the mouse position relative to the canvas.
                Point point = e.GetPosition(canvas1);

                // Calculate the dragged objects new position.
                Point newpos = new Point()
                {
                    X = _originalElementPos.X + (point.X - _dragStart.X),
                    Y = _originalElementPos.Y + (point.Y - _dragStart.Y)
                };

                // Move the object to the new position.
                Canvas.SetLeft(_draggedElement, newpos.X);
                Canvas.SetTop(_draggedElement, newpos.Y);
            }
        }        
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    }
}
