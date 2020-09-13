using System;
using System.IO;
using SautinSoft.Document;

namespace Sample
{
    class Sample
    {
        static void Main(string[] args)
        {
            PageProperties();            
        }
        /// <summary>
        /// How to adjust a document page properties.
        /// </summary>
        public static void PageProperties()
        {
            string docxPath = @"..\..\..\..\..\..\Testing Files\PageProperties.docx";

            // Let's create a simple DOCX document.
            DocumentCore docx = new DocumentCore();
            //DocumentCore.Serial = "put your serial here";

            // Add new section.
            Section s1 = new Section(docx);
            s1.PageSetup.PaperType = PaperType.A4;
            s1.PageSetup.Orientation = Orientation.Landscape;
            s1.PageSetup.PageMargins = new PageMargins()
            {
                Top = LengthUnitConverter.Convert(50, LengthUnit.Millimeter, LengthUnit.Point),
                Right = LengthUnitConverter.Convert(1, LengthUnit.Inch, LengthUnit.Point),
                Bottom = LengthUnitConverter.Convert(10, LengthUnit.Millimeter, LengthUnit.Point),
                Left = LengthUnitConverter.Convert(2, LengthUnit.Centimeter, LengthUnit.Point)
            };

            docx.Sections.Add(s1);

            // Add some text to s1.
            s1.Content.Start.Insert("Shrek, a green ogre who loves the solitude in his swamp, " +
                            "finds his life interrupted when many fairytale characters are " +
                            "exiled there by order of the fairytale-hating Lord Farquaad.", new CharacterFormat() { FontName = "Times New Roman", Size = 14.0 });


            // Add page break.
            s1.Content.End.Insert(new SpecialCharacter(docx, SpecialCharacterType.PageBreak).Content);

            // Add another section.
            Section s2 = new Section(docx);
            s2.PageSetup.PaperType = PaperType.A4;
            s2.PageSetup.Orientation = Orientation.Portrait;
            s2.PageSetup.PageMargins = new PageMargins()
            {
                Top = LengthUnitConverter.Convert(5, LengthUnit.Millimeter, LengthUnit.Point),
                Right = LengthUnitConverter.Convert(5, LengthUnit.Millimeter, LengthUnit.Point),
                Bottom = LengthUnitConverter.Convert(5, LengthUnit.Millimeter, LengthUnit.Point),
                Left = LengthUnitConverter.Convert(5, LengthUnit.Millimeter, LengthUnit.Point)
            };


            docx.Sections.Add(s2);

            // Add some text to s2.
            Paragraph p = new Paragraph(docx);
            p.Content.Start.Insert("Shrek tells them that he will go ask Farquaad to send them back. " +
                                    "He brings along a talking Donkey who is the only fairytale creature who knows the way to Duloc.",
                                    new CharacterFormat() { FontName = "Times New Roman", Size = 14.0 });
            p.ParagraphFormat.Alignment = HorizontalAlignment.Justify;
            s2.Blocks.Add(p);

            // Save DOCX to a file
            docx.Save(docxPath);

            // Open the result for demonstation purposes.
            System.Diagnostics.Process.Start(docxPath);
        }
    }
}
