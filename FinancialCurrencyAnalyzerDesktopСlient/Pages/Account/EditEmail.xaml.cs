using DataBase.Core.Enums;
using Extensions;
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
using DataBase.Core.Models;
using DataBase.Contexts;

namespace FinancialCurrencyAnalyzerDesktopСlient.Pages.Account
{
    public partial class EditEmail : Page
    {
        private User _user { get; set; }

        public EditEmail(User user)
        {
            InitializeComponent();
            _user = user;
            // запись в combobox значений из EmailDomainEnum
            foreach (EmailDomainEnum s in Enum.GetValues(typeof(EmailDomainEnum)))
            {
                ComboBoxEmailDomain.Items.Add(StringEnum.GetStringValue(s));
            }
            ValueSetting.Text = _user.EmailName;
            ComboBoxEmailDomain.SelectedValue = _user.EmailDomain;
        }

        private void SaveEmail_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(ValueSetting.Text)
                || string.IsNullOrEmpty(ComboBoxEmailDomain.SelectedValue.ToString()))
            {
                MessageBox.Show("Заполните все поля!");
            }
            else
            {
                try
                {
                    using (var db = new DataBaseContext())
                    {
                        User user = db.Users.First(x => x.UserId == _user.UserId);
                        user.EmailName = ValueSetting.Text;
                        user.EmailDomain = ComboBoxEmailDomain.SelectedValue.ToString();
                        user.EmailFull = ValueSetting.Text + ComboBoxEmailDomain.SelectedValue.ToString();
                        user.EmailConfirmed = false;
                        string login = user.Login;
                        string alphanumericKey = WorkingWithPasswords.GetGenerateAlphanumericKey(10);
                        string emailFull = user.EmailFull;
                        db.SaveChanges();
                        WorkingWithEmail.EmailRegistration(emailFull, login, alphanumericKey);
                        NavigationService.Navigate(new EditSetting(alphanumericKey, 5));
                    }
                }
                catch
                {
                    MessageBox.Show("Проверьте подключение к интернету");
                }
            }
        }
    }
}
