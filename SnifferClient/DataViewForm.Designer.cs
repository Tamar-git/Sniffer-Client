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
            this.introLabel = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.hexLabel = new System.Windows.Forms.Label();
            this.asciiLabel = new System.Windows.Forms.Label();
            this.hexIntroLabel = new System.Windows.Forms.Label();
            this.asciiIntroLabel = new System.Windows.Forms.Label();
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
            this.closeButton.Location = new System.Drawing.Point(1253, 715);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 1;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // hexLabel
            // 
            this.hexLabel.Location = new System.Drawing.Point(41, 101);
            this.hexLabel.Name = "hexLabel";
            this.hexLabel.Size = new System.Drawing.Size(625, 610);
            this.hexLabel.TabIndex = 2;
            this.hexLabel.Text = "label1";
            // 
            // asciiLabel
            // 
            this.asciiLabel.Location = new System.Drawing.Point(692, 101);
            this.asciiLabel.Name = "asciiLabel";
            this.asciiLabel.Size = new System.Drawing.Size(625, 610);
            this.asciiLabel.TabIndex = 3;
            this.asciiLabel.Text = "label1";
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
            // DataViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1340, 750);
            this.Controls.Add(this.asciiIntroLabel);
            this.Controls.Add(this.hexIntroLabel);
            this.Controls.Add(this.asciiLabel);
            this.Controls.Add(this.hexLabel);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.introLabel);
            this.Name = "DataViewForm";
            this.Text = "DataViewForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label introLabel;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label hexLabel;
        private System.Windows.Forms.Label asciiLabel;
        private System.Windows.Forms.Label hexIntroLabel;
        private System.Windows.Forms.Label asciiIntroLabel;
    }
}