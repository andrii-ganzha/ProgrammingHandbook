Imports System
Imports System.IO
Imports SautinSoft.Document

Module Sample

    Sub Main()
        DeleteContent()
    End Sub

    ''' <summary>
    ''' Open a document and delete some content.
    ''' </summary>
    Public Sub DeleteContent()
        ' Path to a input document.
        Dim loadPath As String = "..\..\..\..\..\Testing Files\example.docx"
        Dim savePath As String = Path.ChangeExtension(loadPath, ".new.docx")

        'DocumentCore.Serial = "put your serial here";
        Dim docx As DocumentCore = DocumentCore.Load(loadPath)

        ' Get all paragraphs from 1st section.
        Dim blocks = docx.Sections(0).Blocks

        ' Remove the text "rich text document" from the 3rd paragraph
        blocks = docx.Sections(0).Blocks

        ' Get 3rd paragraph
        Dim p3 As Paragraph = CType(blocks(2), Paragraph)

        p3.Content.FindFirst("rich text document").Delete()

        ' Remove 1st paragraph
        blocks(0).Content.Delete()

        docx.Save(savePath)

        ' Show original and resulted documents.
        System.Diagnostics.Process.Start(savePath)
        System.Diagnostics.Process.Start(loadPath)

    End Sub


End Module
