namespace ePOS.Application.Contracts;

public interface IMailService
{
    Task<bool> SendMailAsync(string toMail, string toName, string subject, string body);
}