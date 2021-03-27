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

        // requests' kinds
        const int packetDetailsResponse = 1;

        /// <summary>
        /// constructor that initializes the sniffer form
        /// </summary>
        public RawSockSnifferForm(TcpClient client)
        {
            InitializeComponent();
            pA = new PacketAnalyzer();
            this.client = client;
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
                //Console.WriteLine("\nThe following devices are available on this machine:");
                //Console.WriteLine("----------------------------------------------------\n");

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
                if (dataList.Count > 4) //sniffer supports only TCP Ethernet messages for now
                {
                    counter++;
                    dataList[0] = counter.ToString();

                    ListViewItem item = new ListViewItem(dataList.ToArray());
                    item.Tag = dataToTag;
                    //item.Tag = dataList[dataList.Count - 1];

                    this.Invoke(new Action(() => listView1.Items.Add(item)));
                    SendPacketDataToServer(dataList, dataToTag);

                }

                //Thread.Sleep(1000);


            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            /*LinkLayers ethType = p.LinkLayerType;

                for (int i = 0; i < len; i++)
                {
                    int v = p.Data[i];
                    string hexValue = v.ToString("X2");
                    Console.Write(hexValue + " ");
                }
                var packet = PacketDotNet.Packet.ParsePacket(p.LinkLayerType, p.Data);
                if (protocol.Equals("TCP"))
                {

                    var tcpPacket = (TcpPacket)packet.Extract(typeof(TcpPacket));
                    //Console.WriteLine(tcpPacket.ToString());
                    newData += tcpPacket.ToString();

                    
                    string bytes = "";
                    for (int i = 0; i < len; i++)
                    {
                        bytes += p.Data[i].ToString("X2") + " ";
                    }
                    Console.WriteLine("data=" + bytes);
                    
                }
               
            }*/
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
        /// <param name="message">string to send to the server</param>
        private void SendMessage(string message)
        {
            try
            {
                // send message to the server
                NetworkStream ns = client.GetStream();
                byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                // MessageBox.Show("message to send to server: " + message);

                // send the text
                ns.Write(data, 0, data.Length);
                ns.Flush();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

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
            SendMessage(messageToSend);

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

        /// <summary>
        /// when the start button is clicked find the connected devices and starts listening to packets
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Startbutton_Click(object sender, EventArgs e)
        {
            findDevices();
            startPacketListener();
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            device.OnPacketArrival -= new PacketArrivalEventHandler(device_OnPacketArrival);
        }
    }
}
