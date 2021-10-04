using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic.FileIO;
using NLog;

namespace SupportBank
{
    internal static class Database
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private const string CsvPath = @"../../../DodgyTransactions2015.csv";

        // private static readonly string _csvPath = @"../../../Transactions2014.csv";

        public static readonly List<Transaction> TransactionList = new();
        private static readonly List<Account> AccountList = new();
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

        public static void CreateAccountsFromTransactionList()
        {
            foreach (var transaction in Database.TransactionList)
            {
                string fromUser = transaction.FromAccount;
                string toUser = transaction.ToAccount;
                string amount = transaction.Amount;

                
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
                    Console.WriteLine($"Non fatal Error: Cannot convert '{amount}' to type decimal. See log for more details");
                    Logger.Debug(e, $"Error: Cannot convert '{amount}' to type decimal in line " +
                                    $"{Database.TransactionList.IndexOf(transaction) + 2} of {CsvPath}");
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

        public static void AddTransactionsToAccounts()
        {
            foreach (var transaction in TransactionList)
            {
                string fromUser = transaction.FromAccount;
                string toUser = transaction.ToAccount;
                
                Account foundAccountFromUser = AccountList.Find(match => match.GetName() == fromUser);
                foundAccountFromUser.UserTransactions.Add(transaction);

                Account foundAccountToUser = AccountList.Find(match => match.GetName() == toUser);
                foundAccountToUser.UserTransactions.Add(transaction);

            }
        }
        public static void DisplayUserTransactions(string username)
        {
            Account account = AccountList.Find(match => match.GetName() == username);
            Console.WriteLine("Note: Positive amount means inputted account is owed money.");
            foreach (var transaction in account.UserTransactions)
            {
                // try
                // {
                //     decimal amount = Math.Round(Convert.ToDecimal(transaction.Amount), 2); //Round to 2dp
                // }
                // catch
                // {
                //     string amount = transaction.Amount;
                // }
                //
                if (transaction.FromAccount == username)
                {
                    Console.WriteLine($"Date: {transaction.Date}, Person: {transaction.ToAccount}, Narrative: {transaction.Narrative}, Amount: -{transaction.Amount}");
                }
                else if (transaction.ToAccount == username)
                {
                    Console.WriteLine($"Date: {transaction.Date}, Person: {transaction.FromAccount}, Narrative: {transaction.Narrative}, Amount: {transaction.Amount}");

                }
            }
        }
    }
}
 
    
    
    



 