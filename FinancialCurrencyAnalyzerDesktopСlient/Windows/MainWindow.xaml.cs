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

        private void BackClick(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack) MainFrame.GoBack();
        }

        private void MainFrame_OnNavigeted(object sender, NavigationEventArgs e)
        {
            if (!(e.Content is Page page)) return;
            Title = $"FinancialCurrencyAnalyzer - {page.Title}";

            if (page is Pages.Login) Back.Visibility = Visibility.Hidden;
            else Back.Visibility = Visibility.Visible;
        }
    }
}
