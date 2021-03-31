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
            System.Windows.Forms.ColumnHeader ArrivalTime;
            this.listView1 = new System.Windows.Forms.ListView();
            this.SerialNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ProtocolName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Length = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SourceAdd = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DestinationAdd = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Checksum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.previousSniffComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.requestButton = new System.Windows.Forms.Button();
            this.stopPictureBox = new System.Windows.Forms.PictureBox();
            this.startPictureBox = new System.Windows.Forms.PictureBox();
            ArrivalTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.stopPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startPictureBox)).BeginInit();
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
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 75);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(982, 463);
            this.listView1.TabIndex = 6;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ItemMouseHover += new System.Windows.Forms.ListViewItemMouseHoverEventHandler(this.listView1_ItemMouseHover);
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
            this.SourceAdd.Width = 120;
            // 
            // DestinationAdd
            // 
            this.DestinationAdd.Text = "Destination Address";
            this.DestinationAdd.Width = 120;
            // 
            // Checksum
            // 
            this.Checksum.Text = "Checksum";
            this.Checksum.Width = 74;
            // 
            // previousSniffComboBox
            // 
            this.previousSniffComboBox.FormattingEnabled = true;
            this.previousSniffComboBox.Location = new System.Drawing.Point(852, 14);
            this.previousSniffComboBox.Name = "previousSniffComboBox";
            this.previousSniffComboBox.Size = new System.Drawing.Size(121, 21);
            this.previousSniffComboBox.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(687, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Load previously sniffed packets:";
            // 
            // requestButton
            // 
            this.requestButton.Location = new System.Drawing.Point(852, 41);
            this.requestButton.Name = "requestButton";
            this.requestButton.Size = new System.Drawing.Size(121, 23);
            this.requestButton.TabIndex = 11;
            this.requestButton.Text = "Send Request";
            this.requestButton.UseVisualStyleBackColor = true;
            this.requestButton.Click += new System.EventHandler(this.requestButton_Click);
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
            this.stopPictureBox.Click += new System.EventHandler(this.stopPictureBox_Click);
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
            this.startPictureBox.Click += new System.EventHandler(this.startPictureBox_Click);
            // 
            // RawSockSnifferForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 553);
            this.Controls.Add(this.startPictureBox);
            this.Controls.Add(this.requestButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.previousSniffComboBox);
            this.Controls.Add(this.stopPictureBox);
            this.Controls.Add(this.listView1);
            this.Name = "RawSockSnifferForm";
            this.Text = "RawSockSnifferForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RawSockSnifferForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.stopPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader SerialNumber;
        private System.Windows.Forms.ColumnHeader ProtocolName;
        private System.Windows.Forms.ColumnHeader Length;
        private System.Windows.Forms.Label labelChosenPacketData;
        private System.Windows.Forms.ColumnHeader SourceAdd;
        private System.Windows.Forms.ColumnHeader DestinationAdd;
        private System.Windows.Forms.ColumnHeader Checksum;
        private System.Windows.Forms.PictureBox stopPictureBox;
        private System.Windows.Forms.ComboBox previousSniffComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button requestButton;
        private System.Windows.Forms.PictureBox startPictureBox;
    }
}