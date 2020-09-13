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
            HeadersAndFooters();            
        }
        /// <summary>
        /// How to add a header and footer into a document.
        /// </summary>
        public static void HeadersAndFooters()
        {
            string docxPath = @"..\..\..\..\..\..\Testing Files\HeadersAndFooters.docx";

            // Let's create a simple DOCX document.
            DocumentCore docx = new DocumentCore();
            //DocumentCore.Serial = "put your serial here";

            // Add new section
            Section s = new Section(docx);
            docx.Sections.Add(s);

            // Let's add a paragraph with text.
            Paragraph p = new Paragraph(docx);
            docx.Sections[0].Blocks.Add(p);

            p.ParagraphFormat.Alignment = HorizontalAlignment.Justify;
            p.Content.Start.Insert("Once upon a time, in a far away swamp, there lived an ogre named Shrek whose precious " +
                                   "solitude is suddenly shattered by an invasion of annoying fairy tale characters...", new CharacterFormat() { Size = 12, FontName = "Arial" });

            // Add a header.
            HeaderFooter h = new HeaderFooter(docx, HeaderFooterType.HeaderDefault);
            h.Content.Start.Insert("Shrek and Donkey travel to the castle and split up to find Fiona.", new CharacterFormat() { Size = 14.0 });
            s.HeadersFooters.Add(h);

            // Add a footer with text and image.
            HeaderFooter f = new HeaderFooter(docx, HeaderFooterType.FooterDefault);
            Paragraph p1 = new Paragraph(docx);
            p1.Content.Start.Insert("Shrek story. ", new CharacterFormat() { Size = 14.0 });
            p1.ParagraphFormat.Alignment = HorizontalAlignment.Center;

            // add image.
            string pictFilePath = Path.Combine(Path.GetDirectoryName(docxPath), "image1.jpg");
            Picture pict = new Picture(docx, pictFilePath);

            InlineLayout il = new InlineLayout(new Size(30, 30));
            DrawingElement de = new DrawingElement(docx, il, pict);
            p1.Content.End.Insert(de.Content);
            f.Blocks.Add(p1);
            s.HeadersFooters.Add(f);

            // Save DOCX to a file
            docx.Save(docxPath);

            // Open the result for demonstation purposes.
            System.Diagnostics.Process.Start(docxPath);
        }

    }
}
