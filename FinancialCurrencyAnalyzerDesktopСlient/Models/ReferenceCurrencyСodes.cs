using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialCurrencyAnalyzerDesktopСlient.Models
{
    public class ReferenceCurrencyСodes
    {
        public string Vcode { get; set; }
        public string Vname { get; set; }
        public string VEngname { get; set; }
        public string Vnom { get; set; }
        public string VcommonCode { get; set; }
        public string VnumCode { get; set; }
        public string VcharCode { get; set; }

        public ReferenceCurrencyСodes() { }

        public ReferenceCurrencyСodes(string Vcode, string Vname,
            string VEngname, string Vnom, string VcommonCode, string VnumCode,string VcharCode)
        {
            this.Vcode = Vcode;
            this.Vname = Vname;
            this.VEngname = VEngname;
            this.Vnom = Vnom;
            this.VcommonCode = VcommonCode;
            this.VnumCode = VnumCode;
            this.VcharCode = VcharCode;
        }
    }
}
