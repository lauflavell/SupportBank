using System;
using System.Collections.Generic;

namespace SupportBank
{
    class Account
    {
        //FIELDS

        int AccountId { get; set; }
        string Name { get; set; }
        List<Transaction> IncomingTransactions;
        List<Transaction> OutgoingTransactions;

        //CONSTRUCTORS

        public Account(string name)
        {
            AccountId += 1;
            Name = name;
            List<Transaction> incomingTransactions = new List<Transaction>();
            List<Transaction> outgoingTransactions = new List<Transaction>();
        }

        //PROPERTIES


        //METHODS

    }
}