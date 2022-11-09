using Extensions;
using FinancialCurrencyAnalyzerDesktopСlient.Enums;
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

namespace FinancialCurrencyAnalyzerDesktopСlient.Pages.Tools
{
    public partial class ForecastPreciousMetals : Page
    {
        public ForecastPreciousMetals()
        {
            InitializeComponent();

            Title = $"Прогнозы на курсы металлов";
            NamePage.Text = Title;

            PreciousMetalsImport();
        }

        private void PreciousMetalsImport()
        {
            ForecastPreciousMetalsDataGrid.Items.Clear();

            DateTime dateTo = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            DateTime dateFrom = Convert.ToDateTime(dateTo.AddDays(-90).ToString("yyyy-MM-dd"));

            int businessDays = BusinessDays(dateFrom, dateTo);

            _xPoint.Clear();

            for (int i = 1; i <= businessDays; i++)
            {
                _xPoint.Add(Convert.ToDouble(i));
            }

            СentralBankApi.ApiCB.DailyInfoSoapClient client = new СentralBankApi.ApiCB.DailyInfoSoapClient("DailyInfoSoap");
            DateTime dateTimeNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-ddT00:00:00"));
            XmlNode doc = client.DragMetDynamicXML(dateFrom, dateTo);

            foreach (MetalsEnum metal in Enum.GetValues(typeof(MetalsEnum)))
            {
                TableModel preciousMetal = new TableModel();
                preciousMetal.MetName = StringEnum.GetStringValue(metal);

                _MetCurs.Clear();
                string CodMet = Convert.ToString((int)metal);

                foreach (XmlNode xmlNode in doc)
                {
                    if (xmlNode.ChildNodes[1].Name == "CodMet"
                            && xmlNode.ChildNodes[1].InnerText.Trim() == CodMet)
                    {
                        _MetCurs.Add(Convert.ToDouble(xmlNode.ChildNodes[2].InnerText.Trim().Replace('.',',')));
                    }
                }

                double curs = Convert.ToDouble(_MetCurs[_MetCurs.Count - 1].ToString().Replace('.', ','));
                preciousMetal.Vcurs = string.Format("{0:f2}", Math.Round(curs, 2));

                preciousMetal.VForecastCurs = string.Format("{0:f2}", Math.Round(Forecast(_xPoint.ToArray(), _MetCurs.ToArray()
                        , Convert.ToDouble(_xPoint.Count() + 1)), 2));


                ForecastPreciousMetalsDataGrid.Items.Add(preciousMetal);
            }
        }

        private List<double> _xPoint = new List<double>();
        private static List<double> _MetCurs = new List<double>();

        public class TableModel
        {
            public string MetName { get; set; }
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
                row.Background = Brushes.IndianRed;
            }
            else
            {
                row.Background = Brushes.Green;
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
