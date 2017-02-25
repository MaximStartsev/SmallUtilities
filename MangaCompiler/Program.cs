using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MangaCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length != 2)
                {
                    Console.WriteLine("Продолжение невозможно. Для работы программы необходимо передать два параметра. Первый - путь к директории с архивами. Второй - имя выходного файла.");
                    return;
                }
                var fullpath = Path.GetFullPath(args[0]);
                Console.WriteLine(String.Format("Поиск файлов в {0}...", fullpath));
                var files = GetFiles(fullpath);
                var destination = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                Directory.CreateDirectory(destination);

                Console.WriteLine("Распаковка архивов...");
                foreach (var file in files.Where(f => f.EndsWith(".rar") || f.EndsWith(".zip")))
                {
                    Archivator.ExtractArchive(file, Path.Combine(destination, Path.GetFileNameWithoutExtension(file)));
                }
                foreach (var file in files.Where(f => f.EndsWith(".jpg") || f.EndsWith(".png")))
                {
                    var newfile = Path.GetFileName(file);
                    var index = file.LastIndexOf("\\");
                    var path = file.Substring(0, index);
                    index = path.LastIndexOf("\\");
                    path = path.Substring(index + 1);
                    path = Path.Combine(destination, path);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    //newfile = Path.Combine(path, newfile);
                    File.Copy(file, Path.Combine(path, newfile));
                }
                Console.WriteLine(String.Format("Создание pdf файла {0}...", args[1]));
                var tempFiles = GetFiles(destination).Where(f=>f.EndsWith(".jpg") || f.EndsWith(".png"));
                PdfCreator.Create(tempFiles, args[1]);

                Console.WriteLine("Удаление временных файлов...");
                //foreach (var file in tempFiles)
                //{
                //    File.Delete(file);
                //}
                Directory.Delete(destination, true);

                Console.WriteLine("Готово");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex);
            }
        }

        private static IEnumerable<string> GetFiles(string path)
        {
            if (!Directory.Exists(path))
            {
                throw new Exception("Не найден путь: " + path);
            }
            return Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
        }
    }
}
