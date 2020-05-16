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
        GroshyDataBase groshyDataBase = new GroshyDataBase();
        Transaction transaction = new Transaction(true, 0, null, null, DateTime.Now, "");
        private bool isTransactionIncome = false;
        public MainWindow()
        {
            groshyDataBase.CategoriesToList();
            groshyDataBase.AccountsToList();
            InitializeComponent();
            foreach (var item in GroshyModel.shared.categories)
            {
                ComboBoxCategory.Items.Add(item.Name);
            }
            foreach (var item in GroshyModel.shared.accounts)
            {
                ComboBoxAccount.Items.Add(item.Name);
            }
            GroshyDatePicker.Text = Convert.ToString(DateTime.Today);
            double summary = 0;
            foreach (var item in GroshyModel.shared.accounts)
            {
                summary += item.SumOfAccount;
            }
            GroshySumOfAccounts.Content = Convert.ToString(summary);
        }




        private void GroshyCost_Click(object sender, RoutedEventArgs e)
        {
            isTransactionIncome = false;
            (sender as Button).Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#1683e0");
            GroshyIncome.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#0F559A");
            GroshyCost.FontSize = 18;
            GroshyIncome.FontSize = 12;
        }

        private void GroshyIncome_Click(object sender, RoutedEventArgs e)
        {
            isTransactionIncome = true;
            (sender as Button).Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#1683e0");
            GroshyCost.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#0F559A");
            GroshyIncome.FontSize = 18;
            GroshyCost.FontSize = 12;
        }
       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            transaction.IsExpense = !(isTransactionIncome);
            transaction.Account = GroshyModel.shared.accounts.ElementAt(ComboBoxAccount.SelectedIndex);
            transaction.Category = GroshyModel.shared.categories.ElementAt(ComboBoxCategory.SelectedIndex);
            transaction.Date = (DateTime)GroshyDatePicker.SelectedDate;
            transaction.Description = GroshyDescritptionBox.Text;
            transaction.SumOfTransaction = Convert.ToInt32(GroshySumBox.Text);
            GroshyModel.shared.AddTransaction(transaction);
            GroshyDataGrid.ItemsSource = GroshyModel.shared.transactions;
            GroshySumOfAccounts.Content = Convert.ToString(GroshyModel.shared.accounts.ElementAt(0).SumOfAccount);
            MessageBox.Show(Convert.ToString(GroshyModel.shared.transactions.ElementAt(0).Description));
            transaction = new Transaction(true, 12, null, null, DateTime.Now, "ghbdtn");
        }
       
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            groshyDataBase.Insert();
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
