Imports System
Imports System.IO
Imports SautinSoft.Document

Module Sample

    Sub Main()
        Bookmarks()
    End Sub

    ''' <summary>
    ''' This sample shows how to work with bookmarks.
    ''' </summary>
    Public Sub Bookmarks()
        Dim docxPath As String = "..\..\..\..\..\Testing Files\Bookmarks.docx"

        ' Let's create document.
        Dim document As New DocumentCore()
        'DocumentCore.Serial = "put your serial here";

        ' Add text bounded by BookmarkStart and BookmarkEnd.
        document.Sections.Add(New Section(document, New Paragraph(document, New Run(document, "Text before bookmark. "), New BookmarkStart(document, "My bookmark"), New Run(document, "Text inside bookmark."), New BookmarkEnd(document, "My bookmark"), New Run(document, " Text after bookmark."))))

        ' Modify text inside bookmark.
        document.Bookmarks("My bookmark").GetContent(False).Replace("New text.")

        ' Save DOCX to a file
        document.Save(docxPath)

        ' Open the result for demonstation purposes.
        System.Diagnostics.Process.Start(docxPath)
    End Sub


End Module
