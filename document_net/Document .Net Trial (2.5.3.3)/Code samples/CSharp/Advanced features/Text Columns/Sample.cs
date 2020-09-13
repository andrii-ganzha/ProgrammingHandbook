using System;
using System.IO;
using SautinSoft.Document;

namespace Sample
{
    class Sample
    {
        static void Main(string[] args)
        {
            TextColumns();            
        }
        /// <summary>
        /// Working with text columns.
        /// </summary>
        public static void TextColumns()
        {
            string docxPath = @"..\..\..\..\..\..\Testing Files\TextColumns.docx";

            // Let's create DOCX document with 4 columns.
            DocumentCore docx = new DocumentCore();
            //DocumentCore.Serial = "put your serial here";

            // Add new section
            Section s = new Section(docx);
            docx.Sections.Add(s);

            s.PageSetup.PageMargins = new PageMargins()
            {
                Top = LengthUnitConverter.Convert(5, LengthUnit.Millimeter, LengthUnit.Point),
                Right = LengthUnitConverter.Convert(5, LengthUnit.Millimeter, LengthUnit.Point),
                Bottom = LengthUnitConverter.Convert(5, LengthUnit.Millimeter, LengthUnit.Point),
                Left = LengthUnitConverter.Convert(5, LengthUnit.Millimeter, LengthUnit.Point)
            };

            s.PageSetup.TextColumns = new TextColumnCollection(4);
            s.PageSetup.TextColumns.EvenlySpaced = false;
            s.PageSetup.TextColumns[0].Width = LengthUnitConverter.Convert(60, LengthUnit.Millimeter, LengthUnit.Point);
            s.PageSetup.TextColumns[1].Width = LengthUnitConverter.Convert(20, LengthUnit.Millimeter, LengthUnit.Point);
            s.PageSetup.TextColumns[2].Width = LengthUnitConverter.Convert(60, LengthUnit.Millimeter, LengthUnit.Point);
            s.PageSetup.TextColumns[3].Width = LengthUnitConverter.Convert(20, LengthUnit.Millimeter, LengthUnit.Point);


            // Fill our columns by any text.
            string text = "Shrek and Donkey arrive at Farquaad's palace in Duloc, where they end up in a tournament. The winner gets the \"privilege\" of rescuing Fiona so that Farquaad may marry her. ";

            for (int i = 0; i < 20; i++)
                s.Content.End.Insert(text, new CharacterFormat() { FontName = "Arial", Size = 12 });

            (s.Blocks[0] as Paragraph).ParagraphFormat.Alignment = HorizontalAlignment.Justify;

            // Save DOCX to a file
            docx.Save(docxPath);

            // Open the result for demonstation purposes.
            System.Diagnostics.Process.Start(docxPath);
        }
    }
}
