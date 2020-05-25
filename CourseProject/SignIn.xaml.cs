using CourseProject.Entities;
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
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class SignIn : Window
    {
        bool flag = false;
        PasswordConverter converter = new PasswordConverter();
        public SignIn()
        {
            InitializeComponent();
            GroshyLogin.MaxLength = 19;
            GroshyPassword.MaxLength = 8;
        }

        private void GroshyRegister_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Hidden;
            Register register = new Register();
            register.ShowDialog();
            Visibility = Visibility.Visible;
        }

        private void GroshySignIn_Click(object sender, RoutedEventArgs e)
        {
            converter.Password = GroshyPassword.Password;
            GroshyModel.shared.GetUser(GroshyLogin.Text, converter.Password);
            if(GroshyModel.shared.user.Id != 0)
            {
                MainWindow main = new MainWindow();
                main.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Неправильно введён логин или пароль!");
            }
        }

        private void ShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            //GroshyPassword.;
        }
    }
}
