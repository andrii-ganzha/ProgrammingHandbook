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
            CreateNestedTable();
        }

        /// <summary>
        /// How to create a nested table and add it inside a document.
        /// </summary>
        public static void CreateNestedTable()
        {
            string docxFilePath = @"..\..\..\..\..\..\Testing Files\Tables.docx";

            // Let's create a simple DOCX document.
            DocumentCore docx = new DocumentCore();
            //DocumentCore.Serial = "put your serial here";

            // Add new section
            Section s = new Section(docx);
            docx.Sections.Add(s);


            // Let's create table 1: 1x2, with 150 mm width.            
            Table t1 = new Table(docx);
            double twidth = LengthUnitConverter.Convert(150, LengthUnit.Millimeter, LengthUnit.Point);
            t1.TableFormat.PreferredWidth = new TableWidth(twidth, TableWidthUnit.Point);


            // Add 1 rows.
            for (int r = 0; r < 1; r++)
            {
                TableRow row = new TableRow(docx);

                // Add 2 columns
                for (int c = 0; c < 2; c++)
                {
                    TableCell cell = new TableCell(docx);

                    // Set some cell formatting
                    cell.CellFormat.Borders.SetBorders(MultipleBorderTypes.Outside, BorderStyle.Single, Color.Black, 1.0);
                    cell.CellFormat.PreferredWidth = new TableWidth(twidth / 2, TableWidthUnit.Point);

                    double padding = LengthUnitConverter.Convert(3, LengthUnit.Millimeter, LengthUnit.Point);
                    cell.CellFormat.Padding = new Padding(padding);

                    row.Cells.Add(cell);
                }
                t1.Rows.Add(row);
            }

            // Add this table to the current section.
            s.Blocks.Add(t1);

            // Create nested table 3x3.
            Table t2 = new Table(docx);
            twidth = LengthUnitConverter.Convert(75, LengthUnit.Millimeter, LengthUnit.Point);
            t2.TableFormat.PreferredWidth = new TableWidth(twidth, TableWidthUnit.Point);
            t2.TableFormat.Alignment = HorizontalAlignment.Center;

            for (int r = 0; r < 3; r++)
            {
                TableRow row = new TableRow(docx);

                // Add 2 columns
                for (int c = 0; c < 3; c++)
                {
                    TableCell cell = new TableCell(docx);

                    // Set some cell formatting
                    cell.CellFormat.Borders.SetBorders(MultipleBorderTypes.Outside, BorderStyle.Single, Color.Black, 1.0);
                    row.Cells.Add(cell);

                    // Let's add some text into each column.
                    Paragraph p = new Paragraph(docx);
                    p.ParagraphFormat.Alignment = HorizontalAlignment.Center;
                    p.ParagraphFormat.SpaceBefore = LengthUnitConverter.Convert(3, LengthUnit.Millimeter, LengthUnit.Point);
                    p.ParagraphFormat.SpaceAfter = LengthUnitConverter.Convert(3, LengthUnit.Millimeter, LengthUnit.Point); ;

                    p.Content.Start.Insert(String.Format("row: {0} col: {1}", r + 1, c + 1, new CharacterFormat() { FontName = "Arial", Size = 12.0 }));
                    cell.Blocks.Add(p);

                }
                t2.Rows.Add(row);
            }

            // Insert table2 inside 2nd columns of table 1.
            t1.Rows[0].Cells[1].Blocks.Add(t2);

            // Insert some text inside 1st column of table 1.
            Paragraph p2 = new Paragraph(docx);
            p2.ParagraphFormat.Alignment = HorizontalAlignment.Center;
            p2.Content.Start.Insert("This is a 1st column of table 1");
            t1.Rows[0].Cells[0].Blocks.Add(p2);

            // Save DOCX to a file
            docx.Save(docxFilePath);

            // Open the result for demonstation purposes.
            System.Diagnostics.Process.Start(docxFilePath);
        }
    }
}
