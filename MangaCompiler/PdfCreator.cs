using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Collections.Generic;

namespace MangaCompiler
{
    internal static class PdfCreator
    {
        public static void Create(IEnumerable<string> files, string outputFile)
        {
            using (PdfDocument document = new PdfDocument())
            {
                document.Info.Title = "Manga";
                foreach (var file in files)
                {
                    PdfPage page = document.AddPage();
                    using (XGraphics gfx = XGraphics.FromPdfPage(page))
                    {
                        DrawImage(gfx, file);
                    }
                }
                document.Save(outputFile);
            }
        }

        private static void DrawImage(XGraphics gfx, string file)
        {
            using (XImage image = XImage.FromFile(file))
            {
                gfx.DrawImage(image, 0, 0);
            }
        }
    }
}
