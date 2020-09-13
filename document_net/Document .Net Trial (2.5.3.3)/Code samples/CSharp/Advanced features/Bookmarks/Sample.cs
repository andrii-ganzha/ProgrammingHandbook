using System;
using System.IO;
using SautinSoft.Document;

namespace Sample
{
    class Sample
    {
        static void Main(string[] args)
        {
            Bookmarks();            
        }
        /// <summary>
        /// This sample shows how to work with bookmarks.
        /// </summary>
        public static void Bookmarks()
        {
            string docxPath = @"..\..\..\..\..\..\Testing Files\Bookmarks.docx";

            // Let's create document.
            DocumentCore document = new DocumentCore();
            //DocumentCore.Serial = "put your serial here";

            // Add text bounded by BookmarkStart and BookmarkEnd.
            document.Sections.Add(
            new Section(document,
                new Paragraph(document,
                new Run(document, "Text before bookmark. "),
                new BookmarkStart(document, "My bookmark"),
                new Run(document, "Text inside bookmark."),
                new BookmarkEnd(document, "My bookmark"),
                new Run(document, " Text after bookmark."))));

            // Modify text inside bookmark.
            document.Bookmarks["My bookmark"].GetContent(false).Replace("New text.");

            // Save DOCX to a file
            document.Save(docxPath);

            // Open the result for demonstation purposes.
            System.Diagnostics.Process.Start(docxPath);
        }
    }
}
