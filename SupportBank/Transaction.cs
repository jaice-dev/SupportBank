using System;
using System.Globalization;

namespace SupportBank
{
    public class Transaction
    {
        public string Date { get; set; }
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }
        public string Narrative { get; set; }
        public string Amount { get; set; }
        
        //constructors for initialising
        public Transaction(string date, string fromAccount, string toAccount, string narrative, string amount)
        {
            Date = date;
            FromAccount = fromAccount;
            ToAccount = toAccount;
            Narrative = narrative;
            Amount = amount;
        }
        
        public void DisplayTransaction()
        {
            try
            {
                decimal decimalAmount = Math.Round(Convert.ToDecimal(Amount), 2); //shows two decimal places
                Console.WriteLine($"Date: {Date}, From: {FromAccount}, To: {ToAccount}, Narrative: {Narrative}, Amount: {Amount}");
            }
            catch
            {
                Console.WriteLine($"Date: {Date}, From: {FromAccount}, To: {ToAccount}, Narrative: {Narrative}, Amount: {Amount}");
            }
        }
    }
}