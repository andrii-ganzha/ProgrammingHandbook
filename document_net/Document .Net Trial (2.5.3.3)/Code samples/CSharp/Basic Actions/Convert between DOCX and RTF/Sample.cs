using System;
using System.IO;
using SautinSoft.Document;

namespace Sample
{
    class Sample
    {
        static void Main(string[] args)
        {
            //ConvertRtfToDocx();
            ConvertDocxToRtf();
        }
        /// <summary>
        /// Load an existing RTF document and save it as new DOCX.
        /// </summary>
        public static void ConvertRtfToDocx()
        {         
            string rtfFile = @"..\..\..\..\..\..\Testing Files\example.rtf";
            string docxFile = @"..\..\..\..\..\..\Testing Files\Result.docx";

            //DocumentCore.Serial = "put your serial here";
            DocumentCore rtf = DocumentCore.Load(rtfFile);            
            rtf.Save(docxFile);

            // Open resulting DOCX.            
            System.Diagnostics.Process.Start(docxFile);
        }

        /// <summary>
        /// Load an existing DOCX document and save it as new RTF.
        /// </summary>
        public static void ConvertDocxToRtf()
        {   
            string docxFile = @"..\..\..\..\..\..\Testing Files\example.docx";
            string rtfFile = @"..\..\..\..\..\..\Testing Files\Result.rtf";

            //DocumentCore.Serial = "put your serial here";
            DocumentCore docx = DocumentCore.Load(docxFile);
            docx.Save(rtfFile, new RtfSaveOptions());

            // Open resulting RTF.            
            System.Diagnostics.Process.Start(rtfFile);
        }
    }
}
