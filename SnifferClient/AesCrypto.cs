using System;
using System.IO;
using System.Security.Cryptography;

namespace SnifferClient
{
    /// <summary>
    /// class that is responsible for AES cryptogtaphy
    /// </summary>
    public class AesCrypto
    {
        private Aes aes; // Represents the abstract base class

        /// <summary>
        /// constructor that creates an Aes object with a key
        /// </summary>
        public AesCrypto()
        {
            aes = Aes.Create();

        }

        /// <summary>
        /// retrieves the Aes key
        /// </summary>
        /// <returns>key</returns>
        public byte[] GetKey()
        {
            return aes.Key;
        }

        /// <summary>
        /// retrieves the Aes IV
        /// </summary>
        /// <returns>IV</returns>
        public byte[] GetIV()
        {
            return aes.IV;
        }

        /// <summary>
        ///  encryptes plain text using AES protocol
        /// </summary>
        /// <param name="plainText">original text string</param>
        /// <param name="Key">AES's key</param>
        /// <param name="IV">AES's initialization vector</param>
        /// <returns>encrypted bytes</returns>
        public byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                aesAlg.Padding = PaddingMode.Zeros;
                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        /// <summary>
        /// Decryptes encrypted bytes using AES protocol
        /// </summary>
        /// <param name="cipherText">encrypted bytes</param>
        /// <param name="Key">AES's key</param>
        /// <param name="IV">AES's initialization vector</param>
        /// <returns>decrypted string</returns>
        public string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            try
            {
                // Declare the string used to hold
                // the decrypted text.
                string plaintext = null;

                // Create an Aes object
                // with the specified key and IV.
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = Key;
                    aesAlg.IV = IV;
                    aesAlg.Padding = PaddingMode.Zeros;
                    // Create a decryptor to perform the stream transform.
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    // Create the streams used for decryption.
                    using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {

                                // Read the decrypted bytes from the decrypting stream
                                // and place them in a string.
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
                return plaintext;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }
        }
    }
}
