using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SnifferClient
{
    class RsaCrypto
    {
        public static string _privateKey;
        public static string _publicKey;
        public static string _clientPublicKey;
        private static UnicodeEncoding _encoder = new UnicodeEncoding();

        
        private RSACryptoServiceProvider ClientPrivateKey; //client's private key
        private RSACryptoServiceProvider ServerPublicKey; //server's public key
        public string ClientPublicKey; //client's public key

        /// <summary>
        /// constructor that creates an rsa object and keys
        /// </summary>
        public RsaCrypto()
        {
            //var rsa = new RSACryptoServiceProvider();
            //_privateKey = rsa.ToXmlString(true);
            //_clientPublicKey = rsa.ToXmlString(false);
            ClientPrivateKey = new RSACryptoServiceProvider(2048);
            ClientPublicKey = ClientPrivateKey.ToXmlString(false);
        }

        /// <summary>
        /// sets the server's public key
        /// </summary>
        /// <param name="publicKey"></param>
        public void SetServerPublicKey(string publicKey)
        {
            ServerPublicKey = new RSACryptoServiceProvider();
            ServerPublicKey.FromXmlString(publicKey);
            //_publicKey = publicKey;
        }

        /// <summary>
        /// gets an encrypted string, decrypts and returns the readable data
        /// </summary>
        /// <param name="data">encrypted data</param>
        /// <returns>original data</returns>
        public string Decrypt(string data)
        {
            
            //var rsa = new RSACryptoServiceProvider();
            //var dataArray = data.Split(new char[] { ',' });
            var dataArray = data.ToCharArray();
            //byte[] dataByte = new byte[dataArray.Length];
            byte[] dataByte = Convert.FromBase64String(data);
            //byte[] dataByte = _encoder.GetBytes(dataArray, 0, dataArray.Length);
            //for (int i = 0; i < dataArray.Length; i++)
            //{
            //    dataByte[i] = Convert.ToByte(dataArray[i]);
            //}

            //rsa.FromXmlString(_privateKey);
            var decryptedByte = ClientPrivateKey.Decrypt(dataByte, false);
            //var decryptedByte = rsa.Decrypt(dataByte, false);
            return Encoding.Unicode.GetString(decryptedByte);
        }

        /// <summary>
        ///  gets an a string and returns it encrypted (using rsa)
        /// </summary>
        /// <param name="data">original data</param>
        /// <returns>encrypted data</returns>
        public string Encrypt(string data)
        {
            //var rsa = new RSACryptoServiceProvider();
            //rsa.FromXmlString(_publicKey);
            //var dataToEncrypt = _encoder.GetBytes(data);
            //var encryptedByteArray = rsa.Encrypt(dataToEncrypt, false);
            //var length = encryptedByteArray.Length;
            //var item = 0;
            //var sb = new StringBuilder();
            //foreach (var x in encryptedByteArray)
            //{
            //    item++;
            //    sb.Append(x);

            //    if (item < length)
            //        sb.Append(",");
            //}
            var dataArray = data.ToCharArray();
            byte[] dataByte = Convert.FromBase64CharArray(dataArray, 0, dataArray.Length);
            var encryptedByte = ServerPublicKey.Encrypt(dataByte, false);
            return _encoder.GetString(encryptedByte);
        }

        /// <summary>
        /// returns the original public key that the client created
        /// </summary>
        /// <returns></returns>
        public string GetClientPublicKey()
        {
            return ClientPublicKey;
        }

        
        //Saves whether the encryption is ready
        //public bool IsEncryptionReady { get; set; }
        
        /// <summary>
        /// Constructor
        /// </summary>
        //public AsymmetricEncryption()
        //{
        //    ServerPrivateKey = new RSACryptoServiceProvider(2048);
        //    ServerPublicKey = ServerPrivateKey.ToXmlString(false);
        //}
        /// <summary>
        /// Sets the client's public key
        /// </summary>
        /// <param name="otherPublicKey">Client's public key as a string</param>
        //public void ClientPublicKey(string otherPublicKey)
        //{
        //    ClientKey = new RSACryptoServiceProvider();
        //    ClientKey.FromXmlString(otherPublicKey);
        //}
        ///// <summary>
        ///// Decrypts a byte array
        ///// </summary>
        ///// <param name="data">Byte array</param>
        ///// <returns>Decrypted byte array</returns>
        //public byte[] Decrypt(byte[] data)
        //{
        //    return ServerPrivateKey.Decrypt(data, false);
        //}
        ///// <summary>
        ///// Encrypts a byte array
        ///// </summary>
        ///// <param name="data">A byte array</param>
        ///// <returns>Encrypted byte array</returns>
        //public byte[] Encrypt(byte[] data)
        //{
        //    return ClientKey.Encrypt(data, false);
        //}
    }
}
