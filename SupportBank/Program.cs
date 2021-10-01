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
            
            bool programRunning = true;
            while (programRunning)
            {
                Console.WriteLine("Please enter a request (List All / List [Account]: ");
                string userInput = Console.ReadLine();


                if (userInput.ToLower() == "quit" || userInput.ToLower() == "q")
                {
                    programRunning = false;
                }
                else if (userInput.ToLower() == "list all")
                {
                    Databases.DisplayAccounts();
                }
                else if (userInput.Length >= 5 && userInput.ToLower().Substring(0, 5) == "list ")
                { 
                    string account = userInput.Substring(5); 
                    Databases.DisplayUserTransactions(account);
                }
                else
                {
                    Console.WriteLine("Oops! Try again");
                }
            }
            




        }
    }
}