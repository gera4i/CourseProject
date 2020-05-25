using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CourseProject
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class GroshySettingsWindow : Window
    {
        private string Exit = GroshyModel.shared.user.Login;
        public GroshySettingsWindow()
        {
            InitializeComponent();
            GroshyNameOfCategory.MaxLength = 19;
            GroshyNameOfAccount.MaxLength = 19;
            GroshySumOfAccount.MaxLength = 9;
            foreach (var item in GroshyModel.shared.accounts)
            {
                GroshyDeleteAccountComboBox.Items.Add(item.Name);
            }
            foreach (var item in GroshyModel.shared.categories)
            {
                GroshyDeleteCategoryComboBox.Items.Add(item.Name);
            }
            UserName.Content = Exit + ", Вы можете выйти из аккаунта.";
        }


        private void GroshyAddCategory_Click(object sender, RoutedEventArgs e)
        {
            GroshyDeleteCategoryComboBox.Items.Clear();
            string tempStr = GroshyNameOfCategory.Text.Trim().ToLower();
            bool flag = true;
            while (flag)
            {
                if (GroshyNameOfCategory.Text == "Введите название категории" || tempStr == "")
                {
                    MessageBox.Show("Вы не указали название категории");
                    break;
                }

                tempStr = tempStr.Replace(tempStr.First().ToString(), tempStr.First().ToString().ToUpper());
                if (Convert.ToBoolean(RadioButtonExpense.IsChecked) || Convert.ToBoolean(RadioButtonIncome.IsChecked))
                {
                    if (GroshyModel.shared.categories.Find(x => x.Name == tempStr) == null)
                    {
                        if (Regex.IsMatch(GroshyNameOfCategory.Text, @"^[a-zA-Zа-яА-Я0-9\s]*$"))
                        {
                            if (Convert.ToBoolean(RadioButtonExpense.IsChecked))
                            {
                                GroshyModel.shared.AddCategory(1, GroshyNameOfCategory.Text);
                            }
                            else
                            {
                                GroshyModel.shared.AddCategory(0, GroshyNameOfCategory.Text);
                            }
                            foreach (var item in GroshyModel.shared.categories)
                            {
                                GroshyDeleteCategoryComboBox.Items.Add(item.Name);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Неверный формат категории!\n Используйте только буквы и цифры.");
                        }
                           
                    }
                    else MessageBox.Show("Такая категория уже существует");
                }
                else
                {
                    MessageBox.Show("Вы не указали тип категории");
                    return;
                }

                flag = false;
            }
            GroshyNameOfCategory.Text = "Введите название категории";
            GroshyNameOfCategory.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#8A8A8A");
        }
        private Regex regex1 = new Regex("[^0-9\\,]+");

        private void GroshyAddAccount_Click(object sender, RoutedEventArgs e)
        {
            GroshyDeleteAccountComboBox.Items.Clear();
            string NameOfAccount = GroshyNameOfAccount.Text.Trim().ToLower();
            double Summa = 0;
            bool flag = true;
            while (flag)
            {
                if(GroshySumOfAccount.Text != "Введите сумму счёта")
                {
                    if (Regex.Match(GroshySumOfAccount.Text, @"^[0-9]{1,5}[,]?[0-9]{0,2}$").Success)
                    {
                        Summa = Convert.ToDouble(GroshySumOfAccount.Text);
                    }
                    else
                    {
                        MessageBox.Show("Неправильный формат суммы");
                        GroshyNameOfCategory.Text = "Введите сумму счёта";
                        GroshyNameOfCategory.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#8A8A8A");
                        return;
                    }
                }
                if (GroshyNameOfAccount.Text == "Введите название счёта" || NameOfAccount == "")
                {

                    MessageBox.Show("Вы не указали название счёта");
                    break;
                }
                NameOfAccount = NameOfAccount.Replace(NameOfAccount.First().ToString(), NameOfAccount.First().ToString().ToUpper());
                if (GroshyModel.shared.accounts.Find(x => x.Name == NameOfAccount) == null)
                {
                    GroshyModel.shared.AddAccount(Summa, NameOfAccount);
                    foreach (var item in GroshyModel.shared.accounts)
                    {
                        GroshyDeleteAccountComboBox.Items.Add(item.Name);
                    }
                }
                else
                {
                    MessageBox.Show("Счёт с таким названием уже существует!");
                    return;
                }
                flag = false;
            }
            GroshyNameOfAccount.Text = "Введите название счёта";
            GroshyNameOfAccount.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#8A8A8A");
            GroshySumOfAccount.Text = "Введите сумму счёта";
            GroshySumOfAccount.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#8A8A8A");

        }

        ///////////////// 
        private void GroshyNameOfAccount_GotFocus(object sender, RoutedEventArgs e)
        {
            if (GroshyNameOfAccount.Text == "Введите название счёта")
            {
                GroshyNameOfAccount.Text = "";
                GroshyNameOfAccount.Foreground = Brushes.Black;
            }
        }


        private void GroshyNameOfAccount_LostFocus(object sender, RoutedEventArgs e)
        {
            if (GroshyNameOfAccount.Text == "")
            {
                GroshyNameOfAccount.Text = "Введите название счёта";
                GroshyNameOfAccount.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#8A8A8A");
            }
        }

        ///////////////// 
        private void GroshyNameOfCategory_GotFocus(object sender, RoutedEventArgs e)
        {
            if (GroshyNameOfCategory.Text == "Введите название категории")
            {
                GroshyNameOfCategory.Text = "";
                GroshyNameOfCategory.Foreground = Brushes.Black;
            }
        }
        private void GroshyNameOfCategory_LostFocus(object sender, RoutedEventArgs e)
        {
            if (GroshyNameOfCategory.Text == "")
            {
                GroshyNameOfCategory.Text = "Введите название категории";
                GroshyNameOfCategory.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#8A8A8A");
            }
        }
        //////////////////
        private void GroshySumOfAccount_GotFocus(object sender, RoutedEventArgs e)
        {
            if (GroshySumOfAccount.Text == "Введите сумму счёта")
            {
                GroshySumOfAccount.Text = "";
                GroshySumOfAccount.Foreground = Brushes.Black;
            }

        }

        private void GroshySumOfAccount_LostFocus(object sender, RoutedEventArgs e)
        {
            if (GroshySumOfAccount.Text == "")
            {
                GroshySumOfAccount.Text = "Введите сумму счёта";
                GroshySumOfAccount.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#8A8A8A");
            }
        }


        private Regex regex = new Regex("[^0-9\\,]+");


        private void GroshySumOfAccount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = regex.IsMatch(e.Text);
        }

        private void GroshySumOfAccount_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;

        }

        private void GroshyDeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            if(GroshyDeleteCategoryComboBox.Text!="")
            {
                GroshyModel.shared.DeleteCategory(GroshyModel.shared.categories.Find(item => item.Name == GroshyDeleteCategoryComboBox.Text));
                GroshyDeleteCategoryComboBox.Text = "";
                GroshyDeleteCategoryComboBox.Items.Clear();
                foreach (var item in GroshyModel.shared.categories)
                {
                    GroshyDeleteCategoryComboBox.Items.Add(item.Name);
                }

            }
            else
            {
                MessageBox.Show("Выберите категорию!");
            }
        }

        private void GroshyDeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            if (GroshyDeleteAccountComboBox.Text != "")
            {
                GroshyModel.shared.DeleteAccount(GroshyModel.shared.accounts.Find(item => item.Name == GroshyDeleteAccountComboBox.Text));
                GroshyDeleteAccountComboBox.Items.Clear();
                GroshyDeleteAccountComboBox.Text = "";
                foreach (var item in GroshyModel.shared.accounts)
                {
                    GroshyDeleteAccountComboBox.Items.Add(item.Name);
                }
            }
            else
            {
                MessageBox.Show("Выберите счёт!");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SetDayX.SelectedDate != null)
            {
                GroshyModel.shared.user.Day = SetDayX.SelectedDate.Value.Day;
                GroshyModel.shared.UpdateUser();
                MessageBox.Show("День отсчёта: " + SetDayX.SelectedDate.Value.Day + " для каждого месяца");
            }
            else
            {
                MessageBox.Show("Выберите дату!");
            }

        }

        private void UserName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            GroshyModel.shared.isClose = true;
            Close();
        }
    }
}
