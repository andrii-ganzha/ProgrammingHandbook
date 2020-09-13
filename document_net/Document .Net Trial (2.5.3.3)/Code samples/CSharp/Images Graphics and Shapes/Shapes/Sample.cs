using System;
using System.IO;
using SautinSoft.Document;
using SautinSoft.Document.Drawing;

namespace Sample
{
    class Sample
    {
        static void Main(string[] args)
        {
            Shapes();            
        }
        /// <summary>
        /// This sample shows how to work with shapes.
        /// </summary>
        public static void Shapes()
        {
            string pictPath = @"..\..\..\..\..\..\Testing Files\image1.jpg";
            string docxPath = @"..\..\..\..\..\..\Testing Files\Shapes.docx";

            // Let's create document.
            DocumentCore document = new DocumentCore();
            //DocumentCore.Serial = "put your serial here";

            // Create floating layout for shape1.
            FloatingLayout fl1 = new FloatingLayout(
                new HorizontalPosition(HorizontalPositionType.Left, HorizontalPositionAnchor.Column),
                new VerticalPosition(VerticalPositionType.Center, VerticalPositionAnchor.Page),
                new Size(100, 100));
				
            // Create shape1 with preset geometry.
            Shape shape1 = new Shape(document);

            // For top-level shapes, pictures or groups setting values ​​of Size or Offset properties 
            // (see below) has no effect. 
            // shape1.Offset = new Point(0, 0);
            // shape1.Size = new Size(100, 100);

            // Specify outline and fill.
            shape1.Outline.Fill.SetSolid(Color.Red);
            shape1.Outline.Width = 2;
            shape1.Fill.SetSolid(Color.White);

            // Specify a figure.
            shape1.Geometry.SetPreset(Figure.SmileyFace);


            // Create floating layout for shape2.
            FloatingLayout fl2 = new FloatingLayout(
                new HorizontalPosition(HorizontalPositionType.Center, HorizontalPositionAnchor.Column),
                new VerticalPosition(VerticalPositionType.Center, VerticalPositionAnchor.Page),
                new Size(100, 100));

            // Create shape2 with custom geometry path (using points array).
            Shape shape2 = new Shape(document);

            // Specify outline and picture fill.
            shape2.Outline.Fill.SetSolid(Color.Green);
            shape2.Outline.Width = 2;
            shape2.Fill.SetPicture(pictPath);

            // Specify the maximum X and Y coordinates that should be used
            // for within the path coordinate system.
            Size size = new Size(1, 1);

            // Specify the path points (draw a circle of 10 points).
            Point[] points = new Point[10];
            double a = 0;
            for (int i = 0; i < 10; ++i)
            {
                points[i] = new Point(0.5 + Math.Sin(a) * 0.5, 0.5 + Math.Cos(a) * 0.5);
                a += 2 * Math.PI / 10;
            }

            // Create and add new custom path from specified points array.
            shape2.Geometry.SetCustom().AddPath(size, points, true);


            // Create floating layout for shape3.
            FloatingLayout fl3 = new FloatingLayout(
                new HorizontalPosition(HorizontalPositionType.Right, HorizontalPositionAnchor.Column),
                new VerticalPosition(VerticalPositionType.Center, VerticalPositionAnchor.Page),
                new Size(100, 100));

            // Create shape3 with custom geometry path (using path elements).
            Shape shape3 = new Shape(document);

            // Specify outline and fill.
            shape3.Outline.Fill.SetSolid(Color.Blue);
            shape3.Outline.Width = 2;
            shape3.Fill.SetSolid(Color.Yellow);

            // Create and add new custom path.
            CustomPath path = shape3.Geometry.SetCustom().AddPath(new Size(1, 1));

            // Specify path elements.
            path.MoveTo(new Point(0, 0));
            path.AddLine(new Point(0, 1));
            path.AddLine(new Point(1, 1));
            path.AddLine(new Point(1, 0));
            path.ClosePath();


            // Add drawing elements to document.
            document.Content.End.Insert(new DrawingElement(document, fl1, shape1).Content);
            document.Content.End.Insert(new DrawingElement(document, fl2, shape2).Content);
            document.Content.End.Insert(new DrawingElement(document, fl3, shape3).Content);


            // Save DOCX to a file
            document.Save(docxPath);

            // Open the result for demonstation purposes.
            System.Diagnostics.Process.Start(docxPath);
        }
    }
}
