using Homework.ThirdParty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.UnitTests
{
    internal class FakeAccountRepository : IAccountRepository
    {
        private readonly List<Account> _accounts = new List<Account>();
        public bool Add(Account account)
        {
            _accounts.Add(account);
            return true;
        }

        public bool Exists(int accountId)
        {
            return _accounts.Any(acc => acc.Id == accountId);
        }

        public Account Get(int accountId)
        {
            return _accounts.FirstOrDefault(acc => acc.Id == accountId);
        }

        public IEnumerable<Account> GetAll()
        {
            return _accounts;
        }

        public bool Remove(int accountId)
        {
            try
            {
                _accounts.Remove(Get(accountId));
            }
            catch (Exception)
            {
            }
            return !Exists(accountId);
        }
    }
}
