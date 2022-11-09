using DataBase.Contexts;
using DataBase.Core.Models;
using Extensions;
using FinancialCurrencyAnalyzerDesktopСlient.Enums;
using FinancialCurrencyAnalyzerDesktopСlient.Models;
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
using static FinancialCurrencyAnalyzerDesktopСlient.Pages.Tools.CurrencySubscription;

namespace FinancialCurrencyAnalyzerDesktopСlient.Pages.Tools
{
    public partial class PreciousMetalsSubscription : Page
    {
        private string _setting { get; set; }

        public PreciousMetalsSubscription()
        {
            InitializeComponent();

            if (File.Exists("../../UserSettings/UserThemeSettings.txt"))
            {
                using (FileStream fs = new FileStream("../../UserSettings/UserThemeSettings.txt", FileMode.OpenOrCreate, FileAccess.Read))
                {
                    StreamReader reader = new StreamReader(fs);

                    _setting = reader.ReadLine();
                }
            }

            Title = $"Подписка на курсы металлов";
            NamePage.Text = Title;

            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

            using (var db = new DataBaseContext())
            {
                _userMetals = db.UserMetals.Where(x => x.UserId == mainWindow.UserId)
                    .Cast<UserMetals>().ToList();
                int i = 1;
                foreach (MetalsEnum metal in Enum.GetValues(typeof(MetalsEnum)))
                {
                    CheckBoxModel checkBoxModel = new CheckBoxModel();
                    checkBoxModel.Check = false;

                    string nameMet = StringEnum.GetStringValue(metal);

                    Metal metal1 = new Metal();
                    metal1.CodMet = i.ToString();
                    metal1.NameMet = nameMet;

                    checkBoxModel.metals = metal1;

                    if (_userMetals.Count != 0)
                    {
                        UserMetals gg = _userMetals.FirstOrDefault(x => x.CodMet == i.ToString());
                        if (gg != null)
                        {
                            checkBoxModel.Check = true;
                        }
                    }
                    PreciousMetalsSubscriptionDataGrid.Items.Add(checkBoxModel);
                    i++;
                }
            }

        }

        void DataGridCell_Loaded(object sender, RoutedEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;

            if (_setting == "Dictionaries/DarkTheme.xaml")
            {
                row.Background = new SolidColorBrush(Color.FromRgb(13, 13, 44));
                row.Foreground = new SolidColorBrush(Color.FromRgb(98, 240, 178));
            }
        }

        private static List<PreciousMetalsModel> _preciousMetalsModell = new List<PreciousMetalsModel>();
        private static List<UserMetals> _userMetals = new List<UserMetals>();

        public class CheckBoxModel
        {
            public Metal metals { get; set; }
            public bool Check { get; set; }

            public CheckBoxModel() { }
        }

        private void CheckBox_Subscription_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as CheckBox).IsChecked == true)
            {
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                Metal metal = ((sender as CheckBox).DataContext as CheckBoxModel).metals;

                using (var db = new DataBaseContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.UserId == mainWindow.UserId);
                    var met = db.Metals.FirstOrDefault(u => u.CodMet == metal.CodMet);

                    UserMetals gg = new UserMetals();
                    gg.User = user;
                    gg.UserId = user.UserId;
                    gg.CodMet = met.CodMet;
                    gg.Metal = met;

                    if (user != null)
                    {
                        db.UserMetals.Add(gg);
                    }
                    db.SaveChanges();
                }
            }
            else
            {
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                Metal metal = ((sender as CheckBox).DataContext as CheckBoxModel).metals;

                using (var db = new DataBaseContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.UserId == mainWindow.UserId);
                    var met = db.Metals.FirstOrDefault(u => u.CodMet == metal.CodMet);

                    UserMetals userMetals = db.UserMetals.Where(x => x.UserId == mainWindow.UserId
                    && x.Metal.CodMet == met.CodMet).First();

                    if (user != null)
                    {
                        db.UserMetals.Remove(userMetals);
                    }
                    db.SaveChanges();
                }
            }
        }
    }
}
