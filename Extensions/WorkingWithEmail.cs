using System.Net.Mail;
using System.Net;
using Consts;

namespace Extensions
{
    public class WorkingWithEmail
    {
        #region сообщения пользователю

        /// <summary>
        /// Регистрация электронной почты. Отправка кода подтверждения пользователю
        /// </summary>
        /// <param name="toAddressUser"></param>
        /// <param name="login"></param>
        /// <param name="confirmationKey"></param>
        public static void EmailRegistration(string toAddressUser, string login, string confirmationKey)
        {
            MailAddress fromAddress = new MailAddress(ConstsEmail.MAIL_ADDRESS, "FinancialCurrencyAnalyzer");
            MailAddress toAddress = new MailAddress(toAddressUser, login);
            MailMessage message = new MailMessage(fromAddress, toAddress)
            {
                Subject = "Проверка электронной почты",
                IsBodyHtml = true,
                Body = $"Привет, {login}!</br>" +
                $"Чтобы продолжить регистрацию в FinancialCurrencyAnalyzer, пожалуйста, используй следующий код подтверждения: <b>{confirmationKey}</b></br>"
            };

            SendEmailMail(message);
        }

        #endregion

        #region отправки email писем

        /// <summary>
        /// Отправка email письма с Mail почты
        /// </summary>
        /// <param name="message"></param>
        private static void SendEmailMail(MailMessage message)
        {
            SmtpClient smtpClient = new SmtpClient()
            {
                Host = "smtp.mail.ru",
                Port = 25,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(ConstsEmail.MAIL_ADDRESS, ConstsEmail.MAIL_PASSWORD)
            };

            smtpClient.Send(message);
        }

        #endregion
    }
}
