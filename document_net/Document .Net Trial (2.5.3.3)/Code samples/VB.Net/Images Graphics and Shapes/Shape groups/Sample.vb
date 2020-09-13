Imports System
Imports System.IO
Imports SautinSoft.Document
Imports SautinSoft.Document.Drawing

Module Sample

    Sub Main()
        ShapeGroups()
    End Sub

    ''' <summary>
    ''' This sample shows how to work with shape groups.
    ''' </summary>
    Public Sub ShapeGroups()
        Dim pictPath As String = "..\..\..\..\..\Testing Files\image1.jpg"
        Dim docxPath As String = "..\..\..\..\..\Testing Files\Shape Groups.docx"

        ' Let's create document.
        Dim document As New DocumentCore()
        'DocumentCore.Serial = "put your serial here";

        ' Create floating layout.
        Dim hp As New HorizontalPosition(HorizontalPositionType.Center, HorizontalPositionAnchor.Page)
        Dim vp As New VerticalPosition(VerticalPositionType.Center, VerticalPositionAnchor.Page)
        Dim fl As New FloatingLayout(hp, vp, New Size(300, 300))

        ' Create group.
        Dim group As New ShapeGroup(document)

        ' For top-level shapes, pictures or groups setting values ​​of Size or Offset properties 
        ' (see below) has no effect. 
        ' group.Offset = new Point(0, 0);
        ' group.Size = new Size(300, 300);

        ' Specify the size dimensions of the child extents rectangle.
        group.ChildSize = New Size(100, 100)

        ' Create a child shape #1 with preset geometry.
        Dim shape As New Shape(document)

        ' Specify shape's size and offset relative to group's ChildSize (100x100).
        shape.Offset = New Point(0, 0)
        shape.Size = New Size(50, 50)

        ' Specify outline and fill.
        shape.Outline.Fill.SetSolid(Color.Blue)
        shape.Outline.Width = 2
        shape.Fill.SetSolid(Color.Red)

        ' Shape will be rectangle.
        shape.Geometry.SetPreset(Figure.Rectangle)

        ' Create a child shape #2 with picture.
        Dim picture As New Picture(document, pictPath)

        ' Specify picture's size and offset relative to group's ChildSize (100x100).
        picture.Offset = New Point(50, 50)
        picture.Size = New Size(50, 50)

        ' Specify picture fill mode.
        picture.ImageData.FillMode = PictureFillMode.Stretch

        ' Add child shapes into group.
        group.ChildShapes.Add(shape)
        group.ChildShapes.Add(picture)

        ' Add drawing element to document.
        document.Content.End.Insert((New DrawingElement(document, fl, group)).Content)

        ' Save DOCX to a file
        document.Save(docxPath)

        ' Open the result for demonstation purposes.
        System.Diagnostics.Process.Start(docxPath)
    End Sub

End Module
