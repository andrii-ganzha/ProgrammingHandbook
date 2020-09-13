using System;
using System.IO;
using SautinSoft.Document;

namespace Sample
{
    class Sample
    {
        static void Main(string[] args)
        {
            ConvertPdfToDocx();            
        }
        /// <summary>
        /// Load an existing PDF and save it as new DOCX document.
        /// </summary>
        public static void ConvertPdfToDocx()
        {            
            string pdfFile = Path.GetFullPath(@"..\..\..\..\..\..\Testing Files\example.pdf");
            string docxFile = Path.Combine(Path.GetDirectoryName(pdfFile), "Result.docx");

            //DocumentCore.Serial = "put your serial here";

            PdfLoadOptions pdfOptions = new PdfLoadOptions();
            pdfOptions.ConversionMode = PdfConversionMode.Flowing;
            pdfOptions.DetectTables = true;
            pdfOptions.RasterizeVectorGraphics = true;
            pdfOptions.FromPage = 1;
            pdfOptions.KeepCharScaleAndSpacing = true;

            DocumentCore pdf = DocumentCore.Load(pdfFile, pdfOptions);            
            
            pdf.Save(docxFile);

            // Open resulting DOCX.            
            System.Diagnostics.Process.Start(docxFile);
        }
    }
}
