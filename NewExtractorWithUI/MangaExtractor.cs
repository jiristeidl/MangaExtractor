using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Data.Linq;

namespace NewExtractorWithUI
{
    class MangaExtractor
    {
        string rootDirectory;
        string extractedFolderName;
        string[] imgExtensions;
        string[] extensions = {"img","png"};
        string[] otherArchives = { "rar","7z" };        
        List<File> filesToExtract;
        List<File> imagesToExtract;
        int counter;

        public string[] ZipFiles
        {
            get
            {
                if (filesToExtract == null || filesToExtract.Count == 0) return new string[2] { "No Files Found", "" };
                else
                {
                    string[] results = new string[filesToExtract.Count];
                    for (int i = 0; i < filesToExtract.Count; i++)
                    {
                        results[i] = filesToExtract[i].Priority + " - " + filesToExtract[i].Name;
                    }
                    return results;
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
                    string[] results = new string[imagesToExtract.Count];
                    for (int i = 0; i < imagesToExtract.Count; i++)
                    {
                        results[i] = imagesToExtract[i].Priority + " - " + imagesToExtract[i].Name;
                    }
                    return results;
                }
            }
        }

        public MangaExtractor()
        {
            counter = 1;
            filesToExtract = new List<File>();
        }
        public void Extract()
        {            
            filesToExtract = getZipFiles();
            imagesToExtract = extractPictures(filesToExtract);
        }

        private List<File> extractPictures(List<File> filesToExtract)
        {
            //throw new NotImplementedException();
            List<File> allPictures = new List<File>();
            string[] archives = getValuesFromList(filesToExtract);
            File result;

            foreach (string archive in archives)
            {
                using (ZipArchive oneArchive = ZipFile.OpenRead(archive))
                {
                    string[] zipFiles = Directory.GetFiles(RootDirectory, "*.zip", SearchOption.AllDirectories);
                    foreach (ZipArchiveEntry entry in oneArchive.Entries)
                    {
                        result = new File();
                        int length = entry.FullName.Length;
                        string numberString = "";
                        for (int i = length - 1; i != 0; i--)
                        {
                            if (char.IsDigit(entry.FullName[i]))
                            {
                                numberString = entry.FullName[i] + numberString;
                            }
                            if ((!string.IsNullOrEmpty(numberString) && !char.IsDigit(entry.FullName[i])) || entry.FullName[i] == '\\') break;
                        }
                        if (string.IsNullOrEmpty(numberString))
                        {
                            numberString = "-1";
                            break;
                        }
                        result.Name = entry.FullName;
                        result.Priority = int.Parse(numberString);
                        allPictures.Add(result);
                    }                    
                    //foreach (ZipArchiveEntry entry in oneArchive.Entries)
                    //{
                    //    if (isPicture(entry.FullName))
                    //    {
                    //        Console.WriteLine(RootDirectory + ExtractedFolderName + counter.ToString("D6") + entry.FullName.Substring(entry.FullName.Length - 4));
                    //        entry.ExtractToFile(RootDirectory + ExtractedFolderName + counter.ToString("D6") + entry.FullName.Substring(entry.FullName.Length - 4));
                    //        counter++;
                    //    }
                    //}
                }                
            }
            return allPictures;
        }
        private string[] getValuesFromList(List<File> filesToExtract)
        {
            string[] results = new string[filesToExtract.Count];
            filesToExtract.Sort((x, y) => x.Priority.CompareTo(y.Priority));
            for (int i = 0; i < filesToExtract.Count; i++)
            {
                results[i] = filesToExtract[i].Name;
            }
            return results;
        }
        private List<File> getZipFiles()
        {
            List<File> allZipFiles = new List<File>();
            string[] zipFiles = Directory.GetFiles(RootDirectory, "*.zip", SearchOption.AllDirectories);
            File result;
            foreach(string entry in zipFiles)
            {
                result = new File();
                int length = entry.Length;
                string numberString = "";
                for (int i = length - 1; i != 0; i--)
                {
                    if (char.IsDigit(entry[i]))
                    {
                        numberString = entry[i] + numberString;
                    }
                    if ((!string.IsNullOrEmpty(numberString) && !char.IsDigit(entry[i])) || entry[i]=='\\') break;
                }
                if (string.IsNullOrEmpty(numberString)) numberString = "-1";
                result.Priority = int.Parse(numberString);
                result.Name = entry;
                allZipFiles.Add(result);
            }
            return allZipFiles;
        }
        //private int oldextractPics(string[] p, string topDirectory, int counter)
        //{
        //    Directory.CreateDirectory(topDirectory + ExtractedFolderName);
        //    string directoryToExtractTo = topDirectory + ExtractedFolderName;
        //    foreach (string archive in p)
        //    {
        //        using (ZipArchive oneArchive = ZipFile.OpenRead(archive))
        //        {
        //            foreach (ZipArchiveEntry entry in oneArchive.Entries)
        //            {
        //                if (isPicture(entry.FullName))
        //                {
        //                    Console.WriteLine(topDirectory + ExtractedFolderName + counter.ToString("D6") + entry.FullName.Substring(entry.FullName.Length - 4));
        //                    entry.ExtractToFile(topDirectory + ExtractedFolderName + counter.ToString("D6") + entry.FullName.Substring(entry.FullName.Length - 4));
        //                    counter++;
        //                }
        //            }
        //        }
        //    }
        //    return counter;
        //}
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
