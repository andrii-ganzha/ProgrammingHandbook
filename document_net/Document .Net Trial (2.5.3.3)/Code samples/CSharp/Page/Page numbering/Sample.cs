using System;
using System.IO;
using SautinSoft.Document;

namespace Sample
{
    class Sample
    {
        static void Main(string[] args)
        {
            PageNumbering();            
        }
        /// <summary>
        /// How to add a page numbering in a document.
        /// </summary>
        public static void PageNumbering()
        {
            string docxPath = @"..\..\..\..\..\..\Testing Files\PageNumbering.docx";

            // Let's create a DOCX document with multiple pages.
            DocumentCore docx = new DocumentCore();
            //DocumentCore.Serial = "put your serial here";


            string[] pagesText = new string[] { "One", "Two", "Three", "Four", "Five" };
            Random r = new Random();

            Section s = new Section(docx);
            docx.Sections.Add(s);

            HeaderFooter f = new HeaderFooter(docx, HeaderFooterType.FooterDefault);
            Paragraph par = new Paragraph(docx);
            par.ParagraphFormat.Alignment = HorizontalAlignment.Center;
            CharacterFormat cf = new CharacterFormat() { FontName = "Arial", Size = 12.0 };

            par.Content.Start.Insert("Page ", cf.Clone());
            Field fPage = new Field(docx, FieldType.Page);
            fPage.CharacterFormat = cf.Clone();
            par.Content.End.Insert(fPage.Content);
            par.Content.End.Insert(" of ", cf.Clone());
            Field fPages = new Field(docx, FieldType.NumPages);
            fPages.CharacterFormat = cf.Clone();
            par.Content.End.Insert(fPages.Content);

            f.Blocks.Add(par);
            s.HeadersFooters.Add(f);

            foreach (string text in pagesText)
            {
                Paragraph p = new Paragraph(docx);
                p.ParagraphFormat.Alignment = HorizontalAlignment.Center;
                s.Blocks.Add(p);

                int color = r.Next(0x000000, 0xFFFFFF);
                p.Content.Start.Insert(text, new CharacterFormat() { FontName = "Arial", Size = 72.0, FontColor = new Color(color) });
                p.Content.End.Insert(new SpecialCharacter(docx, SpecialCharacterType.PageBreak).Content);
            }

            // Save DOCX to a file
            docx.Save(docxPath);

            // Open the result for demonstation purposes.
            System.Diagnostics.Process.Start(docxPath);
        }
    }
}
