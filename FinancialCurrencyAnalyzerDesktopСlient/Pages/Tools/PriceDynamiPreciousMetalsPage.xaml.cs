using DataBase.Core.Models;
using Extensions;
using FinancialCurrencyAnalyzerDesktopСlient.Enums;
using FinancialCurrencyAnalyzerDesktopСlient.Models;
using FinancialCurrencyAnalyzerDesktopСlient.Properties;
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
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace FinancialCurrencyAnalyzerDesktopСlient.Pages.Tools
{
    public partial class PriceDynamiPreciousMetalsPage : Page
    {
        private string _setting { get; set; }

        public PriceDynamiPreciousMetalsPage()
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

            ChartArea chartArea = new ChartArea("Main");
            chartArea.AxisX.ScrollBar.Enabled = true;
            chartArea.AxisY.ScrollBar.Enabled = true;
            chartArea.AxisX.Interval = 1;
            chartArea.AxisX.MajorGrid.LineColor = System.Drawing.Color.Gold;
            chartArea.AxisY.MajorGrid.LineColor = System.Drawing.Color.Gold;
            chartArea.AxisX.MajorGrid.Enabled = true;
            chartArea.AxisY.MajorGrid.Enabled = true;
            chartArea.BackColor = System.Drawing.Color.Gray;
            ChartTab.ChartAreas.Add(chartArea);

            var currentSeries = new Series("Динамика цены")
            {
                IsValueShownAsLabel = true
            };
            ChartTab.Series.Add(currentSeries);

            Title = $"Динамика изменения курсов драгоценных металлов";
            NamePage.Text = Title;

            DateTime dateTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM"));
            DateCourse.SelectedDate = dateTime;

            foreach (MetalsEnum metal in Enum.GetValues(typeof(MetalsEnum)))
            {
                PreciousMetals.Items.Add(StringEnum.GetStringValue(metal));
            }
            PreciousMetals.SelectedValue = StringEnum.GetStringValue(MetalsEnum.Gold);

            if (_setting == "Dictionaries/DarkTheme.xaml")
            {

                ChartTab.BackColor = System.Drawing.Color.FromArgb(13, 13, 44);
                chartArea.AxisX.LabelStyle.ForeColor = System.Drawing.Color.FromArgb(98, 240, 178);
                chartArea.AxisY.LabelStyle.ForeColor = System.Drawing.Color.FromArgb(98, 240, 178);
                chartArea.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(98, 240, 178);
                chartArea.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(98, 240, 178);
                chartArea.AxisY.MajorTickMark.LineColor = System.Drawing.Color.FromArgb(98, 240, 178);
                chartArea.AxisX.MajorTickMark.LineColor = System.Drawing.Color.FromArgb(98, 240, 178);
            }
        }

        private string _selectMetal { get; set; }
        private List<PreciousMetalsModel> _preciousMetalsDynamic = new List<PreciousMetalsModel>();

        private void PreciousMetals_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (MetalsEnum metal in Enum.GetValues(typeof(MetalsEnum)))
            {
                if (PreciousMetals.SelectedValue.ToString() == StringEnum.GetStringValue(metal))
                {
                    switch (metal)
                    {
                        case MetalsEnum.Gold:
                            _selectMetal = "1";
                            break;
                        case MetalsEnum.Silver:
                            _selectMetal = "2";
                            break;
                        case MetalsEnum.Platinum:
                            _selectMetal = "3";
                            break;
                        case MetalsEnum.Palladium:
                            _selectMetal = "4";
                            break;
                    }
                }
            }

            UpdateChart();
        }

        private void DateCourse_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateChart();
        }

        private void UpdateChart()
        {
            DateTime selectedDate = (DateTime)DateCourse.SelectedDate;
            int days = DateTime.DaysInMonth(selectedDate.Year, selectedDate.Month);

            DateTime dateTo = selectedDate;
            dateTo = dateTo.AddDays(days - 1);
            DateTime dateFrom = selectedDate;

            СentralBankApi.ApiCB.DailyInfoSoapClient client = new СentralBankApi.ApiCB.DailyInfoSoapClient("DailyInfoSoap");
            XmlNode doc = client.DragMetDynamicXML(dateFrom, dateTo);
            _preciousMetalsDynamic.Clear();
            foreach (XmlNode xmlNode in doc)
            {
                PreciousMetalsModel preciousMetal = new PreciousMetalsModel();
                foreach (XmlNode xmlNode1 in xmlNode.ChildNodes)
                {
                    if (xmlNode1.Name == "DateMet") preciousMetal.DateMet = Convert.ToDateTime(xmlNode1.InnerText).ToString("dd.MM.yyyy");
                    if (xmlNode1.Name == "CodMet") preciousMetal.CodMet = xmlNode1.InnerText;
                    if (xmlNode1.Name == "price") preciousMetal.Price = xmlNode1.InnerText;
                }
                _preciousMetalsDynamic.Add(preciousMetal);
            }

            Series currentSeries = ChartTab.Series.FirstOrDefault();
            currentSeries.ChartType = SeriesChartType.Line;
            currentSeries.Points.Clear();
            currentSeries.MarkerStyle = MarkerStyle.Circle;
            currentSeries.MarkerColor = System.Drawing.Color.Red;

            if (_preciousMetalsDynamic.Count() == 0 && _selectMetal != "")
            {
                MessageBox.Show("Данные не найдены!");
            }
            foreach (var metal in _preciousMetalsDynamic)
            {
                if(metal.CodMet == _selectMetal)
                {
                    DataPoint point = new DataPoint();
                    DateTime date = Convert.ToDateTime(metal.DateMet);
                    point.SetValueXY(date.ToString("dd-MM-yyyy"), metal.Price);
                    currentSeries.Points.Add(point);
                }
            }
        }
    }
}
