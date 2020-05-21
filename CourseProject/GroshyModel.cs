using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CourseProject
{
    class GroshyModel
    {
        private GroshyModel()
        {

        }
        GroshyDataBase groshyDataBase = new GroshyDataBase();

        public void AddTransaction(bool flag, double Sum, string Cat, string Acc, DateTime Date, string Discription)
        {
            groshyDataBase.AddTransaction( flag,  Sum,  Cat,  Acc,  Date,  Discription);
        }
        public void AddCategory(int IsExpense, string Name)
        {
            groshyDataBase.AddCategory(IsExpense, Name);
        }
        public void AddAccount(double Sum, string Name)
        {
            groshyDataBase.AddAccount(Sum, Name);
        }
        public void LoadData()
        {
            groshyDataBase.CategoriesToList();
            groshyDataBase.AccountsToList();
            groshyDataBase.TransactionsToList();

        }
       
        public static GroshyModel shared = new GroshyModel();

        public void AddTransaction(Transaction transaction)
        {
            if (transaction.IsExpense)
            {
                transaction.Account.SumOfAccount -= transaction.SumOfTransaction;
            }
            else
            {
                transaction.Account.SumOfAccount += transaction.SumOfTransaction;
            }
            transactions.Add(transaction);
        }
        public BindingList<Transaction> transactions = new BindingList<Transaction>();
        public List<Account> accounts = new List<Account>();
        public List<Category> categories = new List<Category>();

        

    }
}
