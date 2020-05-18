using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CourseProject
{
    public class Category
    {
        public string Name { get; set; }
        public bool IsExpense { get; set; }
        public Category(string Name, bool isExpense)
        {
            this.Name = Name;
            this.IsExpense = isExpense;
        }
    }
}
