Imports System
Imports System.IO
Imports SautinSoft.Document

Module Sample

    Sub Main()
        PageNumbering()
    End Sub

    ''' <summary>
    ''' How to add a page numbering in a document.
    ''' </summary>
    Public Sub PageNumbering()
        Dim docxPath As String = "..\..\..\..\..\Testing Files\PageNumbering.docx"

        ' Let's create a DOCX document with multiple pages.
        Dim docx As New DocumentCore()
        'DocumentCore.Serial = "put your serial here";


        Dim pagesText() As String = {"One", "Two", "Three", "Four", "Five"}
        Dim r As New Random()

        Dim s As New Section(docx)
        docx.Sections.Add(s)

        Dim f As New HeaderFooter(docx, HeaderFooterType.FooterDefault)
        Dim par As New Paragraph(docx)
        par.ParagraphFormat.Alignment = HorizontalAlignment.Center
        Dim cf As New CharacterFormat() With {
            .FontName = "Arial",
            .Size = 12.0
        }

        par.Content.Start.Insert("Page ", cf.Clone())
        Dim fPage As New Field(docx, FieldType.Page)
        fPage.CharacterFormat = cf.Clone()
        par.Content.End.Insert(fPage.Content)
        par.Content.End.Insert(" of ", cf.Clone())
        Dim fPages As New Field(docx, FieldType.NumPages)
        fPages.CharacterFormat = cf.Clone()
        par.Content.End.Insert(fPages.Content)

        f.Blocks.Add(par)
        s.HeadersFooters.Add(f)

        For Each text As String In pagesText
            Dim p As New Paragraph(docx)
            p.ParagraphFormat.Alignment = HorizontalAlignment.Center
            s.Blocks.Add(p)

            Dim color As Integer = r.Next(&H0, &HFFFFFF)
            p.Content.Start.Insert(text, New CharacterFormat() With {
                .FontName = "Arial",
                .Size = 72.0,
                .FontColor = New Color(color)
            })
            p.Content.End.Insert((New SpecialCharacter(docx, SpecialCharacterType.PageBreak)).Content)
        Next text

        ' Save DOCX to a file
        docx.Save(docxPath)

        ' Open the result for demonstation purposes.
        System.Diagnostics.Process.Start(docxPath)
    End Sub

End Module
