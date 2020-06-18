namespace lab13
{
    partial class MainForm
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
            this.searchButton = new System.Windows.Forms.Button();
            this.fileNameTextBox = new System.Windows.Forms.TextBox();
            this.pathLabel = new System.Windows.Forms.Label();
            this.foundFilesListBox = new System.Windows.Forms.ListBox();
            this.fileSourceTextBox = new System.Windows.Forms.TextBox();
            this.displayButton = new System.Windows.Forms.Button();
            this.compressButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(241, 15);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 0;
            this.searchButton.Text = "Искать";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // fileNameTextBox
            // 
            this.fileNameTextBox.Location = new System.Drawing.Point(82, 15);
            this.fileNameTextBox.Name = "fileNameTextBox";
            this.fileNameTextBox.Size = new System.Drawing.Size(142, 20);
            this.fileNameTextBox.TabIndex = 1;
            // 
            // pathLabel
            // 
            this.pathLabel.AutoSize = true;
            this.pathLabel.Location = new System.Drawing.Point(12, 18);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(64, 13);
            this.pathLabel.TabIndex = 2;
            this.pathLabel.Text = "Имя файла";
            // 
            // foundFilesListBox
            // 
            this.foundFilesListBox.FormattingEnabled = true;
            this.foundFilesListBox.Location = new System.Drawing.Point(15, 57);
            this.foundFilesListBox.Name = "foundFilesListBox";
            this.foundFilesListBox.Size = new System.Drawing.Size(301, 355);
            this.foundFilesListBox.TabIndex = 3;
            // 
            // fileSourceTextBox
            // 
            this.fileSourceTextBox.Location = new System.Drawing.Point(350, 57);
            this.fileSourceTextBox.Multiline = true;
            this.fileSourceTextBox.Name = "fileSourceTextBox";
            this.fileSourceTextBox.ReadOnly = true;
            this.fileSourceTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.fileSourceTextBox.Size = new System.Drawing.Size(420, 355);
            this.fileSourceTextBox.TabIndex = 4;
            // 
            // displayButton
            // 
            this.displayButton.Location = new System.Drawing.Point(350, 15);
            this.displayButton.Name = "displayButton";
            this.displayButton.Size = new System.Drawing.Size(92, 23);
            this.displayButton.TabIndex = 5;
            this.displayButton.Text = "Показать";
            this.displayButton.UseVisualStyleBackColor = true;
            this.displayButton.Click += new System.EventHandler(this.displayButton_Click);
            // 
            // compressButton
            // 
            this.compressButton.Location = new System.Drawing.Point(461, 15);
            this.compressButton.Name = "compressButton";
            this.compressButton.Size = new System.Drawing.Size(84, 23);
            this.compressButton.TabIndex = 6;
            this.compressButton.Text = "Сжать";
            this.compressButton.UseVisualStyleBackColor = true;
            this.compressButton.Click += new System.EventHandler(this.compressButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.compressButton);
            this.Controls.Add(this.displayButton);
            this.Controls.Add(this.fileSourceTextBox);
            this.Controls.Add(this.foundFilesListBox);
            this.Controls.Add(this.pathLabel);
            this.Controls.Add(this.fileNameTextBox);
            this.Controls.Add(this.searchButton);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Поиск файлов";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.TextBox fileNameTextBox;
        private System.Windows.Forms.Label pathLabel;
        private System.Windows.Forms.ListBox foundFilesListBox;
        private System.Windows.Forms.TextBox fileSourceTextBox;
        private System.Windows.Forms.Button displayButton;
        private System.Windows.Forms.Button compressButton;
    }
}

