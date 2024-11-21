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

        protected override HttpStatusCode SendMailMessage(MailMessage mailMessage)
        {
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
