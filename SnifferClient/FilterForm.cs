using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnifferClient
{
    public partial class FilterForm : Form
    {
        /// <summary>
        /// string that restores the user's answer
        /// </summary>
        public string text { get; set; }
        public List<string> selectedProtocols { get; set; }
        public List<string> selectedAddresses { get; set; }
        public FilterForm()
        {
            InitializeComponent();
            selectedAddresses = new List<string>();
            checkedListBoxProtocols.Validated += new EventHandler(checkedListBoxProtocols_Validated);
            //textBoxSrcIp.Validated += new EventHandler(textBoxSrcIp_Validated);
            //textBoxDstIp.Validated += new EventHandler(textBoxDstIp_Validated);
            //textBoxSrcPort.Validated += new EventHandler(textBoxSrcPort_Validated);
            //textBoxDstPort.Validated += new EventHandler(textBoxDstPort_Validated);

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

        ///// <summary>
        ///// when the text box is validated, inserts the source ip to the list
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //void textBoxSrcIp_Validated(object sender, EventArgs e)
        //{
        //    if(checkBoxSrcIp.Checked)
        //        selectedAddresses[0] = textBoxSrcIp.Text;
        //}

        ///// <summary>
        ///// when the text box is validated, inserts the destination ip to the list
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //void textBoxDstIp_Validated(object sender, EventArgs e)
        //{
        //    if (checkBoxDstIp.Checked)
        //        selectedAddresses[1] = textBoxDstIp.Text;
        //}

        ///// <summary>
        ///// when the text box is validated, inserts the source port to the list
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //void textBoxSrcPort_Validated(object sender, EventArgs e)
        //{
        //    if (checkBoxSrcPort.Checked)
        //        selectedAddresses[0] = textBoxSrcPort.Text;
        //}

        ///// <summary>
        ///// when the text box is validated, inserts the destination port to the list
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //void textBoxDstPort_Validated(object sender, EventArgs e)
        //{
        //    if (checkBoxDstPort.Checked)
        //        selectedAddresses[1] = textBoxDstPort.Text;
        //}

        /// <summary>
        /// when the button is clicked, closes the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFilter_Click(object sender, EventArgs e)
        {
            selectedAddresses.Add(textBoxSrcIp.Text);
            selectedAddresses.Add(textBoxDstIp.Text);
            selectedAddresses.Add(textBoxSrcPort.Text);
            selectedAddresses.Add(textBoxDstPort.Text);
            //selectedAddresses[0] = textBoxSrcIp.Text;
            //selectedAddresses[1] = textBoxDstIp.Text;
            //selectedAddresses[2] = textBoxSrcPort.Text;
            //selectedAddresses[3] = textBoxDstPort.Text;
            Debug.WriteLine("list length" + selectedAddresses.Count);
            this.DialogResult = DialogResult.OK;
            this.Hide();
        }

        private void checkBoxSrcIp_CheckedChanged(object sender, EventArgs e)
        {
            textBoxSrcIp.ReadOnly = !checkBoxSrcIp.Checked;
        }

        private void checkBoxDstIp_CheckedChanged(object sender, EventArgs e)
        {
            textBoxDstIp.ReadOnly = !checkBoxDstIp.Checked;
        }

        private void checkBoxSrcPort_CheckedChanged(object sender, EventArgs e)
        {
            textBoxSrcPort.ReadOnly = !checkBoxSrcPort.Checked;
        }

        private void checkBoxDstPort_CheckedChanged(object sender, EventArgs e)
        {
            textBoxDstPort.ReadOnly = !checkBoxDstPort.Checked;
        }
    }
}
