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
        public SignIn()
        {
            InitializeComponent();
        }

        private void GroshyRegister_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            Register register = new Register();
            register.ShowDialog();
            Show();
        }

        private void GroshySignIn_Click(object sender, RoutedEventArgs e)
        {
            GroshyModel.shared.GetUser(GroshyLogin.Text, GroshyPassword.Text);
            if(GroshyModel.shared.user.Id != 0)
            {
                Close();
            }
            else
            {
                MessageBox.Show("Неправильно введён логин или пароль!");
            }
        }
    }
}
