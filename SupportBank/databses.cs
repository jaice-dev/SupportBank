using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;


namespace SupportBank.db
{
    class Databases
    {
        var csvPath = @"C:\Training\SupportBank\SupportBank\DodgyTransactions2015.csv";
        
        List<string> csvlist = new(); 

        public static void Initialise(){

            using(TextFieldParser csvReader = new TextFieldParser(csvPath))
            {
                csvReader.SetDelimiters(new string[] { "," });

                while (!csvReader.EndOfData){
                    string fields = csvReader.ReadFields();
                    csvlist.Add(fields);
                    
            }
        
        public void PrintCsv()
            
            foreach (i in csvlist)
            {
                Console.WriteLine(i);
            }
                
        }
    }
}