Imports System
Imports System.IO
Imports SautinSoft.Document

Module Sample

    Sub Main()
        ConvertPdfToDocx()
    End Sub
    ''' <summary>
    ''' Load an existing PDF and save it as new DOCX document.
    ''' </summary>
    Public Sub ConvertPdfToDocx()
        Dim pdfFile As String = Path.GetFullPath("..\..\..\..\..\Testing Files\example.pdf")
        Dim docxFile As String = Path.Combine(Path.GetDirectoryName(pdfFile), "Result.docx")

        'DocumentCore.Serial = "put your serial here";

        Dim pdfOptions As New PdfLoadOptions()
        pdfOptions.ConversionMode = PdfConversionMode.Flowing
        pdfOptions.DetectTables = True
        pdfOptions.RasterizeVectorGraphics = True
        pdfOptions.FromPage = 1

        Dim pdf As DocumentCore = DocumentCore.Load(pdfFile, pdfOptions)

        pdf.Save(docxFile)

        ' Open resulting DOCX.            
        System.Diagnostics.Process.Start(docxFile)
    End Sub
End Module
