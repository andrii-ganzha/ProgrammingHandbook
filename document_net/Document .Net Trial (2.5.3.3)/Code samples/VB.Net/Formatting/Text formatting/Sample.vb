Imports System
Imports System.IO
Imports SautinSoft.Document

Module Sample

    Sub Main()
        TextFormatting()
    End Sub

    ''' <summary>
    ''' This sample shows how to specify a text formatting for a document.
    ''' </summary>
    Public Sub TextFormatting()
        Dim docxPath As String = "..\..\..\..\..\Testing Files\Text Formatting.docx"

        ' Let's create a simple DOCX document.
        Dim docx As New DocumentCore()
        'DocumentCore.Serial = "put your serial here";


        docx.Content.Start.Insert("This is sample document" & ControlChars.Lf, New CharacterFormat() With {
            .FontName = "Calibri",
            .Size = 16.0,
            .FontColor = Color.Orange,
            .Bold = True
        })
        TryCast(docx.Sections(0).Blocks(0), Paragraph).ParagraphFormat.Alignment = HorizontalAlignment.Center


        docx.Content.End.Insert("Bold", New CharacterFormat() With {
            .Bold = True,
            .FontName = "Calibri",
            .Size = 11.0
        })
        docx.Content.End.Insert(" italic ", New CharacterFormat() With {
            .Italic = True,
            .FontName = "Calibri",
            .Size = 11.0
        })
        docx.Content.End.Insert("underline", New CharacterFormat() With {
            .UnderlineStyle = UnderlineType.Single,
            .FontName = "Calibri",
            .Size = 11.0
        })
        docx.Content.End.Insert(" ", New CharacterFormat() With {
            .Bold = True,
            .FontName = "Calibri",
            .Size = 11.0
        })
        docx.Content.End.Insert("strikethrough", New CharacterFormat() With {
            .Strikethrough = True,
            .FontName = "Calibri",
            .Size = 11.0
        })


        Dim p As New Paragraph(docx)
        p.ParagraphFormat.Alignment = HorizontalAlignment.Right
        docx.Sections(0).Blocks.Add(p)
        p.Content.End.Insert("This paragraph is aligned by right.", New CharacterFormat() With {
            .Size = 25,
            .FontColor = New Color(&H4F81BD),
            .Bold = True
        })
        Dim lBr As New SpecialCharacter(docx, SpecialCharacterType.LineBreak)
        p.Content.End.Insert(lBr.Content)
        p.Content.End.Insert("Second line.", New CharacterFormat() With {
            .Size = 25,
            .FontColor = Color.DarkGreen,
            .Bold = True
        })

        ' Save DOCX to a file
        docx.Save(docxPath)

        ' Open the result for demonstation purposes.
        System.Diagnostics.Process.Start(docxPath)
    End Sub

End Module
