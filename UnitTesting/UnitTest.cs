using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FitnessCenter.Class;
using System.Drawing;
using System.Web;
using DataAccess.Model;
using DataAccess.Dao;

namespace UnitTesting
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void HashTest()
        {
            string heslo = "P&ssw0rd";
            string hashHeslo = PasswordHash.CreateHash(heslo);

            Assert.IsTrue(heslo != hashHeslo);
        }

        [TestMethod]
        public void ValidatePasswordTest()
        {
            string heslo = "P&ssw0rd";
            string hashHeslo = PasswordHash.CreateHash(heslo);

            bool isValid = PasswordHash.ValidatePassword(heslo, hashHeslo);
            Assert.IsTrue(isValid=true);
        }


    }
}
