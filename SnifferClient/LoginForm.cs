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
using System.Diagnostics;

namespace SnifferClient
{
    public partial class LoginForm : Form
    {
        private int portNo = 500; // stores the port number of the server
        private string ipAddress = "127.0.0.1"; // stores the ip of the server
        private string name; // stores the player's name
        private TcpClient client; // client Socket
        private byte[] data; //stores the data that sends to & from the server

        Captcha captcha = new Captcha(); // object to creat captcha
        string captchaCode; // stores the current correct captch code

        // Create a UnicodeEncoder to convert between byte array and string.
        UnicodeEncoding ByteConverter = new UnicodeEncoding();

        RsaCrypto rsa; // RSA object for encryption and decryption
        AesCrypto aes; // AES object for encryption and decryption

        // requests' kinds
        const int signUpRequest = 1;
        const int signInRequest = 2;
        const int registerStatusResponse = 3;
        const int QuestionRequest = 4;
        const int EmailRequest = 5;
        const int CodeResponse = 6;
        const int QuestionResponse = 7;
        const int AnswerResponse = 8;
        const int PasswordRequest = 9;
        const int PasswordResponse = 10;
        const int PasswordChangeStatusResponse = 11;
        const int RSAPublicKeyTransfer = 12;
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
            Debug.WriteLine("Constructor");
            // Read data from the client async
            data = new byte[client.ReceiveBufferSize];

            // initializes the captcha
            captchaCode = RandomString(6);
            pictureBoxCaptcha.Image = captcha.ProcessRequest(captchaCode);

            // creates a RSA object and sends its public key to the server
            rsa = new RsaCrypto();
            string messageToSend = RSAPublicKeyTransfer + "#" + rsa.GetClientPublicKey() + "#" + rsa.GetClientPublicKey().Length;
            SendMessage(ByteConverter.GetBytes(messageToSend));

            // start reading data
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

                // send the text
                ns.Write(message, 0, message.Length);
                ns.Flush();
                Debug.WriteLine("sent message");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// gets a string message, encrypts it using AES protocol and sends it to the server
        /// </summary>
        /// <param name="message">string to encrypt and send to the server</param>
        private void SendAesEncryptedMessage(string message)
        {
            Debug.WriteLine("sending aes: " + message);
            byte[] toSend = aes.EncryptStringToBytes(message, aes.GetKey(), aes.GetIV());
            SendMessage(toSend);
        }

        /// <summary>
        /// gets bytes, encrypts it using RSA protocol and sends it to the server
        /// </summary>
        /// <param name="message">bytes to encrypt and send to the server</param>
        private void SendRsaEncryptedMessage(byte[] message)
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

                if (rsa.ServerPublicKey == null) // if RSA object is missing server's public key
                {
                    rsa.SetServerPublicKey(ByteConverter.GetString(data).Split('#')[1]);
                    // creates an Aes instance for symmetric encryption and sends the key to the server
                    aes = new AesCrypto();
                    byte[] bytesArray = AesKeyAndIVBytesToSend(aes.GetKey(), aes.GetIV());
                    SendRsaEncryptedMessage(bytesArray);
                }
                else
                {
                    byte[] arrived = new byte[bytesRead];
                    Array.Copy(data, arrived, bytesRead);
                    string messageReceived;
                    if (aes == null) // if AES object wasn't initialize yet
                    {
                        messageReceived = ByteConverter.GetString(arrived);
                    }
                    else
                    {
                        messageReceived = aes.DecryptStringFromBytes(arrived, aes.GetKey(), aes.GetIV());
                    }
                    Debug.WriteLine("received: " + messageReceived);
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
                    else if (requestNumber == EmailRequest)
                    {
                        string answer = Interaction.InputBox("Please enter the code that has just been sent to your email address:", "Email verification");
                        string textToSend = text + "/" + answer;
                        SendAesEncryptedMessage(CodeResponse + "#" + textToSend + "#" + textToSend.Length);
                    }
                    else if (requestNumber == QuestionResponse)
                    {
                        string[] textArray = text.Split('/');
                        if (textArray.Length > 1)
                            MessageBox.Show("Please try again");
                        string answer = Interaction.InputBox(textArray[0], "Changing Password");
                        SendAesEncryptedMessage(AnswerResponse + "#" + answer + "#" + answer.Length);
                    }
                    else if (requestNumber == PasswordRequest)
                    {
                        string password = Interaction.InputBox("Please enter a new password:", "Changing Password");
                        SendAesEncryptedMessage(PasswordResponse + "#" + password + "#" + password.Length);
                    }
                    else if (requestNumber == PasswordChangeStatusResponse)
                    {
                        if (text.Equals("ok"))
                            MessageBox.Show("password changed successfully");
                        else
                        {
                            string password = Interaction.InputBox("Please try again and enter a new password:", "Changing Password");
                            SendAesEncryptedMessage(PasswordResponse + "#" + password + "#" + password.Length);
                        }
                    }
                    else if (requestNumber == RSAPublicKeyTransfer)
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
                Debug.WriteLine("signing in?");
                SendAesEncryptedMessage(signInRequest + "#" + toSend + "#" + toSend.Length);
            }
            else // sign up
            {
                Debug.WriteLine("signing up?");
                toSend += "/" + textBoxEmail.Text + "/" + comboBoxQuestions.SelectedItem.ToString() + "/" + textBoxAnswer.Text;
                SendAesEncryptedMessage(signUpRequest + "#" + toSend + "#" + toSend.Length);
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
            SendAesEncryptedMessage(QuestionRequest + "#" + username + "#" + username.Length);
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

        /// <summary>
        /// opens the sniffer form and closes the login form
        /// </summary>
        public void OpenSnifferForm()
        {
            // hides the last form
            this.Invoke(new Action(() => this.Hide()));
            name = textBoxName.Text;
            // opens a new menu form that gets the tcp client
            RawSockSnifferForm sniffer = new RawSockSnifferForm(client, aes);
            this.Invoke(new Action(() => sniffer.ShowDialog()));
            this.Invoke(new Action(() => this.Close()));
            return;
        }

        /// <summary>
        /// converts bytes to string in decimal base
        /// </summary>
        /// <param name="arr">bytes to convert</param>
        /// <returns>converted string</returns>
        public static string BytesToString(byte[] arr)
        {
            string s = "";
            foreach (byte b in arr)
            {
                s += b.ToString() + " ";
            }
            return s;
        }

        /// <summary>
        /// gets key and iv in the form of bytes and creates a message to send to the server
        /// </summary>
        /// <param name="key">bytes that represent the AES Key</param>
        /// <param name="iv">bytes that represent the AES IV</param>
        /// <returns>byte array that contains the message to the server</returns>
        public byte[] AesKeyAndIVBytesToSend(byte[] key, byte[] iv)
        {
            Debug.WriteLine("key length: " + key.Length);
            Debug.WriteLine("iv length: " + iv.Length);

            int keyLength = key.Length;
            int ivLength = iv.Length;

            byte[] byteArray = new byte[keyLength + ivLength + 4];
            byteArray[0] = (byte)AesKeyTransfer; //request number
            byteArray[1] = (byte)keyLength;
            byteArray[2] = (byte)ivLength;
            Array.Copy(key, 0, byteArray, 3, keyLength);
            Array.Copy(iv, 0, byteArray, 3 + keyLength, ivLength);
            byteArray[byteArray.Length - 1] = (byte)(byteArray.Length - 1); //number of bytes, not included the last one
            Debug.WriteLine(BytesToString(byteArray));
            return byteArray;

        }
    }
}