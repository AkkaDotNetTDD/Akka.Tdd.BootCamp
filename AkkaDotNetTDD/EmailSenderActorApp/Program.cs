using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Tdd.AutoFac;
using Akka.Tdd.Core;
using Autofac;
using DisplayServiceLib;
using EmailServiceLib;

namespace EmailSenderActorApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting EmailSenderActor App ...");
            var builder = new ContainerBuilder();
            builder.Register<IEmailService>(b => new EmailService());
            builder.Register<IDisplayService>(b => new ConsoleDisplayService());
            var container = builder.Build();
            var system = new ApplicationActorSystem();
          
            system.RegisterAndCreateActorSystem(new AutoFacAkkaDependencyResolver(container));
           
            while (true)
            {
                var message = Console.ReadLine();
            }


        }
    }
}
