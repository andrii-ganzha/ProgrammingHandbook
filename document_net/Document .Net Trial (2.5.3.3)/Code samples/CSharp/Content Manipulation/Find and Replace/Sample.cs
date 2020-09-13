using System;
using System.IO;
using SautinSoft.Document;
using System.Linq;

namespace Sample
{
    class Sample
    {
        static void Main(string[] args)
        {
            FindAndReplace();            
        }
        /// <summary>
        /// Creates a simple Docx document.
        /// </summary>
        public static void FindAndReplace()
        {
            // Path to and input document
            string inputPath = @"..\..\..\..\..\..\Testing Files\table.docx";
            string outPath = Path.ChangeExtension(inputPath, ".new.docx");

            //DocumentCore.Serial = "put your serial here";

            // Open document.
            DocumentCore docx = DocumentCore.Load(inputPath);

            // Replace "row" to "R"
            foreach (ContentRange text in docx.Content.Find("row").Reverse())
            {   
                text.Replace("R");               
            }

            // Make "col" Green, Bold.            
            foreach (ContentRange text in docx.Content.Find("col").Reverse())
            {
                CharacterFormat cf = (text.Start.Parent as Run).CharacterFormat;
                cf.Bold = true;
                cf.FontColor = Color.Green;
            }

            // Save DOCX to a file
            docx.Save(outPath);

            // Open the result for demonstation purposes.
            System.Diagnostics.Process.Start(inputPath);
            System.Diagnostics.Process.Start(outPath);
        }

    }
}
