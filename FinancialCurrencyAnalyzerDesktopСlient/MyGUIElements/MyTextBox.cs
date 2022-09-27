using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace FinancialCurrencyAnalyzerDesktopСlient.MyGUIElements
{
    public class MyTextBox : TextBox
    {
        static MyTextBox()
        {
            TextBox.TextProperty.OverrideMetadata(typeof(MyTextBox),
                new FrameworkPropertyMetadata(string.Empty,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault |
                    FrameworkPropertyMetadataOptions.Journal,
                    null, /* property changed callback */
                    null, /* coerce value callback */
                    true, /* is animation prohibited */
                    UpdateSourceTrigger.LostFocus));
        }
    }
}
