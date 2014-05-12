using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;

namespace NewExtractorWithUI
{
    class MangaExtractor
    {
        string rootDirectory;
        string extractedFolderName;
        string[] imgExtensions;
        string[] extensions = {"img","png"};
        string[] otherArchives = { "rar","7z" };        
        Dictionary<int, string> filesToExtract;
        Dictionary<int, string> imagesToExtract;
        int counter;

        public string[] ZipFiles
        {
            get
            {                
                if (filesToExtract == null || filesToExtract.Count == 0) return new string[2] { "No Files Found", "" };
                else
                {
                    string[] filesReturning = new string[filesToExtract.Count];
                    filesToExtract.Values.CopyTo(filesReturning, 0);
                    return filesReturning;
                }

            }
        }
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
        public string[] Images
        {
            get
            {
                if (imagesToExtract == null || imagesToExtract.Count == 0) return new string[2] { "No Images Found", "" };
                else
                {
                    string[] filesReturning = new string[filesToExtract.Count];
                    imagesToExtract.Values.CopyTo(filesReturning, 0);
                    return filesReturning;
                }
            }
        }

        public MangaExtractor()
        {
            counter = 1;
            filesToExtract = new Dictionary<int, string>();
        }
        public void Extract()
        {
            string dump;
            filesToExtract = getZipFiles();
            imagesToExtract = extractPictures(filesToExtract);
            if (filesToExtract.TryGetValue(-1, out dump))
            {
               
            }
            else
            {

            }
        }

        private Dictionary<int, string> extractPictures(Dictionary<int, string> filesToExtract)
        {
            throw new NotImplementedException();
            string[] archives = getArchives(filesToExtract);
            foreach (string archive in archives)
            {
                using (ZipArchive oneArchive = ZipFile.OpenRead(archive))
                {
                    foreach (ZipArchiveEntry entry in oneArchive.Entries)
                    {
                        if (isPicture(entry.FullName))
                        {
                            Console.WriteLine(RootDirectory + ExtractedFolderName + counter.ToString("D6") + entry.FullName.Substring(entry.FullName.Length - 4));
                            entry.ExtractToFile(RootDirectory + ExtractedFolderName + counter.ToString("D6") + entry.FullName.Substring(entry.FullName.Length - 4));
                            counter++;
                        }
                    }
                }
            }
        }
        private string[] getArchives(Dictionary<int, string> filesToExtract)
        {
            throw new NotImplementedException();
            string dump;
            string[] result = new string[filesToExtract.Count];
            if (filesToExtract.TryGetValue(-1, out dump))
            {
                dump = null;
                filesToExtract.Values.CopyTo(result, 0);
            }
        }
        private Dictionary<int, string> getZipFiles()
        {
            Dictionary<int,string> allZipFiles = new Dictionary<int,string>();
            string[] zipFiles = Directory.GetFiles(RootDirectory, "*.zip", SearchOption.AllDirectories);
            foreach(string entry in zipFiles)
            {
                int length = entry.Length;
                string numberString = "";
                for (int i = length - 1; i != 0; i--)
                {
                    if (char.IsDigit(entry[i]))
                    {
                        numberString = entry[i] + numberString;
                    }
                    if (!string.IsNullOrEmpty(numberString) && !char.IsDigit(entry[i])) break;
                }
                if (string.IsNullOrEmpty(numberString)) numberString = "-1";
                allZipFiles.Add(int.Parse(numberString),entry);
            }
            return allZipFiles;
        }
        private int oldextractPics(string[] p, string topDirectory, int counter)
        {
            Directory.CreateDirectory(topDirectory + ExtractedFolderName);
            string directoryToExtractTo = topDirectory + ExtractedFolderName;
            foreach (string archive in p)
            {
                using (ZipArchive oneArchive = ZipFile.OpenRead(archive))
                {
                    foreach (ZipArchiveEntry entry in oneArchive.Entries)
                    {
                        if (isPicture(entry.FullName))
                        {
                            Console.WriteLine(topDirectory + ExtractedFolderName + counter.ToString("D6") + entry.FullName.Substring(entry.FullName.Length - 4));
                            entry.ExtractToFile(topDirectory + ExtractedFolderName + counter.ToString("D6") + entry.FullName.Substring(entry.FullName.Length - 4));
                            counter++;
                        }
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
    }
}
