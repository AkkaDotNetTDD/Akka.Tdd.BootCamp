namespace AkkaDotNetTDD.Tests
{
    public class EmailService : IEmailService
    {
        public bool SendEmail(EmailMessage emailMessage)
        {
            if (string.IsNullOrEmpty(emailMessage.ToEmail) ||
                string.IsNullOrEmpty(emailMessage.FromEmail) ||
                string.IsNullOrEmpty(emailMessage.Subject) ||
                string.IsNullOrEmpty(emailMessage.Body)
                )
            {
                return false;
            }
            return true;
        }
    }
}