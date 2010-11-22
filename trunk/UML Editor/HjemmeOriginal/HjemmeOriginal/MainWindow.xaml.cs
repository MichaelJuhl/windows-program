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
using System.Collections;
using ShapeConnectors;

namespace HjemmeOriginal
{
    public partial class MainWindow : Window
    {
        bool isAddNewClass = false,
             isAddNewArrow = false,
             isMove = false;

        private UIElement elementForContextMenu;

        // flag that indicates that the link drawing with a mouse started
        bool isLinkStarted = false;
        bool closedArrow = false;
        int arrowType = 2;
        ArrowLine link;


        MyLabel linkedStack;





        public MainWindow()
        {
            InitializeComponent();

            this.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(left_MouseDown);
            this.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(left_MouseUp);
            this.PreviewMouseMove += new MouseEventHandler(MouseMoving);

            //this.PreviewMouseMove += new MouseEventHandler(left_MouseMove);
            //this.PreviewMouseMove += new MouseEventHandler(surface_MouseMove);
            //this.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(left_MouseUp);

            ////this.PreviewMouseRightButtonDown += left_MouseDown;
            //this.PreviewMouseRightButtonDown += btnOnlyShowOffsetIndicators_Checked;
            //this.PreviewMouseRightButtonDown += btnOnlyShowOffsetIndicators_Unchecked;
            //this.ResetZOrder();	
        }







        private void btn_class_click(object sender, RoutedEventArgs e)
        {
            isAddNewClass = true;
            isAddNewArrow = false;
            isMove = false;
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void btn_move_click(object sender, RoutedEventArgs e)
        {
            isAddNewClass = false;
            isAddNewArrow = false;
            isMove = true;
            Mouse.OverrideCursor = Cursors.SizeAll;

        }

        private void btn_Arrow_Click(object sender, RoutedEventArgs e)
        {
            if (isAddNewArrow == true)
            {
                isAddNewClass = false;
                isAddNewArrow = false;
                isMove = false;
            }
            else
            {
                isAddNewClass = false;
                isAddNewArrow = true;
                isMove = false;
                canvas1.isAddNewArrow = true;
            }
            Mouse.OverrideCursor = Cursors.Cross;
        }



        void left_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(canvas1);

            if (button_arrow.IsMouseOver || button_class.IsMouseOver || stackPanel1.IsMouseOver)
            {
                isAddNewClass = canvas1.isAddNewArrow = isMove = false;

            }



            // If adding new class...
            if (isAddNewClass)
            {
                string[] attrib = new string[] { "et", "to", "tre" };
                string[] method = new string[] { "first", "second", "third" };
                string name = "name";

                Klasse kl = new Klasse(name, attrib, method, point.X, point.Y);

                kl.Xkoor = point.X;
                kl.Ykoor = point.Y;

                MyStackPanel stack = new MyStackPanel();
                MyStackPanel stackAtt = new MyStackPanel();
                MyStackPanel stackMet = new MyStackPanel();

                MyLabel labelTop = new MyLabel(e.GetPosition(this));
                MyLabel labelAtt = new MyLabel(e.GetPosition(this));
                MyLabel labelMet = new MyLabel(e.GetPosition(this));

                Expander expAtt = new Expander();
                Expander expMet = new Expander();

                labelTop.Content = kl.Name;
                labelTop.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                labelTop.FontSize = 15;
                labelTop.FontWeight = FontWeights.Bold;


                foreach (string i in attrib)
                {
                    MyLabel labelAt1 = new MyLabel(e.GetPosition(this));
                    labelAt1.Content = i;
                    //  labelAt1.Margin = new Thickness(50, 50, 50, 50);
                    stackAtt.Children.Add(labelAt1);
                }

                foreach (string i in method)
                {
                    MyLabel labelMe1 = new MyLabel(e.GetPosition(this));
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

                //stack.Opacity = new Double();
                //stack.Opacity = 0.25;


                LinearGradientBrush Gradient = new LinearGradientBrush();
                Gradient.StartPoint = new Point(0.5, 0);
                Gradient.EndPoint = new Point(0.5, 1);
                Gradient.GradientStops.Add(new GradientStop(Colors.Blue, 0.0));
                Gradient.GradientStops.Add(new GradientStop(Colors.LightBlue, 1.0));

                stack.Background = Gradient;

                canvas1.Children.Add(stack);

                DragCanvas.SetTop(stack, kl.Ykoor);
                DragCanvas.SetLeft(stack, kl.Xkoor);

                //  obj.Add(kl);         

                // resume common layout for application
                button_class.IsEnabled = button_arrow.IsEnabled = button_move.IsEnabled = true;
                e.Handled = true;
            }
            //  Console.WriteLine("hej");
            //  Console.WriteLine("type: " + e.Source.GetType());


            // Is adding new link and a MyStack object is clicked...
            if (isAddNewArrow && e.Source.GetType() == typeof(MyLabel))
            {
                //  Console.Write("hej1");
                if (!isLinkStarted)
                {
                    // Console.Write("hej2");
                    if (link == null || (link.X1 != link.X2 || link.Y1 != link.Y2))
                    {
                        //  Console.Write("hej3");
                        Point position = e.GetPosition(this);
                        position.X -= 75;
                        position.Y -= 26;
                        link = new ArrowLine();
                        link.X1 = position.X;
                        link.Y1 = position.Y;
                        link.X2 = position.X;
                        link.Y2 = position.Y;
                        canvas1.Children.Add(link);
                        link.Stroke = Brushes.Black;
                        link.StrokeThickness = 1;
                        link.IsArrowClosed = closedArrow;
                        link.ArrowEnds = (ArrowEnds)arrowType;
                        isLinkStarted = true;
                        linkedStack = e.Source as MyLabel;
                        e.Handled = true;
                    }
                }
            }
        }

        void left_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //  Console.WriteLine("UP");
            // If "Add link" mode enabled and line drawing started (line placed to canvas)
            if (isAddNewArrow && isLinkStarted)
            {
                //   Console.WriteLine("UP1");
                // declare the linking state
                bool linked = false;

                // We released the button on MyStackPanel object
                if (e.Source.GetType() == typeof(MyLabel))
                {
                    //   Console.WriteLine("UP2");
                    MyLabel targetStack = e.Source as MyLabel;

                    // define the final endpoint of line
                    link.X2 = e.GetPosition(this).X;
                    link.Y2 = e.GetPosition(this).Y;

                    // if any line was drawn (avoid just clicking on the thumb)
                    if ((link.X1 != link.X2 || link.Y1 != link.Y2) && linkedStack != targetStack)
                    {
                        //  Console.WriteLine("UP3");

                        //Console.WriteLine("endpoint før: " + link.EndPoint.ToString());
                        //Console.WriteLine("startpoint før: " + link.StartPoint.ToString());
                        //Console.WriteLine("targetstack før: " + targetStack.ToString());
                        //Console.WriteLine("sourcestack før: " + linkedStack.ToString());


                        // establish connection

                        linkedStack.LinkTo(targetStack, link, closedArrow, arrowType);
                        // set linked state to true
                        linked = true;
                    }
                }
                // if we didn't manage to approve the linking state
                // button is not released on MyThumb object or double-clicking was performed
                if (!linked)
                {
                    canvas1.Children.Remove(link);
                    // clear the link variable
                    link = null;
                }

                // exit link drawing mode
                isLinkStarted = false;
                // configure GUI
                button_class.IsEnabled = button_arrow.IsEnabled = true;

                e.Handled = true;
            }

        }


        // Handles the mouse move event when dragging/drawing the new connection link
        void MouseMoving(object sender, MouseEventArgs e)
        {


            if (isAddNewArrow && isLinkStarted)
            {
                //Console.WriteLine("endpoint moving: " + link.EndPoint.ToString());
                //Console.WriteLine("startpoint moving: " + link.StartPoint.ToString());

                // Set the new link end point to current mouse position
                Point x = e.GetPosition(this);
                x.X -= 75 - 10;
                x.Y -= 26 - 10;
                link.X2 = x.X;
                link.Y2 = x.Y;
                e.Handled = true;
            }
        }


        private void Temp(object sender, RoutedEventArgs e)
        {

        }




    }





























    /*
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
 
    
    
    
    
    
    public partial class MainWindow : Window
    {
        // The original position of the element and the position where the (mouse-) drag started.
        private Point _originalElementPos, _dragStart;
        
        // flag for enabling "New thumb" mode
        bool isAddNewClass = false;
        bool isAddNewNote = false;
        // flag for enabling "New link" mode
        bool isAddNewArrow = false;
        bool isMove = false;
        // flag that indicates that the link drawing with a mouse started
        bool isLinkStarted = false;
        // variable to hold the thumb drawing started from
        MyStackPanel linkedStack;
        // Line drawn by the mouse before connection established
        LineGeometry link;

        private UIElement elementForContextMenu;
        List<Klasse> obj = new List<Klasse>();

        //Klasse[] objekter = new Klasse[30];
        // ArrayList[] objekter;

        // The selected element (i.e. the element being dragged.)
        private UIElement _draggedElement;



        public MainWindow()
        {
            InitializeComponent();
            this.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(left_MouseDown);
            this.PreviewMouseMove += new MouseEventHandler(left_MouseMove);
            this.PreviewMouseMove += new MouseEventHandler(surface_MouseMove);
            this.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(left_MouseUp);
            
            //this.PreviewMouseRightButtonDown += left_MouseDown;
            this.PreviewMouseRightButtonDown += btnOnlyShowOffsetIndicators_Checked;
            this.PreviewMouseRightButtonDown += btnOnlyShowOffsetIndicators_Unchecked;
            this.ResetZOrder();	
        }

        private void Temp(object sender, RoutedEventArgs e)
        {
            
        }

        private void btn_class_click(object sender, RoutedEventArgs e)
        {
            isAddNewClass = true;
            isAddNewArrow = false;
            isMove = false;
            Mouse.OverrideCursor = Cursors.SizeAll;
        }

        private void btn_move_click(object sender, RoutedEventArgs e)
        {
            isAddNewClass = false;
            isAddNewArrow = false;
            isMove = true;
            Mouse.OverrideCursor = Cursors.SizeAll;

        }

        private void btn_Arrow_Click(object sender, RoutedEventArgs e)
        {
            if (isAddNewArrow == true)
            {
                isAddNewArrow = false;
            }
            else
            {
                isAddNewArrow = true;
            }
            Mouse.OverrideCursor = Cursors.Cross;
            //  btnNewAction.IsEnabled = btnNewLink.IsEnabled = false;
        }

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
                bool canBeDragged = DragCanvas.GetCanBeDragged(this.elementForContextMenu);
                DragCanvas.SetCanBeDragged(this.elementForContextMenu, !canBeDragged);
                (e.Source as MenuItem).IsChecked = !canBeDragged;
            }
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

        private void ResetZOrder()
        {
            // Set the z-index of every visible child in the Canvas.
            int index = 0;
            for (int i = 0; i < this.canvas1.Children.Count; ++i)
                if (this.canvas1.Children[i].Visibility == Visibility.Visible)
                    DragCanvas.SetZIndex(this.canvas1.Children[i], index++);
        }

        void left_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.canvas1.ElementBeingDragged != null)
                this.elementForContextMenu = this.canvas1.ElementBeingDragged;
            else
                this.elementForContextMenu =
                    this.canvas1.FindCanvasChild(e.Source as DependencyObject);
            
            
            
            Point point = e.GetPosition(canvas1);

            if (button_arrow.IsMouseOver || button_class.IsMouseOver || button_note.IsMouseOver || stackPanel1.IsMouseOver)
            {
                isAddNewClass = isAddNewNote = isAddNewArrow = isMove = false;

            }

            // If adding new class...
            if (isAddNewClass)
            {


                string[] attrib = new string[] { "et", "to", "tre" };
                string[] method = new string[] { "first", "second", "third" };
                string name = "name";

                Klasse kl = new Klasse(name, attrib, method, point.X, point.Y);

                MyStackPanel stack = new MyStackPanel();
                MyStackPanel stackAtt = new MyStackPanel();
                MyStackPanel stackMet = new MyStackPanel();

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
                //stack.Children.Add(expAtt);
                //stack.Children.Add(expMet);

                LinearGradientBrush Gradient = new LinearGradientBrush();
                Gradient.StartPoint = new Point(0.5, 0);
                Gradient.EndPoint = new Point(0.5, 1);
                Gradient.GradientStops.Add(new GradientStop(Colors.Blue, 0.0));
                Gradient.GradientStops.Add(new GradientStop(Colors.LightBlue, 1.0));

                stack.Background = Gradient;

                canvas1.Children.Add(stack);

                DragCanvas.SetTop(stack, kl.Ykoor);
                DragCanvas.SetLeft(stack, kl.Xkoor);

              //  obj.Add(kl);

                // resume common layout for application
                button_class.IsEnabled = button_arrow.IsEnabled = true;
                e.Handled = true;
            }


            // Is adding new link and a MyStack object is clicked...
            if (isAddNewArrow && e.Source.GetType() == typeof(MyStackPanel))
            {
                if (!isLinkStarted)
                {
                    if (link == null || link.EndPoint != link.StartPoint)
                    {
                        Point position = e.GetPosition(this);
                        link = new LineGeometry(position, position);
                        isLinkStarted = true;
                        linkedStack = e.Source as MyStackPanel;
                        e.Handled = true;
                    }
                }
            }
        }

       //private void surface_MouseDown(object sender, MouseButtonEventArgs e)
       // {
       //     if (this.canvas1.ElementBeingDragged != null)
       //         this.elementForContextMenu = this.canvas1.ElementBeingDragged;
       //     else
       //         this.elementForContextMenu =
       //             this.canvas1.FindCanvasChild(e.Source as DependencyObject);

       // }
        
       
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
                DragCanvas.SetLeft(_draggedElement, newpos.X);
                DragCanvas.SetTop(_draggedElement, newpos.Y);
            }
        }

        private void surface_MouseLeave(object sender, MouseEventArgs e)
        {
            _draggedElement = null;
        }

        // Handles the mouse up event applying the new connection link or resetting it
        void left_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // If "Add link" mode enabled and line drawing started (line placed to canvas)
            if (isAddNewArrow && isLinkStarted)
            {
                // declare the linking state
                bool linked = false;
                // We released the button on MyThumb object
                if (e.Source.GetType() == typeof(MyStackPanel))
                {
                    MyStackPanel targetStack = e.Source as MyStackPanel;
                    // define the final endpoint of line
                    link.EndPoint = e.GetPosition(this);
                    // if any line was drawn (avoid just clicking on the thumb)
                    if (link.EndPoint != link.StartPoint && linkedStack != targetStack)
                    {
                        // establish connection
                        linkedStack.LinkTo(targetStack, link);
                        // set linked state to true
                        linked = true;
                    }
                }
                // if we didn't manage to approve the linking state
                // button is not released on MyThumb object or double-clicking was performed
                if (!linked)
                {
                    // clear the link variable
                    link = null;
                }

                // exit link drawing mode
                isLinkStarted = false;
                // configure GUI
                button_class.IsEnabled = button_arrow.IsEnabled = true;
                //Mouse.OverrideCursor = null;
                e.Handled = true;
            }
           
       }

        // Handles the mouse move event when dragging/drawing the new connection link
        void left_MouseMove(object sender, MouseEventArgs e)
        {
            if (isAddNewArrow && isLinkStarted)
            {
                // Set the new link end point to current mouse position
                link.EndPoint = e.GetPosition(this);
                e.Handled = true;
            }
        }

    }*/
}
