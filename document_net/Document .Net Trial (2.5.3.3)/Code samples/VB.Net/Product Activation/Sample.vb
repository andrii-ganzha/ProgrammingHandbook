Imports System
Imports System.IO
Imports SautinSoft.Document

Module Sample

    Sub Main()
        ' Steps to activate the component:

        ' 1. Open your project in Visual Studio.
        ' 2. Remove the reference to the "SautinSoft.Document.dll".
        ' 3. Replace the old file "SautinSoft.Document.dll" by the new "SautinSoft.Document.dll".
        '    (You can find the new "SautinSoft.Document.dll" inside "Bin" in "document_net_full.zip").
        ' 4. Add new reference to the new file "SautinSoft.Document.dll".
        ' 5. Fill the property 'Serial' by your serial (Activation key):

        Dim myKey As String = "1234567890"

        Dim docxPath As String = "..\..\..\..\Testing Files\Serial.docx"

        ' Let's create a simple DOCX document.
        Dim docx As New DocumentCore()
        DocumentCore.Serial = myKey

        ' Add new section.
        Dim section As New Section(docx)
        docx.Sections.Add(section)

        docx.Content.Start.Insert("Hello World!", New CharacterFormat() With {
            .Size = 25,
            .FontColor = Color.Blue,
            .Bold = True
        })

        ' Save DOCX to a file
        docx.Save(docxPath)

        ' Open the result for demonstation purposes.
        System.Diagnostics.Process.Start(docxPath)
    End Sub

End Module
