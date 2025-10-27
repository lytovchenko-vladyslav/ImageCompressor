namespace ImageCompressor.UI
{
    partial class CompressorUI
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CompressorUI));
            selectFileButton = new Button();
            sourcePathTextBox = new TextBox();
            qualityNumeric = new NumericUpDown();
            compressButton = new Button();
            statusLabel = new Label();
            qualityLabel = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            statusL = new Label();
            panel3 = new Panel();
            ((System.ComponentModel.ISupportInitialize)qualityNumeric).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // selectFileButton
            // 
            selectFileButton.BackColor = Color.LightSeaGreen;
            selectFileButton.Font = new Font("Showcard Gothic", 11F, FontStyle.Bold);
            selectFileButton.ForeColor = SystemColors.ButtonHighlight;
            selectFileButton.Location = new Point(13, 52);
            selectFileButton.Name = "selectFileButton";
            selectFileButton.Size = new Size(137, 52);
            selectFileButton.TabIndex = 0;
            selectFileButton.Text = "Select file";
            selectFileButton.UseVisualStyleBackColor = false;
            selectFileButton.Click += selectFileButton_Click_1;
            // 
            // sourcePathTextBox
            // 
            sourcePathTextBox.Location = new Point(16, 67);
            sourcePathTextBox.Name = "sourcePathTextBox";
            sourcePathTextBox.ReadOnly = true;
            sourcePathTextBox.Size = new Size(278, 27);
            sourcePathTextBox.TabIndex = 1;
            // 
            // qualityNumeric
            // 
            qualityNumeric.Font = new Font("Showcard Gothic", 10F, FontStyle.Bold);
            qualityNumeric.Location = new Point(16, 115);
            qualityNumeric.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            qualityNumeric.Name = "qualityNumeric";
            qualityNumeric.Size = new Size(64, 28);
            qualityNumeric.TabIndex = 2;
            qualityNumeric.TextAlign = HorizontalAlignment.Center;
            qualityNumeric.Value = new decimal(new int[] { 80, 0, 0, 0 });
            // 
            // compressButton
            // 
            compressButton.AllowDrop = true;
            compressButton.BackColor = Color.LightGreen;
            compressButton.Font = new Font("Showcard Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            compressButton.ForeColor = SystemColors.ControlText;
            compressButton.Location = new Point(35, 245);
            compressButton.Name = "compressButton";
            compressButton.Size = new Size(416, 45);
            compressButton.TabIndex = 3;
            compressButton.Text = "Compress";
            compressButton.UseVisualStyleBackColor = false;
            compressButton.Click += compressButton_Click;
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.BackColor = Color.Chartreuse;
            statusLabel.Font = new Font("Showcard Gothic", 20F, FontStyle.Bold);
            statusLabel.Location = new Point(217, 29);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(136, 43);
            statusLabel.TabIndex = 4;
            statusLabel.Text = "Ready";
            // 
            // qualityLabel
            // 
            qualityLabel.AutoSize = true;
            qualityLabel.Font = new Font("Showcard Gothic", 11F, FontStyle.Bold);
            qualityLabel.Location = new Point(13, 120);
            qualityLabel.Name = "qualityLabel";
            qualityLabel.Size = new Size(99, 23);
            qualityLabel.TabIndex = 5;
            qualityLabel.Text = "Quality";
            // 
            // panel1
            // 
            panel1.BackColor = Color.LightSlateGray;
            panel1.Controls.Add(qualityLabel);
            panel1.Controls.Add(selectFileButton);
            panel1.Location = new Point(-1, -3);
            panel1.Name = "panel1";
            panel1.Size = new Size(171, 223);
            panel1.TabIndex = 6;
            // 
            // panel2
            // 
            panel2.BackColor = Color.LightSlateGray;
            panel2.Controls.Add(qualityNumeric);
            panel2.Controls.Add(sourcePathTextBox);
            panel2.Location = new Point(176, -3);
            panel2.Name = "panel2";
            panel2.Size = new Size(307, 223);
            panel2.TabIndex = 7;
            // 
            // statusL
            // 
            statusL.AutoSize = true;
            statusL.Font = new Font("Showcard Gothic", 20F, FontStyle.Bold);
            statusL.Location = new Point(48, 29);
            statusL.Name = "statusL";
            statusL.Size = new Size(163, 43);
            statusL.TabIndex = 8;
            statusL.Text = "Status:";
            // 
            // panel3
            // 
            panel3.BackColor = Color.LightSlateGray;
            panel3.Controls.Add(statusL);
            panel3.Controls.Add(statusLabel);
            panel3.Location = new Point(35, 315);
            panel3.Name = "panel3";
            panel3.Size = new Size(416, 100);
            panel3.TabIndex = 9;
            // 
            // CompressorUI
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveCaption;
            ClientSize = new Size(494, 465);
            Controls.Add(compressButton);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Controls.Add(panel3);
            ForeColor = SystemColors.ActiveCaptionText;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "CompressorUI";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Image Compressor";
            ((System.ComponentModel.ISupportInitialize)qualityNumeric).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button selectFileButton;
        private TextBox sourcePathTextBox;
        private NumericUpDown qualityNumeric;
        private Button compressButton;
        private Label statusLabel;
        private Label qualityLabel;
        private Panel panel1;
        private Panel panel2;
        private Label statusL;
        private Panel panel3;
    }
}
