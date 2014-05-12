using System;
using System.IO;
using System.IO.Compression;

namespace NewExtractorWithUI
{
    class MangaExtractor
    {
        string rootDirectory;
        string extractedFolderName;
        string[] imgExtensions;
        string[] extensions = {"img","png"};
        string[] otherArchives = { "rar","7z" };
        int counter;
        public string[] ZipFiles { get; set; }
        public string RootDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(rootDirectory)) return Directory.GetCurrentDirectory();
                else return rootDirectory;
            }
            set
            {
                rootDirectory = value;
            }
        }
        public string ExtractedFolderName
        {
            get
            {
                if (string.IsNullOrEmpty(extractedFolderName)) return @"\extracted";
                else return extractedFolderName;
            }
            set
            {
                extractedFolderName = value;
            }
        }
        public string[] ImgExtensions
        {
            get
            {
                if (imgExtensions == null) return extensions;
                else return imgExtensions;
            }
            set
            {
                imgExtensions = value;
            }
        }

        public MangaExtractor()
        {
            counter = 1;            
        }
        public void Extract()
        {
            ZipFiles = getZipFiles(RootDirectory);
            //foreach (string file in ZipFiles) Text += file;
        }
        private string[] getZipFiles(string topDirectory)
        {
            return Directory.GetFiles(topDirectory, "*.zip", SearchOption.AllDirectories);
        }
        private int extractPics(string[] p, string topDirectory, int counter)
        {
            Directory.CreateDirectory(topDirectory + ExtractedFolderName);
            string directoryToExtractTo = topDirectory + ExtractedFolderName;
            foreach (string archive in p)
            {
                using (ZipArchive oneArchive = ZipFile.OpenRead(archive))
                {
                    foreach (ZipArchiveEntry entry in oneArchive.Entries)
                    {
                        //if (isPicture(entry.FullName))
                        //{
                        //    Console.WriteLine(topDirectory+ ExtractedFolderName + counter.ToString("D6") + entry.FullName.Substring(entry.FullName.Length - 4));
                        //    entry.ExtractToFile(topDirectory + ExtractedFolderName + counter.ToString("D6") + entry.FullName.Substring(entry.FullName.Length - 4));
                        //    counter++;
                        //}
                        int length = entry.FullName.Length;
                        string numberString = "";
                        for (int i = length - 1; i != 0; i--)
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
        private bool isPicture(string p)
        {
            bool result = false;
            foreach (string extension in ImgExtensions)
            {
                if (p.EndsWith(extension, StringComparison.OrdinalIgnoreCase))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        private void oldMain()
        {
            string[] allFiles = Directory.GetFiles(RootDirectory, "*", SearchOption.AllDirectories);
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
            if (zipExist) counter = extractPics(getZipFiles(RootDirectory), RootDirectory, counter);
            else Console.WriteLine("No archives Found in current directory");

            Console.ReadKey();
        }
    }
}
