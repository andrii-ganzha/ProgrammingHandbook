using System;
using System.IO;
using SautinSoft.Document;

namespace Sample
{
    class Sample
    {
        static void Main(string[] args)
        {
            Styles();            
        }
        /// <summary>
        /// This sample shows how to work with styles.
        /// </summary>
        public static void Styles()
        {
            string docxPath = @"..\..\..\..\..\..\Testing Files\Styles.docx";

            // Let's create document.
            DocumentCore document = new DocumentCore();
            //DocumentCore.Serial = "put your serial here";

            // Create custom styles.
            CharacterStyle characterStyle = new CharacterStyle("CharacterStyle1");
            characterStyle.CharacterFormat.FontName = "Arial";
            characterStyle.CharacterFormat.UnderlineStyle = UnderlineType.Wave;
            characterStyle.CharacterFormat.Size = 18;

            ParagraphStyle paragraphStyle = new ParagraphStyle("ParagraphStyle1");
            paragraphStyle.CharacterFormat.FontName = "Times New Roman";
            paragraphStyle.CharacterFormat.Size = 14;
            paragraphStyle.ParagraphFormat.Alignment = HorizontalAlignment.Center;

            // First add styles to the document, then use it.
            document.Styles.Add(characterStyle);
            document.Styles.Add(paragraphStyle);

            // Add text content.
            document.Sections.Add(
            new Section(document,
                new Paragraph(document,
                new Run(document, "Once upon a time, in a far away swamp, there lived an ogre named "),
                new Run(document, "Shrek")
                { CharacterFormat = { Style = characterStyle } },
                new Run(document, " whose precious solitude is suddenly shattered by an invasion of annoying fairy tale characters..."))
                { ParagraphFormat = { Style = paragraphStyle } }));


            // Save DOCX to a file
            document.Save(docxPath);

            // Open the result for demonstation purposes.
            System.Diagnostics.Process.Start(docxPath);
        }
    }
}
