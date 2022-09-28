using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace FinancialCurrencyAnalyzerDesktopСlient.Extensions
{
    public class NavigatorExtensions
    {
        /// <summary>
        /// Удалить все записи в навигаторе
        /// </summary>
        public static void RemoveAllEntry(NavigationService navigationService)
        {
            while (navigationService.CanGoBack)
            {
                navigationService.RemoveBackEntry();
            }
        }
    }
}
