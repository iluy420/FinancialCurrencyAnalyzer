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
using System.Windows.Forms.DataVisualization.Charting;
using DataBase.Core.Models;
using FinancialCurrencyAnalyzerDesktopСlient.Models;
using System.Text.RegularExpressions;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;


namespace FinancialCurrencyAnalyzerDesktopСlient.Pages.Tools
{
    public partial class PriceDynamicCurrencyPage : Page
    {
        public PriceDynamicCurrencyPage()
        {
            InitializeComponent();

            Title = $"Динамика изменения курсов валют";
            NamePage.Text = Title;

            ChartArea chartArea = new ChartArea("Main");
            chartArea.AxisX.ScrollBar.Enabled = true;
            chartArea.AxisY.ScrollBar.Enabled = true;
            chartArea.AxisX.Interval = 1;
            ChartPayments.ChartAreas.Add(chartArea);

            var currentSeries = new Series("Динамика цены")
            {
                IsValueShownAsLabel = true
            };
            ChartPayments.Series.Add(currentSeries);

            ValidCurrenciesImport(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-ddT00:00:00")));
            CurrencyСodesImport();

            DateTime dateTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM"));
            DateCourse.SelectedDate = dateTime;

            foreach (var currency in _currencyСodes)
            {
                if (_ValidCurrencies.Contains(currency.VcharCode))
                {
                    Currency.Items.Add(currency.Vname.Trim() + "( наминал " + currency.Vnom + " )");
                    if (currency.VcharCode == "USD") Currency.SelectedValue
                             = currency.Vname.Trim() + "( наминал " + currency.Vnom + " )";
                }
            }
        }

        private  List<CurrencyDynamicModel> _currencyDynamic = new List<CurrencyDynamicModel>();
        private  List<ReferenceCurrencyСodes> _currencyСodes = new List<ReferenceCurrencyСodes>();
        private static List<string> _ValidCurrencies = new List<string>();
        private ReferenceCurrencyСodes _selectCurrency { get; set; }

        /// <summary>
        /// получение курсов валюты
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="VcommonCode"></param>
        private void GetCursDynamicXMLImport(DateTime dateFrom, DateTime dateTo, string VcommonCode)
        {
            _currencyDynamic.Clear();



            СentralBankApi.ApiCB.DailyInfoSoapClient client = new СentralBankApi.ApiCB.DailyInfoSoapClient("DailyInfoSoap");
            XmlNode doc = client.GetCursDynamicXML(Convert.ToDateTime(dateFrom.ToString("yyyy-MM-ddT00:00:00"))
                , Convert.ToDateTime(dateTo.ToString("yyyy-MM-ddT00:00:00")), VcommonCode);

            foreach (XmlNode xmlNode in doc)
            {
                CurrencyDynamicModel urrencyDynamic = new CurrencyDynamicModel();
                foreach (XmlNode xmlNode1 in xmlNode.ChildNodes)
                {
                    if (xmlNode1.Name == "Vcode") urrencyDynamic.Vcode = xmlNode1.InnerText;
                    if (xmlNode1.Name == "Vcurs") urrencyDynamic.Vcurs = xmlNode1.InnerText;
                    if (xmlNode1.Name == "CursDate") urrencyDynamic.CursDate = xmlNode1.InnerText;
                    if (xmlNode1.Name == "Vnom") urrencyDynamic.Vnom = xmlNode1.InnerText;
                }
                _currencyDynamic.Add(urrencyDynamic);
            }
        }

        /// <summary>
        /// полкучение внутренних кодов валют
        /// </summary>
        private void CurrencyСodesImport()
        {
            _currencyСodes.Clear();
            СentralBankApi.ApiCB.DailyInfoSoapClient client = new СentralBankApi.ApiCB.DailyInfoSoapClient("DailyInfoSoap");
            XmlNode doc = client.EnumValutesXML(false);

            foreach (XmlNode xmlNode in doc)
            {
                ReferenceCurrencyСodes currencyСodes = new ReferenceCurrencyСodes();
                foreach (XmlNode xmlNode1 in xmlNode.ChildNodes)
                {
                    if (xmlNode1.Name == "Vcode") currencyСodes.Vcode = xmlNode1.InnerText;
                    if (xmlNode1.Name == "Vname") currencyСodes.Vname = xmlNode1.InnerText.Trim();
                    if (xmlNode1.Name == "VEngname") currencyСodes.VEngname = xmlNode1.InnerText;
                    if (xmlNode1.Name == "Vnom") currencyСodes.Vnom = xmlNode1.InnerText;
                    if (xmlNode1.Name == "VcommonCode") currencyСodes.VcommonCode = xmlNode1.InnerText;
                    if (xmlNode1.Name == "VnumCode") currencyСodes.VnumCode = xmlNode1.InnerText;
                    if (xmlNode1.Name == "VcharCode") currencyСodes.VcharCode = xmlNode1.InnerText;
                }
                _currencyСodes.Add(currencyСodes);
            }
        }

        private void UpdateChart()
        {
            DateTime selectedDate = (DateTime)DateCourse.SelectedDate;
            int days = DateTime.DaysInMonth(selectedDate.Year, selectedDate.Month);

            DateTime dateTo = selectedDate;
            dateTo = dateTo.AddDays(days - 1);
            DateTime dateFrom = selectedDate;

            string VcommonCode ="";
            if (_selectCurrency != null)
            {
                VcommonCode = _selectCurrency.VcommonCode;
            }

            GetCursDynamicXMLImport(dateFrom, dateTo, VcommonCode);

            Series currentSeries = ChartPayments.Series.FirstOrDefault();
            currentSeries.ChartType = SeriesChartType.Line;
            currentSeries.Points.Clear();
            currentSeries.MarkerStyle = MarkerStyle.Circle;
            if (_currencyDynamic.Count() == 0 && VcommonCode != "")
            {
                MessageBox.Show("Данные не найдены!");
            }
            foreach (var currency in _currencyDynamic)
            {
                DataPoint point = new DataPoint();
                DateTime date = Convert.ToDateTime(currency.CursDate);
                point.SetValueXY(date.ToString("dd-MM-yyyy"), currency.Vcurs);
                currentSeries.Points.Add(point);
            }
        }

        private void DateCourse_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateChart();
        }

        private void Currency_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string fullName = Currency.SelectedValue.ToString();
            int start = fullName.IndexOf("(");
            string name = fullName.Remove(start);

            _selectCurrency = (ReferenceCurrencyСodes)_currencyСodes
                .Where(x => x.Vname == name).First();
            UpdateChart();
        }

        private void ValidCurrenciesImport(DateTime date)
        {
            _ValidCurrencies.Clear();

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
                    if (xmlNode1.Name == "VchCode") currency.VchCode = xmlNode1.InnerText.Trim();
                }
                _ValidCurrencies.Add(currency.VchCode);
            }
        }
    }
}
