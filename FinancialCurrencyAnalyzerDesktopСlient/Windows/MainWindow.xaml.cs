using FinancialCurrencyAnalyzerDesktopСlient.Pages.Tools;
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
using System.Windows.Threading;

namespace FinancialCurrencyAnalyzerDesktopСlient.Windows
{ 
    public partial class MainWindow : Window
    {
        public Guid UserId { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += TimerTick;
            timer.Start();
        }

        private void WindowsClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Вы уверены?", "Выход"
                , MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                e.Cancel = true;
            }
        }

        private void TimerTick(object sender, EventArgs e)
        {
            DateTimeNow.Text = DateTime.Now.ToString("dd MMMM yyyy HH:mm:ss");
        }

        private void MainFrame_OnNavigeted(object sender, NavigationEventArgs e)
        {
            if (!(e.Content is Page page)) return;
            //настройки заголовка окна
            Title = $"FinancialCurrencyAnalyzer - {page.Title}";

            //настройки меню
            if (page is Pages.RegistrationAndLogin.Login
                || page is Pages.RegistrationAndLogin.EmailConfirmation
                || page is Pages.RegistrationAndLogin.RegistrationUser)
            {
                MenuProgramm.Visibility = Visibility.Hidden;
            }
            else MenuProgramm.Visibility = Visibility.Visible;

            //настройки кнопки назад
            if (page is Pages.RegistrationAndLogin.EmailConfirmation
                || page is Pages.RegistrationAndLogin.RegistrationUser)
            {
                Backbtn.Visibility = Visibility.Visible;
            }
            else Backbtn.Visibility = Visibility.Hidden;

            //настройки размера окна
            if (page is Pages.RegistrationAndLogin.Login) this.WindowState = WindowState.Normal;
            else this.WindowState = WindowState.Maximized;

        }

        private void PreciousMetals_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new PreciousMetals());
        }

        private void Currency_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Currency());
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack) MainFrame.GoBack();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PriceDynamicPreciousMetals_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new PriceDynamiPreciousMetalsPage());
        }

        private void PriceDynamicCurrency_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new PriceDynamicCurrencyPage());
        }

        private void SubscriptionForecastPricePreciousMetals_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new PreciousMetalsSubscription());
        }

        private void SubscriptionForecastPriceCurrency_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new CurrencySubscription());
        }
    }
}
