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
            ModifyTable();
        }

        /// <summary>
        /// How to modify an existing table in a document.
        /// </summary>
        public static void ModifyTable()
        {
            string currTablePath = Path.GetFullPath(@"..\..\..\..\..\..\Testing Files\table.docx");
            string newTablePath = Path.Combine(Path.GetDirectoryName(currTablePath), "Modified table.docx");

            //DocumentCore.Serial = "put your serial here";

            // Let's load DOCX document
            DocumentCore docx = DocumentCore.Load(currTablePath);

            // Find a first table
            Table t = (Table)docx.GetChildElements(true, ElementType.Table).First();

            // Set dashed borders and yellow background for all cells.
            for (int r = 0; r < t.Rows.Count; r++)
                for (int c = 0; c < t.Rows[r].Cells.Count; c++)
                {
                    TableCell cell = t.Rows[r].Cells[c];
                    cell.CellFormat.Borders.SetBorders(MultipleBorderTypes.Outside, BorderStyle.Dashed, Color.Black, 1);
                    cell.CellFormat.BackgroundColor = new Color(0xFFCC00);
                }



            // Save DOCX to a file
            docx.Save(newTablePath);

            // Show the source and the resulting document.
            System.Diagnostics.Process.Start(currTablePath);
            System.Diagnostics.Process.Start(newTablePath);
        }

    }
}
