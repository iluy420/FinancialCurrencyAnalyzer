using DataBase.Core.Models;
using FinancialCurrencyAnalyzerDesktopСlient.Models;
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
using СentralBankApi;
using Extensions;
using FinancialCurrencyAnalyzerDesktopСlient.Properties;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace FinancialCurrencyAnalyzerDesktopСlient.Pages.Tools
{
    public partial class Currency : Page
    {
        private string _setting { get; set; }

        public Currency()
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

            Title = $"Просмотр курсов валют";
            NamePage.Text = Title;
            //исправляем мифический баг ////////////////////////
            CurrencyModel currency = new CurrencyModel("vname", "vnom", "vcurs", "vcode", "vchcode");
            CurrencyDataGrid.Items.Add(currency);
            CurrencyDataGrid.Items.Clear();
            ////////////////////////////////////////////////////
            if (DateTime.Now.DayOfWeek != DayOfWeek.Monday && DateTime.Now.DayOfWeek != DayOfWeek.Sunday)
            {
                DateCourse.SelectedDate = DateTime.Now;
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

        private static List<CurrencyModel> _currencies = new List<CurrencyModel>();

        private void CurrenciesImport(DateTime date)
        {
            if (date <= DateTime.Now)
            {
                if(date.DayOfWeek == DayOfWeek.Monday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    MessageBox.Show("В воскресенье и понедельник курсы не котируются см. курс на субботу");
                    CurrencyDataGrid.Items.Clear();
                }
                else
                {
                    CurrencyDataGrid.Items.Clear();
                    _currencies.Clear();

                    СentralBankApi.ApiCB.DailyInfoSoapClient client = new СentralBankApi.ApiCB.DailyInfoSoapClient("DailyInfoSoap");
                    XmlNode doc = client.GetCursOnDateXML(Convert.ToDateTime(date.ToString("yyyy-MM-ddT00:00:00")));

                    foreach (XmlNode xmlNode in doc)
                    {
                        CurrencyModel currency = new CurrencyModel();
                        foreach (XmlNode xmlNode1 in xmlNode.ChildNodes)
                        {
                            if (xmlNode1.Name == "Vname") currency.Vname = xmlNode1.InnerText.Trim();
                            if (xmlNode1.Name == "Vnom") currency.Vnom = xmlNode1.InnerText.Trim();
                            if (xmlNode1.Name == "Vcurs") {
                                double curs = Convert.ToDouble(xmlNode1.InnerText.Trim().Replace('.',','));
                                currency.Vcurs = string.Format("{0:f2}", Math.Round(curs, 2));
                            } 
                            if (xmlNode1.Name == "Vcode") currency.Vcode = xmlNode1.InnerText.Trim();
                            if (xmlNode1.Name == "VchCode") currency.VchCode = xmlNode1.InnerText.Trim();
                        }
                        _currencies.Add(currency);
                        CurrencyDataGrid.Items.Add(currency);
                    }
                }
            }else CurrencyDataGrid.Items.Clear();
        }

        private void ButtonConvert_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Converter(((sender as Button).DataContext as CurrencyModel)
                , (DateTime)DateCourse.SelectedDate));
        }

        private void DateСourse_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrenciesImport((DateTime)DateCourse.SelectedDate);
        }
    }
}
