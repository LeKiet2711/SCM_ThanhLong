using SCM_ThanhLong_Group.Components.Connection;
using System.Runtime.Intrinsics.Arm;
using SCM_ThanhLong_Group.Model;
using System.Text;
using System.Security.Cryptography;
using Telerik.SvgIcons;
using Oracle.ManagedDataAccess.Client;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.Common;
namespace SCM_ThanhLong_Group.Service
{
    public class XacThucService
    {
        private readonly OracleDbConnection _dbConnection;
        public string EncryptAES(string plainText, string key)
        {
            using (System.Security.Cryptography.Aes aesDal = System.Security.Cryptography.Aes.Create())
            {
                aesDal.Key = Encoding.UTF8.GetBytes(key);
                aesDal.IV = new byte[16];
                ICryptoTransform encrytor = aesDal.CreateEncryptor(aesDal.Key, aesDal.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encrytor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }
                    byte[] encrypted = msEncrypt.ToArray();
                    return Convert.ToBase64String(encrypted);
                }
            }
        }
        public XacThucService(OracleDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public string Decrypt(string encryptedText, string secretkey)
        {
            byte[] fullCipher = Convert.FromBase64String(encryptedText);
            byte[] iv = new byte[16];
            byte[] cipherTextBytes = new byte[fullCipher.Length - iv.Length];

            Array.Copy(fullCipher, iv, iv.Length);
            Array.Copy(fullCipher, iv.Length, cipherTextBytes, 0, cipherTextBytes.Length);
            using (System.Security.Cryptography.Aes aes = System.Security.Cryptography.Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(secretkey);
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    using (MemoryStream ms = new MemoryStream(cipherTextBytes))
                    {
                        using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader sr = new StreamReader(cs))
                            {
                                return sr.ReadToEnd();
                            }
                        }
                    }
                }
            }

        }

      

    }
}
            

           


