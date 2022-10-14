using Homework.ThirdParty;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.UnitTests
{
    [TestFixture]
    public class FakeAccountRepositoryTests
    {
        List<Account> repositoryslist;
        IAccountRepository _accountRepository;
        [SetUp]
        public void SetUp()
        {
            _accountRepository = new FakeAccountRepository();
            repositoryslist = new List<Account>();
            for (int i = 0; i < 100; i++)
            {
                repositoryslist.Add(new Account(i + 1));
                _accountRepository.Add(repositoryslist.Last());
            }
        }
        [Test]
        public void AddandGetTest()
        {
            //Arrange
            Account accountfortest = new Account(101);
            //Act
            _accountRepository.Add(accountfortest);
            //Assert
            Assert.That(_accountRepository.Get(101) == accountfortest);
        }
        [Test]
        public void ExistTest()
        {
            Assert.That(_accountRepository.Exists(100));
            Assert.That(!_accountRepository.Exists(101));
        }
        [Test]
        public void RemoveTest()
        {
            Assert.That(_accountRepository.Exists(100));

            _accountRepository.Remove(100);

            Assert.That(!_accountRepository.Exists(100));
        }
        [Test]
        public void GetAllTest()
        {
            List<Account> l = _accountRepository.GetAll().ToList();
            bool egyezik = true;
            for (int i = 0; i < l.Count; i++)
            {
                if (l[i] != repositoryslist[i])
                {
                    egyezik = false;
                    break;
                }
            }
            Assert.That(egyezik);
        }
    }
}
