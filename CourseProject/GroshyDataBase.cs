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
        public void Insert()
        {
            string sqlExpression = "INSERT INTO Categories (Category) VALUES ('Uuu')";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
                MessageBox.Show("Добавлено объектов: " + number);
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
                            Category category = new Category(tempCategory);
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
    }
}
