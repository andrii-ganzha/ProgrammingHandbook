using System;
using System.IO;
using SautinSoft.Document;
using System.Text;

namespace Sample
{
    class Sample
    {
        static void Main(string[] args)
        {
            GetContent();
        }
        /// <summary>
        /// Get content example.
        /// </summary>
        public static void GetContent()
        {
            // Path to a input document.
            string loadPath = @"..\..\..\..\..\..\Testing Files\example.docx";

            //DocumentCore.Serial = "put your serial here";
            DocumentCore docx = DocumentCore.Load(loadPath);

            StringBuilder sb = new StringBuilder();

            // Get content from each paragraph.
            foreach (Paragraph paragraph in docx.GetChildElements(true, ElementType.Paragraph))
            {
                sb.AppendFormat("Paragraph: {0}", paragraph.Content.ToString());
                sb.AppendLine();
            }

            // Get content from each bold run.
            foreach (Run run in docx.GetChildElements(true, ElementType.Run))
            {
                if (run.CharacterFormat.Bold)
                {
                    sb.AppendFormat("Bold run: {0}", run.Content.ToString());
                    sb.AppendLine();
                }
            }

            Console.WriteLine(sb.ToString());
            Console.ReadLine();
        }
    }
}
