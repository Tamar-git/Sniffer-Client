using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Net.Sockets;
using SharpPcap;
using System.Diagnostics;

namespace SnifferClient
{
    /// <summary>
    /// form class that captures packets and displays them
    /// responsible for the connection with the server
    /// </summary>
    public partial class RawSockSnifferForm : Form
    {

        ICaptureDevice device = null; // stores the capture device
        PacketAnalyzer pA; // object that analyzes a packet
        int counter = 0; // stores the packet's serial number
        List<List<string>> currentLocalPackets; // stores list view items
        TcpClient client; // client Socket

        AesCrypto aes; // AES object for encryption and decryption

        // used for sending and reciving data
        private byte[] data;

        // requests' kinds
        const int packetDetailsResponse = 1;
        const int logRequest = 2;
        const int logResponse = 3;
        const int noLogResponse = 4;

        /// <summary>
        /// constructor that initializes the sniffer form
        /// </summary>
        public RawSockSnifferForm(TcpClient client, AesCrypto aes)
        {
            InitializeComponent();
            pA = new PacketAnalyzer();
            this.client = client;
            this.aes = aes;
            // Read data from the client async
            data = new byte[client.ReceiveBufferSize];

            // BeginRead will begin async read from the NetworkStream
            // This allows the server to remain responsive and continue accepting new connections from other clients
            // When reading complete control will be transfered to the ReviveMessage() function.
            client.GetStream().BeginRead(data,
                                          0,
                                          System.Convert.ToInt32(client.ReceiveBufferSize),
                                          ReceiveMessage,
                                          null);
        }

        /// <summary>
        /// actions that happens after the form's handle is created
        /// </summary>
        /// <param name="e"></param>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            this.Invoke(new Action(() => previousSniffComboBox.Items.AddRange(GetLastWeekDates())));
        }

        /// <summary>
        /// recursive method that recieves a message from the server and handles it according to the request or response number
        /// </summary>
        /// <param name="ar"></param>
        public void ReceiveMessage(IAsyncResult ar)
        {
            int bytesRead;

            try
            {
                lock (client.GetStream())
                {
                    // call EndRead to handle the end of an async read.
                    bytesRead = client.GetStream().EndRead(ar);
                }
                byte[] arrived = new byte[bytesRead];
                Array.Copy(data, arrived, bytesRead);
                string messageReceived = aes.DecryptStringFromBytes(arrived, aes.GetKey(), aes.GetIV());
                string[] arrayReceived = messageReceived.Split('#');
                int requestNumber = Convert.ToInt32(arrayReceived[0]);
                string details = arrayReceived[1];
                if (requestNumber == logResponse)
                {
                    ReceivingLog(details);
                }
                else if (requestNumber == noLogResponse)
                {
                    // shows message that indicates the absence of a file from the requested date
                    MessageBox.Show("There were no packets from the selected date\nPlease select a different date", "CAPCKET");
                }
                lock (client.GetStream())
                {
                    // continue reading from the client
                    client.GetStream().BeginRead(data, 0, System.Convert.ToInt32(client.ReceiveBufferSize), ReceiveMessage, null);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("catch recieve\n" + ex.ToString());
            }
        }

        /// <summary>
        /// handles the receive of a log file and presents it
        /// </summary>
        /// <param name="details">info about the log that will arrive</param>
        public void ReceivingLog(string details)
        {
            //changes in the form
            this.Invoke(new Action(() => statusLabel.Text = "Loading captured packets from " + previousSniffComboBox.SelectedItem.ToString()));
            this.Invoke(new Action(() => startPictureBox.Enabled = false));
            this.Invoke(new Action(() => stopPictureBox.Enabled = false));
            this.Invoke(new Action(() => startPictureBox.Image =Properties.Resources.play_arrow_button_circle_86280_gray));
            this.Invoke(new Action(() => stopPictureBox.Image = Properties.Resources.red_square_gray));

            string fileSize = details.Split('/')[0];
            string fileName = details.Split('/')[1];
            int length = Convert.ToInt32(fileSize);
            byte[] buffer = new byte[length];
            int received = 0;
            int read = 0;
            int size = 1024;
            int remaining = 0;

            // Read bytes from the server using the length sent from the server
            while (received < length)
            {
                remaining = length - received;
                if (remaining < size)
                {
                    size = remaining;
                }

                read = client.GetStream().Read(buffer, received, size);
                received += read;
            }
            // clears the existing items in the list view
            this.Invoke(new Action(() => listView1.Items.Clear()));
            string messageReceived = System.Text.Encoding.ASCII.GetString(buffer, 0, received);
            List<string> listOfPacketsData = messageReceived.Split('\n').ToList();
            for (int i = 0; i < listOfPacketsData.Count; i++) // presents every packet as a line in the form's list view
            {
                if (!listOfPacketsData[i].Equals(string.Empty) && !listOfPacketsData[i].Equals("\r"))
                {
                    // adds the counter field to the string
                    listOfPacketsData[i] = (i + 1) + "," + listOfPacketsData[i];
                    string[] line = listOfPacketsData[i].Split(',');
                    AddLineToListView(line);
                }
            }
            // changes in the form when all the packets were loaded
            this.Invoke(new Action(() => statusLabel.Text = "Displaying captured packets from " + previousSniffComboBox.SelectedItem.ToString()));
            this.Invoke(new Action(() => startPictureBox.Enabled = true));
            this.Invoke(new Action(() => stopPictureBox.Enabled = true));
            this.Invoke(new Action(() => startPictureBox.Image = Properties.Resources.play_arrow_button_circle_86280));
            this.Invoke(new Action(() => stopPictureBox.Image = Properties.Resources.red_square));
            Debug.WriteLine(messageReceived);

        }

        /// <summary>
        /// adds an array to the list view in the form
        /// </summary>
        /// <param name="detailsToAdd">array of strings with the packet's data</param>
        public void AddLineToListView(string[] detailsToAdd)
        {
            ListViewItem item = new ListViewItem(detailsToAdd);
            byte[] dataToTag = HexToBytes(detailsToAdd[detailsToAdd.Length - 1]);
            item.Tag = dataToTag;

            this.Invoke(new Action(() => listView1.Items.Add(item)));
        }

        /// <summary>
        /// convert string that represnts hex to bytes
        /// </summary>
        /// <param name="original">string with the original hex</param>
        /// <returns>converted byte array</returns>
        public static byte[] HexToBytes(string original)
        {
            original = original.Replace("\r", string.Empty);
            List<string> originalList = original.Split(' ').ToList();
            Debug.WriteLine("original: {0}, length: {1}", original, originalList.Count);
            List<byte> bytes = new List<byte>();
            foreach (string hex in originalList)
            {
                if (!hex.Equals(string.Empty) && !hex.Equals("\r")) // makes sure there is a hex value
                {
                    // Convert the number expressed in base-16 to an integer.
                    int value = Convert.ToInt32(hex, 16);
                    Debug.WriteLine("hex is {0} intValue is {1}", hex, value);
                    byte b = (byte)value;
                    bytes.Add(b);
                }
            }
            return bytes.ToArray();
        }

        /// <summary>
        /// finds the devices that are available in the network
        /// </summary>
        private void findDevices()
        {
            try
            {
                // Print SharpPcap version
                string ver = SharpPcap.Version.VersionString;
                string s = "SharpPcap" + ver + "Example1.IfList.cs";

                // Retrieve the device list
                SharpPcap.LibPcap.LibPcapLiveDeviceList devices = SharpPcap.LibPcap.LibPcapLiveDeviceList.Instance;

                // If no devices were found print an error
                if (devices.Count < 1)
                {
                    s += "No devices were found on this machine";
                    return;
                }

                s += "\nThe following devices are available on this machine:\n----------------------------------------------------\n";

                // Print out the available network devices
                foreach (ICaptureDevice dev in devices)
                {
                    string descr = dev.ToString();

                    if (descr.Contains("192.168.0.186"))
                    {
                        s += "Will use this one for test: \n" + descr;
                        device = dev;
                        break;
                    }
                    s += dev.ToString() + "\n";
                }
                int index = 1;
                if (device == null)
                {
                    device = devices[index];
                }
                //MessageBox.Show(s);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error creating raw socket: " + e.Message);
            }
        }

        /// <summary>
        /// handles every new packet that arrives to the chosen device
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="captureEventArgs"></param>
        private void device_OnPacketArrival(object sender, CaptureEventArgs captureEventArgs)
        {
            try
            {
                RawCapture p = captureEventArgs.Packet;
                byte[] dataToTag = null;
                List<String> dataList = pA.GetInfoList(p, out dataToTag);
                if (dataList.Count > 4) //sniffer supports TCP, UDP, SSDP, DNS and HTTP Ethernet messages for now
                {
                    counter++;
                    dataList[0] = counter.ToString();

                    ListViewItem item = new ListViewItem(dataList.ToArray());
                    item.Tag = dataToTag;

                    this.Invoke(new Action(() => listView1.Items.Add(item)));
                    SendPacketDataToServer(dataList, dataToTag);

                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

        }

        /// <summary>
        /// start listening for arriving packets 
        /// </summary>
        public void startPacketListener()
        {
            if (device == null)
            {
                MessageBox.Show("no adapter selected");
                return;
                // will throw null pointer exception
            }

            // Create listener
            device.OnPacketArrival += new PacketArrivalEventHandler(device_OnPacketArrival);

            device.Open(DeviceMode.Promiscuous, 1500);  // 1.5 sec timeout
            device.StartCapture();

        }

        /// <summary>
        /// gets a message and sends it to the server
        /// </summary>
        /// <param name="message">bytes to send to the server</param>
        private void SendMessage(byte[] message)
        {
            try
            {
                // send message to the server
                NetworkStream ns = client.GetStream();

                // send the text
                ns.Write(message, 0, message.Length);
                ns.Flush();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// gets a string message, encrypts it using Aes protocol and sends it to the server
        /// </summary>
        /// <param name="message">string to encrypt and send to the server</param>
        private void SendAesEncryptedMessage(string message)
        {
            Debug.WriteLine("sending aes: " + message);
            byte[] toSend = aes.EncryptStringToBytes(message, aes.GetKey(), aes.GetIV());
            SendMessage(toSend);
        }

        /// <summary>
        /// sends the analyzed packet to the server
        /// </summary>
        /// <param name="dataList">list that stores tha packet's data</param>
        /// <param name="dataToTag">bytes that stores the packet's body</param>
        public void SendPacketDataToServer(List<string> dataList, byte[] dataToTag)
        {
            //string allData = String.Join(",", dataList.ToArray(), 0, dataList.Count - 1);
            string allData = "";
            for (int i = 1; i < dataList.Count - 1; i++)
            {
                allData += dataList[i].ToString() + ",";
            }

            if (dataToTag.Length > 0)
            {
                string s = "";

                foreach (byte b in dataToTag)
                {
                    s += b.ToString("X") + " ";
                }
                allData += s;
            }

            string messageToSend = packetDetailsResponse + "#" + allData + "#" + allData.Length;
            SendAesEncryptedMessage(messageToSend);
        }

        /// <summary>
        /// when a line in the list view is clicked, opens a data view form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_Click(object sender, EventArgs e)
        {
            // Create a new instance of the DataViewForm class
            DataViewForm dataViewForm = new DataViewForm(listView1.SelectedItems[0]);

            // Show the form
            this.Invoke(new Action(() => dataViewForm.Show()));
        }

        /// <summary>
        /// handles the close of the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RawSockSnifferForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (device != null)
            {
                device.OnPacketArrival -= new PacketArrivalEventHandler(device_OnPacketArrival);
            }
            Environment.Exit(1);
        }

        /// <summary>
        /// when the stop pictureBox is clicked, stops capturing packets
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stopPictureBox_Click(object sender, EventArgs e)
        {
            if (device != null)
            {
                device.OnPacketArrival -= new PacketArrivalEventHandler(device_OnPacketArrival);
                this.Invoke(new Action(() => requestButton.Enabled = true));
                this.Invoke(new Action(() => statusLabel.Text = "Capturing stopped\nDisplaying recently captured packets"));
                counter = 0;
            }
        }

        /// <summary>
        /// gets the dates of the last week in the form of dd/MM/yyyy
        /// </summary>
        /// <returns>string array that contains the dates</returns>
        public static string[] GetLastWeekDates()
        {
            List<string> days = new List<string>();
            DateTime currentDate = DateTime.Now.ToLocalTime().Date;
            for (int i = 0; i < 7; i++)
            {
                days.Add(currentDate.ToString("dd/MM/yyyy"));
                currentDate = currentDate.Subtract(new TimeSpan(1, 0, 0, 0));
            }
            return days.ToArray();
        }

        /// <summary>
        /// when the the button is clicked, sends a request of a log file to the server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void requestButton_Click(object sender, EventArgs e)
        {
            string selectedDate = previousSniffComboBox.SelectedItem.ToString();
            //dd/MM/yyyy --> yyyyMMdd
            string wantedDate = selectedDate.Substring(6) + selectedDate.Substring(3, 2) + selectedDate.Substring(0, 2);
            string messageToSend = logRequest + "#" + wantedDate + "#" + wantedDate.Length;
            SendAesEncryptedMessage(messageToSend);
        }

        /// <summary>
        /// when the start button is clicked find the connected devices and starts listening to packets
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startPictureBox_Click(object sender, EventArgs e)
        {
            // clears the existing items in the list view
            this.Invoke(new Action(() => listView1.Items.Clear()));
            findDevices();
            startPacketListener();
            // while sniffing requesting logs isnt optional
            this.Invoke(new Action(() => requestButton.Enabled = false));
            this.Invoke(new Action(() => statusLabel.Text = "Capturing packets"));
        }

    }
}
