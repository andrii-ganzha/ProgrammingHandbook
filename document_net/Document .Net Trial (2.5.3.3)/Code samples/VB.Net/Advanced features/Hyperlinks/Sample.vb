Imports System
Imports System.IO
Imports SautinSoft.Document

Module Sample

    Sub Main()
        AddHyperlink()
    End Sub

    ''' <summary>
    ''' How to add a hyperlink into a document.
    ''' </summary>
    Public Sub AddHyperlink()
        Dim docxPath As String = "..\..\..\..\..\Testing Files\Hyperlinks.docx"

        ' Let's create a simple DOCX document with a hyperlink.
        Dim docx As New DocumentCore()
        'DocumentCore.Serial = "put your serial here";

        Dim hpl As New Hyperlink(docx, "http://www.zoo.org", "Welcome to Zoo!")
        TryCast(hpl.DisplayInlines(0), Run).CharacterFormat = New CharacterFormat() With {
            .Size = 16,
            .FontColor = New Color(&HCCFF),
            .UnderlineStyle = UnderlineType.Single
        }
        hpl.ScreenTip = "Welcome to WoodLand Zoo!"

        Dim p As New Paragraph(docx)
        p.Inlines.Add(hpl)
        p.ParagraphFormat.Alignment = HorizontalAlignment.Center

        docx.Content.Start.Insert(p.Content)

        ' Save DOCX to a file
        docx.Save(docxPath)

        ' Open the result for demonstation purposes.
        System.Diagnostics.Process.Start(docxPath)
    End Sub

End Module
