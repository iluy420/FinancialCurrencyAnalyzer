using Microsoft.VisualStudio.TestTools.UnitTesting;
using Extensions;

namespace ExtensionsClassTests
{
    [TestClass]
    public class WorkingWithPasswordsTests
    {
        /// <summary>
        /// Тест метода получения хеша (GetHashSHA1)
        /// </summary>
        [TestMethod]
        public void TestMethod_GetHashSHA1()
        {
            Assert.AreEqual(WorkingWithPasswords.GetHashSHA1("O?F%5a@0{4"), "F7BD5DB24980DCE2C784BB6463B59861A34E57AA");
            Assert.AreEqual(WorkingWithPasswords.GetHashSHA1("~XKC~2kA{T"), "36F3D218E76E0DDCC3A83E319B02F4DF161EAFFA");
            Assert.AreEqual(WorkingWithPasswords.GetHashSHA1("PNR2g1Z##s"), "6F191C66FAA08D0C54FE2BD18050055A263B1F61");
            Assert.AreEqual(WorkingWithPasswords.GetHashSHA1("eX9hMSnJcx"), "6B6AD6DD35FF18134B154F49CC048DA9DFF7E264");
            Assert.AreEqual(WorkingWithPasswords.GetHashSHA1("RCZ~%QBkcz"), "7BE3CF114D295603DBEFEC0E716F795B0D7C9E6A");
        }
    }
}
