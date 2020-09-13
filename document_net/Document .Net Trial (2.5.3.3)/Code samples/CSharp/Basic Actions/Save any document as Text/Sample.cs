using System;
using System.IO;
using SautinSoft.Document;

namespace Sample
{
    class Sample
    {
        static void Main(string[] args)
        {
            SaveAsText();            
        }
        /// <summary>
        /// Load an existing .docx file and save it as new Text document.
        /// </summary>
        public static void SaveAsText()
        {            
            string docxFile = @"..\..\..\..\..\..\Testing Files\example.docx";
            string textFile = Path.ChangeExtension(docxFile, ".txt");

            //DocumentCore.Serial = "put your serial here";
            DocumentCore docx = DocumentCore.Load(docxFile);

            TxtSaveOptions to = new TxtSaveOptions();
            to.Encoding = System.Text.Encoding.UTF8;

            docx.Save(textFile, to);

            // Open the result for demonstation purposes.
            System.Diagnostics.Process.Start(textFile);
        }

    }
}
