Imports System
Imports System.IO
Imports System.Linq
Imports SautinSoft.Document
Imports SautinSoft.Document.Tables

Module Sample

    Sub Main()
        CreatePlainTable()
    End Sub

    ''' <summary>
    ''' How to create a plain table and insert it inside a document.
    ''' </summary>
    Public Sub CreatePlainTable()
        Dim docxPath As String = "..\..\..\..\..\Testing Files\Tables.docx"

        ' Let's create a simple DOCX document.
        Dim docx As New DocumentCore()
        'DocumentCore.Serial = "put your serial here";

        ' Add new section
        Dim s As New Section(docx)
        docx.Sections.Add(s)


        ' Let's create a plain table: 2x3, with 100 mm width.
        Dim t As New Table(docx)
        Dim twidth As Double = LengthUnitConverter.Convert(100, LengthUnit.Millimeter, LengthUnit.Point)
        t.TableFormat.PreferredWidth = New TableWidth(twidth, TableWidthUnit.Point)
        t.TableFormat.Alignment = HorizontalAlignment.Center


        Dim counter As Integer = 0

        ' Add 2 rows.
        For r As Integer = 0 To 1
            Dim row As New TableRow(docx)

            ' Add 3 columns
            For c As Integer = 0 To 2
                Dim cell As New TableCell(docx)

                ' Set some cell formatting
                cell.CellFormat.Borders.SetBorders(MultipleBorderTypes.Outside, BorderStyle.Single, Color.Black, 1.0)

                If counter Mod 2 = 1 Then
                    cell.CellFormat.BackgroundColor = New Color(&HCCCCCC)
                End If

                row.Cells.Add(cell)

                ' Let's add some text into each column.
                Dim p As New Paragraph(docx)
                p.ParagraphFormat.Alignment = HorizontalAlignment.Center
                p.ParagraphFormat.SpaceBefore = LengthUnitConverter.Convert(3, LengthUnit.Millimeter, LengthUnit.Point)
                p.ParagraphFormat.SpaceAfter = LengthUnitConverter.Convert(3, LengthUnit.Millimeter, LengthUnit.Point)


                p.Content.Start.Insert(String.Format("{0}", ChrW(counter + AscW("A"c))), New CharacterFormat() With {
                    .FontName = "Arial",
                    .FontColor = New Color(&H3399FF),
                    .Size = 12.0
                })
                cell.Blocks.Add(p)
                counter += 1
            Next c
            t.Rows.Add(row)
        Next r


        ' Add this table to the current section.
        s.Blocks.Add(t)

        ' Save DOCX to a file
        docx.Save(docxPath)

        ' Open the result for demonstation purposes.
        System.Diagnostics.Process.Start(docxPath)
    End Sub

End Module
