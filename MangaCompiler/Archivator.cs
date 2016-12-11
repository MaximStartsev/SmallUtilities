using SevenZip;

namespace MangaCompiler
{
    internal static class Archivator
    {
        static Archivator()
        {
            SevenZipExtractor.SetLibraryPath(@"C:\Program Files\7-Zip\7z.dll");
        }
        public static void ExtractArchive(string sourcePath, string destinationPath)
        {
            using (var extractor = new SevenZipExtractor(sourcePath))
            {
                extractor.ExtractArchive(destinationPath);
            }
        }
    }
}
