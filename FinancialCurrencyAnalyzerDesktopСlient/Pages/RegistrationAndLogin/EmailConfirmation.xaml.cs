using DataBase.Contexts;
using DataBase.Core.Models;
using FinancialCurrencyAnalyzerDesktopСlient.Extensions;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FinancialCurrencyAnalyzerDesktopСlient.Pages.RegistrationAndLogin
{
    public partial class EmailConfirmation : Page
    {
        private string _login;
        private string _alphanumericKey;

        public EmailConfirmation(string login, string alphanumericKey)
        {
            InitializeComponent();
            _login = login;
            _alphanumericKey = alphanumericKey;
        }

        private void ButtonConfirmation_Click(object sender, RoutedEventArgs e)
        {
            if (_alphanumericKey == ConfirmationCode.Text)
            {
                using (var db = new DataBaseContext())
                {
                    try
                    {
                        var user = db.Users.AsNoTracking()
                        .FirstOrDefault(u => u.Login == _login);

                        if (user != null)
                        {
                            user.EmailConfirmed = true;

                            db.Users.Add(user);
                            db.SaveChanges();

                            MessageBox.Show("Почта подтверждена!");
                        }
                        else
                        {
                            MessageBox.Show("Пользователь не найдет в бд!");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Проверьте подключение к интернету!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Введен не верный код подтверждения!");
            }
        }

        private void ButtonPass_Click(object sender, RoutedEventArgs e)
        {
            NavigatorExtensions.RemoveAllEntry(NavigationService);
            NavigationService?.Navigate(new Login());
        }
    }
}
