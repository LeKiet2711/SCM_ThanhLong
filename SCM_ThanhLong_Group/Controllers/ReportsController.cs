using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
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

        public static byte[] EncryptPdf(byte[] pdfData, string userPassword, string ownerPassword)
        {
            using (var input = new MemoryStream(pdfData))
            using (var output = new MemoryStream())
            {
                var reader = new PdfReader(input);
                PdfEncryptor.Encrypt(reader, output, true, userPassword, ownerPassword, PdfWriter.ALLOW_PRINTING);
                return output.ToArray();
            }
        }

        protected override HttpStatusCode SendMailMessage(MailMessage mailMessage)
        {
            string userPassword = "952648";
            string ownerPassword = "password";

            // Tạo danh sách tệp đính kèm mới
            var newAttachments = new List<Attachment>();

            // Mã hóa nội dung tệp đính kèm
            foreach (var attachment in mailMessage.Attachments)
            {
                using (var memoryStream = new MemoryStream())
                {
                    attachment.ContentStream.CopyTo(memoryStream);
                    byte[] encryptedData = EncryptPdf(memoryStream.ToArray(), userPassword, ownerPassword);

                    // Tạo tệp đính kèm mới với nội dung đã mã hóa
                    var newAttachment = new Attachment(new MemoryStream(encryptedData), attachment.Name, attachment.ContentType.MediaType);
                    newAttachments.Add(newAttachment);
                }
            }

            // Xóa các tệp đính kèm cũ
            mailMessage.Attachments.Clear();

            // Thêm các tệp đính kèm mới
            foreach (var newAttachment in newAttachments)
            {
                mailMessage.Attachments.Add(newAttachment);
            }

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
