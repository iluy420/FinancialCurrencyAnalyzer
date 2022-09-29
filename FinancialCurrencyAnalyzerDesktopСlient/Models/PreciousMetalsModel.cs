namespace FinancialCurrencyAnalyzerDesktopСlient.Models
{
    public class PreciousMetalsModel
    {
        public string DateMet { get; set; }
        public string CodMet { get; set; }
        public string Price { get; set; }

        public PreciousMetalsModel() { }

        public PreciousMetalsModel(string dateMet, string codMet, string price)
        {
            DateMet = dateMet;
            CodMet = codMet;
            Price = price;
        }
    }
}
