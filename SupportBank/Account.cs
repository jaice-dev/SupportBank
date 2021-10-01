using System;

namespace SupportBank
{
    public class Account
    {
        private readonly string _name;
        private decimal _balance;
        
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