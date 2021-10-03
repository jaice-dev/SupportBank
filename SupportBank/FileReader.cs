using System;
using System.Collections.Generic;
using System.IO;
using System.Transactions;
using System.Xml.Serialization;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using NLog;
using NLog.Layouts;

namespace SupportBank
{
    public class FileReader
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private static readonly string _2012Path = @"../../../Transactions2012.xml";
        private static readonly string _2013Path= @"../../../Transactions2013.json";
        private static readonly string _2014Path = @"../../../Transactions2014.csv";
        private static readonly string _2015Path = @"../../../DodgyTransactions2015.csv";
        private static string _fileYear;
        

        public static void ImportFile()
        {
            while (_fileYear is not ("2012" or "2013" or "2014" or "2015"))
            {
                Console.WriteLine("Which year's data would you like to view? (2012/2013/2014/2015) :");
                _fileYear = Console.ReadLine();
            }
            
            Logger.Debug("Initialising Database...");


            switch (_fileYear)
            {
                case "2012":
                    InitialiseXml(_2012Path);
                    break;
                case "2013":
                    InitialiseJson(_2013Path);
                    break;
                case "2014":
                    InitialiseCsv(_2014Path);
                    break;
                case "2015":
                    InitialiseCsv(_2015Path);
                    break;
            }

        }

        private static void InitialiseCsv(string filepath)
        {
            
            using var csvReader = new TextFieldParser(filepath);
            csvReader.SetDelimiters(new string[] {","});
            csvReader.ReadLine();

            while (!csvReader.EndOfData)
            {
                string[] fields = csvReader.ReadFields();
                Transaction tempTransaction = new Transaction(fields[0], fields[1], fields[2], fields[3], fields[4]);
                Database.TransactionList.Add(tempTransaction);
            }
        }

        private static void InitialiseJson(string filepath)
        {
            string jsonString = File.ReadAllText(filepath);
            var converted = JsonConvert.DeserializeObject<List<Transaction>>(jsonString);
            foreach (var transaction in converted)
            {
                Database.TransactionList.Add(transaction);
            }

        }

        private static void InitialiseXml(string filepath)
        {
            using (var filestream = File.Open(filepath, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SupportTransactionCollection));
                var TransactionList = (SupportTransactionCollection)serializer.Deserialize(filestream);
                foreach (var transaction in TransactionList.TransactionList)
                {
                    string description = transaction.Description;
                    string date = transaction.Date;
                    string value = transaction.Value;
                    string from = transaction.PartiesArray.From;
                    string to = transaction.PartiesArray.To;
                    Transaction tempTransaction = new Transaction(date, from, to, description, value);
                    Database.TransactionList.Add(tempTransaction);
                }
            }
        }
    }
}