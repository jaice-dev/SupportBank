using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;


namespace SupportBank
{
    internal class Databases
    {
        private static readonly string _csvPath = @"../../../DodgyTransactions2015.csv";

        private static List<string> csvlist = new();
        public static List<Account> AccountList = new();

        public static void Initialise()
        {

            using (TextFieldParser csvReader = new TextFieldParser(_csvPath))
            {
                csvReader.SetDelimiters(new string[] {","});

                while (!csvReader.EndOfData)
                {
                    string fields = csvReader.ReadLine();
                    csvlist.Add(fields);

                }
            }
        }

        public static void PrintCsv()
        {
            foreach (var i in csvlist)
            {
                Console.WriteLine(i);
            }

        }

        public void AddAccountToList(Account account)
        {
            AccountList.Add(account);
        }

        public static void DisplayAccounts()
        {
            this.AddAccountToList();
            AccountList.ForEach(i => i.DisplayAccountData());
        }

    }
}


 