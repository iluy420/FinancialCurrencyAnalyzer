using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

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

        #region проверка пароля

        /// <summary>
        /// Проверка надежности пароля
        /// </summary>
        /// <param name="password"></param>
        /// <returns>True - пароль надежный, False - пароль не надежный</returns>
        public static bool PasswordStrengthCheck(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите пароль!");
                return false;
            }

            if (password.Length <= 6)
            {
                MessageBox.Show("пароль слишком короткий, минимум 6 символов");
                return false;
            }

            bool letter = false;//буква
            bool en = true; // английская раскладка
            bool symbol = false; // символ
            bool number = false; // цифра

            for (int i = 0; i < password.Length; i++) // перебираем символы
            {
                if (password[i] >= 'A' && password[i] <= 'Z' || password[i] >= 'a' && password[i] <= 'z') letter = true; // если есть хотябы одна буква
                if (password[i] >= 'А' && password[i] <= 'Я' || password[i] >= 'а' && password[i] <= 'я') en = false; // если русская раскладка
                if (password[i] >= '0' && password[i] <= '9') number = true; // если цифры
                if (password[i] == '_' || password[i] == '-' || password[i] == '!') symbol = true; // если символ
            }

            if (!en)
            {
                MessageBox.Show("Доступна только английская раскладка"); // выводим сообщение
                return false;
            }

            if (!letter)
            {
                MessageBox.Show("Добавьте хотя бы одну букву"); // выводим сообщение
                return false;
            }

            if (!symbol)
            {
                MessageBox.Show("Добавьте один из следующих символов: -, _, !"); // выводим сообщение
                return false;
            }

            if (!number)
            {
                MessageBox.Show("Добавьте хотя бы одну цифру"); // выводим сообщение
                return false;
            }

            return true;
        }
          
        /// <summary>
        /// Проверка совпадения двух паролей
        /// </summary>
        /// <param name="firstPassword"></param>
        /// <param name="secondPassword"></param>
        /// <returns>True - пароли совпали, False - пароли не совпали</returns>
        public static bool IsMatchPasswords(string firstPassword, string secondPassword)
        {
            if (firstPassword != secondPassword)
            {
                MessageBox.Show("Пароли не совпадают!");
                return false;
            }

            return true;
        }
        
        #endregion

    }
}
