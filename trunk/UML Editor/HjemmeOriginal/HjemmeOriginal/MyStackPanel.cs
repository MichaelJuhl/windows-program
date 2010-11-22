using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls.Primitives;
using ShapeConnectors;

namespace HjemmeOriginal
{
    class MyStackPanel : StackPanel
    {
        public List<ArrowLine> EndLines { get; private set; }
        public List<ArrowLine> StartLines { get; private set; } 

        public MyStackPanel()
            : base()
        {

            StartLines = new List<ArrowLine>();
            EndLines = new List<ArrowLine>();
       
        }


        



        public ArrowLine LinkTo(MyStackPanel target)
        {
            // Create new line geometry
            ArrowLine line = new ArrowLine();
            // Save as starting line for current thumb
            this.StartLines.Add(line);
            // Save as ending line for target thumb
            target.EndLines.Add(line);
            // Ensure both tumbs the latest layout
            this.UpdateLayout();
            target.UpdateLayout();
            // Update line position
            line.X1 = Canvas.GetLeft(this) + this.ActualWidth; //StartPoint = new Point(Canvas.GetLeft(this) + this.ActualWidth / 2, Canvas.GetTop(this) + this.ActualHeight / 2);
            line.Y1 = Canvas.GetTop(this) + this.ActualHeight; //EndPoint = new Point(Canvas.GetLeft(target) + target.ActualWidth / 2, Canvas.GetTop(target) + target.ActualHeight / 2);
            line.X2 = Canvas.GetLeft(target) + target.ActualWidth;
            line.Y2 = Canvas.GetTop(target) + target.ActualHeight;
            // return line for further processing
            line.Stroke = Brushes.Black;
            line.StrokeThickness = 1;
            return line;
        }

        // This method establishes a link between current thumb and target thumb using a predefined line geometry
        // Note: this is commonly to be used for drawing links with mouse when the line object is predefined outside this class
        public bool LinkTo(MyStackPanel target, ArrowLine line, bool arrowType, int arrowPlacement)
        {
            // Save as starting line for current thumb
            this.StartLines.Add(line);
            // Save as ending line for target thumb
            target.EndLines.Add(line);
            // Ensure both tumbs the latest layout
            this.UpdateLayout();
            target.UpdateLayout();
            // Update line position
            line.X1 = Canvas.GetLeft(this) + this.ActualWidth; //StartPoint = new Point(Canvas.GetLeft(this) + this.ActualWidth / 2, Canvas.GetTop(this) + this.ActualHeight / 2);
            line.Y1 = Canvas.GetTop(this) + this.ActualHeight; //EndPoint = new Point(Canvas.GetLeft(target) + target.ActualWidth / 2, Canvas.GetTop(target) + target.ActualHeight / 2);
            line.X2 = Canvas.GetLeft(target) + target.ActualWidth;
            line.Y2 = Canvas.GetTop(target) + target.ActualHeight;
            line.Stroke = Brushes.Black;
            line.StrokeThickness = 1;
            line.IsArrowClosed = arrowType;
            line.ArrowEnds = (ArrowEnds)arrowPlacement;
            return true;
        }
        public void UpdateLinks()
        {
            double left = DragCanvas.GetLeft(this);
            double top = DragCanvas.GetTop(this);

            for (int i = 0; i < this.StartLines.Count; i++)
            {
                this.StartLines[i].X1 = left + this.ActualWidth; //StartPoint = new Point(left + this.ActualWidth / 2, top + this.ActualHeight / 2);
                this.StartLines[i].Y1 = top + this.ActualHeight;
            }
            for (int i = 0; i < this.EndLines.Count; i++)
            {
                this.EndLines[i].X2 = left + this.ActualWidth;
                this.EndLines[i].Y2 = top + this.ActualHeight; //EndPoint = new Point(left + this.ActualWidth / 2, top + this.ActualHeight / 2);
            }
        }

    }
}
