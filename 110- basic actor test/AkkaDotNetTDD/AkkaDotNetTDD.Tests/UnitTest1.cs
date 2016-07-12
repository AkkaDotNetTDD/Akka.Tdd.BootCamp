using Akka.Actor;
using Akka.Tdd.AutoFac;
using Akka.Tdd.Core;
using Akka.Tdd.TestKit;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace AkkaDotNetTDD.Tests
{
    [TestClass]
    public class UnitTest1
    {
        //Install-Package Akka.Tdd.AutoFac
        [TestMethod]
        public void TestMethod1()
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
    }

   
}