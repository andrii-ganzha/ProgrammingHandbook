using System;
using System.IO;
using SautinSoft.Document;

namespace Sample
{
    class Sample
    {
        static void Main(string[] args)
        {
            AddHyperlink();            
        }
        /// <summary>
        /// How to add a hyperlink into a document.
        /// </summary>
        public static void AddHyperlink()
        {
            string docxPath = @"..\..\..\..\..\..\Testing Files\Hyperlinks.docx";

            // Let's create a simple DOCX document with a hyperlink.
            DocumentCore docx = new DocumentCore();
            //DocumentCore.Serial = "put your serial here";

            Hyperlink hpl = new Hyperlink(docx, "http://www.zoo.org", "Welcome to Zoo!");
            (hpl.DisplayInlines[0] as Run).CharacterFormat = new CharacterFormat() { Size = 16, FontColor = new Color(0x00CCFF), UnderlineStyle = UnderlineType.Single };
            hpl.ScreenTip = "Welcome to WoodLand Zoo!";

            Paragraph p = new Paragraph(docx);
            p.Inlines.Add(hpl);
            p.ParagraphFormat.Alignment = HorizontalAlignment.Center;

            docx.Content.Start.Insert(p.Content);

            // Save DOCX to a file
            docx.Save(docxPath);

            // Open the result for demonstation purposes.
            System.Diagnostics.Process.Start(docxPath);
        }
    }
}
