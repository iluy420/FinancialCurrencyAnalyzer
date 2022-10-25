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
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace FinancialCurrencyAnalyzerDesktopСlient.Pages.Tools
{
    public partial class ForecastCurrency : Page
    {
        public ForecastCurrency()
        {
            InitializeComponent();

            Title = $"Прогнозы на курсы валют";
            NamePage.Text = Title;

            ValidCurrenciesImport(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-ddT00:00:00")));
            CurrencyСodesImport();
            UpdateTable();
        }

        private List<CurrencyDynamicModel> _currencyDynamic = new List<CurrencyDynamicModel>();
        private static List<string> _ValidCurrenciesСodes = new List<string>();
        private List<ReferenceCurrencyСodes> _currencyСodes = new List<ReferenceCurrencyСodes>();
        private List<double> _currencyСourses = new List<double>();
        private List<double> _xPoint = new List<double>();

        private void ValidCurrenciesImport(DateTime date)
        {
            _ValidCurrenciesСodes.Clear();

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
                _ValidCurrenciesСodes.Add(currency.VchCode);
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

        private void UpdateTable()
        {
            DateTime dateTo = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            DateTime dateFrom = Convert.ToDateTime(dateTo.AddDays(-90).ToString("yyyy-MM-dd"));
 
             int businessDays = BusinessDays(dateFrom, dateTo);

            _xPoint.Clear();

            for(int i = 1;i<= businessDays; i++)
            {
                _xPoint.Add(Convert.ToDouble(i));
            }

            foreach (var currency in _currencyСodes)
            {
                if (_ValidCurrenciesСodes.Contains(currency.VcharCode))
                {
                    GetCursDynamicXMLImport(dateFrom, dateTo, currency.VcommonCode);
                    _currencyСourses.Clear();

                    foreach (var courses in _currencyDynamic)
                    {
                        _currencyСourses.Add(Convert.ToDouble(courses.Vcurs.Replace('.', ','))); 
                    }

                    TableModel row = new TableModel();

                    row.Vname = currency.Vname;
                    row.Vcurs = _currencyСourses.ElementAt(_currencyСourses.Count() - 1).ToString();

                    var forecast = Forecast(_xPoint.ToArray(), _currencyСourses.ToArray()
                        , Convert.ToDouble(_xPoint.Count() + 1));

                    row.VForecastCurs = forecast.ToString();
                    
                    ForecastCurrencyDataGrid.Items.Add(row);
                }
            }

        }

        public class TableModel
        {
            public string Vname { get; set; }
            public string Vcurs { get; set; }
            public string VForecastCurs { get; set; }
        }

        static double Forecast(double[] xValues, double[] yValues, double forecastPoint)
        {
            var xAverage = xValues.Average();
            var yAverage = yValues.Average();

            var bounds = yValues
                .Select((y, i) => new { Value = y, Index = i })
                .Aggregate(new { Top = 0.0, Bottom = 0.0 }, (acc, cur) =>
                    new
                    {
                        Top = acc.Top + (xValues[cur.Index] - xAverage) * (yValues[cur.Index] - yAverage),
                        Bottom = acc.Bottom + Math.Pow(xValues[cur.Index] - xAverage, 2.0)
                    });

            var level = bounds.Top / bounds.Bottom;

            return (yAverage - level * xAverage) + level * forecastPoint;
        }

        void DataGridCell_Loaded(object sender, RoutedEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            TableModel obj = (TableModel)row.DataContext;
           
            if (Convert.ToDouble(obj.Vcurs) <= Convert.ToDouble(obj.VForecastCurs))
            {
                row.Background = Brushes.Green;
            }
            else
            {
                row.Background = Brushes.IndianRed;
            }
        }

        private static int BusinessDays(DateTime first, DateTime last)
        {
            var count = 0;

            while (first.Date != last.Date)
            {
                if (first.DayOfWeek != DayOfWeek.Monday && first.DayOfWeek != DayOfWeek.Sunday)
                    count++;

                first = first.AddDays(1);
            }
            if (first.DayOfWeek != DayOfWeek.Monday && first.DayOfWeek != DayOfWeek.Sunday)
                count++;

            return count;
        }
    }
}
