using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        public const int DefaultResolution = 72;

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
                    string filename = ResizeImage(file);
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
                        using (XImage image = XImage.FromFile(filename))
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

        private static string ResizeImage(string filename)
        {
            //using (var image = Image.FromFile(filename))
            //{
            //    var newImage = new Bitmap(image);
            //    newImage.SetResolution(DefaultResolution, DefaultResolution);
            //    filename = Path.Combine(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename) + ".jpg");
            //    newImage.Save(filename, ImageFormat.Jpeg);
            //}
            return filename;
        }
        //private static string ResizeImage(string filename)
        //{
        //    using (var img = (Bitmap)Image.FromFile(filename))
        //    {
        //        if (img.HorizontalResolution != DefaultResolution || img.VerticalResolution != DefaultResolution)
        //        {
        //            var hRatio = img.HorizontalResolution / DefaultResolution;
        //            var vRatio = img.VerticalResolution / DefaultResolution;

        //            var width = img.Width;// Convert.ToInt32(img.Width * hRatio);
        //            var height = img.Height;// Convert.ToInt32(img.Height * vRatio);
        //            using (var newImage = new Bitmap(width, height))
        //            {
        //                using (var graphics = Graphics.FromImage(newImage))
        //                {
        //                    graphics.CompositingMode = CompositingMode.SourceCopy;
        //                    graphics.CompositingQuality = CompositingQuality.HighQuality;
        //                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //                    graphics.SmoothingMode = SmoothingMode.HighQuality;
        //                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

        //                    using (var wrapMode = new ImageAttributes())
        //                    {
        //                        wrapMode.SetWrapMode(WrapMode.TileFlipXY);
        //                        var destRect = new Rectangle(0, 0, width, height);
        //                        graphics.DrawImage(img, destRect, 0, 0, width, height, GraphicsUnit.Pixel, wrapMode);
        //                       // graphics.DrawImage(img, new Point(0, 0));
        //                    }
        //                }
        //                filename = Path.Combine(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename) + ".jpg");
        //                newImage.Save(filename, ImageFormat.Jpeg);
        //            }

        //        }
        //    }
        //    return filename;
        //}
    }
}
