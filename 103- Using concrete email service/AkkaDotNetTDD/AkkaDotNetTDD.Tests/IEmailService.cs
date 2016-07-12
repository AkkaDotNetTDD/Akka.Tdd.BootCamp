namespace AkkaDotNetTDD.Tests
{
    public interface IEmailService
    {
        bool SendEmail(EmailMessage emailMessage);
    }
}