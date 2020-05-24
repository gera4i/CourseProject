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
            GroshyComboBoxCategorySort.Items.Add("Все категории");
            GroshyComboBoxCategorySort.Text = "Все категории";
            foreach (var item in GroshyModel.shared.categories)
            {
                if (item.IsExpense)
                {
                    GroshyComboBoxCategory.Items.Add(item.Name);
                    GroshyComboBoxCategorySort.Items.Add(item.Name);
                }
            }
            GroshyComboBoxAccountSort.Items.Add("Все cчета");
            GroshyComboBoxAccountSort.Text = "Все cчета";
            foreach (var item in GroshyModel.shared.accounts)
            {
                GroshyComboBoxAccount.Items.Add(item.Name);
                GroshyComboBoxAccountSort.Items.Add(item.Name);
            }

            GroshyDatePickerStart.Text = Convert.ToString(DateTime.Today.AddMonths(-1));
            GroshyDatePickerEnd.Text = Convert.ToString(DateTime.Today);
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

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            GroshyModel.shared.transactions.Clear();
            foreach (var item in GroshyModel.shared.tempTransactionList)
            {
                GroshyModel.shared.transactions.Add(item);
            }
            if (GroshyDatePickerStart.SelectedDate > GroshyDatePickerEnd.SelectedDate)
            {
                MessageBox.Show("Неправильно введена дата!");
                return;
            }
            string Info = "";
            if (GroshyComboBoxCategorySort.Text == "Все категории" && GroshyComboBoxAccountSort.Text == "Все cчета")
            {
                if (isTransactionExpense)
                {
                    Info += "Расходы ";
                }
                else
                {
                    Info += "Доходы ";
                }
            }
            else
            {
                if (GroshyComboBoxCategorySort.Text == "Все категории")
                {
                    Info += "Расходы по счёту '" + GroshyComboBoxAccountSort.Text + "' ";
                    foreach (var item in GroshyModel.shared.transactions.ToList())
                    {
                        if (item.Account.Name != GroshyComboBoxAccountSort.Text)
                        {
                            GroshyModel.shared.transactions.Remove(item);
                        }
                    }
                }
                else if (GroshyComboBoxAccountSort.Text == "Все cчета")
                {
                    if (isTransactionExpense)
                    {
                        Info += "Расходы по категории ";
                        Info += "'" + GroshyComboBoxCategorySort.Text + "' ";
                    }
                    else
                    {
                        Info += "Доходы по категории ";
                        Info += "'" + GroshyComboBoxCategorySort.Text + "' ";
                    }
                    foreach (var item in GroshyModel.shared.transactions.ToList())
                    {
                        if (item.Category.Name != GroshyComboBoxCategorySort.Text)
                        {
                            GroshyModel.shared.transactions.Remove(item);
                        }
                    }
                }
                else
                {
                    if (isTransactionExpense)
                    {
                        Info += "Расходы по категории ";
                        Info += "'" + GroshyComboBoxCategorySort.Text + "' ";
                    }
                    else
                    {
                        Info += "Доходы по категории";
                        Info += "'" + GroshyComboBoxCategorySort.Text + "' ";
                    }

                    Info += "по счёту '" + GroshyComboBoxAccountSort.Text + "' ";

                    foreach (var item in GroshyModel.shared.transactions.ToList())
                    {
                        if (item.Category.Name != GroshyComboBoxCategorySort.Text)
                        {
                            GroshyModel.shared.transactions.Remove(item);
                        }
                    }
                    foreach (var item in GroshyModel.shared.transactions.ToList())
                    {
                        if (item.Account.Name != GroshyComboBoxAccountSort.Text)
                        {
                            GroshyModel.shared.transactions.Remove(item);
                        }
                    }
                }
            }
            if (GroshyDatePickerStart.SelectedDate != GroshyDatePickerEnd.SelectedDate)
            {
                Info += "за период с " + GroshyDatePickerStart.SelectedDate.ToString().Replace("00:00:00", "") + " по " + GroshyDatePickerEnd.SelectedDate.ToString().Replace("00:00:00", "");
                foreach (var item in GroshyModel.shared.transactions.ToList())
                {
                    if (item.Date >= GroshyDatePickerEnd.SelectedDate.Value.AddDays(1))
                    {
                        GroshyModel.shared.transactions.Remove(item);
                    }
                }
                foreach (var item in GroshyModel.shared.transactions.ToList())
                {
                    if (item.Date <= GroshyDatePickerStart.SelectedDate.Value.AddDays(-1))
                    {
                        GroshyModel.shared.transactions.Remove(item);
                    }
                }
            }
            else
            {
                Info += "за " + GroshyDatePickerStart.SelectedDate.ToString().Replace("00:00:00", "");
                foreach (var item in GroshyModel.shared.transactions.ToList())
                {
                    if (item.Date != GroshyDatePickerStart.SelectedDate)
                    {
                        GroshyModel.shared.transactions.Remove(item);
                    }
                }
            }
            if(GroshyModel.shared.transactions.Count == 0)
            {
                SortInfo.Content = "Ничего не найдено :(";
            }
            else
            {
                SortInfo.Content = Info;
            }
            GroshyDatePickerEnd.Text = Convert.ToString(DateTime.Today);
            GroshyDatePickerStart.Text = Convert.ToString(DateTime.Today.AddMonths(-1));
            GroshyComboBoxCategorySort.Text = "Все категории";
            GroshyComboBoxAccountSort.Text = "Все cчета";
        }

    }
}
