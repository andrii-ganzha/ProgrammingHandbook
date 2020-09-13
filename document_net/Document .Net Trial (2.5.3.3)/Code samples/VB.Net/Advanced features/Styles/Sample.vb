Imports System
Imports System.IO
Imports SautinSoft.Document

Module Sample

    Sub Main()
        Styles()
    End Sub

    ''' <summary>
    ''' This sample shows how to work with styles.
    ''' </summary>
    Public Sub Styles()
        Dim docxPath As String = "..\..\..\..\..\Testing Files\Styles.docx"

        ' Let's create document.
        Dim document As New DocumentCore()
        'DocumentCore.Serial = "put your serial here";

        ' Create custom styles.
        Dim characterStyle As New CharacterStyle("CharacterStyle1")
        characterStyle.CharacterFormat.FontName = "Arial"
        characterStyle.CharacterFormat.UnderlineStyle = UnderlineType.Wave
        characterStyle.CharacterFormat.Size = 18

        Dim paragraphStyle As New ParagraphStyle("ParagraphStyle1")
        paragraphStyle.CharacterFormat.FontName = "Times New Roman"
        paragraphStyle.CharacterFormat.Size = 14
        paragraphStyle.ParagraphFormat.Alignment = HorizontalAlignment.Center

        ' First add styles to the document, then use it.
        document.Styles.Add(characterStyle)
        document.Styles.Add(paragraphStyle)

			' Add text content.
        document.Sections.Add(New Section(document, New Paragraph(document, New Run(document, "Once upon a time, in a far away swamp, there lived an ogre named "), New Run(document, "Shrek") With {
            .CharacterFormat = New CharacterFormat With {.Style = characterStyle}
        },
        New Run(document, " whose precious solitude is suddenly shattered by an invasion of annoying fairy tale characters...")) With {
            .ParagraphFormat = New ParagraphFormat With {.Style = paragraphStyle}
        }))


        ' Save DOCX to a file
        document.Save(docxPath)

        ' Open the result for demonstation purposes.
        System.Diagnostics.Process.Start(docxPath)
    End Sub

End Module
