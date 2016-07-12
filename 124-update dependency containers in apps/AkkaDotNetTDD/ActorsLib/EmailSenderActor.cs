using Akka.Actor;
using DisplayServiceLib;
using EmailServiceLib;

namespace ActorsLib
{
    public class EmailSenderActor:ReceiveActor
    {
        public EmailSenderActor(IEmailService emailService, IDisplayService displayService)
        {
            Receive<string>(message =>
            {
                emailService.SendEmail(null);
                displayService.SendDisplayMessage("I got an email from " + Sender);
            });
        }
    }
}