Imports System
Imports System.IO
Imports System.Linq
Imports SautinSoft.Document
Imports SautinSoft.Document.Tables

Module Sample

    Sub Main()
        ModifyTable()
    End Sub

    ''' <summary>
    ''' How to modify an existing table in a document.
    ''' </summary>
    Public Sub ModifyTable()
        Dim currTablePath As String = Path.GetFullPath("..\..\..\..\..\Testing Files\table.docx")
        Dim newTablePath As String = Path.Combine(Path.GetDirectoryName(currTablePath), "Modified table.docx")

        'DocumentCore.Serial = "put your serial here";

        ' Let's load DOCX document
        Dim docx As DocumentCore = DocumentCore.Load(currTablePath)

        ' Find a first table
        Dim t As Table = CType(docx.GetChildElements(True, ElementType.Table).First(), Table)

        ' Set dashed borders and yellow background for all cells.
        For r As Integer = 0 To t.Rows.Count - 1
            Dim c As Integer = 0
            Do While c < t.Rows(r).Cells.Count
                Dim cell As TableCell = t.Rows(r).Cells(c)
                cell.CellFormat.Borders.SetBorders(MultipleBorderTypes.Outside, BorderStyle.Dashed, Color.Black, 1)
                cell.CellFormat.BackgroundColor = New Color(&HFFCC00)
                c += 1
            Loop
        Next r



        ' Save DOCX to a file
        docx.Save(newTablePath)

        ' Show the source and the resulting document.
        System.Diagnostics.Process.Start(currTablePath)
        System.Diagnostics.Process.Start(newTablePath)
    End Sub

End Module
