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
    public partial class DataViewForm : Form
    {
        private ListViewItem packetItem;

        /// <summary>
        /// constructor that initializes the form
        /// </summary>
        /// <param name="item">List View Item to present in the form</param>
        public DataViewForm(ListViewItem item)
        {
            InitializeComponent();
            packetItem = item;

        }

        /// <summary>
        /// actions that happens after the form's handle is created
        /// </summary>
        /// <param name="e"></param>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            string title = "Sniffer · packet " + packetItem.SubItems[0].Text + " · " + packetItem.SubItems[1].Text;
            this.Invoke(new Action(() => this.Text = title));
            byte[] data = (byte[])packetItem.Tag;
            if (data.Length > 0) // if the packet came with body
            {
                this.Invoke(new Action(() => introLabel.Text = "packet's data:"));
                this.Invoke(new Action(() => HexData()));
                this.Invoke(new Action(() => DecData()));
            }
            else //if the packet came with no body
            {
                this.Invoke(new Action(() => NoDataForm()));
            }
        }

        /// <summary>
        /// changes in the form when the selected packet has no body
        /// </summary>
        public void NoDataForm()
        {
            introLabel.Text = "the packet has no body";
            asciiIntroLabel.Visible = false;
            hexIntroLabel.Visible = false;
            asciiLabel.Visible = false;
            hexLabel.Visible = false;
        }

        /// <summary>
        /// writes the data of the packet in hexadecimal form
        /// </summary>
        public void HexData()
        {
            byte[] data = (byte[])packetItem.Tag;
            string s = "";

            foreach (byte b in data)
            {
                s += b.ToString("X") + " ";
            }
            hexLabel.Text = s;
        }

        /// <summary>
        /// writes the data of the packet in decimal form
        /// </summary>
        public void DecData()
        {
            byte[] data = (byte[])packetItem.Tag;
            string s = "";
            // checks for each byte if he can be represented in a readable char
            foreach (byte b in data)
            {
                if (IsReadeble(b))
                    s += (char)b + "  ";
                else
                    s += "·  ";
            }
            asciiLabel.Text = s;
        }

        /// <summary>
        /// returns wheather a byte is a readeble char
        /// </summary>
        /// <param name="b">byte</param>
        /// <returns>wheather readeble</returns>
        public static Boolean IsReadeble(byte b)
        {
            int dec = (int)b;
            return (dec > 32 && dec < 127);
        }

        /// <summary>
        /// when the close button is clicked, the form gets closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Invoke(new Action(() => this.Close()));
        }


    }
}
