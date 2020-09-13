Imports System
Imports System.IO
Imports System.Collections.Generic
Imports SautinSoft.Document
Imports SautinSoft.Document.Drawing

Module Sample

    Sub Main()
        ExtractImagesFromDocument()
    End Sub

    Public Sub ExtractImagesFromDocument()
        ' Path a .docx, .rtf or .pdf file with images.
        Dim filePath As String = "..\..\..\..\..\Testing Files\example.pdf"

        Dim imageDir As New DirectoryInfo(Path.GetDirectoryName(filePath))
        Dim imageTemplate As String = "Picture"
        Dim imageInventory As New List(Of ImageData)()

        Dim dc As DocumentCore = DocumentCore.Load(filePath)

        ' Extract all images from document
        For Each pict As Picture In GetPictures(dc.Document)
            ' Let's avoid the adding of duplicates.
            If imageInventory.Exists((Function(img) (img.ImageStream.Length = pict.ImageData.ImageStream.Length))) = False Then
                imageInventory.Add(pict.ImageData)
            End If
        Next pict
        ' Save all images
        For i As Integer = 0 To imageInventory.Count - 1
            Dim imagePath As String = Path.Combine(imageDir.FullName, String.Format("{0}{1}.{2}", imageTemplate, i + 1, imageInventory(i).Format.ToString().ToLower()))
            File.WriteAllBytes(imagePath, imageInventory(i).ImageStream.ToArray())
            System.Diagnostics.Process.Start(imagePath)
        Next i
    End Sub

    Iterator Function GetPictures(ByVal document As DocumentCore) As IEnumerable(Of Picture)
        For Each e As DrawingElement In document.GetChildElements(True, ElementType.DrawingElement)
            For Each pict As Picture In GetPicturesRecursive(e.Shape)
                Yield pict
            Next pict
        Next e
    End Function
    Iterator Function GetPicturesRecursive(ByVal sb As ShapeBase) As IEnumerable(Of Picture)
        If TypeOf sb Is Picture Then
            Yield CType(sb, Picture)
            Return
        Else
            If TypeOf sb Is ShapeGroup Then
                For Each shape As ShapeBase In (TryCast(sb, ShapeGroup)).ChildShapes
                    For Each pict As Picture In GetPicturesRecursive(shape)
                        Yield pict
                    Next pict
                Next shape
            End If
        End If
    End Function
End Module
