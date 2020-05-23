using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
                if (item.IsExpense)
                    GroshyComboBoxCategory.Items.Add(item.Name);
            }
            foreach (var item in GroshyModel.shared.accounts)
            {
                GroshyComboBoxAccount.Items.Add(item.Name);
            }

            GroshyDatePicker.Text = Convert.ToString(DateTime.Today);

            GroshySumOfAccounts.Content = Convert.ToString(GroshyModel.shared.CountMoney());

            GroshyDataGrid.ItemsSource = GroshyModel.shared.transactions;
        }
    




        private void GroshyExpense_Click(object sender, RoutedEventArgs e)
        {
            isTransactionExpense = true;
            (sender as Button).Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#1683e0");
            GroshyIncome.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#0F559A");
            //  GroshyComboBoxCategory.SelectedItem = 
            GroshyComboBoxCategory.Items.Clear();
            foreach (var item in GroshyModel.shared.categories)
            {
                if (item.IsExpense)
                    GroshyComboBoxCategory.Items.Add(item.Name);
            }

        }

        private void GroshyIncome_Click(object sender, RoutedEventArgs e)
        {
            isTransactionExpense = false;
            (sender as Button).Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#1683e0");
            GroshyExpense.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#0F559A");
            GroshyComboBoxCategory.Items.Clear();
            foreach (var item in GroshyModel.shared.categories)
            {
                if (!item.IsExpense)
                    GroshyComboBoxCategory.Items.Add(item.Name);
            }
        }
       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            transaction.IsExpense = isTransactionExpense;

            transaction.Account = GroshyModel.shared.accounts.Find(item => item.Name == GroshyComboBoxAccount.Text);
            transaction.Category = GroshyModel.shared.categories.Find(item => item.Name == GroshyComboBoxCategory.Text);
            
            transaction.Date = (DateTime)GroshyDatePicker.SelectedDate;
            transaction.Description = GroshyDescritptionBox.Text;
            transaction.SumOfTransaction = Convert.ToDouble(GroshySumBox.Text);
            GroshyModel.shared.AddTransaction(transaction);
            GroshyDataGrid.ItemsSource = GroshyModel.shared.transactions;
            GroshySumOfAccounts.Content = Convert.ToString(GroshyModel.shared.accounts.ElementAt(0).SumOfAccount);
            MessageBox.Show(Convert.ToString(GroshyModel.shared.transactions.ElementAt(0).Description));
            transaction = new Transaction(true, 12, null, null, DateTime.Now, "ghbdtn");
            GroshySumOfAccounts.Content = Convert.ToString(GroshyModel.shared.CountMoney());
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

        private void GroshySumOfAccounts_Copy_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

            GroshyBlure.Visibility = Visibility.Visible; 
            GroshySettingsWindow groshySettingsWindow = new GroshySettingsWindow();
            //var darkwindow = new Window()
            //{
            //    Background = Brushes.Black,
            //    Opacity = 0.4,
            //    AllowsTransparency = true,
            //    WindowStyle = WindowStyle.None,
            //    WindowState = WindowState.Normal,
            //    Topmost = true
            //};
            //darkwindow.Show();
            groshySettingsWindow.ShowDialog();
            GroshyBlure.Visibility = Visibility.Hidden;
            //darkwindow.Close();

        }

        private void GroshyDescritptionBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            GroshyDescritptionBox.Text = "";
            GroshyDescritptionBox.Foreground = Brushes.Black;
        }

        private void GroshySumBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            GroshySumBox.Text = "";
            GroshySumBox.Foreground = Brushes.Black;
        }

        private void GroshySumBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;
        }

        private Regex regex = new Regex("[^0-9\\,]+");
  
        private void GroshySumBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = regex.IsMatch(e.Text);

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
