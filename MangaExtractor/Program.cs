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
            bool rarExist = false;
            bool zipExist = false;

            foreach (string file in allFiles)
            {
                if (file.Substring(file.Length - 3).ToLower() == "zip")
                {
                    zipExist = true;
                }
                if (file.Substring(file.Length - 3).ToLower() == "rar")
                {
                    rarExist = true;
                }
                if (zipExist && rarExist)
                {
                    break;
                }
            }

            if (zipExist && rarExist) extractPics(getBothTypes(topDirectory),topDirectory);
            else if (zipExist) extractPics(getZipFiles(topDirectory), topDirectory);
            else if (rarExist) extractPics(getRarFiles(topDirectory),topDirectory);
            else Console.WriteLine("No archives Found in current directory");

            Console.ReadKey();
        }

        private static string[] getRarFiles(string topDirectory)
        {
            return Directory.GetFiles(topDirectory, "*.rar", SearchOption.AllDirectories);
        }
        private static string[] getZipFiles(string topDirectory)
        {
            return Directory.GetFiles(topDirectory, "*.zip", SearchOption.AllDirectories);
        }
        private static void extractPics(string[] p, string topDirectory)
        {
            Directory.CreateDirectory(topDirectory + @"\extracted\");
            string directoryToExtractTo = topDirectory + @"\extracted";
            foreach (string archive in p)
            {
                using (ZipArchive oneArchive = ZipFile.OpenRead(archive))
                {
                    foreach (ZipArchiveEntry entry in oneArchive.Entries)
                    {
                        if (isPicture(entry.FullName)) { }
                    }
                }
            }
        }

        private static bool isPicture(string p)
        {
            throw new NotImplementedException();
        }

        private static string[] getBothTypes(string topDirectory)
        {
            throw new NotImplementedException();
        }
    }
}
