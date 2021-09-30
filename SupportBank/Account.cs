using System;

namespace SupportBank
{
    public class Account
    {
        public string name;
        public decimal balance;
        
        //constuctors for initialising
        public Account(string accountName, decimal accountBalance)
        {
            name = accountName;
            balance = accountBalance;
        }
        
        public void DisplayAccountData()
        {
            Console.WriteLine($"Name: {name} Balance: {balance}");
        }

    }
}