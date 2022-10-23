using DataBase.Contexts;
using DataBase.Core.Models;
using Extensions;
using FinancialCurrencyAnalyzerDesktopСlient.Enums;
using FinancialCurrencyAnalyzerDesktopСlient.Pages.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMetals
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var db = new DataBaseContext())
            {
                int i = 1;
                foreach (MetalsEnum metalEnum in Enum.GetValues(typeof(MetalsEnum)))
                {
                    Metal metal = new Metal();
                    metal.NameMet = StringEnum.GetStringValue(metalEnum);
                    metal.CodMet = i.ToString();
                    db.Metals.Add(metal);
                    db.SaveChanges();
                    i++;
                }
            }
        }
    }
}
