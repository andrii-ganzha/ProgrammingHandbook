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
        /// Load an existing .rtf and save it as a new document.
        /// </summary>
        public static void LoadAndSave()
        {
            // Path to a loadable document.
            string loadPath = @"..\..\..\..\..\..\Testing Files\example.rtf";

            //DocumentCore.Serial = "put your serial here";
            DocumentCore docx = DocumentCore.Load(loadPath);

            string savePath = Path.ChangeExtension(loadPath, ".new.rtf");
            docx.Save(savePath, SaveOptions.RtfDefault);

            // Open both files: example.docx and example.new.rtf.
            //System.Diagnostics.Process.Start(loadPath);
            System.Diagnostics.Process.Start(savePath);

        }
 
    }
}
