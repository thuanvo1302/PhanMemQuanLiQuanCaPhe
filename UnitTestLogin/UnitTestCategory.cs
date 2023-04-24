using DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestCategory
{
    
    [TestClass]
    public class UnitTestCategory
    {
        
        [TestMethod]
        public void TestInsertCategiry()
        {
            DAL_Category category= new DAL_Category();
            string name = "abcde";

            Assert.IsTrue(category.InsertCategory(name));
        }

        [TestMethod]
        public void TestInsertExistsCategiry()
        {
            DAL_Category category = new DAL_Category();
            string name = "Cà phê";

            Assert.IsFalse(category.InsertCategory(name));
        }
    }
}
