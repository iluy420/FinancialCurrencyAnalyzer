using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialCurrencyAnalyzerDesktopСlient.Models
{
    public class CurrencyDynamicModel
    {
        public string CursDate { get; set; }
        public string Vcode { get; set; }
        public string Vnom { get; set; }
        public string Vcurs { get; set; }

        public CurrencyDynamicModel() { }

        public CurrencyDynamicModel(string CursDate, string Vcode, string Vnom, string Vcurs)
        {
            this.CursDate = CursDate;
            this.Vcode = Vcode;
            this.Vnom = Vnom;
            this.Vcode = Vcurs;
        }
    }
}
