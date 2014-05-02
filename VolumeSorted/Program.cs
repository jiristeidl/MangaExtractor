using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolumeSorted
{
    class Program
    {
        static void Main(string[] args)
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
            Console.ReadKey();
        }
    }
}
