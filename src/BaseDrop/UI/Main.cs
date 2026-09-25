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
        // Whether a conversion is running
        private bool IsConverting = false;

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
            // Don't let the user close mid conversion
            if (IsConverting && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                return;
            }
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

        private async void Main_DragDrop(object sender, DragEventArgs e)
        {
            // Ignore drops while converting
            if (IsConverting)
                return;
            // Get points
            int x = this.PointToClient(new Point(e.X, e.Y)).X;
            int y = this.PointToClient(new Point(e.X, e.Y)).Y;
            // Check
            if (x >= ConverterBox.Location.X && x <= ConverterBox.Location.X + ConverterBox.Width && y >= ConverterBox.Location.Y && y <= ConverterBox.Location.Y + ConverterBox.Height)
            {
                // Convert them
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                // Run the converter
                if (files != null && files.Length > 0)
                {
                    await ConvertFiles(files);
                }
            }
        }

        private async Task ConvertFiles(string[] Files)
        {
            // Lock the UI
            SetConverting(true);
            try
            {
                // Build the converter with the current options
                var Converter = new AudioConverter(ResultDirectory, WorkingDirectory, this.FormatADPCM.Checked, this.FormatXWMA.Checked, this.FormatLooping.Checked);
                // Report back to the UI thread
                var Progress = new Progress<ConversionStatus>(ShowStatus);
                // Convert off the UI thread
                var Failures = await Task.Run(() => Converter.Run(Files, Progress));
                // Let the user know if anything went wrong
                if (Failures.Count > 0)
                {
                    MessageBox.Show(this, "Some files couldn't be converted:\n\n" + string.Join("\n", Failures.Take(15)) + (Failures.Count > 15 ? "\n..." : ""), "BassDrop", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "The conversion failed: " + ex.Message, "BassDrop", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Unlock the UI
                SetConverting(false);
            }
        }

        private void ShowStatus(ConversionStatus Status)
        {
            // Show what's being converted
            ConverterBox.Text = $"Converting {Status.FileNumber} of {Status.FileCount}...\n{Status.FileName}";
            // No progress yet, animate so it's clear something is happening
            if (Status.Percent <= 0)
            {
                ConversionProgress.Style = ProgressBarStyle.Marquee;
            }
            else
            {
                ConversionProgress.Style = ProgressBarStyle.Continuous;
                ConversionProgress.Value = Math.Clamp(Status.Percent, 0, 100);
            }
        }

        private void SetConverting(bool Converting)
        {
            // Set
            IsConverting = Converting;
            // Toggle everything the user could change mid conversion
            FormatADPCM.Enabled = !Converting;
            FormatXWMA.Enabled = !Converting;
            FormatLooping.Enabled = !Converting;
            ExportFolderPath.Enabled = !Converting;
            ExportFolderBrowse.Enabled = !Converting;
            ConverterBox.Enabled = !Converting;
            ConverterBox.Text = Converting ? "Converting..." : "Drop audio files here";
            if (Converting)
            {
                // Start the bar over for the new conversion
                ConversionProgress.Style = ProgressBarStyle.Marquee;
                ConversionProgress.Value = 0;
                ConversionProgress.Visible = true;
            }
            else
            {
                // Done, keep the bar full until the next conversion
                ConversionProgress.Style = ProgressBarStyle.Continuous;
                ConversionProgress.Value = 100;
            }
        }

        private void Main_DragOver(object sender, DragEventArgs e)
        {
            // Get points
            int x = this.PointToClient(new Point(e.X, e.Y)).X;
            int y = this.PointToClient(new Point(e.X, e.Y)).Y;
            // Check
            if (!IsConverting && x >= ConverterBox.Location.X && x <= ConverterBox.Location.X + ConverterBox.Width && y >= ConverterBox.Location.Y && y <= ConverterBox.Location.Y + ConverterBox.Height)
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
