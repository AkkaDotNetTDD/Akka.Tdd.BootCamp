using Akka.Actor;
using Akka.Tdd.AutoFac;
using Akka.Tdd.Core;
using Akka.Tdd.TestKit;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using ActorsLib;
using DisplayServiceLib;
using EmailServiceLib;
using FakeItEasy;

namespace AkkaDotNetTDD.Tests
{
    [TestClass]
    public class when_send_email_actor_is_used
    {
        //Install-Package Akka.Tdd.AutoFac
        [TestMethod]
        public void basic_actor_test()
        {
            //Arrange
            var container = new ContainerBuilder().Build();
            var system = new ApplicationActorSystem();
            system.Register(new AutoFacAkkaDependencyResolver(container));
            var factory = new TddTestKitfactoryFactory(container, system.ActorSystem);

            var messageReceived = false;
            var emailSenderActor = factory.WhenActorReceives<string>().ItShouldDo(actor =>
            {
                messageReceived = true;
            }).CreateMockActorRef<MockActor>();

            //Act
            emailSenderActor.Tell("hello");

            //Assert
            factory.AwaitAssert(() =>
            {
                Assert.IsTrue(messageReceived);
                return true;
            },3000);
        }
        [TestMethod]
        public void introducing_email_service()
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

            
            var emailSenderActor = factory.WhenActorReceives<string>().ItShouldDo(actor =>
            {
              var actorEmailService=  container.Resolve<IEmailService>();
                actorEmailService.SendEmail(emailMessage);
            }).CreateMockActorRef<MockActor>();

            //Act
            emailSenderActor.Tell("hello");

            //Assert
            factory.AwaitAssert(() =>
            {
                A.CallTo(() => emailService.SendEmail(emailMessage)).MustHaveHappened();
                return true;
            }, 3000);
        }


        [TestMethod]
        public void creating_concrete_actor()
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

            //*****TEST FIXES
            var displayService = A.Fake<IDisplayService>();
            A.CallTo(() => displayService.SendDisplayMessage(null)).Returns(true);
            builder.Register(b => displayService);
            //*****TEST FIXES - END

            builder.Register(b => emailService);
            var container = builder.Build();
            var system = new ApplicationActorSystem();
            system.Register(new AutoFacAkkaDependencyResolver(container));
            var factory = new TddTestKitfactoryFactory(container, system.ActorSystem);

            //Act
            var emailSenderActor = system.ActorSystem.CreateActor<EmailSenderActor>();
            emailSenderActor.Tell("hello");

            //Assert
            factory.AwaitAssert(() =>
            {
                A.CallTo(() => emailService.SendEmail(emailMessage)).WithAnyArguments().MustHaveHappened();
                return true;
            }, 3000);
        }

       
    }
}