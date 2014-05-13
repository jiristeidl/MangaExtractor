using System.Collections.Generic;
using System.IO;

namespace NewExtractorWithUI
{
    internal class VolumeSorter
    {
        private long maxVolumeSize;
        private List<string> output;
        private int volumeNumber;
        private string directoryToSort;

        public string DirectoryToSort
        {
            get
            {
                if (string.IsNullOrEmpty(directoryToSort)) return Directory.GetCurrentDirectory() + @"\Extracted\";
                else return directoryToSort;
            }
            set
            {
                directoryToSort = value;
            }
        }

        public int MaxVolumeSize
        {
            get
            {
                if (maxVolumeSize == 0) return 150;
                else
                {
                    return (int)(maxVolumeSize / 1048576);
                }
            }
            set
            {
                maxVolumeSize = value * 1048576;
            }
        }

        public string[] Output
        {
            get
            {
                if (output == null || output.Count == 0)
                    return new string[2] { "No image to extract found", "" };
                else
                    return output.ToArray();
            }
        }

        public VolumeSorter()
        {
            volumeNumber = 1;
            output = new List<string>();
        }

        public void Sort()
        {
            while (Directory.Exists(DirectoryToSort) && (Directory.GetFiles(DirectoryToSort, "*", SearchOption.TopDirectoryOnly).Length != 0))
            {
                string[] images = Directory.GetFiles(DirectoryToSort, "*", SearchOption.TopDirectoryOnly);
                List<string> volumeFiles = new List<string>();
                long totalFileSize = 0;
                for (int i = 0; i < images.Length; i++)
                {
                    FileInfo file = new FileInfo(images[i]);
                    totalFileSize += file.Length;
                    if (totalFileSize <= MaxVolumeSize * 1048576)
                    {
                        volumeFiles.Add(images[i]);
                    }
                    else
                    {
                        break;
                    }
                }
                string volumeDirectory = DirectoryToSort + @"/volume_" + volumeNumber.ToString("D3") + @"/";
                Directory.CreateDirectory(volumeDirectory);
                output.Add("Copying files for Volume " + volumeNumber);
                volumeNumber++;
                foreach (string fileName in volumeFiles)
                {
                    File.Move(fileName, volumeDirectory + fileName.Substring(fileName.Length - 10));
                }
            }
        }
    }
}