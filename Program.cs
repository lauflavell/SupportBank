using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace SupportBank
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Transaction> transactionList = File.ReadAllLines("C:\\Training\\SupportBank\\support-bank-resources-master\\Transactions2014.csv")
            .Skip(1)
            .Select(v => Transaction.FromCsv(v))
            .ToList();

            List<Account> accountsList = new List<Account>();
            CreateAllAccounts(accountsList, transactionList);

            // foreach(Transaction transaction in transactionList)
            // {
            //     Console.WriteLine(transaction.Date.ToString("MM-dd-yyyy") + "\t" + transaction.To + "\t" + transaction.From + "\t" + transaction.Narrative + "\t" + transaction.Amount);
            // }
        }

        //METHOD

        public static void CreateAllAccounts(List<Transaction> transactionList, List<Account> accountsList)
        {
            List<string> employeeList = new List<string>();
            foreach (Transaction transaction in transactionList)
            {
                if (!employeeList.Exists(e => e == transaction.From))
                {
                    employeeList.Add(transaction.From);
                }
                if (!employeeList.Exists(e => e == transaction.To))
                {
                    employeeList.Add(transaction.To);
                }

            }
            employeeList.ForEach(Console.WriteLine);
            Console.WriteLine(employeeList.Count);

            foreach (string employee in employeeList)
            {
                var something = new Account(employee);

                foreach (Transaction transaction in transactionList)
                {
                    accountsList.Add(CreateAccount(employee, transactionList));
                }
            }

        }

        public static void CreateAccount()
        {

        }
    }
}
