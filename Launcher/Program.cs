using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launcher
{
    class Program
    {
        static void Main(string[] args)
        {
            int choice;
            while (true)
            {
                Console.WriteLine("Would you like to:");
                Console.WriteLine("1. Extract all images from zip files");
                Console.WriteLine("2. Sort extracted images to volume folders");
                Console.WriteLine("3. Exit");
                choice = validIntInput(1,3);
                switch (choice)
                {
                    case 1:
                        Extractor();
                        break;
                    case 2:
                        Sorter();
                        break;
                    case 3:
                        return;
                }               
            }
        }

        private static int validIntInput(int min,int max)
        {
            while (true)
            {
                int value;
                int.TryParse(Console.ReadLine(),out value);
                if (value >= min && value <= max) return value;
            }
        }
        static void Extractor()
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
            if (zipExist) counter = extractPics(getZipFiles(topDirectory), topDirectory, counter);
            else Console.WriteLine("No archives Found in current directory");
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
                        if (isPicture(entry.FullName))
                        {
                            Console.WriteLine(topDirectory + @"\extracted\" + counter.ToString("D6") + entry.FullName.Substring(entry.FullName.Length - 4));
                            entry.ExtractToFile(topDirectory + @"\extracted\" + counter.ToString("D6") + entry.FullName.Substring(entry.FullName.Length - 4));
                            counter++;
                        }
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
        static void Sorter()
        {
            string extractedDirectory = Directory.GetCurrentDirectory() + @"/extracted/";
            int volumeNumber = 1;
            while (Directory.Exists(extractedDirectory) && (Directory.GetFiles(extractedDirectory, "*", SearchOption.TopDirectoryOnly).Length != 0))
            {
                string[] images = Directory.GetFiles(extractedDirectory, "*", SearchOption.TopDirectoryOnly);
                List<string> volumeFiles = new List<string>();
                long totalFileSize = 0;
                long maxFileSize = 157286400;
                for (int i = 0; i < images.Length; i++)
                {
                    FileInfo file = new FileInfo(images[i]);
                    totalFileSize += file.Length;
                    if (totalFileSize <= maxFileSize)
                    {
                        volumeFiles.Add(images[i]);
                    }
                    else
                    {
                        break;
                    }
                }
                string volumeDirectory = extractedDirectory + @"/volume_" + volumeNumber.ToString("D3") + @"/";
                Directory.CreateDirectory(volumeDirectory);
                Console.WriteLine("Moving Files for Volume: " + volumeNumber);
                volumeNumber++;
                foreach (string fileName in volumeFiles)
                {
                    File.Move(fileName, volumeDirectory + fileName.Substring(fileName.Length - 10));
                }
            }
            if (!Directory.Exists(extractedDirectory)) Console.WriteLine("Extracted directory doesn't exists");
            else if (Directory.GetFiles(extractedDirectory, "*", SearchOption.TopDirectoryOnly).Length == 0) Console.WriteLine("Extracted directory is empty or all files were processed");
        }
    }
}
