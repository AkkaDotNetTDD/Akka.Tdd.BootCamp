namespace AkkaDotNetTDD.Tests
{
    public class EmailService : IEmailService
    {
        public bool SendEmail(EmailMessage emailMessage)
        {
            return true;
        }
    }
}