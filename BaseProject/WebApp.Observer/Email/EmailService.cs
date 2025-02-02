
using FluentEmail.Core;
using System.Net.Mail;


namespace WebApp.Observer.Email
{
    public class EmailService : IEmailService
    {
        private readonly IFluentEmail _fluentEmail;
        public EmailService(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
        }
        public async Task SendEmailAsync(string toEmail, string subject, string message, CancellationToken cancellationToken)
        {
            var email = await _fluentEmail.To(toEmail)
                .Subject(subject)
                .Body(message)
                .SendAsync(cancellationToken);
            if (!email.Successful)
            {
                throw new Exception("Email could not be sent");
            }
        }   
    }
}
