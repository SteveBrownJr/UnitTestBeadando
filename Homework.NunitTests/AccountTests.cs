using Homework.ThirdParty;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.UnitTests
{
    [TestFixture]

    internal class AccountTests
    {
        private Mock<IAction> _mockAction;
        private Mock<IAction> _mockActionFalse;
        [SetUp]
        public void Setup()
        {
            _mockAction = new Mock<IAction>();
            _mockActionFalse = new Mock<IAction>();
            _mockAction.Setup(mockaction => mockaction.Execute()).Returns(true);
            _mockActionFalse.Setup(mockaction => mockaction.Execute()).Returns(false);
        }
        [Test]
        public void InactiveUserExceptionTest()
        {
            //Arrange
            Account acc1 = new Account(1);
            //Assert
            Assert.Throws<InactiveUserException>(() => acc1.TakeAction(_mockAction.Object));
            acc1.Activate();
            Assert.DoesNotThrow(() => acc1.TakeAction(_mockAction.Object));
        }
        [Test]
        public void AccountRegisterTest()
        {
            //Arrange
            Account acc1 = new Account(1);
            //Assert
            Assert.That(acc1.IsRegistered == false);
            acc1.Register();
            Assert.That(acc1.IsRegistered == true);

        }
        [Test]
        public void AccountActivateTest()
        {
            //Arrange
            Account acc1 = new Account(1);
            //Assert
            Assert.That(acc1.IsConfirmed == false);
            acc1.Activate();
            Assert.That(acc1.IsConfirmed == true);
        }
        [Test]
        public void AccountActionPerformTest()
        {
            //Arrange
            Account acc1 = new Account(1);
            //Act
            acc1.Activate();
            //Assert
            acc1.TakeAction(_mockActionFalse.Object);
            Assert.That(acc1.ActionsSuccessfullyPerformed == 0);
            acc1.TakeAction(_mockAction.Object);
            Assert.That(acc1.ActionsSuccessfullyPerformed == 1);
        }
    }
}
