Imports System
Imports System.IO
Imports SautinSoft.Document

Module Sample

    Sub Main()
        TextColumns()
    End Sub

    ''' <summary>
    ''' Working with text columns.
    ''' </summary>
    Public Sub TextColumns()
        Dim docxPath As String = "..\..\..\..\..\Testing Files\TextColumns.docx"

        ' Let's create DOCX document with 4 columns.
        Dim docx As New DocumentCore()
        'DocumentCore.Serial = "put your serial here";

        ' Add new section
        Dim s As New Section(docx)
        docx.Sections.Add(s)

        s.PageSetup.PageMargins = New PageMargins() With {
            .Top = LengthUnitConverter.Convert(5, LengthUnit.Millimeter, LengthUnit.Point),
            .Right = LengthUnitConverter.Convert(5, LengthUnit.Millimeter, LengthUnit.Point),
            .Bottom = LengthUnitConverter.Convert(5, LengthUnit.Millimeter, LengthUnit.Point),
            .Left = LengthUnitConverter.Convert(5, LengthUnit.Millimeter, LengthUnit.Point)
        }

        s.PageSetup.TextColumns = New TextColumnCollection(4)
        s.PageSetup.TextColumns.EvenlySpaced = False
        s.PageSetup.TextColumns(0).Width = LengthUnitConverter.Convert(60, LengthUnit.Millimeter, LengthUnit.Point)
        s.PageSetup.TextColumns(1).Width = LengthUnitConverter.Convert(20, LengthUnit.Millimeter, LengthUnit.Point)
        s.PageSetup.TextColumns(2).Width = LengthUnitConverter.Convert(60, LengthUnit.Millimeter, LengthUnit.Point)
        s.PageSetup.TextColumns(3).Width = LengthUnitConverter.Convert(20, LengthUnit.Millimeter, LengthUnit.Point)


        ' Fill our columns by any text.
        Dim text As String = "Shrek and Donkey arrive at Farquaad's palace in Duloc, where they end up in a tournament. The winner gets the ""privilege"" of rescuing Fiona so that Farquaad may marry her. "

        For i As Integer = 0 To 19
            s.Content.End.Insert(text, New CharacterFormat() With {
                .FontName = "Arial",
                .Size = 12
            })
        Next i

        TryCast(s.Blocks(0), Paragraph).ParagraphFormat.Alignment = HorizontalAlignment.Justify

        ' Save DOCX to a file
        docx.Save(docxPath)

        ' Open the result for demonstation purposes.
        System.Diagnostics.Process.Start(docxPath)
    End Sub

End Module
