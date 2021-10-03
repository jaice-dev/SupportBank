using System;

namespace SupportBank
{
    public class Transaction
    {
        public string Date { get; set; }
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }
        public string Narrative { get; set; }
        public decimal Amount { get; set; }

        public void DisplayTransaction()
        {
            Console.WriteLine($"Date: {Date}, From: {FromAccount}, To: {ToAccount}, Narrative: {Narrative}, Amount: {Amount}");
        }
    }
}