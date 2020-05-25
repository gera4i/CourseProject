using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    public class Transaction
    {
        public bool IsExpense;
        public double SumOfTransaction { get; set; }
        public Account Account { get; set; }
        public Category Category { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int IdTransaction { get; set; }
        public Transaction(bool IsExpense, double SumOfTransaction, Account Account, Category Category, DateTime Date, string Description, int Id)
        {
            this.IsExpense = IsExpense;
            this.SumOfTransaction = SumOfTransaction;
            this.Account = Account;
            this.Category = Category;
            this.Date = Date;
            this.Description = Description;
            this.IdTransaction = Id;
        }
    }
}
