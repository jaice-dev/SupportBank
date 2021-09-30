using System;
using SupportBank.db;

namespace SupportBank
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Databases.Initialise();
            Databases.PrintCsv();
            
        }
    }
}