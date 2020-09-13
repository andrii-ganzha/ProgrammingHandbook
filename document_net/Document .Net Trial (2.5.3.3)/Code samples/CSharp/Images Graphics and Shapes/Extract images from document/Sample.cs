using System;
using System.IO;
using System.Collections.Generic;
using SautinSoft.Document;
using SautinSoft.Document.Drawing;

namespace Sample
{
    class Sample
    {
        static void Main(string[] args)
        {            
            ExtractImagesFromDocument();
        }

        public static void ExtractImagesFromDocument()
        {
            // Path a .docx, .rtf or .pdf file with images.
            string filePath = @"..\..\..\..\..\..\Testing Files\example.pdf";

            DirectoryInfo imageDir = new DirectoryInfo(Path.GetDirectoryName(filePath));
            string imageTemplate = "Picture";
            List<ImageData> imageInventory = new List<ImageData>();

            DocumentCore dc = DocumentCore.Load(filePath);

            // Extract all images from document, skip duplicates.
            foreach (Picture pict in GetPictures(dc.Document))
            {
                // Let's avoid the adding of duplicates.
                if (imageInventory.Exists((img => (img.ImageStream.Length == pict.ImageData.ImageStream.Length))) == false)
                    imageInventory.Add(pict.ImageData);
            }
            // Save and show all images.
            for (int i = 0; i < imageInventory.Count; i++)
            {
                string imagePath = Path.Combine(imageDir.FullName, String.Format("{0}{1}.{2}", imageTemplate, i + 1, imageInventory[i].Format.ToString().ToLower()));
                File.WriteAllBytes(imagePath, imageInventory[i].ImageStream.ToArray());
                System.Diagnostics.Process.Start(imagePath);
            }
        }

        static IEnumerable<Picture> GetPictures(DocumentCore document)
        {
            foreach (DrawingElement e in document.GetChildElements(true, ElementType.DrawingElement))
            {
                foreach (Picture pict in GetPicturesRecursive(e.Shape))
                {
                    yield return pict;
                }
            }
        }
        static IEnumerable<Picture> GetPicturesRecursive(ShapeBase sb)
        {
            if (sb is Picture)
            {
                yield return (Picture)sb;
                yield break;
            }
            else
                if (sb is ShapeGroup)
                {
                    foreach (ShapeBase shape in (sb as ShapeGroup).ChildShapes)
                    {
                        foreach (Picture pict in GetPicturesRecursive(shape))
                        {
                            yield return pict;
                        }
                    }
                }
        }
    }
}
