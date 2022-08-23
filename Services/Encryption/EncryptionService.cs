using Microsoft.Extensions.Configuration;
using System;
using System.Security.Cryptography;
using System.Text;


namespace StandingOrders.API.Services.Encryption
{
    public class EncryptionService : IEncryptionService
    {
        private readonly IConfiguration _config;
        private readonly byte[] key;

        public EncryptionService(IConfiguration config)
        {
            _config = config;
            key = Convert.FromBase64String(_config.GetValue<string>("EncryptionSettings:key"));
        }


        public string Encrypt(string dataToEncrypt)
        {
            byte[] dataToEncryptByteArray = Encoding.UTF8.GetBytes(dataToEncrypt);
            byte[] nonce = new byte[12];
            byte[] tag = new byte[16];
            byte[] ciphertext = new byte[dataToEncryptByteArray.Length];

            RandomNumberGenerator.Fill(nonce);
            
            using (AesCcm aes = new AesCcm(key))
                aes.Encrypt(nonce, dataToEncryptByteArray, ciphertext, tag);

            return (Convert.ToBase64String(nonce) + Convert.ToBase64String(tag) + Convert.ToBase64String(ciphertext));
        }


        public string Decrypt(string dataToDecrypt)
        {
            byte[] nonce = Convert.FromBase64String(dataToDecrypt.Substring(0, 16));
            byte[] tag = Convert.FromBase64String(dataToDecrypt.Substring(16, 24));
            byte[] ciphertext = Convert.FromBase64String(dataToDecrypt.Substring(40, 20));
            byte[] decryptedData = new byte[ciphertext.Length];

            using (AesCcm aes = new AesCcm(key))
                aes.Decrypt(nonce, ciphertext, tag, decryptedData);
            string decryptedToken = Encoding.UTF8.GetString(decryptedData);

            return decryptedToken;           
        }       
    }
}
