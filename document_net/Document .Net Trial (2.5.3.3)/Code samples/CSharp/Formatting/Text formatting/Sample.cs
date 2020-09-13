using System;
using System.IO;
using SautinSoft.Document;

namespace Sample
{
    class Sample
    {
        static void Main(string[] args)
        {
            TextFormatting();            
        }
        /// <summary>
        /// This sample shows how to specify a text formatting for a document.
        /// </summary>
        public static void TextFormatting()
        {   
            string docxPath = @"..\..\..\..\..\..\Testing Files\Text Formatting.docx";

            // Let's create a simple DOCX document.
            DocumentCore docx = new DocumentCore();
            //DocumentCore.Serial = "put your serial here";


            docx.Content.Start.Insert("This is sample document\n", new CharacterFormat() { FontName = "Calibri", Size = 16.0, FontColor = Color.Orange, Bold = true });
            (docx.Sections[0].Blocks[0] as Paragraph).ParagraphFormat.Alignment = HorizontalAlignment.Center;


            docx.Content.End.Insert("Bold", new CharacterFormat() { Bold = true, FontName = "Calibri", Size = 11.0 });
            docx.Content.End.Insert(" italic ", new CharacterFormat() { Italic = true, FontName = "Calibri", Size = 11.0 });
            docx.Content.End.Insert("underline", new CharacterFormat() { UnderlineStyle = UnderlineType.Single, FontName = "Calibri", Size = 11.0 });
            docx.Content.End.Insert(" ", new CharacterFormat() { Bold = true, FontName = "Calibri", Size = 11.0 });
            docx.Content.End.Insert("strikethrough", new CharacterFormat() { Strikethrough = true, FontName = "Calibri", Size = 11.0 });


            Paragraph p = new Paragraph(docx);
            p.ParagraphFormat.Alignment = HorizontalAlignment.Right;
            docx.Sections[0].Blocks.Add(p);
            p.Content.End.Insert("This paragraph is aligned by right.", new CharacterFormat() { Size = 25, FontColor = new Color(0x4F81BD), Bold = true });
            SpecialCharacter lBr = new SpecialCharacter(docx, SpecialCharacterType.LineBreak);
            p.Content.End.Insert(lBr.Content);
            p.Content.End.Insert("Second line.", new CharacterFormat() { Size = 25, FontColor = Color.DarkGreen, Bold = true });

            // Save DOCX to a file
            docx.Save(docxPath);

            // Open the result for demonstation purposes.
            System.Diagnostics.Process.Start(docxPath);
        }
    }
}
