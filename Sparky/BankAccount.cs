using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    public class BankAccount
    {
        public int Balance { get; set; }
        private readonly ILogBook _logbook;

        public BankAccount(ILogBook logBook)
        {
            _logbook = logBook; 
            Balance = 0;
        }

        public bool Deposit(int amount)
        {
            _logbook.Message("Deposit Invoked");
            _logbook.Message("Test");
            _logbook.LogSeverity = 101;
            var temp = _logbook.LogSeverity;
            Balance += amount;
            return true;
        }

        public bool Withdraw(int amount)
        {
            if(amount <= Balance)
            {
                _logbook.LogToDb("Withdrawal Amount: " + amount.ToString());
                Balance -= amount;

                return _logbook.LogBalanceAfterWithdrawal(Balance);
            }
            return _logbook.LogBalanceAfterWithdrawal(Balance-amount);
        }

        public int GetBalance()
        {
            return Balance;
        }
    }
}
