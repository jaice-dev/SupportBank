using System;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace SupportBank
{
    class Program
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            var config = new LoggingConfiguration();
            var target = new FileTarget { FileName = @"C:\Work\Logs\SupportBank.log", 
                Layout = @"${longdate} ${level} - ${logger}: ${message}" };
            config.AddTarget("File Logger", target);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));
            LogManager.Configuration = config;
            
            Logger.Debug("Importing File...");
            FileReader.ImportFile();

            Logger.Debug("Creating accounts from file...");
            Database.CreateAccountsFromTransactionList();
            
            Logger.Debug("Adding transactions to accounts...");
            Database.AddTransactionsToAccounts();
            
            //TODO make AddTransaction call automatically
            //TODO get/setters
            //TODO add logging for if files dont exist
            //TODO address error if user lists account that doesn't exist
            //TODO make amounts all to 2 decimal places

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
                    Database.DisplayAccounts();
                }
                else if (userInput.Length >= 5 && userInput.ToLower().Substring(0, 5) == "list ")
                { 
                    string account = userInput.Substring(5); 
                    Database.DisplayUserTransactions(account);
                }
                else
                {
                    Console.WriteLine("Oops! Try again");
                }
            }
            
        }
    }
}