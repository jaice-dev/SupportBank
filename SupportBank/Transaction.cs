namespace SupportBank
{
    public class Transaction
    {
        
        // TODO ask about getting and setters and how to call them
        public string Date { get; set;}
        public string FromAccount { get; set;}
        public string ToAccount { get; set;}
        public string Narrative { get; set;}
        public string Amount { get; set;}

        public Transaction(string date, string fromAccount, string toAccount, string narrative, string amount)
        {
            Date = date;
            FromAccount = fromAccount;
            ToAccount = toAccount;
            Narrative = narrative;
            Amount = amount;
        }
    }
    
        
}