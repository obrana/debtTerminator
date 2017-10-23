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
using Models;

namespace PapaJonsDebtTerminator.Views
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private DbLayer.DbLayer dbLayer;
        public RegistrationWindow()
        {
            InitializeComponent();
            dbLayer = DbLayer.DbLayer.Database;
        }

        private void ButtonRegister_OnClick(object sender, RoutedEventArgs e)
        {
            if (TxtLogin.Text.Length == 0 || TxtPassword.Text.Length == 0)
            {
                MessageBox.Show("Login and password is not corect!", "Error");
                return;
            }
            dbLayer.CreateUser(new User()
            {
                Username = TxtLogin.Text,
                Password = TxtPassword.Text
            });
        }

        private void ButtonBack_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
