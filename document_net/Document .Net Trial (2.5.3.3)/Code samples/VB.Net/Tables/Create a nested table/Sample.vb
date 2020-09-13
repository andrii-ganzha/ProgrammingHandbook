Imports System
Imports System.IO
Imports System.Linq
Imports SautinSoft.Document
Imports SautinSoft.Document.Tables

Module Sample

    Sub Main()
        CreateNestedTable()
    End Sub

    ''' <summary>
    ''' How to create a nested table and add it inside a document.
    ''' </summary>
    Public Sub CreateNestedTable()
        Dim docxFilePath As String = "..\..\..\..\..\Testing Files\Tables.docx"

        ' Let's create a simple DOCX document.
        Dim docx As New DocumentCore()
        'DocumentCore.Serial = "put your serial here";

        ' Add new section
        Dim s As New Section(docx)
        docx.Sections.Add(s)


        ' Let's create table 1: 1x2, with 150 mm width.            
        Dim t1 As New Table(docx)
        Dim twidth As Double = LengthUnitConverter.Convert(150, LengthUnit.Millimeter, LengthUnit.Point)
        t1.TableFormat.PreferredWidth = New TableWidth(twidth, TableWidthUnit.Point)


        ' Add 1 rows.
        For r As Integer = 0 To 0
            Dim row As New TableRow(docx)

            ' Add 2 columns
            For c As Integer = 0 To 1
                Dim cell As New TableCell(docx)

                ' Set some cell formatting
                cell.CellFormat.Borders.SetBorders(MultipleBorderTypes.Outside, BorderStyle.Single, Color.Black, 1.0)
                cell.CellFormat.PreferredWidth = New TableWidth(twidth / 2, TableWidthUnit.Point)

                Dim padding As Double = LengthUnitConverter.Convert(3, LengthUnit.Millimeter, LengthUnit.Point)
                cell.CellFormat.Padding = New Padding(padding)

                row.Cells.Add(cell)
            Next c
            t1.Rows.Add(row)
        Next r

        ' Add this table to the current section.
        s.Blocks.Add(t1)

        ' Create nested table 3x3.
        Dim t2 As New Table(docx)
        twidth = LengthUnitConverter.Convert(75, LengthUnit.Millimeter, LengthUnit.Point)
        t2.TableFormat.PreferredWidth = New TableWidth(twidth, TableWidthUnit.Point)
        t2.TableFormat.Alignment = HorizontalAlignment.Center

        For r As Integer = 0 To 2
            Dim row As New TableRow(docx)

            ' Add 2 columns
            For c As Integer = 0 To 2
                Dim cell As New TableCell(docx)

                ' Set some cell formatting
                cell.CellFormat.Borders.SetBorders(MultipleBorderTypes.Outside, BorderStyle.Single, Color.Black, 1.0)
                row.Cells.Add(cell)

                ' Let's add some text into each column.
                Dim p As New Paragraph(docx)
                p.ParagraphFormat.Alignment = HorizontalAlignment.Center
                p.ParagraphFormat.SpaceBefore = LengthUnitConverter.Convert(3, LengthUnit.Millimeter, LengthUnit.Point)
                p.ParagraphFormat.SpaceAfter = LengthUnitConverter.Convert(3, LengthUnit.Millimeter, LengthUnit.Point)


                p.Content.Start.Insert(String.Format("row: {0} col: {1}", r + 1, c + 1, New CharacterFormat() With {
                    .FontName = "Arial",
                    .Size = 12.0
                }))
                cell.Blocks.Add(p)

            Next c
            t2.Rows.Add(row)
        Next r

        ' Insert table2 inside 2nd columns of table 1.
        t1.Rows(0).Cells(1).Blocks.Add(t2)

        ' Insert some text inside 1st column of table 1.
        Dim p2 As New Paragraph(docx)
        p2.ParagraphFormat.Alignment = HorizontalAlignment.Center
        p2.Content.Start.Insert("This is a 1st column of table 1")
        t1.Rows(0).Cells(0).Blocks.Add(p2)

        ' Save DOCX to a file
        docx.Save(docxFilePath)

        ' Open the result for demonstation purposes.
        System.Diagnostics.Process.Start(docxFilePath)
    End Sub

End Module
