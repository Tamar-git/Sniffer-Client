namespace SnifferClient
{
    partial class FilterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilterForm));
            this.checkedListBoxProtocols = new System.Windows.Forms.CheckedListBox();
            this.labelProtocols = new System.Windows.Forms.Label();
            this.labelInstruction = new System.Windows.Forms.Label();
            this.checkBoxSrcIp = new System.Windows.Forms.CheckBox();
            this.textBoxSrcIp = new System.Windows.Forms.TextBox();
            this.textBoxDstIp = new System.Windows.Forms.TextBox();
            this.checkBoxDstIp = new System.Windows.Forms.CheckBox();
            this.textBoxDstPort = new System.Windows.Forms.TextBox();
            this.checkBoxDstPort = new System.Windows.Forms.CheckBox();
            this.textBoxSrcPort = new System.Windows.Forms.TextBox();
            this.checkBoxSrcPort = new System.Windows.Forms.CheckBox();
            this.buttonFilter = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkedListBoxProtocols
            // 
            this.checkedListBoxProtocols.BackColor = System.Drawing.SystemColors.Control;
            this.checkedListBoxProtocols.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBoxProtocols.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.checkedListBoxProtocols.FormattingEnabled = true;
            this.checkedListBoxProtocols.Items.AddRange(new object[] {
            "TCP",
            "UDP",
            "HTTP",
            "SSDP",
            "DNS"});
            this.checkedListBoxProtocols.Location = new System.Drawing.Point(17, 99);
            this.checkedListBoxProtocols.Name = "checkedListBoxProtocols";
            this.checkedListBoxProtocols.Size = new System.Drawing.Size(120, 108);
            this.checkedListBoxProtocols.TabIndex = 0;
            // 
            // labelProtocols
            // 
            this.labelProtocols.AutoSize = true;
            this.labelProtocols.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelProtocols.Location = new System.Drawing.Point(13, 76);
            this.labelProtocols.Name = "labelProtocols";
            this.labelProtocols.Size = new System.Drawing.Size(79, 20);
            this.labelProtocols.TabIndex = 1;
            this.labelProtocols.Text = "Protocols:";
            // 
            // labelInstruction
            // 
            this.labelInstruction.AllowDrop = true;
            this.labelInstruction.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelInstruction.Location = new System.Drawing.Point(12, 9);
            this.labelInstruction.Name = "labelInstruction";
            this.labelInstruction.Size = new System.Drawing.Size(451, 70);
            this.labelInstruction.TabIndex = 2;
            this.labelInstruction.Text = "Please choose the requiered filters for the captured packets, then click \"Filter\"" +
    ".";
            // 
            // checkBoxSrcIp
            // 
            this.checkBoxSrcIp.AutoSize = true;
            this.checkBoxSrcIp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.checkBoxSrcIp.Location = new System.Drawing.Point(17, 214);
            this.checkBoxSrcIp.Name = "checkBoxSrcIp";
            this.checkBoxSrcIp.Size = new System.Drawing.Size(88, 21);
            this.checkBoxSrcIp.TabIndex = 3;
            this.checkBoxSrcIp.Text = "Source IP";
            this.checkBoxSrcIp.UseVisualStyleBackColor = true;
            this.checkBoxSrcIp.CheckedChanged += new System.EventHandler(this.checkBoxSrcIp_CheckedChanged);
            // 
            // textBoxSrcIp
            // 
            this.textBoxSrcIp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.textBoxSrcIp.Location = new System.Drawing.Point(137, 215);
            this.textBoxSrcIp.Name = "textBoxSrcIp";
            this.textBoxSrcIp.ReadOnly = true;
            this.textBoxSrcIp.Size = new System.Drawing.Size(100, 23);
            this.textBoxSrcIp.TabIndex = 4;
            // 
            // textBoxDstIp
            // 
            this.textBoxDstIp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.textBoxDstIp.Location = new System.Drawing.Point(137, 261);
            this.textBoxDstIp.Name = "textBoxDstIp";
            this.textBoxDstIp.ReadOnly = true;
            this.textBoxDstIp.Size = new System.Drawing.Size(100, 23);
            this.textBoxDstIp.TabIndex = 6;
            // 
            // checkBoxDstIp
            // 
            this.checkBoxDstIp.AutoSize = true;
            this.checkBoxDstIp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.checkBoxDstIp.Location = new System.Drawing.Point(17, 260);
            this.checkBoxDstIp.Name = "checkBoxDstIp";
            this.checkBoxDstIp.Size = new System.Drawing.Size(114, 21);
            this.checkBoxDstIp.TabIndex = 5;
            this.checkBoxDstIp.Text = "Destination IP";
            this.checkBoxDstIp.UseVisualStyleBackColor = true;
            this.checkBoxDstIp.CheckedChanged += new System.EventHandler(this.checkBoxDstIp_CheckedChanged);
            // 
            // textBoxDstPort
            // 
            this.textBoxDstPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.textBoxDstPort.Location = new System.Drawing.Point(430, 260);
            this.textBoxDstPort.Name = "textBoxDstPort";
            this.textBoxDstPort.ReadOnly = true;
            this.textBoxDstPort.Size = new System.Drawing.Size(100, 23);
            this.textBoxDstPort.TabIndex = 10;
            // 
            // checkBoxDstPort
            // 
            this.checkBoxDstPort.AutoSize = true;
            this.checkBoxDstPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.checkBoxDstPort.Location = new System.Drawing.Point(293, 259);
            this.checkBoxDstPort.Name = "checkBoxDstPort";
            this.checkBoxDstPort.Size = new System.Drawing.Size(128, 21);
            this.checkBoxDstPort.TabIndex = 9;
            this.checkBoxDstPort.Text = "Destination Port";
            this.checkBoxDstPort.UseVisualStyleBackColor = true;
            this.checkBoxDstPort.CheckedChanged += new System.EventHandler(this.checkBoxDstPort_CheckedChanged);
            // 
            // textBoxSrcPort
            // 
            this.textBoxSrcPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.textBoxSrcPort.Location = new System.Drawing.Point(430, 215);
            this.textBoxSrcPort.Name = "textBoxSrcPort";
            this.textBoxSrcPort.ReadOnly = true;
            this.textBoxSrcPort.Size = new System.Drawing.Size(100, 23);
            this.textBoxSrcPort.TabIndex = 8;
            // 
            // checkBoxSrcPort
            // 
            this.checkBoxSrcPort.AutoSize = true;
            this.checkBoxSrcPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.checkBoxSrcPort.Location = new System.Drawing.Point(293, 213);
            this.checkBoxSrcPort.Name = "checkBoxSrcPort";
            this.checkBoxSrcPort.Size = new System.Drawing.Size(102, 21);
            this.checkBoxSrcPort.TabIndex = 7;
            this.checkBoxSrcPort.Text = "Source Port";
            this.checkBoxSrcPort.UseVisualStyleBackColor = true;
            this.checkBoxSrcPort.CheckedChanged += new System.EventHandler(this.checkBoxSrcPort_CheckedChanged);
            // 
            // buttonFilter
            // 
            this.buttonFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.buttonFilter.Location = new System.Drawing.Point(17, 293);
            this.buttonFilter.Name = "buttonFilter";
            this.buttonFilter.Size = new System.Drawing.Size(75, 32);
            this.buttonFilter.TabIndex = 11;
            this.buttonFilter.Text = "Filter";
            this.buttonFilter.UseVisualStyleBackColor = true;
            this.buttonFilter.Click += new System.EventHandler(this.buttonFilter_Click);
            // 
            // FilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 336);
            this.Controls.Add(this.buttonFilter);
            this.Controls.Add(this.textBoxDstPort);
            this.Controls.Add(this.checkBoxDstPort);
            this.Controls.Add(this.textBoxSrcPort);
            this.Controls.Add(this.checkBoxSrcPort);
            this.Controls.Add(this.textBoxDstIp);
            this.Controls.Add(this.checkBoxDstIp);
            this.Controls.Add(this.textBoxSrcIp);
            this.Controls.Add(this.checkBoxSrcIp);
            this.Controls.Add(this.labelInstruction);
            this.Controls.Add(this.labelProtocols);
            this.Controls.Add(this.checkedListBoxProtocols);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FilterForm";
            this.Text = "CAPCKET Filter Captured Packets";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBoxProtocols;
        private System.Windows.Forms.Label labelProtocols;
        private System.Windows.Forms.Label labelInstruction;
        private System.Windows.Forms.CheckBox checkBoxSrcIp;
        private System.Windows.Forms.TextBox textBoxSrcIp;
        private System.Windows.Forms.TextBox textBoxDstIp;
        private System.Windows.Forms.CheckBox checkBoxDstIp;
        private System.Windows.Forms.TextBox textBoxDstPort;
        private System.Windows.Forms.CheckBox checkBoxDstPort;
        private System.Windows.Forms.TextBox textBoxSrcPort;
        private System.Windows.Forms.CheckBox checkBoxSrcPort;
        private System.Windows.Forms.Button buttonFilter;
    }
}