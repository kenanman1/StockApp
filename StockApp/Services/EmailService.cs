using System.Net;
using System.Net.Mail;

namespace StockApp.Services;

public class EmailService : IEmailService
{
    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress("kenanaliyev13@gmail.com", "Stock App"),
            Subject = subject,
            Body = message,
            IsBodyHtml = true
        };

        mailMessage.To.Add(new MailAddress(email));
        using SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)
        {
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential("kenanaliyev13@gmail.com", "pangjsbuxxjxvtol")
        };
        try
        {
            await smtpClient.SendMailAsync(mailMessage);
        }
        catch (Exception ex)
        {
            
        }
    }
}
