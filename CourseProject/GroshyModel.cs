using CourseProject.Entities;
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
        public User user = new User(null, 0);
     
        public void AddCategory(int IsExpense, string Name)
        {
            groshyDataBase.AddCategory(IsExpense, Name);
        }
        public void AddAccount(double Sum, string Name)
        {
            groshyDataBase.AddAccount(Sum, Name);
        }
        public void GetUser(string Login, string Password)
        {
            groshyDataBase.GetUser(Login, Password, user);
        }
        public void SetUser(string Login, string Password)
        {
            groshyDataBase.SetUser(Login, Password);
        }
        public bool CheckLogin(string Login)
        {
            return groshyDataBase.CheckLogin(Login);
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
            tempTransactionList.Add(transaction);
            groshyDataBase.AddTransactionToDB(transaction, user.Id);
        }

        public double CountMoney()
        {
            double summary = 0; // начало пробного примера
            foreach (var item in accounts)
            {
                summary += item.SumOfAccount;
            }
            return summary;
        }
        public BindingList<Transaction> tempTransactionList = new BindingList<Transaction>();
        public BindingList<Transaction> transactions = new BindingList<Transaction>();
        public List<Account> accounts = new List<Account>();
        public List<Category> categories = new List<Category>();

        

    }
}
