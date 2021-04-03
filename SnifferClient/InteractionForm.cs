using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnifferClient
{
    public partial class InteractionForm : Form
    {
        /// <summary>
        /// string that restores the user's answer
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// constructor that creates the new form
        /// </summary>
        /// <param name="command">orders for the user</param>
        /// <param name="title">form's requiered title</param>
        public InteractionForm(string command, string title)
        {
            InitializeComponent();
            commandLabel.Text = command;
            this.Text = title;
            commandTextBox.Validated += new EventHandler(commandTextBox_Validated);
        }

        /// <summary>
        /// when the answer is validated, inserts the answer to the text string
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void commandTextBox_Validated(object sender, EventArgs e)
        {
            text = commandTextBox.Text.Trim();
        }

        /// <summary>
        /// when the button is clicked, closes the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Hide();
        }
    }
}
