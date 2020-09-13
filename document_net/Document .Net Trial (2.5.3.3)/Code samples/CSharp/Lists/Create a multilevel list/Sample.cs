using System;
using System.IO;
using SautinSoft.Document;

namespace Sample
{
    class Sample
    {
        static void Main(string[] args)
        {            
            CreateMultilevelLists();
        }
        /// <summary>
        /// How to create multilevel ordered and unordered lists.
        /// </summary>
        public static void CreateMultilevelLists()
        {
            string docxPath = @"..\..\..\..\..\..\Testing Files\Multilvel Lists.docx";

            // Let's create a simple DOCX document.
            DocumentCore docx = new DocumentCore();
            //DocumentCore.Serial = "put your serial here";

            // Add new section
            Section s = new Section(docx);
            docx.Sections.Add(s);

            string[] myCollection = new string[] { "One", "Two", "Three", "Four", "Five" };

            // Create list style.
            ListStyle ls = new ListStyle("", ListTemplateType.NumberWithDot);
            docx.Styles.Add(ls);

            // Add the collection of paragraphs marked as ordered list.
            int lvl = 0;
            foreach (string listText in myCollection)
            {
                Paragraph p = new Paragraph(docx);
                docx.Sections[0].Blocks.Add(p);

                p.Content.End.Insert(listText, new CharacterFormat() { Size = 14.0, FontColor = Color.Black });
                p.ListFormat.Style = ls;
                p.ListFormat.ListLevelNumber = lvl++;
                p.ParagraphFormat.SpaceAfter = 0;
            }

            // Add the collection of paragraphs marked as unordered list (bullets).
            // Create list style.
            ListStyle ls1 = new ListStyle("", ListTemplateType.Bullet);
            docx.Styles.Add(ls1);

            lvl = 0;
            foreach (string listText in myCollection)
            {
                Paragraph p = new Paragraph(docx);
                docx.Sections[0].Blocks.Add(p);

                p.Content.End.Insert(listText, new CharacterFormat() { Size = 14.0, FontColor = Color.Black });
                p.ListFormat.Style = ls1;
                p.ListFormat.ListLevelNumber = lvl++;
                p.ParagraphFormat.SpaceAfter = 0;
            }

            // Save DOCX to a file
            docx.Save(docxPath);

            // Open the result for demonstation purposes.
            System.Diagnostics.Process.Start(docxPath);
        }


    }
}
