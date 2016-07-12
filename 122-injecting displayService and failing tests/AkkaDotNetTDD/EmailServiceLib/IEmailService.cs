namespace EmailServiceLib
{
    public interface IEmailService
    {
        bool SendEmail(EmailMessage emailMessage);
    }
}