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
