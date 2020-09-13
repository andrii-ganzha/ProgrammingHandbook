using System;
using System.IO;
using SautinSoft.Document;

namespace Sample
{
    class Sample
    {
        static void Main(string[] args)
        {
            CreateForms();
        }
        /// <summary>
        /// Working with forms.
        /// </summary>
        public static void CreateForms()
        {
            string docxFilePath = @"..\..\..\..\..\..\Testing Files\Froms.docx";

            // Let's create document.
            DocumentCore document = new DocumentCore();
            //DocumentCore.Serial = "put your serial here";

            string placeHolder = new string('\x2002', 5);

            // Create form fields.

            Field fFullName = new Field(document, FieldType.FormText, null, placeHolder);
            fFullName.FormData.Name = "FullName";

            Field fBirthData = new Field(document, FieldType.FormText, null, placeHolder);
            fBirthData.FormData.Name = "BirthDate";

            Field fGender = new Field(document, FieldType.FormDropDown);
            fGender.FormData.Name = "Gender";

            Field fMarried = new Field(document, FieldType.FormCheckBox);
            fMarried.FormData. Name = "Married";

            Field fPhone = new Field(document, FieldType.FormText, null, placeHolder);
            fPhone.FormData.Name = "Phone";

            document.Sections.Add(new Section(document,

                new Paragraph(document,
                new Run(document, "Full name: "),
                fFullName),

                new Paragraph(document,
                new Run(document, "Birth date: "),
                fBirthData),

                new Paragraph(document,
                new Run(document, "Gender: "),
                fGender),

                new Paragraph(document,
                new Run(document, "Married: "),
                fMarried),

                new Paragraph(document,
                new Run(document, "Phone: "),
                fPhone)));


            // Customize form fields.
            var formFieldsData = document.Content.FormFieldsData;

            var fullNameFieldData = (FormTextData)formFieldsData["FullName"];
            fullNameFieldData.MaximumLength = 50;
            fullNameFieldData.ValueFormat = "Title case";
            fullNameFieldData.StatusText = fullNameFieldData.HelpText =
                "Enter your name and surname (trimmed to 50 characters).";

            var birthdateFieldData = (FormTextData)formFieldsData["BirthDate"];
            birthdateFieldData.TextType = FormTextType.Date;
            birthdateFieldData.ValueFormat = "yyyy-MM-dd";
            birthdateFieldData.StatusText = birthdateFieldData.HelpText =
                "Enter your date of birth.";

            var genderFieldData = (FormDropDownData)formFieldsData["Gender"];
            genderFieldData.Items.Add("<Select sex>");
            genderFieldData.Items.Add("Male");
            genderFieldData.Items.Add("Female");
            genderFieldData.StatusText = genderFieldData.HelpText =
                "Select your gender.";

            var marriedFieldData = (FormCheckBoxData)formFieldsData["Married"];
            marriedFieldData.StatusText = marriedFieldData.HelpText =
                "Mark as checked if you are married.";

            var salaryFieldData = (FormTextData)formFieldsData["Phone"];
            salaryFieldData.TextType = FormTextType.Number;
            salaryFieldData.ValueFormat = "(###) ###-####";
            salaryFieldData.StatusText = salaryFieldData.HelpText =
                "Enter your phone number.";

            // Save DOCX to a file
            document.Save(docxFilePath);

            // Open the result for demonstration purposes.
            System.Diagnostics.Process.Start(docxFilePath);
        }
    }
}
