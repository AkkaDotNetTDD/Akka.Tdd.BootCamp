using Akka.Actor;
using Akka.Tdd.Core;

namespace ActorsLib
{
    public class EmailSenderActorCoOrdinator<TEmailSenderActor> : ReceiveActor where TEmailSenderActor : ActorBase
    {
        public EmailSenderActorCoOrdinator()
        {
            var emailSenderActor = Context.CreateActor<TEmailSenderActor>();
            Receive<string>(message =>
            {
                emailSenderActor.Tell("hello");
            });
        }
    }
}