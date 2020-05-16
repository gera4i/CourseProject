using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    public class Account
    {
        public string Name { get; set; }
        public double SumOfAccount { get; set; }
        public Account(string Name, double Sum)
        {
            this.Name = Name;
            this.SumOfAccount = Sum;
        }
    }
}
