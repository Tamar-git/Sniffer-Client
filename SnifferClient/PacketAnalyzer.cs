using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using PacketDotNet;
using PacketDotNet.Utils;
using SharpPcap;
using System.IO;
using System.Diagnostics;
using System.Net.Http;
using static System.Net.WebRequestMethods;

namespace SnifferClient
{
    class PacketAnalyzer
    {
        /// <summary>
        /// analyzing the packet
        /// </summary>
        /// <param name="raw">packet</param>
        /// <returns>list of strings with the details of the packet</returns>
        public List<String> GetInfoList(RawCapture raw, out byte[] dataToTag)
        {
            List<String> info = new List<string>();
            dataToTag = null;
            try
            {
                LinkLayers ethType = raw.LinkLayerType;
                string protocol = GetPacketProtocol(raw);
                Debug.WriteLine("protocol: " + protocol);
                if (ethType is LinkLayers.Ethernet)
                {

                    info.Add("0");
                    info.Add(protocol);
                    DateTime time = raw.Timeval.Date.ToLocalTime();
                    info.Add(String.Format("{0}:{1}:{2}:{3}", time.Hour, time.Minute, time.Second, time.Millisecond));
                    info.Add(raw.Data.Length.ToString());

                    Packet packet = PacketDotNet.Packet.ParsePacket(ethType, raw.Data);

                    if (protocol.Equals("TCP") || protocol.Equals("HTTP"))
                    {
                        var tcpPacket = (TcpPacket)packet.Extract(typeof(TcpPacket));
                        var ipPacket = (IpPacket)packet.Extract(typeof(IpPacket));

                        //var tcp = (TcpPacket)packet.Extract(typeof(TcpPacket));
                        string srcIp = ipPacket.SourceAddress.ToString();
                        string dstIp = ipPacket.DestinationAddress.ToString();
                        string srcPort = tcpPacket.SourcePort.ToString();
                        string dstPort = tcpPacket.DestinationPort.ToString();
                        info.Add(srcIp + " " + srcPort);
                        info.Add(dstIp + " " + dstPort);
                        info.Add(tcpPacket.Checksum.ToString());

                        dataToTag = tcpPacket.PayloadData;

                        UnicodeEncoding _encoder = new UnicodeEncoding();
                        string data = System.Text.Encoding.ASCII.GetString(dataToTag, 0, dataToTag.Length);
                        info.Add(data);


                    }
                    else if (protocol.Equals("UDP") || protocol.Equals("SSDP") || protocol.Equals("DNS"))
                    {
                        var udpPacket = (UdpPacket)packet.Extract(typeof(UdpPacket));
                        var ipPacket = (IpPacket)packet.Extract(typeof(IpPacket));

                        string srcIp = ipPacket.SourceAddress.MapToIPv4().ToString();
                        string dstIp = ipPacket.DestinationAddress.MapToIPv4().ToString();
                        string srcPort = udpPacket.SourcePort.ToString();
                        string dstPort = udpPacket.DestinationPort.ToString();
                        info.Add(srcIp + " " + srcPort);
                        info.Add(dstIp + " " + dstPort);
                        info.Add(udpPacket.Checksum.ToString());

                        dataToTag = udpPacket.PayloadData;

                        UnicodeEncoding _encoder = new UnicodeEncoding();
                        string data = System.Text.Encoding.ASCII.GetString(dataToTag, 0, dataToTag.Length);
                        info.Add(data);

                    }
                    //else if (protocol.Equals("ARP"))
                    //{
                    //    var arpPacket = (ARPPacket)packet.Extract(typeof(ARPPacket));
                    //    var ipPacket = (IpPacket)packet.Extract(typeof(IpPacket));

                    //    string srcIp = ipPacket.SourceAddress.MapToIPv4().ToString();
                    //    string dstIp = ipPacket.DestinationAddress.MapToIPv4().ToString();
                    //    string srcPort = arpPacket.SourcePort.ToString();
                    //    string dstPort = udpPacket.DestinationPort.ToString();
                    //    info.Add(srcIp + " " + srcPort);
                    //    info.Add(dstIp + " " + dstPort);
                    //    info.Add(udpPacket.Checksum.ToString());

                    //    dataToTag = udpPacket.PayloadData;

                    //    UnicodeEncoding _encoder = new UnicodeEncoding();
                    //    string data = System.Text.Encoding.ASCII.GetString(dataToTag, 0, dataToTag.Length);
                    //    info.Add(data);
                    //}
                    /*else if (protocol.Equals("HTTP"))
                    {
                        var httpPacket = (ApplicationPacket)packet.Extract(typeof(ApplicationPacket));
                        var ipPacket = (IpPacket)packet.Extract(typeof(IpPacket));

                        string srcIp = ipPacket.SourceAddress.MapToIPv4().ToString();
                        string dstIp = ipPacket.DestinationAddress.MapToIPv4().ToString();
                        var http = new System.Net.Http();
                        http.Packet = packet;
                        
                        System.Net.Http.()
                        info.Add(srcIp + " " + httpPacket..SourcePort.ToString());
                        info.Add(dstIp + " " + udpPacket.DestinationPort.ToString());
                        info.Add(udpPacket.Checksum.ToString());

                        dataToTag = udpPacket.PayloadData;

                        UnicodeEncoding _encoder = new UnicodeEncoding();
                        string data = System.Text.Encoding.ASCII.GetString(dataToTag, 0, dataToTag.Length);
                        info.Add(data);
                    }*/
                }
                return info;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return info;
            }
        }


        /// <summary>
        /// checks the packet's protocol
        /// </summary>
        /// <param name="packet">raw packet</param>
        /// <returns>string that contains the packet's protocol</returns>
        public string GetPacketProtocol(RawCapture raw)
        {
            string source = "";
            string destination = "";
            string protocol = "";
            string info = "";
            try
            {
                var currentPacket = PacketDotNet.Packet.ParsePacket(raw.LinkLayerType, raw.Data);
                var tempPacket = currentPacket;

                while (tempPacket.PayloadPacket != null)
                {
                    tempPacket = tempPacket.PayloadPacket;
                }

                if (tempPacket is PacketDotNet.ApplicationPacket)
                {
                    var applicationPacket = tempPacket as PacketDotNet.ApplicationPacket;
                    string headerString1 = "", headerString2 = "";
                    for (int index = 0; index < 4; ++index)
                    {
                        headerString1 += applicationPacket.Header[index].ToString();
                    }
                    for (int index = 12; index <= 15; ++index)
                    {
                        headerString2 += applicationPacket.Header[index].ToString();
                    }

                    if (headerString1.Equals("HTTP") || headerString2.Equals("HTTP"))
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
                    if ((tcpPacket.DestinationPort.ToString().Equals("80")) || (tcpPacket.DestinationPort.ToString().Equals("8080")))
                    {
                        protocol = "HTTP";
                    }
                    else if (tcpPacket.DestinationPort.ToString().Equals("1900"))
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
                    if (udpPacket.DestinationPort.ToString().Equals("80") || udpPacket.DestinationPort.ToString().Equals("8080"))
                    {
                        protocol = "HTTP";
                    }
                    else if (udpPacket.DestinationPort.ToString().Equals("1900"))
                    {
                        protocol = "SSDP";
                    }
                    else if (udpPacket.DestinationPort.ToString().Equals("53") || udpPacket.SourcePort.ToString().Equals("53"))
                    {
                        protocol = "DNS";
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
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return e.ToString();
            }
        }

        static string BytesToStringConverted(byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
    }
}
