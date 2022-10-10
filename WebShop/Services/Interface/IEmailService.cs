namespace WebShop.Services.Interface;

public interface IEmailService
{
    void SendEmail(EmailDto request);
    Task SendEmail(string email, string subject, string htmlMessage);
    void SendEmailMessage(ContactBinding model);
}