using DataBase.Contexts;
using DataBase.Core.Models;
using Extensions;
using FinancialCurrencyAnalyzerDesktopСlient.Pages.RegistrationAndLogin;
using FinancialCurrencyAnalyzerDesktopСlient.Properties;
using FinancialCurrencyAnalyzerDesktopСlient.Windows;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace FinancialCurrencyAnalyzerDesktopСlient.Pages.Account
{
    public partial class AccountSettings : Page
    {
        private User _User { get; set; }
        public AccountSettings()
        {
            InitializeComponent();

            string setting = "";
            if (File.Exists("../../UserSettings/UserThemeSettings.txt"))
            {
                using (FileStream fs = new FileStream("../../UserSettings/UserThemeSettings.txt", FileMode.OpenOrCreate, FileAccess.Read))
                {
                    StreamReader reader = new StreamReader(fs);

                    setting = reader.ReadLine();
                }
            }
            if (setting == "Dictionaries/DarkTheme.xaml")
            {
                ThemaCheckbox.IsChecked = true;

                IconLogin.Foreground = new SolidColorBrush(Color.FromRgb(98, 240, 178));
                IconName.Foreground = new SolidColorBrush(Color.FromRgb(98, 240, 178));
                IconSurname.Foreground = new SolidColorBrush(Color.FromRgb(98, 240, 178));
                IconPatronymic.Foreground = new SolidColorBrush(Color.FromRgb(98, 240, 178));
                IconFullEmail.Foreground = new SolidColorBrush(Color.FromRgb(98, 240, 178));
                IconEmailConfirmed.Foreground = new SolidColorBrush(Color.FromRgb(98, 240, 178));
            }
                
            Title = $"Профиль";
            NamePage.Text = Title;

            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

            using (var db = new DataBaseContext())
            {
                _User = db.Users.First(x => x.UserId == mainWindow.UserId);

                NameUser.Text = _User.Name;
                Surname.Text = _User.Surname;
                Patronymic.Text = _User.Patronymic;
                FullEmail.Text = _User.EmailFull;
                Login.Text = _User.Login;
                if (!_User.EmailConfirmed)
                {
                    EmailConfirmed.Text = "Почта не подтверждена!";
                }
                else
                {
                    EmailConfirmed.Visibility = Visibility.Hidden;
                    BtnEmailConfirmed.Visibility = Visibility.Hidden;
                }
            }
        }

        private void BtnEmailConfirmed_Click(object sender, RoutedEventArgs e)
        {
            string login = _User.Login;
            string alphanumericKey = WorkingWithPasswords.GetGenerateAlphanumericKey(10);
            string emailFull = _User.EmailFull;

            WorkingWithEmail.EmailRegistration(emailFull, login, alphanumericKey);
            NavigationService.Navigate(new EditSetting(alphanumericKey, 5));
        }

        private void BtnEmail_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditEmail(_User));
        }

        private void BtnPatronymic_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditSetting(Patronymic.Text, 3));
        }

        private void BtnSurname_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditSetting(Surname.Text, 2));
        }

        private void BtnName_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditSetting(NameUser.Text, 1));
        }

        private void ThemaCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            if(ThemaCheckbox.IsChecked == true)
            {
                using (FileStream fs = new FileStream("../../UserSettings/UserThemeSettings.txt", FileMode.OpenOrCreate, FileAccess.Write))
                {
                    StreamWriter writer = new StreamWriter(fs);

                    writer.WriteLine("Dictionaries/DarkTheme.xaml");

                    writer.Close();
                }
                // определяем путь к файлу ресурсов
                var uri = new Uri("Dictionaries/DarkTheme.xaml", UriKind.Relative);
                // загружаем словарь ресурсов
                ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
                // очищаем коллекцию ресурсов приложения
                Application.Current.Resources.MergedDictionaries.Clear();
                // добавляем загруженный словарь ресурсов
                Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            }
            else
            {
                using (FileStream fs = new FileStream("../../UserSettings/UserThemeSettings.txt", FileMode.OpenOrCreate, FileAccess.Write))
                {
                    StreamWriter writer = new StreamWriter(fs);

                    writer.WriteLine("../Dictionaries/LightTheme.xaml");

                    writer.Close();
                }
                // определяем путь к файлу ресурсов
                var uri = new Uri("Dictionaries/LightTheme.xaml", UriKind.Relative);
                // загружаем словарь ресурсов
                ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
                // очищаем коллекцию ресурсов приложения
                Application.Current.Resources.MergedDictionaries.Clear();
                // добавляем загруженный словарь ресурсов
                Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            }
            NavigationService.Navigate(new AccountSettings());
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditSetting(Login.Text, 4));
        }
    }
}
