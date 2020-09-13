using System;
using System.IO;
using System.Collections.Generic;
using SautinSoft.Document;
using SautinSoft.Document.Drawing;

namespace Sample
{
    class Sample
    {
        static void Main(string[] args)
        {
            AddPictures();
            
        }

        /// <summary>
        /// How to add pictures into a document.
        /// </summary>
        public static void AddPictures()
        {
            string docxPath = @"..\..\..\..\..\..\Testing Files\Pictures.docx";
            string pictPath = @"..\..\..\..\..\..\Testing Files\image1.jpg";

            // Let's create a simple DOCX document.
            DocumentCore docx = new DocumentCore();
            //DocumentCore.Serial = "put your serial here";

            // Add new section
            Section s = new Section(docx);
            docx.Sections.Add(s);

            // Let's add a some text.
            Paragraph p = new Paragraph(docx);
            s.Blocks.Add(p);
            p.Content.End.Insert("Once upon a time, in a far away swamp, there lived an ogre named Shrek...", new CharacterFormat() { FontName = "Calibri", Size = 16.0, FontColor = Color.Black });

            // Add picture after paragraph. 
            Picture pict = new Picture(docx, pictPath);
            InlineLayout il = new InlineLayout(new Size(100, 100));
            DrawingElement de = new DrawingElement(docx, il, pict);

            Paragraph pWithImg = new Paragraph(docx);
            pWithImg.ParagraphFormat.Alignment = HorizontalAlignment.Center;
            pWithImg.Content.Start.Insert(de.Content);
            s.Blocks.Add(pWithImg);

            // Add another text.
            s.Content.End.Insert("\nShrek and Donkey arrive at Farquaad's palace in Duloc, where they end up in a tournament.", new CharacterFormat() { FontName = "Calibri", Size = 16.0, FontColor = Color.Black });


            // Add the image at once after text.
            s.Content.End.Insert(de.Content);


            // Add the image as a shape with coordinates.
            Picture pict1 = new Picture(docx, pictPath);

            FloatingLayout fl = new FloatingLayout(
                new HorizontalPosition(150, LengthUnit.Millimeter, HorizontalPositionAnchor.Page),
                new VerticalPosition(90, LengthUnit.Millimeter, VerticalPositionAnchor.TopMargin),
                new Size(70, 70));
            DrawingElement de1 = new DrawingElement(docx, fl, pict1);
            s.Content.End.Insert(de1.Content);

            // Save DOCX to a file
            docx.Save(docxPath);

            // Open the result for demonstation purposes.
            System.Diagnostics.Process.Start(docxPath);
        }
       
    }
}
