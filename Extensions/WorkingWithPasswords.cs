using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System;

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

            if (password.Length < 6)
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

        #region Генерация паролей и ключей

        /// <summary>
        /// Создает случайный пароль указанной длинны 
        /// Не меньше 3 символов!
        /// </summary>
        /// <returns>Случайный пароль указанной длинны</returns>
        public static string GetGeneratePassword(uint lenghtPassword)
        {
            char[] upperCase = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            char[] lowerCase = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            char[] numbers = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            char[] specialСharacters = new char[] { '_', '-', '!'};

            char[] total = upperCase.Concat(lowerCase).ToArray();

            string result = "";

            Random rnd = new Random();
            result += total[rnd.Next(total.Length)];
            result += numbers[rnd.Next(numbers.Length)];
            result += specialСharacters[rnd.Next(specialСharacters.Length)];

            if (lenghtPassword > 3)
            {
                total.Concat(numbers).Concat(specialСharacters).ToArray();

                total = Enumerable.Repeat<int>(0, total.Length).Select(i => total[rnd.Next(total.Length)]).ToArray();
                total = Enumerable.Repeat<int>(0, total.Length).Select(i => total[rnd.Next(total.Length)]).ToArray();

                char[] chars = Enumerable.Repeat<int>(0,(int)lenghtPassword - 3).Select(i => total[rnd.Next(total.Length)]).ToArray();
                result += new string(chars);
            }

            result = new string(result.ToCharArray().OrderBy(x => Guid.NewGuid()).ToArray());

            return result;
        }

        /// <summary>
        /// Создает случайный буквенно-цифровой ключ указанной длинны 
        /// </summary>
        /// <returns>Случайный буквенно-цифровой ключ указанной длинны</returns>
        public static string GetGenerateAlphanumericKey(uint lenghtKey)
        {
            char[] upperCase = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            char[] lowerCase = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            char[] numbers = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            char[] total = upperCase.Concat(lowerCase).Concat(numbers).ToArray();

            Random rnd = new Random();

            total = Enumerable.Repeat<int>(0, total.Length).Select(i => total[rnd.Next(total.Length)]).ToArray();
            total = Enumerable.Repeat<int>(0, total.Length).Select(i => total[rnd.Next(total.Length)]).ToArray();

            char[] chars = Enumerable.Repeat<int>(0, (int)lenghtKey).Select(i => total[rnd.Next(total.Length)]).ToArray();
            string result = new string(chars);
            result = new string(result.ToCharArray().OrderBy(x => Guid.NewGuid()).ToArray());

            return result;
        }

        #endregion

    }
}
