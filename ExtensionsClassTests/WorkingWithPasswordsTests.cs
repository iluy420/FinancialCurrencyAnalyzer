﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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

        /// <summary>
        /// Тест метода проверки совпадения двух паролей
        /// </summary>
        [TestMethod]
        public void TestMethod_IsMatchPasswords()
        {
            Assert.IsTrue(WorkingWithPasswords.IsMatchPasswords("O?F%5a@0{4", "O?F%5a@0{4"));
            Assert.IsTrue(WorkingWithPasswords.IsMatchPasswords("~XKC~2kA{T", "~XKC~2kA{T"));
            Assert.IsTrue(WorkingWithPasswords.IsMatchPasswords("eX9hMSnJcx", "eX9hMSnJcx"));
            Assert.IsFalse(WorkingWithPasswords.IsMatchPasswords("PNRZ##s", "PNR2g1Z##s"));
            Assert.IsFalse(WorkingWithPasswords.IsMatchPasswords("RCZ~%QBkcz", "RCZBkcz"));
            Assert.IsFalse(WorkingWithPasswords.IsMatchPasswords("", "RCZBkcz"));
            Assert.IsFalse(WorkingWithPasswords.IsMatchPasswords("RCZ~%QBkcz", ""));
        }

        /// <summary>
        /// Тест метода проверки надежности пароля
        /// </summary>
        [TestMethod]
        public void TestMethod_PasswordStrengthCheck()
        {
            //хороший пароль
            Assert.IsTrue(WorkingWithPasswords.PasswordStrengthCheck("32R_jsd2"));
            Assert.IsTrue(WorkingWithPasswords.PasswordStrengthCheck("32R66djh-"));
            //русские буквы
            Assert.IsFalse(WorkingWithPasswords.PasswordStrengthCheck("32R!jвd2"));
            Assert.IsFalse(WorkingWithPasswords.PasswordStrengthCheck("32R!jsdфс2"));
            //нет цифры
            Assert.IsFalse(WorkingWithPasswords.PasswordStrengthCheck("gdgdgd!_"));
            Assert.IsFalse(WorkingWithPasswords.PasswordStrengthCheck("dsfdssd!"));
            //нет буквы
            Assert.IsFalse(WorkingWithPasswords.PasswordStrengthCheck("232332!"));
            Assert.IsFalse(WorkingWithPasswords.PasswordStrengthCheck("234!7448"));
            //короткий пароль
            Assert.IsFalse(WorkingWithPasswords.PasswordStrengthCheck("1!a"));
            Assert.IsFalse(WorkingWithPasswords.PasswordStrengthCheck("4d-"));
            //нет нет спец символа
            Assert.IsFalse(WorkingWithPasswords.PasswordStrengthCheck("hhdsss23"));
            Assert.IsFalse(WorkingWithPasswords.PasswordStrengthCheck("3g5sfsd7d"));
            //пустая строка
            Assert.IsFalse(WorkingWithPasswords.PasswordStrengthCheck(""));
        }
    }
}
