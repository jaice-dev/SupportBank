using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;


namespace SupportBank
{
    internal class Databases
    {
        private static readonly string _csvPath = @"../../../DodgyTransactions2015.csv";

        public static List<string[]> csvlist = new();
        public static List<Account> AccountList = new();

        public static void Initialise()
        {

            using (TextFieldParser csvReader = new TextFieldParser(_csvPath))
            {
                csvReader.SetDelimiters(new string[] {","});
                csvReader.ReadLine();

                while (!csvReader.EndOfData)
                {
                    string[] fields = csvReader.ReadFields();
                    csvlist.Add(fields);

                }
            }
        }

        public static void PrintCsv()
        {
            foreach (var array in csvlist)
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
            foreach (var line in Databases.csvlist)
            {
                if(!CheckAccountExists(line[1]))
                {
                    Account tempAccount = new(line[1], Convert.ToDecimal(line[4]));
                    AddAccountToList(tempAccount);
                }
            }
            /*
            loop through csv:
                if user_name not in Account:
                    create user
                    update balance
                otherwise:
                    update balance
                    */
            
            

        }
        
        

    }
}


 