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
            CreateAllAccounts(transactionList, accountsList);
            MainMenu(accountsList);

            // Console.WriteLine("\nWelcome to SupportBank\nPress 0 to List All acouunts\nPress 1 to select account");
            // string input = Console.ReadLine();
            // if (input == "0")
            // {
            //     PrintAllTransactions(accountsList);
            // }
            // else if (input == "1")
            // {
            //     Console.WriteLine("Please enter the account name you wish to print:");
            //     string nameInput = Console.ReadLine();
            //     PrintAccount(accountsList, nameInput);
            // }
            // else Console.WriteLine("Please enter valid number");




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
                else if (!employeeList.Exists(e => e == transaction.To))
                {
                    employeeList.Add(transaction.To);
                }

            }

            foreach (string employee in employeeList)
            {
                Account account = new Account(employee);

                foreach (Transaction transaction in transactionList)
                {
                    if (transaction.From == employee)
                    {
                        account.OutgoingTransactions.Add(transaction);
                    }
                    else if (transaction.To == employee)
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
                    Console.WriteLine($"Name: {account.Name}\nIncoming Transactions:");
                    Console.WriteLine("{0,-10} {1,-10} {2,-20} {3,-10}", "Date", "From", "Description", "Amount");
                    foreach (Transaction transaction in account.IncomingTransactions)
                    {
                        Console.WriteLine("{0,-10} {1,-10} {2,-20} {3,-10}",
                        transaction.Date.ToShortDateString(),
                        transaction.From,
                        transaction.Narrative,
                        transaction.Amount);
                    }

                    Console.WriteLine("\nOutgoing Transactions:");
                    Console.WriteLine("{0,-10} {1,-10} {2,-20} {3,-10}", "Date", "To", "Description", "Amount");

                    foreach (Transaction transaction in account.OutgoingTransactions)
                    {
                        Console.WriteLine("{0,-10} {1,-10} {2,-20} {3,-10}",
                        transaction.Date.ToShortDateString(),
                        transaction.To,
                        transaction.Narrative,
                        transaction.Amount);
                    }
                }

            }
        }

        private static bool MainMenu(List<Account> accountsList)
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
                    Console.WriteLine("Please enter the account name you wish to print:");
                    string nameInput = Console.ReadLine();
                    PrintAccount(accountsList, nameInput);
                    return true;
                case "3":
                    return false;
                default:
                    return true;
            }
        }

    }
}
