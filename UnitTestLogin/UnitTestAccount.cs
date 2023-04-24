using BUS;
using DAL;
using DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTest
{
    [TestClass]
    public class UnitTestAccount
    {
        [TestMethod]
        public void TestHashPassword()
        {
            string password = "1";
            string hashPassword = SecurityUtils.SaltedHash(password);
            string expected = "D33D32916FD95CB7DCE6044096815D4013FCE0BA";

            Assert.AreEqual(expected, hashPassword);
        }
        
        [TestMethod]
        public void TestExistsAccount()
        {
            BUS_Account account = new BUS_Account();
            string userName = "Admin";
            string password = "1";
            string hashPassword = SecurityUtils.SaltedHash(password);


            Assert.IsTrue(account.Login(userName, hashPassword));
        }
        
        [TestMethod]
        public void TestNotExistsAccount()
        {
            BUS_Account account = new BUS_Account();
            string userName = "abcde";
            string password = "1234567";
            string hashPassword = SecurityUtils.SaltedHash(password);


            Assert.IsFalse(account.Login(userName, hashPassword));
        }

        [TestMethod]
        public void TestIsCheckAdmin()
        {
            BUS_Account account = new BUS_Account();
            string userName = "Admin";
            string password = "1";
            string hashPassword = SecurityUtils.SaltedHash(password);


            Assert.IsTrue(account.CheckAdmin(userName, hashPassword));
        }

        [TestMethod]
        public void TestNotCheckAdmin()
        {
            BUS_Account account = new BUS_Account();
            string userName = "user1";
            string password = "123456789";
            string hashPassword = SecurityUtils.SaltedHash(password);


            Assert.IsFalse(account.CheckAdmin(userName, hashPassword));
        }
    }
}
