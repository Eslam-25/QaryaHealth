using Microsoft.Extensions.Options;
using Services.Abstraction.Interfaces;
using Services.Abstraction.Models;
using Services.EmailEngine;
using System.Net;
using System.Net.Mail;

namespace Services.EmailService;

public class EmailService : IEmailService
{
    private readonly EmailConfig _emailConfig;
    public EmailService(IOptions<EmailConfig> emailConfig)
    {
        _emailConfig = emailConfig.Value;
    }

    public bool SendAsync(PayloadModel payload)
    {
        try
        {
            var client = new SmtpClient(_emailConfig.Host, _emailConfig.Port);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(_emailConfig.SendFrom, _emailConfig.Password);
            MailMessage mailMessage = new MailMessage(_emailConfig.SendFrom, payload.To, payload.Subject, payload.Body);
            mailMessage.IsBodyHtml = true;
            client.Send(mailMessage);
            return true;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.InnerException);
            return false;
        }
    }
}
