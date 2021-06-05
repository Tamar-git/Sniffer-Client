using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace SnifferClient
{
    /// <summary>
    /// form class that let the user choose filters for the packets
    /// </summary>
    public partial class FilterForm : Form
    {
        /// <summary>
        /// list of strings that restores the user's chosen protocols
        /// </summary>
        public List<string> selectedProtocols { get; set; }

        /// <summary>
        /// list of strings that restores the user's chosen addresses
        /// </summary>
        public List<string> selectedAddresses { get; set; }
        
        /// <summary>
        /// construction that creates the filter settings form
        /// </summary>
        public FilterForm()
        {
            InitializeComponent();
            selectedAddresses = new List<string>();
            checkedListBoxProtocols.Validated += new EventHandler(checkedListBoxProtocols_Validated);
        }

        /// <summary>
        /// when the protocols are validated, inserts the chosen ones to a list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void checkedListBoxProtocols_Validated(object sender, EventArgs e)
        {
            selectedProtocols = new List<string>();
            for (int i = 0; i < checkedListBoxProtocols.Items.Count; i++)
            {
                if (checkedListBoxProtocols.GetItemChecked(i))// getting selected value from CheckBox List  
                {
                    selectedProtocols.Add(checkedListBoxProtocols.Items[i].ToString()); // adds selected item to the list
                }
            }
        }

        /// <summary>
        /// when the button is clicked, closes the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFilter_Click(object sender, EventArgs e)
        {
            if (checkBoxSrcIp.Checked)
                selectedAddresses.Add(textBoxSrcIp.Text);
            else
                selectedAddresses.Add("");
            if (checkBoxDstIp.Checked)
                selectedAddresses.Add(textBoxDstIp.Text);
            else
                selectedAddresses.Add("");
            if (checkBoxSrcPort.Checked) 
                selectedAddresses.Add(textBoxSrcPort.Text);
            else
                selectedAddresses.Add("");
            if (checkBoxDstPort.Checked) 
                selectedAddresses.Add(textBoxDstPort.Text);
            else
                selectedAddresses.Add("");

            Debug.WriteLine("list length" + selectedAddresses.Count);
            this.DialogResult = DialogResult.OK;
            this.Hide();
        }

        /// <summary>
        /// change the read only status of the source ip according to the check box checked status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxSrcIp_CheckedChanged(object sender, EventArgs e)
        {
            textBoxSrcIp.ReadOnly = !checkBoxSrcIp.Checked;
        }

        /// <summary>
        /// change the read only status of the destination ip according to the check box checked status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxDstIp_CheckedChanged(object sender, EventArgs e)
        {
            textBoxDstIp.ReadOnly = !checkBoxDstIp.Checked;
        }

        /// <summary>
        /// change the read only status of the source port according to the check box checked status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxSrcPort_CheckedChanged(object sender, EventArgs e)
        {
            textBoxSrcPort.ReadOnly = !checkBoxSrcPort.Checked;
        }

        /// <summary>
        /// change the read only status of the destination port according to the check box checked status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxDstPort_CheckedChanged(object sender, EventArgs e)
        {
            textBoxDstPort.ReadOnly = !checkBoxDstPort.Checked;
        }
    }
}
