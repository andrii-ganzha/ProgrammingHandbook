Imports System
Imports System.IO
Imports SautinSoft.Document

Module Sample

    Sub Main()
        ParagraphFormatting()
    End Sub

    ''' <summary>
    ''' This sample shows how to specify a paragraph formatting for a document.
    ''' </summary>
    Public Sub ParagraphFormatting()
        Dim docxPath As String = "..\..\..\..\..\Testing Files\Paragraph Formatting.docx"

        ' Let's create a simple DOCX document.
        Dim docx As New DocumentCore()
        'DocumentCore.Serial = "put your serial here";

        ' Add new section
        Dim s As New Section(docx)
        docx.Sections.Add(s)


        ' Paragraph 1
        Dim p As New Paragraph(docx)
        docx.Sections(0).Blocks.Add(p)

        p.ParagraphFormat.Alignment = HorizontalAlignment.Left
        p.ParagraphFormat.SpaceAfter = 20.0

        p.Content.End.Insert("Let's add a first paragraph.", New CharacterFormat() With {
            .Size = 20,
            .FontColor = New Color(&H999999)
        })

        ' Paragraph 2.
        docx.Content.End.Insert(ControlChars.Lf & "This is a second paragraph.", New CharacterFormat() With {
            .FontName = "Calibri",
            .Size = 16.0,
            .FontColor = Color.Black,
            .Bold = True
        })
        TryCast(docx.Sections(0).Blocks(0), Paragraph).ParagraphFormat.Alignment = HorizontalAlignment.Center

        ' Multiple paragraphs
        For i As Integer = 0 To 9
            docx.Content.End.Insert(String.Format(ControlChars.Lf & "Paragraph {0}.", i + 1), New CharacterFormat() With {
                .FontName = "Arial",
                .Size = 12.0
            })
        Next i

        ' Save DOCX to a file
        docx.Save(docxPath)

        ' Open the result for demonstation purposes.
        System.Diagnostics.Process.Start(docxPath)
    End Sub

End Module
