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
            labelTop.Content = "KlasseNavn";
         
            stackAtt.Children.Add(labelAt1);
         
            stackMet.Children.Add(labelMe1);

            expAtt.Header = "Attributter";
            expAtt.Content = stackAtt;

            expMet.Header = "Metoder";
            expMet.Content = stackMet;

            stack.Children.Add(labelTop);
            stack.Children.Add(expAtt);
            stack.Children.Add(expMet);

            

            canvas1.Children.Add(stack);

            /* 
             TextBlock txt1 = new TextBlock();
             txt1.FontSize = 14;
             txt1.Text = "Hello World!"; 

             TextBlock txt2 = new TextBlock();
             txt1.FontSize = 14;
             txt1.Text = "Hello World2!";

             Expander exp1 = new Expander();

             Rectangle rec = new Rectangle(10,10);
             rec.Height = 100;
             rec.Width = 200;
            
            
            

             canvas1.Children.Add(rec);

             StackPanel stack = new StackPanel();
             stack.Children.Add(txt1);
             stack.Children.Add(txt2);
            
             canvas1.Children.Add(stack);*/
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
