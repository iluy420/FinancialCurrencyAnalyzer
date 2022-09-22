using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Extensions
{
    public class WorkingWithPasswords
    {
        #region Хеширование и шифрование
        /// <summary>
        /// Хеширует строку (SHA1)
        /// </summary>
        /// <param name="password"></param>
        /// <returns>Возвращает хеш (SHA1)</returns>
        public static string GetHashSHA1(string password)
        {
            using (var hash = SHA1.Create())
            {
                return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));
            }
        }
        #endregion

        //не готово!
        #region проверка пароля

        #endregion

    }
}
