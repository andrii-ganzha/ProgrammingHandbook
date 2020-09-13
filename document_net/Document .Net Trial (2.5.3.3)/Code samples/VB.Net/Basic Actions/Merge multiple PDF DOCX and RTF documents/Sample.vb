Imports System
Imports System.IO
Imports SautinSoft.Document
Imports System.Collections.Generic

Module Sample

    Sub Main()
        MergeMultipleDocuments()
    End Sub

    ''' <summary>
    ''' This sample shows how to merge multiple DOCX, RTF, PDF and Text files.
    ''' </summary>
    Public Sub MergeMultipleDocuments()
        ' Working directory
        Dim workingDir As String = Path.GetFullPath(Directory.GetCurrentDirectory() & "..\..\..\..\..\..\Testing Files")

        ' Path to our combined document.
        Dim singleDocxPath As String = Path.Combine(workingDir, "Single.docx")
        ' Remove it if our combined document is already exist.
        If File.Exists(singleDocxPath) Then
            File.Delete(singleDocxPath)
        End If




        Dim supportedFiles As New List(Of String)()

        ' Fill the collection 'supportedFiles' by *.docx, *.rtf, *.pdf and *.txt.
        For Each file As String In Directory.GetFiles(workingDir, "*.*")
            Dim ext As String = Path.GetExtension(file).ToLower()

            If ext = ".docx" OrElse ext = ".rtf" OrElse ext = ".pdf" OrElse ext = ".txt" Then
                supportedFiles.Add(file)
            End If
        Next file

        'DocumentCore.Serial = "put your serial here";

        ' Create single docx.
        Dim singleDocx As New DocumentCore()

        For Each file As String In supportedFiles
            Dim document As DocumentCore = DocumentCore.Load(file)

            Console.WriteLine("Adding: {0}...", Path.GetFileName(file))

            ' Create import session.
            Dim session As New ImportSession(document, singleDocx, StyleImportingMode.UseDestinationStyles)

            ' Loop through all sections in the source document.
            For Each sourceSection As Section In document.Sections
                ' Because we are copying a section from one document to another, 
                ' it is required to import the Section into the destination document.
                ' This adjusts any document-specific references to styles, bookmarks, etc.
                ' 
                ' Importing a element creates a copy of the original element, but the copy
                ' is ready to be inserted into the destination document.
                Dim importedSection As Section = singleDocx.Import(Of Section)(sourceSection, True, session)

                ' Now the new section can be appended to the destination document.
                singleDocx.Sections.Add(importedSection)
            Next sourceSection
        Next file

        ' Save single DOCX to a file.            
        singleDocx.Save(singleDocxPath)

        ' Open the result for demonstation purposes.
        System.Diagnostics.Process.Start(singleDocxPath)
    End Sub

End Module
