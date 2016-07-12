using Akka.Actor;
using EmailServiceLib;

namespace ActorsLib
{
    public class EmailSenderActor:ReceiveActor
    {
        public EmailSenderActor(IEmailService emailService)
        {
            Receive<string>(message =>
            {
                emailService.SendEmail(null);
            });
        }
    }
}