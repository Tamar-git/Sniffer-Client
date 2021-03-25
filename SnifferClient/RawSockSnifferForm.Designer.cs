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
            this.StartButton = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.SerialNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ProtocolName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Length = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SourceAdd = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DestinationAdd = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Checksum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelChosenPacketData = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ArrivalTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ArrivalTime
            // 
            ArrivalTime.Text = "Arrival Time";
            ArrivalTime.Width = 94;
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(12, 12);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 3;
            this.StartButton.Text = "Start Sniffing";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.Startbutton_Click);
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
            this.listView1.Location = new System.Drawing.Point(12, 61);
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
            this.SourceAdd.Width = 100;
            // 
            // DestinationAdd
            // 
            this.DestinationAdd.Text = "Destination Address";
            this.DestinationAdd.Width = 117;
            // 
            // Checksum
            // 
            this.Checksum.Text = "Checksum";
            this.Checksum.Width = 74;
            // 
            // labelChosenPacketData
            // 
            this.labelChosenPacketData.AutoSize = true;
            this.labelChosenPacketData.Location = new System.Drawing.Point(288, 12);
            this.labelChosenPacketData.Name = "labelChosenPacketData";
            this.labelChosenPacketData.Size = new System.Drawing.Size(0, 13);
            this.labelChosenPacketData.TabIndex = 7;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SnifferClient.Properties.Resources.red_square;
            this.pictureBox1.Location = new System.Drawing.Point(93, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // RawSockSnifferForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 536);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelChosenPacketData);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.StartButton);
            this.Name = "RawSockSnifferForm";
            this.Text = "RawSockSnifferForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RawSockSnifferForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader SerialNumber;
        private System.Windows.Forms.ColumnHeader ProtocolName;
        private System.Windows.Forms.ColumnHeader Length;
        private System.Windows.Forms.Label labelChosenPacketData;
        private System.Windows.Forms.ColumnHeader SourceAdd;
        private System.Windows.Forms.ColumnHeader DestinationAdd;
        private System.Windows.Forms.ColumnHeader Checksum;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}