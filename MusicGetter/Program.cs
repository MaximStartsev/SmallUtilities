using System;
using System.ComponentModel;
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
                var options = new Options();
                if (args.Length == 0)
                {
                    do
                    {
                        Console.WriteLine("Введите папку откуда копировать:");
                        options.SourceFolder = Console.ReadLine();
                    } while (String.IsNullOrEmpty(options.SourceFolder) || !Directory.Exists(options.SourceFolder));
                    do
                    {
                        Console.WriteLine("Введите папку куда копировать:");
                        options.TargetFolder = Console.ReadLine();
                        try
                        {
                            if (!String.IsNullOrEmpty(options.TargetFolder) && !Directory.Exists(options.TargetFolder))
                            {
                                Directory.CreateDirectory(options.TargetFolder);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    } while (String.IsNullOrEmpty(options.TargetFolder) || !Directory.Exists(options.TargetFolder));
                    string sizeString;
                    int size;
                    do
                    {
                        Console.WriteLine("Введите максимальный объем копируемых данных (в гигабайтах):");
                        sizeString = Console.ReadLine();
                    } while (String.IsNullOrEmpty(sizeString) || !Int32.TryParse(sizeString, out size));
                    options.MaxSize = size;
                }
                else
                {
                    options.SourceFolder = args[0];
                    options.TargetFolder = args[1];
                    options.MaxSize = Int32.Parse(args[2]);
                }
                Console.WriteLine("Получение списка файлов...");
                var list = Directory.GetFiles(options.SourceFolder, "*.mp3", SearchOption.AllDirectories).ToList();
                Console.WriteLine("Найдено {0} файлов.", list.Count);
                long allSile = list.Select(file => new FileInfo(file)).Select(fileInfo => fileInfo.Length).Sum();
                //Если общий размер файлов меньше заданного числа гигабайт, то копируем все.
                Console.WriteLine("Копирование файлов...");
                if (allSile / (1024 * 1024 * 1024) < options.MaxSize)
                {
                    foreach (var file in list)
                    {
                        File.Copy(file, Path.Combine(options.TargetFolder, Path.GetFileName(file)), true);
                    }
                    Console.WriteLine("Скопировано {0} файлов.", list.Count);
                }
                else
                {
                    var random = new Random();
                    const int maxCount = 100000;
                    var counter = 0;
                    long totalSize = 0;
                    long size = (long)options.MaxSize * 1024 * 1024 * 1024;
                    long percent = -1;
                    while (counter < maxCount && totalSize < size && list.Count > 0)
                    {
                        counter++;
                        var index = random.Next(list.Count);
                        var randomFile = list[index];
                        list.RemoveAt(index);
                        totalSize += (new FileInfo(randomFile)).Length;
                        File.Copy(randomFile, Path.Combine(options.TargetFolder, Path.GetFileName(randomFile)), true);
                        var curPercent = (int)((double)totalSize / (double)size * 100);
                        if(percent != curPercent)
                        {
                            percent = curPercent;
                            Console.SetCursorPosition(0, Console.CursorTop);
                            Console.Write(percent.ToString() + "%");
                        }
                    }
                    Console.WriteLine();
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
