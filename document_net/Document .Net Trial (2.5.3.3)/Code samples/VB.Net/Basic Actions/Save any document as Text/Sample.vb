Imports System
Imports System.IO
Imports SautinSoft.Document

Module Sample

    Sub Main()
        SaveAsText()
    End Sub

    ''' <summary>
    ''' Load an existing .docx file and save it as new Text document.
    ''' </summary>
    Public Sub SaveAsText()
        Dim docxFile As String = "..\..\..\..\..\Testing Files\example.docx"
        Dim textFile As String = Path.ChangeExtension(docxFile, ".txt")

        'DocumentCore.Serial = "put your serial here";
        Dim docx As DocumentCore = DocumentCore.Load(docxFile)

        Dim [to] As New TxtSaveOptions()
        [to].Encoding = System.Text.Encoding.UTF8

        docx.Save(textFile, [to])

        ' Open the result for demonstation purposes.
        System.Diagnostics.Process.Start(textFile)
    End Sub



End Module
