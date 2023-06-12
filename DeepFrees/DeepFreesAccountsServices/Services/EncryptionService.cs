using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Security.Cryptography;
using System.Text;

namespace DeepFreesAccountsServices.Services
{
    public static class EncryptionService
    {
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("b14ca5898a4e4133bbce2ea2315a1916"); // 16, 24, or 32 bytes key for AES-128, AES-192, or AES-256
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("testtesttesttest"); // 16 bytes IV for AES

        //AES Industrial Level Encryptor
        public static string EncryptPassword(string password)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                byte[] encryptedBytes = null;
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(password);
                        }
                        encryptedBytes = msEncrypt.ToArray();
                    }
                }

                return Convert.ToBase64String(encryptedBytes);
            }
        }

        public static string DecryptPassword(string encryptedPassword)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedPassword);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                byte[] decryptedBytes = null;
                using (MemoryStream msDecrypt = new MemoryStream(encryptedBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            decryptedBytes = Encoding.UTF8.GetBytes(srDecrypt.ReadToEnd());
                        }
                    }
                }

                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
    }
}
