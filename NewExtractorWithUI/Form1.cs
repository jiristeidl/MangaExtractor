using System;
using System.Windows.Forms;

namespace NewExtractorWithUI
{
    public partial class Form1 : Form
    {
        private MangaExtractor extractor;

        public Form1()
        {
            InitializeComponent();
            extractor = new MangaExtractor();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            extractor.Extract();
            results.Lines = extractor.Images;
            this.Refresh();
        }
    }
}