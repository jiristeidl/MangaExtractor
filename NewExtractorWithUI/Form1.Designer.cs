namespace NewExtractorWithUI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.browseBtn = new System.Windows.Forms.Button();
            this.results = new System.Windows.Forms.TextBox();
            this.selectedFolderLbl = new System.Windows.Forms.Label();
            this.extractedFolderName = new System.Windows.Forms.TextBox();
            this.extractBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // browseBtn
            // 
            this.browseBtn.Location = new System.Drawing.Point(16, 29);
            this.browseBtn.Name = "browseBtn";
            this.browseBtn.Size = new System.Drawing.Size(75, 20);
            this.browseBtn.TabIndex = 0;
            this.browseBtn.Text = "Browse...";
            this.browseBtn.UseVisualStyleBackColor = true;
            this.browseBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // results
            // 
            this.results.AcceptsReturn = true;
            this.results.Location = new System.Drawing.Point(12, 72);
            this.results.Multiline = true;
            this.results.Name = "results";
            this.results.ReadOnly = true;
            this.results.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.results.Size = new System.Drawing.Size(716, 354);
            this.results.TabIndex = 1;
            // 
            // selectedFolderLbl
            // 
            this.selectedFolderLbl.AutoSize = true;
            this.selectedFolderLbl.Location = new System.Drawing.Point(13, 13);
            this.selectedFolderLbl.Name = "selectedFolderLbl";
            this.selectedFolderLbl.Size = new System.Drawing.Size(35, 13);
            this.selectedFolderLbl.TabIndex = 2;
            this.selectedFolderLbl.Text = "label1";
            // 
            // extractedFolderName
            // 
            this.extractedFolderName.Location = new System.Drawing.Point(98, 29);
            this.extractedFolderName.Name = "extractedFolderName";
            this.extractedFolderName.Size = new System.Drawing.Size(100, 20);
            this.extractedFolderName.TabIndex = 3;
            this.extractedFolderName.Text = "Extracted";
            this.extractedFolderName.TextChanged += new System.EventHandler(this.extractedFolderName_TextChanged);
            // 
            // extractBtn
            // 
            this.extractBtn.Location = new System.Drawing.Point(205, 29);
            this.extractBtn.Name = "extractBtn";
            this.extractBtn.Size = new System.Drawing.Size(75, 20);
            this.extractBtn.TabIndex = 4;
            this.extractBtn.Text = "Extract!";
            this.extractBtn.UseVisualStyleBackColor = true;
            this.extractBtn.Click += new System.EventHandler(this.extractBtn_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.Location = new System.Drawing.Point(287, 29);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 20);
            this.button1.TabIndex = 5;
            this.button1.Text = "Sort!";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(369, 29);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 6;
            this.numericUpDown1.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 485);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.extractBtn);
            this.Controls.Add(this.extractedFolderName);
            this.Controls.Add(this.selectedFolderLbl);
            this.Controls.Add(this.results);
            this.Controls.Add(this.browseBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button browseBtn;
        private System.Windows.Forms.TextBox results;
        private System.Windows.Forms.Label selectedFolderLbl;
        private System.Windows.Forms.TextBox extractedFolderName;
        private System.Windows.Forms.Button extractBtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
    }
}

