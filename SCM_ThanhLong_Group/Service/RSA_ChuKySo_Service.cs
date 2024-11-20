using System;
using System.IO;
using System.Security.Cryptography;

namespace SCM_ThanhLong_Group.Service
{
    public class RSA_ChuKySo_Service
    {
        public (string PublicKey, string PrivateKey) GenerateKeys()
        {
            using (var rsa = RSA.Create(2048)) // Tạo khóa RSA 2048-bit
            {
                string publicKey = Convert.ToBase64String(rsa.ExportSubjectPublicKeyInfo());
                string privateKey = Convert.ToBase64String(rsa.ExportPkcs8PrivateKey());
                return (publicKey, privateKey);
            }
        }

        public byte[] ComputeHash(byte[] fileContent)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(fileContent); // Tạo giá trị hash từ nội dung file
            }
        }

        public byte[] SignData(byte[] hash, string privateKey)
        {
            using (var rsa = RSA.Create())
            {
                rsa.ImportPkcs8PrivateKey(Convert.FromBase64String(privateKey), out _);
                return rsa.SignData(hash, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            }
        }

        public bool VerifySignature(byte[] hash, byte[] signature, string publicKey)
        {
            using (var rsa = RSA.Create())
            {
                rsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(publicKey), out _);
                return rsa.VerifyData(hash, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            }
        }

        public void SaveSignedFile(byte[] originalFileContent, byte[] signature, string outputPath)
        {
            // Tạo file kết hợp chữ ký và nội dung file PDF
            using (var fs = new FileStream(outputPath, FileMode.Create))
            {
                // Ghi nội dung file gốc
                fs.Write(originalFileContent, 0, originalFileContent.Length);

                // Ghi chữ ký vào cuối file (hoặc metadata nếu dùng thư viện PDF)
                fs.Write(signature, 0, signature.Length);
            }
        }
    }
}
