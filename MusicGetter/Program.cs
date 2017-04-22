using System;
using System.IO;
using System.Linq;

namespace MaximStartsev.SmallUtilities.MusicGetter
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string inDirectory;
                string outDirecotry;
                long size = 0;
                if (args.Length == 0)
                {
                    do
                    {
                        Console.WriteLine("Введите папку откуда копировать:");
                        inDirectory = Console.ReadLine();
                    } while (String.IsNullOrEmpty(inDirectory) || !Directory.Exists(inDirectory));
                    do
                    {
                        Console.WriteLine("Введите папку куда копировать:");
                        outDirecotry = Console.ReadLine();
                        try
                        {
                            if (!String.IsNullOrEmpty(outDirecotry) && !Directory.Exists(outDirecotry))
                            {
                                Directory.CreateDirectory(outDirecotry);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    } while (String.IsNullOrEmpty(outDirecotry) || !Directory.Exists(outDirecotry));
                    string sizeString;
                    do
                    {
                        Console.WriteLine("Введите максимальный объем копируемых данных (в гигабайтах):");
                        sizeString = Console.ReadLine();
                    } while (String.IsNullOrEmpty(sizeString) || !long.TryParse(sizeString, out size));
                }
                else
                {
                    inDirectory = args[0];
                    outDirecotry = args[1];
                    size = int.Parse(args[2]);
                }
                Console.WriteLine("Получение списка файлов...");
                var list = Directory.GetFiles(inDirectory, "*.mp3", SearchOption.AllDirectories).ToList();
                Console.WriteLine("Найдено {0} файлов.", list.Count);
                long allSile = list.Select(file => new FileInfo(file)).Select(fileInfo => fileInfo.Length).Sum();
                //Если общий размер файлов меньше заданного числа гигабайт, то копируем все.
                Console.WriteLine("Копирование файлов...");
                if (allSile / (1024 * 1024 * 1024) < size)
                {
                    foreach (var file in list)
                    {
                        File.Copy(file, Path.Combine(outDirecotry, Path.GetFileName(file)), true);
                    }
                    Console.WriteLine("Скопировано {0} файлов.", list.Count);
                }
                else
                {
                    var random = new Random();
                    const int maxCount = 100000;
                    var counter = 0;
                    long totalSize = 0;
                    size = size * 1024 * 1024 * 1024;
                    while (counter < maxCount && totalSize < size && list.Count > 0)
                    {
                        counter++;
                        var index = random.Next(list.Count());
                        var randomFile = list[index];
                        list.RemoveAt(index);
                        totalSize += (new FileInfo(randomFile)).Length;
                        File.Copy(randomFile, Path.Combine(outDirecotry, Path.GetFileName(randomFile)), true);
                    }
                    Console.WriteLine("Скопировано {0} файлов.", counter);
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
            }
            Console.ReadKey();
        }
    }
}
