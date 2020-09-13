using System;
using System.IO;
using SautinSoft.Document;

namespace Sample
{
    class Sample
    {
        static void Main(string[] args)
        {
            DeleteContent();
        }
        /// <summary>
        /// Open a document and delete some content.
        /// </summary>
        public static void DeleteContent()
        {
            // Path to a input document.
            string loadPath = @"..\..\..\..\..\..\Testing Files\example.docx";
            string savePath = Path.ChangeExtension(loadPath, ".new.docx");

            //DocumentCore.Serial = "put your serial here";
            DocumentCore docx = DocumentCore.Load(loadPath);

            // Get all paragraphs from 1st section.
            var blocks = docx.Sections[0].Blocks;

            // Remove the text "rich text document" from the 3rd paragraph
            blocks = docx.Sections[0].Blocks;

            // Get 3rd paragraph
            Paragraph p3 = (Paragraph)blocks[2];

            p3.Content.FindFirst("rich text document").Delete();

            // Remove 1st paragraph
            blocks[0].Content.Delete();

            docx.Save(savePath);

            // Show original and resulted documents.
            System.Diagnostics.Process.Start(savePath);
            System.Diagnostics.Process.Start(loadPath);

        }

    }
}
