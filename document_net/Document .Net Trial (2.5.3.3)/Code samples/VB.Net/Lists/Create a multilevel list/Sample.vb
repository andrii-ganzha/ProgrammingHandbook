Imports System
Imports System.IO
Imports SautinSoft.Document

Module Sample

    Sub Main()
        CreateMultilevelLists()
    End Sub


    ''' <summary>
    ''' How to create multilevel ordered and unordered lists.
    ''' </summary>
    Public Sub CreateMultilevelLists()
        Dim docxPath As String = "..\..\..\..\..\Testing Files\Multilvel Lists.docx"

        ' Let's create a simple DOCX document.
        Dim docx As New DocumentCore()
        'DocumentCore.Serial = "put your serial here";

        ' Add new section
        Dim s As New Section(docx)
        docx.Sections.Add(s)

        Dim myCollection() As String = {"One", "Two", "Three", "Four", "Five"}

        ' Create list style.
        Dim ls As New ListStyle("", ListTemplateType.NumberWithDot)
        docx.Styles.Add(ls)

        ' Add the collection of paragraphs marked as ordered list.
        Dim lvl As Integer = 0
        For Each listText As String In myCollection
            Dim p As New Paragraph(docx)
            docx.Sections(0).Blocks.Add(p)

            p.Content.End.Insert(listText, New CharacterFormat() With {
                .Size = 14.0,
                .FontColor = Color.Black
            })
            p.ListFormat.Style = ls
            p.ListFormat.ListLevelNumber = lvl
            lvl += 1
            p.ParagraphFormat.SpaceAfter = 0
        Next listText

        ' Add the collection of paragraphs marked as unordered list (bullets).
        ' Create list style.
        Dim ls1 As New ListStyle("", ListTemplateType.Bullet)
        docx.Styles.Add(ls1)

        lvl = 0
        For Each listText As String In myCollection
            Dim p As New Paragraph(docx)
            docx.Sections(0).Blocks.Add(p)

            p.Content.End.Insert(listText, New CharacterFormat() With {
                .Size = 14.0,
                .FontColor = Color.Black
            })
            p.ListFormat.Style = ls1
            p.ListFormat.ListLevelNumber = lvl
            lvl += 1
            p.ParagraphFormat.SpaceAfter = 0
        Next listText

        ' Save DOCX to a file
        docx.Save(docxPath)

        ' Open the result for demonstation purposes.
        System.Diagnostics.Process.Start(docxPath)
    End Sub


End Module
