using System;
using System.Windows.Forms;

namespace SnifferClient
{
    /// <summary>
    /// form class that displays a packet's body
    /// </summary>
    public partial class DataViewForm : Form
    {
        private ListViewItem packetItem; // stores the chosen packet data

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
            string title = "CAPCKET · packet " + packetItem.SubItems[0].Text + " · " + packetItem.SubItems[1].Text;
            this.Invoke(new Action(() => this.Text = title));
            byte[] data = (byte[])packetItem.Tag;
            if (data.Length > 0) // if the packet came with body
            {
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
            introLabel.Text = "The packet has an empty body.";
            this.introLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177))); asciiIntroLabel.Visible = false;
            hexIntroLabel.Visible = false;
            asciiTextBox.Visible = false;
            hexTextBox.Visible = false;
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
            hexTextBox.Text = s;
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
            asciiTextBox.Text = s;
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
