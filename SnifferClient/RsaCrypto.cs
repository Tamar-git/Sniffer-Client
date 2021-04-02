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

        private RSACryptoServiceProvider ClientPrivateKey; //client's private key
        public RSACryptoServiceProvider ServerPublicKey; //server's public key
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
            ServerPublicKey = new RSACryptoServiceProvider(2048);
            ServerPublicKey.FromXmlString(publicKey);
            //_publicKey = publicKey;
        }

        /// <summary>
        /// returns the original public key that the client created
        /// </summary>
        /// <returns></returns>
        public string GetClientPublicKey()
        {
            return ClientPublicKey;
        }

        /// <summary>
        /// encryptes bytes using RSA protocol
        /// </summary>
        /// <param name="DataToEncrypt">original bytes</param>
        /// <returns>encrypted bytes</returns>
        public byte[] RSAEncrypt(byte[] DataToEncrypt)
        {
            try
            {
                byte[] encryptedData;
                //RSAParameters RSAKeyInfo = ServerPublicKey.ExportParameters(false);
                //Create a new instance of RSACryptoServiceProvider. 
                //using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                //{

                //    //Import the RSA Key information. This only needs 
                //    //toinclude the public key information.
                //    RSA.ImportParameters(RSAKeyInfo);

                //    //Encrypt the passed byte array and specify OAEP padding.   
                //    //OAEP padding is only available on Microsoft Windows XP or 
                //    //later.  
                encryptedData = ServerPublicKey.Encrypt(DataToEncrypt, false);
                //}
                return encryptedData;
            }
            //Catch and display a CryptographicException   
            //to the console. 
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }

        /// <summary>
        /// decryptes encrypted bytes using RSA protocol
        /// </summary>
        /// <param name="DataToDecrypt">encrypted bytes</param>
        /// <returns>decrypted bytes</returns>
        public byte[] RSADecrypt(byte[] DataToDecrypt)
        {
            try
            {
                byte[] decryptedData;
                //RSAParameters RSAKeyInfo = ClientPrivateKey.ExportParameters(false);
                //Create a new instance of RSACryptoServiceProvider. 
                //using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                //{
                //    //Import the RSA Key information. This needs 
                //    //to include the private key information.
                //    RSA.ImportParameters(RSAKeyInfo);

                //    //Decrypt the passed byte array and specify OAEP padding.   
                //    //OAEP padding is only available on Microsoft Windows XP or 
                //    //later.  
                decryptedData = ClientPrivateKey.Decrypt(DataToDecrypt, false);
                //}
                return decryptedData;
            }
            //Catch and display a CryptographicException   
            //to the console. 
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }
        }

    }
}
