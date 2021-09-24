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
            //EXTRACT CSV USING LINQ
            List<Transaction> transactionList = File.ReadAllLines("C:\\Training\\SupportBank\\support-bank-resources-master\\Transactions2014.csv")
            .Skip(1)
            .Select(v => Transaction.FromCsv(v))
            .ToList();

            //EXTRACT CSV USING FOR LOOP
            // List<Transaction> transactionList = new List<Transaction>();
            // string path = @"C:\Training\SupportBank\support-bank-resources-master\Transactions2014.csv";
            // string[] lines = File.ReadAllLines(path);
            // foreach(string line in lines.Skip(1))
            // {
            //     Transaction.FromCsv(line, transactionList);
            // }

            List<Account> accountsList = new List<Account>();
            Dictionary<int, string> employeeDict = new Dictionary<int, string>();
            CreateEmployeeDictionary(transactionList, employeeDict);
            CreateAllAccounts(transactionList, accountsList, employeeDict);
            MainMenu(accountsList, employeeDict);

        }

        //METHOD

        public static void CreateEmployeeDictionary(List<Transaction> transactionList, Dictionary<int, string> employeeDict)
        {
            var count = 1;
            foreach (Transaction transaction in transactionList)
            {
                if (!employeeDict.ContainsValue(transaction.From))
                {
                    employeeDict.Add(count, transaction.From);
                    count++;
                }
                else if (!employeeDict.ContainsValue(transaction.To))
                {
                    employeeDict.Add(count, transaction.To);
                    count++;
                }
            }

        }
        public static void CreateAllAccounts(List<Transaction> transactionList, List<Account> accountsList, Dictionary<int, string> employeeDict)
        {

            foreach (KeyValuePair<int, string> employee in employeeDict)
            {
                Account account = new Account(employee.Value, employee.Key);

                foreach (Transaction transaction in transactionList)
                {
                    if (transaction.From == employee.Value)
                    {
                        account.OutgoingTransactions.Add(transaction);
                    }
                    else if (transaction.To == employee.Value)
                    {
                        account.IncomingTransactions.Add(transaction);
                    }
                }

                accountsList.Add(account);
            }
        }

        public static void PrintAllTransactions(List<Account> accountsList)
        {
            foreach (Account account in accountsList)
            {
                decimal totalOwes = 0;
                decimal totalOwed = 0;
                foreach (Transaction transaction in account.OutgoingTransactions)
                {
                    totalOwes += transaction.Amount;
                }
                foreach (Transaction transaction in account.IncomingTransactions)
                {
                    totalOwed += transaction.Amount;
                }

                Console.WriteLine($"\nName: {account.Name}\nMoney Owes: £{totalOwes}\nMoney Owed: £{totalOwed}");
            }
        }

        public static void PrintAccount(List<Account> accountsList, string name)
        {
            foreach (Account account in accountsList)
            {
                if (account.Name == name)
                {
                    Console.WriteLine($"\nName: {account.Name}\r\nIncoming Transactions:");
                    Console.WriteLine("{0,-15} {1,-15} {2,-40} {3,-10}", "Date", "From", "Description", "Amount");
                    foreach (Transaction transaction in account.IncomingTransactions)
                    {
                        Console.WriteLine("{0,-15} {1,-15} {2,-40} {3,-10}",
                        transaction.Date.ToShortDateString(),
                        transaction.From,
                        transaction.Narrative,
                        transaction.Amount);
                    }

                    Console.WriteLine("\nOutgoing Transactions:");
                    Console.WriteLine("{0,-15} {1,-15} {2,-40} {3,-10}", "Date", "To", "Description", "Amount");

                    foreach (Transaction transaction in account.OutgoingTransactions)
                    {
                        Console.WriteLine("{0,-15} {1,-15} {2,-40} {3,-10}",
                        transaction.Date.ToShortDateString(),
                        transaction.To,
                        transaction.Narrative,
                        transaction.Amount);
                    }
                }

            }
        }

        private static bool MainMenu(List<Account> accountsList, Dictionary<int, string> employeeDict)
        {
            Console.Clear();
            Console.WriteLine("Welcome to Support Bank\n");
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) List all accounts");
            Console.WriteLine("2) Select an account");
            Console.WriteLine("3) Exit");
            Console.Write("\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    PrintAllTransactions(accountsList);
                    return true;
                case "2":
                Console.WriteLine("\n");
                    foreach (KeyValuePair<int, string> employee in employeeDict)
                    {
                        Console.WriteLine("{0,3}) {1}", employee.Key, employee.Value);
                    }
                    Console.Write("\nSelect an account number:");
                    PrintAccount(accountsList, employeeDict[Convert.ToInt32(Console.ReadLine())]);
                    return true;
                case "3":
                    return false;
                default:
                    return true;
            }
        }

    }
}
