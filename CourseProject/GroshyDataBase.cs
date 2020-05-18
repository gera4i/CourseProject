using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CourseProject
{
    class GroshyDataBase
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["CourseProject.Properties.Settings.GroshyConnectionString"].ConnectionString;
        public void AddCategory() // категория
        {
            string sqlExpression = "INSERT INTO Categories (Category) VALUES ('Экскурсии')";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
                MessageBox.Show("Добавлено объектов: " + number);
            }
        }
        public void AddTransaction(bool flag, double Sum, string Cat, string Acc, DateTime Date, string Discription)
        {
            string convertDate;
            convertDate = Convert.ToString(Date.Year) + "-0" + Convert.ToString(Date.Month) + "-" + Convert.ToString(Date.Day);
            if (flag)
            {
                Sum = -Sum;
            }
            string sqlExpression1 = String.Format("INSERT INTO Transactions (SumOfTransaction, Category, Account, Date, Discription) VALUES ({0}, '{1}', '{2}', '{3}', '{4}')", Sum, Cat, Acc, convertDate, Discription);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression1, connection);
                int number = command.ExecuteNonQuery();
            }
        }
        public void CategoriesToList()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string readString = "select * from Categories";
                using (SqlCommand command = new SqlCommand(readString, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            String tempCategory = reader["Category"].ToString();
                            bool isExpense = Convert.ToBoolean(reader["IsExpense"]);
                            Category category = new Category(tempCategory, isExpense);
                            GroshyModel.shared.categories.Add(category);
                        }
                    }
                }
            }
        }
        public void AccountsToList()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string readString = "select * from Accounts";
                using (SqlCommand command = new SqlCommand(readString, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            String tempAccount = reader["Account"].ToString();
                            Double tempSum = Convert.ToDouble(reader["SumOfAccount"].ToString());
                            Account account = new Account(tempAccount, tempSum);
                            GroshyModel.shared.accounts.Add(account);
                        }
                    }
                }
            }
        }

        public void TransactionsToList()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                bool isExpenceFlag = true;
                connection.Open();
                string readString = "select * from Transactions";
                using (SqlCommand command = new SqlCommand(readString, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        { 
                            Category cat = GroshyModel.shared.categories.ElementAt(0);
                            Account acc  = GroshyModel.shared.accounts.ElementAt(0);

                            Double tempSum = Convert.ToDouble(reader["SumOfTransaction"].ToString());
                            String tempAccount = reader["Account"].ToString();
                            String tempCategory = reader["Category"].ToString();
                            DateTime tempDate = Convert.ToDateTime(reader["Date"].ToString());
                            String tempDiscription = reader["Discription"].ToString();
                            if(tempSum>0)
                            {
                                isExpenceFlag = false;
                            }
                            foreach (var item in GroshyModel.shared.categories)
                            {
                                if(item.Name == tempCategory)
                                {
                                    cat = item;
                                }
                            }
                            foreach (var item in GroshyModel.shared.accounts)
                            {
                                if (item.Name == tempAccount)
                                {
                                    acc = item;
                                }
                            }
                            Transaction transaction = new Transaction(isExpenceFlag, tempSum, acc, cat, tempDate, tempDiscription );
                            GroshyModel.shared.transactions.Add(transaction);
                        }
                    }
                }
            }
        }
    }
}
