using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.NetworkInformation;
using Microsoft.VisualBasic.FileIO;
using NLog;


namespace SupportBank
{
    internal class Databases
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private static readonly string _csvPath = @"../../../DodgyTransactions2015.csv";

        public static readonly List<Transaction> csvTransactionslist = new();
        public static Dictionary<string, List<string[]>> Transactions = new();

        public static void Initialise()
        {

            using (TextFieldParser csvReader = new TextFieldParser(_csvPath))
            {
                csvReader.SetDelimiters(new string[] {","});
                csvReader.ReadLine();

                while (!csvReader.EndOfData)
                {
                    string[] fields = csvReader.ReadFields();
                    
                    Transaction tempTransaction = new Transaction(fields[0], fields[1], fields[2], fields[3], fields[4]);
                    csvTransactionslist.Add(tempTransaction);

                }
            }
        }

        public static void PrintCsv()
        {
            foreach (var array in csvTransactionslist)
            {
                foreach (var item in array)
                {
                    Console.Write($"{item}, ");
                }

                Console.Write("\n");
            }

        }

        public static void AddAccountToList(Account account)
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

        public static bool CheckAccountExists(string name) // returns true if account exists
        {
            bool flag = false;
            foreach (Account account in AccountList)
            {
                if (account.GetName() == name)
                {
                    flag = true;
                }
            }

            return flag;
        }

        public static void CreateAccountsFromCsv()
        {
            foreach (var line in Databases.csvTransactionslist)
            {
                if (!CheckAccountExists(line[1]))
                {
                    Account tempAccount = new(line[1], 0m);
                    AddAccountToList(tempAccount);
                }

                if (!CheckAccountExists(line[2]))
                {
                    Account tempAccount = new(line[2], 0m);
                    AddAccountToList(tempAccount);
                }

                ChangeBalances(line[1], line[2], (line[4]));

            }
        }

        public static void ChangeBalances(string nameLent, string nameOwed, string amount)
        {

            try
            {
                Account foundLent = AccountList.Find(x => x.GetName() == nameLent);
                foundLent.ChangeBalance(-(Convert.ToDecimal(amount)));

                Account foundOwed = AccountList.Find(x => x.GetName() == nameOwed);
                foundOwed.ChangeBalance(Convert.ToDecimal(amount));


            }
            catch (Exception e)
            {
                Logger.Debug(e, amount + "is not a number");
                Console.WriteLine(amount + "is not a number");
            }
        }

        public static void AddTransactionsToDict()
        {
            foreach (var line in csvTransactionslist)
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

            foreach (var transaction in account.newUserTransactions)
            {
                //Console.WriteLine(transaction);
                Console.WriteLine($"Date{Transaction.Date} FromAccount{Transaction.FromAccount} ToAccounts{Transaction.ToAccount} Narrative{Transaction.Narrative} Amount{Transaction.Amount} ");
            }
        }

        public static void AddTransactionsToAccount()
        {
            foreach (var line in csvTransactionslist)
            {
                string fromUser = line.FromAccount;
                string toUser = line.ToAccount;
                
                Account foundAccountFromUser = AccountList.Find(match => match.GetName() == fromUser);
                foundAccountFromUser.newUserTransactions.Add(line);

                Account foundAccountToUser = AccountList.Find(match => match.GetName() == toUser);
                foundAccountToUser.newUserTransactions.Add(line);

            }
        }
    }
}
 
    
    
    



 