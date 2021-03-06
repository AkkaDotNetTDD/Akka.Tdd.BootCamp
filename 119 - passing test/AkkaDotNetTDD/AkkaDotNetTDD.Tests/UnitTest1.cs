﻿using System;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AkkaDotNetTDD.Tests
{
    [TestClass]
    public class when_display_service_is_used
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            var displayService = A.Fake<IDisplayService>();
            var displayMessage = "hello";
            A.CallTo(() => displayService.SendDisplayMessage(displayMessage)).Returns(true);

            //Act
            var displaySuccessfullAck=   displayService.SendDisplayMessage(displayMessage);

            //Assert
            Assert.IsTrue(displaySuccessfullAck);
        }
    }

    public interface IDisplayService
    {
        bool SendDisplayMessage(string displayMessage);
    }
}
