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
        public User user = new User(null, 0, 30);
        public bool isClose = false;
     
        public void AddCategory(int IsExpense, string Name)
        {
            groshyDataBase.AddCategory(IsExpense, Name);
            bool flag;
            if (IsExpense == 1)
                flag = true;
            else flag = false;
            Category category = new Category(Name, flag);
            categories.Add(category);
        }
        public void AddAccount(double Sum, string Name)
        {
            groshyDataBase.AddAccount(Sum, Name);
            Account account = new Account(Name, Sum);
            accounts.Add(account);
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
        public void UpdateUser()
        {
            groshyDataBase.UpdateUser(user);
        }

        public void DeleteCategory(Category category)
        {
            groshyDataBase.DeleteCategory(category, user.Id);
            categories.Remove(category);
        }
        public void DeleteAccount(Account account)
        {
            groshyDataBase.DeleteAccount(account, user.Id);
            accounts.Remove(account);
        }
        public void DeleteTransaction(Transaction transaction)
        {
            groshyDataBase.DeleteTransaction(transaction);
            transactions.Remove(transaction);
            tempTransactionList.Remove(transaction);
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

        public double CountMoney(string flag)
        {
            double summary = 0;

            if (flag == "")
            {
                foreach (var item in accounts)
                {
                    summary += item.SumOfAccount;
                }
            }
            else
            {
                summary = accounts.Find(item => item.Name == flag).SumOfAccount;
            }

          
            return summary;
        }
        public double MoneyPerMounth()
        {
            int period;
            if (user.Day > DateTime.Today.Day)
            {
                period = user.Day - DateTime.Today.Day + 1;
            }
            else if(user.Day == DateTime.Today.Day)
            {
                period = 1;
            }
            else
            {
                period = 30 - DateTime.Today.Day + user.Day;
            }
            double X = Math.Round(CountMoney("") / period, 2);
            return X;
        }
        public BindingList<Transaction> tempTransactionList = new BindingList<Transaction>();
        public BindingList<Transaction> transactions = new BindingList<Transaction>();
        public List<Account> accounts = new List<Account>();
        public List<Category> categories = new List<Category>();
    }
}
