using EmailServiceLib;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AkkaDotNetTDD.Tests
{
    [TestClass]
    public class when_email_service_is_used
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


        [TestMethod]
        public void it_should_not_send_email_with_missing_recipient()
        {
            //Arrange
            IEmailService emailService = new EmailService();
            var emailMessage = new EmailMessage()
            {
                FromEmail = "from@email.com",
                Subject = "subject",
                Body = "body"
            };

            //Act
            var emailSentAck = emailService.SendEmail(emailMessage);

            //Assert
            Assert.IsFalse(emailSentAck);
        }

        [TestMethod]
        public void it_should_not_send_email_with_missing_sender()
        {
            //Arrange
            IEmailService emailService = new EmailService();
            var emailMessage = new EmailMessage()
            {
                ToEmail = "to@email.com",
                Subject = "subject",
                Body = "body"
            };

            //Act
            var emailSentAck = emailService.SendEmail(emailMessage);

            //Assert
            Assert.IsFalse(emailSentAck);
        }

        [TestMethod]
        public void it_should_not_send_email_with_missing_subject()
        {
            //Arrange
            IEmailService emailService = new EmailService();
            var emailMessage = new EmailMessage()
            {
                ToEmail = "to@email.com",
                FromEmail = "from@email.com",
                Body = "body"
            };

            //Act
            var emailSentAck = emailService.SendEmail(emailMessage);

            //Assert
            Assert.IsFalse(emailSentAck);
        }

        [TestMethod]
        public void it_should_not_send_email_with_empty_body()
        {
            //Arrange
            IEmailService emailService = new EmailService();
            var emailMessage = new EmailMessage()
            {
                ToEmail = "to@email.com",
                FromEmail = "from@email.com",
                Subject = "subject"
            };

            //Act
            var emailSentAck = emailService.SendEmail(emailMessage);

            //Assert
            Assert.IsFalse(emailSentAck);
        }

        [TestMethod]
        public void email_service_should_be_called_with_an_object()
        {
            //Arrange
            IEmailService emailService = new EmailService();

            //Act
            var emailSentAck = emailService.SendEmail(null);

            //Assert
            Assert.IsFalse(emailSentAck);
        }
    }
}