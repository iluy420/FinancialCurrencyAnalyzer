using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace SendingAnEmailTestProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MailAddress fromAddress = new MailAddress("FinancialCurrencyAnalyzerTest1@mail.ru", "FinancialCurrencyAnalyzerTestProject");
            MailAddress toAddress = new MailAddress("FinancialCurrencyAnalyzerTest1@gmail.com", "Юзверь");
            MailMessage message = new MailMessage(fromAddress, toAddress)
            {
                Subject = "Test",
                Body = "Тебе пришел тестовый спам от Илюши:)"
            };

            SmtpClient smtpClient = new SmtpClient()
            {
                Host = "smtp.mail.ru",
                Port = 25,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, "tt9bazBhKb00igat0Tqx")
            };

            smtpClient.Send(message);
        }
    }
}
