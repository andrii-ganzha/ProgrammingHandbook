Imports System
Imports System.IO
Imports SautinSoft.Document

Module Sample

    Sub Main()
        CreateForms()
    End Sub

    ''' <summary>
    ''' Working with forms.
    ''' </summary>
    Public Sub CreateForms()
        Dim docxFilePath As String = "..\..\..\..\..\Testing Files\Froms.docx"

        ' Let's create document.
        Dim document As New DocumentCore()
        'DocumentCore.Serial = "put your serial here";

        Dim placeHolder As New String(ChrW(&H2002), 5)

        ' Create form fields.

        Dim fFullName As New Field(document, FieldType.FormText, Nothing, placeHolder)
        fFullName.FormData.Name = "FullName"

        Dim fBirthData As New Field(document, FieldType.FormText, Nothing, placeHolder)
        fBirthData.FormData.Name = "BirthDate"

        Dim fGender As New Field(document, FieldType.FormDropDown)
        fGender.FormData.Name = "Gender"

        Dim fMarried As New Field(document, FieldType.FormCheckBox)
        fMarried.FormData.Name = "Married"

        Dim fPhone As New Field(document, FieldType.FormText, Nothing, placeHolder)
        fPhone.FormData.Name = "Phone"

        document.Sections.Add(New Section(document, New Paragraph(document, New Run(document, "Full name: "), fFullName), New Paragraph(document, New Run(document, "Birth date: "), fBirthData), New Paragraph(document, New Run(document, "Gender: "), fGender), New Paragraph(document, New Run(document, "Married: "), fMarried), New Paragraph(document, New Run(document, "Phone: "), fPhone)))


        ' Customize form fields.
        Dim formFieldsData = document.Content.FormFieldsData

        Dim fullNameFieldData = CType(formFieldsData("FullName"), FormTextData)
        fullNameFieldData.MaximumLength = 50
        fullNameFieldData.ValueFormat = "Title case"
        fullNameFieldData.HelpText = "Enter your name and surname (trimmed to 50 characters)."
        fullNameFieldData.StatusText = fullNameFieldData.HelpText

        Dim birthdateFieldData = CType(formFieldsData("BirthDate"), FormTextData)
        birthdateFieldData.TextType = FormTextType.Date
        birthdateFieldData.ValueFormat = "yyyy-MM-dd"
        birthdateFieldData.HelpText = "Enter your date of birth."
        birthdateFieldData.StatusText = birthdateFieldData.HelpText

        Dim genderFieldData = CType(formFieldsData("Gender"), FormDropDownData)
        genderFieldData.Items.Add("<Select sex>")
        genderFieldData.Items.Add("Male")
        genderFieldData.Items.Add("Female")
        genderFieldData.HelpText = "Select your gender."
        genderFieldData.StatusText = genderFieldData.HelpText

        Dim marriedFieldData = CType(formFieldsData("Married"), FormCheckBoxData)
        marriedFieldData.HelpText = "Mark as checked if you are married."
        marriedFieldData.StatusText = marriedFieldData.HelpText

        Dim salaryFieldData = CType(formFieldsData("Phone"), FormTextData)
        salaryFieldData.TextType = FormTextType.Number
        salaryFieldData.ValueFormat = "(###) ###-####"
        salaryFieldData.HelpText = "Enter your phone number."
        salaryFieldData.StatusText = salaryFieldData.HelpText

        ' Save DOCX to a file
        document.Save(docxFilePath)

        ' Open the result for demonstration purposes.
        System.Diagnostics.Process.Start(docxFilePath)
    End Sub

End Module
