using System;
using System.Security.Cryptography;

namespace SnifferClient
{
    /// <summary>
    /// class that is responsible for RSA cryptogtaphy
    /// </summary>
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
            ClientPrivateKey = new RSACryptoServiceProvider(2048);
            ClientPublicKey = ClientPrivateKey.ToXmlString(false);
        }

        /// <summary>
        /// sets the server's public key
        /// </summary>
        /// <param name="publicKey">server's public key</param>
        public void SetServerPublicKey(string publicKey)
        {
            ServerPublicKey = new RSACryptoServiceProvider(2048);
            ServerPublicKey.FromXmlString(publicKey);
        }

        /// <summary>
        /// returns the original public key that the client created
        /// </summary>
        /// <returns>client's public key</returns>
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

                //Encrypt the passed byte array and specify OAEP padding.   
                //OAEP padding is only available on Microsoft Windows XP or 
                //later.  
                encryptedData = ServerPublicKey.Encrypt(DataToEncrypt, false);
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

                //Decrypt the passed byte array and specify OAEP padding.   
                //OAEP padding is only available on Microsoft Windows XP or 
                //later.  
                decryptedData = ClientPrivateKey.Decrypt(DataToDecrypt, false);
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
