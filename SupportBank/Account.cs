using System;
using System.Collections.Generic;

namespace SupportBank
{
    public class Account
    {
        private readonly string _name;
        private decimal _balance;
        public List<string> userTransactions = new();
        
        
        //TODO Refactor to make transaction a List belonging to account object
        
        //constuctors for initialising
        public Account(string accountName, decimal accountBalance)
        {
            _name = accountName;
            _balance = accountBalance;
        }
        
        public void DisplayAccountData()
        {
            Console.WriteLine($"Name: {_name}, Balance: {_balance}");
        }

        public void ChangeBalance(decimal amount)
        {
            _balance += amount;
        }

        public string GetName()
        {
            return _name;
        }
        
        
    

    }
}
      