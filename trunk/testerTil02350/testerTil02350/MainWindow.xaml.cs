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
    }
}
