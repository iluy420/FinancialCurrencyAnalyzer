using Extensions;
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
using FinancialCurrencyAnalyzerDesktopСlient.Models;
using System.Runtime.ConstrainedExecution;
using System.IO;

namespace FinancialCurrencyAnalyzerDesktopСlient.Pages.Tools
{
    public partial class Converter : Page
    {
        public Converter(CurrencyModel currentCurrency, DateTime date)
        {
            InitializeComponent();

            string setting = "";
            if (File.Exists("../../UserSettings/UserThemeSettings.txt"))
            {
                using (FileStream fs = new FileStream("../../UserSettings/UserThemeSettings.txt", FileMode.OpenOrCreate, FileAccess.Read))
                {
                    StreamReader reader = new StreamReader(fs);

                    setting = reader.ReadLine();
                }
            }
            if (setting == "Dictionaries/DarkTheme.xaml")
            {
                IconConventer.Foreground = new SolidColorBrush(Color.FromRgb(98, 240, 178));
            }

            _currentCurrency = currentCurrency;
            _date = date;

            Title = $"Конвертер \"Российский рубль\" к \"{_currentCurrency.Vname}\"";
            NamePage.Text = Title;
            DateCourse.Text = date.ToString("dd MMMM yyyy");

            LabelCurrencyFrom.Content = "Российский рубль";
            LabelCurrencyTo.Content = $"{_currentCurrency.Vname} нонинал {_currentCurrency.Vnom}";

            DataContext = _currentCurrency;
        }

        private CurrencyModel _currentCurrency { get; set; }
        
        private DateTime _date { get; set; }

        /// <summary>
        /// Отмена неверных символов введенных пользователем
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelingInvalidCharacters(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                if (e.Text != "," || ((TextBox)sender).Text.Contains(","))
                {
                    e.Handled = true;
                }
            }
        }

        private void CurrencyFrom_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsNullString(CurrencyFrom.Text);

            if (Converters.IsConvertToDouble(CurrencyFrom.Text))
            {
                double course = Convert.ToDouble(_currentCurrency.Vcurs.Replace(".", ","));
                double quantity = Convert.ToDouble(CurrencyFrom.Text);

                double result = quantity / course;

                CurrencyTo.Text = result.ToString();
            }
        }

        private void CurrencyTo_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsNullString(CurrencyTo.Text);

            if (Converters.IsConvertToDouble(CurrencyTo.Text))
            {
                double course = Convert.ToDouble(_currentCurrency.Vcurs.Replace(".", ","));
                double quantity = Convert.ToDouble(CurrencyTo.Text);

                double result = quantity * course;

                CurrencyFrom.Text = result.ToString();
            }
        }
        
        /// <summary>
        /// Если ввели пустое значение в textbox
        /// </summary>
        /// <param name="text"></param>
        private void IsNullString(string text)
        {
            if(text == "")
            {
                CurrencyTo.Text = "0";
                CurrencyFrom.Text = CurrencyTo.Text;
            }
        }
    }
}
