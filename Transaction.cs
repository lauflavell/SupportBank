using System;
using System.Collections.Generic;

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

        //METHOD
        public static Transaction FromCsv(string csvLine)
        {
            string[] line = csvLine.Split(',');
            Transaction transaction = new Transaction();
            transaction.Date = Convert.ToDateTime(line[0]);
            transaction.From = line[1];
            transaction.To = line[2];
            transaction.Narrative = line[3];
            transaction.Amount = Convert.ToDecimal(line[4]);
            return transaction;
        }
        // METHOD USING FOR LOOP
        public static void FromCsv(string csvLine, List<Transaction> transactionList)
        {
            string[] line = csvLine.Split(',');
            Transaction transaction = new Transaction();
            transaction.Date = Convert.ToDateTime(line[0]);
            transaction.From = line[1];
            transaction.To = line[2];
            transaction.Narrative = line[3];
            transaction.Amount = Convert.ToDecimal(line[4]);
            transactionList.Add(transaction);
        }
    }
}
