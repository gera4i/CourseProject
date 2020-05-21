using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace CourseProject
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class GroshySettingsWindow : Window
    {
        public GroshySettingsWindow()
        {
            InitializeComponent();
            GroshyNameOfCategory.MaxLength = 19;
            GroshyNameOfAccount.MaxLength = 19;
        }


        private void GroshyAddCategory_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            while (flag)
            {
                if (GroshyNameOfCategory.Text != "Введите название категории")
                {
                    string tempStr = GroshyNameOfCategory.Text.Trim().ToLower();
                    if (tempStr == "")
                    {
                        MessageBox.Show("Вы не указали название категории");
                        break;
                    }

                    tempStr = tempStr.Replace(tempStr.First().ToString(), tempStr.First().ToString().ToUpper());
                    if (Convert.ToBoolean(RadioButtonExpense.IsChecked) || Convert.ToBoolean(RadioButtonIncome.IsChecked))
                    {
                        if (GroshyModel.shared.categories.Find(x => x.Name == tempStr) == null)
                        {
                            if (Convert.ToBoolean(RadioButtonExpense.IsChecked))
                            {
                                GroshyModel.shared.AddCategory(1, GroshyNameOfCategory.Text);
                            }
                            else
                            {
                                GroshyModel.shared.AddCategory(0, GroshyNameOfCategory.Text);
                            }
                        }
                        else MessageBox.Show("Такая категория уже существует");
                    }
                    else
                    {
                        MessageBox.Show("Вы не указали тип категории");
                        return;
                    }
                }
                else
                { 
                    MessageBox.Show("Вы не указали название категории"); 
                }
                flag = false;
            }
            GroshyNameOfCategory.Text = "Введите название категории";
            GroshyNameOfCategory.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#8A8A8A");
        }

        private void GroshyAddAccount_Click(object sender, RoutedEventArgs e)
        {
            string tempStr = GroshyNameOfAccount.Text.Trim().ToLower();
            tempStr = tempStr.Replace(tempStr.First().ToString(), tempStr.First().ToString().ToUpper());
            if (Convert.ToBoolean(RadioButtonExpense.IsChecked) || Convert.ToBoolean(RadioButtonIncome.IsChecked))
            {
                if (GroshyModel.shared.categories.Find(x => x.Name == tempStr) == null)
                {
                    if (Convert.ToBoolean(RadioButtonExpense.IsChecked))
                    {
                        GroshyModel.shared.AddCategory(1, GroshyNameOfCategory.Text);
                    }
                    else
                    {
                        GroshyModel.shared.AddCategory(0, GroshyNameOfCategory.Text);
                    }
                }
                else
                {
                    MessageBox.Show("Такая категория уже существует");
                }
            }
            else
            {
                MessageBox.Show("Вы не указали тип категории");
            }

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
            if(GroshyNameOfCategory.Text == "Введите название категории")
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
    }
}
