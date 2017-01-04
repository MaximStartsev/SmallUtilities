using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace MangaCompiler
{
    internal static class PdfCreator
    {
        /// <summary>
        /// Максимальное количество страниц в документе
        /// </summary>
        public const int MaxPagesCount = 500;

        public static void Create(IEnumerable<string> files, string outputFile, int part = 1)
        {
            bool isPart = part != 1;
            IEnumerable<string> partFiles = null;
            using (PdfDocument document = new PdfDocument())
            {
                document.Info.Title = Path.GetFileNameWithoutExtension(outputFile);
                foreach (var file in files)
                {
                    PdfPage page = document.AddPage();
                    //todo: Некоторые изображения (по догадкам, из-за нестандартного dpi) неверно отрисовываются
                    //string filename;
                    //if (!file.EndsWith(".jpg"))
                    //{
                    //    filename = Path.Combine(Path.GetDirectoryName(file), Path.GetFileNameWithoutExtension(file) + ".jpg");
                    //    using (var img = (Bitmap)Image.FromFile(file))
                    //    {
                    //        img.SetResolution(72, 72);
                    //        img.Save(filename, ImageFormat.Jpeg);
                    //    }
                    //}
                    //else
                    //{
                    //    filename = file;
                    //}

                    using (XGraphics gfx = XGraphics.FromPdfPage(page))
                    {
                        using (XImage image = XImage.FromFile(file))
                        {
                            //Если использовать свойства PixelWidth и PixelHeight, то значения неверные
                            page.Width = image.PointWidth;
                            page.Height = image.PointHeight;
                            gfx.DrawImage(image, 0, 0);
                        }
                    }
                    page.Close();
                    if (document.PageCount == MaxPagesCount)
                    {
                        isPart = true;
                        partFiles = files.Skip(MaxPagesCount);
                        break;
                    }
                }
                document.Save(isPart ? outputFile.Insert(outputFile.LastIndexOf("."), String.Format("-part-{0}", part)) : outputFile);
            }
            if (partFiles != null && partFiles.Any())
            {
                GC.Collect();
                Create(partFiles, outputFile, ++part);
            }
        }
    }
}
