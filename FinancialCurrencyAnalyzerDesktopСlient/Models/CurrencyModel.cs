namespace FinancialCurrencyAnalyzerDesktopСlient.Models
{
    public class CurrencyModel
    {
        public string Vname { get; set; }
        public string Vnom { get; set; }
        public string Vcurs { get; set; }
        public string Vcode { get; set; }
        public string VchCode { get; set; }

        public CurrencyModel() { }

        public CurrencyModel(string vname, string vnom, string vcurs, string vcode, string vchCode)
        {
            Vname = vname;
            Vnom = vnom;
            Vcurs = vcurs;
            Vcode = vcode;
            VchCode = vchCode;
        }
    }
}
