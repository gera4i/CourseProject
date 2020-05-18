using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Transaction transaction = new Transaction(true, 0, null, null, DateTime.Now, "");
        private bool isTransactionExpense = true;
        public MainWindow()
        {
            GroshyModel.shared.LoadData();
            InitializeComponent();
            Style = (Style)FindResource(typeof(Window));
            foreach (var item in GroshyModel.shared.categories)
            {
                GroshyComboBoxCategory.Items.Add(item.Name);
            }
            foreach (var item in GroshyModel.shared.accounts)
            {
                GroshyComboBoxAccount.Items.Add(item.Name);
            }
            GroshyDatePicker.Text = Convert.ToString(DateTime.Today);
            double summary = 0; // начало пробного примера
            foreach (var item in GroshyModel.shared.accounts)
            {
                summary += item.SumOfAccount;
            }
            GroshySumOfAccounts.Content = Convert.ToString(summary); // кнец пробный пример

            GroshyDataGrid.ItemsSource = GroshyModel.shared.transactions;

        }




        private void GroshyExpense_Click(object sender, RoutedEventArgs e)
        {
            isTransactionExpense = true;
            (sender as Button).Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#1683e0");
            GroshyIncome.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#0F559A");
       
        }

        private void GroshyIncome_Click(object sender, RoutedEventArgs e)
        {
            isTransactionExpense = false;
            (sender as Button).Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#1683e0");
            GroshyExpense.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#0F559A");
      
        }
       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            transaction.IsExpense = isTransactionExpense;
            transaction.Account = GroshyModel.shared.accounts.ElementAt(GroshyComboBoxAccount.SelectedIndex);
            transaction.Category = GroshyModel.shared.categories.ElementAt(GroshyComboBoxCategory.SelectedIndex);
            transaction.Date = (DateTime)GroshyDatePicker.SelectedDate;
            transaction.Description = GroshyDescritptionBox.Text;
            transaction.SumOfTransaction = Convert.ToInt32(GroshySumBox.Text);
            GroshyModel.shared.AddTransaction(transaction);
            GroshyDataGrid.ItemsSource = GroshyModel.shared.transactions;
            GroshySumOfAccounts.Content = Convert.ToString(GroshyModel.shared.accounts.ElementAt(0).SumOfAccount);
            MessageBox.Show(Convert.ToString(GroshyModel.shared.transactions.ElementAt(0).Description));
            transaction = new Transaction(true, 12, null, null, DateTime.Now, "ghbdtn");
            GroshyModel.shared.AddTransaction(isTransactionExpense, Convert.ToDouble(GroshySumBox.Text), GroshyComboBoxCategory.Text, GroshyComboBoxAccount.Text, Convert.ToDateTime(GroshyDatePicker.SelectedDate), GroshyDescritptionBox.Text);
        }
       
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            foreach (var item in GroshyModel.shared.accounts)
            {
                int i = 0;
                TabItem newTabItem = new TabItem
                {
                    Header = item.Name,
                    Name = "a" + i
                    };
                    GroshyTabControl.Items.Add(newTabItem);
                i++;
            }
        }

        // ДОБАВЛЕНИЕ КНОПКОЙ НОВОЙ ВКЛАДКИ 
        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{

        //    TabItem newTabItem = new TabItem
        //    {
        //        Header = "Test",
        //        Name = "Test"
        //    };
        //    GroshyTabControl.Items.Add(newTabItem);
        //}
    }
}
