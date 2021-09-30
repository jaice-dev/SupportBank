using System;

namespace SupportBank
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = Databases.AccountList;
            
            Databases.DisplayAccounts();
            /*Databases.Initialise();
            // Databases.PrintCsv();
            Account ewa = new Account(accountName: "Ewa", accountBalance: 10m);
            Account jordan = new Account("jordan",5m);
            Databases.PrintCsv();*/


        }
    }
}