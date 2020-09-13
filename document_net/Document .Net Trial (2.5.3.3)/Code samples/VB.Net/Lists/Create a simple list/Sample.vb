Imports System
Imports System.IO
Imports SautinSoft.Document

Module Sample

    Sub Main()
        CreateSimpleLists()
    End Sub

    ''' <summary>
    ''' How to create simple ordered and unordered lists.
    ''' </summary>
    Public Sub CreateSimpleLists()
        Dim docxPath As String = "..\..\..\..\..\Testing Files\Lists.docx"

        ' Let's create a simple DOCX document.
        Dim docx As New DocumentCore()
        'DocumentCore.Serial = "put your serial here";

        ' Add new section
        Dim s As New Section(docx)
        docx.Sections.Add(s)

        Dim myCollection() As String = {"One", "Two", "Three", "Four", "Five"}

        ' Create list style.
        Dim ordList As New ListStyle("Simple Numbers", ListTemplateType.NumberWithDot)
        For Each f As ListLevelFormat In ordList.ListLevelFormats
            f.CharacterFormat.Size = 14
        Next f
        docx.Styles.Add(ordList)

        ' Add caption
        docx.Content.Start.Insert("This is a simple ordered list:", New CharacterFormat() With {
            .Size = 14.0,
            .FontColor = Color.Black
        })


        ' Add the collection of paragraphs marked as ordered list.
        Dim lvl As Integer = 0
        For Each listText As String In myCollection
            Dim p As New Paragraph(docx)
            docx.Sections(0).Blocks.Add(p)

            p.Content.End.Insert(listText, New CharacterFormat() With {
                .Size = 14.0,
                .FontColor = Color.Black
            })
            p.ListFormat.Style = ordList
            p.ListFormat.ListLevelNumber = lvl
            p.ParagraphFormat.SpaceAfter = 0
        Next listText

        ' Add the collection of paragraphs marked as unordered list (bullets).

        ' Add caption
        docx.Content.End.Insert(ControlChars.Lf & ControlChars.Lf & "This is a simple bulleted list:", New CharacterFormat() With {
            .Size = 14.0,
            .FontColor = Color.Black
        })

        ' Create list style.
        Dim bulList As New ListStyle("Bullets", ListTemplateType.Bullet)
        docx.Styles.Add(bulList)

        lvl = 0
        For Each listText As String In myCollection
            Dim p As New Paragraph(docx)
            docx.Sections(0).Blocks.Add(p)

            p.Content.End.Insert(listText, New CharacterFormat() With {
                .Size = 14.0,
                .FontColor = Color.Black
            })
            p.ListFormat.Style = bulList
            p.ListFormat.ListLevelNumber = lvl
            p.ParagraphFormat.SpaceAfter = 0
        Next listText

        ' Save DOCX to a file
        docx.Save(docxPath)

        ' Open the result for demonstation purposes.
        System.Diagnostics.Process.Start(docxPath)
    End Sub

End Module
