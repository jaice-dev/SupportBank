using System;
using System.Data;

namespace SupportBank
{
    class Program
    {
        static void Main(string[] args)
        {

            Databases.Initialise();
            Databases.CreateAccountsFromCsv();
            Databases.AddTransactionsToDict();
            //TODO make AddTransaction call automatically

            Console.WriteLine("Please enter a request (List All / List [Account]: ");
            string UserInput = Console.ReadLine();

            if (UserInput.ToLower() == "list all")
            {
                Databases.DisplayAccounts();
            }

            else if (UserInput.ToLower().Substring(0, 5) == "list ")
            { 
                string account = UserInput.Substring(5); 
                Databases.DisplayUserTransactions(account);
            }




        }
    }
}