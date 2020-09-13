Imports System
Imports System.IO
Imports SautinSoft.Document
Imports System.Linq

Module Sample

    Sub Main()
        FindAndReplace()
    End Sub

    ''' <summary>
    ''' Creates a simple Docx document.
    ''' </summary>
    Public Sub FindAndReplace()
        ' Path to and input document
        Dim inputPath As String = "..\..\..\..\..\Testing Files\table.docx"
        Dim outPath As String = Path.ChangeExtension(inputPath, ".new.docx")

        'DocumentCore.Serial = "put your serial here";

        ' Open document.
        Dim docx As DocumentCore = DocumentCore.Load(inputPath)

        ' Replace "row" to "R"
        For Each text As ContentRange In docx.Content.Find("row").Reverse()
            text.Replace("R")
        Next text

        ' Make "col" Green, Bold.            
        For Each text As ContentRange In docx.Content.Find("col").Reverse()
            Dim cf As CharacterFormat = (TryCast(text.Start.Parent, Run)).CharacterFormat
            cf.Bold = True
            cf.FontColor = Color.Green
        Next text

        ' Save DOCX to a file
        docx.Save(outPath)

        ' Open the result for demonstation purposes.
        System.Diagnostics.Process.Start(inputPath)
        System.Diagnostics.Process.Start(outPath)
    End Sub


End Module
