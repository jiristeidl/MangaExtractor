using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace NewExtractorWithUI
{
    internal class MangaExtractor
    {
        private string rootDirectory;
        private string extractedFolderName;
        private string[] imgExtensions;
        private string[] extensions = { "img", "png" };
        private string[] otherArchives = { "rar", "7z" };
        private List<CustomFile> filesToExtract;
        private List<CustomFile> imagesToExtract;
        private int counter;

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
                if (string.IsNullOrEmpty(extractedFolderName)) return @"\extracted\";
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
            filesToExtract = new List<CustomFile>();
        }

        public void Extract()
        {
            filesToExtract = getZipFiles();
            imagesToExtract = extractPictures(filesToExtract);
        }

        private List<CustomFile> extractPictures(List<CustomFile> filesToExtract)
        {
            //throw new NotImplementedException();
            List<CustomFile> allPictures = new List<CustomFile>();
            List<Image> currentPictures;
            string[] archives = getValuesFromList(filesToExtract);
            Image result;

            foreach (string archive in archives)
            {
                currentPictures = new List<Image>();
                using (ZipArchive oneArchive = ZipFile.OpenRead(archive))
                {
                    string[] zipFiles = Directory.GetFiles(RootDirectory, "*.zip", SearchOption.AllDirectories);
                    foreach (ZipArchiveEntry entry in oneArchive.Entries)
                    {
                        result = new Image();
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
                        }
                        result.Name = entry;
                        result.Priority = int.Parse(numberString);
                        currentPictures.Add(result);
                    }
                    currentPictures.Sort((x, y) => x.Priority.CompareTo(y.Priority));
                    allPictures.AddRange(getFileListFromImageList(currentPictures));
                    foreach (Image entry in currentPictures)
                    {
                        if (isPicture(entry.Name.FullName))
                        {
                            Directory.CreateDirectory(RootDirectory + ExtractedFolderName);
                            entry.Name.ExtractToFile(RootDirectory + ExtractedFolderName + counter.ToString("D6") + entry.Name.FullName.Substring(entry.Name.FullName.Length - 4));
                            counter++;
                        }
                    }
                }
            }
            return allPictures;
        }

        private List<CustomFile> getFileListFromImageList(List<Image> currentPictures)
        {
            List<CustomFile> results = new List<CustomFile>();
            CustomFile result;
            foreach (Image entry in currentPictures)
            {
                result = new CustomFile();
                result.Name = entry.Name.FullName;
                result.Priority = entry.Priority;
                results.Add(result);
            }
            return results;
        }

        private string[] getValuesFromList(List<CustomFile> filesToExtract)
        {
            string[] results = new string[filesToExtract.Count];
            filesToExtract.Sort((x, y) => x.Priority.CompareTo(y.Priority));
            for (int i = 0; i < filesToExtract.Count; i++)
            {
                results[i] = filesToExtract[i].Name;
            }
            return results;
        }

        private List<CustomFile> getZipFiles()
        {
            List<CustomFile> allZipFiles = new List<CustomFile>();
            string[] zipFiles = Directory.GetFiles(RootDirectory, "*.zip", SearchOption.AllDirectories);
            CustomFile result;
            foreach (string entry in zipFiles)
            {
                result = new CustomFile();
                int length = entry.Length;
                string numberString = "";
                for (int i = length - 1; i != 0; i--)
                {
                    if (char.IsDigit(entry[i]))
                    {
                        numberString = entry[i] + numberString;
                    }
                    if ((!string.IsNullOrEmpty(numberString) && !char.IsDigit(entry[i])) || entry[i] == '\\') break;
                }
                if (string.IsNullOrEmpty(numberString)) numberString = "-1";
                result.Priority = int.Parse(numberString);
                result.Name = entry;
                allZipFiles.Add(result);
            }
            return allZipFiles;
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