namespace FinancialCurrencyAnalyzerDesktopСlient.Models.Settings
{
    public class UserLoginSettings
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public UserLoginSettings(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
