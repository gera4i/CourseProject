using CourseProject.Entities;
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
        public void AddCategory(int IsExpense, string Name) // категория
        {
            string sqlExpression = String.Format("INSERT INTO Categories (IdUser, Category, IsExpense) VALUES ({0}, '{1}', {2})", GroshyModel.shared.user.Id, Name, IsExpense);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
                MessageBox.Show("Добавлено объектов: " + number);
            }
        }
        public void AddAccount(double SumOfAccount, string Name) // аккаунт
        {
            string Summa = Convert.ToString(SumOfAccount).Replace(",", ".");
            string sqlExpression = String.Format("INSERT INTO Accounts (IdUser, Account, SumOfAccount) VALUES ({0}, '{1}', {2})", GroshyModel.shared.user.Id, Name, Summa);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
                MessageBox.Show("Добавлено объектов: " + number);
            }
        }
        public void AddTransactionToDB(Transaction transaction, int id)
        {
            string convertDate;
            if(transaction.Date.Month < 10)
            {
                convertDate = Convert.ToString(transaction.Date.Year) + "-0" + Convert.ToString(transaction.Date.Month) + "-" + Convert.ToString(transaction.Date.Day);
            }
            else
            {
                convertDate = Convert.ToString(transaction.Date.Year) + "-" + Convert.ToString(transaction.Date.Month) + "-" + Convert.ToString(transaction.Date.Day);
            }
            if (transaction.IsExpense)
            {
                transaction.SumOfTransaction = -transaction.SumOfTransaction;
            }
            string Summa = Convert.ToString(transaction.SumOfTransaction).Replace(",", ".");
            string sqlExpression1 = String.Format("INSERT INTO Transactions (IdUser, SumOfTransaction, Category, Account, Date, Discription) VALUES ({0}, {1}, '{2}', '{3}', '{4}', '{5}')", id, Summa, transaction.Category.Name, transaction.Account.Name, convertDate, transaction.Description);
            string sqlExpression2 = String.Format("UPDATE Accounts SET SumOfAccount = '{0}' WHERE Account = '{1}' and IdUser = {2}", Convert.ToString(transaction.Account.SumOfAccount).Replace(",", "."), transaction.Account.Name, 1);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transactionForDB = connection.BeginTransaction();
                SqlCommand command = connection.CreateCommand();
                command.Transaction = transactionForDB;

                try
                {
                    command.CommandText = sqlExpression1;
                    command.ExecuteNonQuery();
                    command.CommandText = sqlExpression2;
                    command.ExecuteNonQuery();

                    transactionForDB.Commit();
                }
                catch (Exception ex)
                {
                    transactionForDB.Rollback();
                    MessageBox.Show(ex.Message);
                }
                int number = command.ExecuteNonQuery();
            }
        }


        public void CategoriesToList()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string readString = String.Format("select * from Categories WHERE IdUser = {0}", GroshyModel.shared.user.Id);
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
                string readString = String.Format("select * from Accounts WHERE IdUser = {0}", GroshyModel.shared.user.Id);
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
       
        
        public void GetUser(string Login, string Password, User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string readString = String.Format("select * from Users where Password = '{0}' and Login = '{1}'", Password, Login);
                using (SqlCommand command = new SqlCommand(readString, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            String tempLogin = reader["Login"].ToString();
                            int tempId = Convert.ToInt32(reader["IdUser"]);
                            String tempDay = reader["DayX"].ToString();
                            user.Id = tempId;
                            user.Login = tempLogin;
                            if(tempDay != "")
                            {
                                user.Day = Convert.ToInt32(tempDay);
                            }
                        }
                    }
                }
            }
        }
        public bool CheckLogin(string Login)
        {
            bool IsLogineExists = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string readString = String.Format("select * from Users where Login = '{0}'", Login);
                using (SqlCommand command = new SqlCommand(readString, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            IsLogineExists = true;
                        }
                    }
                }
            }
            return IsLogineExists;
        }

        public void SetUser(string Login, string Password)
        {
                string sqlExpression = String.Format("INSERT INTO Users (Login, Password) VALUES ('{0}', '{1}')", Login, Password);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    int number = command.ExecuteNonQuery();
                    MessageBox.Show("Добавлено объектов: " + number);
                }
        }
        public void UpdateUser(User user)
        {
            string sqlExpression = String.Format("UPDATE Users SET DayX = {0} WHERE IdUser = {1}", user.Day, user.Id);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }
        public void DeleteCategory(Category category, int Id )
        {
            string sqlExpression = String.Format("DELETE FROM Categories WHERE Category = '{0}' and IdUser = {1}", category.Name, Id);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
                MessageBox.Show("Удалена категория: " + category.Name);
            }
        }
        public void DeleteAccount(Account account, int Id)
        {
            string sqlExpression = String.Format("DELETE FROM Accounts WHERE Account = '{0}' and IdUser = {1}", account.Name, Id);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
                MessageBox.Show("Удален счёт: " + account.Name);
            }
        }

        public void TransactionsToList()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                bool isExpenceFlag = true;
                connection.Open();
                string readString = String.Format("select * from Transactions WHERE IdUser = {0}", GroshyModel.shared.user.Id);
                using (SqlCommand command = new SqlCommand(readString, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        { 
                            Category cat = new Category(null, true);
                            Account acc  = new Account(null, 0);

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
                            GroshyModel.shared.tempTransactionList.Add(transaction);
                        }
                    }
                }
            }
        }
    }
}
