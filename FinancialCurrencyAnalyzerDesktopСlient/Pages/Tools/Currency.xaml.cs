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

namespace FinancialCurrencyAnalyzerDesktopСlient.Pages.Tools
{
    public partial class Currency : Page
    {
        public Currency()
        {
            InitializeComponent();

            CurrenciesImport();
        }

        private void CurrenciesImport()
        {
            СentralBankApi.ApiCB.DailyInfoSoapClient client = new СentralBankApi.ApiCB.DailyInfoSoapClient("DailyInfoSoap");
            XmlNode doc = client.GetCursOnDateXML(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-ddT00:00:00")));

            foreach (XmlNode xmlNode in doc)
            {
                CurrencyModel currency = new CurrencyModel();
                foreach (XmlNode xmlNode1 in xmlNode.ChildNodes)
                {
                    if (xmlNode1.Name == "Vname") currency.Vname = xmlNode1.InnerText;
                    if (xmlNode1.Name == "Vnom") currency.Vnom = xmlNode1.InnerText;
                    if (xmlNode1.Name == "Vcurs") currency.Vcurs = xmlNode1.InnerText;
                    if (xmlNode1.Name == "Vcode") currency.Vcode = xmlNode1.InnerText;
                    if (xmlNode1.Name == "VchCode") currency.VchCode = xmlNode1.InnerText;
                }
                CurrencyDataGrid.Items.Add(currency);
            }
        }
    }
}
