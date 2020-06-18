using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;

namespace lab13
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            foundFilesListBox.Items.Clear();
            searchButton.Enabled = false;
            fileNameTextBox.Enabled = false;

            var fileName = fileNameTextBox.Text.Trim();
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (var drive in allDrives)
            {
                MessageBox.Show(drive.Name);
                if (drive.Name != "E:\\")
                {
                    var allFoundFiles = FilesFinder.SafeEnumerateFiles(drive.Name, fileName, SearchOption.AllDirectories);
                    foreach (var file in allFoundFiles)
                    {
                        //MessageBox.Show(file);
                        foundFilesListBox.Items.Add(file);
                    }
                }
            }
            MessageBox.Show($"Поиск завершён, найдено файлов: {foundFilesListBox.Items.Count}");

            searchButton.Enabled = true;
            fileNameTextBox.Enabled = true;
        }

        private void displayButton_Click(object sender, EventArgs e)
        {
            if (foundFilesListBox.Items.Count != 0)
            {
                var filePath = (string)foundFilesListBox.SelectedItem;
                var content = File.ReadAllText(filePath);
                fileSourceTextBox.Text = content;
            }
        }

        private void compressButton_Click(object sender, EventArgs e)
        {
            if (foundFilesListBox.Items.Count != 0)
            {
                var filePath = (string)foundFilesListBox.SelectedItem;
                var fileToCompress = new FileInfo(filePath);
                Archiver.Compress(fileToCompress);
            }
        }
    }
}
