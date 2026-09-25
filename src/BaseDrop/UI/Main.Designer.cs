namespace BaseDrop
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            ConverterBox = new System.Windows.Forms.Label();
            FormatLooping = new System.Windows.Forms.CheckBox();
            FormatXWMA = new System.Windows.Forms.RadioButton();
            FormatADPCM = new System.Windows.Forms.RadioButton();
            ExportFolderLabel = new System.Windows.Forms.Label();
            ExportFolderPath = new System.Windows.Forms.TextBox();
            ExportFolderBrowse = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // ConverterBox
            // 
            ConverterBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            ConverterBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            ConverterBox.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            ConverterBox.Location = new System.Drawing.Point(14, 81);
            ConverterBox.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            ConverterBox.Name = "ConverterBox";
            ConverterBox.Size = new System.Drawing.Size(642, 320);
            ConverterBox.TabIndex = 2;
            ConverterBox.Text = "Drop audio files here";
            ConverterBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormatLooping
            // 
            FormatLooping.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            FormatLooping.Location = new System.Drawing.Point(531, 14);
            FormatLooping.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            FormatLooping.Name = "FormatLooping";
            FormatLooping.Size = new System.Drawing.Size(126, 20);
            FormatLooping.TabIndex = 3;
            FormatLooping.Text = "Mark as looping";
            // 
            // FormatXWMA
            // 
            FormatXWMA.Location = new System.Drawing.Point(19, 40);
            FormatXWMA.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            FormatXWMA.Name = "FormatXWMA";
            FormatXWMA.Size = new System.Drawing.Size(290, 20);
            FormatXWMA.TabIndex = 1;
            FormatXWMA.Text = "Export XWMA - Used for everything else";
            // 
            // FormatADPCM
            // 
            FormatADPCM.Checked = true;
            FormatADPCM.Location = new System.Drawing.Point(19, 14);
            FormatADPCM.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            FormatADPCM.Name = "FormatADPCM";
            FormatADPCM.Size = new System.Drawing.Size(365, 20);
            FormatADPCM.TabIndex = 0;
            FormatADPCM.TabStop = true;
            FormatADPCM.Text = "Export ADPCM - Used for player audio and music mostly";
            //
            // ExportFolderLabel
            //
            ExportFolderLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            ExportFolderLabel.AutoSize = true;
            ExportFolderLabel.Location = new System.Drawing.Point(14, 419);
            ExportFolderLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            ExportFolderLabel.Name = "ExportFolderLabel";
            ExportFolderLabel.Size = new System.Drawing.Size(80, 15);
            ExportFolderLabel.TabIndex = 4;
            ExportFolderLabel.Text = "Export folder:";
            //
            // ExportFolderPath
            //
            ExportFolderPath.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            ExportFolderPath.Location = new System.Drawing.Point(102, 415);
            ExportFolderPath.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ExportFolderPath.Name = "ExportFolderPath";
            ExportFolderPath.ReadOnly = true;
            ExportFolderPath.Size = new System.Drawing.Size(456, 23);
            ExportFolderPath.TabIndex = 5;
            //
            // ExportFolderBrowse
            //
            ExportFolderBrowse.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            ExportFolderBrowse.Location = new System.Drawing.Point(566, 414);
            ExportFolderBrowse.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ExportFolderBrowse.Name = "ExportFolderBrowse";
            ExportFolderBrowse.Size = new System.Drawing.Size(90, 25);
            ExportFolderBrowse.TabIndex = 6;
            ExportFolderBrowse.Text = "Browse...";
            ExportFolderBrowse.UseVisualStyleBackColor = true;
            ExportFolderBrowse.Click += ExportFolderBrowse_Click;
            //
            // Main
            // 
            AllowDrop = true;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(672, 452);
            Controls.Add(ExportFolderBrowse);
            Controls.Add(ExportFolderPath);
            Controls.Add(ExportFolderLabel);
            Controls.Add(FormatLooping);
            Controls.Add(ConverterBox);
            Controls.Add(FormatXWMA);
            Controls.Add(FormatADPCM);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MaximumSize = new System.Drawing.Size(688, 491);
            MinimumSize = new System.Drawing.Size(688, 491);
            Name = "Main";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "BassDrop - BO1 Sound Transcoder";
            FormClosing += Main_FormClosing;
            DragDrop += Main_DragDrop;
            DragOver += Main_DragOver;
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton FormatADPCM;
        private System.Windows.Forms.RadioButton FormatXWMA;
        private System.Windows.Forms.Label ConverterBox;
        private System.Windows.Forms.CheckBox FormatLooping;
        private System.Windows.Forms.Label ExportFolderLabel;
        private System.Windows.Forms.TextBox ExportFolderPath;
        private System.Windows.Forms.Button ExportFolderBrowse;
    }
}

