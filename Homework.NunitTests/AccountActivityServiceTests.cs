using Homework.ThirdParty;
using Moq;
using NUnit.Framework;
using System;

namespace Homework.UnitTests
{
    [TestFixture]
    public class AccountActivityServiceTests
    {
        private IAccountActivityService _AccountActivityService;
        private IAccountRepository _FakeAccountRepository;
        private Mock<IAction> _mockAction;
        [SetUp]
        public void Setup()
        {
            _mockAction = new Mock<IAction>();
            _mockAction.Setup(mockaction => mockaction.Execute()).Returns(true);
            _FakeAccountRepository = new FakeAccountRepository();
            _AccountActivityService = new AccountActivityService(_FakeAccountRepository);
        }

        [Test]
        public void AccountNotExistExceptionTest()
        {
            //Arrange
            _FakeAccountRepository.Add(new Account(1));
            //Act
            //Assert
            AccountNotExistsException ex = Assert.Throws<AccountNotExistsException>(()=>_AccountActivityService.GetActivity(2));
            Assert.That(ex.AccountId==2);
        }

        [TestCase(0,0)]
        [TestCase(19,1)]
        [TestCase(20,2)]
        [TestCase(39,2)]
        [TestCase(40,3)]
        public void GetActivityTestCase(int lvl,int exp)
        {
            //Arrange
            Account acc1 = new Account(1);
            acc1.Activate();
            _FakeAccountRepository.Add(acc1);

            //Act
            for (int i = 0; i < lvl; i++)
            {
                _FakeAccountRepository.Get(1).TakeAction(_mockAction.Object);
            }
            //Assert
            Assert.That(_AccountActivityService.GetActivity(1) == (ActivityLevel)exp);
        }

        [Test]
        public void GetAmountForActivityTest() {
            //Arrange
            _FakeAccountRepository.Add(new Account(1));
            _FakeAccountRepository.Add(new Account(2));
            _FakeAccountRepository.Add(new Account(3));
            _FakeAccountRepository.Add(new Account(4));
            _FakeAccountRepository.Add(new Account(5));
            //Act Part 1
            _FakeAccountRepository.Get(1).Activate();
            _FakeAccountRepository.Get(2).Activate();
            _FakeAccountRepository.Get(3).Activate();
            _FakeAccountRepository.Get(4).Activate();
            _FakeAccountRepository.Get(5).Activate();
            //Act Part 2
            _FakeAccountRepository.Get(1).TakeAction(_mockAction.Object);
            _FakeAccountRepository.Get(2).TakeAction(_mockAction.Object);
            _FakeAccountRepository.Get(3).TakeAction(_mockAction.Object);
            _FakeAccountRepository.Get(4).TakeAction(_mockAction.Object);
            _FakeAccountRepository.Get(5).TakeAction(_mockAction.Object);
            //Assert
            Assert.That(_AccountActivityService.GetAmountForActivity(ActivityLevel.Low)==5);
        }
        
        [TestCase(20,5,5,5,5,0,5)]
        [TestCase(20,5,5,5,5,1,5)]
        [TestCase(20,5,5,5,5,2,5)]
        [TestCase(20,5,5,5,5,3,5)]
        [TestCase(15,5,0,5,5,3,5)]
        public void GetAmountForActivityTestCase(int amount,int numberofnons,int numberoflows,int numberofmediums, int numberofhighs,int explvl, int expamount) {
            //Arrange
            Account testaccount;
            for (int i = 0; i < amount; i++)
            {
                testaccount = new Account(i+1);
                testaccount.Activate();
                _FakeAccountRepository.Add(testaccount);
            }
            //Act
            for (int i = 0; i < numberofnons; i++)
            {
                _FakeAccountRepository.Get(i + 1);
            }
            for (int i = numberofnons; i < numberofnons + numberoflows; i++)
            {
                for (int j = 0; j < 19; j++)
                {
                    _FakeAccountRepository.Get(i + 1).TakeAction(_mockAction.Object);
                }
            }
            for (int i = numberofnons + numberoflows; i < numberofnons + numberoflows + numberofmediums; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    _FakeAccountRepository.Get(i + 1).TakeAction(_mockAction.Object);
                }
            }
            for (int i = numberofnons + numberoflows + numberofmediums; i < numberofnons + numberoflows + numberofmediums + numberofhighs; i++)
            {
                for (int j = 0; j < 40; j++)
                {
                    _FakeAccountRepository.Get(i + 1).TakeAction(_mockAction.Object);
                }
            }
            //Assert
            Assert.That(_AccountActivityService.GetAmountForActivity((ActivityLevel)explvl)==expamount);
        }
    }
}
