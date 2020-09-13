Imports System
Imports System.IO
Imports SautinSoft.Document

Module Sample

    Sub Main()
        ConvertRtfToDocx()
        ConvertDocxToRtf()
    End Sub
    ''' <summary>
    ''' Load an existing RTF document and save it as new DOCX.
    ''' </summary>
    Public Sub ConvertRtfToDocx()
        Dim rtfFile As String = "..\..\..\..\..\Testing Files\example.rtf"
        Dim docxFile As String = "..\..\..\..\..\Testing Files\Result.docx"

        'DocumentCore.Serial = "put your serial here";
        Dim rtf As DocumentCore = DocumentCore.Load(rtfFile)
        rtf.Save(docxFile)

        ' Open resulting DOCX.            
        System.Diagnostics.Process.Start(docxFile)
    End Sub

    ''' <summary>
    ''' Load an existing DOCX document and save it as new RTF.
    ''' </summary>
    Public Sub ConvertDocxToRtf()
        Dim docxFile As String = "..\..\..\..\..\Testing Files\example.docx"
        Dim rtfFile As String = "..\..\..\..\..\Testing Files\Result.rtf"

        'DocumentCore.Serial = "put your serial here";
        Dim docx As DocumentCore = DocumentCore.Load(docxFile)
        docx.Save(rtfFile, New RtfSaveOptions())

        ' Open resulting RTF.            
        System.Diagnostics.Process.Start(rtfFile)
    End Sub
End Module
