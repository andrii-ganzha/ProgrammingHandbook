Imports System
Imports System.IO
Imports SautinSoft.Document
Imports SautinSoft.Document.Drawing

Module Sample

    Sub Main()
        HeadersAndFooters()
    End Sub

    ''' <summary>
    ''' How to add a header and footer into a document.
    ''' </summary>
    Public Sub HeadersAndFooters()
        Dim docxPath As String = "..\..\..\..\..\Testing Files\HeadersAndFooters.docx"

        ' Let's create a simple DOCX document.
        Dim docx As New DocumentCore()
        'DocumentCore.Serial = "put your serial here";

        ' Add new section
        Dim s As New Section(docx)
        docx.Sections.Add(s)

        ' Let's add a paragraph with text.
        Dim p As New Paragraph(docx)
        docx.Sections(0).Blocks.Add(p)

        p.ParagraphFormat.Alignment = HorizontalAlignment.Justify
        p.Content.Start.Insert("Once upon a time, in a far away swamp, there lived an ogre named Shrek whose precious " & "solitude is suddenly shattered by an invasion of annoying fairy tale characters...", New CharacterFormat() With {
            .Size = 12,
            .FontName = "Arial"
        })

        ' Add a header.
        Dim h As New HeaderFooter(docx, HeaderFooterType.HeaderDefault)
        h.Content.Start.Insert("Shrek and Donkey travel to the castle and split up to find Fiona.", New CharacterFormat() With {.Size = 14.0})
        s.HeadersFooters.Add(h)

        ' Add a footer with text and image.
        Dim f As New HeaderFooter(docx, HeaderFooterType.FooterDefault)
        Dim p1 As New Paragraph(docx)
        p1.Content.Start.Insert("Shrek story. ", New CharacterFormat() With {.Size = 14.0})
        p1.ParagraphFormat.Alignment = HorizontalAlignment.Center

        ' add image.
        Dim pictFilePath As String = Path.Combine(Path.GetDirectoryName(docxPath), "image1.jpg")
        Dim pict As New Picture(docx, pictFilePath)

        Dim il As New InlineLayout(New Size(30, 30))
        Dim de As New DrawingElement(docx, il, pict)
        p1.Content.End.Insert(de.Content)
        f.Blocks.Add(p1)
        s.HeadersFooters.Add(f)

        ' Save DOCX to a file
        docx.Save(docxPath)

        ' Open the result for demonstation purposes.
        System.Diagnostics.Process.Start(docxPath)
    End Sub

End Module
