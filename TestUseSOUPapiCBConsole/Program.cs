using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TestUseSOUPapiCBConsole.ServiceReferenceApiCB;

namespace TestUseSOUPapiCBConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceReferenceApiCB.DailyInfoSoapClient client = new ServiceReferenceApiCB.DailyInfoSoapClient("DailyInfoSoap");

            //XmlElement doc = new XmlElement();
            //doc = (XmlElement)client.GetCursOnDateXML(DateTime.Now);
            DateTime dateTimeNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-ddT00:00:00"));

            XmlNode doc = client.GetCursDynamicXML(dateTimeNow, dateTimeNow, "36");
            //Console.WriteLine(DateTime.Now.ToString("yyyy-MM-ddT00:00:00"));
            //Console.WriteLine(doc.OuterXml);
            //Console.WriteLine(doc.ChildNodes.Item(0).OuterXml);
            Console.WriteLine(doc.ChildNodes);
            //Console.WriteLine(doc.ChildNodes.Item(0).ChildNodes.Item(0).ChildNodes.Item(0).Value);
            //Console.WriteLine(doc.ParentNode["ValuteCursOnDate"].OuterXml);
            
            //foreach(XmlNode xmlNode in doc)
            //{
            //    foreach(XmlNode xmlNode1 in xmlNode.ChildNodes)
            //    {
            //        if (xmlNode1.Name == "Vname") Console.Write($"Vname : {xmlNode1.InnerText} \t"); 
            //        if (xmlNode1.Name == "Vnom") Console.Write($"Vnom : {xmlNode1.InnerText} \t"); 
            //        if (xmlNode1.Name == "Vcurs") Console.Write($"Vcurs : {xmlNode1.InnerText} \t"); 
            //        if (xmlNode1.Name == "Vcode") Console.Write($"Vcode : {xmlNode1.InnerText} \t"); 
            //        if (xmlNode1.Name == "VchCode") Console.Write($"VchCode : {xmlNode1.InnerText} \t"); 
            //    }
            //    Console.WriteLine();
            //}

            //for (int i = 0; i < doc.ChildNodes.Count; i++)
            //{
            //    Console.WriteLine();
            //    for (int j = 0; j < doc.ChildNodes.Item(i).ChildNodes.Count; j++)
            //    {
            //        Console.Write(doc.ChildNodes.Item(i).ChildNodes.Item(j).ChildNodes.Item(0).Value);
            //        Console.Write("\t");
            //    }
            //    Console.WriteLine();

            //}
            Console.ReadLine();
        }
    }
}
