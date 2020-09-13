Imports System
Imports System.IO
Imports System.Collections.Generic
Imports SautinSoft.Document
Imports SautinSoft.Document.Drawing

Module Sample

    Sub Main()
        AddPictures()
    End Sub

    ''' <summary>
    ''' How to add pictures into a document.
    ''' </summary>
    Public Sub AddPictures()
        Dim docxPath As String = "..\..\..\..\..\Testing Files\Pictures.docx"
        Dim pictPath As String = "..\..\..\..\..\Testing Files\image1.jpg"

        ' Let's create a simple DOCX document.
        Dim docx As New DocumentCore()
        'DocumentCore.Serial = "put your serial here";

        ' Add new section
        Dim s As New Section(docx)
        docx.Sections.Add(s)

        ' Let's add a some text.
        Dim p As New Paragraph(docx)
        s.Blocks.Add(p)
        p.Content.End.Insert("Once upon a time, in a far away swamp, there lived an ogre named Shrek...", New CharacterFormat() With {
            .FontName = "Calibri",
            .Size = 16.0,
            .FontColor = Color.Black
        })

        ' Add picture after paragraph. 
        Dim pict As New Picture(docx, pictPath)
        Dim il As New InlineLayout(New Size(100, 100))
        Dim de As New DrawingElement(docx, il, pict)

        Dim pWithImg As New Paragraph(docx)
        pWithImg.ParagraphFormat.Alignment = HorizontalAlignment.Center
        pWithImg.Content.Start.Insert(de.Content)
        s.Blocks.Add(pWithImg)

        ' Add another text.
        s.Content.End.Insert(ControlChars.Lf & "Shrek and Donkey arrive at Farquaad's palace in Duloc, where they end up in a tournament.", New CharacterFormat() With {
            .FontName = "Calibri",
            .Size = 16.0,
            .FontColor = Color.Black
        })


        ' Add the image at once after text.
        s.Content.End.Insert(de.Content)


        ' Add the image as a shape with coordinates.
        Dim pict1 As New Picture(docx, pictPath)

        Dim fl As New FloatingLayout(New HorizontalPosition(150, LengthUnit.Millimeter, HorizontalPositionAnchor.Page), New VerticalPosition(90, LengthUnit.Millimeter, VerticalPositionAnchor.TopMargin), New Size(70, 70))
        Dim de1 As New DrawingElement(docx, fl, pict1)
        s.Content.End.Insert(de1.Content)

        ' Save DOCX to a file
        docx.Save(docxPath)

        ' Open the result for demonstation purposes.
        System.Diagnostics.Process.Start(docxPath)
    End Sub

End Module
