using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActorsLib;
using Akka.Actor;
using Akka.Tdd.AutoFac;
using Akka.Tdd.Core;
using Autofac;
using DisplayServiceLib;
using EmailServiceLib;

namespace EmailSenderCoOrdinatorActorApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting EmailSenderCoOrdinatorActor App ...");
            var builder = new ContainerBuilder();
            builder.Register<IEmailService>(b => new EmailService());
            builder.Register<IDisplayService>(b => new ConsoleDisplayService());
            var container = builder.Build();
            var system = new ApplicationActorSystem();
            system.Register(new AutoFacAkkaDependencyResolver(container));
            var emailSenderActorCoOrdinator = system.ActorSystem.CreateActor<EmailSenderActorCoOrdinator<EmailSenderActor>>();
            while (true)
            {
                var message = Console.ReadLine();
                emailSenderActorCoOrdinator.Tell("hello");
            }
        }
    }
}
