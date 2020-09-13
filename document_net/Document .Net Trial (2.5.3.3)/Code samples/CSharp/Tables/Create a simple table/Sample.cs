using System;
using System.IO;
using System.Linq;
using SautinSoft.Document;
using SautinSoft.Document.Tables;

namespace Sample
{
    class Sample
    {
        static void Main(string[] args)
        {
            CreatePlainTable();            
        }
        /// <summary>
        /// How to create a plain table and insert it inside a document.
        /// </summary>
        public static void CreatePlainTable()
        {
            string docxPath = @"..\..\..\..\..\..\Testing Files\Tables.docx";

            // Let's create a simple DOCX document.
            DocumentCore docx = new DocumentCore();
            //DocumentCore.Serial = "put your serial here";

            // Add new section
            Section s = new Section(docx);
            docx.Sections.Add(s);


            // Let's create a plain table: 2x3, with 100 mm width.
            Table t = new Table(docx);
            double twidth = LengthUnitConverter.Convert(100, LengthUnit.Millimeter, LengthUnit.Point);
            t.TableFormat.PreferredWidth = new TableWidth(twidth, TableWidthUnit.Point);
            t.TableFormat.Alignment = HorizontalAlignment.Center;


            int counter = 0;

            // Add 2 rows.
            for (int r = 0; r < 2; r++)
            {
                TableRow row = new TableRow(docx);

                // Add 3 columns
                for (int c = 0; c < 3; c++)
                {
                    TableCell cell = new TableCell(docx);

                    // Set some cell formatting
                    cell.CellFormat.Borders.SetBorders(MultipleBorderTypes.Outside, BorderStyle.Single, Color.Black, 1.0);

                    if (counter % 2 == 1)
                        cell.CellFormat.BackgroundColor = new Color(0xCCCCCC);

                    row.Cells.Add(cell);

                    // Let's add some text into each column.
                    Paragraph p = new Paragraph(docx);
                    p.ParagraphFormat.Alignment = HorizontalAlignment.Center;
                    p.ParagraphFormat.SpaceBefore = LengthUnitConverter.Convert(3, LengthUnit.Millimeter, LengthUnit.Point);
                    p.ParagraphFormat.SpaceAfter = LengthUnitConverter.Convert(3, LengthUnit.Millimeter, LengthUnit.Point); ;

                    p.Content.Start.Insert(String.Format("{0}", (char)(counter + 'A')), new CharacterFormat() { FontName = "Arial", FontColor = new Color(0x3399FF), Size = 12.0 });
                    cell.Blocks.Add(p);
                    counter++;
                }
                t.Rows.Add(row);
            }


            // Add this table to the current section.
            s.Blocks.Add(t);

            // Save DOCX to a file
            docx.Save(docxPath);

            // Open the result for demonstation purposes.
            System.Diagnostics.Process.Start(docxPath);
        }
    }
}
