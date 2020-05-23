using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    public partial class Register : Window
    {
        PasswordConverter converter = new PasswordConverter();
        public Register()
        {
            InitializeComponent();
            GroshyLogin.MaxLength = 19;
        }

    
        private void GroshyRegister_Click(object sender, RoutedEventArgs e)
        {

            if (GroshyPassword1.Text == GroshyPassword2.Text)
            {
                
                if (Regex.IsMatch(GroshyLogin.Text, @"^[a-zA-Zа-яА-Я0-9]*$"))
                {
                    if(GroshyModel.shared.CheckLogin(GroshyLogin.Text))
                    {
                        MessageBox.Show("Пользователь с таким логином уже существует.");
                        return;
                    }
                    converter.Password = GroshyPassword1.Text;
                    if (converter.Password != null)
                    {
                        MessageBox.Show("Вы успешно зарегистрированы." + "\n" + "Ваш логин: " + GroshyLogin.Text + "\n" + "Ваш пароль: " + GroshyPassword1.Text);
                        GroshyModel.shared.SetUser(GroshyLogin.Text, converter.Password);
                        Close();
                    }
                    else MessageBox.Show("Неверно введен пароль!\nПароль может содержать только буквы [a-z]\nХотя бы одну заглавную букву и цифры\nДлина должна быть 8 символов.");
                }
                else MessageBox.Show("Неверно введен логин!\nЛогин должен состоять из букв и цифр.");
            }
            else MessageBox.Show("Пароли не совпадают!");
        }
    }
}
