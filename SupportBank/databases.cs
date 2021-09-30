using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;


namespace SupportBank
{
    internal class Databases
    {
        private static readonly string _csvPath = @"C:\Training\SupportBank\SupportBank\DodgyTransactions2015.csv";
        //C:\Training\SupportBank\SupportBank\DodgyTransactions2015.csv
        // C:\Training\SupportBank\SupportBank\SupportBank\Program.cs

        private static List<string> csvlist = new();

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

    }
}


 