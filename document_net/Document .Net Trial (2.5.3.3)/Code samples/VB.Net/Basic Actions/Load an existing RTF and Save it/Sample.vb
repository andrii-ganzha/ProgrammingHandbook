Imports System
Imports System.IO
Imports SautinSoft.Document

Module Sample

    Sub Main()
        LoadAndSave()
    End Sub

    ''' <summary>
    ''' Load an existing .rtf and save it as a new document.
    ''' </summary>
    Public Sub LoadAndSave()
        ' Path to a loadable document.
        Dim loadPath As String = "..\..\..\..\..\Testing Files\example.rtf"

        'DocumentCore.Serial = "put your serial here";
        Dim docx As DocumentCore = DocumentCore.Load(loadPath)

        Dim savePath As String = Path.ChangeExtension(loadPath, ".new.rtf")
        docx.Save(savePath, SaveOptions.RtfDefault)

        ' Open both files: example.docx and example.new.rtf.
        'System.Diagnostics.Process.Start(loadPath);
        System.Diagnostics.Process.Start(savePath)
    End Sub

End Module
