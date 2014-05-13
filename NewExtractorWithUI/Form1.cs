using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace NewExtractorWithUI
{
    public partial class Form1 : Form
    {
        private MangaExtractor extractor;
        private VolumeSorter sorter;

        public Form1()
        {
            InitializeComponent();
            extractor = new MangaExtractor();
            sorter = new VolumeSorter();
            folderBrowserDialog1.SelectedPath = Directory.GetCurrentDirectory();
            selectedFolderLbl.Text = folderBrowserDialog1.SelectedPath.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                selectedFolderLbl.Text = folderBrowserDialog1.SelectedPath.ToString();
                extractor.RootDirectory = folderBrowserDialog1.SelectedPath;
            }
            this.Refresh();
        }

        private void extractBtn_Click(object sender, EventArgs e)
        {
            startExtraction();
        }

        private void startExtraction()
        {
            extractor.Extract();
            List<string> result = new List<string>(extractor.ZipFiles);
            result.AddRange(extractor.Images);
            results.Lines = result.ToArray();
            this.Refresh();
        }

        private void extractedFolderName_TextChanged(object sender, EventArgs e)
        {
            extractor.ExtractedFolderName = "\\" + extractedFolderName.Text + "\\";
            sorter.DirectoryToSort = extractor.RootDirectory + "\\" + extractedFolderName.Text + "\\";
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            sorter.MaxVolumeSize = (int)numericUpDown1.Value;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            sorter.Sort();
            results.Lines = sorter.Output;
            this.Refresh();
        }
    }
}