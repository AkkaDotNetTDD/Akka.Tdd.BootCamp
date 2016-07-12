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
        public void an_email_service_api()
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
            A.CallTo(() => emailService.SendEmail(emailMessage)).Returns(true);

            //Act
            var emailSentAck = emailService.SendEmail(emailMessage);

            //Assert
            Assert.IsTrue(emailSentAck);
        }

        [TestMethod]
        public void it_should_send_out_email()
        {
            //Arrange
            IEmailService emailService = new EmailService();
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