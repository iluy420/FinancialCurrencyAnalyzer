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
using FinancialCurrencyAnalyzerDesktopСlient.Enums;
using Extensions;

namespace FinancialCurrencyAnalyzerDesktopСlient.Pages.Tools
{
    public partial class PreciousMetals : Page
    {
        public PreciousMetals()
        {
            InitializeComponent();

            Title = $"Просмотр курсов драгоценных металлов";
            NamePage.Text = Title;

            if(DateTime.Now.DayOfWeek != DayOfWeek.Monday && DateTime.Now.DayOfWeek != DayOfWeek.Sunday)
            {
                DateCourse.SelectedDate = DateTime.Now;
            }
        }

        private void DateСourse_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            PreciousMetalsImport((DateTime)DateCourse.SelectedDate);
        }

        private void PreciousMetalsImport(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Monday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                MessageBox.Show("В воскресенье и понедельник курсы не котируются см. курс на субботу");
                PreciousMetalsDataGrid.Items.Clear();
            }
            else
            {
                PreciousMetalsDataGrid.Items.Clear();

                СentralBankApi.ApiCB.DailyInfoSoapClient client = new СentralBankApi.ApiCB.DailyInfoSoapClient("DailyInfoSoap");
                DateTime dateTimeNow = Convert.ToDateTime(date.ToString("yyyy-MM-ddT00:00:00"));
                XmlNode doc = client.DragMetDynamicXML(dateTimeNow, dateTimeNow);

                foreach (XmlNode xmlNode in doc)
                {
                    PreciousMetalsModel preciousMetal = new PreciousMetalsModel();
                    foreach (XmlNode xmlNode1 in xmlNode.ChildNodes)
                    {
                        if (xmlNode1.Name == "DateMet") preciousMetal.DateMet = Convert.ToDateTime(xmlNode1.InnerText).ToString("dd.MM.yyyy");
                        if (xmlNode1.Name == "CodMet")
                        {
                            switch (xmlNode1.InnerText)
                            {
                                case "1":
                                    preciousMetal.CodMet = StringEnum.GetStringValue(MetalsEnum.Gold);
                                    break;
                                case "2":
                                    preciousMetal.CodMet = StringEnum.GetStringValue(MetalsEnum.Silver);
                                    break;
                                case "3":
                                    preciousMetal.CodMet = StringEnum.GetStringValue(MetalsEnum.Platinum);
                                    break;
                                case "4":
                                    preciousMetal.CodMet = StringEnum.GetStringValue(MetalsEnum.Palladium);
                                    break;
                                default:
                                    preciousMetal.CodMet = "Не известный металл";
                                    break;
                            }
                        }
                        if (xmlNode1.Name == "price") preciousMetal.Price = xmlNode1.InnerText;
                    }
                    PreciousMetalsDataGrid.Items.Add(preciousMetal);
                }
            }
        }
    }
}
