using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic.FileIO;
using NLog;

namespace SupportBank
{
    internal static class Databases
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private const string CsvPath = @"../../../DodgyTransactions2015.csv";

        // private static readonly string _csvPath = @"../../../Transactions2014.csv";

        public static readonly List<string[]> CsvList = new();
        private static readonly List<Account> AccountList = new();
        private static readonly Dictionary<string, List<string[]>> Transactions = new();

        public static void InitialiseDatabase()
        {
            using var csvReader = new TextFieldParser(CsvPath);
            csvReader.SetDelimiters(new string[] {","});
            csvReader.ReadLine();

            while (!csvReader.EndOfData)
            {
                string[] fields = csvReader.ReadFields();
                CsvList.Add(fields);
            }
        }

        public static void PrintCsv()
        {
            foreach (var array in CsvList)
            {
                foreach (var item in array)
                {
                    Console.Write($"{item}, ");
                }

                Console.Write("\n");
            }

        }

        private static void AddAccountToList(Account account)
        {
            AccountList.Add(account);
        }

        public static void DisplayAccounts()
        {
            if (AccountList.Count == 0)
            {
                Console.WriteLine("Account list is empty!");
            }
            else
            {
                AccountList.ForEach(i => i.DisplayAccountData());

            }
        }

        private static bool CheckAccountExists(string name) // returns true if account exists
        {
            var flag = false;
            foreach (var account in AccountList.Where(account => account.GetName() == name))
            {
                flag = true;
            }

            return flag;
        }

        public static void CreateAccountsFromCsv()
        {
            foreach (var line in Databases.CsvList)
            {
                string fromUser = line[1];
                string toUser = line[2];
                string amount = line[4];

                
                if (!CheckAccountExists(fromUser))
                {
                    Account tempAccount = new(accountName:fromUser, 0m);
                    AddAccountToList(tempAccount);
                }

                if (!CheckAccountExists(toUser))
                {
                    Account tempAccount = new(accountName:toUser, 0m);
                    AddAccountToList(tempAccount);
                }

                try
                {
                    ChangeBalances(fromUser, toUser, amount);

                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: Cannot convert '{amount}' to type decimal. See log for more details");
                    Logger.Debug(e, $"Error: Cannot convert '{amount}' to type decimal in line " +
                                    $"{Databases.CsvList.IndexOf(line) + 2} of {CsvPath}");
                    //TODO Discuss with Ben what 'failing gracefully' means here. Should we import the remaining transactions from the file? Should we just stop at the line that failed? Could we validate the rest of the file and tell the user up-front where all of the errors are? What would make sense if you were using the software?
                }
            }
        }

        private static void ChangeBalances(string nameLent, string nameOwed, string amount)
        {
            Account foundLent = AccountList.Find(x => x.GetName() == nameLent);
            foundLent.ChangeBalance(-(Convert.ToDecimal(amount)));
            
            Account foundOwed = AccountList.Find(x => x.GetName() == nameOwed);
            foundOwed.ChangeBalance(Convert.ToDecimal(amount));
        }

        public static void AddTransactionsToDict()
        {
            foreach (var line in CsvList)
            {
                string date = line[0];
                string fromUser = line[1];
                string toUser = line[2];
                string narrative = line[3];
                string amount = line[4];

                List<string[]> fromUserList = new List<string[]>();
                List<string[]> toUserList = new List<string[]>();

                string[] fromUserArray = new[] {date, toUser, narrative, "-" + amount};
                string[] toUserArray = new[] {date, fromUser, narrative, amount};


                fromUserList.Add(fromUserArray);
                toUserList.Add(toUserArray);

                if (!Transactions.ContainsKey(fromUser))
                {
                    Transactions.Add(fromUser, fromUserList);
                }
                else
                {
                    Transactions[fromUser].Add(fromUserArray);
                }

                if (!Transactions.ContainsKey(toUser))
                {
                    Transactions.Add(toUser, toUserList);
                }
                else
                {
                    Transactions[toUser].Add(toUserArray);
                }
            }
        }


        // public static void DisplayUserTransactionsFromDict(string username)
        // {
        //     foreach (var transaction in Transactions)
        //     {
        //         if (transaction.Key == username)
        //         {
        //             foreach (var col in transaction.Value)
        //             {
        //                 Console.WriteLine($"Date: {col[0]} Person: {col[1]} Narrative: {col[2]} Amount: {col[3]}");
        //             }
        //         }
        //
        //     }
        // }
        
        public static void DisplayUserTransactions(string username)
        {
            Account account = AccountList.Find(match => match.GetName() == username);

            foreach (var transaction in account.UserTransactions)
            {
                Console.WriteLine(transaction);
            }


        }

        public static void AddTransactionsToAccount()
        {
            foreach (var line in CsvList)
            {
                string date = line[0];
                string fromUser = line[1];
                string toUser = line[2];
                string narrative = line[3];
                string amount = line[4];


                string fromUserString = $"Date: {date}, Person: {toUser}, Narrative: {narrative}, Amount: -{amount}";
                string toUserString = $"Date: {date}, Person: {fromUser}, Narrative: {narrative}, Amount: {amount}";

                Account foundAccountFromUser = AccountList.Find(match => match.GetName() == fromUser);
                foundAccountFromUser.UserTransactions.Add(fromUserString);

                Account foundAccountToUser = AccountList.Find(match => match.GetName() == toUser);
                foundAccountToUser.UserTransactions.Add(toUserString);

            }
        }
    }
}
 
    
    
    



 