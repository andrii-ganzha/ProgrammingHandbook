Imports System
Imports System.IO
Imports SautinSoft.Document

Module Sample

    Sub Main()
        'CreateRtf()
        CreateRtfFromString()
    End Sub

    ''' <summary>
    ''' Creates a simple RTF document.
    ''' </summary>
    Public Sub CreateRtf()
        ' Path to our RTF document.            
        Dim rtfPath As String = "..\..\..\..\..\Testing Files\Result.rtf"

        ' Let's create a simple RTF document.
        Dim rtf As New DocumentCore()
        'DocumentCore.Serial = "put your serial here";

        ' Add new section.
        Dim section As New Section(rtf)
        rtf.Sections.Add(section)

        ' Let's set page size A4.
        section.PageSetup.PaperType = PaperType.A4

        ' Add two paragraphs using different ways:

        ' Way 1: Add 1st paragraph.
        section.Blocks.Add(New Paragraph(rtf, "This is a first line in 1st paragraph!"))
        Dim par1 As Paragraph = TryCast(section.Blocks(0), Paragraph)
        par1.ParagraphFormat.Alignment = HorizontalAlignment.Center

        ' Let's add a second line.
        par1.Inlines.Add(New SpecialCharacter(rtf, SpecialCharacterType.LineBreak))
        par1.Inlines.Add(New Run(rtf, "Let's type a second line."))

        ' Let's change font name, size and color.
        Dim cf As New CharacterFormat() With {
            .FontName = "Verdana",
            .Size = 16,
            .FontColor = Color.Orange
        }
        For Each inline As Inline In par1.Inlines
            If TypeOf inline Is Run Then
                TryCast(inline, Run).CharacterFormat = cf.Clone()
            End If
        Next inline

        ' Way 2 (easy): Add 2nd paragarph using another way.
        rtf.Content.End.Insert(ControlChars.Lf & "This is a first line in 2nd paragraph.", New CharacterFormat() With {
            .Size = 25,
            .FontColor = Color.Blue,
            .Bold = True
        })
        Dim lBr As New SpecialCharacter(rtf, SpecialCharacterType.LineBreak)
        rtf.Content.End.Insert(lBr.Content)
        rtf.Content.End.Insert("This is a second line.", New CharacterFormat() With {
            .Size = 20,
            .FontColor = Color.DarkGreen,
            .UnderlineStyle = UnderlineType.Single
        })

        ' Save RTF to a file
        rtf.Save(rtfPath, SaveOptions.RtfDefault)

        ' Open the result for demonstation purposes.
        System.Diagnostics.Process.Start(rtfPath)
    End Sub
    Public Sub CreateRtfFromString()
        Dim someRtf As String = "{\rtf1\ansi\ansicpg1252\deff0\nouicompat\deflang1031{\fonttbl{\f0\fnil\fcharset0 Calibri;}}" & "{\*\generator Riched20 10.0.10586}\viewkind4\uc1 " & "\pard\sa200\sl276\slmult1\f0\fs22\lang7 test\par}"

        Dim rtf As New DocumentCore()
        rtf.Content.End.Insert(someRtf, RtfLoadOptions.RtfDefault)

        ' Get the RTF as string back from Document object.
        Dim loadedRtf As String = String.Empty

        Using ms As New MemoryStream()
            rtf.Save(ms, RtfSaveOptions.RtfDefault)
            loadedRtf = System.Text.Encoding.UTF8.GetString(ms.ToArray())
        End Using

        ' Save the document to a RTF file and show it.
        Dim file As String = Path.GetFullPath("Result.rtf")
        System.IO.File.WriteAllText(file, loadedRtf)
        System.Diagnostics.Process.Start(file)

    End Sub


End Module
