using System;
using ActorsLib;
using Akka.Actor;
using Akka.Tdd.AutoFac;
using Akka.Tdd.Core;
using Akka.Tdd.TestKit;
using Autofac;
using EmailServiceLib;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AkkaDotNetTDD.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void using_emailSenderCoordinatorActor()
        {
            //Arrange
            var builder = new ContainerBuilder();
            var emailService = A.Fake<IEmailService>();
            var emailMessage = new EmailMessage()
            {
                ToEmail = "to@email.com",
                FromEmail = "from@email.com",
                Subject = "subject",
                Body = "body"
            };
            A.CallTo(() => emailService.SendEmail(emailMessage)).Returns(true);
            builder.Register(b => emailService);
            var container = builder.Build();
            var system = new ApplicationActorSystem();
            system.Register(new AutoFacAkkaDependencyResolver(container));
            var factory = new TddTestKitfactoryFactory(container, system.ActorSystem);

            factory.WhenActorStarts()
            .ItShouldDo((actor) =>
            {
                actor.ActorChildrenOrDependencies.Item1.ActorRef = actor.Context.CreateActor<EmailSenderActor>();
            })
            .WhenActorReceives<string>()
            .ItShouldDo((actor) =>
            {
                actor.ActorChildrenOrDependencies.Item1.ActorRef.Tell("hello");
            })
            .SetUpMockActor<MockActor<EmailSenderActor>>();

            //Act
           var emailSenderCorordinatorActor= system.ActorSystem.CreateActor<MockActor<EmailSenderActor>>();
            emailSenderCorordinatorActor.Tell("hello");

            //Assert
            factory.AwaitAssert(() =>
            {
                A.CallTo(() => emailService.SendEmail(emailMessage)).WithAnyArguments().MustHaveHappened();
                return true;
            }, 3000);
        }
    }
}
