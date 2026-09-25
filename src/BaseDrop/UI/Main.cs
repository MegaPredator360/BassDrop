using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaseDrop
{
    public partial class Main : Form
    {
        // Get the path for exporting
        private static string ApplicationDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        private static string WorkingDirectory = string.Empty;
        private static string ResultDirectory = string.Empty;

        public Main()
        {
            InitializeComponent();
            // Setup
            SetupDirectories();
            // Setup files
            SetupFiles();
        }

        private void SetupFiles()
        {
            // Extract the converters
            File.WriteAllBytes(Path.Combine(WorkingDirectory, "ADPCMEnc.exe"), Properties.Resources.ADPCMEncode);
            File.WriteAllBytes(Path.Combine(WorkingDirectory, "XWMAEnc.exe"), Properties.Resources.xWMAEncode);
        }

        private void SetupDirectories()
        {
            // Make directories
            WorkingDirectory = Path.Combine(Path.GetTempPath(), "BaseDrop");
            // Make it
            if (!Directory.Exists(WorkingDirectory))
            {
                // Make it
                Directory.CreateDirectory(WorkingDirectory);
            }
            // Result, use the user's saved folder if it's still usable
            string SavedDirectory = Properties.Settings.Default.ExportDirectory;
            if (string.IsNullOrEmpty(SavedDirectory) || !SetResultDirectory(SavedDirectory))
            {
                // Fall back to the default next to the app
                SetResultDirectory(DefaultResultDirectory);
            }
        }

        private static string DefaultResultDirectory => Path.Combine(ApplicationDirectory, "exported_files");

        private bool SetResultDirectory(string Folder)
        {
            // Make it and the specific dirs
            try
            {
                Directory.CreateDirectory(Path.Combine(Folder, "normal"));
                Directory.CreateDirectory(Path.Combine(Folder, "bo_ready"));
            }
            catch
            {
                // Not usable
                return false;
            }
            // Use it
            ResultDirectory = Folder;
            ExportFolderPath.Text = Folder;
            return true;
        }

        private void ExportFolderBrowse_Click(object sender, EventArgs e)
        {
            // Ask for the folder
            using (var Dialog = new FolderBrowserDialog())
            {
                Dialog.Description = "Select the export folder";
                Dialog.UseDescriptionForTitle = true;
                Dialog.InitialDirectory = ResultDirectory;
                if (Dialog.ShowDialog(this) != DialogResult.OK)
                    return;
                // Try and use it
                if (!SetResultDirectory(Dialog.SelectedPath))
                {
                    MessageBox.Show(this, "The selected folder can't be used for exporting.", "BassDrop", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // Remember it
                Properties.Settings.Default.ExportDirectory = Dialog.SelectedPath;
                Properties.Settings.Default.Save();
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Clean up
            if (Directory.Exists(WorkingDirectory))
            {
                // Delete
                try
                {
                    // Clean it up
                    Directory.Delete(WorkingDirectory, true);
                }
                catch
                {
                    // Nothing
                }
            }
        }

        private void Main_DragDrop(object sender, DragEventArgs e)
        {
            // Get points
            int x = this.PointToClient(new Point(e.X, e.Y)).X;
            int y = this.PointToClient(new Point(e.X, e.Y)).Y;
            // Check
            if (x >= ConverterBox.Location.X && x <= ConverterBox.Location.X + ConverterBox.Width && y >= ConverterBox.Location.Y && y <= ConverterBox.Location.Y + ConverterBox.Height)
            {
                // Convert them
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                // Show converter
                if (files != null && files.Length > 0)
                {
                    // Show it
                    new Converter(this, files, ResultDirectory, WorkingDirectory, this.FormatADPCM.Checked, this.FormatXWMA.Checked, this.FormatLooping.Checked).ShowDialog();
                }
            }
        }

        private void Main_DragOver(object sender, DragEventArgs e)
        {
            // Get points
            int x = this.PointToClient(new Point(e.X, e.Y)).X;
            int y = this.PointToClient(new Point(e.X, e.Y)).Y;
            // Check
            if (x >= ConverterBox.Location.X && x <= ConverterBox.Location.X + ConverterBox.Width && y >= ConverterBox.Location.Y && y <= ConverterBox.Location.Y + ConverterBox.Height)
            {
                // Allow it
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                // Nope
                e.Effect = DragDropEffects.None;
            }
        }
    }
}
