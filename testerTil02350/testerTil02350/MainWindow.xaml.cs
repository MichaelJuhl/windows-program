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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            statusBar1.ItemsSource = "Place Item on Canvas";

            /* Placere en Stack med indhold på canvas */

            StackPanel stack = new StackPanel();        
            StackPanel stackAtt = new StackPanel();
            StackPanel stackMet = new StackPanel();

            Label labelTop = new Label();
            Label labelAtt = new Label();
            Label labelMet = new Label();
            Label labelAt1 = new Label();
            Label labelMe1 = new Label();

            Expander expAtt = new Expander();
            Expander expMet = new Expander();

            labelAt1.Content = "AttribNavn";
            labelMe1.Content = "MetodeNavn";
            if (textBox1.Text == "Name")
            {
                MessageBox.Show("Please, fill out name.");
            }
            else
            {
                labelTop.Content = textBox1.Text;
                labelTop.FontWeight = FontWeights.Bold;

                stackAtt.Children.Add(labelAt1);

                stackMet.Children.Add(labelMe1);

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

                Canvas.SetTop(stack, 100);
                Canvas.SetLeft(stack, 100);

                canvas1.Children.Add(stack);
                
            }
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

        private void button3_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
