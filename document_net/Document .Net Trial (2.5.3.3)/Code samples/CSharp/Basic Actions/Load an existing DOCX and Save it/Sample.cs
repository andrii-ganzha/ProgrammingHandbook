using System;
using System.IO;
using SautinSoft.Document;

namespace Sample
{
    class Sample
    {
        static void Main(string[] args)
        {
            LoadAndSave();
        }
        /// <summary>
        /// Load an existing document (*.docx, *.rtf, *.pdf) and save it as a new.
        /// </summary>
        public static void LoadAndSave()
        {
            // Path to a loadable document.
            string loadPath = @"..\..\..\..\..\..\Testing Files\example.docx";

            //DocumentCore.Serial = "put your serial here";
            DocumentCore docx = DocumentCore.Load(loadPath);

            string savePath = Path.ChangeExtension(loadPath, ".new.docx");
            docx.Save(savePath);

            // Open both files: example.docx and example.new.docx.
            //System.Diagnostics.Process.Start(loadPath);
            System.Diagnostics.Process.Start(savePath);
        }
 
    }
}
