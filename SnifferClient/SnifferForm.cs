using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnifferClient
{
    public partial class SnifferForm : Form
    {
        private TcpClient client; // client Socket
        private byte[] data; //stores the data that sends to & from the server

        public SnifferForm(TcpClient client)
        {
            this.client = client;

            // Read data from the client async
            data = new byte[client.ReceiveBufferSize];

            InitializeComponent();

            client.GetStream().BeginRead(data,
                                        0,
                                        System.Convert.ToInt32(client.ReceiveBufferSize),
                                        ReceiveMessage,
                                        null);

            // For sniffing the socket to capture the packets 
            // has to be a raw socket, with the address family
            // being of type internetwork, and protocol being IP
            Socket mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Raw,
                                   ProtocolType.IP);
            /*
            // Bind the socket to the selected IP address
            mainSocket.Bind(newIPEndPoint(IPAddress.Parse(cmbInterfaces.Text), 0));

            // Set the socket options
            mainSocket.SetSocketOption(SocketOptionLevel.IP,  //Applies only to IP packets
                                       SocketOptionName.HeaderIncluded, //Set the include header
                                       true);                           //option to true

            byte[] byTrue = newbyte[4]{ 1, 0, 0, 0};
            byte[] byOut = newbyte[4];

            //Socket.IOControl is analogous to the WSAIoctl method of Winsock 2
            mainSocket.IOControl(IOControlCode.ReceiveAll,  //SIO_RCVALL of Winsock
                                 byTrue, byOut);

            //Start receiving the packets asynchronously
            mainSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None,
                                    newAsyncCallback(OnReceive), null);
            */
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

        /// <summary>
        /// recursive method that recieves a message from the server and handles it according to the request or response number
        /// </summary>
        /// <param name="ar"></param>
        private void ReceiveMessage(IAsyncResult ar)
        {
            try
            {

                int bytesRead;
                lock (client.GetStream())
                {
                    // call EndRead to handle the end of an async read and read the data from the server
                    bytesRead = client.GetStream().EndRead(ar);
                }
                string messageReceived = System.Text.Encoding.ASCII.GetString(data, 0, bytesRead);
                MessageBox.Show(messageReceived);
                string[] arrayReceived = messageReceived.Split('#');
                int requestNumber = Convert.ToInt32(arrayReceived[0]);
                string text = arrayReceived[1];


                // continue reading
                client.GetStream().BeginRead(data,
                                         0,
                                         System.Convert.ToInt32(client.ReceiveBufferSize),
                                         ReceiveMessage,
                                         null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
