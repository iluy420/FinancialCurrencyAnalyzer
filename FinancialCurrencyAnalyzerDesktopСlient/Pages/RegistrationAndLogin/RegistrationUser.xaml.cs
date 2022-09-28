using Consts;
using DataBase.Contexts;
using DataBase.Core.Enums;
using DataBase.Core.Models;
using Extensions;
using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class RegistrationUser : Page
    {
        public RegistrationUser()
        {
            InitializeComponent();

            // запись в combobox значений из EmailDomainEnum
            foreach (EmailDomainEnum s in Enum.GetValues(typeof(EmailDomainEnum)))
            {
                ComboBoxEmailDomain.Items.Add(StringEnum.GetStringValue(s));
            }
        }

        private void ButtunRegistration_Click(object sender, RoutedEventArgs e)
        {
            if (RegistrationUsers(TextBoxName.Text, TextBoxSurname.Text, TextBoxPatronymic.Text,
                TextBoxLogin.Text, TextBoxPassword.Password, TextBoxCopyPassword.Password, TextBoxEmailName.Text,
                ComboBoxEmailDomain.SelectedValue != null ? ComboBoxEmailDomain.SelectedValue.ToString() : ""))
            {
                string alphanumericKey =  WorkingWithPasswords.GetGenerateAlphanumericKey(10);
                string emailFull = TextBoxEmailName.Text + ComboBoxEmailDomain.SelectedValue.ToString();
                string login = TextBoxLogin.Text;

                WorkingWithEmail.EmailRegistration(emailFull, login, alphanumericKey);

                NavigationService?.Navigate(new EmailConfirmation(login, alphanumericKey));
            }
            
        }

        private bool RegistrationUsers(string name, string surname, string patronymic
            , string loginUser, string password, string passwordCopy, string emailName, string emailDomain)
        {
            if (string.IsNullOrEmpty(loginUser.Replace(" ", ""))
                || string.IsNullOrEmpty(password)
                || string.IsNullOrEmpty(passwordCopy)
                || string.IsNullOrEmpty(emailName)
                || string.IsNullOrEmpty(emailDomain)
                || string.IsNullOrEmpty(name)
                || string.IsNullOrEmpty(surname))
            {
                MessageBox.Show("Введите все обязательнае данные!");
                return false;
            }

            else
            {
                using (var db = new DataBaseContext())
                {
                    try
                    {
                        var login = db.Users.AsNoTracking()
                        .FirstOrDefault(u => u.Login == loginUser);

                        var emailFull = db.Users.AsNoTracking()
                        .FirstOrDefault(u => u.EmailFull == (emailName + emailDomain));

                        if (login != null)
                        {
                            MessageBox.Show("Логин занят!");
                            return false;
                        }

                        if (emailFull != null)
                        {
                            MessageBox.Show("Данная почта email уже используется!");
                            return false;
                        }
                    

                        if (WorkingWithPasswords.IsMatchPasswords(password, passwordCopy))
                        {
                            if (WorkingWithPasswords.PasswordStrengthCheck(password))
                            {
                                User user = new User
                                {
                                    UserId = Guid.NewGuid(),
                                    Name = name,
                                    Surname = surname,
                                    Patronymic = patronymic,
                                    Login = loginUser,
                                    Password = WorkingWithPasswords.GetHashSHA1(password),
                                    EmailName = emailName,
                                    EmailDomain = emailDomain,
                                    EmailFull = emailName + emailDomain,
                                    EmailConfirmed = false
                                };

                                db.Users.Add(user);
                                db.SaveChanges();
                                MessageBox.Show("Пользователь зарегистрирован!");
                                return true;

                            }
                            else return false;
                        }
                        else return false;
                    }
                    catch
                    {
                        MessageBox.Show("Проверьте подключение к интернету!");
                        return false;
                    }
                }
            }
        }
    }
}
