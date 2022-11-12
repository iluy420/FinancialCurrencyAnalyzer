using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DataBase.Contexts;
using Extensions;
using FinancialCurrencyAnalyzerDesktopСlient.Models.Settings;
using FinancialCurrencyAnalyzerDesktopСlient.Pages.Tools;
using FinancialCurrencyAnalyzerDesktopСlient.Windows;
using Newtonsoft.Json;



namespace FinancialCurrencyAnalyzerDesktopСlient.Pages.RegistrationAndLogin
{
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();

            if (File.Exists("../../UserSettings/UserLoginSettings.json"))
            {
                RememberData.IsChecked = true;
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
            if(UserLogin(TextBoxLogin.Text, Password.Password, RememberData.IsChecked.Value))
            {
                NavigationService?.Navigate(new Currency());
                NavigationService.RemoveBackEntry();// удаляем страницу login из журнала навигации 
            }
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

            using (var db = new DataBaseContext())
            {
                try
                {
                    string passwordSha = WorkingWithPasswords.GetHashSHA1(password);
                    var user = db.Users.AsNoTracking().FirstOrDefault(u => u.Login == login
                                && u.Password == passwordSha);
                    if (user == null)
                    {
                        MessageBox.Show("Пользователь с такими данными не найден!");
                        return false;
                    }

                    MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                    mainWindow.UserId = user.UserId;
                    //MessageBox.Show("Пользователь успешно найден!");
                }
                catch
                {
                    MessageBox.Show("Проверьте подключение к интернету!");
                    return false;
                }
            }

            RememberDataLogin(isRememberData, login, password);
            return true;
        }

        private void RememberDataLogin(bool isRememberData, string login, string password)
        {
            if (isRememberData == true)
            {
                //запоминаем логин и пароль пользователя на компе 
                using (FileStream fs = new FileStream("../../UserSettings/UserLoginSettings.json", FileMode.OpenOrCreate, FileAccess.Write))
                {
                    StreamWriter writer = new StreamWriter(fs);

                    UserLoginSettings userSettings = new UserLoginSettings(login, password);
                    string json = JsonConvert.SerializeObject(userSettings);

                    writer.WriteLine(json);

                    writer.Close();
                }
            }
            else
            {
                File.Delete("../../UserSettings/UserLoginSettings.json");
            }
        }
    }
}
