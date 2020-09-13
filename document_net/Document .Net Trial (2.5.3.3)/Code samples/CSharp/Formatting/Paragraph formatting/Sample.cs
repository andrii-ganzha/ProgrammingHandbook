using System;
using System.IO;
using SautinSoft.Document;

namespace Sample
{
    class Sample
    {
        static void Main(string[] args)
        {
            ParagraphFormatting();            
        }
        /// <summary>
        /// This sample shows how to specify a paragraph formatting for a document.
        /// </summary>
        public static void ParagraphFormatting()
        {                        
            string docxPath = @"..\..\..\..\..\..\Testing Files\Paragraph Formatting.docx";

            // Let's create a simple DOCX document.
            DocumentCore docx = new DocumentCore();
            //DocumentCore.Serial = "put your serial here";

            // Add new section
            Section s = new Section(docx);
            docx.Sections.Add(s);


            // Paragraph 1
            Paragraph p = new Paragraph(docx);
            docx.Sections[0].Blocks.Add(p);

            p.ParagraphFormat.Alignment = HorizontalAlignment.Left;
            p.ParagraphFormat.SpaceAfter = 20.0;

            p.Content.End.Insert("Let's add a first paragraph.", new CharacterFormat() { Size = 20, FontColor = new Color(0x999999) });

            // Paragraph 2.
            docx.Content.End.Insert("\nThis is a second paragraph.", new CharacterFormat() { FontName = "Calibri", Size = 16.0, FontColor = Color.Black, Bold = true });
            (docx.Sections[0].Blocks[0] as Paragraph).ParagraphFormat.Alignment = HorizontalAlignment.Center;

            // Multiple paragraphs
            for (int i = 0; i < 10; i++)
                docx.Content.End.Insert(String.Format("\nParagraph {0}.", i + 1), new CharacterFormat() { FontName = "Arial", Size = 12.0 });

            // Save DOCX to a file
            docx.Save(docxPath);

            // Open the result for demonstation purposes.
            System.Diagnostics.Process.Start(docxPath);
        }
    }
}
