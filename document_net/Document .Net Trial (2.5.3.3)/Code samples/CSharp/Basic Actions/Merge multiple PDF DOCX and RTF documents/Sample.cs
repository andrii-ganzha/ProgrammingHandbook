using System;
using System.IO;
using SautinSoft.Document;
using System.Collections.Generic;

namespace Sample
{
    class Sample
    {
        static void Main(string[] args)
        {
            MergeMultipleDocuments();            
        }
        /// <summary>
        /// This sample shows how to merge multiple DOCX, RTF, PDF and Text files.
        /// </summary>
        public static void MergeMultipleDocuments()
        {
            // Working directory
            string workingDir = Path.GetFullPath(Directory.GetCurrentDirectory() + @"..\..\..\..\..\..\..\Testing Files");
            
            // Path to our combined document.
            string singleDocxPath = Path.Combine(workingDir, "Single.docx");
            // Remove it if our combined document is already exist.
            if (File.Exists(singleDocxPath))
                File.Delete(singleDocxPath);




            List<string> supportedFiles = new List<string>();

            // Fill the collection 'supportedFiles' by *.docx, *.rtf, *.pdf and *.txt.
            foreach (string file in Directory.GetFiles(workingDir, "*.*"))
            {
                string ext = Path.GetExtension(file).ToLower();

                if (ext == ".docx" || ext == ".rtf" || ext == ".pdf" || ext == ".txt")
                    supportedFiles.Add(file);
            }

            //DocumentCore.Serial = "put your serial here";

            // Create single docx.
            DocumentCore singleDocx = new DocumentCore();

            foreach (string file in supportedFiles)
            {
                DocumentCore document = DocumentCore.Load(file);

                Console.WriteLine("Adding: {0}...", Path.GetFileName(file));

                // Create import session.
                ImportSession session = new ImportSession(document, singleDocx, StyleImportingMode.UseDestinationStyles);

                // Loop through all sections in the source document.
                foreach (Section sourceSection in document.Sections)
                {
                    // Because we are copying a section from one document to another, 
                    // it is required to import the Section into the destination document.
                    // This adjusts any document-specific references to styles, bookmarks, etc.
                    // 
                    // Importing a element creates a copy of the original element, but the copy
                    // is ready to be inserted into the destination document.
                    Section importedSection = singleDocx.Import<Section>(sourceSection, true, session);

                    // Now the new section can be appended to the destination document.
                    singleDocx.Sections.Add(importedSection);
                }
            }

            // Save single DOCX to a file.            
            singleDocx.Save(singleDocxPath);

            // Open the result for demonstation purposes.
            System.Diagnostics.Process.Start(singleDocxPath);
        }
    }
}
