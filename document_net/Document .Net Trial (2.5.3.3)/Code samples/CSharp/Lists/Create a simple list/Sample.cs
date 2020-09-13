using System;
using System.IO;
using SautinSoft.Document;

namespace Sample
{
    class Sample
    {
        static void Main(string[] args)
        {
            CreateSimpleLists();            
        }
        /// <summary>
        /// How to create simple ordered and unordered lists.
        /// </summary>
        public static void CreateSimpleLists()
        {
            string docxPath = @"..\..\..\..\..\..\Testing Files\Lists.docx";

            // Let's create a simple DOCX document.
            DocumentCore docx = new DocumentCore();
            //DocumentCore.Serial = "put your serial here";

            // Add new section
            Section s = new Section(docx);
            docx.Sections.Add(s);

            string[] myCollection = new string[] { "One", "Two", "Three", "Four", "Five" };

            // Create list style.
            ListStyle ordList = new ListStyle("Simple Numbers", ListTemplateType.NumberWithDot);
            foreach (ListLevelFormat f in ordList.ListLevelFormats)
            {
                f.CharacterFormat.Size = 14;
            }
            docx.Styles.Add(ordList);

            // Add caption
            docx.Content.Start.Insert("This is a simple ordered list:", new CharacterFormat() { Size = 14.0, FontColor = Color.Black });


            // Add the collection of paragraphs marked as ordered list.
            int lvl = 0;
            foreach (string listText in myCollection)
            {
                Paragraph p = new Paragraph(docx);
                docx.Sections[0].Blocks.Add(p);

                p.Content.End.Insert(listText, new CharacterFormat() { Size = 14.0, FontColor = Color.Black });
                p.ListFormat.Style = ordList;
                p.ListFormat.ListLevelNumber = lvl;
                p.ParagraphFormat.SpaceAfter = 0;
            }

            // Add the collection of paragraphs marked as unordered list (bullets).

            // Add caption
            docx.Content.End.Insert("\n\nThis is a simple bulleted list:", new CharacterFormat() { Size = 14.0, FontColor = Color.Black });

            // Create list style.
            ListStyle bulList = new ListStyle("Bullets", ListTemplateType.Bullet);
            docx.Styles.Add(bulList);

            lvl = 0;
            foreach (string listText in myCollection)
            {
                Paragraph p = new Paragraph(docx);
                docx.Sections[0].Blocks.Add(p);

                p.Content.End.Insert(listText, new CharacterFormat() { Size = 14.0, FontColor = Color.Black });
                p.ListFormat.Style = bulList;
                p.ListFormat.ListLevelNumber = lvl;
                p.ParagraphFormat.SpaceAfter = 0;
            }

            // Save DOCX to a file
            docx.Save(docxPath);

            // Open the result for demonstation purposes.
            System.Diagnostics.Process.Start(docxPath);
        }
    }
}
