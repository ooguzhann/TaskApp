using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class EmailSenderImplementation : IEmailSender
{
    private readonly IConfiguration _configuration;

    public EmailSenderImplementation(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var smtpHost = _configuration["Smtp:Host"];
        var smtpPort = int.Parse(_configuration["Smtp:Port"]);
        var smtpEnableSsl = bool.Parse(_configuration["Smtp:EnableSsl"]);
        var smtpUserName = _configuration["Smtp:UserName"];
        var smtpPassword = _configuration["Smtp:Password"];

        var smtpClient = new SmtpClient
        {
            Host = smtpHost,
            Port = smtpPort,
            EnableSsl = smtpEnableSsl,
            Credentials = new NetworkCredential(smtpUserName, smtpPassword)
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(smtpUserName),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true
        };

        mailMessage.To.Add(email);

        return smtpClient.SendMailAsync(mailMessage);
    }
}
