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
        // Animates the progress bars that have nothing to show yet
        private readonly System.Windows.Forms.Timer MarqueeTimer = new System.Windows.Forms.Timer { Interval = 30 };
        private int MarqueeOffset = 0;

        // State of each row in the conversion table
        private enum FileState { Pending, Converting, Done, Failed }

        private class FileProgress
        {
            public FileState State = FileState.Pending;
            public int Percent = 0;
        }

        public Main()
        {
            InitializeComponent();
            MarqueeTimer.Tick += MarqueeTimer_Tick;
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
            // Fill the table with what we're about to convert
            FillConversionTable(Files);
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

        private void FillConversionTable(string[] Files)
        {
            ConversionTable.Rows.Clear();
            foreach (var FilePath in Files)
            {
                // Name, duration and size, the progress column is drawn from the row's tag
                bool IsFile = File.Exists(FilePath);
                string Duration = AudioInfo.FormatDuration(AudioInfo.GetDuration(FilePath));
                string Size = IsFile ? AudioInfo.FormatSize(new FileInfo(FilePath).Length) : "—";
                int Row = ConversionTable.Rows.Add(Path.GetFileName(FilePath), Duration, Size, null);
                ConversionTable.Rows[Row].Tag = new FileProgress();
            }
            ConversionTable.ClearSelection();
        }

        private void ShowStatus(ConversionStatus Status)
        {
            // Update the file's row
            var Row = ConversionTable.Rows[Status.FileIndex];
            var Progress = (FileProgress)Row.Tag;
            Progress.Percent = Status.FilePercent;
            if (Status.FileDone)
            {
                Progress.State = Status.Error == null ? FileState.Done : FileState.Failed;
                // Hover the bar to see why it failed
                Row.Cells[ProgressColumn.Index].ToolTipText = Status.Error ?? string.Empty;
            }
            else
            {
                Progress.State = FileState.Converting;
                // Keep the file being converted in view
                if (!Row.Displayed)
                    ConversionTable.FirstDisplayedScrollingRowIndex = Status.FileIndex;
            }
            ConversionTable.InvalidateCell(ProgressColumn.Index, Status.FileIndex);
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
            // The table replaces the drop box from the first conversion on, and stays up until the next one
            if (Converting)
            {
                ConverterBox.Visible = false;
                ConversionTable.Visible = true;
                MarqueeTimer.Start();
            }
            else
            {
                MarqueeTimer.Stop();
            }
        }

        private void MarqueeTimer_Tick(object sender, EventArgs e)
        {
            // Move the marquee on rows that are converting without progress to show yet
            MarqueeOffset += 4;
            foreach (DataGridViewRow Row in ConversionTable.Rows)
            {
                if (Row.Tag is FileProgress Progress && Progress.State == FileState.Converting && Progress.Percent == 0 && Row.Displayed)
                    ConversionTable.InvalidateCell(ProgressColumn.Index, Row.Index);
            }
        }

        private void ConversionTable_SelectionChanged(object sender, EventArgs e)
        {
            // The table is just for showing progress
            ConversionTable.ClearSelection();
        }

        private void ConversionTable_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Draw a progress bar in the progress column
            if (e.RowIndex < 0 || e.ColumnIndex != ProgressColumn.Index || ConversionTable.Rows[e.RowIndex].Tag is not FileProgress Progress)
                return;

            e.PaintBackground(e.ClipBounds, false);

            var Bar = Rectangle.Inflate(e.CellBounds, -6, -4);
            var Inner = Rectangle.Inflate(Bar, -2, -2);
            Color FillColor = Progress.State == FileState.Failed ? Color.IndianRed : SystemColors.Highlight;
            Color TextColor = e.CellStyle.ForeColor;
            string Text;

            // Track
            using (var Border = new Pen(SystemColors.ControlDark))
                e.Graphics.DrawRectangle(Border, Bar.X, Bar.Y, Bar.Width - 1, Bar.Height - 1);

            using (var Fill = new SolidBrush(FillColor))
            {
                switch (Progress.State)
                {
                    case FileState.Converting when Progress.Percent == 0:
                        // Nothing to measure yet, slide a block along the bar
                        int BlockWidth = Math.Max(Inner.Width / 4, 1);
                        int BlockX = Inner.X - BlockWidth + (MarqueeOffset % (Inner.Width + BlockWidth));
                        var Block = Rectangle.Intersect(new Rectangle(BlockX, Inner.Y, BlockWidth, Inner.Height), Inner);
                        if (!Block.IsEmpty)
                            e.Graphics.FillRectangle(Fill, Block);
                        Text = "Converting...";
                        break;
                    case FileState.Converting:
                        e.Graphics.FillRectangle(Fill, Inner.X, Inner.Y, Inner.Width * Math.Clamp(Progress.Percent, 0, 100) / 100, Inner.Height);
                        Text = Progress.Percent + "%";
                        break;
                    case FileState.Done:
                        e.Graphics.FillRectangle(Fill, Inner);
                        Text = "Done";
                        TextColor = Color.White;
                        break;
                    case FileState.Failed:
                        e.Graphics.FillRectangle(Fill, Inner);
                        Text = "Failed";
                        TextColor = Color.White;
                        break;
                    default:
                        Text = "Pending";
                        break;
                }
            }

            TextRenderer.DrawText(e.Graphics, Text, e.CellStyle.Font, Bar, TextColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine);
            e.Handled = true;
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
