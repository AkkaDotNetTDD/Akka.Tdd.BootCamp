namespace EmailServiceLib
{
    public class EmailMessage
    {
        public string ToEmail { get; set; }
        public string FromEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}