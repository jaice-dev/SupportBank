using System;
using System.Data;

namespace SupportBank
{
    class Program
    {
        static void Main(string[] args)
        {

            Databases.Initialise();
            // Databases.PrintCsv();
            /*Account ewa = new Account(accountName: "Ewa", accountBalance: 10.00m);
            Account jordan = new Account("jordan",5.00m);
            Databases.AddAccountToList(ewa);
            Databases.AddAccountToList(jordan);*/

            Databases.DisplayAccounts();
            Databases.CreateAccountsFromCsv();
            Databases.DisplayAccounts();




        }
    }
}