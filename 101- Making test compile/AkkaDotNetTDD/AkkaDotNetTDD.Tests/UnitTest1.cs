using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AkkaDotNetTDD.Tests
{
    [TestClass]
    public class UnitTest1
    {
        /*
           Goal: Create a mass email sender
           Tools: Akka.NET & Akka.TDD
           Install-Package Akka
           Install-Package Akka.Tdd
           Install-Package Akka.Tdd.TestKit
           Install-Package FakeItEasy
        */

        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            var emailService = A.Fake<IEmailService>();
            var emailMessage = new EmailMessage()
            {
                ToEmail = "to@email.com",
                FromEmail = "from@email.com",
                Subject = "subject",
                Body = "body"
            };

            //Act
            var emailSentAck = emailService.SendEmail(emailMessage);

            //Assert
            Assert.IsTrue(emailSentAck);
        }
    }
}