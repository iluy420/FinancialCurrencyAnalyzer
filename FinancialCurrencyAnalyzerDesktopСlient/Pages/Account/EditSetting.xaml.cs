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
using DataBase.Contexts;
using DataBase.Core.Models;
using FinancialCurrencyAnalyzerDesktopСlient.Extensions;
using FinancialCurrencyAnalyzerDesktopСlient.Pages.RegistrationAndLogin;
using FinancialCurrencyAnalyzerDesktopСlient.Windows;

namespace FinancialCurrencyAnalyzerDesktopСlient.Pages.Account
{
    public partial class EditSetting : Page
    {
        private int _idSetting { get; set; }
        private string _alphanumericKey { get; set; }

        public EditSetting(string valueSetting, int idSetting)
        {
            InitializeComponent();
            _idSetting = idSetting;
            if (idSetting != 5)
            {
                Title = $"Редактирование настройки профиля";
                SaveSetting.Content = "Сохранить";
                ValueSetting.Text = valueSetting;
            }
            else
            {
                Title = $"Подтверждение почты";
                Helper.Text = "На вашу электронную почту отправлен код подтверждения, введите его ниже";
                SaveSetting.Content = "Подтвердить";
                _alphanumericKey = valueSetting;
            }
            NamePage.Text = Title;
        }

        private void SaveSetting_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ValueSetting.Text))
            {
                MessageBox.Show("Заполните все поля!");
            }
            else
            {
                bool exit = true;
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

                using (var db = new DataBaseContext())
                {
                    User user = db.Users.First(x => x.UserId == mainWindow.UserId);
                    try
                    {
                        switch (_idSetting)
                        {
                            case 1:
                                user.Name = ValueSetting.Text;
                                break;
                            case 2:
                                user.Surname = ValueSetting.Text;
                                break;
                            case 3:
                                user.Patronymic = ValueSetting.Text;
                                break;
                            case 4:
                                var userold = db.Users.FirstOrDefault(x => x.Login == ValueSetting.Text);
                                if(userold == null|| userold.UserId == mainWindow.UserId)
                                {
                                    user.Login = ValueSetting.Text;
                                }
                                else
                                {
                                    MessageBox.Show("Логин занят!");
                                    exit = false;
                                }
                                break;
                            case 5:
                                if (_alphanumericKey == ValueSetting.Text)
                                {
                                    user.EmailConfirmed = true;
                                    MessageBox.Show("Почта подтверждена!");
                                }
                                else
                                {
                                    MessageBox.Show("Введен не верный код подтверждения!");
                                    exit = false;
                                }
                                break;
                        }
                        db.SaveChanges();
                        MessageBox.Show("Данные изменены!");
                    }
                    catch
                    {
                        MessageBox.Show("Что-то пошло не так! Проверте подключение интернета и попробуйте снова.");
                    }

                }
                if (exit)
                {
                    NavigationService.Navigate(new AccountSettings());
                }
            }
        }
    }
}
