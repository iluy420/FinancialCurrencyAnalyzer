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

namespace FinancialCurrencyAnalyzerDesktopСlient.Pages.Tools
{
    public partial class Currency : Page
    {
        public Currency()
        {
            InitializeComponent();

            Title = $"Просмотр курсов валют";
            NamePage.Text = Title;

            DateCourse.SelectedDate = DateTime.Now;
        }

        private static List<CurrencyModel> _currencies = new List<CurrencyModel>();

        private void CurrenciesImport(DateTime date)
        {
            if (DateCourse.SelectedDate <= DateTime.Now)
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
                        if (xmlNode1.Name == "Vnom") currency.Vnom = xmlNode1.InnerText;
                        if (xmlNode1.Name == "Vcurs") currency.Vcurs = xmlNode1.InnerText;
                        if (xmlNode1.Name == "Vcode") currency.Vcode = xmlNode1.InnerText;
                        if (xmlNode1.Name == "VchCode") currency.VchCode = xmlNode1.InnerText;
                    }
                    _currencies.Add(currency);
                    CurrencyDataGrid.Items.Add(currency);
                }
            }else CurrencyDataGrid.Items.Clear();
        }

        private void ButtonConvert_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Converter(((sender as Button).DataContext as CurrencyModel)
                , (DateTime)DateCourse.SelectedDate));
        }

        private void DateСourse_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrenciesImport((DateTime)DateCourse.SelectedDate);
        }
    }
}
