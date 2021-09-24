using System.Collections.Generic;

namespace SupportBank
{
    class Account
    {
        //FIELDS
        public int AccountId { get; set; }
        public string Name { get; set; }
        public List<Transaction> IncomingTransactions;
        public List<Transaction> OutgoingTransactions;

        //CONSTRUCTOR
        public Account(string name, int accountId)
        {
            AccountId = accountId;
            Name = name;
            IncomingTransactions = new List<Transaction>();
            OutgoingTransactions = new List<Transaction>();

        }
    }
}
