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

                Console.WriteLine("Поиск файлов...");
                var files = GetFiles(args[0]);
                var destination = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                Directory.CreateDirectory(destination);

                Console.WriteLine("Распаковка архивов...");
                foreach(var file in files.Where(f=>f.EndsWith(".rar") || f.EndsWith(".zip")))
                {
                    Archivator.ExtractArchive(file, destination);
                }

                Console.WriteLine("Создание pdf файла...");
                var tempFiles = GetFiles(destination);
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
