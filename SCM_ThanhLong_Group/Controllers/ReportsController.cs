using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using Telerik.Reporting.Services;
using Telerik.Reporting.Services.AspNetCore;

namespace SCM_ThanhLong_Group.Components.Pages.Controllers
{
    [Route("api/reports")]
    public class ReportsController : ReportsControllerBase
    {
        public ReportsController(IReportServiceConfiguration reportServiceConfiguration)
            : base(reportServiceConfiguration)
        {
        }

        public static byte[] EncryptWithAES(byte[] reportData, out byte[] aesKey, out byte[] aesIV)
        {
            using (Aes aes = Aes.Create())
            {
                aesKey = aes.Key;
                aesIV = aes.IV;

                using (var encryptor = aes.CreateEncryptor(aesKey, aesIV))
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        cs.Write(reportData, 0, reportData.Length);
                    }
                    return ms.ToArray();
                }
            }
        }

        public static byte[] EncryptAESKeyWithRSA(byte[] aesKey, RSAParameters publicKey)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(publicKey);
                return rsa.Encrypt(aesKey, false); // Mã hóa khóa AES bằng RSA
            }
        }

        private byte[] EncryptReport(byte[] reportData, RSAParameters publicKey)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(publicKey);
                return rsa.Encrypt(reportData, false);
            }
        }

        protected override HttpStatusCode SendMailMessage(MailMessage mailMessage)
        {
            //byte[] reportData = System.IO.File.ReadAllBytes("C:\\mg\\SampleReport.pdf");

            //// Khóa công khai của người nhận
            //RSAParameters publicKey = new RSAParameters
            //{
            //    Modulus = Convert.FromBase64String("MIIBIANBgkqjkqhANBgkqhkiG9w0BhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAuK5Q5Q=="),
            //    Exponent = Convert.FromBase64String("AQABgkqjkqhAhANNBgkqhANhkiG9w0BA")
            //};
            //byte[] encryptedReport = EncryptReport(reportData, publicKey);

            //// Tạo tệp đính kèm từ báo cáo đã mã hóa
            //var attachment = new Attachment(new MemoryStream(encryptedReport), "report_encrypted.pdf", "application/pdf");
            //mailMessage.Attachments.Add(attachment);

            using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
            {
                smtpClient.Credentials = new NetworkCredential("lgklgk2711@gmail.com", "lpwliwpwybzylkpj");
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true; // Đặt thành true để kích hoạt TTS

                smtpClient.Send(mailMessage);
            }
            return HttpStatusCode.OK;
        }
    }
}
