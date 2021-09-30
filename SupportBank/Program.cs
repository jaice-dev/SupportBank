using System;

namespace SupportBank
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Databases.Initialise();
            Databases.PrintCsv();
            Account ewa = new Account(accountName: "Ewa", accountBalance: 10m);
            ewa.DisplayAccountData();

        }
    }
}