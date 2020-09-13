Imports System
Imports System.IO
Imports System.Text
Imports SautinSoft.Document

Module Sample

    Sub Main()
        GetContent()
    End Sub

    ''' <summary>
    ''' Get content example.
    ''' </summary>
    Public Sub GetContent()
        ' Path to a input document.
        Dim loadPath As String = "..\..\..\..\..\Testing Files\example.docx"

        'DocumentCore.Serial = "put your serial here";
        Dim docx As DocumentCore = DocumentCore.Load(loadPath)

        Dim sb As New StringBuilder()

        ' Get content from each paragraph.
        For Each paragraph As Paragraph In docx.GetChildElements(True, ElementType.Paragraph)
            sb.AppendFormat("Paragraph: {0}", paragraph.Content.ToString())
            sb.AppendLine()
        Next paragraph

        ' Get content from each bold run.
        For Each run As Run In docx.GetChildElements(True, ElementType.Run)
            If run.CharacterFormat.Bold Then
                sb.AppendFormat("Bold run: {0}", run.Content.ToString())
                sb.AppendLine()
            End If
        Next run

        Console.WriteLine(sb.ToString())
        Console.ReadLine()
    End Sub

End Module
