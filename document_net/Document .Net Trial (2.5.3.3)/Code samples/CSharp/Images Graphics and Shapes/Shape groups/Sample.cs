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
            ShapeGroups();            
        }
        /// <summary>
        /// This sample shows how to work with shape groups.
        /// </summary>
        public static void ShapeGroups()
        {
            string pictPath = @"..\..\..\..\..\..\Testing Files\image1.jpg";
            string docxPath = @"..\..\..\..\..\..\Testing Files\Shape Groups.docx";

            // Let's create document.
            DocumentCore document = new DocumentCore();
            //DocumentCore.Serial = "put your serial here";

            // Create floating layout.
            HorizontalPosition hp = new HorizontalPosition(HorizontalPositionType.Center, HorizontalPositionAnchor.Page);
            VerticalPosition vp = new VerticalPosition(VerticalPositionType.Center, VerticalPositionAnchor.Page);
            FloatingLayout fl = new FloatingLayout(hp, vp, new Size(300, 300));

            // Create group.
            ShapeGroup group = new ShapeGroup(document);

            // For top-level shapes, pictures or groups setting values ​​of Size or Offset properties 
            // (see below) has no effect. 
            // group.Offset = new Point(0, 0);
            // group.Size = new Size(300, 300);

            // Specify the size dimensions of the child extents rectangle.
            group.ChildSize = new Size(100, 100);

            // Create a child shape #1 with preset geometry.
            Shape shape = new Shape(document);

            // Specify shape's size and offset relative to group's ChildSize (100x100).
            shape.Offset = new Point(0, 0);
            shape.Size = new Size(50, 50);

            // Specify outline and fill.
            shape.Outline.Fill.SetSolid(Color.Blue);
            shape.Outline.Width = 2;
            shape.Fill.SetSolid(Color.Red);

            // Shape will be rectangle.
            shape.Geometry.SetPreset(Figure.Rectangle);

            // Create a child shape #2 with picture.
            Picture picture = new Picture(document, pictPath);

            // Specify picture's size and offset relative to group's ChildSize (100x100).
            picture.Offset = new Point(50, 50);
            picture.Size = new Size(50, 50);

            // Specify picture fill mode.
            picture.ImageData.FillMode = PictureFillMode.Stretch;

            // Add child shapes into group.
            group.ChildShapes.Add(shape);
            group.ChildShapes.Add(picture);

            // Add drawing element to document.
            document.Content.End.Insert(new DrawingElement(document, fl, group).Content);

            // Save DOCX to a file
            document.Save(docxPath);

            // Open the result for demonstation purposes.
            System.Diagnostics.Process.Start(docxPath);
        }
    }
}
