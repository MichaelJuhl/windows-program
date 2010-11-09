using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ShapeConnectors
{
    public partial class Window1 : Window
    {
        // flag for enabling "New thumb" mode
        bool isAddNewAction = false;
        bool isAddNewUser = false;
        // flag for enabling "New link" mode
        bool isAddNewLink = false;
        // flag that indicates that the link drawing with a mouse started
        bool isLinkStarted = false;
        // variable to hold the thumb drawing started from
        MyThumb linkedThumb;
        // Line drawn by the mouse before connection established
        ArrowLine link;
        ArrowLine aline1 = new ArrowLine();
        bool closedArrow = false;
        int arrowType = 2;


        public Window1()
        {
            InitializeComponent();            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            aline1.Stroke = Brushes.Black;
            aline1.StrokeThickness = 1;
            aline1.X1 = 100;
            aline1.Y1 = 100;
            aline1.X2 = 200;
            aline1.Y2 = 200;
            aline1.ArrowEnds = (ArrowEnds) 3;
            aline1.IsArrowClosed = false;

            
            //myCanvas.Children.Add(myThumb1.LinkTo(myThumb2));
            //connectors.Children.Add(aline1);
            // Setup connections for predefined thumbs            
       /*     connectors.Children.Add(myThumb1.LinkTo(myThumb2));
            connectors.Children.Add(myThumb2.LinkTo(myThumb3));
            connectors.Children.Add(myThumb3.LinkTo(myThumb4));
            connectors.Children.Add(myThumb4.LinkTo(myThumb1)); */

            this.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(Window1_PreviewMouseLeftButtonDown);
            this.PreviewMouseMove += new MouseEventHandler(Window1_PreviewMouseMove);
            this.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(Window1_PreviewMouseLeftButtonUp);
            

            this.Title = "Links established: " + connectors.Children.Count.ToString();
            btnClosedArrow.IsEnabled = btnOpenArrow.IsEnabled = btnNoArrow.IsEnabled = btnStartArrow.IsEnabled = btnBothArrow.IsEnabled = btnEndArrow.IsEnabled = false;

        }

        // Event hanlder for dragging functionality support
        private void onDragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            // Exit dragging operation during adding new link
            if (isAddNewLink) return;

            MyThumb thumb = e.Source as MyThumb;

            Canvas.SetLeft(thumb, Canvas.GetLeft(thumb) + e.HorizontalChange);
            Canvas.SetTop(thumb, Canvas.GetTop(thumb) + e.VerticalChange);
             
            // Update links' layouts for active thumb
            thumb.UpdateLinks();
        }

        // Event handler for creating new thumb element by left mouse click
        // and visually connecting it to the myThumb2 element
        void Window1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (btnNewUser.IsMouseOver || btnNewLink.IsMouseOver || btnNewAction.IsMouseOver || btnMove.IsMouseOver || btnNoArrow.IsMouseOver ||
                btnOpenArrow.IsMouseOver || btnClosedArrow.IsMouseOver || btnStartArrow.IsMouseOver || btnBothArrow.IsMouseOver || btnEndArrow.IsMouseOver)
            {
                isAddNewLink = isAddNewUser = isAddNewAction = false;

            }

            // If adding new action...
            if (isAddNewAction)
            {
                // Create new thumb object based on a static template "BasicShape1"
                // specifying thumb text as "action" and icon as "/Images/gear_connection.png"
                MyThumb newThumb = new MyThumb(
                    Application.Current.Resources["BasicShape2"] as ControlTemplate,
                    "action",
                    "/Images/gear_connection.png",     
                    e.GetPosition(this),
                    onDragDelta);                

                // Put newly created thumb on the canvas
                myCanvas.Children.Add(newThumb);
                
                // resume common layout for application
                //isAddNewAction = false;                
                //Mouse.OverrideCursor = null;
                btnNewAction.IsEnabled = btnNewLink.IsEnabled = true;
                e.Handled = true;
            }

            if (isAddNewUser)
            {
                // Create new thumb object based on a static template "BasicShape1"
                // specifying thumb text as "action" and icon as "/Images/gear_connection.png"
                MyThumb newThumb = new MyThumb(
                    Application.Current.Resources["BasicShape1"] as ControlTemplate,
                    "user",
                    "/Images/user1.png",
                    e.GetPosition(this),
                    onDragDelta);

                // Put newly created thumb on the canvas
                myCanvas.Children.Add(newThumb);

                // resume common layout for application
                //isAddNewUser = false;
                //Mouse.OverrideCursor = null;
                btnNewUser.IsEnabled = btnNewLink.IsEnabled = true;
                e.Handled = true;
            }

            // Is adding new link and a thumb object is clicked...
            if (isAddNewLink && e.Source.GetType() == typeof(MyThumb))
            {                
                if (!isLinkStarted)
                {
                    if (link == null || (link.X1 != link.X2 || link.Y1 != link.Y2))
                    {
                        Point position = e.GetPosition(this);
                        link = new ArrowLine();
                        link.X1 = position.X;
                        link.Y1 = position.Y;
                        link.X2 = position.X;
                        link.Y2 = position.Y;
                        
                        myCanvas.Children.Add(link);
                        link.Stroke = Brushes.Black;
                        link.StrokeThickness = 1;
                        link.IsArrowClosed = closedArrow;
                        link.ArrowEnds = (ArrowEnds)arrowType;

                        isLinkStarted = true;
                        linkedThumb = e.Source as MyThumb;
                        e.Handled = true;
                    }
                }
            }
        }

        // Handles the mouse move event when dragging/drawing the new connection link
        void Window1_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (isAddNewLink && isLinkStarted)
            {
                // Set the new link end point to current mouse position
                link.X2 = e.GetPosition(this).X;
                link.Y2 = e.GetPosition(this).Y;
                
                e.Handled = true;
            }
        }

        // Handles the mouse up event applying the new connection link or resetting it
        void Window1_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // If "Add link" mode enabled and line drawing started (line placed to canvas)
            if (isAddNewLink && isLinkStarted)
            {
                // declare the linking state
                bool linked = false;
                // We released the button on MyThumb object

                if (e.Source.GetType() == typeof(MyThumb))
                {
                    MyThumb targetThumb = e.Source as MyThumb;
                    // define the final endpoint of line
                    link.X2 = e.GetPosition(this).X;
                    link.Y2 = e.GetPosition(this).Y;
                    // if any line was drawn (avoid just clicking on the thumb)

                    if ((link.X1 != link.X2 || link.Y1 != link.Y2) && linkedThumb != targetThumb)
                    {

                        // establish connection
                        linkedThumb.LinkTo(targetThumb, link,closedArrow,arrowType);
                        // set linked state to true
                        linked = true;
                    }
                }
                // if we didn't manage to approve the linking state
                // button is not released on MyThumb object or double-clicking was performed
                if (!linked)
                {
                    // remove line from the canvas
                    myCanvas.Children.Remove(link);
                    // clear the link variable
                    link = null;
                }
                
                // exit link drawing mode
                isLinkStarted = false;
                // configure GUI
                btnNewAction.IsEnabled = btnNewLink.IsEnabled = true;
                //Mouse.OverrideCursor = null;
                e.Handled = true;
            }
            this.Title = "Links established: " + connectors.Children.Count.ToString();
        }
        // Event handler for enabling new thumb creation by left mouse button click
        private void btnNewAction_Click(object sender, RoutedEventArgs e)
        {            
            isAddNewAction = true;
            isAddNewLink = false;
            Mouse.OverrideCursor = Cursors.SizeAll;
            btnClosedArrow.IsEnabled = btnOpenArrow.IsEnabled = btnNoArrow.IsEnabled = btnEndArrow.IsEnabled = btnBothArrow.IsEnabled = btnStartArrow.IsEnabled = false;
            //btnNewAction.IsEnabled = btnNewLink.IsEnabled = false;
        }
        private void btnNewUser_Click(object sender, RoutedEventArgs e)
        {
            isAddNewUser = true;
            isAddNewLink = false;
            Mouse.OverrideCursor = Cursors.SizeAll;
            btnClosedArrow.IsEnabled = btnOpenArrow.IsEnabled = btnNoArrow.IsEnabled = btnEndArrow.IsEnabled = btnBothArrow.IsEnabled = btnStartArrow.IsEnabled = false;
           // btnNewUser.IsEnabled = btnNewLink.IsEnabled = false;
        }
        private void btnMove_Click(object sender, RoutedEventArgs e)
        {
            isAddNewAction = false;
            isAddNewUser = false;
            isAddNewLink = false;
            Mouse.OverrideCursor = Cursors.SizeAll;
            // btnNewUser.IsEnabled = btnNewLink.IsEnabled = false;
            btnClosedArrow.IsEnabled = btnOpenArrow.IsEnabled = btnNoArrow.IsEnabled = btnEndArrow.IsEnabled = btnBothArrow.IsEnabled = btnStartArrow.IsEnabled = false;
        }

        private void btnNoArrow_Click(object sender, RoutedEventArgs e)
        {
            arrowType = 0;
            isAddNewLink = true;
            btnClosedArrow.IsEnabled = btnOpenArrow.IsEnabled = true;
            btnNoArrow.IsEnabled = false;

        }

        private void btnOpenArrow_Click(object sender, RoutedEventArgs e)
        {

            closedArrow = false;
            isAddNewLink = true;
            btnClosedArrow.IsEnabled = btnNoArrow.IsEnabled = true;
            btnOpenArrow.IsEnabled = false;
        }

        private void btnClosedArrow_Click(object sender, RoutedEventArgs e)
        {

            closedArrow = true;
            isAddNewLink = true;
            btnNoArrow.IsEnabled = btnOpenArrow.IsEnabled = true;
            btnClosedArrow.IsEnabled = false;
        }

        private void btnStartArrow_Click(object sender, RoutedEventArgs e)
        {
            arrowType = 1;
            isAddNewLink = true;
            btnEndArrow.IsEnabled = btnBothArrow.IsEnabled = true;
            btnStartArrow.IsEnabled = false;

        }

        private void btnEndArrow_Click(object sender, RoutedEventArgs e)
        {
            arrowType = 2;
            isAddNewLink = true;
            btnStartArrow.IsEnabled = btnBothArrow.IsEnabled = true;
            btnEndArrow.IsEnabled = false;

        }

        private void btnBothArrow_Click(object sender, RoutedEventArgs e)
        {
            arrowType = 3;
            isAddNewLink = true;
            btnEndArrow.IsEnabled = btnStartArrow.IsEnabled = true;
            btnBothArrow.IsEnabled = false;

        }

        private void btnNewLink_Click(object sender, RoutedEventArgs e)
        {
            if (isAddNewLink == true)
            {
                isAddNewLink = false;
            }
            else
            {
                isAddNewLink = true;

                if (closedArrow)
                {
                    btnNoArrow.IsEnabled = btnOpenArrow.IsEnabled = true;
                    btnClosedArrow.IsEnabled = false;
                }
                else
                {
                    btnNoArrow.IsEnabled = btnClosedArrow.IsEnabled = true;
                    btnOpenArrow.IsEnabled = false;
                }
                if (arrowType == 0)
                {
                    btnClosedArrow.IsEnabled = btnOpenArrow.IsEnabled = true;
                    btnNoArrow.IsEnabled = false;
                }
                if (arrowType == 1)
                {
                    btnEndArrow.IsEnabled = btnBothArrow.IsEnabled = true;
                    btnStartArrow.IsEnabled = false;
                }
                if (arrowType == 2)
                {
                    btnStartArrow.IsEnabled = btnBothArrow.IsEnabled = true;
                    btnEndArrow.IsEnabled = false;
                }
                if (arrowType == 3)
                {
                    btnEndArrow.IsEnabled = btnStartArrow.IsEnabled = true;
                    btnBothArrow.IsEnabled = false;
                }
            }
            Mouse.OverrideCursor = Cursors.Cross;
          //  btnNewAction.IsEnabled = btnNewLink.IsEnabled = false;
        }
    }
}
