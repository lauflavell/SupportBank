
using System.Collections.Generic;

namespace SupportBank
{
    class Account
    {
        //FIELDS

        public int AccountId;
        public string Name;
        public List<Transaction> IncomingTransactions;
        public List<Transaction> OutgoingTransactions;


        //CONSTRUCTORS

        public Account(string name, int accountId)
        {
            AccountId = accountId;
            Name = name;
            IncomingTransactions = new List<Transaction>();
            OutgoingTransactions = new List<Transaction>();

        }

        //PROPERTIES

    }

    //METHODS

}
