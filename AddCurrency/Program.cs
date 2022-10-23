using DataBase.Contexts;
using DataBase.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace AddCurrency
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CurrencyСodesImport();
            using (var db = new DataBaseContext())
            {
                foreach(ReferenceCurrencyСodes currencyVar in _currencyСodes)
                {
                    DataBase.Core.Models.Сurrency сurrency = new DataBase.Core.Models.Сurrency();
                    сurrency.Vcode = currencyVar.Vcode.Trim();
                    сurrency.Vname = currencyVar.Vname.Trim();
                    сurrency.VEngname = currencyVar.VEngname.Trim();
                    сurrency.Vnom = currencyVar.Vnom.Trim();
                    сurrency.VcommonCode = currencyVar.VcommonCode.Trim();
                    if (currencyVar.VnumCode != null)
                    {
                        сurrency.VnumCode = currencyVar.VnumCode.Trim();
                    }
                    if (currencyVar.VcharCode != null)
                    {
                        сurrency.VcharCode = currencyVar.VcharCode.Trim();
                    }
                    

                    db.Сurrencys.Add(сurrency);
                    db.SaveChanges();
                }
            }
        }

        private static List<ReferenceCurrencyСodes> _currencyСodes = new List<ReferenceCurrencyСodes>();

        private static void CurrencyСodesImport()
        {
            _currencyСodes.Clear();
            СentralBankApi.ApiCB.DailyInfoSoapClient client = new СentralBankApi.ApiCB.DailyInfoSoapClient("DailyInfoSoap");
            XmlNode doc = client.EnumValutesXML(false);

            foreach (XmlNode xmlNode in doc)
            {
                ReferenceCurrencyСodes currencyСodes = new ReferenceCurrencyСodes();
                foreach (XmlNode xmlNode1 in xmlNode.ChildNodes)
                {
                    if (xmlNode1.Name == "Vcode") currencyСodes.Vcode = xmlNode1.InnerText;
                    if (xmlNode1.Name == "Vname") currencyСodes.Vname = xmlNode1.InnerText.Trim();
                    if (xmlNode1.Name == "VEngname") currencyСodes.VEngname = xmlNode1.InnerText;
                    if (xmlNode1.Name == "Vnom") currencyСodes.Vnom = xmlNode1.InnerText;
                    if (xmlNode1.Name == "VcommonCode") currencyСodes.VcommonCode = xmlNode1.InnerText;
                    if (xmlNode1.Name == "VnumCode") currencyСodes.VnumCode = xmlNode1.InnerText;
                    if (xmlNode1.Name == "VcharCode") currencyСodes.VcharCode = xmlNode1.InnerText;
                }
                _currencyСodes.Add(currencyСodes);
            }
        }
    }

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
            string VEngname, string Vnom, string VcommonCode, string VnumCode, string VcharCode)
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
