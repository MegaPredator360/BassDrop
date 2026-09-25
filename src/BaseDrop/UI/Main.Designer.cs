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
            this.ConverterBox = new System.Windows.Forms.PictureBox();
            this.FormatLooping = new System.Windows.Forms.CheckBox();
            this.FormatXWMA = new System.Windows.Forms.RadioButton();
            this.FormatADPCM = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.ConverterBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ConverterBox
            // 
            this.ConverterBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ConverterBox.Image = global::BaseDrop.Properties.Resources.ConvertDrop;
            this.ConverterBox.Location = new System.Drawing.Point(12, 70);
            this.ConverterBox.Name = "ConverterBox";
            this.ConverterBox.Size = new System.Drawing.Size(551, 278);
            this.ConverterBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ConverterBox.TabIndex = 2;
            this.ConverterBox.TabStop = false;
            // 
            // FormatLooping
            // 
            this.FormatLooping.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FormatLooping.Checked = false;
            this.FormatLooping.Location = new System.Drawing.Point(455, 12);
            this.FormatLooping.Name = "FormatLooping";
            this.FormatLooping.Size = new System.Drawing.Size(108, 17);
            this.FormatLooping.TabIndex = 3;
            this.FormatLooping.Text = "Mark as looping";
            // 
            // FormatXWMA
            // 
            this.FormatXWMA.Checked = false;
            this.FormatXWMA.Location = new System.Drawing.Point(16, 35);
            this.FormatXWMA.Name = "FormatXWMA";
            this.FormatXWMA.Size = new System.Drawing.Size(249, 17);
            this.FormatXWMA.TabIndex = 1;
            this.FormatXWMA.Text = "Export XWMA - Used for everything else";
            // 
            // FormatADPCM
            // 
            this.FormatADPCM.Checked = true;
            this.FormatADPCM.Location = new System.Drawing.Point(16, 12);
            this.FormatADPCM.Name = "FormatADPCM";
            this.FormatADPCM.Size = new System.Drawing.Size(313, 17);
            this.FormatADPCM.TabIndex = 0;
            this.FormatADPCM.Text = "Export ADPCM - Used for player audio and music mostly";
            // 
            // Main
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 361);
            this.Controls.Add(this.FormatLooping);
            this.Controls.Add(this.ConverterBox);
            this.Controls.Add(this.FormatXWMA);
            this.Controls.Add(this.FormatADPCM);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(592, 400);
            this.MinimumSize = new System.Drawing.Size(592, 400);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BassDrop - BO1 Sound Transcoder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Main_DragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.Main_DragOver);
            ((System.ComponentModel.ISupportInitialize)(this.ConverterBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton FormatADPCM;
        private System.Windows.Forms.RadioButton FormatXWMA;
        private System.Windows.Forms.PictureBox ConverterBox;
        private System.Windows.Forms.CheckBox FormatLooping;
    }
}

