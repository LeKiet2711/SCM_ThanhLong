using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using Telerik.Reporting.Services;
using Telerik.Reporting.Services.AspNetCore;

namespace SCM_ThanhLong_Group.Components.Pages.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Net;
    using System.Net.Mail;
    using Telerik.Reporting.Services;
    using Telerik.Reporting.Services.AspNetCore;

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
                smtpClient.EnableSsl = true; // Đặt thành true để kích hoạt SSL

                smtpClient.Send(mailMessage);
            }
            return HttpStatusCode.OK;
        }


    }
}
