using CarModelMgmt.WebApi.Services;
using System.Security.Cryptography;
using System.Text;

namespace CarModelMgmt.WebApi.Helpers
{
    public class EncryptionHelper
    {
        private readonly ConfigurationService _configurationService;

        public EncryptionHelper(ConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        public string Encrypt(int id)
        {
            var encryptionSettings = _configurationService.GetEncryptionSettings();

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(encryptionSettings.Key);
                aesAlg.IV = Encoding.UTF8.GetBytes(encryptionSettings.IV);

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(id.ToString());
                        }
                    }

                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public int Decrypt(string encryptedId)
        {
            var encryptionSettings = _configurationService.GetEncryptionSettings();
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(encryptionSettings.Key);
                aesAlg.IV = Encoding.UTF8.GetBytes(encryptionSettings.IV);

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(encryptedId)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            string decryptedId = srDecrypt.ReadToEnd();
                            return int.Parse(decryptedId);
                        }
                    }
                }
            }
        }
    }
}
