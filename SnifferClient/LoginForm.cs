using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using SnifferServer;

namespace SnifferClient
{
    public partial class LoginForm : Form
    {
        private int portNo = 500; // stores the port number of the server
        private string ipAddress = "127.0.0.1"; // stores the ip of the server
        private string name; // stores the player's name
        private TcpClient client; // client Socket
        private byte[] data; //stores the data that sends to & from the server

        Captcha captcha = new Captcha();
        string captchaCode;
        
        // Create a UnicodeEncoder to convert between byte array and string.
        UnicodeEncoding ByteConverter = new UnicodeEncoding();

        RsaCrypto rsa;
        AesCrypto aes;
        // requests' kinds
        const int signUpRequest = 1;
        const int signInRequest = 2;
        const int registerStatusResponse = 3;
        const int QuestionRequest = 4;
        const int EmailResponse = 5;
        const int CodeRequest = 6;
        const int QuestionResponse = 7;
        const int AnswerRequest = 8;
        const int PasswordRequest = 9;
        const int PasswordResponse = 10;
        const int PasswordChangeStatusResponse = 11;
        const int PublicKeyTransfer = 12;
        const int AesKeyTransfer = 13;

        /// <summary>
        /// constructor that initializes the form's components, creates a new TCP client and connects it to the server 
        /// and starts reading the data stream
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();
            client = new TcpClient();
            client.Connect(ipAddress, portNo);

            // Read data from the client async
            data = new byte[client.ReceiveBufferSize];

            //SendMessage("AAAAAA");
            /// test to the sniffer part
            NetworkStream ns;

            // we use lock to present multiple threads from using the networkstream object
            // this is likely to occur when the server is connected to multiple clients all of 
            // them trying to access to the networkstram at the same time.
            lock (client.GetStream())
            {
                ns = client.GetStream();
            }

            // Send data to the client
            //byte[] bytesToSend = { 0, 1, 0, 1, 0, 1, 0, 1 };
            //ns.Write(bytesToSend, 0, bytesToSend.Length);
            //ns.Flush();

            captchaCode = RandomString(6);
            pictureBoxCaptcha.Image = captcha.ProcessRequest(captchaCode);

            rsa = new RsaCrypto();
            /*string s = "hello";
            string s2 = rsa.Encrypt(s);
            MessageBox.Show(s2);
            s2 = rsa.Decrypt(s2);
            MessageBox.Show(s2);*/
            string messageToSend = PublicKeyTransfer + "#" + rsa.GetClientPublicKey() + "#" + rsa.GetClientPublicKey().Length;
            SendMessage(ByteConverter.GetBytes(messageToSend));
            
            client.GetStream().BeginRead(data,
                                        0,
                                        System.Convert.ToInt32(client.ReceiveBufferSize),
                                        ReceiveMessage,
                                        null);
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
        /// gets a string message, encrypts it and sends it to the server
        /// </summary>
        /// <param name="message">string to encrypt and send to the server</param>
        private void SendEncryptedMessage(string message)
        {
            SendMessage(rsa.RSAEncrypt(ByteConverter.GetBytes(message)));
        }

        /// <summary>
        /// gets bytes, encrypts it and sends it to the server
        /// </summary>
        /// <param name="message">bytes to encrypt and send to the server</param>
        private void SendEncryptedMessage(byte[] message)
        {
            SendMessage(rsa.RSAEncrypt(message));
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


                if (rsa.ServerPublicKey == null)
                {
                    rsa.SetServerPublicKey(ByteConverter.GetString(data).Split('#')[1]);
                    // creates an Aes instance for symmetric encryption and sends the key to the server
                    aes = new AesCrypto();
                    string key = ByteConverter.GetString(aes.GetKey());
                    //MessageBox.Show("AES key: " + key);
                    string toSend = AesKeyTransfer + "#" + key + "#" + key.Length;
                    SendEncryptedMessage(toSend);
                }
                else
                {
                    byte[] arrived = new byte[bytesRead];
                    Array.Copy(data, arrived, bytesRead);
                    byte[] bytesDecrypted = rsa.RSADecrypt(arrived);
                    string messageReceived = ByteConverter.GetString(bytesDecrypted);
                    //string messageReceived = System.Text.Encoding.ASCII.GetString(bytesDecrypted, 0, bytesDecrypted.Length);
                    //messageReceived = rsa.Decrypt(messageReceived);
                    MessageBox.Show(messageReceived);
                    string[] arrayReceived = messageReceived.Split('#');
                    int requestNumber = Convert.ToInt32(arrayReceived[0]);
                    string text = arrayReceived[1];


                    if (requestNumber == registerStatusResponse)
                    {
                        if (text.Equals("ok"))
                        {
                            name = textBoxName.Text;
                            MessageBox.Show("valid details");

                            OpenSnifferForm();
                            return;

                        }
                        else if (text.Equals("not ok"))
                        {
                            MessageBox.Show("invalid username or password, please try again");
                        }

                    }
                    else if (requestNumber == EmailResponse)
                    {
                        string answer = Interaction.InputBox("Please enter the code that has just been sent to your email address:", "Email verification");
                        string textToSend = text + "/" + answer;
                        SendEncryptedMessage(CodeRequest + "#" + textToSend + "#" + textToSend.Length);
                    }
                    else if (requestNumber == QuestionResponse)
                    {
                        string[] textArray = text.Split('/');
                        if (textArray.Length > 1)
                            MessageBox.Show("Please try again");
                        string answer = Interaction.InputBox(textArray[0], "Changing Password");
                        SendEncryptedMessage(AnswerRequest + "#" + answer + "#" + answer.Length);
                    }
                    else if (requestNumber == PasswordRequest)
                    {
                        string password = Interaction.InputBox("Please enter a new password:", "Changing Password");
                        SendEncryptedMessage(PasswordResponse + "#" + password + "#" + password.Length);
                    }
                    else if (requestNumber == PasswordChangeStatusResponse)
                    {
                        if (text.Equals("ok"))
                            MessageBox.Show("password changed successfully");
                        else
                        {
                            string password = Interaction.InputBox("Please try again and enter a new password:", "Changing Password");
                            SendEncryptedMessage(PasswordResponse + "#" + password + "#" + password.Length);
                        }
                    }
                    else if (requestNumber == PublicKeyTransfer)
                    {
                        rsa.SetServerPublicKey(text);
                    }
                }
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

        /// <summary>
        /// When sign in button is clicked sends a request to the server to check if the details that were inserted are valid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SignInButton_Click(object sender, EventArgs e)
        {

            string name = textBoxName.Text;
            string password = textBoxPassword.Text;
            string toSend = name + "/" + password;

            if (SignInButton.Text.Equals("Sign In") && !captchaCode.Equals(textBoxCaptcha.Text.ToUpper()))
            {
                MessageBox.Show("please try again the captcha, the code was " + captchaCode);
                captchaCode = RandomString(6);
                pictureBoxCaptcha.Image = captcha.ProcessRequest(captchaCode);
            }
            else if (SignInButton.Text.Equals("Sign In"))
            {
                SendEncryptedMessage(signInRequest + "#" + toSend + "#" + toSend.Length);
                //SendMessage(signInRequest + "#" + toSend + "#" + toSend.Length);
            }
            else
            {
                toSend += "/" + textBoxEmail.Text + "/" + comboBoxQuestions.SelectedItem.ToString() + "/" + textBoxAnswer.Text;
                SendEncryptedMessage(signUpRequest + "#" + toSend + "#" + toSend.Length);
            }
        }

        /// <summary>
        /// when the link label is clicked, changes the form from the sign in/ sign up environment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabelInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (linkLabelInfo.Text.Equals("Don't have an account? Sign up here!"))
                this.Invoke(new Action(() => ChangeToSignUp()));

            else
                this.Invoke(new Action(() => ChangeToSignIn()));

            textBoxName.Text = "";
            textBoxPassword.Text = "";
        }

        /// <summary>
        /// visual changes in the form that changes it to a sign up form
        /// </summary>
        private void ChangeToSignUp()
        {
            SignInButton.Text = "Sign Up";
            linkLabelInfo.Text = "Already have a account? Sign in here!";
            labelQA.Visible = true;
            comboBoxQuestions.Visible = true;
            textBoxAnswer.Visible = true;
            textBoxEmail.Visible = true;
            labelEmail.Visible = true;
            textBoxCaptcha.Visible = false;
            pictureBoxCaptcha.Visible = false;
            pictureBoxRefresh.Visible = false;
        }

        /// <summary>
        /// visual changes in the form that changes it to a sign in form
        /// </summary>
        private void ChangeToSignIn()
        {
            SignInButton.Text = "Sign In";
            linkLabelInfo.Text = "Don't have an account? Sign up here!";
            labelQA.Visible = false;
            comboBoxQuestions.Visible = false;
            textBoxAnswer.Visible = false;
            textBoxEmail.Visible = false;
            labelEmail.Visible = false;
            textBoxCaptcha.Visible = true;
            pictureBoxCaptcha.Visible = true;
            pictureBoxRefresh.Visible = true;

            captchaCode = RandomString(6);
            pictureBoxCaptcha.Image = captcha.ProcessRequest(captchaCode);
        }

        /// <summary>
        /// when the forget password label is clicked, opens an input box that allows the user to enter his username and sends it to the server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabelForgotPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string username = Interaction.InputBox("Please enter your username:", "Changing Password");
            SendEncryptedMessage(QuestionRequest + "#" + username + "#" + username.Length);
        }

        /// <summary>
        /// creates a random string according to the length it gets
        /// </summary>
        /// <param name="length">number of wanted characters in the code</param>
        /// <returns>random code</returns>
        public static string RandomString(int length)
        {
            string s = "";
            Random rnd = new Random();
            int validChars = 0;

            while (validChars < length)
            {
                int randomInt = rnd.Next(50, 90);
                if ((randomInt < 58) || (randomInt > 64))
                {
                    s += Convert.ToChar(randomInt);
                    validChars++;
                }
            }

            return s;
        }

        /// <summary>
        /// creates a new captcha when the refresh button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxRefresh_Click(object sender, EventArgs e)
        {
            captchaCode = RandomString(6);
            pictureBoxCaptcha.Image = captcha.ProcessRequest(captchaCode);
        }

        public void OpenSnifferForm()
        {
            // hides the last form
            this.Invoke(new Action(() => this.Hide()));
            name = textBoxName.Text;
            // opens a new menu form that gets the tcp client
            SnifferForm sniffer = new SnifferForm(client);
            this.Invoke(new Action(() => sniffer.ShowDialog()));
            return;
        }
    }
}