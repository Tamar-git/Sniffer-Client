using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using PacketDotNet;
using PacketDotNet.Utils;
using SharpPcap;
using System.Drawing.Drawing2D;
using System.Xml;
using System.Threading;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using SnifferServer;

namespace SnifferClient
{
    public partial class RawSockSnifferForm : Form
    {

        Socket sock = null;
        IPEndPoint hostEndPoint;
        IPAddress hostAddress = null;
        int conPort = 80;
        ICaptureDevice device = null;
        PacketAnalyzer pA;
        int counter = 0;
        TcpClient client;

        AesCrypto aes; // AES object for encryption and decryption

        // requests' kinds
        const int packetDetailsResponse = 1;
        const int logRequest = 2;
        const int logResponse = 3;
        const int noLogResponse = 4;

        // used for sending and reciving data
        private byte[] data;

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
                //string messageReceived = System.Text.Encoding.ASCII.GetString(data, 0, bytesRead);
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
                    MessageBox.Show("There were no packets from the requested date, please try a different one");
                }
                lock (client.GetStream())
                {
                    // continue reading from the client
                    client.GetStream().BeginRead(data, 0, System.Convert.ToInt32(client.ReceiveBufferSize), ReceiveMessage, null);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("catch recieve");
            }
        }

        /// <summary>
        /// receives a log file
        /// </summary>
        /// <param name="details"></param>
        public void ReceivingLog(string details)
        {
            this.Invoke(new Action(() => statusLabel.Text = "loading captured packets from " + previousSniffComboBox.SelectedItem.ToString()));
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
            this.Invoke(new Action(() => statusLabel.Text = "presenting captured packets from " + previousSniffComboBox.SelectedItem.ToString()));
            Debug.WriteLine(messageReceived);
            // Save the file using the filename sent by the client    
            //using (FileStream fStream = new FileStream(Path.GetFileName(cmdFileName), FileMode.Create))
            //{
            //    fStream.Write(buffer, 0, buffer.Length);
            //    fStream.Flush();
            //    fStream.Close();
            //}
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
                //Console.WriteLine("SharpPcap {0}, Example1.IfList.cs", ver);

                // Retrieve the device list
                //CaptureDeviceList devices = CaptureDeviceList.Instance;
                SharpPcap.LibPcap.LibPcapLiveDeviceList devices = SharpPcap.LibPcap.LibPcapLiveDeviceList.Instance;

                // If no devices were found print an error
                if (devices.Count < 1)
                {
                    s += "No devices were found on this machine";
                    //Console.WriteLine("No devices were found on this machine");
                    return;
                }

                s += "\nThe following devices are available on this machine:\n----------------------------------------------------\n";

                // Print out the available network devices
                foreach (ICaptureDevice dev in devices)
                {
                    string descr = dev.ToString();
                    /* try
                     {
                         dev.Open(DeviceMode.Normal);
                         string addr = dev.MacAddress.ToString();
                         Console.WriteLine("Was able to open dev " + dev.Name + " - " + addr);
                         device = dev;
                     }
                     catch (Exception e2)
                     {
                         Console.WriteLine("Couldn't open device " + dev.Name );
                     }
                     * */
                    if (descr.Contains("192.168.0.186"))
                    //if (descr.Contains("10.20.11.11"))
                    {
                        s += "Will use this one for test: \n" + descr;
                        //Console.WriteLine("Will use this one for test: \n" + descr);
                        device = dev;
                        break;
                    }
                    s += dev.ToString() + "\n";
                    //Console.WriteLine("{0}\n", dev.ToString());
                }
                //Console.WriteLine("Choose device");
                //String s2 = Console.ReadLine();
                int index = 1;// Convert.ToInt16(s2);
                if (device == null)
                {
                    //    Console.Write("Test device (192.168...) not found. Hit 'Enter' to exit...");
                    //    Console.ReadLine();
                    //    int i = 1;
                    device = devices[index];
                }
                MessageBox.Show(s);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error creating raw socket: " + e.Message);
                //System.Console.WriteLine("Error creating raw socket: " + e.Message);
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
                //if (dataList[1].Equals("TCP")) //sniffer supports only TCP messages for now
                if (dataList.Count > 4) //sniffer supports only TCP and UDP Ethernet messages for now
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

            MessageBox.Show("Listening for packets....");
            //device.Capture();
            //            Console.ReadLine();
            // user says 'done'
            //            device.StopCapture();
            //            device.Close();

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
                //byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                // MessageBox.Show("message to send to server: " + message);

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
            //Debug.WriteLine("sending aes: " + message);
            byte[] toSend = aes.EncryptStringToBytes(message, aes.GetKey(), aes.GetIV());
            SendMessage(toSend);
        }

        /// <summary>
        /// sends the analyzed packet to the server
        /// </summary>
        /// <param name="dataList"></param>
        /// <param name="dataToTag"></param>
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
        /// checks the packet's protocol
        /// </summary>
        /// <param name="packet">raw packet</param>
        /// <returns>string that contains the packet's protocol</returns>
        public string GetPacketProtocol(RawCapture packet)
        {
            string source = "";
            string destination = "";
            string protocol = "";
            string info = "";
            var currentPacket = PacketDotNet.Packet.ParsePacket(packet.LinkLayerType, packet.Data);
            var tempPacket = currentPacket;

            while (tempPacket.PayloadPacket != null)
            {
                tempPacket = tempPacket.PayloadPacket;
            }

            if (tempPacket is PacketDotNet.ApplicationPacket)
            {
                var applicationPacket = tempPacket as PacketDotNet.ApplicationPacket;
                string headerString = "";
                for (int index = 12; index <= 15; ++index)
                {
                    headerString += applicationPacket.Header[index].ToString();
                }

                if (headerString.CompareTo("HTTP") == 0)
                {
                    protocol = "HTTP";
                }
                else if (applicationPacket.ParentPacket is PacketDotNet.UdpPacket)
                {
                    protocol = "UDP";
                }
                else
                {
                    protocol = "TCP";
                }

                var ipPacket = tempPacket.ParentPacket.ParentPacket as PacketDotNet.IpPacket;
                source = ipPacket.SourceAddress.ToString();
                destination = ipPacket.DestinationAddress.ToString();
            }
            else if (tempPacket is PacketDotNet.TcpPacket)
            {
                protocol = "TCP";
                var ipPacket = tempPacket.ParentPacket as PacketDotNet.IpPacket;
                source = ipPacket.SourceAddress.ToString();
                destination = ipPacket.DestinationAddress.ToString();
                var tcpPacket = tempPacket as PacketDotNet.TcpPacket;
                if ((tcpPacket.DestinationPort.ToString().CompareTo("80") == 0) || (tcpPacket.DestinationPort.ToString().CompareTo("8080") == 0))
                {
                    protocol = "HTTP";
                }
                else if (tcpPacket.DestinationPort.ToString().CompareTo("1900") == 0)
                {
                    protocol = "SSDP";
                }

            }
            else if (tempPacket is PacketDotNet.UdpPacket)
            {
                protocol = "UDP";
                var ipPacket = tempPacket.ParentPacket as PacketDotNet.IpPacket;
                source = ipPacket.SourceAddress.ToString();
                destination = ipPacket.DestinationAddress.ToString();
                var udpPacket = tempPacket as PacketDotNet.UdpPacket;
                if (udpPacket.DestinationPort.ToString().CompareTo("80") == 0 || udpPacket.DestinationPort.ToString().CompareTo("8080") == 0)
                {
                    protocol = "HTTP";
                }
                else if (udpPacket.DestinationPort.ToString().CompareTo("1900") == 0)
                {
                    protocol = "SSDP";
                }
            }
            else if (tempPacket is PacketDotNet.IpPacket)
            {
                if (tempPacket is PacketDotNet.IPv4Packet)
                {
                    protocol = "Ipv4";
                }
                else
                {
                    protocol = "Ipv6";
                }
                var ipPacket = tempPacket as PacketDotNet.IpPacket;
                source = ipPacket.SourceAddress.ToString();
                destination = ipPacket.DestinationAddress.ToString();
            }
            else if (tempPacket is PacketDotNet.ARPPacket)
            {
                var arpPacket = tempPacket as PacketDotNet.ARPPacket;
                source = arpPacket.SenderHardwareAddress.ToString();
                destination = arpPacket.TargetHardwareAddress.ToString();
                protocol = "ARP";
                //info = System.Text.Encoding.ASCII.GetString(arpPacket.Bytes);
            }
            else if (tempPacket is PacketDotNet.EthernetPacket)
            {
                var ethernetPacket = tempPacket as PacketDotNet.EthernetPacket;
                source = ethernetPacket.SourceHwAddress.ToString();
                destination = ethernetPacket.DestinationHwAddress.ToString();
                protocol = "Ethernet";
                //info = System.Text.Encoding.ASCII.GetString(arpPacket.Bytes);
            }

            else if (tempPacket.ParentPacket is PacketDotNet.IpPacket)
            {
                var ipPacket = tempPacket.ParentPacket as PacketDotNet.IpPacket;
                System.Net.IPAddress srcIp = ipPacket.SourceAddress;
                System.Net.IPAddress dstIp = ipPacket.DestinationAddress;
                source = srcIp.ToString();
                destination = dstIp.ToString();
                protocol = ipPacket.Protocol.ToString();
                //ipPacket.Bytes
            }
            else if (tempPacket.ParentPacket is PacketDotNet.TcpPacket)
            {
                var tcpPacket = tempPacket.ParentPacket as PacketDotNet.TcpPacket;
                source = ((PacketDotNet.IpPacket)tcpPacket.ParentPacket).SourceAddress.ToString();
                destination = ((PacketDotNet.IpPacket)tcpPacket.ParentPacket).DestinationAddress.ToString();
                protocol = ((PacketDotNet.IpPacket)tcpPacket.ParentPacket).Protocol.ToString();
            }
            else if (tempPacket.ParentPacket is PacketDotNet.UdpPacket)
            {
                var udpPacket = tempPacket.ParentPacket as PacketDotNet.UdpPacket;
                source = ((PacketDotNet.IpPacket)udpPacket.ParentPacket).SourceAddress.ToString();
                destination = ((PacketDotNet.IpPacket)udpPacket.ParentPacket).DestinationAddress.ToString();
                protocol = ((PacketDotNet.IpPacket)udpPacket.ParentPacket).Protocol.ToString();
            }
            else if (tempPacket.ParentPacket is PacketDotNet.EthernetPacket)
            {
                var ethernetPacket = tempPacket.ParentPacket as PacketDotNet.EthernetPacket;
                source = ethernetPacket.SourceHwAddress.ToString();
                destination = ethernetPacket.DestinationHwAddress.ToString();
                protocol = ethernetPacket.Type.ToString();
            }
            return protocol;
            // return new CustomerPacket((PacketCount++).ToString(), packet.Timeval.ToString(), source, destination, protocol, packet.Data.Length.ToString(), info);
        }

        public void shutdown()
        {
            if (device != null)
            {
                device.StopCapture();
                device.Close();
                device = null;
            }
        }

        private ushort csum(byte[] buf, int nwords)
        {
            ulong sum;
            int i;
            for (sum = 0, i = 0; nwords > 0; nwords--)
            {
                sum += buf[i++];
            }
            sum = (sum >> 16) + (sum & 0xffff);
            sum += (sum >> 16);
            return (ushort)~sum;
        }

        public int sendPacket()
        {
            byte[] bytes = new byte[4096];
            ByteArraySegment bas = new ByteArraySegment(bytes);
            ushort srcPort = 23444;
            ushort dstPort = 12345;

            PacketDotNet.UdpPacket udpPacket = new UdpPacket(srcPort, dstPort);
            string cmdString = "xxxxyyyyHello world!";
            byte[] sendBuffer = Encoding.ASCII.GetBytes(cmdString);
            udpPacket.PayloadData = sendBuffer;

            ICMPv4Packet icmpPacket = new ICMPv4Packet(new ByteArraySegment(sendBuffer));
            // sanity check:
            //bas.BytesLength = 10;
            //Console.WriteLine("bas - Offset = " + bas.Offset + " len= " + bas.Length);

            IPAddress ipSrcAddr = System.Net.IPAddress.Parse("192.168.0.186"); // laptop
            IPAddress ipDestAddr = System.Net.IPAddress.Parse("192.168.0.185"); // my linux box
            IpPacket ipPacket = new IPv4Packet(ipSrcAddr, ipDestAddr);
            ipPacket.PayloadPacket = udpPacket;

            icmpPacket.TypeCode = ICMPv4TypeCodes.Unassigned1;
            icmpPacket.Sequence = 1;

            Console.WriteLine("icmpPacket - TypeCode = " + icmpPacket.TypeCode + " Sequence= " + icmpPacket.Sequence);
            Console.WriteLine("icmpPacket : " + icmpPacket.PrintHex());
            icmpPacket.UpdateCalculatedValues();
            Console.WriteLine("icmpPacket : " + icmpPacket.PrintHex());

            //ushort etype = 0xFF00; //EthernetPacketType RAW ?
            System.Net.NetworkInformation.PhysicalAddress ethSrcAddr = System.Net.NetworkInformation.PhysicalAddress.Parse("02-1E-EC-8F-7F-E1");
            System.Net.NetworkInformation.PhysicalAddress ethDstAddr = System.Net.NetworkInformation.PhysicalAddress.Parse("48-5B-39-ED-96-36");
            EthernetPacket ethernetPacket = new EthernetPacket(ethSrcAddr, ethDstAddr, EthernetPacketType.IpV4);
            // I thought "None" for type would fill in type automatically; but it remained zero on the wire and was flagged "Malformed"
            ethernetPacket.PayloadPacket = icmpPacket;
            Console.WriteLine("ethernetPacket : " + ethernetPacket.PrintHex());
            ethernetPacket.Type = EthernetPacketType.IpV4;

            Console.WriteLine("ethernetPacket : " + ethernetPacket.PrintHex());

            //ipPacket.PayloadPacket = udpPacket;
            //ethernetPacket.PayloadPacket = gmpPacket; //ipPacket;
            ethernetPacket.UpdateCalculatedValues();
            Console.WriteLine(ethernetPacket.ToString());

            ipPacket.UpdateCalculatedValues();
            //udpPacket.UpdateUDPChecksum();
            // Experiment with raw ip packet?
            ipPacket.Protocol = IPProtocolType.RAW;

            // Why isn't ValidChecksum true?
            //Console.WriteLine("After updating calculated values, ValidUDPChecksum = " + udpPacket.ValidUDPChecksum + " ValidChecksum = " + udpPacket.ValidChecksum);
            //Console.WriteLine(ethernetPacket.ToString());

            device.Open(DeviceMode.Normal, 15000);  // 15 sec timeout

            //ushort checksum = csum(ipPacket.BytesHighPerformance, ipPacket.P);
            device.SendPacket(ethernetPacket);

            return 0;
        }

        public void sendUDP()
        {
            string cmdString = "Hello world!";
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse("192.168.0.185"), 12345);
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse("192.168.0.186"), 10000);
            //Byte[] data = new Byte[120];
            //ByteArraySegment bas = new ByteArraySegment(data);
            //UdpClient udpClient = new UdpClient("192.168.0.186", 12345);
            //UdpPacket udpPacket = new UdpPacket(
            //udpClient.Send(bas);
            //    Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            UdpClient udpClient = new UdpClient(11000);  // local port binding

            byte[] sendBuffer = Encoding.ASCII.GetBytes(cmdString);
            try
            {

                //        sock.SendTo(sendBuffer, ipEndPoint);
                udpClient.Connect(remoteEndPoint);
                udpClient.Send(sendBuffer, sendBuffer.Length);
                Console.WriteLine("Packet sent: " + cmdString);
                udpClient.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error sending packet: " + e.Message);
            }

            UdpClient udpClientB = new UdpClient();
            udpClientB.Send(sendBuffer, 12, "192.168.0.185", 12345);
            Console.WriteLine("Alt msg sent");
            udpClientB.Close();
        }

        private void listView1_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
        {
            //this.Invoke(new Action(() => labelChosenPacketData.Text = "data: " + e.Item.Tag));
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            //this.Invoke(new Action(() => labelChosenPacketData.Text = "data: " + listView1.SelectedItems[0].Tag));
            // Create a new instance of the DataViewForm class
            DataViewForm dataViewForm = new DataViewForm(listView1.SelectedItems[0]);

            // Show the form
            this.Invoke(new Action(() => dataViewForm.Show()));
        }

        private void RawSockSnifferForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            device.OnPacketArrival -= new PacketArrivalEventHandler(device_OnPacketArrival);
            Environment.Exit(1);
        }

        private void stopPictureBox_Click(object sender, EventArgs e)
        {
            device.OnPacketArrival -= new PacketArrivalEventHandler(device_OnPacketArrival);
            this.Invoke(new Action(() => requestButton.Enabled = true));
            this.Invoke(new Action(() => statusLabel.Text = "Capturing stopped\npresents currently captured packets"));
            counter = 0;
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
        /// when the the button is clicked sends a request of a log file to the server
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
            this.Invoke(new Action(() => statusLabel.Text = "capturing packets"));
        }
    }
}
