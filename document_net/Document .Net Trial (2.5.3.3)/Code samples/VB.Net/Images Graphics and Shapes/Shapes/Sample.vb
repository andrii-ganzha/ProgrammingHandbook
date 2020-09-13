Imports System
Imports System.IO
Imports SautinSoft.Document
Imports SautinSoft.Document.Drawing

Module Sample

    Sub Main()
        Shapes()
    End Sub

    ''' <summary>
    ''' This sample shows how to work with shapes.
    ''' </summary>
    Public Sub Shapes()
        Dim pictPath As String = "..\..\..\..\..\Testing Files\image1.jpg"
        Dim docxPath As String = "..\..\..\..\..\Testing Files\Shapes.docx"

        ' Let's create document.
        Dim document As New DocumentCore()
        'DocumentCore.Serial = "put your serial here";

        ' Create floating layout for shape1.
        Dim fl1 As New FloatingLayout(New HorizontalPosition(HorizontalPositionType.Left, HorizontalPositionAnchor.Column), New VerticalPosition(VerticalPositionType.Center, VerticalPositionAnchor.Page), New Size(100, 100))

        ' Create shape1 with preset geometry.
        Dim shape1 As New Shape(document)

        ' For top-level shapes, pictures or groups setting values ​​of Size or Offset properties 
        ' (see below) has no effect. 
        ' shape1.Offset = new Point(0, 0);
        ' shape1.Size = new Size(100, 100);

        ' Specify outline and fill.
        shape1.Outline.Fill.SetSolid(Color.Red)
        shape1.Outline.Width = 2
        shape1.Fill.SetSolid(Color.White)

        ' Specify a figure.
        shape1.Geometry.SetPreset(Figure.SmileyFace)


        ' Create floating layout for shape2.
        Dim fl2 As New FloatingLayout(New HorizontalPosition(HorizontalPositionType.Center, HorizontalPositionAnchor.Column), New VerticalPosition(VerticalPositionType.Center, VerticalPositionAnchor.Page), New Size(100, 100))

        ' Create shape2 with custom geometry path (using points array).
        Dim shape2 As New Shape(document)

        ' Specify outline and picture fill.
        shape2.Outline.Fill.SetSolid(Color.Green)
        shape2.Outline.Width = 2
        shape2.Fill.SetPicture(pictPath)

        ' Specify the maximum X and Y coordinates that should be used
        ' for within the path coordinate system.
        Dim size As New Size(1, 1)

        ' Specify the path points (draw a circle of 10 points).
        Dim points(9) As Point
        Dim a As Double = 0
        For i As Integer = 0 To 9
            points(i) = New Point(0.5 + Math.Sin(a) * 0.5, 0.5 + Math.Cos(a) * 0.5)
            a += 2 * Math.PI / 10
        Next i

        ' Create and add new custom path from specified points array.
        shape2.Geometry.SetCustom().AddPath(size, points, True)


        ' Create floating layout for shape3.
        Dim fl3 As New FloatingLayout(New HorizontalPosition(HorizontalPositionType.Right, HorizontalPositionAnchor.Column), New VerticalPosition(VerticalPositionType.Center, VerticalPositionAnchor.Page), New Size(100, 100))

        ' Create shape3 with custom geometry path (using path elements).
        Dim shape3 As New Shape(document)

        ' Specify outline and fill.
        shape3.Outline.Fill.SetSolid(Color.Blue)
        shape3.Outline.Width = 2
        shape3.Fill.SetSolid(Color.Yellow)

        ' Create and add new custom path.
        Dim path As CustomPath = shape3.Geometry.SetCustom().AddPath(New Size(1, 1))

        ' Specify path elements.
        path.MoveTo(New Point(0, 0))
        path.AddLine(New Point(0, 1))
        path.AddLine(New Point(1, 1))
        path.AddLine(New Point(1, 0))
        path.ClosePath()


        ' Add drawing elements to document.
        document.Content.End.Insert((New DrawingElement(document, fl1, shape1)).Content)
        document.Content.End.Insert((New DrawingElement(document, fl2, shape2)).Content)
        document.Content.End.Insert((New DrawingElement(document, fl3, shape3)).Content)


        ' Save DOCX to a file
        document.Save(docxPath)

        ' Open the result for demonstation purposes.
        System.Diagnostics.Process.Start(docxPath)
    End Sub

End Module
