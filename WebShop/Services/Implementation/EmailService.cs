namespace WebShop.Services.Implementation;

public class EmailService : IEmailService
{
    private readonly IConfiguration config;

    public EmailService(IConfiguration config)
    {
        this.config = config;
    }

    /// <summary>
    /// SendEmail
    /// </summary>
    /// <param name="to"></param>
    /// <param name="subject"></param>
    /// <param name="body"></param>
    /// <returns></returns>
    public Task SendEmail(string to, string subject, string body)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(this.config.GetSection("EmailUsername").Value));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

        //send email
        using (var smtp = new SmtpClient())
        {
            smtp.Connect(this.config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(this.config.GetSection("EmailUsername").Value, this.config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
        return Task.CompletedTask;
    }
    
    public void SendEmail(EmailDto request)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(this.config.GetSection("EmailUsername").Value));
        email.To.Add(MailboxAddress.Parse(request.To));
        email.Subject = request.Subject;
        email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

        using var smtp = new SmtpClient();
        smtp.Connect(this.config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
        smtp.Authenticate(this.config.GetSection("EmailUsername").Value, this.config.GetSection("EmailPassword").Value);
        smtp.Send(email);
        smtp.Disconnect(true);
    }

    /// <summary>
    /// SendEmailMessage
    /// </summary>
    /// <param name="model"></param>
    public void SendEmailMessage(ContactBinding model)
    {
        model.To = this.config.GetSection("EmailUsername").Value;
        model.Subject = "Bolta WebShop Message from: " + model.MessageName;
        model.Body = "<h3>" + model.MessageName + "</h3><br/>" + model.MessageEmail + "<br/><br/><h4>" + model.Body + "</h4>";
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(this.config.GetSection("EmailUsername").Value));
        email.To.Add(MailboxAddress.Parse(model.To));
        email.Subject = model.Subject;
        email.Body = new TextPart(TextFormat.Html) { Text = model.Body };

        using var smtp = new SmtpClient();
        smtp.Connect(this.config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
        smtp.Authenticate(this.config.GetSection("EmailUsername").Value, this.config.GetSection("EmailPassword").Value);
        smtp.Send(email);
        smtp.Disconnect(true);
    }
}