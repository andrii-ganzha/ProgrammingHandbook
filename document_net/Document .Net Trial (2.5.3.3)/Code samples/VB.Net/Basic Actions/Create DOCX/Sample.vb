Imports System
Imports System.IO
Imports SautinSoft.Document

Module Sample

    Sub Main()
        CreateDocx()
    End Sub

    ''' <summary>
    ''' Creates a simple Docx document.
    ''' </summary>
    Public Sub CreateDocx()
        ' Set a path to our docx file.
        Dim docxPath As String = "..\..\..\..\..\Testing Files\Result.docx"

        ' Let's create a simple DOCX document.
        Dim docx As New DocumentCore()
        'DocumentCore.Serial = "put your serial here";

        ' Add new section.
        Dim section As New Section(docx)
        docx.Sections.Add(section)

        ' Let's set page size A4.
        section.PageSetup.PaperType = PaperType.A4

        ' Add two paragraphs using different ways:

        ' Way 1: Add 1st paragraph.
        section.Blocks.Add(New Paragraph(docx, "This is a first line in 1st paragraph!"))
        Dim par1 As Paragraph = TryCast(section.Blocks(0), Paragraph)
        par1.ParagraphFormat.Alignment = HorizontalAlignment.Center

        ' Let's add a second line.
        par1.Inlines.Add(New SpecialCharacter(docx, SpecialCharacterType.LineBreak))
        par1.Inlines.Add(New Run(docx, "Let's type a second line."))

        ' Let's change font name, size and color.
        Dim cf As New CharacterFormat() With {
            .FontName = "Verdana",
            .Size = 16,
            .FontColor = Color.Orange
        }
        For Each inline As Inline In par1.Inlines
            If TypeOf inline Is Run Then
                TryCast(inline, Run).CharacterFormat = cf.Clone()
            End If
        Next inline

        ' Way 2 (easy): Add 2nd paragarph using another way.
        docx.Content.End.Insert(ControlChars.Lf & "This is a first line in 2nd paragraph.", New CharacterFormat() With {
            .Size = 25,
            .FontColor = Color.Blue,
            .Bold = True
        })
        Dim lBr As New SpecialCharacter(docx, SpecialCharacterType.LineBreak)
        docx.Content.End.Insert(lBr.Content)
        docx.Content.End.Insert("This is a second line.", New CharacterFormat() With {
            .Size = 20,
            .FontColor = Color.DarkGreen,
            .UnderlineStyle = UnderlineType.Single
        })

        ' Save DOCX to a file
        docx.Save(docxPath)

        ' Open the result for demonstation purposes.
        System.Diagnostics.Process.Start(docxPath)
    End Sub

End Module
