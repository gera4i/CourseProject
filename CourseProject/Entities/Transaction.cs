﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    public class Transaction
    {
        public bool IsExpense = true;
        public float SumOfTransaction { get; set; }
        public Account Account { get; set; }
        public Category Category { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public Transaction(bool IsExpense, float SumOfTransaction, Account Account, Category Category, DateTime Date, string Description)
        {
            this.IsExpense = IsExpense;
            this.SumOfTransaction = SumOfTransaction;
            this.Account = Account;
            this.Category = Category;
            this.Date = Date;
            this.Description = Description;
        }
    }
}
