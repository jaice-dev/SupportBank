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
            Databases.CreateAccountsFromCsv();
            
            Logger.Debug("Adding transactions to accounts...");
            Databases.AddTransactionsToAccount();
            //Databases.AddTransactionsToDict();
            
            //TODO make AddTransaction call automatically
            //TODO get/setters
            //TODO add logging for if files dont exist
            

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
                    //Databases.DisplayUserTransactionsFromDict(account);
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