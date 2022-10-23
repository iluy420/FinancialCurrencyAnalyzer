using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Extensions
{
    public class Converters
    {
        /// <summary>
        /// Проверка преобразуется ли в число double
        /// </summary>
        /// <param name="valueFrom"></param>
        /// <returns></returns>
        public static bool IsConvertToDouble(string valueFrom)
        {
            try
            {
                double valueTo = Convert.ToDouble(valueFrom);
                return true;
            }
            catch
            {
                MessageBox.Show("Введите верные значения!");
                return false;
            }
        }
    }
}
