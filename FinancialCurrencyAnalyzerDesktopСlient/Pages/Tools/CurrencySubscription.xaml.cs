using DataBase.Contexts;
using DataBase.Core.Models;
using FinancialCurrencyAnalyzerDesktopСlient.Models;
using FinancialCurrencyAnalyzerDesktopСlient.Pages.RegistrationAndLogin;
using FinancialCurrencyAnalyzerDesktopСlient.Windows;
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
using System.Xml;

namespace FinancialCurrencyAnalyzerDesktopСlient.Pages.Tools
{
    public partial class CurrencySubscription : Page
    {
        public CurrencySubscription()
        {
            InitializeComponent();

            Title = $"Подписка на курсы валюты";
            NamePage.Text = Title;

            CurrenciesImport(DateTime.Now);

            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;


            using (var db = new DataBaseContext())
            {
                _userCurrency = db.UserCurrencys.Where(x => x.UserId == mainWindow.UserId)
                    .Cast<UserCurrency>().ToList();

                foreach (var currency in _currencies)
                {
                    CheckBoxModel checkBoxModel = new CheckBoxModel();
                    checkBoxModel.currencyModel = currency;
                    checkBoxModel.Check = false;

                    if(_userCurrency.Count != 0)
                    {
                        UserCurrency gg = _userCurrency.FirstOrDefault(x => x.Сurrency.VcharCode == currency.VchCode);
                        if (gg != null)
                        {
                            checkBoxModel.Check = true;
                        }
                    }
                    CurrencySubscriptionDataGrid.Items.Add(checkBoxModel);
                }

            }
        }

        private void CheckBox_Subscription_Click(object sender, RoutedEventArgs e)
        {
            if((sender as CheckBox).IsChecked == true)
            {
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                CurrencyModel currency = ((sender as CheckBox).DataContext as CheckBoxModel).currencyModel;
                using (var db = new DataBaseContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.UserId == mainWindow.UserId);
                    var ucurrenc = db.Сurrencys.FirstOrDefault(u => u.VcharCode == currency.VchCode);
                    UserCurrency gg = new UserCurrency();
                    gg.User = user;
                    gg.UserId = user.UserId;
                    gg.Сurrency = ucurrenc;
                    gg.Vcode = ucurrenc.Vcode;

                    if (user != null)
                    {
                        db.UserCurrencys.Add(gg);
                    }
                    db.SaveChanges();
                }
            }
            else
            {
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                CurrencyModel currency = ((sender as CheckBox).DataContext as CheckBoxModel).currencyModel;
                using (var db = new DataBaseContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.UserId == mainWindow.UserId);
                    var ucurrenc = db.Сurrencys.FirstOrDefault(u => u.VcharCode == currency.VchCode);

                    UserCurrency userCurrency = db.UserCurrencys.Where(x => x.UserId == mainWindow.UserId
                    && x.Сurrency.VcharCode == ucurrenc.VcharCode).First();

                    if (user != null)
                    {
                        db.UserCurrencys.Remove(userCurrency);
                    }
                    db.SaveChanges();
                }
            }
        }

        private static List<CurrencyModel> _currencies = new List<CurrencyModel>();
        private static List<UserCurrency> _userCurrency = new List<UserCurrency>();

        private void CurrenciesImport(DateTime date)
        {
            _currencies.Clear();

            СentralBankApi.ApiCB.DailyInfoSoapClient client = new СentralBankApi.ApiCB.DailyInfoSoapClient("DailyInfoSoap");
            XmlNode doc = client.GetCursOnDateXML(Convert.ToDateTime(date.ToString("yyyy-MM-ddT00:00:00")));

            foreach (XmlNode xmlNode in doc)
            {
                CurrencyModel currency = new CurrencyModel();
                foreach (XmlNode xmlNode1 in xmlNode.ChildNodes)
                {
                    if (xmlNode1.Name == "Vname") currency.Vname = xmlNode1.InnerText.Trim();
                    if (xmlNode1.Name == "Vnom") currency.Vnom = xmlNode1.InnerText;
                    if (xmlNode1.Name == "Vcurs") currency.Vcurs = xmlNode1.InnerText;
                    if (xmlNode1.Name == "Vcode") currency.Vcode = xmlNode1.InnerText;
                    if (xmlNode1.Name == "VchCode") currency.VchCode = xmlNode1.InnerText;
                }
                _currencies.Add(currency);

            }
        }

        public class CheckBoxModel
        {
            public CurrencyModel currencyModel { get; set; }
            public bool Check { get; set; }

            public CheckBoxModel() { }

        }
       
    }
}
