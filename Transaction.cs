using System;

namespace SupportBank
{
    class Transaction
    {
        //FIELDS

        public DateTime Date;
        public string From;
        public string To;
        public string Narrative;
        public decimal Amount;
        public int TotalOwed;
        public int TotalOwes;


        //CONSTRUCTOR

        public static Transaction FromCsv(string csvLine)
        {
            string[] transactionList = csvLine.Split(',');
            Transaction transaction = new Transaction();
            transaction.Date = Convert.ToDateTime(transactionList[0]);
            transaction.From = transactionList[1];
            transaction.To = transactionList[2];
            transaction.Narrative = transactionList[3];
            transaction.Amount = Convert.ToDecimal(transactionList[4]);
            return transaction;
        }

        //METHOD



    }
}