using IdentityBase.Models;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace WebApp.ChainOfResponsibility.ChainOfRes
{
    public class SendEmailProcessHandler : ProcessHandler
    {
        private readonly string _fileName;
        private readonly string _toEmail;
        private readonly IConfiguration _configuration;

        public SendEmailProcessHandler(string fileName, IConfiguration _configuration, string toEmail)
        {
            _fileName = fileName;
            _toEmail = toEmail;
            this._configuration = _configuration;
        }
        public override object Handle(object request)
        {
            try
            {
                var zipMemoryStream = request as MemoryStream;
                if (zipMemoryStream == null)
                    throw new ArgumentNullException(nameof(zipMemoryStream));

                zipMemoryStream.Position = 0;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress($"{_configuration.GetSection("SmtpClient")["FromAddress"]}"),
                    Subject = "Zip File",
                    Body = "Zip File Ektedir",
                    IsBodyHtml = true
                };
                mailMessage.To.Add(new MailAddress($"{_toEmail}"));

                var attachment = new Attachment(zipMemoryStream, _fileName, MediaTypeNames.Application.Zip);
                mailMessage.Attachments.Add(attachment);

                using (var smtpClient = new SmtpClient(_configuration["SmtpClient:Host"]))
                {
                    smtpClient.Port = Convert.ToInt32(_configuration["SmtpClient:Port"]);
                    smtpClient.Credentials = new NetworkCredential(
                        _configuration["SmtpClient:Username"],
                        _configuration["SmtpClient:AppPassword"]
                    );
                    smtpClient.EnableSsl = true;

                    smtpClient.Send(mailMessage);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return base.Handle(null!);
        }
    }
}
