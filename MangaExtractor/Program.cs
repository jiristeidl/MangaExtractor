using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;


namespace MangaExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            string topDirectory = Directory.GetCurrentDirectory();
            string[] allFiles = Directory.GetFiles(topDirectory, "*", SearchOption.AllDirectories);
            bool zipExist = false;
            int counter = 1;

            foreach (string file in allFiles)
            {
                if (file.Substring(file.Length - 3).ToLower() == "zip")
                {
                    zipExist = true;
                    break;
                }
            }
            if (zipExist) counter = extractPics(getZipFiles(topDirectory), topDirectory,counter);
            else Console.WriteLine("No archives Found in current directory");

            Console.ReadKey();
        }
        
        private static string[] getZipFiles(string topDirectory)
        {
            return Directory.GetFiles(topDirectory, "*.zip", SearchOption.AllDirectories);
        }
        private static int extractPics(string[] p, string topDirectory, int counter)
        {
            Directory.CreateDirectory(topDirectory + @"\extracted\");
            string directoryToExtractTo = topDirectory + @"\extracted";
            foreach (string archive in p)
            {
                using (ZipArchive oneArchive = ZipFile.OpenRead(archive))
                {
                    foreach (ZipArchiveEntry entry in oneArchive.Entries)
                    {
                        //if (isPicture(entry.FullName))
                        //{
                        //    Console.WriteLine(topDirectory+ @"\extracted\" + counter.ToString("D6") + entry.FullName.Substring(entry.FullName.Length - 4));
                        //    entry.ExtractToFile(topDirectory + @"\extracted\" + counter.ToString("D6") + entry.FullName.Substring(entry.FullName.Length - 4));
                        //    counter++;
                        //}
                        int length = entry.FullName.Length;
                        string numberString = "";
                        for (int i = length-1; i != 0; i--)
                        {
                            if (char.IsDigit(entry.FullName[i]))
                            {
                                numberString = entry.FullName[i] + numberString;
                            }
                            if (!string.IsNullOrEmpty(numberString) && !char.IsDigit(entry.FullName[i])) break;
                        }
                        Console.WriteLine(numberString);
                    }
                }
            }
            return counter;
        }
        private static bool isPicture(string p)
        {
            string[] possibleExtensions = {
                                              "jpg",
                                              "png"
                                          };
            bool result = false;
            foreach (string extension in possibleExtensions)
            {
                if (p.EndsWith(extension, StringComparison.OrdinalIgnoreCase))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }        
    }
}
