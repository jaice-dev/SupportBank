using System;
using System.Security.Cryptography;
using Microsoft.VisualBasic.FileIO;
using NLog;

namespace SupportBank
{
    public class FileReader
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private static readonly string _2013Path= @"../../../Transactions2013.json";
        private static readonly string _2014Path = @"../../../Transactions2014.csv";
        private static readonly string _2015Path = @"../../../DodgyTransactions2015.csv";
        private static string _fileYear;
        

        public static void ImportFile()
        {
            while (!((_fileYear == "2013") || (_fileYear == "2014") || (_fileYear == "2015")))
            {
                Console.WriteLine("Which year's data would you like to view? (2013/2014/2015) :");
                _fileYear = Console.ReadLine();
            }
            
            Logger.Debug("Initialising Database...");


            if (_fileYear == "2013")
            {
                
            }
            else if (_fileYear == "2014")
                InitialiseCsv(_2014Path);
            else if (_fileYear == "2015")
                InitialiseCsv(_2015Path);

        }

        public static void InitialiseCsv(string filepath)
        {
            
            using var csvReader = new TextFieldParser(filepath);
            csvReader.SetDelimiters(new string[] {","});
            csvReader.ReadLine();

            while (!csvReader.EndOfData)
            {
                string[] fields = csvReader.ReadFields();
                Databases.CsvList.Add(fields);
            }
        }
    }
}