using Oracle.ManagedDataAccess.Client;
using SCM_ThanhLong_Group.Model;
using SCM_ThanhLong_Group.Components.Connection;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

public class XacThucService
{
    private readonly OracleDbConnection _dbConnection;
    private readonly Users _user;

    public XacThucService(OracleDbConnection dbConnection, Users user)
    {
        _dbConnection = dbConnection;
        _user = user;
    }

    public async Task<bool> ValidateKey(string key)
    {
        using (var connection = _dbConnection.GetConnection(_user.username, _user.password))
        {
            await connection.OpenAsync();
            using (var command = new OracleCommand("SELECT COUNT(*) FROM KEY_ASE WHERE KEY_VALUE = :key",connection))
            {
                command.Parameters.Add(new OracleParameter("key", OracleDbType.Varchar2, key, ParameterDirection.Input));

                try
                {
                    var result = await command.ExecuteScalarAsync();
                    return Convert.ToInt32(result) > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error executing query", ex);
                }
            }
        }
    }

    public string EncryptAES(string plainText, string key)
    {
        if (string.IsNullOrEmpty(plainText))
            throw new ArgumentNullException(nameof(plainText));
        if (string.IsNullOrEmpty(key))
            throw new ArgumentNullException(nameof(key));

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(key);
            aesAlg.IV = new byte[16]; // Initialization vector with zeros

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (var msEncrypt = new System.IO.MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }
    }

    public string Decrypt(string cipherText, string key)
    {
        if (string.IsNullOrEmpty(cipherText))
            throw new ArgumentNullException(nameof(cipherText));
        if (string.IsNullOrEmpty(key))
            throw new ArgumentNullException(nameof(key));

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(key);
            aesAlg.IV = new byte[16]; // Initialization vector with zeros

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (var msDecrypt = new System.IO.MemoryStream(Convert.FromBase64String(cipherText)))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (var srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }

    public string GenerateRandomText(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
