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

        private void button_Click_Class(object sender, RoutedEventArgs e)
        {
            statusBar1.ItemsSource = "Place Item on Canvas";

            string[] attrib = new string[] {"et","to","tre" };
            

            string[] method = new string[3]{"first","second","third"};
          

            int x = 100;
            int y = 100;

            /* Placere en Stack med indhold på canvas */  

            Klasse kl = new Klasse(textBox1.Text,attrib,method,x,y);

            StackPanel stack = new StackPanel();        
            StackPanel stackAtt = new StackPanel();
            StackPanel stackMet = new StackPanel();

            Label labelTop = new Label();
            Label labelAtt = new Label();
            Label labelMet = new Label();
            /*Label labelAt1 = new Label();
            Label labelMe1 = new Label();*/

            Expander expAtt = new Expander();
            Expander expMet = new Expander();

            /*labelAt1.Content = "AttribNavn";
            labelMe1.Content = "MetodeNavn";*/
            
            if (textBox1.Text == "Name")
            {
                MessageBox.Show("Please, fill out name.");
            }
            else
            {
                

                
                
                /*labelTop.Content = textBox1.Text;*/

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

               /* for (int i=0 ; i < kl.Attrib.Length ; i++)
                {
                    labelAt1.Content = kl.Attrib[i];
                    stackAtt.Children.Add(labelAt1);
                }*/

                /*for (int i = 0; i < kl.Method.Length; i++)
                {
                    labelMe1.Content = kl.Method[i];
                    stackMet.Children.Add(labelMe1);
                }*/

                /*stackAtt.Children.Add(labelAt1);

                stackMet.Children.Add(labelMe1);*/

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

                Canvas.SetTop(stack, x);
                Canvas.SetLeft(stack, y);

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

        private void button_Click_Arrow(object sender, RoutedEventArgs e)
        {

        }

        private void button_Click_Note(object sender, RoutedEventArgs e)
        {

        }
    }
}
