using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using FinancialCurrencyAnalyzerDesktopСlient.Models.Settings;
using FinancialCurrencyAnalyzerDesktopСlient.Properties;
using Newtonsoft.Json;



namespace FinancialCurrencyAnalyzerDesktopСlient.Pages
{
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();

            if (File.Exists("../../UserSettings/UserLoginSettings.json"))
            {
                using (FileStream fs = new FileStream("../../UserSettings/UserLoginSettings.json", FileMode.OpenOrCreate, FileAccess.Read))
                {
                    StreamReader reader = new StreamReader(fs);

                    string setting = reader.ReadLine();

                    UserLoginSettings userLoginSettings = JsonConvert.DeserializeObject<UserLoginSettings>(setting);

                    TextBoxLogin.Text = userLoginSettings.Login;
                    Password.Password = userLoginSettings.Password;
                }
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = TextBoxLogin.Text;
            string password = Password.Password;
            bool isRememberDat = RememberData.IsChecked.Value;
            TextBoxLogin.Clear();///тут чтото не так!

            Password.Clear();
            RememberData.IsChecked = false;
            UserLogin(login, password, isRememberDat);

            
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new RegistrationUser());
        }

        private bool UserLogin(string login, string password, bool isRememberData)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль!");
                return false;
            }

            //using (var db = new DataBaseContext())
            //{
            //    password = WorkingWithPasswords.GetHashSHA1(password);
            //    var user = db.Users.AsNoTracking().FirstOrDefault(u => u.Login == login
            //                && u.Password == password);
            //    if (user == null)
            //    {
            //        MessageBox.Show("Пользователь с такими данными не найден!");
            //        return false;
            //    }

            //    MessageBox.Show("Пользователь успешно найден!");
            //}

            if (isRememberData == true) {
                //запоминаем логин и пароль пользователя на компе 
                using (FileStream fs = new FileStream("../../UserSettings/UserLoginSettings.json", FileMode.OpenOrCreate, FileAccess.Write))
                {
                    StreamWriter writer = new StreamWriter(fs);

                    UserLoginSettings userSettings = new UserLoginSettings(login, password);
                    string json = JsonConvert.SerializeObject(userSettings);

                    writer.WriteLine(json);
                    writer.Close();
                    MessageBox.Show("Пароль и логин записаны");

                }
                
            }

            NavigationService?.Navigate(new MainMenu());//переход в главное меню

            return true;

        }
    }
}
