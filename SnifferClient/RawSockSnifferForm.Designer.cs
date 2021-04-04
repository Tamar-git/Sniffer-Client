namespace SnifferClient
{
    partial class RawSockSnifferForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ColumnHeader ArrivalTime;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RawSockSnifferForm));
            this.listView1 = new System.Windows.Forms.ListView();
            this.SerialNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ProtocolName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Length = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SourceAdd = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DestinationAdd = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Checksum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.previousSniffComboBox = new System.Windows.Forms.ComboBox();
            this.chooseDateLabel = new System.Windows.Forms.Label();
            this.requestButton = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.stopToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.startToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.startPictureBox = new System.Windows.Forms.PictureBox();
            this.stopPictureBox = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ArrivalTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.startPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stopPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ArrivalTime
            // 
            ArrivalTime.Text = "Arrival Time";
            ArrivalTime.Width = 94;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.SerialNumber,
            this.ProtocolName,
            ArrivalTime,
            this.Length,
            this.SourceAdd,
            this.DestinationAdd,
            this.Checksum});
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 75);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1033, 463);
            this.listView1.TabIndex = 6;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.Click += new System.EventHandler(this.listView1_Click);
            // 
            // SerialNumber
            // 
            this.SerialNumber.Text = "No.";
            // 
            // ProtocolName
            // 
            this.ProtocolName.Text = "Protocol";
            // 
            // Length
            // 
            this.Length.Text = "Length";
            // 
            // SourceAdd
            // 
            this.SourceAdd.Text = "Source Address";
            this.SourceAdd.Width = 185;
            // 
            // DestinationAdd
            // 
            this.DestinationAdd.Text = "Destination Address";
            this.DestinationAdd.Width = 185;
            // 
            // Checksum
            // 
            this.Checksum.Text = "Checksum";
            this.Checksum.Width = 75;
            // 
            // previousSniffComboBox
            // 
            this.previousSniffComboBox.FormattingEnabled = true;
            this.previousSniffComboBox.Location = new System.Drawing.Point(919, 11);
            this.previousSniffComboBox.Name = "previousSniffComboBox";
            this.previousSniffComboBox.Size = new System.Drawing.Size(121, 21);
            this.previousSniffComboBox.TabIndex = 9;
            // 
            // chooseDateLabel
            // 
            this.chooseDateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.chooseDateLabel.Location = new System.Drawing.Point(789, 13);
            this.chooseDateLabel.Name = "chooseDateLabel";
            this.chooseDateLabel.Size = new System.Drawing.Size(124, 49);
            this.chooseDateLabel.TabIndex = 10;
            this.chooseDateLabel.Text = "Load previously captured packets:";
            // 
            // requestButton
            // 
            this.requestButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.requestButton.Location = new System.Drawing.Point(919, 38);
            this.requestButton.Name = "requestButton";
            this.requestButton.Size = new System.Drawing.Size(121, 23);
            this.requestButton.TabIndex = 11;
            this.requestButton.Text = "Request";
            this.requestButton.UseVisualStyleBackColor = true;
            this.requestButton.Click += new System.EventHandler(this.requestButton_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.statusLabel.Location = new System.Drawing.Point(249, 28);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(362, 41);
            this.statusLabel.TabIndex = 13;
            this.statusLabel.Text = "Please start capturing packets or load previously captured packets from a selecte" +
    "d date";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(235, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 22);
            this.label1.TabIndex = 14;
            this.label1.Text = "Status:";
            // 
            // startPictureBox
            // 
            this.startPictureBox.Image = global::SnifferClient.Properties.Resources.play_arrow_button_circle_86280;
            this.startPictureBox.Location = new System.Drawing.Point(23, 12);
            this.startPictureBox.Name = "startPictureBox";
            this.startPictureBox.Size = new System.Drawing.Size(100, 50);
            this.startPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.startPictureBox.TabIndex = 12;
            this.startPictureBox.TabStop = false;
            this.startToolTip.SetToolTip(this.startPictureBox, "start capturing packets");
            this.startPictureBox.Click += new System.EventHandler(this.startPictureBox_Click);
            // 
            // stopPictureBox
            // 
            this.stopPictureBox.Image = global::SnifferClient.Properties.Resources.red_square;
            this.stopPictureBox.Location = new System.Drawing.Point(129, 12);
            this.stopPictureBox.Name = "stopPictureBox";
            this.stopPictureBox.Size = new System.Drawing.Size(100, 50);
            this.stopPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.stopPictureBox.TabIndex = 8;
            this.stopPictureBox.TabStop = false;
            this.stopToolTip.SetToolTip(this.stopPictureBox, "stop capturing packets");
            this.stopPictureBox.Click += new System.EventHandler(this.stopPictureBox_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SnifferClient.Properties.Resources.CAPCKET_logo_red_cut_removebg_preview;
            this.pictureBox1.Location = new System.Drawing.Point(647, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // RawSockSnifferForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 553);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.startPictureBox);
            this.Controls.Add(this.requestButton);
            this.Controls.Add(this.chooseDateLabel);
            this.Controls.Add(this.previousSniffComboBox);
            this.Controls.Add(this.stopPictureBox);
            this.Controls.Add(this.listView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RawSockSnifferForm";
            this.Text = "CAPCKET";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RawSockSnifferForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.startPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stopPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader SerialNumber;
        private System.Windows.Forms.ColumnHeader ProtocolName;
        private System.Windows.Forms.ColumnHeader Length;
        private System.Windows.Forms.ColumnHeader SourceAdd;
        private System.Windows.Forms.ColumnHeader DestinationAdd;
        private System.Windows.Forms.ColumnHeader Checksum;
        private System.Windows.Forms.PictureBox stopPictureBox;
        private System.Windows.Forms.ComboBox previousSniffComboBox;
        private System.Windows.Forms.Label chooseDateLabel;
        private System.Windows.Forms.Button requestButton;
        private System.Windows.Forms.PictureBox startPictureBox;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip stopToolTip;
        private System.Windows.Forms.ToolTip startToolTip;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}