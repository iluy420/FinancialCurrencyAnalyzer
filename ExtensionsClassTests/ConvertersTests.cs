using Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExtensionsClassTests
{
    [TestClass]
    public class ConvertersTests
    {
        /// <summary>
        /// Тест метода проверки преобразуется ли в число double
        /// </summary>
        [TestMethod]
        public void TestMethod_IsConvertToDouble()
        {
            Assert.IsTrue(Converters.IsConvertToDouble("123,2324"));
            Assert.IsTrue(Converters.IsConvertToDouble("23323,2532"));
            Assert.IsTrue(Converters.IsConvertToDouble("124576433,2344"));
            Assert.IsTrue(Converters.IsConvertToDouble("533"));
            Assert.IsTrue(Converters.IsConvertToDouble("34,00"));

            Assert.IsFalse(Converters.IsConvertToDouble("21341adwq"));
            Assert.IsFalse(Converters.IsConvertToDouble("23.234"));
            Assert.IsFalse(Converters.IsConvertToDouble("1111111111111111111111111111111111111...234"));
            Assert.IsFalse(Converters.IsConvertToDouble("123,,2324"));
            Assert.IsFalse(Converters.IsConvertToDouble(""));
        }
    }
}
