using ePOS.Shared.Contracts;
using MimeKit;

namespace ePOS.Infrastructure.Services;

public class MailService : IMailService
{
    private readonly MailSetting _mailSetting;
    private readonly ILogger<MailService> _logger;

    public MailService(AppSettings appSettings, ILogger<MailService> logger)
    {
        _logger = logger ?? throw new NullReferenceException(nameof(logger));
        _mailSetting = appSettings.MailSetting ?? throw new NullReferenceException();
    }
    
    public async Task<bool> SendMailAsync(string toMail, string toName, string subject, string body)
    {
        var mimeMessage = new MimeMessage();
        mimeMessage.Sender = new MailboxAddress(_mailSetting.DisplayName, _mailSetting.Mail);
        mimeMessage.From.Add(new MailboxAddress(_mailSetting.DisplayName, _mailSetting.Mail));
        mimeMessage.To.Add(MailboxAddress.Parse(toMail));
        mimeMessage.Subject = subject;
        mimeMessage.Body = new TextPart("html") { Text = body };
        
        using var smtp = new MailKit.Net.Smtp.SmtpClient();
        
        try {
            _logger.LogInformation("Start send mail to {Mail}", toMail);
            await smtp.ConnectAsync(_mailSetting.Host, _mailSetting.Port, false);
            await smtp.AuthenticateAsync (_mailSetting.Mail, _mailSetting.Password);
            await smtp.SendAsync(mimeMessage);
            await smtp.DisconnectAsync(true);
            _logger.LogInformation("Send mail to {Mail} successfully", toMail);
            return true;
        }
        catch (Exception ex) {
            _logger.LogError("Send mail failed: {Error}", ex.Message);
            return false;
        }
    }
}