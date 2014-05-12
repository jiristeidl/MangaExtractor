using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewExtractorWithUI
{
    public partial class Form1 : Form
    {
        MangaExtractor extractor;
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
