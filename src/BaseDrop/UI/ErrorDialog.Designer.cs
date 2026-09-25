namespace BaseDrop
{
    partial class ErrorDialog
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
            IconBox = new System.Windows.Forms.PictureBox();
            MessageLabel = new System.Windows.Forms.Label();
            DetailsBox = new System.Windows.Forms.TextBox();
            OkButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)IconBox).BeginInit();
            SuspendLayout();
            //
            // IconBox
            //
            IconBox.Location = new System.Drawing.Point(16, 16);
            IconBox.Name = "IconBox";
            IconBox.Size = new System.Drawing.Size(32, 32);
            IconBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            IconBox.TabIndex = 0;
            IconBox.TabStop = false;
            //
            // MessageLabel
            //
            MessageLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            MessageLabel.Location = new System.Drawing.Point(60, 16);
            MessageLabel.Name = "MessageLabel";
            MessageLabel.Size = new System.Drawing.Size(404, 32);
            MessageLabel.TabIndex = 1;
            MessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // DetailsBox
            //
            DetailsBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            DetailsBox.Location = new System.Drawing.Point(60, 56);
            DetailsBox.Multiline = true;
            DetailsBox.Name = "DetailsBox";
            DetailsBox.ReadOnly = true;
            DetailsBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            DetailsBox.Size = new System.Drawing.Size(404, 176);
            DetailsBox.TabIndex = 2;
            DetailsBox.WordWrap = false;
            //
            // OkButton
            //
            OkButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            OkButton.Location = new System.Drawing.Point(374, 244);
            OkButton.Name = "OkButton";
            OkButton.Size = new System.Drawing.Size(90, 27);
            OkButton.TabIndex = 0;
            OkButton.Text = "OK";
            OkButton.UseVisualStyleBackColor = true;
            //
            // ErrorDialog
            //
            AcceptButton = OkButton;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = OkButton;
            ClientSize = new System.Drawing.Size(480, 283);
            Controls.Add(OkButton);
            Controls.Add(DetailsBox);
            Controls.Add(MessageLabel);
            Controls.Add(IconBox);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ErrorDialog";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "BassDrop";
            ((System.ComponentModel.ISupportInitialize)IconBox).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox IconBox;
        private System.Windows.Forms.Label MessageLabel;
        private System.Windows.Forms.TextBox DetailsBox;
        private System.Windows.Forms.Button OkButton;
    }
}
