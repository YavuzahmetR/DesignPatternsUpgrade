namespace WebApp.Observer.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string message,CancellationToken cancellationToken);
    }
}
