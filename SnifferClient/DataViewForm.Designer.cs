namespace SnifferClient
{
    partial class DataViewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataViewForm));
            this.introLabel = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.hexIntroLabel = new System.Windows.Forms.Label();
            this.asciiIntroLabel = new System.Windows.Forms.Label();
            this.hexTextBox = new System.Windows.Forms.TextBox();
            this.asciiTextBox = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // introLabel
            // 
            this.introLabel.AutoSize = true;
            this.introLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.introLabel.Location = new System.Drawing.Point(38, 23);
            this.introLabel.Name = "introLabel";
            this.introLabel.Size = new System.Drawing.Size(126, 20);
            this.introLabel.TabIndex = 0;
            this.introLabel.Text = "Packet\'s Data:";
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(1264, 635);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 1;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // hexIntroLabel
            // 
            this.hexIntroLabel.AutoSize = true;
            this.hexIntroLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.hexIntroLabel.Location = new System.Drawing.Point(41, 71);
            this.hexIntroLabel.Name = "hexIntroLabel";
            this.hexIntroLabel.Size = new System.Drawing.Size(147, 17);
            this.hexIntroLabel.TabIndex = 4;
            this.hexIntroLabel.Text = "bytes as HexaDecimal";
            // 
            // asciiIntroLabel
            // 
            this.asciiIntroLabel.AutoSize = true;
            this.asciiIntroLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.asciiIntroLabel.Location = new System.Drawing.Point(692, 71);
            this.asciiIntroLabel.Name = "asciiIntroLabel";
            this.asciiIntroLabel.Size = new System.Drawing.Size(98, 17);
            this.asciiIntroLabel.TabIndex = 5;
            this.asciiIntroLabel.Text = "bytes as ASCII";
            // 
            // hexTextBox
            // 
            this.hexTextBox.Location = new System.Drawing.Point(44, 101);
            this.hexTextBox.Multiline = true;
            this.hexTextBox.Name = "hexTextBox";
            this.hexTextBox.ReadOnly = true;
            this.hexTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.hexTextBox.Size = new System.Drawing.Size(489, 520);
            this.hexTextBox.TabIndex = 6;
            // 
            // asciiTextBox
            // 
            this.asciiTextBox.Location = new System.Drawing.Point(695, 101);
            this.asciiTextBox.Multiline = true;
            this.asciiTextBox.Name = "asciiTextBox";
            this.asciiTextBox.ReadOnly = true;
            this.asciiTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.asciiTextBox.Size = new System.Drawing.Size(489, 520);
            this.asciiTextBox.TabIndex = 7;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SnifferClient.Properties.Resources.CAPCKET_logo_red_cut_removebg_preview;
            this.pictureBox1.Location = new System.Drawing.Point(1219, 31);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // DataViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1351, 670);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.asciiTextBox);
            this.Controls.Add(this.hexTextBox);
            this.Controls.Add(this.asciiIntroLabel);
            this.Controls.Add(this.hexIntroLabel);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.introLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DataViewForm";
            this.Text = "CAPCKET";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label introLabel;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label hexIntroLabel;
        private System.Windows.Forms.Label asciiIntroLabel;
        private System.Windows.Forms.TextBox hexTextBox;
        private System.Windows.Forms.TextBox asciiTextBox;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}