using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace iRecruit.Security
{
    public class Token
    {
        public Token(string userid, string clientAddress, string applicationKey, string accessFeatures)
        {
            User = userid;
            ClientAddress = clientAddress;
            ApplicationKey = applicationKey;
            AccessFeatures = accessFeatures;
        }

        public string User { get; private set; }
        public string ClientAddress { get; private set; }
        public string ApplicationKey { get; private set; }
        public string AccessFeatures { get; private set; }
        public string Encrypt()
        {
            CryptographyHelper cryptographyHelper = new CryptographyHelper();
            X509Certificate2 certificate = cryptographyHelper.GetX509Certificate("CN=Auth-Token");
            return cryptographyHelper.Encrypt(certificate, this.ToString());
        }

        public override string ToString()
        {
            return String.Format("User={0}@^ClientAddress={1}@^ApplicationKey={2}@^AccessFeatures={3}", this.User, this.ClientAddress, this.ApplicationKey, this.AccessFeatures);
        }

        public static Token Decrypt(string encryptedToken)
        {
            CryptographyHelper cryptographyHelper = new CryptographyHelper();
            X509Certificate2 certificate = cryptographyHelper.GetX509Certificate("CN=Auth-Token");
            string decrypted = cryptographyHelper.Decrypt(certificate, encryptedToken);

            //Splitting it to dictionary
            Dictionary<string, string> dictionary = decrypted.ToDictionary();
            return new Token(dictionary["User"], dictionary["ClientAddress"], dictionary["ApplicationKey"], dictionary["AccessFeatures"]);
        }

        #region No Certificate
        static readonly string PasswordHash = "P@@Sword@";
        static readonly string SaltKey = "SA&LT&KEY";
        static readonly string VIKey = "@1b2c3D4E5F6g7h8";
        public string EncryptKey()
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(this.ToString());

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }

        public static Token DecryptKey(string encryptedText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            string decrypted = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
            decrypted = decrypted.Replace("@^", ";");
            //Splitting it to dictionary
            Dictionary<string, string> dictionary = decrypted.ToDictionary();
            return new Token(dictionary["User"], dictionary["ClientAddress"], dictionary["ApplicationKey"], dictionary["AccessFeatures"]);
        }

        #endregion
    }
}
