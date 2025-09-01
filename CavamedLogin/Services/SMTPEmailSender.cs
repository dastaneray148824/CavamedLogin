using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;

public class SmtpOptions
{
    public string From { get; set; } = "";
    public string Host { get; set; } = "";
    public int Port { get; set; } = 587;
    public string User { get; set; } = "";
    public string Pass { get; set; } = "";
    public bool EnableSsl { get; set; } = true;
}

public class SMTPEmailSender : IEmailSender
{
    private readonly SmtpOptions _opt;
    public SMTPEmailSender(IOptions<SmtpOptions> opt) => _opt = opt.Value;

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var msg = new MimeMessage();
        msg.From.Add(MailboxAddress.Parse(_opt.From));
        msg.To.Add(MailboxAddress.Parse(email));
        msg.Subject = subject;
        msg.Body = new BodyBuilder { HtmlBody = htmlMessage }.ToMessageBody();

        using var client = new SmtpClient();
        await client.ConnectAsync(_opt.Host, _opt.Port,
            _opt.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto);
        if (!string.IsNullOrWhiteSpace(_opt.User))
            await client.AuthenticateAsync(_opt.User, _opt.Pass);

        await client.SendAsync(msg);
        await client.DisconnectAsync(true);
    }
}
