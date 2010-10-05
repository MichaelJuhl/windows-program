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

            this.PreviewMouseRightButtonDown += Window1_PreviewMouseRightButtonDown;
            this.btnOnlyShowOffsetIndicators.Checked += btnOnlyShowOffsetIndicators_Checked;
            this.btnOnlyShowOffsetIndicators.Unchecked += btnOnlyShowOffsetIndicators_Unchecked;

            // Add the blocks which display their positions within the Canvas.
           /* foreach (string key in new string[] { 
													"buttonTopLeft", 
													"buttonTopRight", 
													"buttonBottomRight", 
													"buttonBottomLeft", 
													"buttonAll", 
													"buttonNone" 
												})
            {
                Button button = this.FindResource(key) as Button;
                this.canvas1.Children.Add(button);
            }*/

            this.ResetZOrder();	

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
  
        /*    if (e.LeftButton == MouseButtonState.Pressed && radioButton4.IsChecked == true)
            {
                ?????
            }*/

            //Check if left mousebutton is pressed + radiobutton (class) is pressed
            if (e.LeftButton == MouseButtonState.Pressed && radioButton1.IsChecked == true)
            {

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

                Expander expAtt = new Expander();
                Expander expMet = new Expander();

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

                expAtt.IsExpanded = true;
                expMet.IsExpanded = true;

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

     
    
    
        
        private UIElement elementForContextMenu;

        void OnMenuItemClick(object sender, RoutedEventArgs e)
        {
            if (this.elementForContextMenu == null)
                return;

            if (e.Source == this.menuItemBringToFront ||
                e.Source == this.menuItemSendToBack)
            {
                bool bringToFront = e.Source == this.menuItemBringToFront;

                if (bringToFront)      
                    this.canvas1.BringToFront(this.elementForContextMenu);
                else
                    this.canvas1.SendToBack(this.elementForContextMenu);
            }
            else
            {
                bool canBeDragged = WPF.JoshSmith.Controls.DragCanvas.GetCanBeDragged(this.elementForContextMenu);
                WPF.JoshSmith.Controls.DragCanvas.SetCanBeDragged(this.elementForContextMenu, !canBeDragged);
                (e.Source as MenuItem).IsChecked = !canBeDragged;
            }
        }
        
        void OnContextMenuOpened(object sender, RoutedEventArgs e)
        {
            if (this.elementForContextMenu != null)
                this.menuItemCanBeDragged.IsChecked = WPF.JoshSmith.Controls.DragCanvas.GetCanBeDragged(this.elementForContextMenu);
        }

        private void ResetZOrder()
        {
            // Set the z-index of every visible child in the Canvas.
            int index = 0;
            for (int i = 0; i < this.canvas1.Children.Count; ++i)
                if (this.canvas1.Children[i].Visibility == Visibility.Visible)
                    Canvas.SetZIndex(this.canvas1.Children[i], index++);
        }

        void Window1_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            // If the user right-clicks while dragging an element, assume that they want 
            // to manipulate the z-index of the element being dragged (even if it is  
            // behind another element at the time).
            if (this.canvas1.ElementBeingDragged != null)
                this.elementForContextMenu = this.canvas1.ElementBeingDragged;
            else
                this.elementForContextMenu =
                    this.canvas1.FindCanvasChild(e.Source as DependencyObject);
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Thank you for clicking today, your clicks are important to us.");
        }

        void btnOnlyShowOffsetIndicators_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (UIElement child in this.canvas1.Children)
                child.Visibility = Visibility.Visible;

            this.ResetZOrder();
        }

        void btnOnlyShowOffsetIndicators_Checked(object sender, RoutedEventArgs e)
        {
            foreach (UIElement child in this.canvas1.Children)
            {
                child.Visibility =
                    child is Button && (child as Button).Content == null ?
                    Visibility.Visible :
                    Visibility.Collapsed;
            }

            this.ResetZOrder();
        }   
    }
}
